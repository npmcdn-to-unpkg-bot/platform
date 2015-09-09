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
            Development.Repository.Tasks.Generate.Execute("../../../Templates/domain.ts.stg", "../../../Angular/Generated");
            
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
