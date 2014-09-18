namespace Allors
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Generate();
        }

        public static void Generate()
        {
            var log = Development.Repository.Tasks.Generate.Execute("../../../Templates/allors.cs.stg", "../../../Domain/Generated/allors");
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
