namespace Allors
{
    using System;

    using Domain;

    class Program
    {
        static void Main(string[] args)
        {
            var domain = M.D;
            var log = domain.Validate();
            Console.WriteLine(string.Join("\n", log.Messages));

            Console.ReadKey();
        }
    }
}
