﻿namespace RJCP.Core
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

#if NET45_OR_GREATER || NET6_0_OR_GREATER
    using System.Runtime.ExceptionServices;
#endif

    /// <summary>
    /// Represents extension methods for type <see cref="Result{T}"/>.
    /// </summary>
    public static class Result
    {
        /// <summary>
        /// Creates a new instance of <see cref="Result{T}"/> from the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to be placed to the container.</param>
        /// <returns>The value encapsulated by <see cref="Result{T}"/>.</returns>
        public static Result<T> FromValue<T>(T value)
        {
            return new Result<T>(value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Result{T}"/> from the specified exception.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="e">The exception to be placed to the container.</param>
        /// <returns>The exception encapsulated by <see cref="Result{T}"/>.</returns>
        /// <remarks>
        /// This method is not inlined, that the stack trace is reliable.
        /// </remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Result<T> FromException<T>(Exception e)
        {
            ThrowHelper.ThrowIfNull(e);
            if (e.StackTrace is null) {
                StackTrace stack = new(1, true);
                return new Result<T>(e, stack);
            }
            return new Result<T>(e, null);
        }

        /// <summary>
        /// Gets a reference to the underlying value.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="result">The result container.</param>
        /// <returns>The reference to the result.</returns>
        /// <exception cref="Exception">The result is unavailable.</exception>
        public static ref readonly T GetReference<T>(in Result<T> result)
        {
            return ref Result<T>.GetReference(in result);
        }
    }

    /// <summary>
    /// Represents a result of operation which can be actual result or exception.
    /// </summary>
    /// <typeparam name="T">The type of the value stored in the Result monad.</typeparam>
    public readonly struct Result<T>
    {
        private readonly T m_Value;
#if NET45_OR_GREATER || NET6_0_OR_GREATER
        private readonly ExceptionDispatchInfo m_Exception;
#else
        private readonly Exception m_Exception;
#endif

        /// <summary>
        /// Initializes a new successful result.
        /// </summary>
        /// <param name="value">The value to be stored as result.</param>
        public Result(T value)
        {
            m_Value = value;
            m_Exception = null;
        }

        /// <summary>
        /// Initializes a new unsuccessful result.
        /// </summary>
        /// <param name="error">The exception representing error. Cannot be <see langword="null"/>.</param>
        /// <remarks>
        /// This method is not inlined, that the stack trace is reliable.
        /// </remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Result(Exception error)
        {
            ThrowHelper.ThrowIfNull(error);
#if NET6_0_OR_GREATER
            if (error.StackTrace is not null) {
                m_Exception = ExceptionDispatchInfo.Capture(error);
            } else {
                m_Exception = ExceptionDispatchInfo.Capture(ExceptionDispatchInfo.SetCurrentStackTrace(error));
            }
#elif NET45_OR_GREATER
            if (error.StackTrace is not null) {
                m_Exception = ExceptionDispatchInfo.Capture(error);
            } else {
                StackTrace stack = new(1, true);
                m_Exception = ExceptionDispatchInfo.Capture(ExceptionExtensions.SetStackTrace(error, stack));
            }
#else
            if (error.StackTrace is not null) {
                m_Exception = error;
            } else {
                StackTrace stack = new(1, true);
                m_Exception = ExceptionExtensions.SetStackTrace(error, stack);
            }
#endif
            m_Value = default;
        }

        internal Result(Exception error, StackTrace stack)
        {
#if NET6_0_OR_GREATER
            if (stack is null || error.StackTrace is not null) {
                m_Exception = ExceptionDispatchInfo.Capture(error);
            } else {
                m_Exception = ExceptionDispatchInfo.Capture(ExceptionDispatchInfo.SetRemoteStackTrace(error, stack.ToString()));
            }
#elif NET45_OR_GREATER
            if (stack is null || error.StackTrace is not null) {
                m_Exception = ExceptionDispatchInfo.Capture(error);
            } else {
                m_Exception = ExceptionDispatchInfo.Capture(ExceptionExtensions.SetStackTrace(error, stack));
            }
#else
            if (stack is null || error.StackTrace is not null) {
                m_Exception = error;
            } else {
                m_Exception = ExceptionExtensions.SetStackTrace(error, stack);
            }
#endif
            m_Value = default;
        }

#if NET45_OR_GREATER || NET6_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static ref readonly T GetReference(in Result<T> result)
        {
            result.Validate();
            return ref result.m_Value;
        }

        /// <summary>
        /// Indicates that the result is successful.
        /// </summary>
        /// <value><see langword="true"/> if this result is successful; <see langword="false"/> if this result represents exception.</value>
        public bool HasValue
        {
            get
            {
                return m_Exception is null;
            }
        }

        private void Validate()
        {
#if NET45_OR_GREATER || NET6_0_OR_GREATER
            //if (m_Exception is not null) m_Exception.Throw();
            m_Exception?.Throw();
#else
            if (m_Exception is not null) throw m_Exception;
#endif
        }

        /// <summary>
        /// Extracts the actual result.
        /// </summary>
        /// <exception cref="Exception">This result is not successful.</exception>
        public T Value
        {
            get
            {
                Validate();
                return m_Value;
            }
        }

        /// <summary>
        /// Attempts to extract value from container if it is present.
        /// </summary>
        /// <param name="value">Extracted value.</param>
        /// <returns><see langword="true"/> if value is present; otherwise, <see langword="false"/>.</returns>
        public bool TryGet(out T value)
        {
            value = m_Value;
            return m_Exception is null;
        }

        /// <summary>
        /// Gets exception associated with this result.
        /// </summary>
        public Exception Error
        {
            get
            {
#if NET45_OR_GREATER || NET6_0_OR_GREATER
                return m_Exception?.SourceException;
#else
                return m_Exception;
#endif
            }
        }

        /// <summary>
        /// Extracts actual result.
        /// </summary>
        /// <param name="result">The result object.</param>
        /// <returns>The value contained within the result.</returns>
        /// <exception cref="Exception">This result is not successful.</exception>
        public static explicit operator T(Result<T> result)
        {
            return result.Value;
        }

        /// <summary>
        /// Converts value into the result.
        /// </summary>
        /// <param name="result">The result to be converted.</param>
        /// <returns>The result representing <paramref name="result"/> value.</returns>
        public static implicit operator Result<T>(T result)
        {
            return new Result<T>(result);
        }

        /// <summary>
        /// Indicates that both results are successful.
        /// </summary>
        /// <param name="left">The first result to check.</param>
        /// <param name="right">The second result to check.</param>
        /// <returns><see langword="true"/> if both results are successful; otherwise, <see langword="false"/>.</returns>
        public static bool operator &(in Result<T> left, in Result<T> right)
        {
            return left.m_Exception is null && right.m_Exception is null;
        }

        /// <summary>
        /// Indicates that the result is successful.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <returns><see langword="true"/> if this result is successful; <see langword="false"/> if this result represents exception.</returns>
        public static bool operator true(in Result<T> result)
        {
            return result.m_Exception is null;
        }

        /// <summary>
        /// Indicates that the result represents error.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <returns><see langword="false"/> if this result is successful; <see langword="true"/> if this result represents exception.</returns>
        public static bool operator false(in Result<T> result)
        {
            // Note, this appears not testable, as we don't have a short-circuit operator that we can use.
            return result.m_Exception is not null;
        }

        /// <summary>
        /// Returns textual representation of this object.
        /// </summary>
        /// <returns>The textual representation of this object.</returns>
        public override string ToString()
        {
#if NET45_OR_GREATER || NET6_0_OR_GREATER
            return m_Exception?.SourceException.ToString() ?? m_Value?.ToString() ?? "<NULL>";
#else
            return m_Exception?.ToString() ?? m_Value?.ToString() ?? "<NULL>";
#endif
        }
    }
}
