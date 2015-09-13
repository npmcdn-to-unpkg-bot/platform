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
            //Development.Repository.Tasks.Generate.Execute("../../../Templates/meta.ts.stg", "../../../Website/Allors/Client/Generated/meta");
            Development.Repository.Tasks.Generate.Execute("../../../Templates/domain.ts.stg", "../../../Website/Allors/Client/Generated/domain");
            
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
