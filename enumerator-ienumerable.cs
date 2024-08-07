// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

using System;
using System.Collections;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        var list = new List<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3); 
        list.Add(4);

        for (int i = 0; i < 2; i++)
        {
            Console.WriteLine(list[i]);
        }
       Console.WriteLine("---------------------------");

        IterateNext(list); // enumarable

        Console.WriteLine("---------------------------");

        var enumerator = list.GetEnumerator();

        for (int i = 0; i < 2; i++)
        {
            if (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }
        IterateNext(enumerator); 

    }

    public static void IterateNext(IEnumerator<int> enumerator)
    {
         Console.WriteLine("--Enumerator--");
        while (enumerator.MoveNext())
        {
            Console.WriteLine(enumerator.Current);
        }
    }

    public static void IterateNext(IEnumerable<int> enumerable)
    {
        Console.WriteLine("--IEnumerable--");
        foreach (var item in enumerable)
        {
            
            Console.WriteLine(item);
        }
                

    }
}