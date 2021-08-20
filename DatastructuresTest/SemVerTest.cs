namespace RJCP.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using NUnit.Framework;

    [SuppressMessage("Assertion", "NUnit2010:Use EqualConstraint for better assertion messages in case of failure", Justification = "Direct testing of operators")]
    [SuppressMessage("Assertion", "NUnit2043:Use ComparisonConstraint for better assertion messages in case of failure", Justification = "Direct testing of operators")]
    public class SemVerTest
    {
        [TestCase("0.1.0", 0, 1, 0, TestName = "Ver_0.1.0")]
        [TestCase("1.0.0", 1, 0, 0, TestName = "Ver_1.0.0")]
        [TestCase("1.1.2", 1, 1, 2, TestName = "Ver_1.1.2")]

        public void BasicSemVer(string version, int major, int minor, int patch)
        {
            SemVer v = new SemVer(version);
            Assert.That(v.Major, Is.EqualTo(major));
            Assert.That(v.Minor, Is.EqualTo(minor));
            Assert.That(v.Patch, Is.EqualTo(patch));
            Assert.That(v.HasBuild, Is.False);
            Assert.That(v.PreRelease, Is.EqualTo(string.Empty));
            Assert.That(v.IsPreRelease, Is.False);
        }

        [TestCase("0.1.0.0", 0, 1, 0, 0, TestName = "Ver_0.1.0.0")]
        [TestCase("1.0.0.0", 1, 0, 0, 0, TestName = "Ver_1.0.0.0")]
        [TestCase("1.1.2.234", 1, 1, 2, 234, TestName = "Ver_1.1.2.234")]
        public void SemVerWithBuild(string version, int major, int minor, int patch, int build)
        {
            SemVer v = new SemVer(version);
            Assert.That(v.Major, Is.EqualTo(major));
            Assert.That(v.Minor, Is.EqualTo(minor));
            Assert.That(v.Patch, Is.EqualTo(patch));
            Assert.That(v.HasBuild, Is.True);
            Assert.That(v.Build, Is.EqualTo(build));
            Assert.That(v.PreRelease, Is.EqualTo(string.Empty));
            Assert.That(v.IsPreRelease, Is.False);
        }

        [TestCase("0.1.0-ci-20150505", 0, 1, 0, "ci-20150505", TestName = "Ver_0.1.0-ci-20150505")]
        [TestCase("1.0.0-alpha", 1, 0, 0, "alpha", TestName = "Ver_1.0.0-alpha")]
        [TestCase("1.1.2-beta", 1, 1, 2, "beta", TestName = "Ver_1.1.2-beta")]
        [TestCase("1.0.123-prex.2", 1, 0, 123, "prex.2", TestName = "Ver_1.0.123-prex.2")]
        public void SemVerWithPreRelease(string version, int major, int minor, int patch, string preRelease)
        {
            SemVer v = new SemVer(version);
            Assert.That(v.Major, Is.EqualTo(major));
            Assert.That(v.Minor, Is.EqualTo(minor));
            Assert.That(v.Patch, Is.EqualTo(patch));
            Assert.That(v.HasBuild, Is.False);
            Assert.That(v.PreRelease, Is.EqualTo(preRelease));
            Assert.That(v.IsPreRelease, Is.True);
        }

        [TestCase("0.1.0.0-ci-20150505", 0, 1, 0, 0, "ci-20150505", TestName = "Ver_0.1.0.0-ci-20150505")]
        [TestCase("1.0.0.0-alpha", 1, 0, 0, 0, "alpha", TestName = "Ver_1.0.0.0-alpha")]
        [TestCase("1.1.2.234-beta", 1, 1, 2, 234, "beta", TestName = "Ver_1.1.2.234-beta")]
        public void SemVerWithBuildPreRelease(string version, int major, int minor, int patch, int build, string preRelease)
        {
            SemVer v = new SemVer(version);
            Assert.That(v.Major, Is.EqualTo(major));
            Assert.That(v.Minor, Is.EqualTo(minor));
            Assert.That(v.Patch, Is.EqualTo(patch));
            Assert.That(v.HasBuild, Is.True);
            Assert.That(v.Build, Is.EqualTo(build));
            Assert.That(v.PreRelease, Is.EqualTo(preRelease));
            Assert.That(v.IsPreRelease, Is.True);
        }

        [Test]
        public void SemVerWithNullPreRelease()
        {
            SemVer v = new SemVer(1, 0, 0, 0, null);
            Assert.That(v.ToString(), Is.EqualTo("1.0.0.0"));
            Assert.That(v.Major, Is.EqualTo(1));
            Assert.That(v.Minor, Is.EqualTo(0));
            Assert.That(v.Patch, Is.EqualTo(0));
            Assert.That(v.Build, Is.EqualTo(0));
            Assert.That(v.PreRelease, Is.EqualTo(string.Empty));
            Assert.That(v.IsPreRelease, Is.False);
        }

        [TestCase("")]
        [TestCase("1")]                              // Must be X.Y.Z
        [TestCase("1.0")]
        [TestCase("1..123")]
        [TestCase("...123")]
        [TestCase("1.")]
        [TestCase("1..")]
        [TestCase("1...")]
        [TestCase("1.0..")]
        [TestCase("1.0.1.")]
        [TestCase("-1.0.1")]
        [TestCase("1.-0.1")]
        [TestCase("1.0.-1")]
        [TestCase("a1.0.1")]
        [TestCase("1.b.1")]
        [TestCase("1.b1.1")]
        [TestCase("1.1b1.1")]
        [TestCase("1.1.c2")]
        [TestCase("1.1.2.c")]
        [TestCase("1.1.2.99999999999999999999")]
        [TestCase("1.0.1.-65535")]
        [TestCase("1.0-alpha")]                      // Must be X.Y.Z-PRE
        [TestCase("1.0.1.-foo")]
        [TestCase("1.00.0000")]                      // No preceding zeros
        [TestCase("1.01.123")]
        [TestCase("1.0.0123")]
        [TestCase("01.0.123")]
        [TestCase("1.0.123.065535")]
        [TestCase("1.0.123-prex~2")]
        [TestCase("1.0.123-1")]
        [TestCase("1.0.0-pre1+foo.1")]
        [TestCase("1.0.0-pre1+foo.2")]
        public void InvalidVersion(string version)
        {
            Assert.That(() => { _ = new SemVer(version); }, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void InvalidSemVerNull()
        {
            Assert.That(() => { _ = new SemVer(null); }, Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase("1.0.0.0", "1.0.0.1")]
        [TestCase("1.0.0-alpha", "1.0.0-alpha.1")]
        [TestCase("1.0.0-alpha.1", "1.0.0-alpha.beta")]
        [TestCase("1.0.0-alpha.beta", "1.0.0-beta")]
        [TestCase("1.0.0-beta", "1.0.0-beta.11")]
        [TestCase("1.0.0-beta.11", "1.0.0-beta.2")]
        [TestCase("1.0.0-beta.11", "1.0.0-rc.1")]
        [TestCase("1.0.0-rc.1", "1.0.0")]
        [TestCase("1.0.0", "2.0.0")]
        [TestCase("2.0.0", "2.1.0")]
        [TestCase("2.1.0", "2.1.1")]
        public void Compare(string left, string right)
        {
            SemVer leftVersion = new SemVer(left);
            SemVer rightVersion = new SemVer(right);

            SemVer left2 = leftVersion;
            SemVer right2 = rightVersion;

            Assert.That(leftVersion == left2);
            Assert.That(rightVersion == right2);
            Assert.That(leftVersion.CompareTo(leftVersion) == 0);
            Assert.That(rightVersion.CompareTo(rightVersion) == 0);

            Assert.That(leftVersion < rightVersion);
            Assert.That(rightVersion > leftVersion);
            Assert.That(leftVersion != rightVersion);
            Assert.That(leftVersion.GetHashCode() != rightVersion.GetHashCode());

            Assert.That(leftVersion.CompareTo(rightVersion) < 0);
            Assert.That(rightVersion.CompareTo(leftVersion) > 0);
        }

        [Test]
        public void CompareNull()
        {
            SemVer vn1 = null;
            SemVer vn2 = null;
            SemVer vv1 = new SemVer("1.0.0");
            SemVer vv2 = new SemVer("2.0.0");

            Assert.That(vn1 == vn2);
            Assert.That(vn1 <= vn2);
            Assert.That(vn1 >= vn2);

            Assert.That(vn1 < vv1);
            Assert.That(vn1 <= vv1);
            Assert.That(vv2 > vn2);
            Assert.That(vv2 >= vn2);
        }

        [TestCase("1.0.0.0", "1.0.0.0")]
        [TestCase("1.0.0", "1.0.0.0")]
        public void CompareEqual(string left, string right)
        {
            SemVer leftVersion = new SemVer(left);
            SemVer rightVersion = new SemVer(right);

            Assert.That(leftVersion == new SemVer(left));
            Assert.That(rightVersion == new SemVer(right));
            Assert.That(leftVersion.CompareTo(leftVersion) == 0);
            Assert.That(rightVersion.CompareTo(rightVersion) == 0);

            Assert.That(leftVersion == rightVersion);
            Assert.That(leftVersion.Equals(rightVersion));
            Assert.That(rightVersion.Equals(leftVersion));
            Assert.That(leftVersion.GetHashCode() == rightVersion.GetHashCode());

            Assert.That(leftVersion.CompareTo(rightVersion) == 0);
            Assert.That(rightVersion.CompareTo(leftVersion) == 0);
        }

        [TestCase("1.0.0")]
        [TestCase("1.0.0-beta")]
        [TestCase("1.0.0.1")]
        [TestCase("1.0.0.1-beta")]
        public void CompareToSemVer2(string version)
        {
            SemVer2 left = new SemVer2(version);
            SemVer right = new SemVer(version);

            // The right object can't be is downcast to the left object. Thus it is treated as newer.
            Assert.That(() => { left.CompareTo(right); }, Throws.TypeOf<ArgumentException>());
        }
    }
}