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
            Development.Repository.Tasks.Generate.Execute("../../../Templates/base.cs.stg", "../../../Domain/Generated/Base");
            
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
