using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CodeNames.Services
{
    public static class GenericService
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    byte[] randomNumber = new byte[4];
                    rng.GetBytes(randomNumber);
                    int k = Math.Abs(BitConverter.ToInt32(randomNumber, 0) % (n + 1));
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        }

        /*public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }*/
    }
}
