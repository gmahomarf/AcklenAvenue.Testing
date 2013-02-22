using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcklenAvenue.Testing.BDD.MSTest
{
    public static class SpecificationExtensions
    {
        public static void ShouldContain(this string shouldContain, string desiredText)
        {
            try
            {
                Assert.IsTrue(shouldContain.Contains(desiredText));
            }
            catch
            {
                Assert.Fail(string.Format("Expected to contain '{0}', but was not found in '{1}'.", desiredText,
                                          shouldContain));
            }
        }

        public static void ShouldNotContain(this string shouldContain, string desiredText)
        {
            try
            {
                Assert.IsFalse(shouldContain.Contains(desiredText));
            }
            catch
            {
                Assert.Fail(string.Format("Expected not to contain '{0}', but was found in '{1}'.", desiredText,
                                          shouldContain));
            }
        }

        public static void ShouldBeEmpty(this string shouldBeEmpty)
        {
            try
            {
                Assert.IsTrue(string.IsNullOrEmpty(shouldBeEmpty));
            }
            catch
            {
                Assert.Fail(string.Format("\nExpected empty but found '{0}'.", shouldBeEmpty));
            }
        }

        public static void ShouldBeFalse(this bool compartTo)
        {
            Assert.IsFalse(compartTo);
        }

        public static void ShouldBeTrue(this bool compartTo)
        {
            Assert.IsTrue(compartTo);
        }

        public static void ShouldBeGreaterThan(this int compareTo, int shouldBeGreaterThan)
        {
            try
            {
                Assert.IsTrue(compareTo > shouldBeGreaterThan);
            }
            catch (Exception)
            {
                Assert.Fail(string.Format("\nExpected '{0}' to be greater than '{1}', but it was not.",
                                          shouldBeGreaterThan, compareTo));
            }
        }

        public static void ShouldBeOfType<T>(this object compareObject)
        {
            Assert.IsInstanceOfType(compareObject, typeof (T));
        }

        public static void ShouldEqual(this string actual, string expected)
        {
            string[] expectedLines = expected.Split(new[] {'\n'});
            string[] actualLines = actual.Split(new[] {'\n'});
            int maxLines = expectedLines.Length > actualLines.Length ? expected.Length : actualLines.Length;

            for (int lineIndex = 0; lineIndex < maxLines; lineIndex++)
            {
                string expectedLine = expectedLines[lineIndex];
                string actualLine = actualLines[lineIndex];
                int maxChars = expectedLine.Length > actualLine.Length ? expectedLine.Length : actualLine.Length;

                for (int charIndex = 0; charIndex < maxChars; charIndex++)
                    if (expectedLine[charIndex] != actualLine[charIndex])
                    {
                        Assert.Fail(
                            string.Format(
                                "Expected this: \nLine {2}: \"...{0}...\"\n\nbut found this:\n\nLine {2}: \"...{1}...\".\n\n    Expected:\n{4}\n\nActual:\n{5})",
                                GetSection(expectedLine, charIndex),
                                GetSection(actualLine, charIndex),
                                lineIndex + 1,
                                charIndex + 1,
                                AddLineNumbers(expected),
                                AddLineNumbers(actual)));
                    }
            }
        }

        static string GetSection(string line, int charIndex)
        {
            int maxChars = line.Length;
            int startIndex = (charIndex - 20 < 0 ? 0 : charIndex - 20);
            int endIndex = (maxChars < 40 ? maxChars : 40);
            return line.Substring(startIndex, endIndex);
        }

        static string AddLineNumbers(string stringWithLines)
        {
            string[] lines = stringWithLines.Split(new[] {'\n'});
            var sb = new StringBuilder();
            for (int index = 0; index < lines.Length; index++)
            {
                sb.AppendLine(string.Format("{0:000}: {1}", index + 1, lines[index]));
            }
            return sb.ToString();
        }

        public static void ShouldEqual(this object compareObject, object shouldEqualObject)
        {
            try
            {
                Assert.AreEqual(shouldEqualObject, compareObject);
            }
            catch
            {
                Assert.Fail(string.Format("\nExpected '{0}' but found '{1}'.", shouldEqualObject, compareObject));
            }
        }

        public static void ShouldBeNull(this object obj)
        {
            try
            {
                Assert.IsNull(obj);
            }
            catch (Exception)
            {
                Assert.Fail(string.Format("\nExpected null but found not null."));
            }
        }

        public static void ShouldNotBeNull(this object obj)
        {
            try
            {
                Assert.IsNotNull(obj);
            }
            catch
            {
                Assert.Fail(string.Format("\nExpected not null but found null."));
            }
        }
    }
}