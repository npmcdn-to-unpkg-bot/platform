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
            var log = Allors.Development.Repository.Tasks.Generate.Execute("../../../Templates/Core/domain.cs.stg", "../../../Domain/Adapters/Output");
            Console.WriteLine(log);
            Console.ReadKey();
        }
    }
}
