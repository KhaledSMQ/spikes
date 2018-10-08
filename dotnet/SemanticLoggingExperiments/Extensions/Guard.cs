using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SemanticLoggingExperiments.Extensions
{
    internal static class Guard
    {
        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();

        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }

            if (argumentValue.Length == 0)
            {
                throw new ArgumentException("Argument is empty", argumentName);
            }
        }

        public static void ArgumentGreaterOrEqualThan<T>(T lowerValue, T argumentValue, string argumentName) where T : struct, IComparable
        {
            if (argumentValue.CompareTo((T)lowerValue) < 0)
            {
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, string.Format(CultureInfo.CurrentCulture, "The size of '{0}' should be greater or equal to '{1}'.", argumentName, lowerValue));
            }
        }

        public static void ArgumentLowerOrEqualThan<T>(T higherValue, T argumentValue, string argumentName) where T : struct, IComparable
        {
            if (argumentValue.CompareTo((T)higherValue) > 0)
            {
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, string.Format(CultureInfo.CurrentCulture, "The size of '{0}' should be lower or equal to '{1}'.", argumentName, higherValue));
            }
        }

        public static void ArgumentIsValidTimeout(TimeSpan? argumentValue, string argumentName)
        {
            if (argumentValue.HasValue)
            {
                long totalMilliseconds = (long)argumentValue.Value.TotalMilliseconds;
                if (totalMilliseconds < (long)-1 || totalMilliseconds > (long)2147483647)
                {
                    throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, "The valid range for '{0}' is from 0 to 24.20:31:23.647", argumentName));
                }
            }
        }

        public static void ValidateTimestampPattern(string timestampPattern, string argumentName)
        {
            Guard.ArgumentNotNullOrEmpty(timestampPattern, argumentName);

            foreach (var item in timestampPattern.ToCharArray())
            {
                if (InvalidFileNameChars.Contains(item))
                {
                    throw new ArgumentException("Timestamp contains invalid characters", argumentName);
                }
            }
        }

        public static void ValidDateTimeFormat(string format, string argumentName)
        {
            if (format == null)
            {
                return;
            }

            try
            {
                DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                throw new ArgumentException(argumentName, "The date time format is invalid.", e);
            }
        }
    }
}
