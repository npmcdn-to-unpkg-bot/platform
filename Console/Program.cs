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
            var log = Development.Repository.Tasks.Generate.Execute("../../../Templates/Core/domain.cs.stg", "../../../Domain/Adapters/Generated");
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
