// One has to carefully look at the code. While it references mutable fields, they aren't changeable after the object is
// constructed (note, anyone deriving from this class needs to ensure that's the case also).

namespace RJCP.Core
{
    using System;
    using System.Runtime.Serialization;
    using System.Text;
    using Resources;

    /// <summary>
    /// Semantic Versioning 1.0.0.
    /// </summary>
    /// <remarks>
    /// Implements version 1.0.0 of the Semantic Versioning as specified at http://semver.org/spec/v1.0.0.html. The
    /// types of versions this class supports are: MAJOR.MINOR.PATCH[.BUILD][-PRERELEASE].
    /// </remarks>
    public sealed class SemVer : IComparable, IComparable<SemVer>, IEquatable<SemVer>, IDeepCloneable<SemVer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SemVer"/> class.
        /// </summary>
        /// <param name="major">The major version part.</param>
        /// <param name="minor">The minor version part.</param>
        /// <param name="patch">The patch version part.</param>
        public SemVer(int major, int minor, int patch)
            : this(major, minor, patch, 0, null)
        {
            m_HasBuild = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemVer"/> class.
        /// </summary>
        /// <param name="major">The major version part.</param>
        /// <param name="minor">The minor version part.</param>
        /// <param name="patch">The patch version part.</param>
        /// <param name="build">The optional build version part.</param>
        public SemVer(int major, int minor, int patch, int build)
            : this(major, minor, patch, build, null)
        {
            m_HasBuild = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemVer"/> class.
        /// </summary>
        /// <param name="major">The major version part.</param>
        /// <param name="minor">The minor version part.</param>
        /// <param name="patch">The patch version part.</param>
        /// <param name="preRelease">The prerelease string portion.</param>
        public SemVer(int major, int minor, int patch, string preRelease)
            : this(major, minor, patch, 0, preRelease)
        {
            m_HasBuild = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemVer"/> class.
        /// </summary>
        /// <param name="major">The major version part.</param>
        /// <param name="minor">The minor version part.</param>
        /// <param name="patch">The patch version part.</param>
        /// <param name="build">The optional build version part.</param>
        /// <param name="preRelease">The prerelease string portion.</param>
        public SemVer(int major, int minor, int patch, int build, string preRelease)
        {
            m_Major = major;
            m_Minor = minor;
            m_Patch = patch;
            m_Build = build;
            m_HasBuild = true;
            m_PreRelease = string.IsNullOrWhiteSpace(preRelease) ? string.Empty : preRelease;
            CheckProperties();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemVer"/> class.
        /// </summary>
        /// <param name="version">The version to interpret.</param>
        public SemVer(string version)
        {
            InternalParseSemVer(version);
        }

        private void CheckProperties()
        {
            if (m_Major < 0) {
                string message = string.Format(Messages.Infra_SemVer_MustBePositive, "Major");
                throw new ArgumentException(message);
            }
            if (m_Minor < 0) {
                string message = string.Format(Messages.Infra_SemVer_MustBePositive, "Minor");
                throw new ArgumentException(message);
            }
            if (m_Patch < 0) {
                string message = string.Format(Messages.Infra_SemVer_MustBePositive, "Patch");
                throw new ArgumentException(message);
            }
            if (m_Build < 0) {
                string message = string.Format(Messages.Infra_SemVer_MustBePositive, "Build");
                throw new ArgumentException(message);
            }
            CheckPreRelease(m_PreRelease);
        }

        private int m_Major;

        /// <summary>
        /// Gets the major version.
        /// </summary>
        /// <value>The major version.</value>
        public int Major
        {
            get { return m_Major; }
            private set
            {
                if (value < 0) {
                    string message = string.Format(Messages.Infra_SemVer_MustBePositive, "Major");
                    throw new ArgumentException(message);
                }
                m_Major = value;
                m_Version = null;
                m_HashCodeGenerated = false;
            }
        }

        private int m_Minor;

        /// <summary>
        /// Gets the minor version.
        /// </summary>
        /// <value>The minor version.</value>
        public int Minor
        {
            get { return m_Minor; }
            private set
            {
                if (value < 0) {
                    string message = string.Format(Messages.Infra_SemVer_MustBePositive, "Minor");
                    throw new ArgumentException(message);
                }
                m_Minor = value;
                m_Version = null;
                m_HashCodeGenerated = false;
            }
        }

        private int m_Patch;

        /// <summary>
        /// Gets the patch version.
        /// </summary>
        /// <value>The patch version.</value>
        public int Patch
        {
            get { return m_Patch; }
            private set
            {
                if (value < 0) {
                    string message = string.Format(Messages.Infra_SemVer_MustBePositive, "Patch");
                    throw new ArgumentException(message);
                }
                m_Patch = value;
                m_Version = null;
                m_HashCodeGenerated = false;
            }
        }

        private int m_Build;

        /// <summary>
        /// Gets the optional build version.
        /// </summary>
        /// <value>The optional build version.</value>
        /// <remarks>This section is not defined in Semantic Versioning 2.0.0, but is used by NuGet.</remarks>
        public int Build
        {
            get { return m_HasBuild ? m_Build : 0; }
            private set
            {
                if (value < 0) {
                    string message = string.Format(Messages.Infra_SemVer_MustBePositive, "Build");
                    throw new ArgumentException(message);
                }
                m_Build = value;
                m_HasBuild = true;
                m_Version = null;
                m_HashCodeGenerated = false;
            }
        }

        private bool m_HasBuild;

        /// <summary>
        /// Gets a value indicating whether this instance has a build version.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if this instance has a build version; otherwise, <see langword="false"/>.
        /// </value>
        public bool HasBuild
        {
            get { return m_HasBuild; }
            private set { m_HasBuild = value; }
        }

        private string m_PreRelease = string.Empty;

        /// <summary>
        /// Gets the prerelease string.
        /// </summary>
        /// <value>The prerelease string.</value>
        public string PreRelease
        {
            get { return m_PreRelease; }
            private set
            {
                if (value == null) {
                    m_PreRelease = string.Empty;
                } else {
                    CheckPreRelease(value);
                    m_PreRelease = value;
                }
                m_Version = null;
                m_HashCodeGenerated = false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is prerelease.
        /// </summary>
        /// <value><see langword="true"/> if this instance is prerelease; otherwise, <see langword="false"/>.</value>
        public bool IsPreRelease
        {
            get { return !string.IsNullOrEmpty(m_PreRelease); }
        }

        private static void CheckPreRelease(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            for (int i = 0; i < value.Length; i++) {
                char c = value[i];
                if (!(c >= 'a' && c <= 'z' ||
                    c >= 'A' && c <= 'Z' ||
                    i != 0 && (c >= '0' && c <= '9' || c == '-' || c == '.')))
                    throw new ArgumentException(Messages.Infra_SemVer_IllegalCharInPreRelease);
            }
        }

        /// <summary>
        /// Parses the string into a SemVer and sets its properties.
        /// </summary>
        /// <param name="version">The version to parse.</param>
        /// <returns>Returns a new <see cref="SemVer"/> object from the string.</returns>
        /// <exception cref="ArgumentNullException">version may not be null.</exception>
        /// <exception cref="ArgumentException">Invalid Version.</exception>
        public static SemVer ParseSemVer(string version)
        {
            return new SemVer(version);
        }

        /// <summary>
        /// Tries to parse the string into a SemVer and sets its properties.
        /// </summary>
        /// <param name="version">The version to parse.</param>
        /// <param name="semVer">The <see cref="SemVer"/> on successful parse, undefined otherwise.</param>
        /// <returns><see langword="true"/> if it could be parsed, <c>false</c> otherwise.</returns>
        public static bool TryParseSemVer(string version, out SemVer semVer)
        {
            semVer = null;
            try {
                semVer = ParseSemVer(version);
            } catch (ArgumentException) {
                return false;
            }
            return true;
        }

        private void InternalParseSemVer(string version)
        {
            bool build = false;
            bool preRelease = false;

            if (version == null) throw new ArgumentNullException(nameof(version));
            int length = version.Length;

            int cursor = 0;
            m_Major = SemVerHelper.ParseNumber(version, ref cursor, '.', false);
            if (cursor >= length) throw new ArgumentException(Messages.Infra_SemVer_InvalidVersion);
            cursor++;

            m_Minor = SemVerHelper.ParseNumber(version, ref cursor, '.', false);
            if (cursor >= length) throw new ArgumentException(Messages.Infra_SemVer_InvalidVersion);
            cursor++;

            m_Patch = SemVerHelper.ParseNumber(version, ref cursor, new char[] { '-', '.' }, false);
            if (cursor < length) {
                switch (version[cursor]) {
                case '.':
                    build = true;
                    break;
                case '-':
                    preRelease = true;
                    break;
                }
                cursor++;
            }

            if (build) {
                m_Build = SemVerHelper.ParseNumber(version, ref cursor, '-', false);
                if (cursor < length) preRelease = true;
                cursor++;
            }
            m_HasBuild = build;

            if (preRelease) {
                m_PreRelease = SemVerHelper.ParseString(version, ref cursor);
            } else {
                m_PreRelease = string.Empty;
            }

            CheckProperties();
        }

        #region IComparable
        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates
        /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
        /// other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these
        /// meanings: Value Meaning Less than zero This instance precedes <paramref name="obj"/> in the sort order. Zero
        /// This instance occurs in the same position in the sort order as <paramref name="obj"/>. Greater than zero
        /// This instance follows <paramref name="obj"/> in the sort order.
        /// </returns>
        public int CompareTo(object obj)
        {
            if (!GetType().IsInstanceOfType(obj)) throw new ArgumentException(Messages.Infra_ObjectTypeNotCompatible);
            SemVer semObj = obj as SemVer;
            return CompareTo(semObj);
        }
        #endregion

        #region IComparable<SemVer>
        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following
        /// meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter. Zero
        /// This object is equal to <paramref name="other"/>. Greater than zero This object is greater than
        /// <paramref name="other"/>.
        /// </returns>
        public int CompareTo(SemVer other)
        {
            if (other == null) return 1;
            if (!GetType().IsInstanceOfType(other)) throw new ArgumentException(Messages.Infra_ObjectTypeNotCompatible);

            if (Major > other.Major) return 1;
            if (Major < other.Major) return -1;

            if (Minor > other.Minor) return 1;
            if (Minor < other.Minor) return -1;

            if (Patch > other.Patch) return 1;
            if (Patch < other.Patch) return -1;

            if (Build > other.Build) return 1;
            if (Build < other.Build) return -1;

            bool leftEmpty = string.IsNullOrEmpty(PreRelease);
            bool rightEmpty = string.IsNullOrEmpty(other.PreRelease);
            if (leftEmpty && rightEmpty) return 0;
            if (leftEmpty) return 1;
            if (rightEmpty) return -1;
            return string.CompareOrdinal(PreRelease, other.PreRelease);
        }

        /// <summary>
        /// Implements the Greater Than operator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(SemVer left, SemVer right)
        {
            if (left is null) return false;
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the Less Than operator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(SemVer left, SemVer right)
        {
            if (left is null) return right is object;
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the Greater Than or Equal To operator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(SemVer left, SemVer right)
        {
            if (left is null) return right is null;
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Implements the Less Than or Equal To operator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(SemVer left, SemVer right)
        {
            if (left is null) return true;
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the Equality operator.
        /// </summary>
        /// <param name="other">The other operand.</param>
        /// <returns><see langword="true"/> if the objects are equal; <see langword="false"/> otherwise.</returns>
        public bool Equals(SemVer other)
        {
            if (other is null) return false;
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="object"/> is equal to this instance; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            SemVer semObj = obj as SemVer;
            if (semObj == null) return false;

            return Equals(semObj);
        }

        private bool m_HashCodeGenerated;
        private int m_HashCode;

        /// <summary>
        /// Gets the hash code.
        /// </summary>
        /// <returns>The 32-bit hash code based on this instance.</returns>
        public override int GetHashCode()
        {
            if (!m_HashCodeGenerated) {
                string version = GetVersion(true);
                m_HashCode = version.GetHashCode();
                m_HashCodeGenerated = true;
            }
            return m_HashCode;
        }

        /// <summary>
        /// Implements the Equality operator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(SemVer left, SemVer right)
        {
            if (left is null) return right is null;
            return left.CompareTo(right) == 0;
        }

        /// <summary>
        /// Implements the Inequality operator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(SemVer left, SemVer right)
        {
            if (left is null) return right is object;
            return left.CompareTo(right) != 0;
        }
        #endregion

        private string m_Version;

        private string GetVersion(bool hashGen)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Major).Append('.')
                .Append(Minor).Append('.')
                .Append(Patch);
            if (HasBuild || hashGen) sb.Append('.').Append(Build);

            if (!string.IsNullOrEmpty(PreRelease)) {
                sb.Append('-').Append(PreRelease);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            if (m_Version == null) {
                m_Version = GetVersion(false);
            }
            return m_Version;
        }

        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="StreamingContext"/>) for this serialization.</param>
        /// <exception cref="ArgumentNullException">info</exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            info.AddValue("SemVer", ToString());
        }

        /// <summary>
        /// Performs a deep clone of the object.
        /// </summary>
        /// <returns>A copy of the original object.</returns>
        public SemVer DeepClone()
        {
            if (HasBuild) {
                return new SemVer(Major, Minor, Patch, Build, PreRelease);
            }
            return new SemVer(Major, Minor, Patch, PreRelease);
        }
    }
}
