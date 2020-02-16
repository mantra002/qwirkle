using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Qwirkle.Game
{
    static public class RandomHelper
    {
        static public void Shuffle<T>(this IList<T> list)
        {
            if (list != null)
            {
                RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
                int n = list.Count;
                while (n > 1)
                {
                    byte[] box = new byte[1];
                    do provider.GetBytes(box);
                    while (!(box[0] < n * (Byte.MaxValue / n)));
                    int k = (box[0] % n);
                    n--;
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
                provider.Dispose();
            }
        }
    }
}
