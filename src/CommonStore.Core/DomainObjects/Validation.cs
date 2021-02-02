using System.Text.RegularExpressions;

namespace CommonStore.Core.DomainObjects
{
    //AssertConcern
    public class Validation
    {
        public static void ValidateEqual(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateNotEqual(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateDifferent(string pattern, string value, string message)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(value))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateSize(string value, int max, string message)
        {
            var length = value.Trim().Length;
            if (length > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateSize(string value, int min, int max, string message)
        {
            var length = value.Trim().Length;
            if (length < min || length > max)
            {
                throw new DomainException(message);
            }
        }
        
        public static void ValidateEmpty(string value, string message)
        {
            if (value == null || value.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateNull(object object1, string message)
        {
            if (object1 == null)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(double value, double min, double max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(float value, float min, float max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(int value, int min, int max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(long value, long min, long max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(decimal value, decimal min, decimal max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateGreaterThan(long value, long min, string message)
        {
            if (value < min)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLesserThan(double value, double min, string message)
        {
            if (value < min)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLesserThan(decimal value, decimal min, string message)
        {
            if (value < min)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLesserThan(int value, int min, string message)
        {
            if (value < min)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateFalse(bool boolvalue, string message)
        {
            if (!boolvalue)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateTrue(bool boolvalue, string message)
        {
            if (boolvalue)
            {
                throw new DomainException(message);
            }
        }
    }
}