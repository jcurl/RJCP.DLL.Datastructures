namespace RJCP.Core
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Basic numeric type operations.
    /// </summary>
    /// <remarks>
    /// The methods provided here support very fast mechanisms to copy a value (short, int, long, float, double) into a
    /// byte array and their reverse operations for converting a byte array to its equivalent numerical representation.
    /// <para>To choose the fastest method, consider:</para>
    /// <list type="bullet">
    /// <item>
    /// If the result must always be little endian, use the CopyXXShiftLittleEndian functions. This has no if statements
    /// and uses shift operations to push the result in the byte array that it is always little endian.
    /// </item>
    /// <item>
    /// If the result must always be big endian, use the CopyXXBigLittleEndian functions. This has no if statements and
    /// uses shift operations to push the result in the byte array that it is always big endian.
    /// </item>
    /// <item>
    /// If the endianness of the output is the same as the machine that is running, then use the CopyXXPointer functions
    /// which are overall the fastest. Please note, you should consider using the CopyXXShift(Big|Little)Endian if the
    /// data being stored needs to be transferable between different machines.
    /// </item>
    /// <item>
    /// Generally, do not use the CopyXXPointerSwap as they are slower than the above three possibilities. It is the
    /// same as CopyXXPointer, except the endianness is swapped to that of the native machine.
    /// </item>
    /// </list>
    /// <para>
    /// Use one of the above groups of functions without checking <see cref="BitConverter.IsLittleEndian"/>. It is
    /// observed that using an if statement to check the endianness is approximately 20% slower. Thus, this is what
    /// makes the CopyXXShiftLittleEndian faster than doing an endianness check and then a CopyXXPointer afterwards. The
    /// same rule applies to the reverse operations of the copy methods described above.
    /// </para>
    /// </remarks>
    public static class BitOperations
    {
        /// <summary>
        /// Copy the first byte of a given integer at a position in the buffer.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy8Simple(long value, byte[] buffer, int offset)
        {
            buffer[offset] = unchecked((byte)value);
        }

        /// <summary>
        /// Copy the first byte of a given integer at a position in the buffer.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy8Simple(int value, byte[] buffer, int offset)
        {
            buffer[offset] = unchecked((byte)value);
        }

        /// <summary>
        /// Copy the first byte of a given integer at a position in the buffer.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy8Simple(short value, byte[] buffer, int offset)
        {
            buffer[offset] = unchecked((byte)value);
        }

        /// <summary>
        /// Copy the first byte of a given integer at a position in the buffer.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy8Simple(byte value, byte[] buffer, int offset)
        {
            buffer[offset] = value;
        }

        /// <summary>
        /// Copy the first byte of a given integer at a position in the buffer.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// The corresponding CLS compliant implementation is <see cref="Copy8Simple(byte, byte[], int)"/>.
        /// </remarks>
        [CLSCompliant(false)]
        public static void Copy8Simple(sbyte value, byte[] buffer, int offset)
        {
            buffer[offset] = unchecked((byte)value);
        }

        #region Unsafe Copy
        /// <summary>
        /// Copy 16-bit integer using pointer arithmetics.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 2 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy16Pointer(long value, byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                *(short*)pointer = unchecked((short)value);
            }
        }

        /// <summary>
        /// Copy 16-bit integer using pointer arithmetics.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 2 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy16Pointer(int value, byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                *(short*)pointer = unchecked((short)value);
            }
        }

        /// <summary>
        /// Copy 16-bit integer using pointer arithmetics.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 2 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy16Pointer(short value, byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                *(short*)pointer = value;
            }
        }

        /// <summary>
        /// Copy 32-bit integer using pointer arithmetics.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 4 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy32Pointer(long value, byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                *(int*)pointer = unchecked((int)value);
            }
        }

        /// <summary>
        /// Copy 32-bit integer using pointer arithmetics.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 4 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy32Pointer(int value, byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                *(int*)pointer = value;
            }
        }

        /// <summary>
        /// Copy 64-bit integer using pointer arithmetics.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 8 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy64Pointer(long value, byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                *(long*)pointer = value;
            }
        }

        /// <summary>
        /// Copy 32-bit floating point number using pointer arithmetics.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 4 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy32FloatPointer(float value, byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                *(float*)pointer = value;
            }
        }

        /// <summary>
        /// Copy 64-bit floating point number using pointer arithmetics.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 8 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy64FloatPointer(double value, byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                *(double*)pointer = value;
            }
        }
        #endregion

        #region Unsafe Copy and Swap
        /// <summary>
        /// Copy 16-bit integer after swapping its bytes.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 2 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy16PointerSwap(long value, byte[] buffer, int offset)
        {
            long result = ((value & 0x00FF) << 8) | ((value & 0xFF00) >> 8);

            fixed (byte* pointer = &buffer[offset]) {
                *(short*)pointer = unchecked((short)result);
            }
        }

        /// <summary>
        /// Copy 16-bit integer after swapping its bytes.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 2 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy16PointerSwap(int value, byte[] buffer, int offset)
        {
            long result = ((value & 0x00FF) << 8) | ((value & 0xFF00) >> 8);

            fixed (byte* pointer = &buffer[offset]) {
                *(short*)pointer = unchecked((short)result);
            }
        }

        /// <summary>
        /// Copy 16-bit integer after swapping its bytes.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 2 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy16PointerSwap(short value, byte[] buffer, int offset)
        {
            int result = ((value & 0x00FF) << 8) | ((value & 0xFF00) >> 8);

            fixed (byte* pointer = &buffer[offset]) {
                *(short*)pointer = unchecked((short)result);
            }
        }

        /// <summary>
        /// Copy 32-bit integer after swapping its bytes.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 4 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy32PointerSwap(long value, byte[] buffer, int offset)
        {
            value = ((value & 0x0000FFFF) << 16) | ((value & 0xFFFF0000) >> 16);
            value = ((value & 0x00FF00FF) << 8) | ((value & 0xFF00FF00) >> 8);

            fixed (byte* pointer = &buffer[offset]) {
                *(int*)pointer = unchecked((int)value);
            }
        }

        /// <summary>
        /// Copy 32-bit integer after swapping its bytes.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 4 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy32PointerSwap(int value, byte[] buffer, int offset)
        {
            uint result = unchecked((uint)value);

            result = ((result & 0x0000FFFF) << 16) | ((result & 0xFFFF0000) >> 16);
            result = ((result & 0x00FF00FF) << 8) | ((result & 0xFF00FF00) >> 8);

            fixed (byte* pointer = &buffer[offset]) {
                *(int*)pointer = unchecked((int)result);
            }
        }

        /// <summary>
        /// Copy 64-bit integer after swapping its bytes.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 8 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy64PointerSwap(long value, byte[] buffer, int offset)
        {
            ulong result = unchecked((ulong)value);

            result = ((result & 0x00000000FFFFFFFF) << 32) | ((result & 0xFFFFFFFF00000000) >> 32);
            result = ((result & 0x0000FFFF0000FFFF) << 16) | ((result & 0xFFFF0000FFFF0000) >> 16);
            result = ((result & 0x00FF00FF00FF00FF) << 8) | ((result & 0xFF00FF00FF00FF00) >> 8);

            fixed (byte* pointer = &buffer[offset]) {
                *(long*)pointer = unchecked((long)result);
            }
        }

        /// <summary>
        /// Copy 32-bit floating point number using pointer arithmetics.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 4 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy32FloatPointerSwap(float value, byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                uint iValue = *(uint*)&value;
                iValue = ((iValue & 0x0000FFFF) << 16) | ((iValue & 0xFFFF0000) >> 16);
                iValue = ((iValue & 0x00FF00FF) << 8) | ((iValue & 0xFF00FF00) >> 8);
                *(uint*)pointer = iValue;
            }
        }

        /// <summary>
        /// Copy 64-bit floating point number using pointer arithmetics.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 8 bytes is within the buffer array provided. No checks are made by this function, and may result in
        /// memory corruption. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// <para>
        /// If you attempt to write out of the bounds of <paramref name="buffer"/> with <paramref name="offset"/>, no
        /// exception is likely to be raised by the run-time, and latent memory corruption can occur.
        /// </para>
        /// </remarks>
        public unsafe static void DangerousCopy64FloatPointerSwap(double value, byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                ulong lValue = *(ulong*)&value;
                lValue = ((lValue & 0x00000000FFFFFFFF) << 32) | ((lValue & 0xFFFFFFFF00000000) >> 32);
                lValue = ((lValue & 0x0000FFFF0000FFFF) << 16) | ((lValue & 0xFFFF0000FFFF0000) >> 16);
                lValue = ((lValue & 0x00FF00FF00FF00FF) << 8) | ((lValue & 0xFF00FF00FF00FF00) >> 8);
                *(ulong*)pointer = lValue;
            }
        }
        #endregion

        #region Copy by Shifting
        /// <summary>
        /// Copy 16-bit integer after shifting its bytes in little endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy16ShiftLittleEndian(long value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)(value & 0xFF);
            buffer[offset + 1] = (byte)((value >> 8) & 0xFF);
        }

        /// <summary>
        /// Copy 16-bit integer after shifting its bytes in little endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy16ShiftLittleEndian(int value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)(value & 0xFF);
            buffer[offset + 1] = (byte)((value >> 8) & 0xFF);
        }

        /// <summary>
        /// Copy 16-bit integer after shifting its bytes in little endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy16ShiftLittleEndian(short value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)(value & 0xFF);
            buffer[offset + 1] = (byte)((value >> 8) & 0xFF);
        }

        /// <summary>
        /// Copy 32-bit integer after shifting its bytes in little endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy32ShiftLittleEndian(long value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)(value & 0xFF);
            buffer[offset + 1] = (byte)((value >> 8) & 0xFF);
            buffer[offset + 2] = (byte)((value >> 16) & 0xFF);
            buffer[offset + 3] = (byte)((value >> 24) & 0xFF);
        }

        /// <summary>
        /// Copy 32-bit integer after shifting its bytes in little endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy32ShiftLittleEndian(int value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)(value & 0xFF);
            buffer[offset + 1] = (byte)((value >> 8) & 0xFF);
            buffer[offset + 2] = (byte)((value >> 16) & 0xFF);
            buffer[offset + 3] = (byte)((value >> 24) & 0xFF);
        }

        /// <summary>
        /// Copy 64-bit integer after shifting its bytes in little endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy64ShiftLittleEndian(long value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)(value & 0xFF);
            buffer[offset + 1] = (byte)((value >> 8) & 0xFF);
            buffer[offset + 2] = (byte)((value >> 16) & 0xFF);
            buffer[offset + 3] = (byte)((value >> 24) & 0xFF);
            buffer[offset + 4] = (byte)((value >> 32) & 0xFF);
            buffer[offset + 5] = (byte)((value >> 40) & 0xFF);
            buffer[offset + 6] = (byte)((value >> 48) & 0xFF);
            buffer[offset + 7] = (byte)((value >> 56) & 0xFF);
        }

        /// <summary>
        /// Copy a 32-bit float in little endian format.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy32FloatShiftLittleEndian(float value, byte[] buffer, int offset)
        {
            unsafe {
                int iValue = *(int*)&value;
                buffer[offset] = (byte)(iValue & 0xFF);
                buffer[offset + 1] = (byte)((iValue >> 8) & 0xFF);
                buffer[offset + 2] = (byte)((iValue >> 16) & 0xFF);
                buffer[offset + 3] = (byte)((iValue >> 24) & 0xFF);
            }
        }

        /// <summary>
        /// Copy a 64-bit float in little endian format.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy64FloatShiftLittleEndian(double value, byte[] buffer, int offset)
        {
            unsafe {
                long lValue = *(long*)&value;
                buffer[offset] = (byte)(lValue & 0xFF);
                buffer[offset + 1] = (byte)((lValue >> 8) & 0xFF);
                buffer[offset + 2] = (byte)((lValue >> 16) & 0xFF);
                buffer[offset + 3] = (byte)((lValue >> 24) & 0xFF);
                buffer[offset + 4] = (byte)((lValue >> 32) & 0xFF);
                buffer[offset + 5] = (byte)((lValue >> 40) & 0xFF);
                buffer[offset + 6] = (byte)((lValue >> 48) & 0xFF);
                buffer[offset + 7] = (byte)((lValue >> 56) & 0xFF);
            }
        }

        /// <summary>
        /// Copy 16-bit integer after shifting its bytes in big endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy16ShiftBigEndian(long value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)((value >> 8) & 0xFF);
            buffer[offset + 1] = (byte)(value & 0xFF);
        }

        /// <summary>
        /// Copy 16-bit integer after shifting its bytes in big endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy16ShiftBigEndian(int value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)((value >> 8) & 0xFF);
            buffer[offset + 1] = (byte)(value & 0xFF);
        }

        /// <summary>
        /// Copy 16-bit integer after shifting its bytes in big endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy16ShiftBigEndian(short value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)((value >> 8) & 0xFF);
            buffer[offset + 1] = (byte)(value & 0xFF);
        }

        /// <summary>
        /// Copy 32-bit integer after shifting its bytes in big endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy32ShiftBigEndian(long value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)((value >> 24) & 0xFF);
            buffer[offset + 1] = (byte)((value >> 16) & 0xFF);
            buffer[offset + 2] = (byte)((value >> 8) & 0xFF);
            buffer[offset + 3] = (byte)(value & 0xFF);
        }

        /// <summary>
        /// Copy 32-bit integer after shifting its bytes in big endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy32ShiftBigEndian(int value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)((value >> 24) & 0xFF);
            buffer[offset + 1] = (byte)((value >> 16) & 0xFF);
            buffer[offset + 2] = (byte)((value >> 8) & 0xFF);
            buffer[offset + 3] = (byte)(value & 0xFF);
        }

        /// <summary>
        /// Copy 64-bit integer after shifting its bytes in big endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy64ShiftBigEndian(long value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)((value >> 56) & 0xFF);
            buffer[offset + 1] = (byte)((value >> 48) & 0xFF);
            buffer[offset + 2] = (byte)((value >> 40) & 0xFF);
            buffer[offset + 3] = (byte)((value >> 32) & 0xFF);
            buffer[offset + 4] = (byte)((value >> 24) & 0xFF);
            buffer[offset + 5] = (byte)((value >> 16) & 0xFF);
            buffer[offset + 6] = (byte)((value >> 8) & 0xFF);
            buffer[offset + 7] = (byte)(value & 0xFF);
        }

        /// <summary>
        /// Copy a 32-bit float in big endian format.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy32FloatShiftBigEndian(float value, byte[] buffer, int offset)
        {
            unsafe {
                int iValue = *(int*)&value;
                buffer[offset] = (byte)((iValue >> 24) & 0xFF);
                buffer[offset + 1] = (byte)((iValue >> 16) & 0xFF);
                buffer[offset + 2] = (byte)((iValue >> 8) & 0xFF);
                buffer[offset + 3] = (byte)(iValue & 0xFF);
            }
        }

        /// <summary>
        /// Copy a 64-bit float in big endian format.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        public static void Copy64FloatShiftBigEndian(double value, byte[] buffer, int offset)
        {
            unsafe {
                long lValue = *(long*)&value;
                buffer[offset] = (byte)((lValue >> 56) & 0xFF);
                buffer[offset + 1] = (byte)((lValue >> 48) & 0xFF);
                buffer[offset + 2] = (byte)((lValue >> 40) & 0xFF);
                buffer[offset + 3] = (byte)((lValue >> 32) & 0xFF);
                buffer[offset + 4] = (byte)((lValue >> 24) & 0xFF);
                buffer[offset + 5] = (byte)((lValue >> 16) & 0xFF);
                buffer[offset + 6] = (byte)((lValue >> 8) & 0xFF);
                buffer[offset + 7] = (byte)(lValue & 0xFF);
            }
        }

        /// <summary>
        /// Copy 16-bit integer into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy16Shift(long value, byte[] buffer, int offset, bool littleEndian)
        {
            if (littleEndian) {
                Copy16ShiftLittleEndian(value, buffer, offset);
            } else {
                Copy16ShiftBigEndian(value, buffer, offset);
            }
        }

        /// <summary>
        /// Copy 16-bit integer into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy16Shift(int value, byte[] buffer, int offset, bool littleEndian)
        {
            if (littleEndian) {
                Copy16ShiftLittleEndian(value, buffer, offset);
            } else {
                Copy16ShiftBigEndian(value, buffer, offset);
            }
        }

        /// <summary>
        /// Copy 16-bit integer into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy16Shift(short value, byte[] buffer, int offset, bool littleEndian)
        {
            if (littleEndian) {
                Copy16ShiftLittleEndian(value, buffer, offset);
            } else {
                Copy16ShiftBigEndian(value, buffer, offset);
            }
        }

        /// <summary>
        /// Copy 32-bit integer into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy32Shift(long value, byte[] buffer, int offset, bool littleEndian)
        {
            if (littleEndian) {
                Copy32ShiftLittleEndian(value, buffer, offset);
            } else {
                Copy32ShiftBigEndian(value, buffer, offset);
            }
        }

        /// <summary>
        /// Copy 32-bit integer into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy32Shift(int value, byte[] buffer, int offset, bool littleEndian)
        {
            if (littleEndian) {
                Copy32ShiftLittleEndian(value, buffer, offset);
            } else {
                Copy32ShiftBigEndian(value, buffer, offset);
            }
        }

        /// <summary>
        /// Copy 64-bit integer into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy64Shift(long value, byte[] buffer, int offset, bool littleEndian)
        {
            if (littleEndian) {
                Copy64ShiftLittleEndian(value, buffer, offset);
            } else {
                Copy64ShiftBigEndian(value, buffer, offset);
            }
        }

        /// <summary>
        /// Copy 32-bit float into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy32FloatShift(float value, byte[] buffer, int offset, bool littleEndian)
        {
            if (littleEndian) {
                Copy32FloatShiftLittleEndian(value, buffer, offset);
            } else {
                Copy32FloatShiftBigEndian(value, buffer, offset);
            }
        }

        /// <summary>
        /// Copy 64-bit float into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset in the destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy64FloatShift(double value, byte[] buffer, int offset, bool littleEndian)
        {
            if (littleEndian) {
                Copy64FloatShiftLittleEndian(value, buffer, offset);
            } else {
                Copy64FloatShiftBigEndian(value, buffer, offset);
            }
        }
        #endregion

#if NETSTANDARD2_1
        #region Copy Span<byte>
        /// <summary>
        /// Copy 16-bit integer after shifting its bytes in little endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy16ShiftLittleEndian(long value, Span<byte> buffer)
        {
            buffer[0] = (byte)(value & 0xFF);
            buffer[1] = (byte)((value >> 8) & 0xFF);
        }

        /// <summary>
        /// Copy 16-bit integer after shifting its bytes in little endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy16ShiftLittleEndian(int value, Span<byte> buffer)
        {
            buffer[0] = (byte)(value & 0xFF);
            buffer[1] = (byte)((value >> 8) & 0xFF);
        }

        /// <summary>
        /// Copy 16-bit integer after shifting its bytes in little endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy16ShiftLittleEndian(short value, Span<byte> buffer)
        {
            buffer[0] = (byte)(value & 0xFF);
            buffer[1] = (byte)((value >> 8) & 0xFF);
        }

        /// <summary>
        /// Copy 32-bit integer after shifting its bytes in little endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy32ShiftLittleEndian(long value, Span<byte> buffer)
        {
            buffer[0] = (byte)(value & 0xFF);
            buffer[1] = (byte)((value >> 8) & 0xFF);
            buffer[2] = (byte)((value >> 16) & 0xFF);
            buffer[3] = (byte)((value >> 24) & 0xFF);
        }

        /// <summary>
        /// Copy 32-bit integer after shifting its bytes in little endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy32ShiftLittleEndian(int value, Span<byte> buffer)
        {
            buffer[0] = (byte)(value & 0xFF);
            buffer[1] = (byte)((value >> 8) & 0xFF);
            buffer[2] = (byte)((value >> 16) & 0xFF);
            buffer[3] = (byte)((value >> 24) & 0xFF);
        }

        /// <summary>
        /// Copy 64-bit integer after shifting its bytes in little endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy64ShiftLittleEndian(long value, Span<byte> buffer)
        {
            buffer[0] = (byte)(value & 0xFF);
            buffer[1] = (byte)((value >> 8) & 0xFF);
            buffer[2] = (byte)((value >> 16) & 0xFF);
            buffer[3] = (byte)((value >> 24) & 0xFF);
            buffer[4] = (byte)((value >> 32) & 0xFF);
            buffer[5] = (byte)((value >> 40) & 0xFF);
            buffer[6] = (byte)((value >> 48) & 0xFF);
            buffer[7] = (byte)((value >> 56) & 0xFF);
        }

        /// <summary>
        /// Copy a 32-bit float in little endian format.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy32FloatShiftLittleEndian(float value, Span<byte> buffer)
        {
            unsafe {
                int iValue = *(int*)&value;
                buffer[0] = (byte)(iValue & 0xFF);
                buffer[1] = (byte)((iValue >> 8) & 0xFF);
                buffer[2] = (byte)((iValue >> 16) & 0xFF);
                buffer[3] = (byte)((iValue >> 24) & 0xFF);
            }
        }

        /// <summary>
        /// Copy a 64-bit float in little endian format.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy64FloatShiftLittleEndian(double value, Span<byte> buffer)
        {
            unsafe {
                long lValue = *(long*)&value;
                buffer[0] = (byte)(lValue & 0xFF);
                buffer[1] = (byte)((lValue >> 8) & 0xFF);
                buffer[2] = (byte)((lValue >> 16) & 0xFF);
                buffer[3] = (byte)((lValue >> 24) & 0xFF);
                buffer[4] = (byte)((lValue >> 32) & 0xFF);
                buffer[5] = (byte)((lValue >> 40) & 0xFF);
                buffer[6] = (byte)((lValue >> 48) & 0xFF);
                buffer[7] = (byte)((lValue >> 56) & 0xFF);
            }
        }

        /// <summary>
        /// Copy 16-bit integer after shifting its bytes in big endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy16ShiftBigEndian(long value, Span<byte> buffer)
        {
            buffer[0] = (byte)((value >> 8) & 0xFF);
            buffer[1] = (byte)(value & 0xFF);
        }

        /// <summary>
        /// Copy 16-bit integer after shifting its bytes in big endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy16ShiftBigEndian(int value, Span<byte> buffer)
        {
            buffer[0] = (byte)((value >> 8) & 0xFF);
            buffer[1] = (byte)(value & 0xFF);
        }

        /// <summary>
        /// Copy 16-bit integer after shifting its bytes in big endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy16ShiftBigEndian(short value, Span<byte> buffer)
        {
            buffer[0] = (byte)((value >> 8) & 0xFF);
            buffer[1] = (byte)(value & 0xFF);
        }

        /// <summary>
        /// Copy 32-bit integer after shifting its bytes in big endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy32ShiftBigEndian(long value, Span<byte> buffer)
        {
            buffer[0] = (byte)((value >> 24) & 0xFF);
            buffer[1] = (byte)((value >> 16) & 0xFF);
            buffer[2] = (byte)((value >> 8) & 0xFF);
            buffer[3] = (byte)(value & 0xFF);
        }

        /// <summary>
        /// Copy 32-bit integer after shifting its bytes in big endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy32ShiftBigEndian(int value, Span<byte> buffer)
        {
            buffer[0] = (byte)((value >> 24) & 0xFF);
            buffer[1] = (byte)((value >> 16) & 0xFF);
            buffer[2] = (byte)((value >> 8) & 0xFF);
            buffer[3] = (byte)(value & 0xFF);
        }

        /// <summary>
        /// Copy 64-bit integer after shifting its bytes in big endian order.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy64ShiftBigEndian(long value, Span<byte> buffer)
        {
            buffer[0] = (byte)((value >> 56) & 0xFF);
            buffer[1] = (byte)((value >> 48) & 0xFF);
            buffer[2] = (byte)((value >> 40) & 0xFF);
            buffer[3] = (byte)((value >> 32) & 0xFF);
            buffer[4] = (byte)((value >> 24) & 0xFF);
            buffer[5] = (byte)((value >> 16) & 0xFF);
            buffer[6] = (byte)((value >> 8) & 0xFF);
            buffer[7] = (byte)(value & 0xFF);
        }

        /// <summary>
        /// Copy a 32-bit float in big endian format.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy32FloatShiftBigEndian(float value, Span<byte> buffer)
        {
            unsafe {
                int iValue = *(int*)&value;
                buffer[0] = (byte)((iValue >> 24) & 0xFF);
                buffer[1] = (byte)((iValue >> 16) & 0xFF);
                buffer[2] = (byte)((iValue >> 8) & 0xFF);
                buffer[3] = (byte)(iValue & 0xFF);
            }
        }

        /// <summary>
        /// Copy a 64-bit float in big endian format.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        public static void Copy64FloatShiftBigEndian(double value, Span<byte> buffer)
        {
            unsafe {
                long lValue = *(long*)&value;
                buffer[0] = (byte)((lValue >> 56) & 0xFF);
                buffer[1] = (byte)((lValue >> 48) & 0xFF);
                buffer[2] = (byte)((lValue >> 40) & 0xFF);
                buffer[3] = (byte)((lValue >> 32) & 0xFF);
                buffer[4] = (byte)((lValue >> 24) & 0xFF);
                buffer[5] = (byte)((lValue >> 16) & 0xFF);
                buffer[6] = (byte)((lValue >> 8) & 0xFF);
                buffer[7] = (byte)(lValue & 0xFF);
            }
        }

        /// <summary>
        /// Copy 16-bit integer into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy16Shift(long value, Span<byte> buffer, bool littleEndian)
        {
            if (littleEndian) {
                Copy16ShiftLittleEndian(value, buffer);
            } else {
                Copy16ShiftBigEndian(value, buffer);
            }
        }

        /// <summary>
        /// Copy 16-bit integer into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy16Shift(int value, Span<byte> buffer, bool littleEndian)
        {
            if (littleEndian) {
                Copy16ShiftLittleEndian(value, buffer);
            } else {
                Copy16ShiftBigEndian(value, buffer);
            }
        }

        /// <summary>
        /// Copy 16-bit integer into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy16Shift(short value, Span<byte> buffer, bool littleEndian)
        {
            if (littleEndian) {
                Copy16ShiftLittleEndian(value, buffer);
            } else {
                Copy16ShiftBigEndian(value, buffer);
            }
        }

        /// <summary>
        /// Copy 32-bit integer into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy32Shift(long value, Span<byte> buffer, bool littleEndian)
        {
            if (littleEndian) {
                Copy32ShiftLittleEndian(value, buffer);
            } else {
                Copy32ShiftBigEndian(value, buffer);
            }
        }

        /// <summary>
        /// Copy 32-bit integer into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy32Shift(int value, Span<byte> buffer, bool littleEndian)
        {
            if (littleEndian) {
                Copy32ShiftLittleEndian(value, buffer);
            } else {
                Copy32ShiftBigEndian(value, buffer);
            }
        }

        /// <summary>
        /// Copy 64-bit integer into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy64Shift(long value, Span<byte> buffer, bool littleEndian)
        {
            if (littleEndian) {
                Copy64ShiftLittleEndian(value, buffer);
            } else {
                Copy64ShiftBigEndian(value, buffer);
            }
        }

        /// <summary>
        /// Copy 32-bit float into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy32FloatShift(float value, Span<byte> buffer, bool littleEndian)
        {
            if (littleEndian) {
                Copy32FloatShiftLittleEndian(value, buffer);
            } else {
                Copy32FloatShiftBigEndian(value, buffer);
            }
        }

        /// <summary>
        /// Copy 64-bit float into a buffer in the endianness specified.
        /// </summary>
        /// <param name="value">The value to be copied.</param>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="littleEndian">If <see langword="true"/>, copy as little endian, else big endian.</param>
        public static void Copy64FloatShift(double value, Span<byte> buffer, bool littleEndian)
        {
            if (littleEndian) {
                Copy64FloatShiftLittleEndian(value, buffer);
            } else {
                Copy64FloatShiftBigEndian(value, buffer);
            }
        }
        #endregion
#endif

        /// <summary>
        /// Convert a byte found at a given position in a buffer to its integer equivalent.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The value stored in the buffer at the given offset.</returns>
        public static long To8Simple(byte[] buffer, int offset)
        {
            return buffer[offset];
        }

        #region Unsafe Conversion
        /// <summary>
        /// Get the 16-bit value found at a given position in a buffer.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer using the machine endianness at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 2 bytes is within the buffer array provided. No checks are made by this function, and may result in an
        /// incorrect conversion. It is only provided for advanced programming with high performance. Consider using
        /// another conversion function instead.
        /// </remarks>
        public unsafe static short DangerousTo16Pointer(byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                return *(short*)pointer;
            }
        }

        /// <summary>
        /// Get the 32-bit value found at a given position in a buffer.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 32-bit value stored in the buffer using the machine endianness at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 4 bytes is within the buffer array provided. No checks are made by this function, and may result in an
        /// incorrect conversion. It is only provided for advanced programming with high performance. Consider using
        /// another conversion function instead.
        /// </remarks>
        public unsafe static int DangerousTo32Pointer(byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                return *(int*)pointer;
            }
        }

        /// <summary>
        /// Get the 64-bit value found at a given position in a buffer.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 64-bit value stored in the buffer using the machine endianness at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 8 bytes is within the buffer array provided. No checks are made by this function, and may result in an
        /// incorrect conversion. It is only provided for advanced programming with high performance. Consider using
        /// another conversion function instead.
        /// </remarks>
        public unsafe static long DangerousTo64Pointer(byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                return *(long*)pointer;
            }
        }

        /// <summary>
        /// Get the 32-bit floating point number found at a given position in a buffer.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 32-bit float stored in the buffer using the machine endianness at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 4 bytes is within the buffer array provided. No checks are made by this function, and may result in an
        /// incorrect conversion. It is only provided for advanced programming with high performance. Consider using
        /// another conversion function instead.
        /// </remarks>
        public unsafe static float DangerousTo32FloatPointer(byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                return *(float*)pointer;
            }
        }

        /// <summary>
        /// Get the 64-bit floating point number found at a given position in a buffer.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 64-bit double stored in the buffer using the machine endianness at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 8 bytes is within the buffer array provided. No checks are made by this function, and may result in an
        /// incorrect conversion. It is only provided for advanced programming with high performance. Consider using
        /// another conversion function instead.
        /// </remarks>
        public unsafe static double DangerousTo64FloatPointer(byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                return *(double*)pointer;
            }
        }
        #endregion

        #region Unsafe Conversion and Swap
        /// <summary>
        /// Get the 16-bit integer after swapping its bytes.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>
        /// The 16-bit value stored in the buffer using the reverse machine endianness at the given offset.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 2 bytes is within the buffer array provided. No checks are made by this function, and may result in an
        /// incorrect conversion. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// </remarks>
        public unsafe static short DangerousTo16PointerSwap(byte[] buffer, int offset)
        {
            short value;
            fixed (byte* pointer = &buffer[offset]) {
                value = *(short*)pointer;
            }

            return unchecked((short)(((value & 0x00FF) << 8) | ((value & 0xFF00) >> 8)));
        }

        /// <summary>
        /// Get the 32-bit integer after swapping its bytes.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>
        /// The 32-bit value stored in the buffer using the reverse machine endianness at the given offset.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 4 bytes is within the buffer array provided. No checks are made by this function, and may result in an
        /// incorrect conversion. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// </remarks>
        public unsafe static int DangerousTo32PointerSwap(byte[] buffer, int offset)
        {
            uint value;
            fixed (byte* pointer = &buffer[offset]) {
                value = *(uint*)pointer;
            }

            value = ((value & 0x0000FFFF) << 16) | ((value & 0xFFFF0000) >> 16);
            value = ((value & 0x00FF00FF) << 8) | ((value & 0xFF00FF00) >> 8);
            return unchecked((int)value);
        }

        /// <summary>
        /// Get the 64-bit integer after swapping its bytes.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>
        /// The 64-bit value stored in the buffer using the reverse machine endianness at the given offset.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 8 bytes is within the buffer array provided. No checks are made by this function, and may result in an
        /// incorrect conversion. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// </remarks>
        public unsafe static long DangerousTo64PointerSwap(byte[] buffer, int offset)
        {
            ulong value;
            fixed (byte* pointer = &buffer[offset]) {
                value = *(ulong*)pointer;
            }

            value = ((value & 0x00000000FFFFFFFF) << 32) | ((value & 0xFFFFFFFF00000000) >> 32);
            value = ((value & 0x0000FFFF0000FFFF) << 16) | ((value & 0xFFFF0000FFFF0000) >> 16);
            value = ((value & 0x00FF00FF00FF00FF) << 8) | ((value & 0xFF00FF00FF00FF00) >> 8);
            return unchecked((long)value);
        }

        /// <summary>
        /// Get the 32-bit floating point number using pointer arithmetics.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>
        /// The 32-bit float stored in the buffer using the reverse machine endianness at the given offset.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 4 bytes is within the buffer array provided. No checks are made by this function, and may result in an
        /// incorrect conversion. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// </remarks>
        public unsafe static float DangerousTo32FloatPointerSwap(byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                uint iValue = *(uint*)pointer;
                iValue = ((iValue & 0x0000FFFF) << 16) | ((iValue & 0xFFFF0000) >> 16);
                iValue = ((iValue & 0x00FF00FF) << 8) | ((iValue & 0xFF00FF00) >> 8);
                return *(float*)&iValue;
            }
        }

        /// <summary>
        /// Get the 64-bit floating point number using pointer arithmetics.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>
        /// The 64-bit double stored in the buffer using the reverse machine endianness at the given offset.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        /// <remarks>
        /// Usage of this function is DANGEROUS and is not recommended. You must ensure that the offset and the length
        /// of 8 bytes is within the buffer array provided. No checks are made by this function, and may result in an
        /// incorrect conversion. It is only provided for advanced programming with high performance. Consider using
        /// another copy function instead.
        /// </remarks>
        public unsafe static double DangerousTo64FloatPointerSwap(byte[] buffer, int offset)
        {
            fixed (byte* pointer = &buffer[offset]) {
                ulong lValue = *(ulong*)pointer;
                lValue = ((lValue & 0x00000000FFFFFFFF) << 32) | ((lValue & 0xFFFFFFFF00000000) >> 32);
                lValue = ((lValue & 0x0000FFFF0000FFFF) << 16) | ((lValue & 0xFFFF0000FFFF0000) >> 16);
                lValue = ((lValue & 0x00FF00FF00FF00FF) << 8) | ((lValue & 0xFF00FF00FF00FF00) >> 8);
                return *(double*)&lValue;
            }
        }
        #endregion

        #region Convert by Shifting
        /// <summary>
        /// Convert bytes to 16-bit integer by shifting its bytes in little endian order.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static short To16ShiftLittleEndian(byte[] buffer, int offset)
        {
            ushort value = buffer[offset];
            return unchecked((short)(value | (buffer[offset + 1] << 8)));
        }

        /// <summary>
        /// Convert bytes to 32-bit integer by shifting its bytes in little endian order.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 32-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static int To32ShiftLittleEndian(byte[] buffer, int offset)
        {
            int value = buffer[offset];
            value |= buffer[offset + 1] << 8;
            value |= buffer[offset + 2] << 16;
            return value | (buffer[offset + 3] << 24);
        }

        /// <summary>
        /// Convert bytes to 64-bit integer by shifting its bytes in little endian order.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 64-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static long To64ShiftLittleEndian(byte[] buffer, int offset)
        {
            ulong value = buffer[offset];
            value |= (ulong)buffer[offset + 1] << 8;
            value |= (ulong)buffer[offset + 2] << 16;
            value |= (ulong)buffer[offset + 3] << 24;
            value |= (ulong)buffer[offset + 4] << 32;
            value |= (ulong)buffer[offset + 5] << 40;
            value |= (ulong)buffer[offset + 6] << 48;
            value |= (ulong)buffer[offset + 7] << 56;
            return unchecked((long)value);
        }

        /// <summary>
        /// Convert bytes to 32-bit float in little endian format.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 32-bit float stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static float To32FloatShiftLittleEndian(byte[] buffer, int offset)
        {
            unsafe {
                uint value = buffer[offset];
                value |= (uint)buffer[offset + 1] << 8;
                value |= (uint)buffer[offset + 2] << 16;
                value |= (uint)buffer[offset + 3] << 24;
                return *(float*)&value;
            }
        }

        /// <summary>
        /// Convert bytes to 64-bit float in little endian format.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 64-bit double stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static double To64FloatShiftLittleEndian(byte[] buffer, int offset)
        {
            unsafe {
                ulong value = buffer[offset];
                value |= (ulong)buffer[offset + 1] << 8;
                value |= (ulong)buffer[offset + 2] << 16;
                value |= (ulong)buffer[offset + 3] << 24;
                value |= (ulong)buffer[offset + 4] << 32;
                value |= (ulong)buffer[offset + 5] << 40;
                value |= (ulong)buffer[offset + 6] << 48;
                value |= (ulong)buffer[offset + 7] << 56;
                return *(double*)&value;
            }
        }

        /// <summary>
        /// Convert bytes to 16-bit integer by shifting its bytes in big endian order.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer in big endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static short To16ShiftBigEndian(byte[] buffer, int offset)
        {
            ushort value = (ushort)(buffer[offset] << 8);
            return unchecked((short)(value | (buffer[offset + 1])));
        }

        /// <summary>
        /// Convert bytes to 32-bit integer by shifting its bytes in big endian order.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 32-bit value stored in the buffer in big endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static int To32ShiftBigEndian(byte[] buffer, int offset)
        {
            int value = buffer[offset] << 24;
            value |= buffer[offset + 1] << 16;
            value |= buffer[offset + 2] << 8;
            return value | (buffer[offset + 3]);
        }

        /// <summary>
        /// Convert bytes to 64-bit integer by shifting its bytes in big endian order.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 64-bit value stored in the buffer in big endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static long To64ShiftBigEndian(byte[] buffer, int offset)
        {
            ulong value = (ulong)buffer[offset] << 56;
            value |= (ulong)buffer[offset + 1] << 48;
            value |= (ulong)buffer[offset + 2] << 40;
            value |= (ulong)buffer[offset + 3] << 32;
            value |= (ulong)buffer[offset + 4] << 24;
            value |= (ulong)buffer[offset + 5] << 16;
            value |= (ulong)buffer[offset + 6] << 8;
            value |= buffer[offset + 7];
            return unchecked((long)value);
        }

        /// <summary>
        /// Convert bytes to 32-bit float in big endian format.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 32-bit float stored in the buffer in big endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static float To32FloatShiftBigEndian(byte[] buffer, int offset)
        {
            unsafe {
                uint value = (uint)buffer[offset] << 24;
                value |= (uint)buffer[offset + 1] << 16;
                value |= (uint)buffer[offset + 2] << 8;
                value |= buffer[offset + 3];
                return *(float*)&value;
            }
        }

        /// <summary>
        /// Convert bytes to 64-bit float in big endian format.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <returns>The 64-bit double stored in the buffer in big endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static double To64FloatShiftBigEndian(byte[] buffer, int offset)
        {
            unsafe {
                ulong value = (ulong)buffer[offset] << 56;
                value |= (ulong)buffer[offset + 1] << 48;
                value |= (ulong)buffer[offset + 2] << 40;
                value |= (ulong)buffer[offset + 3] << 32;
                value |= (ulong)buffer[offset + 4] << 24;
                value |= (ulong)buffer[offset + 5] << 16;
                value |= (ulong)buffer[offset + 6] << 8;
                value |= buffer[offset + 7];
                return *(double*)&value;
            }
        }

        /// <summary>
        /// Convert bytes to 16-bit integer by shifting its bytes in the endian order specified.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <param name="littleEndian">
        /// If <see langword="true"/>, convert from a source format in little endian, else convert from a source format
        /// in big endian.
        /// </param>
        /// <returns>The 16-bit value stored in the buffer at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static short To16Shift(byte[] buffer, int offset, bool littleEndian)
        {
            return littleEndian ? To16ShiftLittleEndian(buffer, offset) : To16ShiftBigEndian(buffer, offset);
        }

        /// <summary>
        /// Convert bytes to 32-bit integer by shifting its bytes in the endian order specified.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <param name="littleEndian">
        /// If <see langword="true"/>, convert from a source format in little endian, else convert from a source format
        /// in big endian.
        /// </param>
        /// <returns>The 32-bit value stored in the buffer at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static int To32Shift(byte[] buffer, int offset, bool littleEndian)
        {
            return littleEndian ? To32ShiftLittleEndian(buffer, offset) : To32ShiftBigEndian(buffer, offset);
        }

        /// <summary>
        /// Convert bytes to 64-bit integer by shifting its bytes in the endian order specified.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <param name="littleEndian">
        /// If <see langword="true"/>, convert from a source format in little endian, else convert from a source format
        /// in big endian.
        /// </param>
        /// <returns>The 64-bit value stored in the buffer at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static long To64Shift(byte[] buffer, int offset, bool littleEndian)
        {
            return littleEndian ? To64ShiftLittleEndian(buffer, offset) : To64ShiftBigEndian(buffer, offset);
        }

        /// <summary>
        /// Convert bytes to 32-bit float in the endian order specified.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <param name="littleEndian">
        /// If <see langword="true"/>, convert from a source format in little endian, else convert from a source format
        /// in big endian.
        /// </param>
        /// <returns>The 32-bit float stored in the buffer at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static float To32FloatShift(byte[] buffer, int offset, bool littleEndian)
        {
            return littleEndian ? To32FloatShiftLittleEndian(buffer, offset) : To32FloatShiftBigEndian(buffer, offset);
        }

        /// <summary>
        /// Convert bytes to 64-bit float in the endian order specified.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="offset">The offset in the source buffer.</param>
        /// <param name="littleEndian">
        /// If <see langword="true"/>, convert from a source format in little endian, else convert from a source format
        /// in big endian.
        /// </param>
        /// <returns>The 64-bit double stored in the buffer at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> with an <paramref name="offset"/> outside its bounds, has
        /// been made.
        /// </exception>
        public static double To64FloatShift(byte[] buffer, int offset, bool littleEndian)
        {
            return littleEndian ? To64FloatShiftLittleEndian(buffer, offset) : To64FloatShiftBigEndian(buffer, offset);
        }
        #endregion

#if NETSTANDARD2_1
        #region Convert by Shifting
        /// <summary>
        /// Convert bytes to 16-bit integer by shifting its bytes in little endian order.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static short To16ShiftLittleEndian(ReadOnlySpan<byte> buffer)
        {
            ushort value = buffer[0];
            return unchecked((short)(value | (buffer[1] << 8)));
        }

        /// <summary>
        /// Convert bytes to 32-bit integer by shifting its bytes in little endian order.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static int To32ShiftLittleEndian(ReadOnlySpan<byte> buffer)
        {
            int value = buffer[0];
            value |= buffer[1] << 8;
            value |= buffer[2] << 16;
            return value | (buffer[3] << 24);
        }

        /// <summary>
        /// Convert bytes to 64-bit integer by shifting its bytes in little endian order.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static long To64ShiftLittleEndian(ReadOnlySpan<byte> buffer)
        {
            ulong value = buffer[0];
            value |= (ulong)buffer[1] << 8;
            value |= (ulong)buffer[2] << 16;
            value |= (ulong)buffer[3] << 24;
            value |= (ulong)buffer[4] << 32;
            value |= (ulong)buffer[5] << 40;
            value |= (ulong)buffer[6] << 48;
            value |= (ulong)buffer[7] << 56;
            return unchecked((long)value);
        }

        /// <summary>
        /// Convert bytes to 32-bit float in little endian format.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static float To32FloatShiftLittleEndian(ReadOnlySpan<byte> buffer)
        {
            unsafe {
                uint value = buffer[0];
                value |= (uint)buffer[1] << 8;
                value |= (uint)buffer[2] << 16;
                value |= (uint)buffer[3] << 24;
                return *(float*)&value;
            }
        }

        /// <summary>
        /// Convert bytes to 64-bit float in little endian format.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static double To64FloatShiftLittleEndian(ReadOnlySpan<byte> buffer)
        {
            unsafe {
                ulong value = buffer[0];
                value |= (ulong)buffer[1] << 8;
                value |= (ulong)buffer[2] << 16;
                value |= (ulong)buffer[3] << 24;
                value |= (ulong)buffer[4] << 32;
                value |= (ulong)buffer[5] << 40;
                value |= (ulong)buffer[6] << 48;
                value |= (ulong)buffer[7] << 56;
                return *(double*)&value;
            }
        }

        /// <summary>
        /// Convert bytes to 16-bit integer by shifting its bytes in big endian order.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static short To16ShiftBigEndian(ReadOnlySpan<byte> buffer)
        {
            ushort value = (ushort)(buffer[0] << 8);
            return unchecked((short)(value | (buffer[1])));
        }

        /// <summary>
        /// Convert bytes to 32-bit integer by shifting its bytes in big endian order.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static int To32ShiftBigEndian(ReadOnlySpan<byte> buffer)
        {
            int value = buffer[0] << 24;
            value |= buffer[1] << 16;
            value |= buffer[2] << 8;
            return value | (buffer[3]);
        }

        /// <summary>
        /// Convert bytes to 64-bit integer by shifting its bytes in big endian order.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static long To64ShiftBigEndian(ReadOnlySpan<byte> buffer)
        {
            ulong value = (ulong)buffer[0] << 56;
            value |= (ulong)buffer[1] << 48;
            value |= (ulong)buffer[2] << 40;
            value |= (ulong)buffer[3] << 32;
            value |= (ulong)buffer[4] << 24;
            value |= (ulong)buffer[5] << 16;
            value |= (ulong)buffer[6] << 8;
            value |= buffer[7];
            return unchecked((long)value);
        }

        /// <summary>
        /// Convert bytes to 32-bit float in big endian format.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static float To32FloatShiftBigEndian(ReadOnlySpan<byte> buffer)
        {
            unsafe {
                uint value = (uint)buffer[0] << 24;
                value |= (uint)buffer[1] << 16;
                value |= (uint)buffer[2] << 8;
                value |= buffer[3];
                return *(float*)&value;
            }
        }

        /// <summary>
        /// Convert bytes to 64-bit float in big endian format.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <returns>The 16-bit value stored in the buffer in little endian at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static double To64FloatShiftBigEndian(ReadOnlySpan<byte> buffer)
        {
            unsafe {
                ulong value = (ulong)buffer[0] << 56;
                value |= (ulong)buffer[1] << 48;
                value |= (ulong)buffer[2] << 40;
                value |= (ulong)buffer[3] << 32;
                value |= (ulong)buffer[4] << 24;
                value |= (ulong)buffer[5] << 16;
                value |= (ulong)buffer[6] << 8;
                value |= buffer[7];
                return *(double*)&value;
            }
        }

        /// <summary>
        /// Convert bytes to 16-bit integer by shifting its bytes in the endian order specified.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="littleEndian">
        /// If <see langword="true"/>, convert from a source format in little endian, else convert from a source format
        /// in big endian.
        /// </param>
        /// <returns>The 16-bit value stored in the buffer at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static short To16Shift(ReadOnlySpan<byte> buffer, bool littleEndian)
        {
            return littleEndian ? To16ShiftLittleEndian(buffer) : To16ShiftBigEndian(buffer);
        }

        /// <summary>
        /// Convert bytes to 32-bit integer by shifting its bytes in the endian order specified.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="littleEndian">
        /// If <see langword="true"/>, convert from a source format in little endian, else convert from a source format
        /// in big endian.
        /// </param>
        /// <returns>The 32-bit value stored in the buffer at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static int To32Shift(ReadOnlySpan<byte> buffer, bool littleEndian)
        {
            return littleEndian ? To32ShiftLittleEndian(buffer) : To32ShiftBigEndian(buffer);
        }

        /// <summary>
        /// Convert bytes to 64-bit integer by shifting its bytes in the endian order specified.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="littleEndian">
        /// If <see langword="true"/>, convert from a source format in little endian, else convert from a source format
        /// in big endian.
        /// </param>
        /// <returns>The 64-bit value stored in the buffer at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static long To64Shift(ReadOnlySpan<byte> buffer, bool littleEndian)
        {
            return littleEndian ? To64ShiftLittleEndian(buffer) : To64ShiftBigEndian(buffer);
        }

        /// <summary>
        /// Convert bytes to 32-bit float in the endian order specified.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="littleEndian">
        /// If <see langword="true"/>, convert from a source format in little endian, else convert from a source format
        /// in big endian.
        /// </param>
        /// <returns>The 32-bit float stored in the buffer at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static float To32FloatShift(ReadOnlySpan<byte> buffer, bool littleEndian)
        {
            return littleEndian ? To32FloatShiftLittleEndian(buffer) : To32FloatShiftBigEndian(buffer);
        }

        /// <summary>
        /// Convert bytes to 64-bit float in the endian order specified.
        /// </summary>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="littleEndian">
        /// If <see langword="true"/>, convert from a source format in little endian, else convert from a source format
        /// in big endian.
        /// </param>
        /// <returns>The 64-bit double stored in the buffer at the given offset.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// An attempt to access the <paramref name="buffer"/> outside its bounds has been made.
        /// </exception>
        public static double To64FloatShift(ReadOnlySpan<byte> buffer, bool littleEndian)
        {
            return littleEndian ? To64FloatShiftLittleEndian(buffer) : To64FloatShiftBigEndian(buffer);
        }
        #endregion
#endif
    }
}
