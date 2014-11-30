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
            //Development.Repository.Tasks.Generate.Execute("../../../../Core/Templates/domain.cs.stg", "../../../Domain/Generated/Core");
            Development.Repository.Tasks.Generate.Execute("../../../../Core/Templates/repository.stg", "../../../Repository/Generated");
            //Development.Repository.Tasks.Generate.Execute("../../../Templates/domain.cs.stg", "../../../Domain/Generated/Base");
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
