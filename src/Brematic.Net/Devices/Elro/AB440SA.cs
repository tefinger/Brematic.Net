namespace Brematic.Net.Devices.Elro
{
    public class AB440SA : Device
    {
        public AB440SA(string systemCode, string unitCode) : base(systemCode, unitCode) { }

        protected internal override int SpeedBS => 14;

    }
}