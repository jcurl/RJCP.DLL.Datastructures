namespace RJCP.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using NUnit.Framework;

    [TestFixture]
    public class ResultTest
    {
        private static bool HasStackFrame(string stackTrace, string fragment, bool first = true)
        {
            Console.WriteLine("{0}", stackTrace);
            string[] stackframes = stackTrace.Split('\n');
            if (first)
                return stackframes[0].Contains(fragment);

            foreach (string frame in stackframes) {
                if (frame.Contains(fragment)) return true;
            }
            return false;
        }

        private static Result<int> ParseSuccess()
        {
            return 42;
        }

        // For tests to work that refer to the stack trace (and we look for 'ParseError`), we must ensure that this
        // method is not inlined, so that it shows up in the stack trace.
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static Result<int> ParseError()
        {
            return Result.FromException<int>(new ArgumentException("Test argument error"));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static Result<int> ParseRaisedError()
        {
            try {
                int v = -1;
                ThrowHelper.ThrowIfNegative(v);
            } catch (Exception ex) {
                return Result.FromException<int>(ex);
            }
            return 42;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static Result<int> ParseRaisedErrorCtor()
        {
            try {
                int v = -1;
                ThrowHelper.ThrowIfNegative(v);
            } catch (Exception ex) {
                return new Result<int>(ex);
            }
            return 42;
        }

        [Test]
        public void ImplicitSuccess()
        {
            var result = ParseSuccess();
            Assert.That(result.Value, Is.EqualTo(42));
            Assert.That(result.HasValue, Is.True);
        }

        [Test]
        public void Success()
        {
            var result = new Result<int>(10);
            Assert.That(result.Value, Is.EqualTo(10));
            Assert.That(result.HasValue, Is.True);
        }

        [Test]
        public void ExplicitSuccess()
        {
            var result = new Result<int>(10);
            int value = (int)result;
            Assert.That(value, Is.EqualTo(10));
            Assert.That(result.HasValue, Is.True);
        }

        [Test]
        public void SuccessFromValue()
        {
            var result = Result.FromValue(42);
            Assert.That(result.Value, Is.EqualTo(42));
            Assert.That(result.HasValue, Is.True);
        }

        [Test]
        public void SuccessFromConstructor()
        {
            var result = new Result<int>(42);
            Assert.That(result.Value, Is.EqualTo(42));
            Assert.That(result.HasValue, Is.True);
        }

        [Test]
        public void Exception()
        {
            var result = ParseError();
            Assert.That(result.Error, Is.TypeOf<ArgumentException>());
            Assert.That(result.Error.StackTrace, Is.Not.Null);
            Assert.That(HasStackFrame(result.Error.StackTrace, "RJCP.Core.ResultTest.ParseError"), Is.True);
            Assert.That(result.HasValue, Is.False);
        }

        [Test]
        public void ExceptionFromConstructor()
        {
            var result = new Result<int>(new ArgumentException("Test argument error"));
            Assert.That(result.Error, Is.TypeOf<ArgumentException>());
            Assert.That(result.Error.StackTrace, Is.Not.Null);
        }

        [Test]
        public void NullException()
        {
            Assert.That(() => {
                _ = Result.FromException<int>(null);
            }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void SuccessTrue()
        {
            var result = ParseSuccess();
            Assert.That(result.HasValue, Is.True);
            if (result) {
                Assert.Pass();
            } else {
                Assert.Fail();
            }
        }

        [Test]
        public void ExceptionOnExplicitAccess()
        {
            var result = ParseError();
            Exception captured = null;
            try {
                _ = (int)result;
            } catch (Exception ex) {
                captured = ex;
            }

            Assert.That(captured, Is.Not.Null);
            Assert.That(captured, Is.TypeOf<ArgumentException>());
            Assert.That(HasStackFrame(result.Error.StackTrace, "RJCP.Core.ResultTest.ParseError"), Is.True);
        }

        [Test]
        public void ExceptionOnValueAccess()
        {
            var result = ParseError();
            Exception captured = null;
            try {
                _ = result.Value;
            } catch (Exception ex) {
                captured = ex;
            }

            Assert.That(captured, Is.Not.Null);
            Assert.That(captured, Is.TypeOf<ArgumentException>());
            Assert.That(HasStackFrame(result.Error.StackTrace, "RJCP.Core.ResultTest.ParseError"), Is.True);
        }

        [Test]
        public void TryGetSuccess()
        {
            var result = ParseSuccess();
            Assert.That(result.TryGet(out int value), Is.True);
            Assert.That(value, Is.EqualTo(42));
        }

        [Test]
        public void TryGetError()
        {
            var result = ParseError();
            Assert.That(result.TryGet(out int _), Is.False);
        }

        private static int GetRef(int value)
        {
            return value;
        }

        [Test]
        public void GetReference()
        {
            var result = ParseSuccess();
            int value = GetRef(Result.GetReference(result));
            Assert.That(value, Is.EqualTo(42));
        }

        [Test]
        public void CheckResultsSuccess()
        {
            var r1 = ParseSuccess();
            var r2 = ParseSuccess();

            Assert.That(r1 & r2, Is.True);
            Assert.That(r2 & r1, Is.True);
        }

        [Test]
        public void CheckResultsError()
        {
            var r1 = ParseSuccess();
            var r2 = ParseError();
            var r3 = ParseError();

            Assert.That(r1 & r2, Is.False);
            Assert.That(r2 & r1, Is.False);
            Assert.That(r2 & r3, Is.False);
            Assert.That(r3 & r2, Is.False);
        }

        [Test]
        public void RaisedException()
        {
            var result = ParseRaisedError();
            Assert.That(result.Error, Is.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(result.Error.StackTrace, Is.Not.Null);
            Assert.That(HasStackFrame(result.Error.StackTrace, "RJCP.Core.ResultTest.ParseRaisedError", false), Is.True);
            Assert.That(result.HasValue, Is.False);
        }

        [Test]
        public void RaisedExceptionCtor()
        {
            var result = ParseRaisedErrorCtor();
            Assert.That(result.Error, Is.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(result.Error.StackTrace, Is.Not.Null);
            Assert.That(HasStackFrame(result.Error.StackTrace, "RJCP.Core.ResultTest.ParseRaisedError", false), Is.True);
            Assert.That(result.HasValue, Is.False);
        }
    }
}
