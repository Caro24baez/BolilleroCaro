using System;
using System.Security.Cryptography;

namespace BolilleroCaro
{
    public interface IRandomNumberGenerator
    {
        int Next(int minValue, int maxValue);
        int Next(int count);
    }
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random azar = new Random();
        /*
        public RandomNumberGenerator()
        {
            azar = new Random();
        }*/

        public int Next(int minValue, int maxValue)
        {
            return azar.Next(minValue, maxValue);
        }

        public int Next(int count)
        {
            return azar.Next(count);
        }
    }

}
