using System;
using System.Collections.Generic;

class Program
{
    static long Calc(long n, long prev)
    {
        long ret = 0, s = 1;
        while (true)
        {
            if (prev + s - 1 <= n)
            {
                ret += s;
            }
            else if (prev <= n)
            {
                ret += n - prev + 1;
            }
            else
            {
                break;
            }
            prev *= 10;
            s *= 10;
        }
        return ret;
    }

    static long FindPos(long n, long k)
    {
        var a = new List<long>();

        long res = 0, tmp = k, cur = 0;

        a.Add(0);
        while (tmp > 0)
        {
            a.Add(tmp % 10);
            tmp /= 10;
        }
        for (int i = a.Count - 1; i >= 0; i--)
        {
            for (int j = a[i] - 1; j >= ((i == a.Count - 1) ? 1 : 0); j--)
            {
                res += Calc(n, cur * 10 + j);
            }
            cur = cur * 10 + a[i];
        }

        return res + a.Count - 1;
    }

    static void Main()
    {
        long k, m;
        if (long.TryParse(Console.ReadLine(), out k) && long.TryParse(Console.ReadLine(), out m))
        {
            long mi, lo = k, hi = (1L << 63) - 1;
            while (lo < hi)
            {
                mi = (lo + hi) >> 1;
                if (FindPos(mi, k) >= m)
                {
                    hi = mi;
                }
                else
                {
                    lo = mi + 1;
                }
            }
            if (FindPos(lo, k) != m)
            {
                Console.WriteLine("0");
            }
            else
            {
                Console.WriteLine(lo);
            }
        }
    }
}
