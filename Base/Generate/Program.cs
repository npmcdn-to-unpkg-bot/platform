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
            //Development.Repository.Tasks.Generate.Execute("../../../Templates/meta.ts.stg", "../../../Website/Allors/Client/Generated/meta", Groups.Workspace);
            //Development.Repository.Tasks.Generate.Execute("../../../Templates/domain.ts.stg", "../../../Website/Allors/Client/Generated/domain", Groups.Workspace);

            Development.Repository.Tasks.Generate.Execute("../../../Templates/workspace.meta.cs.stg", "../../../Workspace.Meta/Generated", Groups.Workspace);

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
