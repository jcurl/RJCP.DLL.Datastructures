namespace RJCP.Core
{
    using System;
    using BenchmarkDotNet.Attributes;

    public class CRCBenchmark
    {
        private readonly byte[] m_Data;
        private readonly Crc16Ibm m_Crc16 = new Crc16Ibm();
        private readonly Crc32 m_Crc32 = new Crc32Standard();

        public CRCBenchmark()
        {
            m_Data = new byte[1024 * 1024];
            new Random(0).NextBytes(m_Data);
        }

        [Benchmark]
        public void CRC16() => m_Crc16.ComputeHash(m_Data);

        [Benchmark]
        public void CRC32() => m_Crc32.ComputeHash(m_Data);
    }
}
