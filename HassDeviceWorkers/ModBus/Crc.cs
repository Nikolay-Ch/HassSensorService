using System;

namespace HassDeviceWorkers.ModBus
{
    public class Crc
    {
        protected static uint[] CrcTable = new uint[256]
        {
            0u, 49345u, 49537u, 320u, 49921u, 960u, 640u, 49729u, 50689u, 1728u,
            1920u, 51009u, 1280u, 50625u, 50305u, 1088u, 52225u, 3264u, 3456u, 52545u,
            3840u, 53185u, 52865u, 3648u, 2560u, 51905u, 52097u, 2880u, 51457u, 2496u,
            2176u, 51265u, 55297u, 6336u, 6528u, 55617u, 6912u, 56257u, 55937u, 6720u,
            7680u, 57025u, 57217u, 8000u, 56577u, 7616u, 7296u, 56385u, 5120u, 54465u,
            54657u, 5440u, 55041u, 6080u, 5760u, 54849u, 53761u, 4800u, 4992u, 54081u,
            4352u, 53697u, 53377u, 4160u, 61441u, 12480u, 12672u, 61761u, 13056u, 62401u,
            62081u, 12864u, 13824u, 63169u, 63361u, 14144u, 62721u, 13760u, 13440u, 62529u,
            15360u, 64705u, 64897u, 15680u, 65281u, 16320u, 16000u, 65089u, 64001u, 15040u,
            15232u, 64321u, 14592u, 63937u, 63617u, 14400u, 10240u, 59585u, 59777u, 10560u,
            60161u, 11200u, 10880u, 59969u, 60929u, 11968u, 12160u, 61249u, 11520u, 60865u,
            60545u, 11328u, 58369u, 9408u, 9600u, 58689u, 9984u, 59329u, 59009u, 9792u,
            8704u, 58049u, 58241u, 9024u, 57601u, 8640u, 8320u, 57409u, 40961u, 24768u,
            24960u, 41281u, 25344u, 41921u, 41601u, 25152u, 26112u, 42689u, 42881u, 26432u,
            42241u, 26048u, 25728u, 42049u, 27648u, 44225u, 44417u, 27968u, 44801u, 28608u,
            28288u, 44609u, 43521u, 27328u, 27520u, 43841u, 26880u, 43457u, 43137u, 26688u,
            30720u, 47297u, 47489u, 31040u, 47873u, 31680u, 31360u, 47681u, 48641u, 32448u,
            32640u, 48961u, 32000u, 48577u, 48257u, 31808u, 46081u, 29888u, 30080u, 46401u,
            30464u, 47041u, 46721u, 30272u, 29184u, 45761u, 45953u, 29504u, 45313u, 29120u,
            28800u, 45121u, 20480u, 37057u, 37249u, 20800u, 37633u, 21440u, 21120u, 37441u,
            38401u, 22208u, 22400u, 38721u, 21760u, 38337u, 38017u, 21568u, 39937u, 23744u,
            23936u, 40257u, 24320u, 40897u, 40577u, 24128u, 23040u, 39617u, 39809u, 23360u,
            39169u, 22976u, 22656u, 38977u, 34817u, 18624u, 18816u, 35137u, 19200u, 35777u,
            35457u, 19008u, 19968u, 36545u, 36737u, 20288u, 36097u, 19904u, 19584u, 35905u,
            17408u, 33985u, 34177u, 17728u, 34561u, 18368u, 18048u, 34369u, 33281u, 17088u,
            17280u, 33601u, 16640u, 33217u, 32897u, 16448u
        };

        public static byte[] QuickGetModbusCrc16(byte[] bytes)
        {
            ushort num = ushort.MaxValue;
            foreach (byte b in bytes)
                num = (ushort)((num >> 8) ^ CrcTable[(num ^ b) & 0xFF]);

            return BitConverter.GetBytes(num);
        }

        public static byte[] GetModbusCrc16(byte[] bytes)
        {
            byte crcRegister_H = 0xFF, crcRegister_L = 0xFF;//  Preset a 16-bit register for a value of 0xffff
            byte polynomialCode_H = 0xA0, polynomialCode_L = 0x01;//  Multi-model code 0xA001

            for (int i = 0; i < bytes.Length; i++)
            {
                crcRegister_L = (byte)(crcRegister_L ^ bytes[i]);

                for (int j = 0; j < 8; j++)
                {
                    byte tempCRC_H = crcRegister_H;
                    byte tempCRC_L = crcRegister_L;

                    crcRegister_H = (byte)(crcRegister_H >> 1);
                    crcRegister_L = (byte)(crcRegister_L >> 1);
                    //  The last one in the high right shift should be the first one after the low right shift: if the last bit is 1, the low right shift is added before 1
                    if ((tempCRC_H & 0x01) == 0x01)
                        crcRegister_L = (byte)(crcRegister_L | 0x80);

                    if ((tempCRC_L & 0x01) == 0x01)
                    {
                        crcRegister_H = (byte)(crcRegister_H ^ polynomialCode_H);
                        crcRegister_L = (byte)(crcRegister_L ^ polynomialCode_L);
                    }
                }
            }

            return new byte[] { crcRegister_L, crcRegister_H };
        }
    }
}
