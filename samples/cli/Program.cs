using KSUID;
using System;
using Base62;

namespace ksuid
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Ksuid.Generate());
        }
    }
}