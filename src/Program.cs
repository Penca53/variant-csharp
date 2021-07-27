using System;
using Penca53.Variant;

namespace Penca53
{
    public class Program
    {
        static void Main(string[] args)
        {
            Variant<double, string> variant = new Variant<double, string>();

            // Get the type of the current item
            Console.WriteLine(variant.Type);

            Console.WriteLine();

            // Set the double value to the variant
            variant = 1d;

            // Get the item using the method
            Console.WriteLine(variant.GetT1());
            // Get the item using the overridden cast operator
            Console.WriteLine((double)variant);

            // Get the index of the current item
            Console.WriteLine(variant.Type);

            Console.WriteLine();

            // Set the string value to the variant
            variant = "Hello World!";

            // Get the item using the method
            Console.WriteLine(variant.GetT2());
            // Get the item using the overridden cast operator
            Console.WriteLine((string)variant);

            // Get the index of the current item
            Console.WriteLine(variant.Type);

            Console.ReadKey();
        }
    }
}
