using Allors.Meta;

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
            Development.Repository.Tasks.Generate.Execute("../../../Templates/adapters.cs.stg", "../../../Domain/Generated", null);

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
