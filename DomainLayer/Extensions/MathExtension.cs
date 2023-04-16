namespace DomainLayer.Extensions
{
    public static class MathExtension 
    {
        /// <summary>
        /// Calculate the greatest common divisor of two numbers.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Greatest common divisor</returns>
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
