namespace Allors.Tools.Cmd
{
    using System;
    using System.IO;

    using Allors.Tools.Repository.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    Console.WriteLine("Error: missing required arguments");
                }

                var context = args[0];

                switch (context.ToLowerInvariant())
                {
                    case "repository":
                        Repository(args);
                        break;

                    default:
                        Console.WriteLine("Error: unknown context");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Finished");
        }

        private static void Repository(string[] args)
        {
            var command = args[1];

            switch (command.ToLowerInvariant())
            {
                case "generate":
                    RepositoryGenerate(args);
                    break;

                default:
                    Console.WriteLine("Error: unknown context");
                    break;
            }
        }

        private static void RepositoryGenerate(string[] args)
        {
            var solutionPath = args[2];
            var projectName = args[3];
            var template = args[4];
            var output = args[5];

            var fileInfo = new FileInfo(solutionPath);

            try
            {
                var log = Generate.Execute(fileInfo.FullName, projectName, template, output);
            }
            catch (Exception e)
            {
                var exception = e.InnerException ?? e;
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
            }
        }
    }
}
