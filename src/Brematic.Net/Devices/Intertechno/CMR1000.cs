using Brematic.Net.Exceptions;

namespace Brematic.Net.Devices.Intertechno
{
    public class CMR1000 : Device
    {
        public CMR1000(string systemCode, string unitCode) : base(systemCode, unitCode) { }

        protected internal override int Repeat => 6;
        protected internal override int PauseBS => 11125;
        protected internal override int PauseIT => 11125;
        protected internal override int Tune => 89;
        protected internal override int SpeedBS => 140;
        protected internal override int SpeedIT => 125;
        protected override string Low => "4,";
        protected override string High => "12,";
        protected override string SeqLow => Low + High + Low + High;
        protected override string SeqHigh => Low + High + High + Low;
        protected override string On => SeqHigh + SeqHigh;
        protected override string Off => SeqHigh + SeqLow;
        private string AdditionalMsg => SeqLow + SeqHigh;

        public override string GetSignal(string head, string tail, DeviceAction action)
        {
            var systemMsg = Encode(SystemCode);
            var unitMsg = Encode(UnitCode);

            var actionValue = action == DeviceAction.On ? On : Off;
            return head + systemMsg + unitMsg + AdditionalMsg + actionValue + tail;
        }

        protected internal override string Encode(string code)
        {
            var encodedMsg = "";
            foreach (var c in code)
                switch (c)
                {
                    case '0':
                        encodedMsg += SeqLow;
                        break;
                    case '1':
                        encodedMsg += SeqHigh;
                        break;
                    default:
                        throw new InvalidCodeException();
                }

            return encodedMsg;
        }
    }
}