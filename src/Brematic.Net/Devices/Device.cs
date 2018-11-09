using Brematic.Net.Exceptions;
using Brematic.Net.Gateways;

namespace Brematic.Net.Devices
{
    public abstract class Device
    {
        protected Device(string systemCode, string unitCode)
        {
            SystemCode = systemCode;
            UnitCode = unitCode;
        }

        protected internal virtual int Repeat => 10;
        protected internal virtual int PauseBS => 5600;
        protected internal virtual int PauseIT => 11200;
        protected internal virtual int Tune => 350;
        protected internal virtual int BaudBS => 25;
        protected internal virtual int BaudIT => 26;
        protected internal virtual int SpeedBS => 16;
        protected internal virtual int SpeedIT => 32;
        protected internal virtual int TxVersion => 1;

        protected virtual string Low => "1,";
        protected virtual string High => "3,";

        protected virtual string SeqLow => Low + High + Low + High;
        protected virtual string SeqHigh => Low + High + High + Low;

        protected virtual string On => SeqLow + SeqHigh;
        protected virtual string Off => SeqHigh + SeqLow;

        protected string SystemCode { get; }

        protected string UnitCode { get; }

        public virtual string GetSignal(string head, string tail, DeviceAction action)
        {
            var systemMsg = Encode(SystemCode);
            var unitMsg = Encode(UnitCode);

            var actionValue = action == DeviceAction.On ? On : Off;
            return head + systemMsg + unitMsg + actionValue + tail;
        }

        protected internal virtual string Encode(string code)
        {
            var encodedMsg = "";
            foreach (var c in code)
                switch (c)
                {
                    case '0':
                        encodedMsg += SeqHigh;
                        break;
                    case '1':
                        encodedMsg += SeqLow;
                        break;
                    default:
                        throw new InvalidCodeException();
                }

            return encodedMsg;
        }
    }
}