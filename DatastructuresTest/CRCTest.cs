namespace RJCP.Core
{
    using NUnit.Framework;

    [TestFixture]
    public class CrcTest
    {
        [Test]
        public void CRC32_Test()
        {
            byte[] m = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30 };

            using (Crc32 crc32 = new Crc32Standard()) {
                byte[] r = crc32.ComputeHash(m, 0, m.Length);
                Assert.That(r.Length, Is.EqualTo(4));
                Assert.That(r[0], Is.EqualTo(0x26));
                Assert.That(r[1], Is.EqualTo(0x1D));
                Assert.That(r[2], Is.EqualTo(0xAE));
                Assert.That(r[3], Is.EqualTo(0xE5));
                Assert.That(crc32.Compute(m), Is.EqualTo((uint)0x261DAEE5));
            }

            // Check that calculating the CRC without a predetermined table also works
            using (Crc32 crc32 = new Crc32(0x04C11DB7, 0xFFFFFFFF, 0xFFFFFFFF, true)) {
                byte[] r = crc32.ComputeHash(m);
                Assert.That(r.Length, Is.EqualTo(4));
                Assert.That(r[0], Is.EqualTo(0x26));
                Assert.That(r[1], Is.EqualTo(0x1D));
                Assert.That(r[2], Is.EqualTo(0xAE));
                Assert.That(r[3], Is.EqualTo(0xE5));
                Assert.That(crc32.Compute(m), Is.EqualTo((uint)0x261DAEE5));
            }
        }

        [Test]
        public void CRC32Q_Test()
        {
            byte[] m = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 };

            using (Crc32 crc32 = new Crc32Q()) {
                byte[] r = crc32.ComputeHash(m);
                Assert.That(r.Length, Is.EqualTo(4));
                Assert.That(r[0], Is.EqualTo(0x30));
                Assert.That(r[1], Is.EqualTo(0x10));
                Assert.That(r[2], Is.EqualTo(0xBF));
                Assert.That(r[3], Is.EqualTo(0x7F));
                Assert.That(crc32.Compute(m), Is.EqualTo((uint)0x3010BF7F));
            }
        }

        [Test]
        public void CRC32Posix_Test()
        {
            byte[] m = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 };

            using (Crc32 crc32 = new Crc32Posix()) {
                byte[] r = crc32.ComputeHash(m);
                Assert.That(r.Length, Is.EqualTo(4));
                Assert.That(r[0], Is.EqualTo(0x76));
                Assert.That(r[1], Is.EqualTo(0x5E));
                Assert.That(r[2], Is.EqualTo(0x76));
                Assert.That(r[3], Is.EqualTo(0x80));
                Assert.That(crc32.Compute(m), Is.EqualTo((uint)0x765E7680));
            }
        }

        [Test]
        public void CRC16IBM_Test()
        {
            byte[] m = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30 };

            using (Crc16 crc16 = new Crc16Ibm()) {
                byte[] r = crc16.ComputeHash(m);
                Assert.That(r.Length, Is.EqualTo(2));
                Assert.That(r[0], Is.EqualTo(0xC5));
                Assert.That(r[1], Is.EqualTo(0x7A));
                Assert.That(crc16.Compute(m), Is.EqualTo(0xC57A));
            }
        }

        [Test]
        public void CRC16CcittFalse_Test()
        {
            byte[] m = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30 };

            using (Crc16 crc16 = new Crc16CcittFalse()) {
                byte[] r = crc16.ComputeHash(m);
                Assert.That(r.Length, Is.EqualTo(2));
                Assert.That(r[0], Is.EqualTo(0x32));
                Assert.That(r[1], Is.EqualTo(0x18));
                Assert.That(crc16.Compute(m), Is.EqualTo(0x3218));
            }
        }

        [Test]
        public void CRC16KermitLsb_Test()
        {
            byte[] m = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30 };

            using (Crc16 crc16 = new Crc16CcittKermitLsb()) {
                byte[] r = crc16.ComputeHash(m);
                Assert.That(r.Length, Is.EqualTo(2));
                Assert.That(r[0], Is.EqualTo(0x6B));
                Assert.That(r[1], Is.EqualTo(0x28));
                Assert.That(crc16.Compute(m), Is.EqualTo(0x6B28));
            }
        }

        [Test]
        public void CRC16Kermit_Test()
        {
            byte[] m = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30 };

            using (Crc16 crc16 = new Crc16CcittKermit()) {
                byte[] r = crc16.ComputeHash(m);
                Assert.That(r.Length, Is.EqualTo(2));
                Assert.That(r[0], Is.EqualTo(0x28));
                Assert.That(r[1], Is.EqualTo(0x6B));
                Assert.That(crc16.Compute(m), Is.EqualTo(0x286B));
            }
        }

        [Test]
        public void CRC16XModem_Test()
        {
            byte[] m = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30 };

            using (Crc16 crc16 = new Crc16CcittXModem()) {
                byte[] r = crc16.ComputeHash(m);
                Assert.That(r.Length, Is.EqualTo(2));
                Assert.That(r[0], Is.EqualTo(0xD3));
                Assert.That(r[1], Is.EqualTo(0x21));
                Assert.That(crc16.Compute(m), Is.EqualTo(0xD321));
            }
        }

        [Test]
        public void CRC16MCRF4xx_Test()
        {
            byte[] m = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30 };

            using (Crc16 crc16 = new Crc16CcittMCRF4XX()) {
                byte[] r = crc16.ComputeHash(m);
                Assert.That(r.Length, Is.EqualTo(2));
                Assert.That(r[0], Is.EqualTo(0xB4));
                Assert.That(r[1], Is.EqualTo(0xEC));
                Assert.That(crc16.Compute(m), Is.EqualTo(0xB4EC));
            }
        }

        [Test]
        public void CRC16Aug_Test()
        {
            byte[] m = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30 };

            using (Crc16 crc16 = new Crc16AugCcitt()) {
                byte[] r = crc16.ComputeHash(m);
                Assert.That(r.Length, Is.EqualTo(2));
                Assert.That(r[0], Is.EqualTo(0x57));
                Assert.That(r[1], Is.EqualTo(0xD8));
                Assert.That(crc16.Compute(m), Is.EqualTo(0x57D8));
            }
        }
    }
}
