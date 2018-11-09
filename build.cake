#tool "nuget:?package=OpenCover"
#tool "nuget:?package=Codecov&version=1.1.0"
#addin "nuget:?package=Cake.Codecov"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var assemblyVersion = "0.0.1";
var packageVersion = "0.0.1";

var artifactsDir = MakeAbsolute(Directory("artifacts"));
var testsResultsDir = artifactsDir.Combine(Directory("test-results"));
var packagesDir = artifactsDir.Combine(Directory("packages"));

EnsureDirectoryExists(artifactsDir);

var solutionPath = "./Brematic.Net.sln";

Task("Clean")
	.Does(() => 
	{
		CleanDirectory(artifactsDir);

		var settings = new DotNetCoreCleanSettings
		{
			Configuration = configuration
		};

		DotNetCoreClean(solutionPath, settings);
	});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

Task("SemVer")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        GitVersion gitVersion = GitVersion();

        assemblyVersion = gitVersion.AssemblySemVer;
        packageVersion = gitVersion.NuGetVersion;

        Information($"AssemblySemVer: {assemblyVersion}");
        Information($"NuGetVersion: {packageVersion}");
    });

Task("SetAppVeyorVersion")
    .IsDependentOn("Semver")
    .WithCriteria(() => AppVeyor.IsRunningOnAppVeyor)
    .Does(() =>
    {
        AppVeyor.UpdateBuildVersion(packageVersion);
    });

Task("Build")
    .IsDependentOn("SetAppVeyorVersion")
    .Does(() =>
    {
        var settings = new DotNetCoreBuildSettings
        {
            Configuration = configuration,
            NoIncremental = true,
            NoRestore = true,
            MSBuildSettings = new DotNetCoreMSBuildSettings()
                .SetVersion(assemblyVersion)
                .WithProperty("FileVersion", packageVersion)
                .WithProperty("InformationalVersion", packageVersion)
                .WithProperty("nowarn", "7035")
        };

        DotNetCoreBuild(solutionPath, settings);
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var settings = new DotNetCoreToolSettings();

        var argumentsBuilder = new ProcessArgumentBuilder()
            .Append("-configuration")
            .Append(configuration)
            .Append("-nobuild");

        var projectFiles = GetFiles("./tests/*/*Tests.csproj");

        foreach (var projectFile in projectFiles)
        {
            var testResultsFile = testsResultsDir.Combine($"{projectFile.GetFilenameWithoutExtension()}.xml");
            var arguments = $"{argumentsBuilder.Render()} -xml \"{testResultsFile}\"";

            DotNetCoreTool(projectFile, "xunit", arguments, settings);
        }
    })
    .DeferOnError();

Task("Cover")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var projects = GetFiles("./src/**/*.csproj");
        foreach(var project in projects)
        {
            TransformTextFile(project.FullPath, ">", "<")
                .WithToken("portable", ">full<")
                .Save(project.FullPath);
        }

        projects = GetFiles("./tests/**/*.csproj");
        var resultsFile = artifactsDir.CombineWithFilePath("coverage.xml");
        foreach(var project in projects)
        {
            OpenCover(
                x => x.DotNetCoreTest(
                     project.FullPath,
                     new DotNetCoreTestSettings() { 
                       Configuration = configuration, 
                       NoBuild = true 
                     }
                ),
                resultsFile,
                new OpenCoverSettings()
                {
                    ArgumentCustomization = args => args
                        .Append("-threshold:100")
                        .Append("-returntargetcode")
                        .Append("-hideskipped:Filter;Attribute"),
                    Register = "user",
                    MergeOutput = true,
                    OldStyle = true,
                    SkipAutoProps = true,
                    MergeByHash = true
                }
                    .WithFilter("+[Brematic.Net]*")
                    .WithFilter("-[Brematic.Net.Tests]*")
                    .WithFilter("-[xunit*]*")
                    .ExcludeByAttribute("*.ExcludeFromCodeCoverage*")
            );
        }

        Codecov(resultsFile.FullPath);
    });

Task("Pack")
    .IsDependentOn("Test")
    .WithCriteria(() => HasArgument("pack"))
    .Does(() =>
    {
        var settings = new DotNetCorePackSettings
        {
            Configuration = configuration,
            NoBuild = true,
            NoRestore = true,
            IncludeSymbols = true,
            OutputDirectory = packagesDir,
            MSBuildSettings = new DotNetCoreMSBuildSettings()
                .WithProperty("PackageVersion", packageVersion)
                .WithProperty("Copyright", $"Copyright Tobias Efinger {DateTime.Now.Year}")
        };

        FixProps();

        GetFiles("./src/*/*.csproj")
            .ToList()
            .ForEach(f => DotNetCorePack(f.FullPath, settings));
    });

Task("PublishAppVeyorArtifacts")
    .IsDependentOn("Pack")
    .WithCriteria(() => HasArgument("pack") && AppVeyor.IsRunningOnAppVeyor)
    .Does(() =>
    {
        CopyFiles($"{packagesDir}/*.nupkg", MakeAbsolute(Directory("./")), false);

        GetFiles($"./*.nupkg")
            .ToList()
            .ForEach(f => AppVeyor.UploadArtifact(f, new AppVeyorUploadArtifactsSettings { DeploymentName = "packages" }));
    });


Task("Default")
    .IsDependentOn("PublishAppVeyorArtifacts");

RunTarget(target);

private void FixProps()
{
    /* Workaround this issue: https://github.com/NuGet/Home/issues/4337
       `pack` does not respect the `Version` and ends up generating invalid
       `NuGet` packages when same-solution project dependencies
     */

    var restoreSettings = new DotNetCoreRestoreSettings
    {
        MSBuildSettings = new DotNetCoreMSBuildSettings()
            .WithProperty("Version", packageVersion)
            .WithProperty("Configuration", configuration)
    };

    DotNetCoreRestore(restoreSettings);
}