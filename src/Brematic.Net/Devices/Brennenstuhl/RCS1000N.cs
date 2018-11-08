using Brematic.Net.Exceptions;
using Brematic.Net.Gateways;

namespace Brematic.Net.Devices.Brennenstuhl
{
    public class RCS1000N : Device
    {
        private const int Repeat = 10;
        private const int PauseBS = 5600;
        private const int PauseIT = 11200;
        private const int Tune = 350;
        private const int BaudBS = 25;
        private const int BaudIT = 26;
        private const int SpeedBS = 16;
        private const int SpeedIT = 32;
        private const int TxVersion = 1;

        private const string Low = "1,";
        private const string High = "3,";

        private const string SeqLow = Low + High + Low + High;
        private const string SeqHigh = Low + High + High + Low;

        private const string Additional = SeqLow + SeqHigh;
        private const string On = SeqLow + SeqHigh;
        private const string Off = SeqHigh + SeqLow;

        public RCS1000N(string systemCode, string unitCode) : base(systemCode, unitCode) { }

        private string HeadBSGW => $"TXP:0,0,{Repeat},{PauseBS},{Tune},{BaudBS},";
        private string TailBSGW => $"{TxVersion},{SpeedBS};";
        private string HeadITGW => $"0,0,{Repeat},{PauseIT},{Tune},{BaudIT},0,";
        private string TailITGW => $"{TxVersion},{SpeedIT},0";

        private string Encode(string code)
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

        public override string GetSignal(Gateway gateway, DeviceAction action)
        {
            var systemMsg = Encode(SystemCode);
            var unitMsg = Encode(UnitCode);

            string head;
            string tail;

            if (gateway.GetType() == typeof(BrennenstuhlGateway))
            {
                head = HeadBSGW;
                tail = TailBSGW;
            }
            else if (gateway.GetType() == typeof(IntertechnoGateway))
            {
                head = HeadITGW;
                tail = TailITGW;
            }
            else
            {
                throw new UnsupportedGatewayException();
            }

            var actionValue = action == DeviceAction.On ? On : Off;
            var data = head + systemMsg + unitMsg + actionValue + tail;

            return data;
        }
    }
}