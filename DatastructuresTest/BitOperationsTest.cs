namespace RJCP.Core
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class BitOperationsTest
    {
        #region Unsafe Copy
        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16PointerLongInput(long number)
        {
            byte[] actual = new byte[2];
            BitOperations.DangerousCopy16Pointer(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16PointerLongInput(int number)
        {
            byte[] actual = new byte[2];
            BitOperations.DangerousCopy16Pointer(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(unchecked((short)0xFECD))]
        public void Copy16PointerShortInput(short number)
        {
            byte[] actual = new byte[2];
            BitOperations.DangerousCopy16Pointer(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(0xFECD5678)]
        public void Copy32PointerLongInput(long number)
        {
            byte[] actual = new byte[4];
            BitOperations.DangerousCopy32Pointer(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(unchecked((int)number));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(unchecked((int)0xFECD5678))]
        public void Copy32PointerIntInput(int number)
        {
            byte[] actual = new byte[4];
            BitOperations.DangerousCopy32Pointer(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x123456780ABCDEF0)]
        [TestCase(unchecked((long)0xFEDCBA0123456789))]
        public void Copy64Pointer(long number)
        {
            byte[] actual = new byte[8];
            BitOperations.DangerousCopy64Pointer(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(12.45F)]
        [TestCase(float.NegativeInfinity)]
        [TestCase(float.PositiveInfinity)]
        [TestCase(float.Epsilon)]
        [TestCase(float.NaN)]
        [TestCase(float.MinValue)]
        [TestCase(float.MaxValue)]
        public void Copy32FloatPointer(float number)
        {
            byte[] actual = new byte[4];
            BitOperations.DangerousCopy32FloatPointer(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(52.65)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.Epsilon)]
        [TestCase(double.NaN)]
        [TestCase(double.MinValue)]
        [TestCase(double.MaxValue)]
        public void Copy64FloatPointer(double number)
        {
            byte[] actual = new byte[8];
            BitOperations.DangerousCopy64FloatPointer(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);

            Assert.That(actual, Is.EqualTo(expected));
        }
        #endregion

        #region Copy by Swapping
        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16SwapLongInput(long number)
        {
            byte[] actual = new byte[2];
            BitOperations.DangerousCopy16PointerSwap(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16SwapLongInput(int number)
        {
            byte[] actual = new byte[2];
            BitOperations.DangerousCopy16PointerSwap(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(unchecked((short)0xFECD))]
        public void Copy16SwapShortInput(short number)
        {
            byte[] actual = new byte[2];
            BitOperations.DangerousCopy16PointerSwap(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(0xFECD5678)]
        public void Copy32SwapLongInput(long number)
        {
            byte[] actual = new byte[4];
            BitOperations.DangerousCopy32PointerSwap(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(unchecked((int)number));
            Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(unchecked((int)0xFECD5678))]
        public void Copy32SwapIntInput(int number)
        {
            byte[] actual = new byte[4];
            BitOperations.DangerousCopy32PointerSwap(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x123456780ABCDEF0)]
        [TestCase(unchecked((long)0xFEDCBA0123456789))]
        public void Copy64Swap(long number)
        {
            byte[] actual = new byte[8];
            BitOperations.DangerousCopy64PointerSwap(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(12.45F)]
        [TestCase(float.NegativeInfinity)]
        [TestCase(float.PositiveInfinity)]
        [TestCase(float.Epsilon)]
        [TestCase(float.NaN)]
        [TestCase(float.MinValue)]
        [TestCase(float.MaxValue)]
        public void Copy32FloatPointerSwap(float number)
        {
            byte[] actual = new byte[4];
            BitOperations.DangerousCopy32FloatPointerSwap(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(52.65)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.Epsilon)]
        [TestCase(double.NaN)]
        [TestCase(double.MinValue)]
        [TestCase(double.MaxValue)]
        public void Copy64FloatPointerSwap(double number)
        {
            byte[] actual = new byte[8];
            BitOperations.DangerousCopy64FloatPointerSwap(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }
        #endregion

        [TestCase(0x3F)]
        [TestCase(0xF3)]
        public void Copy8Simple(byte octet)
        {
            byte[] actual = new byte[1];
            BitOperations.Copy8Simple(octet, actual, 0);
            Assert.That(actual[0], Is.EqualTo(octet));
        }

        #region Copy by Shifting
        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16ShiftLittleEndianLongInput(long number)
        {
            byte[] actual = new byte[2];
            BitOperations.Copy16ShiftLittleEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16ShiftLittleEndianIntInput(int number)
        {
            byte[] actual = new byte[2];
            BitOperations.Copy16ShiftLittleEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(unchecked((short)0xFECD))]
        public void Copy16ShiftLittleEndianShortInput(short number)
        {
            byte[] actual = new byte[2];
            BitOperations.Copy16ShiftLittleEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(0xFECD5678)]
        public void Copy32ShiftLittleEndianLongInput(long number)
        {
            byte[] actual = new byte[4];
            BitOperations.Copy32ShiftLittleEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(unchecked((int)number));
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(unchecked((int)0xFECD5678))]
        public void Copy32ShiftLittleEndianIntInput(int number)
        {
            byte[] actual = new byte[4];
            BitOperations.Copy32ShiftLittleEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x123456780ABCDEF0)]
        [TestCase(unchecked((long)0xFEDCBA0123456789))]
        public void Copy64ShiftLittleEndian(long number)
        {
            byte[] actual = new byte[8];
            BitOperations.Copy64ShiftLittleEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(12.45F)]
        [TestCase(float.NegativeInfinity)]
        [TestCase(float.PositiveInfinity)]
        [TestCase(float.Epsilon)]
        [TestCase(float.NaN)]
        [TestCase(float.MinValue)]
        [TestCase(float.MaxValue)]
        public void Copy32FloatShiftLittleEndian(float number)
        {
            byte[] actual = new byte[4];
            BitOperations.Copy32FloatShiftLittleEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(52.65)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.Epsilon)]
        [TestCase(double.NaN)]
        [TestCase(double.MinValue)]
        [TestCase(double.MaxValue)]
        public void Copy64FloatShiftLittleEndian(double number)
        {
            byte[] actual = new byte[8];
            BitOperations.Copy64FloatShiftLittleEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16ShiftBigEndianLongInput(long number)
        {
            byte[] actual = new byte[2];
            BitOperations.Copy16ShiftBigEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16ShiftBigEndianIntInput(int number)
        {
            byte[] actual = new byte[2];
            BitOperations.Copy16ShiftBigEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(unchecked((short)0xFECD))]
        public void Copy16ShiftBigEndianShortInput(short number)
        {
            byte[] actual = new byte[2];
            BitOperations.Copy16ShiftBigEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(0xFECD5678)]
        public void Copy32ShiftBigEndianLongInput(long number)
        {
            byte[] actual = new byte[4];
            BitOperations.Copy32ShiftBigEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(unchecked((int)number));
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(unchecked((int)0xFECD5678))]
        public void Copy32ShiftBigEndianIntInput(int number)
        {
            byte[] actual = new byte[4];
            BitOperations.Copy32ShiftBigEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x123456780ABCDEF0)]
        [TestCase(unchecked((long)0xFEDCBA0123456789))]
        public void Copy64ShiftBigEndian(long number)
        {
            byte[] actual = new byte[8];
            BitOperations.Copy64ShiftBigEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(12.45F)]
        [TestCase(float.NegativeInfinity)]
        [TestCase(float.PositiveInfinity)]
        [TestCase(float.Epsilon)]
        [TestCase(float.NaN)]
        [TestCase(float.MinValue)]
        [TestCase(float.MaxValue)]
        public void Copy32FloatShiftBigEndian(float number)
        {
            byte[] actual = new byte[4];
            BitOperations.Copy32FloatShiftBigEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(52.65)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.Epsilon)]
        [TestCase(double.NaN)]
        [TestCase(double.MinValue)]
        [TestCase(double.MaxValue)]
        public void Copy64FloatShiftBigEndian(double number)
        {
            byte[] actual = new byte[8];
            BitOperations.Copy64FloatShiftBigEndian(number, actual, 0);
            byte[] expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16ShiftLongInput(long number)
        {
            byte[] actual = new byte[2];
            BitOperations.Copy16Shift(number, actual, 0, true);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));

            BitOperations.Copy16Shift(number, actual, 0, false);
            expected = BitConverter.GetBytes(unchecked((short)number));
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16ShiftIntInput(int number)
        {
            byte[] actual = new byte[2];
            BitOperations.Copy16Shift(number, actual, 0, true);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));

            BitOperations.Copy16Shift(number, actual, 0, false);
            expected = BitConverter.GetBytes(unchecked((short)number));
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(unchecked((short)0xFECD))]
        public void Copy16ShiftShortInput(short number)
        {
            byte[] actual = new byte[2];
            BitOperations.Copy16Shift(number, actual, 0, true);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));

            BitOperations.Copy16Shift(number, actual, 0, false);
            expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(0xFECD5678)]
        public void Copy32ShiftLongInput(long number)
        {
            byte[] actual = new byte[4];
            BitOperations.Copy32Shift(number, actual, 0, true);
            byte[] expected = BitConverter.GetBytes(unchecked((int)number));
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));

            BitOperations.Copy32Shift(number, actual, 0, false);
            expected = BitConverter.GetBytes(unchecked((int)number));
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(unchecked((int)0xFECD5678))]
        public void Copy32ShiftIntInput(int number)
        {
            byte[] actual = new byte[4];
            BitOperations.Copy32Shift(number, actual, 0, true);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));

            BitOperations.Copy32Shift(number, actual, 0, false);
            expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0x123456780ABCDEF0)]
        [TestCase(unchecked((long)0xFEDCBA0123456789))]
        public void Copy64ShiftIntInput(long number)
        {
            byte[] actual = new byte[8];
            BitOperations.Copy64Shift(number, actual, 0, true);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));

            BitOperations.Copy64Shift(number, actual, 0, false);
            expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(12.45F)]
        [TestCase(float.NegativeInfinity)]
        [TestCase(float.PositiveInfinity)]
        [TestCase(float.Epsilon)]
        [TestCase(float.NaN)]
        [TestCase(float.MinValue)]
        [TestCase(float.MaxValue)]
        public void Copy32FloatShift(float number)
        {
            byte[] actual = new byte[4];
            BitOperations.Copy32FloatShift(number, actual, 0, true);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));

            BitOperations.Copy32FloatShift(number, actual, 0, false);
            expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(52.65)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.Epsilon)]
        [TestCase(double.NaN)]
        [TestCase(double.MinValue)]
        [TestCase(double.MaxValue)]
        public void Copy64FloatShift(double number)
        {
            byte[] actual = new byte[8];
            BitOperations.Copy64FloatShift(number, actual, 0, true);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));

            BitOperations.Copy64FloatShift(number, actual, 0, false);
            expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual, Is.EqualTo(expected));
        }
        #endregion

        [TestCase(new byte[] { 0x05 }, 0, 5)]
        [TestCase(new byte[] { 0x05, 0x03 }, 1, 3)]
        public void To8Simple(byte[] buffer, int offset, long expectedResult)
        {
            long result = BitOperations.To8Simple(buffer, offset);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        #region Unsafe Conversion
        [TestCase(new byte[] { 0x05, 0x03 }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF }, 0, TestName = "DangerousTo16Pointer_Overflow")]
        [TestCase(new byte[] { 0x05, 0x03, 0x11, 0x22 }, 2)]
        [TestCase(new byte[] { 0x05, 0x03, 0x11 }, 1)]
        public void DangerousTo16Pointer(byte[] buffer, int offset)
        {
            short result = BitOperations.DangerousTo16Pointer(buffer, offset);
            short expectedResult = BitConverter.ToInt16(buffer, offset);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x05, 0x03, 0xA0, 0x10 }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "DangerousTo32Pointer_Overflow")]
        [TestCase(new byte[] { 0x05, 0x03, 0x11, 0x22, 0x40, 0xC1, 0xF7 }, 2)]
        [TestCase(new byte[] { 0x05, 0x03, 0x11, 0x33, 0x1F, 0xCC }, 1)]
        public void DangerousTo32Pointer(byte[] buffer, int offset)
        {
            int result = BitOperations.DangerousTo32Pointer(buffer, offset);
            int expectedResult = BitConverter.ToInt32(buffer, offset);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x05, 0x03, 0xA0, 0x10, 0x12, 0xB1, 0x87, 0x1B }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "DangerousTo64Pointer_Overflow")]
        [TestCase(new byte[] { 0x05, 0x03, 0x11, 0x22, 0x40, 0xC1, 0xF7, 0x12, 0xB1, 0x87 }, 2)]
        [TestCase(new byte[] { 0x05, 0x03, 0x11, 0x33, 0x1F, 0xCC, 0x12, 0xB1, 0x87 }, 1)]
        public void DangerousTo64Pointer(byte[] buffer, int offset)
        {
            long result = BitOperations.DangerousTo64Pointer(buffer, offset);
            long expectedResult = BitConverter.ToInt64(buffer, offset);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x05, 0x03, 0xA0, 0x10, 0x12, 0xB1, 0x87, 0x1B }, 0)]
        [TestCase(new byte[] { 0x05, 0x03, 0x11, 0x22, 0x40, 0xC1, 0xF7, 0x12, 0xB1, 0x87 }, 2)]
        [TestCase(new byte[] { 0x05, 0x03, 0x11, 0x33, 0x1F, 0xCC, 0x12, 0xB1, 0x87 }, 1)]
        [TestCase(new byte[] { 0x01, 0x00, 0x00, 0x00 }, 0, TestName = "DangerousTo32FloatPointer_Float.Epsilon_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0x7F, 0xFF }, 0, TestName = "DangerousTo32FloatPointer_Float.Min_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0x7F, 0x7F }, 0, TestName = "DangerousTo32FloatPointer_Float.Max_LittleEndian")]
        [TestCase(new byte[] { 0x00, 0x00, 0x00, 0x01 }, 0, TestName = "DangerousTo32FloatPointer_Float.Epsilon_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0x7F, 0xFF, 0xFF }, 0, TestName = "DangerousTo32FloatPointer_Float.Min_BigEndian")]
        [TestCase(new byte[] { 0x7F, 0x7F, 0xFF, 0xFF }, 0, TestName = "DangerousTo32FloatPointer_Float.Max_BigEndian")]
        public void DangerousTo32FloatPointer(byte[] buffer, int offset)
        {
            float result = BitOperations.DangerousTo32FloatPointer(buffer, offset);
            float expectedResult = BitConverter.ToSingle(buffer, offset);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x05, 0x03, 0xA0, 0x10, 0x12, 0xB1, 0x87, 0x1B }, 0)]
        [TestCase(new byte[] { 0x05, 0x03, 0x11, 0x22, 0x40, 0xC1, 0xF7, 0x12, 0xB1, 0x87 }, 2)]
        [TestCase(new byte[] { 0x05, 0x03, 0x11, 0x33, 0x1F, 0xCC, 0x12, 0xB1, 0x87 }, 1)]
        [TestCase(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, TestName = "DangerousTo64FloatPointer_Float.Epsilon_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "DangerousTo64FloatPointer_Float.Min_LittleEndian")]
        [TestCase(new byte[] { 0x7F, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "DangerousTo64FloatPointer_Float.Max_LittleEndian")]
        [TestCase(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 }, 0, TestName = "DangerousTo64FloatPointer_Float.Epsilon_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xEF, 0xFF }, 0, TestName = "DangerousTo64FloatPointer_Float.Min_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xEF, 0x7F }, 0, TestName = "DangerousTo64FloatPointer_Float.Max_BigEndian")]
        public void DangerousTo64FloatPointer(byte[] buffer, int offset)
        {
            double result = BitOperations.DangerousTo64FloatPointer(buffer, offset);
            double expectedResult = BitConverter.ToDouble(buffer, offset);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        #endregion

        #region Unsafe Conversion and Swap
        [TestCase(new byte[] { 0x11, 0x22 }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF }, 0, TestName = "DangerousTo16PointerSwap_Overflow")]
        [TestCase(new byte[] { 0x11, 0x22, 0x45, 0x39 }, 1)]
        [TestCase(new byte[] { 0x11, 0x22, 0x45, 0xC8 }, 2)]
        public void DangerousTo16PointerSwap(byte[] buffer, int offset)
        {
            short result = BitOperations.DangerousTo16PointerSwap(buffer, offset);
            byte[] bufferTemp = new byte[2];
            Array.Copy(buffer, offset, bufferTemp, 0, 2);
            Array.Reverse(bufferTemp);
            short expectedResult = BitConverter.ToInt16(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0xF2, 0xB4, 0x33 }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "DangerousTo32PointerSwap_Overflow")]
        [TestCase(new byte[] { 0x01, 0x07, 0xF2, 0xB4, 0x33 }, 1)]
        [TestCase(new byte[] { 0x01, 0xC7, 0x02, 0xB4, 0x33, 0x23 }, 2)]
        public void DangerousTo32PointerSwap(byte[] buffer, int offset)
        {
            int result = BitOperations.DangerousTo32PointerSwap(buffer, offset);
            byte[] bufferTemp = new byte[4];
            Array.Copy(buffer, offset, bufferTemp, 0, 4);
            Array.Reverse(bufferTemp);
            int expectedResult = BitConverter.ToInt32(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0xF2, 0xB4, 0x33, 0x27, 0xF2, 0x1E }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "DangerousTo64PointerSwap_Overflow")]
        [TestCase(new byte[] { 0x01, 0x07, 0xF2, 0xB4, 0x33, 0x27, 0xF2, 0x1E, 0x25 }, 1)]
        [TestCase(new byte[] { 0x01, 0xC7, 0x05, 0xB4, 0x33, 0x27, 0xF2, 0x1E, 0x25, 0x9C }, 2)]
        public void DangerousTo64PointerSwap(byte[] buffer, int offset)
        {
            long result = BitOperations.DangerousTo64PointerSwap(buffer, offset);
            byte[] bufferTemp = new byte[8];
            Array.Copy(buffer, offset, bufferTemp, 0, 8);
            Array.Reverse(bufferTemp);
            long expectedResult = BitConverter.ToInt64(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "DangerousTo32FloatPointerSwap_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0xF2, 0xB4, 0x33 }, 0)]
        [TestCase(new byte[] { 0x01, 0xC7, 0xF2, 0xB4, 0x33 }, 1)]
        [TestCase(new byte[] { 0x01, 0xC7, 0xF2, 0xB4, 0x33, 0x23 }, 2)]
        [TestCase(new byte[] { 0x01, 0x00, 0x00, 0x00 }, 0, TestName = "DangerousTo32FloatPointerSwap_Float.Epsilon_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0x7F, 0xFF }, 0, TestName = "DangerousTo32FloatPointerSwap_Float.Min_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0x7F, 0x7F }, 0, TestName = "DangerousTo32FloatPointerSwap_Float.Max_LittleEndian")]
        [TestCase(new byte[] { 0x00, 0x00, 0x00, 0x01 }, 0, TestName = "DangerousTo32FloatPointerSwap_Float.Epsilon_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0x7F, 0xFF, 0xFF }, 0, TestName = "DangerousTo32FloatPointerSwap_Float.Min_BigEndian")]
        [TestCase(new byte[] { 0x7F, 0x7F, 0xFF, 0xFF }, 0, TestName = "DangerousTo32FloatPointerSwap_Float.Max_BigEndian")]
        public void DangerousTo32FloatPointerSwap(byte[] buffer, int offset)
        {
            float result = BitOperations.DangerousTo32FloatPointerSwap(buffer, offset);
            byte[] bufferTemp = new byte[4];
            Array.Copy(buffer, offset, bufferTemp, 0, 4);
            Array.Reverse(bufferTemp);
            float expectedResult = BitConverter.ToSingle(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "DangerousTo64FloatPointerSwap_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0xF2, 0xB4, 0x33, 0x27, 0xF2, 0x1E }, 0)]
        [TestCase(new byte[] { 0x01, 0xC7, 0xF2, 0xB4, 0x33, 0x27, 0xF2, 0x1E, 0x25 }, 1)]
        [TestCase(new byte[] { 0x01, 0xC7, 0xF2, 0xB4, 0x33, 0x27, 0xF2, 0x1E, 0x25, 0x9C }, 2)]
        [TestCase(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, TestName = "DangerousTo64FloatPointerSwap_Float.Epsilon_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "DangerousTo64FloatPointerSwap_Float.Min_LittleEndian")]
        [TestCase(new byte[] { 0x7F, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "DangerousTo64FloatPointerSwap_Float.Max_LittleEndian")]
        [TestCase(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 }, 0, TestName = "DangerousTo64FloatPointerSwap_Float.Epsilon_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xEF, 0xFF }, 0, TestName = "DangerousTo64FloatPointerSwap_Float.Min_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xEF, 0x7F }, 0, TestName = "DangerousTo64FloatPointerSwap_Float.Max_BigEndian")]
        public void DangerousTo64FloatPointerSwap(byte[] buffer, int offset)
        {
            double result = BitOperations.DangerousTo64FloatPointerSwap(buffer, offset);
            byte[] bufferTemp = new byte[8];
            Array.Copy(buffer, offset, bufferTemp, 0, 8);
            Array.Reverse(bufferTemp);
            double expectedResult = BitConverter.ToDouble(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
        #endregion

        #region Convert by Shifting
        [TestCase(new byte[] { 0x01, 0x07 }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF }, 0, TestName = "To16ShiftLittleEndian_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0x02, 0xFF }, 1)]
        public void To16ShiftLittleEndian(byte[] buffer, int offset)
        {
            short result = BitOperations.To16ShiftLittleEndian(buffer, offset);
            byte[] bufferTemp = new byte[2];
            Array.Copy(buffer, offset, bufferTemp, 0, 2);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            short expectedResult = BitConverter.ToInt16(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0x1F, 0xB9 }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To32ShiftLittleEndian_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0x8D, 0xFF, 0x5D }, 1)]
        public void To32ShiftLittleEndian(byte[] buffer, int offset)
        {
            int result = BitOperations.To32ShiftLittleEndian(buffer, offset);
            byte[] bufferTemp = new byte[4];
            Array.Copy(buffer, offset, bufferTemp, 0, 4);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            int expectedResult = BitConverter.ToInt32(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0x1F, 0xB9, 0x05, 0x23, 0xFE, 0x1D }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To64ShiftLittleEndian_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0x8D, 0xFF, 0x5D, 0x23, 0xFE, 0x1D, 0x02 }, 1)]
        public void To64ShiftLittleEndian(byte[] buffer, int offset)
        {
            long result = BitOperations.To64ShiftLittleEndian(buffer, offset);
            byte[] bufferTemp = new byte[8];
            Array.Copy(buffer, offset, bufferTemp, 0, 8);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            long expectedResult = BitConverter.ToInt64(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0x1F, 0x01 }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To32FloatShiftLittleEndian_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0x8D, 0xFF, 0x5D }, 1)]
        [TestCase(new byte[] { 0x01, 0x00, 0x00, 0x00 }, 0, TestName = "To32FloatShiftLittleEndian_Float.Epsilon_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0x7F, 0xFF }, 0, TestName = "To32FloatShiftLittleEndian_Float.Min_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0x7F, 0x7F }, 0, TestName = "To32FloatShiftLittleEndian_Float.Max_LittleEndian")]
        [TestCase(new byte[] { 0x00, 0x00, 0x00, 0x01 }, 0, TestName = "To32FloatShiftLittleEndian_Float.Epsilon_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0x7F, 0xFF, 0xFF }, 0, TestName = "To32FloatShiftLittleEndian_Float.Min_BigEndian")]
        [TestCase(new byte[] { 0x7F, 0x7F, 0xFF, 0xFF }, 0, TestName = "To32FloatShiftLittleEndian_Float.Max_BigEndian")]
        public void To32FloatShiftLittleEndian(byte[] buffer, int offset)
        {
            float result = BitOperations.To32FloatShiftLittleEndian(buffer, offset);
            byte[] bufferTemp = new byte[4];
            Array.Copy(buffer, offset, bufferTemp, 0, 4);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            float expectedResult = BitConverter.ToSingle(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0x1F, 0xB9, 0x05, 0x23, 0xFE, 0x1D }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To64FloatShiftLittleEndian_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0x8D, 0xFF, 0x5D, 0x23, 0xFE, 0x1D, 0x92 }, 1)]
        [TestCase(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, TestName = "To64FloatShiftLittleEndian_Float.Epsilon_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To64FloatShiftLittleEndian_Float.Min_LittleEndian")]
        [TestCase(new byte[] { 0x7F, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To64FloatShiftLittleEndian_Float.Max_LittleEndian")]
        [TestCase(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 }, 0, TestName = "To64FloatShiftLittleEndian_Float.Epsilon_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xEF, 0xFF }, 0, TestName = "To64FloatShiftLittleEndian_Float.Min_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xEF, 0x7F }, 0, TestName = "To64FloatShiftLittleEndian_Float.Max_BigEndian")]
        public void To64FloatShiftLittleEndian(byte[] buffer, int offset)
        {
            double result = BitOperations.To64FloatShiftLittleEndian(buffer, offset);
            byte[] bufferTemp = new byte[8];
            Array.Copy(buffer, offset, bufferTemp, 0, 8);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            double expectedResult = BitConverter.ToDouble(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7 }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF }, 0, TestName = "To16ShiftBigEndian_Overflow")]
        [TestCase(new byte[] { 0x01, 0x07, 0x8D, 0xFF }, 1)]
        public void To16ShiftBigEndian(byte[] buffer, int offset)
        {
            short result = BitOperations.To16ShiftBigEndian(buffer, offset);
            byte[] bufferTemp = new byte[2];
            Array.Copy(buffer, offset, bufferTemp, 0, 2);
            if (BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            short expectedResult = BitConverter.ToInt16(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0x1F, 0xB9 }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To32ShiftBigEndian_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0x8D, 0xFF, 0x5D }, 1)]
        public void To32ShiftBigEndian(byte[] buffer, int offset)
        {
            int result = BitOperations.To32ShiftBigEndian(buffer, offset);
            byte[] bufferTemp = new byte[4];
            Array.Copy(buffer, offset, bufferTemp, 0, 4);
            if (BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            int expectedResult = BitConverter.ToInt32(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0x1F, 0xB9, 0x05, 0x23, 0xFE, 0x1D }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To64ShiftBigEndian_Overflow")]
        [TestCase(new byte[] { 0x01, 0x07, 0x8D, 0xFF, 0x5D, 0x23, 0xFE, 0x1D, 0x92 }, 1)]
        public void To64ShiftBigEndian(byte[] buffer, int offset)
        {
            long result = BitOperations.To64ShiftBigEndian(buffer, offset);
            byte[] bufferTemp = new byte[8];
            Array.Copy(buffer, offset, bufferTemp, 0, 8);
            if (BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            long expectedResult = BitConverter.ToInt64(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0x1F, 0xB9 }, 0)]
        [TestCase(new byte[] { 0x01, 0x07, 0x8D, 0xFF, 0x5D }, 1)]
        [TestCase(new byte[] { 0x01, 0xC7, 0x8D, 0xFF, 0x5D }, 1, TestName = "To32FloatShiftBigEndian_Overflow")]
        [TestCase(new byte[] { 0x01, 0x00, 0x00, 0x00 }, 0, TestName = "To32FloatShiftBigEndian_Float.Epsilon_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0x7F, 0xFF }, 0, TestName = "To32FloatShiftBigEndian_Float.Min_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0x7F, 0x7F }, 0, TestName = "To32FloatShiftBigEndian_Float.Max_LittleEndian")]
        [TestCase(new byte[] { 0x00, 0x00, 0x00, 0x01 }, 0, TestName = "To32FloatShiftBigEndian_Float.Epsilon_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0x7F, 0xFF, 0xFF }, 0, TestName = "To32FloatShiftBigEndian_Float.Min_BigEndian")]
        [TestCase(new byte[] { 0x7F, 0x7F, 0xFF, 0xFF }, 0, TestName = "To32FloatShiftBigEndian_Float.Max_BigEndian")]
        public void To32FloatShiftBigEndian(byte[] buffer, int offset)
        {
            float result = BitOperations.To32FloatShiftBigEndian(buffer, offset);
            byte[] bufferTemp = new byte[4];
            Array.Copy(buffer, offset, bufferTemp, 0, 4);
            if (BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            float expectedResult = BitConverter.ToSingle(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0x1F, 0xB9, 0x05, 0x23, 0xFE, 0x1D }, 0)]
        [TestCase(new byte[] { 0x01, 0xC7, 0x8D, 0xFF, 0x5D, 0x23, 0xFE, 0x1D, 0x92 }, 1)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To64FloatShiftBigEndian_Overflow")]
        [TestCase(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, TestName = "To64FloatShiftBigEndian_Float.Epsilon_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To64FloatShiftBigEndian_Float.Min_LittleEndian")]
        [TestCase(new byte[] { 0x7F, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To64FloatShiftBigEndian_Float.Max_LittleEndian")]
        [TestCase(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 }, 0, TestName = "To64FloatShiftBigEndian_Float.Epsilon_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xEF, 0xFF }, 0, TestName = "To64FloatShiftBigEndian_Float.Min_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xEF, 0x7F }, 0, TestName = "To64FloatShiftBigEndian_Float.Max_BigEndian")]
        public void To64FloatShiftBigEndian(byte[] buffer, int offset)
        {
            double result = BitOperations.To64FloatShiftBigEndian(buffer, offset);
            byte[] bufferTemp = new byte[8];
            Array.Copy(buffer, offset, bufferTemp, 0, 8);
            if (BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            double expectedResult = BitConverter.ToDouble(bufferTemp, 0);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0x07 }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF }, 0, TestName = "To16Shift_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0x02, 0xFF }, 1)]
        public void To16Shift(byte[] buffer, int offset)
        {
            byte[] bufferTemp = new byte[2];
            short result = BitOperations.To16Shift(buffer, offset, true);
            Array.Copy(buffer, offset, bufferTemp, 0, 2);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            short expectedResult = BitConverter.ToInt16(bufferTemp, 0);
            Assert.That(result, Is.EqualTo(expectedResult));

            result = BitOperations.To16Shift(buffer, offset, false);
            Array.Copy(buffer, offset, bufferTemp, 0, 2);
            if (BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            expectedResult = BitConverter.ToInt16(bufferTemp, 0);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0x1F, 0xB9 }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To32Shift_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0x8D, 0xFF, 0x5D }, 1)]
        public void To32Shift(byte[] buffer, int offset)
        {
            byte[] bufferTemp = new byte[4];
            int result = BitOperations.To32Shift(buffer, offset, true);
            Array.Copy(buffer, offset, bufferTemp, 0, 4);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            int expectedResult = BitConverter.ToInt32(bufferTemp, 0);
            Assert.That(result, Is.EqualTo(expectedResult));

            result = BitOperations.To32Shift(buffer, offset, false);
            Array.Copy(buffer, offset, bufferTemp, 0, 4);
            if (BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            expectedResult = BitConverter.ToInt32(bufferTemp, 0);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0x1F, 0xB9, 0x05, 0x23, 0xFE, 0x1D }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To64Shift_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0x8D, 0xFF, 0x5D, 0x23, 0xFE, 0x1D, 0x02 }, 1)]
        public void To64Shift(byte[] buffer, int offset)
        {
            byte[] bufferTemp = new byte[8];
            long result = BitOperations.To64Shift(buffer, offset, true);
            Array.Copy(buffer, offset, bufferTemp, 0, 8);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            long expectedResult = BitConverter.ToInt64(bufferTemp, 0);
            Assert.That(result, Is.EqualTo(expectedResult));

            result = BitOperations.To64Shift(buffer, offset, false);
            Array.Copy(buffer, offset, bufferTemp, 0, 8);
            if (BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            expectedResult = BitConverter.ToInt64(bufferTemp, 0);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0x1F, 0x01 }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To32FloatShift_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0x8D, 0xFF, 0x5D }, 1)]
        [TestCase(new byte[] { 0x01, 0x00, 0x00, 0x00 }, 0, TestName = "To32FloatShift_Float.Epsilon_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0x7F, 0xFF }, 0, TestName = "To32FloatShift_Float.Min_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0x7F, 0x7F }, 0, TestName = "To32FloatShift_Float.Max_LittleEndian")]
        [TestCase(new byte[] { 0x00, 0x00, 0x00, 0x01 }, 0, TestName = "To32FloatShift_Float.Epsilon_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0x7F, 0xFF, 0xFF }, 0, TestName = "To32FloatShift_Float.Min_BigEndian")]
        [TestCase(new byte[] { 0x7F, 0x7F, 0xFF, 0xFF }, 0, TestName = "To32FloatShift_Float.Max_BigEndian")]
        public void To32FloatShift(byte[] buffer, int offset)
        {
            byte[] bufferTemp = new byte[4];
            float result = BitOperations.To32FloatShift(buffer, offset, true);
            Array.Copy(buffer, offset, bufferTemp, 0, 4);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            float expectedResult = BitConverter.ToSingle(bufferTemp, 0);
            Assert.That(result, Is.EqualTo(expectedResult));

            result = BitOperations.To32FloatShift(buffer, offset, false);
            Array.Copy(buffer, offset, bufferTemp, 0, 4);
            if (BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            expectedResult = BitConverter.ToSingle(bufferTemp, 0);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(new byte[] { 0x01, 0xC7, 0x1F, 0xB9, 0x05, 0x23, 0xFE, 0x1D }, 0)]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To64FloatShift_Overflow")]
        [TestCase(new byte[] { 0x01, 0xC7, 0x8D, 0xFF, 0x5D, 0x23, 0xFE, 0x1D, 0x92 }, 1)]
        [TestCase(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, TestName = "To64FloatShift_Float.Epsilon_LittleEndian")]
        [TestCase(new byte[] { 0xFF, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To64FloatShift_Float.Min_LittleEndian")]
        [TestCase(new byte[] { 0x7F, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 0, TestName = "To64FloatShift_Float.Max_LittleEndian")]
        [TestCase(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 }, 0, TestName = "To64FloatShift_Float.Epsilon_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xEF, 0xFF }, 0, TestName = "To64FloatShift_Float.Min_BigEndian")]
        [TestCase(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xEF, 0x7F }, 0, TestName = "To64FloatShift_Float.Max_BigEndian")]
        public void To64FloatShift(byte[] buffer, int offset)
        {
            byte[] bufferTemp = new byte[8];
            double result = BitOperations.To64FloatShift(buffer, offset, true);
            Array.Copy(buffer, offset, bufferTemp, 0, 8);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            double expectedResult = BitConverter.ToDouble(bufferTemp, 0);
            Assert.That(result, Is.EqualTo(expectedResult));

            result = BitOperations.To64FloatShift(buffer, offset, false);
            Array.Copy(buffer, offset, bufferTemp, 0, 8);
            if (BitConverter.IsLittleEndian) Array.Reverse(bufferTemp);
            expectedResult = BitConverter.ToDouble(bufferTemp, 0);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        #endregion
    }
}
