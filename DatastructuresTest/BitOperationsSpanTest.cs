namespace RJCP.Core
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class BitOperationsSpanTest
    {
        #region Copy by Shifting
        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16ShiftLittleEndianLongInput(long number)
        {
            Span<byte> actual = new byte[2];
            BitOperations.Copy16ShiftLittleEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16ShiftLittleEndianIntInput(int number)
        {
            Span<byte> actual = new byte[2];
            BitOperations.Copy16ShiftLittleEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(unchecked((short)0xFECD))]
        public void Copy16ShiftLittleEndianShortInput(short number)
        {
            Span<byte> actual = new byte[2];
            BitOperations.Copy16ShiftLittleEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(0xFECD5678)]
        public void Copy32ShiftLittleEndianLongInput(long number)
        {
            Span<byte> actual = new byte[4];
            BitOperations.Copy32ShiftLittleEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(unchecked((int)number));
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(unchecked((int)0xFECD5678))]
        public void Copy32ShiftLittleEndianIntInput(int number)
        {
            Span<byte> actual = new byte[4];
            BitOperations.Copy32ShiftLittleEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x123456780ABCDEF0)]
        [TestCase(unchecked((long)0xFEDCBA0123456789))]
        public void Copy64ShiftLittleEndian(long number)
        {
            Span<byte> actual = new byte[8];
            BitOperations.Copy64ShiftLittleEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
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
            Span<byte> actual = new byte[4];
            BitOperations.Copy32FloatShiftLittleEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
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
            Span<byte> actual = new byte[8];
            BitOperations.Copy64FloatShiftLittleEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16ShiftBigEndianLongInput(long number)
        {
            Span<byte> actual = new byte[2];
            BitOperations.Copy16ShiftBigEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16ShiftBigEndianIntInput(int number)
        {
            Span<byte> actual = new byte[2];
            BitOperations.Copy16ShiftBigEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(unchecked((short)0xFECD))]
        public void Copy16ShiftBigEndianShortInput(short number)
        {
            Span<byte> actual = new byte[2];
            BitOperations.Copy16ShiftBigEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(0xFECD5678)]
        public void Copy32ShiftBigEndianLongInput(long number)
        {
            Span<byte> actual = new byte[4];
            BitOperations.Copy32ShiftBigEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(unchecked((int)number));
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(unchecked((int)0xFECD5678))]
        public void Copy32ShiftBigEndianIntInput(int number)
        {
            Span<byte> actual = new byte[4];
            BitOperations.Copy32ShiftBigEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x123456780ABCDEF0)]
        [TestCase(unchecked((long)0xFEDCBA0123456789))]
        public void Copy64ShiftBigEndian(long number)
        {
            Span<byte> actual = new byte[8];
            BitOperations.Copy64ShiftBigEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
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
            Span<byte> actual = new byte[4];
            BitOperations.Copy32FloatShiftBigEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
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
            Span<byte> actual = new byte[8];
            BitOperations.Copy64FloatShiftBigEndian(number, actual);
            byte[] expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);

            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16ShiftLongInput(long number)
        {
            Span<byte> actual = new byte[2];
            BitOperations.Copy16Shift(number, actual, true);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));

            BitOperations.Copy16Shift(number, actual, false);
            expected = BitConverter.GetBytes(unchecked((short)number));
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(0xFECD)]
        public void Copy16ShiftIntInput(int number)
        {
            Span<byte> actual = new byte[2];
            BitOperations.Copy16Shift(number, actual, true);
            byte[] expected = BitConverter.GetBytes(unchecked((short)number));
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));

            BitOperations.Copy16Shift(number, actual, false);
            expected = BitConverter.GetBytes(unchecked((short)number));
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x1234)]
        [TestCase(unchecked((short)0xFECD))]
        public void Copy16ShiftShortInput(short number)
        {
            Span<byte> actual = new byte[2];
            BitOperations.Copy16Shift(number, actual, true);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));

            BitOperations.Copy16Shift(number, actual, false);
            expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(0xFECD5678)]
        public void Copy32ShiftLongInput(long number)
        {
            Span<byte> actual = new byte[4];
            BitOperations.Copy32Shift(number, actual, true);
            byte[] expected = BitConverter.GetBytes(unchecked((int)number));
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));

            BitOperations.Copy32Shift(number, actual, false);
            expected = BitConverter.GetBytes(unchecked((int)number));
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x12345678)]
        [TestCase(unchecked((int)0xFECD5678))]
        public void Copy32ShiftIntInput(int number)
        {
            Span<byte> actual = new byte[4];
            BitOperations.Copy32Shift(number, actual, true);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));

            BitOperations.Copy32Shift(number, actual, false);
            expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }

        [TestCase(0x123456780ABCDEF0)]
        [TestCase(unchecked((long)0xFEDCBA0123456789))]
        public void Copy64ShiftIntInput(long number)
        {
            Span<byte> actual = new byte[8];
            BitOperations.Copy64Shift(number, actual, true);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));

            BitOperations.Copy64Shift(number, actual, false);
            expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));
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
            Span<byte> actual = new byte[4];
            BitOperations.Copy32FloatShift(number, actual, true);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));

            BitOperations.Copy32FloatShift(number, actual, false);
            expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));
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
            Span<byte> actual = new byte[8];
            BitOperations.Copy64FloatShift(number, actual, true);
            byte[] expected = BitConverter.GetBytes(number);
            if (!BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));

            BitOperations.Copy64FloatShift(number, actual, false);
            expected = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian) Array.Reverse(expected);
            Assert.That(actual.ToArray(), Is.EqualTo(expected));
        }
        #endregion
    }
}
