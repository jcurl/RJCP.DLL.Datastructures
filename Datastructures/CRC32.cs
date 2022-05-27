namespace RJCP.Core
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// CRC32 Algorithm base class.
    /// </summary>
    /// <remarks>
    /// The code was originally found on the Internet, but there have been so many changes that the code can be
    /// considered my own work. In particular, the tables are generated using other tools, the algorithms are published
    /// in so many places, that it can't come from any particular code. In the end, the code was translated from C from
    /// http://www.ross.net/crc/download/crc_v3.txt
    /// </remarks>
    [CLSCompliant(false)]
    public class Crc32 : HashAlgorithm
    {
        private readonly uint m_Poly;
        private readonly uint[] m_Table;
        private readonly uint m_Seed;
        private readonly uint m_FinalXor;
        private readonly bool m_RefIn;
        private readonly bool m_RefOut;
        private readonly bool m_ByteSwap;

        /// <summary>
        /// Current hash value, that needs to be maintained between HashCore function calls.
        /// </summary>
        private uint m_Hash;

        /// <summary>
        /// Prepare the CRC16 base class using the polynomial provided.
        /// </summary>
        /// <param name="poly">The polynomial (not reflected).</param>
        /// <param name="seed">The initial value for the CRC.</param>
        /// <param name="finalXor">The final XOR before returning the CRC.</param>
        /// <param name="refIn">If the input should be reflected.</param>
        public Crc32(uint poly, uint seed, uint finalXor, bool refIn)
            : this(poly, seed, finalXor, refIn, refIn, false) { }

        /// <summary>
        /// Prepare the CRC16 base class using the precomputed table.
        /// </summary>
        /// <param name="poly">The polynomial (not reflected) - used for information only.</param>
        /// <param name="table">The precomputed table.</param>
        /// <param name="seed">The initial value for the CRC.</param>
        /// <param name="finalXor">The final XOR before returning the CRC.</param>
        /// <param name="refIn">If the table was calculated using reflected inputs.</param>
        public Crc32(uint poly, uint[] table, uint seed, uint finalXor, bool refIn)
            : this(poly, table, seed, finalXor, refIn, refIn, false) { }

        /// <summary>
        /// Prepare the CRC16 base class using the polynomial provided.
        /// </summary>
        /// <param name="poly">The polynomial (not reflected).</param>
        /// <param name="seed">The initial value for the CRC.</param>
        /// <param name="finalXor">The final XOR before returning the CRC.</param>
        /// <param name="refIn">If the table was calculated using reflected inputs.</param>
        /// <param name="refOut">If the output should be reflected.</param>
        /// <param name="byteSwap">If the output bytes need to be swapped.</param>
        public Crc32(uint poly, uint seed, uint finalXor, bool refIn, bool refOut, bool byteSwap)
        {
            m_Poly = poly;
            m_Table = refIn ?
                InitializeTableShiftRight(Reflect(poly, 32)) :
                InitializeTableShiftLeft(poly);
            m_Seed = seed;
            m_FinalXor = finalXor;
            m_RefIn = refIn;
            m_RefOut = refOut;
            m_ByteSwap = byteSwap;
            m_Hash = m_Seed;
        }

        /// <summary>
        /// Prepare the CRC16 base class using the precomputed table.
        /// </summary>
        /// <param name="poly">The polynomial (not reflected) - used for information only.</param>
        /// <param name="table">The precomputed table.</param>
        /// <param name="seed">The initial value for the CRC.</param>
        /// <param name="finalXor">The final XOR before returning the CRC.</param>
        /// <param name="refIn">If the table was calculated using reflected inputs.</param>
        /// <param name="refOut">If the output should be reflected.</param>
        /// <param name="byteSwap">If the output bytes need to be swapped.</param>
        public Crc32(uint poly, uint[] table, uint seed, uint finalXor, bool refIn, bool refOut, bool byteSwap)
        {
            m_Poly = poly;
            m_Table = table;
            m_Seed = seed;
            m_FinalXor = finalXor;
            m_RefIn = refIn;
            m_RefOut = refOut;
            m_ByteSwap = byteSwap;
            m_Hash = m_Seed;
        }

        /// <summary>
        /// The polynomial used for the calculation of the CRC.
        /// </summary>
        public uint Polynomial { get { return m_Poly; } }

        /// <summary>
        /// The initial seed used for the calculation of the CRC.
        /// </summary>
        public uint Seed { get { return m_Seed; } }

        /// <summary>
        /// A bit mask where an XOR operation is performed at the end of the CRC calculation.
        /// </summary>
        public uint FinalXor { get { return m_FinalXor; } }

        /// <summary>
        /// Indicates if the input is reflected (LSB / MSB reflected about the center).
        /// </summary>
        public bool ReflectedIn { get { return m_RefIn; } }

        /// <summary>
        /// Indicates if the output is reflected (LSB / MSB reflected about the center).
        /// </summary>
        public bool ReflectedOut { get { return m_RefOut; } }

        /// <summary>
        /// Gets the lookup table used for the CRC.
        /// </summary>
        protected uint[] Table { get { return m_Table; } }

        /// <summary>
        /// Initialize the CRC algorithm.
        /// </summary>
        public override void Initialize()
        {
            m_Hash = m_Seed;
        }

        /// <summary>
        /// Main functionality to calculate the CRC based on input data.
        /// </summary>
        /// <param name="array">The array containing the data to calculate the CRC for.</param>
        /// <param name="ibStart">The offset into the byte array from which to begin using data.</param>
        /// <param name="cbSize">The number of bytes in the byte array to use as data.</param>
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            m_Hash = m_RefIn ?
                CalculateHashShiftRight(m_Table, m_Hash, array, ibStart, cbSize) :
                CalculateHashShiftLeft(m_Table, m_Hash, array, ibStart, cbSize);
        }

        /// <summary>
        /// Perform any final actions on the hashing algorithm.
        /// </summary>
        /// <returns>Get the final CRC.</returns>
        protected override byte[] HashFinal()
        {
            bool doRefOut = m_RefIn ^ m_RefOut;

            byte[] hashBuffer = m_ByteSwap ?
                UInt32ToLittleEndianBytes((doRefOut ? Reflect(m_Hash, 32) : m_Hash) ^ m_FinalXor) :
                UInt32ToBigEndianBytes((doRefOut ? Reflect(m_Hash, 32) : m_Hash) ^ m_FinalXor);
            return hashBuffer;
        }

        /// <summary>
        /// Size of the CRC (32-bits).
        /// </summary>
        public override int HashSize
        {
            get { return 32; }
        }

        /// <summary>
        /// Basic CRC32 algorithm to generate a table based on a polynomial by shifting bits to the right.
        /// </summary>
        /// <param name="polynomial">The polynomial to use for the CRC.</param>
        /// <returns>
        /// An array of <see cref="uint"/> (256 entries) containing the table for faster CRC calculations.
        /// </returns>
        protected static uint[] InitializeTableShiftRight(uint polynomial)
        {
            uint[] createTable = new uint[256];
            for (int i = 0; i < 256; i++) {
                uint r = (uint)i;
                for (int j = 0; j < 8; j++) {
                    if ((r & 0x00000001) != 0) {
                        r = (r >> 1) ^ polynomial;
                    } else {
                        r >>= 1;
                    }
                }
                createTable[i] = r;
            }

            return createTable;
        }

        /// <summary>
        /// Basic CRC32 algorithm to generate a table based on a polynomial by shifting bits to the left.
        /// </summary>
        /// <param name="polynomial">The polynomial to use for the CRC.</param>
        /// <returns>
        /// An array of <see cref="uint"/> (256 entries) containing the table for faster CRC calculations.
        /// </returns>
        protected static uint[] InitializeTableShiftLeft(uint polynomial)
        {
            uint[] createTable = new uint[256];
            for (int i = 0; i < 256; i++) {
                uint r = (uint)(i << 24);
                for (int j = 0; j < 8; j++) {
                    if ((r & 0x80000000) != 0) {
                        r = (r << 1) ^ polynomial;
                    } else {
                        r <<= 1;
                    }
                }
                createTable[i] = r;
            }

            return createTable;
        }

        /// <summary>
        /// Calculate the CRC when given a CRC table and an initial seed.
        /// </summary>
        /// <param name="table">The CRC table calculated by InitializeTableShiftRight.</param>
        /// <param name="seed">The initial seed (or the state of the CRC register from the last calculation).</param>
        /// <param name="buffer">The buffer to calculate the CRC for.</param>
        /// <param name="offset">Offset into the buffer from where to start the calculations.</param>
        /// <param name="count">Number of bytes in the buffer to operate on.</param>
        /// <returns>
        /// The CRC of the value in <paramref name="buffer"/> based on the <paramref name="table"/> and the
        /// <paramref name="seed"/>.
        /// </returns>
        protected static uint CalculateHashShiftRight(uint[] table, uint seed, byte[] buffer, int offset, int count)
        {
            uint crc = seed;
            for (int i = offset; i < offset + count; i++) {
                unchecked {
                    crc = (crc >> 8) ^ table[(buffer[i] ^ crc) & 0xFF];
                }
            }
            return crc;
        }

        /// <summary>
        /// Calculate the CRC when given a CRC table and an initial seed.
        /// </summary>
        /// <param name="table">The CRC table calculated by InitializeTableShiftLeft().</param>
        /// <param name="seed">The initial seed (or the state of the CRC register from the last calculation).</param>
        /// <param name="buffer">The buffer to calculate the CRC for.</param>
        /// <param name="offset">Offset into the buffer from where to start the calculations.</param>
        /// <param name="count">Number of bytes in the buffer to operate on.</param>
        /// <returns>
        /// The CRC of the value in <paramref name="buffer"/> based on the <paramref name="table"/> and the
        /// <paramref name="seed"/>.
        /// </returns>
        protected static uint CalculateHashShiftLeft(uint[] table, uint seed, byte[] buffer, int offset, int count)
        {
            uint crc = seed;
            for (int i = offset; i < offset + count; i++) {
                unchecked {
                    crc = (crc << 8) ^ table[(buffer[i] ^ (crc >> 24)) & 0xFF];
                }
            }
            return crc;
        }

        /// <summary>
        /// Reflect a value about the center that is 'bits' long.
        /// </summary>
        /// <param name="value">The value to reflect.</param>
        /// <param name="bits">The size of the bits.</param>
        /// <returns>The reflected bytes.</returns>
        protected static uint Reflect(uint value, int bits)
        {
            uint t = value;
            for (int i = 0; i < bits; i++) {
                unchecked {
                    if ((t & 0x0001) != 0) {
                        value |= (uint)(1 << (bits - 1 - i));
                    } else {
                        value &= (uint)(~(1 << (bits - 1 - i)));
                    }
                    t >>= 1;
                }
            }
            return value;
        }

        /// <summary>
        /// Convert the <see cref="uint"/> to an array of four bytes in BigEndian.
        /// </summary>
        /// <param name="x">The <see cref="uint"/> to convert.</param>
        /// <returns>An array of four bytes.</returns>
        private static byte[] UInt32ToBigEndianBytes(uint x)
        {
            unchecked {
                return new byte[] {
                (byte)((x >> 24) & 0xff),
                (byte)((x >> 16) & 0xff),
                (byte)((x >> 8) & 0xff),
                (byte)(x & 0xff)};
            }
        }

        /// <summary>
        /// Convert the <see cref="uint"/> to an array of four bytes in LittleEndian.
        /// </summary>
        /// <param name="x">The <see cref="uint"/> to convert.</param>
        /// <returns>An array of four bytes.</returns>
        private static byte[] UInt32ToLittleEndianBytes(uint x)
        {
            unchecked {
                return new byte[] {
                (byte)(x & 0xff),
                (byte)((x >> 8) & 0xff),
                (byte)((x >> 16) & 0xff),
                (byte)((x >> 24) & 0xff)};
            }
        }

        /// <summary>
        /// Calculate the CRC32 on the buffer provided.
        /// </summary>
        /// <param name="buffer">The buffer to calculate the CRC32 for.</param>
        /// <returns>A CRC32 based on the contents of buffer.</returns>
        public virtual uint Compute(byte[] buffer)
        {
            return Compute(m_Seed, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Calculate the CRC32 on the buffer provided, with a user seed (e.g. the results of the last CRC32).
        /// </summary>
        /// <param name="seed">The seed to use for starting the calculation.</param>
        /// <param name="buffer">The buffer to calculate the CRC32 for.</param>
        /// <returns>A CRC32 based on the contents of buffer.</returns>
        public virtual uint Compute(uint seed, byte[] buffer)
        {
            return Compute(seed, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Calculate the CRC32 on the buffer provided, from the offset given for count bytes.
        /// </summary>
        /// <param name="buffer">The buffer to calculate the CRC32 for.</param>
        /// <param name="offset">Offset into the buffer to start the calculations.</param>
        /// <param name="count">Number of bytes to make the calculation for.</param>
        /// <returns>A CRC32 based on the contents of buffer.</returns>
        public virtual uint Compute(byte[] buffer, int offset, int count)
        {
            return Compute(m_Seed, buffer, offset, count);
        }

        /// <summary>
        /// Calculate the CRC32 on the buffer provided, from the offset given for count bytes.
        /// </summary>
        /// <param name="seed">The seed to use for starting the calculation.</param>
        /// <param name="buffer">The buffer to calculate the CRC32 for.</param>
        /// <param name="offset">Offset into the buffer to start the calculations.</param>
        /// <param name="count">Number of bytes to make the calculation for.</param>
        /// <returns>A CRC32 based on the contents of buffer.</returns>
        public virtual uint Compute(uint seed, byte[] buffer, int offset, int count)
        {
            uint crc = m_RefIn ?
                CalculateHashShiftRight(m_Table, seed, buffer, offset, count) :
                CalculateHashShiftLeft(m_Table, seed, buffer, offset, count);

            unchecked {
                if (m_RefIn ^ m_RefOut) crc = Reflect(crc, 32);
                crc ^= m_FinalXor;

                if (m_ByteSwap) crc = ((crc << 24) | (crc & 0xFF00 << 8) | (crc & 0xFF0000 >> 8) | (crc >> 24));
            }

            return crc;
        }
    }

    /// <summary>
    /// The standard CRC32 algorithm with a precomputed table.
    /// </summary>
    [CLSCompliant(false)]
    public class Crc32Standard : Crc32
    {
        private static readonly uint[] PrecomputedTable = {
            0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA, 0x076DC419, 0x706AF48F, 0xE963A535, 0x9E6495A3,
            0x0EDB8832, 0x79DCB8A4, 0xE0D5E91E, 0x97D2D988, 0x09B64C2B, 0x7EB17CBD, 0xE7B82D07, 0x90BF1D91,
            0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE, 0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7,
            0x136C9856, 0x646BA8C0, 0xFD62F97A, 0x8A65C9EC, 0x14015C4F, 0x63066CD9, 0xFA0F3D63, 0x8D080DF5,
            0x3B6E20C8, 0x4C69105E, 0xD56041E4, 0xA2677172, 0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B,
            0x35B5A8FA, 0x42B2986C, 0xDBBBC9D6, 0xACBCF940, 0x32D86CE3, 0x45DF5C75, 0xDCD60DCF, 0xABD13D59,
            0x26D930AC, 0x51DE003A, 0xC8D75180, 0xBFD06116, 0x21B4F4B5, 0x56B3C423, 0xCFBA9599, 0xB8BDA50F,
            0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924, 0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D,
            0x76DC4190, 0x01DB7106, 0x98D220BC, 0xEFD5102A, 0x71B18589, 0x06B6B51F, 0x9FBFE4A5, 0xE8B8D433,
            0x7807C9A2, 0x0F00F934, 0x9609A88E, 0xE10E9818, 0x7F6A0DBB, 0x086D3D2D, 0x91646C97, 0xE6635C01,
            0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E, 0x6C0695ED, 0x1B01A57B, 0x8208F4C1, 0xF50FC457,
            0x65B0D9C6, 0x12B7E950, 0x8BBEB8EA, 0xFCB9887C, 0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3, 0xFBD44C65,
            0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2, 0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB,
            0x4369E96A, 0x346ED9FC, 0xAD678846, 0xDA60B8D0, 0x44042D73, 0x33031DE5, 0xAA0A4C5F, 0xDD0D7CC9,
            0x5005713C, 0x270241AA, 0xBE0B1010, 0xC90C2086, 0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,
            0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4, 0x59B33D17, 0x2EB40D81, 0xB7BD5C3B, 0xC0BA6CAD,
            0xEDB88320, 0x9ABFB3B6, 0x03B6E20C, 0x74B1D29A, 0xEAD54739, 0x9DD277AF, 0x04DB2615, 0x73DC1683,
            0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8, 0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1,
            0xF00F9344, 0x8708A3D2, 0x1E01F268, 0x6906C2FE, 0xF762575D, 0x806567CB, 0x196C3671, 0x6E6B06E7,
            0xFED41B76, 0x89D32BE0, 0x10DA7A5A, 0x67DD4ACC, 0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5,
            0xD6D6A3E8, 0xA1D1937E, 0x38D8C2C4, 0x4FDFF252, 0xD1BB67F1, 0xA6BC5767, 0x3FB506DD, 0x48B2364B,
            0xD80D2BDA, 0xAF0A1B4C, 0x36034AF6, 0x41047A60, 0xDF60EFC3, 0xA867DF55, 0x316E8EEF, 0x4669BE79,
            0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236, 0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F,
            0xC5BA3BBE, 0xB2BD0B28, 0x2BB45A92, 0x5CB36A04, 0xC2D7FFA7, 0xB5D0CF31, 0x2CD99E8B, 0x5BDEAE1D,
            0x9B64C2B0, 0xEC63F226, 0x756AA39C, 0x026D930A, 0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713,
            0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38, 0x92D28E9B, 0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21,
            0x86D3D2D4, 0xF1D4E242, 0x68DDB3F8, 0x1FDA836E, 0x81BE16CD, 0xF6B9265B, 0x6FB077E1, 0x18B74777,
            0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C, 0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45,
            0xA00AE278, 0xD70DD2EE, 0x4E048354, 0x3903B3C2, 0xA7672661, 0xD06016F7, 0x4969474D, 0x3E6E77DB,
            0xAED16A4A, 0xD9D65ADC, 0x40DF0B66, 0x37D83BF0, 0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,
            0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6, 0xBAD03605, 0xCDD70693, 0x54DE5729, 0x23D967BF,
            0xB3667A2E, 0xC4614AB8, 0x5D681B02, 0x2A6F2B94, 0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B, 0x2D02EF8D
        };

        /// <summary>
        /// Initialize the standard 32-bit CRC using a precomputed table.
        /// </summary>
        public Crc32Standard() : base(0x04C11DB7, PrecomputedTable, 0xFFFFFFFF, 0xFFFFFFFF, true) { }

        /// <summary>
        /// Initialize the standard 32-bit CRC using a custom seed.
        /// </summary>
        /// <param name="seed">The seed to use for the CRC32.</param>
        public Crc32Standard(uint seed) : base(0x04C11DB7, PrecomputedTable, seed, 0xFFFFFFFF, true) { }

        /// <summary>
        /// Creates a new CRC32 instance.
        /// </summary>
        /// <returns>A newly created object.</returns>
        public static new Crc32Standard Create()
        {
            return new Crc32Standard();
        }
    }

    /// <summary>
    /// The standard CRC32Q algorithm with a precomputed table.
    /// </summary>
    [CLSCompliant(false)]
    public class Crc32Q : Crc32
    {
        private static readonly uint[] PrecomputedTable = {
            0x00000000, 0x814141AB, 0x83C3C2FD, 0x02828356, 0x86C6C451, 0x078785FA, 0x050506AC, 0x84444707,
            0x8CCCC909, 0x0D8D88A2, 0x0F0F0BF4, 0x8E4E4A5F, 0x0A0A0D58, 0x8B4B4CF3, 0x89C9CFA5, 0x08888E0E,
            0x98D8D3B9, 0x19999212, 0x1B1B1144, 0x9A5A50EF, 0x1E1E17E8, 0x9F5F5643, 0x9DDDD515, 0x1C9C94BE,
            0x14141AB0, 0x95555B1B, 0x97D7D84D, 0x169699E6, 0x92D2DEE1, 0x13939F4A, 0x11111C1C, 0x90505DB7,
            0xB0F0E6D9, 0x31B1A772, 0x33332424, 0xB272658F, 0x36362288, 0xB7776323, 0xB5F5E075, 0x34B4A1DE,
            0x3C3C2FD0, 0xBD7D6E7B, 0xBFFFED2D, 0x3EBEAC86, 0xBAFAEB81, 0x3BBBAA2A, 0x3939297C, 0xB87868D7,
            0x28283560, 0xA96974CB, 0xABEBF79D, 0x2AAAB636, 0xAEEEF131, 0x2FAFB09A, 0x2D2D33CC, 0xAC6C7267,
            0xA4E4FC69, 0x25A5BDC2, 0x27273E94, 0xA6667F3F, 0x22223838, 0xA3637993, 0xA1E1FAC5, 0x20A0BB6E,
            0xE0A08C19, 0x61E1CDB2, 0x63634EE4, 0xE2220F4F, 0x66664848, 0xE72709E3, 0xE5A58AB5, 0x64E4CB1E,
            0x6C6C4510, 0xED2D04BB, 0xEFAF87ED, 0x6EEEC646, 0xEAAA8141, 0x6BEBC0EA, 0x696943BC, 0xE8280217,
            0x78785FA0, 0xF9391E0B, 0xFBBB9D5D, 0x7AFADCF6, 0xFEBE9BF1, 0x7FFFDA5A, 0x7D7D590C, 0xFC3C18A7,
            0xF4B496A9, 0x75F5D702, 0x77775454, 0xF63615FF, 0x727252F8, 0xF3331353, 0xF1B19005, 0x70F0D1AE,
            0x50506AC0, 0xD1112B6B, 0xD393A83D, 0x52D2E996, 0xD696AE91, 0x57D7EF3A, 0x55556C6C, 0xD4142DC7,
            0xDC9CA3C9, 0x5DDDE262, 0x5F5F6134, 0xDE1E209F, 0x5A5A6798, 0xDB1B2633, 0xD999A565, 0x58D8E4CE,
            0xC888B979, 0x49C9F8D2, 0x4B4B7B84, 0xCA0A3A2F, 0x4E4E7D28, 0xCF0F3C83, 0xCD8DBFD5, 0x4CCCFE7E,
            0x44447070, 0xC50531DB, 0xC787B28D, 0x46C6F326, 0xC282B421, 0x43C3F58A, 0x414176DC, 0xC0003777,
            0x40005999, 0xC1411832, 0xC3C39B64, 0x4282DACF, 0xC6C69DC8, 0x4787DC63, 0x45055F35, 0xC4441E9E,
            0xCCCC9090, 0x4D8DD13B, 0x4F0F526D, 0xCE4E13C6, 0x4A0A54C1, 0xCB4B156A, 0xC9C9963C, 0x4888D797,
            0xD8D88A20, 0x5999CB8B, 0x5B1B48DD, 0xDA5A0976, 0x5E1E4E71, 0xDF5F0FDA, 0xDDDD8C8C, 0x5C9CCD27,
            0x54144329, 0xD5550282, 0xD7D781D4, 0x5696C07F, 0xD2D28778, 0x5393C6D3, 0x51114585, 0xD050042E,
            0xF0F0BF40, 0x71B1FEEB, 0x73337DBD, 0xF2723C16, 0x76367B11, 0xF7773ABA, 0xF5F5B9EC, 0x74B4F847,
            0x7C3C7649, 0xFD7D37E2, 0xFFFFB4B4, 0x7EBEF51F, 0xFAFAB218, 0x7BBBF3B3, 0x793970E5, 0xF878314E,
            0x68286CF9, 0xE9692D52, 0xEBEBAE04, 0x6AAAEFAF, 0xEEEEA8A8, 0x6FAFE903, 0x6D2D6A55, 0xEC6C2BFE,
            0xE4E4A5F0, 0x65A5E45B, 0x6727670D, 0xE66626A6, 0x622261A1, 0xE363200A, 0xE1E1A35C, 0x60A0E2F7,
            0xA0A0D580, 0x21E1942B, 0x2363177D, 0xA22256D6, 0x266611D1, 0xA727507A, 0xA5A5D32C, 0x24E49287,
            0x2C6C1C89, 0xAD2D5D22, 0xAFAFDE74, 0x2EEE9FDF, 0xAAAAD8D8, 0x2BEB9973, 0x29691A25, 0xA8285B8E,
            0x38780639, 0xB9394792, 0xBBBBC4C4, 0x3AFA856F, 0xBEBEC268, 0x3FFF83C3, 0x3D7D0095, 0xBC3C413E,
            0xB4B4CF30, 0x35F58E9B, 0x37770DCD, 0xB6364C66, 0x32720B61, 0xB3334ACA, 0xB1B1C99C, 0x30F08837,
            0x10503359, 0x911172F2, 0x9393F1A4, 0x12D2B00F, 0x9696F708, 0x17D7B6A3, 0x155535F5, 0x9414745E,
            0x9C9CFA50, 0x1DDDBBFB, 0x1F5F38AD, 0x9E1E7906, 0x1A5A3E01, 0x9B1B7FAA, 0x9999FCFC, 0x18D8BD57,
            0x8888E0E0, 0x09C9A14B, 0x0B4B221D, 0x8A0A63B6, 0x0E4E24B1, 0x8F0F651A, 0x8D8DE64C, 0x0CCCA7E7,
            0x044429E9, 0x85056842, 0x8787EB14, 0x06C6AABF, 0x8282EDB8, 0x03C3AC13, 0x01412F45, 0x80006EEE
        };

        /// <summary>
        /// Initialize the CRC32Q algorithm with a precomputed table.
        /// </summary>
        public Crc32Q() : base(0x814141AB, PrecomputedTable, 0x00000000, 0x00000000, false) { }

        /// <summary>
        /// Initialize the CRC32 algorithm with a precomputed table and custom seed.
        /// </summary>
        /// <param name="seed">The seed for the CRC calculation.</param>
        public Crc32Q(uint seed) : base(0x814141AB, PrecomputedTable, seed, 0x00000000, false) { }

        /// <summary>
        /// Creates a new CRC32 instance.
        /// </summary>
        /// <returns>A newly created object.</returns>
        public static new Crc32Q Create()
        {
            return new Crc32Q();
        }
    }

    /// <summary>
    /// The CRC32 Posix Algorithm.
    /// </summary>
    [CLSCompliant(false)]
    public class Crc32Posix : Crc32
    {
        private static readonly uint[] PrecomputedTable = {
            0x00000000, 0x04C11DB7, 0x09823B6E, 0x0D4326D9, 0x130476DC, 0x17C56B6B, 0x1A864DB2, 0x1E475005,
            0x2608EDB8, 0x22C9F00F, 0x2F8AD6D6, 0x2B4BCB61, 0x350C9B64, 0x31CD86D3, 0x3C8EA00A, 0x384FBDBD,
            0x4C11DB70, 0x48D0C6C7, 0x4593E01E, 0x4152FDA9, 0x5F15ADAC, 0x5BD4B01B, 0x569796C2, 0x52568B75,
            0x6A1936C8, 0x6ED82B7F, 0x639B0DA6, 0x675A1011, 0x791D4014, 0x7DDC5DA3, 0x709F7B7A, 0x745E66CD,
            0x9823B6E0, 0x9CE2AB57, 0x91A18D8E, 0x95609039, 0x8B27C03C, 0x8FE6DD8B, 0x82A5FB52, 0x8664E6E5,
            0xBE2B5B58, 0xBAEA46EF, 0xB7A96036, 0xB3687D81, 0xAD2F2D84, 0xA9EE3033, 0xA4AD16EA, 0xA06C0B5D,
            0xD4326D90, 0xD0F37027, 0xDDB056FE, 0xD9714B49, 0xC7361B4C, 0xC3F706FB, 0xCEB42022, 0xCA753D95,
            0xF23A8028, 0xF6FB9D9F, 0xFBB8BB46, 0xFF79A6F1, 0xE13EF6F4, 0xE5FFEB43, 0xE8BCCD9A, 0xEC7DD02D,
            0x34867077, 0x30476DC0, 0x3D044B19, 0x39C556AE, 0x278206AB, 0x23431B1C, 0x2E003DC5, 0x2AC12072,
            0x128E9DCF, 0x164F8078, 0x1B0CA6A1, 0x1FCDBB16, 0x018AEB13, 0x054BF6A4, 0x0808D07D, 0x0CC9CDCA,
            0x7897AB07, 0x7C56B6B0, 0x71159069, 0x75D48DDE, 0x6B93DDDB, 0x6F52C06C, 0x6211E6B5, 0x66D0FB02,
            0x5E9F46BF, 0x5A5E5B08, 0x571D7DD1, 0x53DC6066, 0x4D9B3063, 0x495A2DD4, 0x44190B0D, 0x40D816BA,
            0xACA5C697, 0xA864DB20, 0xA527FDF9, 0xA1E6E04E, 0xBFA1B04B, 0xBB60ADFC, 0xB6238B25, 0xB2E29692,
            0x8AAD2B2F, 0x8E6C3698, 0x832F1041, 0x87EE0DF6, 0x99A95DF3, 0x9D684044, 0x902B669D, 0x94EA7B2A,
            0xE0B41DE7, 0xE4750050, 0xE9362689, 0xEDF73B3E, 0xF3B06B3B, 0xF771768C, 0xFA325055, 0xFEF34DE2,
            0xC6BCF05F, 0xC27DEDE8, 0xCF3ECB31, 0xCBFFD686, 0xD5B88683, 0xD1799B34, 0xDC3ABDED, 0xD8FBA05A,
            0x690CE0EE, 0x6DCDFD59, 0x608EDB80, 0x644FC637, 0x7A089632, 0x7EC98B85, 0x738AAD5C, 0x774BB0EB,
            0x4F040D56, 0x4BC510E1, 0x46863638, 0x42472B8F, 0x5C007B8A, 0x58C1663D, 0x558240E4, 0x51435D53,
            0x251D3B9E, 0x21DC2629, 0x2C9F00F0, 0x285E1D47, 0x36194D42, 0x32D850F5, 0x3F9B762C, 0x3B5A6B9B,
            0x0315D626, 0x07D4CB91, 0x0A97ED48, 0x0E56F0FF, 0x1011A0FA, 0x14D0BD4D, 0x19939B94, 0x1D528623,
            0xF12F560E, 0xF5EE4BB9, 0xF8AD6D60, 0xFC6C70D7, 0xE22B20D2, 0xE6EA3D65, 0xEBA91BBC, 0xEF68060B,
            0xD727BBB6, 0xD3E6A601, 0xDEA580D8, 0xDA649D6F, 0xC423CD6A, 0xC0E2D0DD, 0xCDA1F604, 0xC960EBB3,
            0xBD3E8D7E, 0xB9FF90C9, 0xB4BCB610, 0xB07DABA7, 0xAE3AFBA2, 0xAAFBE615, 0xA7B8C0CC, 0xA379DD7B,
            0x9B3660C6, 0x9FF77D71, 0x92B45BA8, 0x9675461F, 0x8832161A, 0x8CF30BAD, 0x81B02D74, 0x857130C3,
            0x5D8A9099, 0x594B8D2E, 0x5408ABF7, 0x50C9B640, 0x4E8EE645, 0x4A4FFBF2, 0x470CDD2B, 0x43CDC09C,
            0x7B827D21, 0x7F436096, 0x7200464F, 0x76C15BF8, 0x68860BFD, 0x6C47164A, 0x61043093, 0x65C52D24,
            0x119B4BE9, 0x155A565E, 0x18197087, 0x1CD86D30, 0x029F3D35, 0x065E2082, 0x0B1D065B, 0x0FDC1BEC,
            0x3793A651, 0x3352BBE6, 0x3E119D3F, 0x3AD08088, 0x2497D08D, 0x2056CD3A, 0x2D15EBE3, 0x29D4F654,
            0xC5A92679, 0xC1683BCE, 0xCC2B1D17, 0xC8EA00A0, 0xD6AD50A5, 0xD26C4D12, 0xDF2F6BCB, 0xDBEE767C,
            0xE3A1CBC1, 0xE760D676, 0xEA23F0AF, 0xEEE2ED18, 0xF0A5BD1D, 0xF464A0AA, 0xF9278673, 0xFDE69BC4,
            0x89B8FD09, 0x8D79E0BE, 0x803AC667, 0x84FBDBD0, 0x9ABC8BD5, 0x9E7D9662, 0x933EB0BB, 0x97FFAD0C,
            0xAFB010B1, 0xAB710D06, 0xA6322BDF, 0xA2F33668, 0xBCB4666D, 0xB8757BDA, 0xB5365D03, 0xB1F740B4
        };

        /// <summary>
        /// Initialize the CRC32 Posix algorithm with a precomputed table.
        /// </summary>
        public Crc32Posix() : base(0x04C11DB7, PrecomputedTable, 0x00000000, 0xFFFFFFFF, false) { }

        /// <summary>
        /// Initialize the CRC32 Posix algorithm with a precomputed table and custom seed.
        /// </summary>
        /// <param name="seed">The seed to use for the CRC32.</param>
        public Crc32Posix(uint seed) : base(0x04C11DB7, PrecomputedTable, seed, 0xFFFFFFFF, false) { }

        /// <summary>
        /// Creates a new CRC32 instance.
        /// </summary>
        /// <returns>A newly created object.</returns>
        public static new Crc32Posix Create()
        {
            return new Crc32Posix();
        }
    }
}
