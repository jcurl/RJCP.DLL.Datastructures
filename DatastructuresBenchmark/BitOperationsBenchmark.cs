namespace RJCP.Core
{
    using BenchmarkDotNet.Attributes;

    public class BitOperationsBenchmark
    {
        private readonly byte[] m_Buffer = new byte[128];

        [Benchmark]
        public void Copy16Pointer() => BitOperations.DangerousCopy16Pointer((short)0x1234, m_Buffer, 0);  // Make sure the input is a short, default is an int

        [Benchmark]
        public void Copy16PointerFrom32() => BitOperations.DangerousCopy16Pointer(0x1234, m_Buffer, 0);   // Default is an int

        [Benchmark]
        public void Copy32Pointer() => BitOperations.DangerousCopy32Pointer(0x12345678, m_Buffer, 0);

        [Benchmark]
        public void Copy64Pointer() => BitOperations.DangerousCopy64Pointer(0x1234567890abcdef, m_Buffer, 0);

        [Benchmark]
        public void Copy32FloatPointer() => BitOperations.DangerousCopy32FloatPointer(3.14159265359F, m_Buffer, 0);

        [Benchmark]
        public void Copy64FloatPointer() => BitOperations.DangerousCopy64FloatPointer(3.14159265359, m_Buffer, 0);

        [Benchmark]
        public void Copy16PointerSwap() => BitOperations.DangerousCopy16PointerSwap((short)0x1234, m_Buffer, 0);

        [Benchmark]
        public void Copy16PointerSwapFrom32() => BitOperations.DangerousCopy16PointerSwap(0x1234, m_Buffer, 0);

        [Benchmark]
        public void Copy32PointerSwap() => BitOperations.DangerousCopy32PointerSwap(0x12345678, m_Buffer, 0);

        [Benchmark]
        public void Copy64PointerSwap() => BitOperations.DangerousCopy64PointerSwap(0x1234567890abcdef, m_Buffer, 0);

        [Benchmark]
        public void Copy32FloatPointerSwap() => BitOperations.DangerousCopy32FloatPointerSwap(3.14159265359F, m_Buffer, 0);

        [Benchmark]
        public void Copy64FloatPointerSwap() => BitOperations.DangerousCopy64FloatPointerSwap(3.14159265359, m_Buffer, 0);

        [Benchmark]
        public void Copy16ShiftLE() => BitOperations.Copy16ShiftLittleEndian((short)0x1234, m_Buffer, 0);

        [Benchmark]
        public void Copy16ShiftLEFrom32() => BitOperations.Copy16ShiftLittleEndian(0x1234, m_Buffer, 0);

        [Benchmark]
        public void Copy32ShiftLE() => BitOperations.Copy32ShiftLittleEndian(0x12345678, m_Buffer, 0);

        [Benchmark]
        public void Copy64ShiftLE() => BitOperations.Copy64ShiftLittleEndian(0x1234567890abcdef, m_Buffer, 0);

        [Benchmark]
        public void Copy32FloatShiftLE() => BitOperations.Copy32FloatShiftLittleEndian(3.14159265359F, m_Buffer, 0);

        [Benchmark]
        public void Copy64FloatShiftLE() => BitOperations.Copy64FloatShiftLittleEndian(3.14159265359, m_Buffer, 0);

        [Benchmark]
        public void Copy16ShiftBE() => BitOperations.Copy16ShiftBigEndian((short)0x1234, m_Buffer, 0);

        [Benchmark]
        public void Copy16ShiftBEFrom32() => BitOperations.Copy16ShiftBigEndian(0x1234, m_Buffer, 0);

        [Benchmark]
        public void Copy32ShiftBE() => BitOperations.Copy32ShiftBigEndian(0x12345678, m_Buffer, 0);

        [Benchmark]
        public void Copy64ShiftBE() => BitOperations.Copy64ShiftBigEndian(0x1234567890abcdef, m_Buffer, 0);

        [Benchmark]
        public void Copy32FloatShiftBE() => BitOperations.Copy32FloatShiftBigEndian(3.14159265359F, m_Buffer, 0);

        [Benchmark]
        public void Copy64FloatShiftBE() => BitOperations.Copy64FloatShiftBigEndian(3.14159265359, m_Buffer, 0);

        [Benchmark]
        public void Copy16Shift() => BitOperations.Copy16Shift((short)0x1234, m_Buffer, 0, true);

        [Benchmark]
        public void Copy16ShiftFrom32() => BitOperations.Copy16Shift(0x1234, m_Buffer, 0, true);

        [Benchmark]
        public void Copy32Shift() => BitOperations.Copy32Shift(0x12345678, m_Buffer, 0, true);

        [Benchmark]
        public void Copy64Shift() => BitOperations.Copy64Shift(0x1234567890abcdef, m_Buffer, 0, true);

        [Benchmark]
        public void Copy32FloatShift() => BitOperations.Copy32FloatShift(3.14159265359F, m_Buffer, 0, true);

        [Benchmark]
        public void Copy64FloatShift() => BitOperations.Copy64FloatShift(3.14159265359, m_Buffer, 0, true);

#if NET6_0_OR_GREATER
        [Benchmark]
        public void Copy16ShiftLESpan() => BitOperations.Copy16ShiftLittleEndian((short)0x1234, m_Buffer);

        [Benchmark]
        public void Copy16ShiftLESpanFrom32() => BitOperations.Copy16ShiftLittleEndian(0x1234, m_Buffer);

        [Benchmark]
        public void Copy32ShiftLESpan() => BitOperations.Copy32ShiftLittleEndian(0x12345678, m_Buffer);

        [Benchmark]
        public void Copy64ShiftLESpan() => BitOperations.Copy64ShiftLittleEndian(0x1234567890abcdef, m_Buffer);

        [Benchmark]
        public void Copy32FloatShiftLESpan() => BitOperations.Copy32FloatShiftLittleEndian(3.14159265359F, m_Buffer);

        [Benchmark]
        public void Copy64FloatShiftLESpan() => BitOperations.Copy64FloatShiftLittleEndian(3.14159265359, m_Buffer);

        [Benchmark]
        public void Copy16ShiftBESpan() => BitOperations.Copy16ShiftBigEndian((short)0x1234, m_Buffer);

        [Benchmark]
        public void Copy16ShiftBESpanFrom32() => BitOperations.Copy16ShiftBigEndian(0x1234, m_Buffer);

        [Benchmark]
        public void Copy32ShiftBESpan() => BitOperations.Copy32ShiftBigEndian(0x12345678, m_Buffer);

        [Benchmark]
        public void Copy64ShiftBESpan() => BitOperations.Copy64ShiftBigEndian(0x1234567890abcdef, m_Buffer);

        [Benchmark]
        public void Copy32FloatShiftBESpan() => BitOperations.Copy32FloatShiftBigEndian(3.14159265359F, m_Buffer);

        [Benchmark]
        public void Copy64FloatShiftBESpan() => BitOperations.Copy64FloatShiftBigEndian(3.14159265359, m_Buffer);

        [Benchmark]
        public void Copy16ShiftSpan() => BitOperations.Copy16Shift((short)0x1234, m_Buffer, true);

        [Benchmark]
        public void Copy16ShiftSpanFrom32() => BitOperations.Copy16Shift(0x1234, m_Buffer, true);

        [Benchmark]
        public void Copy32ShiftSpan() => BitOperations.Copy32Shift(0x12345678, m_Buffer, true);

        [Benchmark]
        public void Copy64ShiftSpan() => BitOperations.Copy64Shift(0x1234567890abcdef, m_Buffer, true);

        [Benchmark]
        public void Copy32FloatShiftSpan() => BitOperations.Copy32FloatShift(3.14159265359F, m_Buffer, true);

        [Benchmark]
        public void Copy64FloatShiftSpan() => BitOperations.Copy64FloatShift(3.14159265359, m_Buffer, true);
#endif
    }
}
