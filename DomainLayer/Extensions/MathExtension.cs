
namespace DomainLayer.Extensions
{
    public static class MathExtension 
    {
        // Calculate the greatest common divisor of two numbers.
        public static int GetGreatestCommonDivisor(this int a, int b)
        {
            while (b != 0)
            {
                int remainder = a % b;
                a = b;
                b = remainder;
            }
            return a;
        }
    }
}
