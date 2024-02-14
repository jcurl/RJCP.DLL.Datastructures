namespace RJCP.Core
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class CrcTestInfo
    {
        private static void CalculateAndPrint(Crc16 crc)
        {
            try {
                Console.WriteLine("{0} Poly={1:X4}, Seed={2:X4}, Xor={3:X4}, Reflected={4}",
                    crc.GetType(), crc.Polynomial, crc.Seed, crc.FinalXor, crc.ReflectedIn);

                byte[] r = crc.ComputeHash(new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 });
                Console.WriteLine("    Message '123456789' = {0:X2} {1:X2}", r[0], r[1]);

                r = crc.ComputeHash(new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30 });
                Console.WriteLine("    Message '1234567890' = {0:X2} {1:X2}", r[0], r[1]);

                r = crc.ComputeHash(new byte[] { 0x41, 0x20, 0x54, 0x65, 0x73, 0x74, 0x20, 0x53, 0x74, 0x72, 0x69, 0x6e, 0x67 });
                Console.WriteLine("    Message 'A Test String' = {0:X2} {1:X2}", r[0], r[1]);
            } finally {
                crc.Dispose();
            }
        }

        [Test]
        public void Crc16_Info_AllOutputTest()
        {
            CalculateAndPrint(new Crc16Ibm());
            CalculateAndPrint(new Crc16CcittFalse());
            CalculateAndPrint(new Crc16CcittMCRF4XX());
            CalculateAndPrint(new Crc16CcittXModem());
            CalculateAndPrint(new Crc16AugCcitt());
            CalculateAndPrint(new Crc16CcittKermit());
            CalculateAndPrint(new Crc16CcittKermitLsb());
        }

        // When doing a dump, change the base class and call DumpTable()
        private class Crc16TableDump : Crc16CcittKermit
        {
            public void DumpTable()
            {
                Console.WriteLine("Poly={0:X4}, ReflectIn={1}", Polynomial, ReflectedIn);
                Console.WriteLine("        private static readonly ushort[] PrecomputedTable = {");

                for (int i = 0; i < 256; i += 16) {
                    Console.Write("            ");
                    for (int j = 0; j < 16; j++) {
                        Console.Write("0x{0:X4}", Table[i + j]);
                        if (j != 15 || i != 240) Console.Write(", ");
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("        };");
            }
        }

        [Test]
        public void Crc16_Info_DumpTable()
        {
            using (Crc16TableDump crcdump = new()) {
                crcdump.DumpTable();
            }
        }

        private class Crc32TableDump : Crc32Posix
        {
            public void DumpTable()
            {
                Console.WriteLine("Poly={0:X8}, ReflectIn={1}", Polynomial, ReflectedIn);
                Console.WriteLine("        private static readonly uint[] PrecomputedTable = {");

                for (int i = 0; i < 256; i += 8) {
                    Console.Write("            ");
                    for (int j = 0; j < 8; j++) {
                        Console.Write("0x{0:X8}", Table[i + j]);
                        if (j != 7 || i != 248) Console.Write(", ");
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("        };");
            }
        }

        [Test]
        public void Crc32_Info_DumpTable()
        {
            using (Crc32TableDump crcdump = new()) {
                crcdump.DumpTable();
            }
        }
    }
}
