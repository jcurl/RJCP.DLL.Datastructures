namespace RJCP.Core
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class CrcTestPerf
    {
        [Test]
        [Category("Manual")]
        [Explicit("Performance Test")]
        public void Crc16_Performance()
        {
            // Initialize a set of data
            const int Rep = 5;

            byte[] data = new byte[100 * 1024 * 1024];
            Crc16 crc16;
            Random r = new Random(0);

            for (int i = 0; i < data.Length; i++) {
                data[i] = (byte)r.Next(255);
            }

            // Performance check for a CRC algorithm
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            using (crc16 = new Crc16Ibm()) {
                sw.Start();
                for (int c = 0; c < Rep; c++) {
                    crc16.ComputeHash(data);
                }
                sw.Stop();
            }

            // Print the results
            Console.WriteLine("Duration: {0}ms for {1}MB data, over {2} runs",
                sw.ElapsedMilliseconds / Rep, data.Length / 1024 / 1024, Rep);
            Console.WriteLine("Speed: {0:F} MB/sec",
                (double)data.Length * Rep / sw.ElapsedMilliseconds * 1000 / 1024 / 1024);
        }

        [Test]
        [Category("Manual")]
        [Explicit("Performance Test")]
        public void Crc32_Performance()
        {
            // Initialize a set of data
            const int rep = 5;

            byte[] data = new byte[100 * 1024 * 1024];
            Crc32 crc32;
            Random r = new Random(0);

            for (int i = 0; i < data.Length; i++) {
                data[i] = (byte)r.Next(255);
            }

            // Performance check for a CRC algorithm
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            using (crc32 = new Crc32Q()) {
                sw.Start();
                for (int c = 0; c < rep; c++) {
                    crc32.ComputeHash(data);
                }
                sw.Stop();
            }

            // Print the results
            Console.WriteLine("Duration: {0}ms for {1}MB data, over {2} runs",
                sw.ElapsedMilliseconds / rep, data.Length / 1024 / 1024, rep);
            Console.WriteLine("Speed: {0:F} MB/sec",
                (double)data.Length * rep / sw.ElapsedMilliseconds * 1000 / 1024 / 1024);
        }
    }
}
