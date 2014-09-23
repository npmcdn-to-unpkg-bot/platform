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
            Development.Repository.Tasks.Generate.Execute("../../../../Core/Templates/Core/domain.cs.stg", "../../../Domain/Generated/Core/Domain");
            Development.Repository.Tasks.Generate.Execute("../../../../Base/Templates/Base/domain.cs.stg", "../../../Domain/Generated/Base/Domain");
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
