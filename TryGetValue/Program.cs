using System;
using System.Collections.Generic;
using System.Diagnostics;

// https://www.dotnetperls.com/trygetvalue
// TryGetValue is better performance than ContainsKey, indexer !
class Program
{

    const int _max = 10000000;
    static void Main()
    {
        var ids = new Dictionary<string, bool>() { { "X1", true } };

        
        // We can specify the "out" type by argument.
        if (ids.TryGetValue("X1", out bool result))
        {
            Console.WriteLine($"got X1, and here's its value: {result}");
        }   // OUTPUT: got X1, and here's its value: True



        // eg 2:
        var test = new Dictionary<string, int>();
        test["key"] = 1;
        int sum = 0;

        // use TryGetValue and use the key already accessed.
        var s1 = Stopwatch.StartNew();
        for (int i = 0; i < _max; i++)
        {
            if (test.TryGetValue("key", out int result1))
            {
                sum += result1;
            }
        }
        s1.Stop();
        Console.WriteLine(sum.ToString());
        Console.WriteLine(((double)(s1.Elapsed.TotalMilliseconds * 1000000) / _max).ToString("0.00 ns"));
    }
}