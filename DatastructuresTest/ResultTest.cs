namespace RJCP.Core
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class ResultTest
    {
        private static Result<int> ParseSuccess()
        {
            return 42;
        }

        private static Result<int> ParseError()
        {
            return Result.FromException<int>(new ArgumentException("Test argument error"));
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
            Console.WriteLine($"{result}");
            Assert.That(result.Error, Is.TypeOf<ArgumentException>());
            Console.WriteLine($"{result.Error.StackTrace}");
            Assert.That(result.HasValue, Is.False);
        }

        [Test]
        public void ExceptionFromConstructor()
        {
            var result = new Result<int>(new ArgumentException("Test argument error"));
            Assert.That(result.Error, Is.TypeOf<ArgumentException>());
            Assert.That(result.HasValue, Is.False);
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
        public void ExceptionFalse()
        {
            var result = ParseError();
            Assert.That(result.HasValue, Is.False);
            if (result) {
                Assert.Fail();
            } else {
                Assert.Pass();
            }
        }

        [Test]
        public void ExceptionOnExplicitAccess()
        {
            var result = ParseError();
            Assert.That(() => {
                _ = (int)result;
            }, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void ExceptionOnValueAccess()
        {
            var result = ParseError();
            Assert.That(() => {
                _ = result.Value;
            }, Throws.TypeOf<ArgumentException>());
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
    }
}
