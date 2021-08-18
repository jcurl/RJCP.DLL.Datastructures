namespace RJCP.Core
{
    using System;
    using System.Diagnostics;
    using NUnit.Framework;

    /// <summary>
    /// The reverse operations for converting a byte array to its numerical representation are expected to have similar
    /// performance measurement results, since the methods instructions are the same, just in reverse order. For this
    /// reason they are not being tested here.
    /// </summary>
    [TestFixture]
    public class BitOperationsPerfTest
    {
        private const int Runs = 10;
        private const long Iterations = 500000000;

        private static long TestPerformance(long offset, int runs, long iterations, Action<byte[]> aTest, string aRun)
        {
            byte[] buffer = new byte[10];
            long best;

            Console.WriteLine("{0}: Running {1} iterations", aRun, iterations);
            Stopwatch sw = new Stopwatch();
            best = long.MaxValue;
            for (int run = 0; run < runs; run++) {
                sw.Restart();
                for (long index = 0; index < iterations; index++) {
                    aTest(buffer);
                }
                sw.Stop();
                if (sw.ElapsedMilliseconds < best) best = sw.ElapsedMilliseconds;
                Console.WriteLine("  #{0,-2}: {1,-4} {2,-4}", run + 1, sw.ElapsedMilliseconds, best);
            }
            Console.WriteLine("  BEST: {0}ms", best - offset);
            return best - offset;
        }

        private void Nothing(byte[] buffer) { /* Placebo benchmark test */ }

        private void Copy16Pointer(byte[] buffer)
        {
            BitOperations.DangerousCopy16Pointer(0x1234, buffer, 0);
        }

        private void Copy32Pointer(byte[] buffer)
        {
            BitOperations.DangerousCopy32Pointer(0x12345678, buffer, 0);
        }

        private void Copy64Pointer(byte[] buffer)
        {
            BitOperations.DangerousCopy64Pointer(0x1234567890abcdef, buffer, 0);
        }

        private void Copy32FloatPointer(byte[] buffer)
        {
            BitOperations.DangerousCopy32FloatPointer(3.14159265359F, buffer, 0);
        }

        private void Copy64FloatPointer(byte[] buffer)
        {
            BitOperations.DangerousCopy64FloatPointer(3.14159265359, buffer, 0);
        }

        private void Copy16PointerSwap(byte[] buffer)
        {
            BitOperations.DangerousCopy16PointerSwap(0x1234, buffer, 0);
        }

        private void Copy32PointerSwap(byte[] buffer)
        {
            BitOperations.DangerousCopy32PointerSwap(0x12345678, buffer, 0);
        }

        private void Copy64PointerSwap(byte[] buffer)
        {
            BitOperations.DangerousCopy64PointerSwap(0x1234567890abcdef, buffer, 0);
        }

        private void Copy32FloatPointerSwap(byte[] buffer)
        {
            BitOperations.DangerousCopy32FloatPointerSwap(3.14159265359F, buffer, 0);
        }

        private void Copy64FloatPointerSwap(byte[] buffer)
        {
            BitOperations.DangerousCopy64FloatPointerSwap(3.14159265359, buffer, 0);
        }

        private void Copy8Simple(byte[] buffer)
        {
            BitOperations.Copy8Simple(0x34, buffer, 0);
        }

        private void Copy16ShiftLE(byte[] buffer)
        {
            BitOperations.Copy16ShiftLittleEndian(0x1234, buffer, 0);
        }

        private void Copy32ShiftLE(byte[] buffer)
        {
            BitOperations.Copy32ShiftLittleEndian(0x12345678, buffer, 0);
        }

        private void Copy64ShiftLE(byte[] buffer)
        {
            BitOperations.Copy64ShiftLittleEndian(0x1234567890abcdef, buffer, 0);
        }

        private void Copy32FloatShiftLE(byte[] buffer)
        {
            BitOperations.Copy32FloatShiftLittleEndian(3.14159265359F, buffer, 0);
        }

        private void Copy64FloatShiftLE(byte[] buffer)
        {
            BitOperations.Copy64FloatShiftLittleEndian(3.14159265359, buffer, 0);
        }

        private void Copy16ShiftBE(byte[] buffer)
        {
            BitOperations.Copy16ShiftBigEndian(0x1234, buffer, 0);
        }

        private void Copy32ShiftBE(byte[] buffer)
        {
            BitOperations.Copy32ShiftBigEndian(0x12345678, buffer, 0);
        }

        private void Copy64ShiftBE(byte[] buffer)
        {
            BitOperations.Copy64ShiftBigEndian(0x1234567890abcdef, buffer, 0);
        }

        private void Copy32FloatShiftBE(byte[] buffer)
        {
            BitOperations.Copy32FloatShiftBigEndian(3.14159265359F, buffer, 0);
        }

        private void Copy64FloatShiftBE(byte[] buffer)
        {
            BitOperations.Copy64FloatShiftBigEndian(3.14159265359, buffer, 0);
        }

        private void Copy16Shift(byte[] buffer)
        {
            BitOperations.Copy16Shift(0x1234, buffer, 0, true);
        }

        private void Copy32Shift(byte[] buffer)
        {
            BitOperations.Copy32Shift(0x12345678, buffer, 0, true);
        }

        private void Copy64Shift(byte[] buffer)
        {
            BitOperations.Copy64Shift(0x1234567890abcdef, buffer, 0, true);
        }

        private void Copy32FloatShift(byte[] buffer)
        {
            BitOperations.Copy32FloatShift(3.14159265359F, buffer, 0, true);
        }

        private void Copy64FloatShift(byte[] buffer)
        {
            BitOperations.Copy64FloatShift(3.14159265359, buffer, 0, true);
        }

        [Test]
        [Category("Manual")]
        [Explicit("Performance test")]
        public void CopyPerf()
        {
            Console.WriteLine("OS: {0}", Environment.Is64BitOperatingSystem ? "x64" : "x86");
            Console.WriteLine("Process: {0}", Environment.Is64BitProcess ? "x64" : "x86");

            long nothing = TestPerformance(0, Runs * 4, Iterations, Nothing, "Nothing");

            TestPerformance(nothing, Runs, Iterations, Copy8Simple, "Copy8Simple");

            TestPerformance(nothing, Runs, Iterations, Copy16Pointer, "Copy16Pointer");
            TestPerformance(nothing, Runs, Iterations, Copy16PointerSwap, "Copy16PointerSwap");
            TestPerformance(nothing, Runs, Iterations, Copy16ShiftLE, "Copy16ShiftLE");
            TestPerformance(nothing, Runs, Iterations, Copy16ShiftBE, "Copy16ShiftBE");
            TestPerformance(nothing, Runs, Iterations, Copy16Shift, "Copy16Shift");

            TestPerformance(nothing, Runs, Iterations, Copy32Pointer, "Copy32Pointer");
            TestPerformance(nothing, Runs, Iterations, Copy32PointerSwap, "Copy32PointerSwap");
            TestPerformance(nothing, Runs, Iterations, Copy32ShiftLE, "Copy32ShiftLE");
            TestPerformance(nothing, Runs, Iterations, Copy32ShiftBE, "Copy32ShiftBE");
            TestPerformance(nothing, Runs, Iterations, Copy32Shift, "Copy32Shift");

            TestPerformance(nothing, Runs, Iterations, Copy64Pointer, "Copy64Pointer");
            TestPerformance(nothing, Runs, Iterations, Copy64PointerSwap, "Copy64PointerSwap");
            TestPerformance(nothing, Runs, Iterations, Copy64ShiftLE, "Copy64ShiftLE");
            TestPerformance(nothing, Runs, Iterations, Copy64ShiftBE, "Copy64ShiftBE");
            TestPerformance(nothing, Runs, Iterations, Copy64Shift, "Copy64Shift");

            TestPerformance(nothing, Runs, Iterations, Copy32FloatPointer, "Copy32FloatPointer");
            TestPerformance(nothing, Runs, Iterations, Copy32FloatPointerSwap, "Copy32FloatPointerSwap");
            TestPerformance(nothing, Runs, Iterations, Copy32FloatShiftLE, "Copy32FloatShiftLE");
            TestPerformance(nothing, Runs, Iterations, Copy32FloatShiftBE, "Copy32FloatShiftBE");
            TestPerformance(nothing, Runs, Iterations, Copy32FloatShift, "Copy32FloatShift");

            TestPerformance(nothing, Runs, Iterations, Copy64FloatPointer, "Copy64FloatPointer");
            TestPerformance(nothing, Runs, Iterations, Copy64FloatPointerSwap, "Copy64FloatPointerSwap");
            TestPerformance(nothing, Runs, Iterations, Copy64FloatShiftLE, "Copy64FloatShiftLE");
            TestPerformance(nothing, Runs, Iterations, Copy64FloatShiftBE, "Copy64FloatShiftBE");
            TestPerformance(nothing, Runs, Iterations, Copy64FloatShift, "Copy64FloatShift");
        }
    }
}
