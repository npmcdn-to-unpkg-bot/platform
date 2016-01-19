using Allors.Meta;

namespace Allors
{
    namespace Console
    {
        using System;
        using System.Collections.Generic;
        using System.Configuration;
        using System.Globalization;
        using System.IO;
        using System.Linq;
        using System.Runtime.CompilerServices;
        using System.Xml;

        using Allors;
        using Allors.Domain;

        public class Program
        {
            private const string PopulationFileName = "population.xml";
            private const string PasswordsFileName = "passwords.xml";

            private enum Options
            {
                /// <summary>
                /// Saves the current population to population.xml
                /// </summary>
                Save,

                /// <summary>
                /// Loads a the population from population.xml
                /// </summary>
                Load,

                /// <summary>
                /// Upgrades the current population
                /// </summary>
                Upgrade,

                /// <summary>
                /// Import data
                /// </summary>
                Import,

                /// <summary>
                /// Custom
                /// </summary>
                Custom,

                /// <summary>
                /// Creates a new population
                /// </summary>
                Populate,

                /// <summary>
                /// Exist the application
                /// </summary>
                Exit,
            }

            public static void Main(string[] args)
            {
                var configuration = new Allors.Adapters.Object.SqlClient.Configuration
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                    ObjectFactory = Config.ObjectFactory,
                    IsolationLevel = System.Data.IsolationLevel.RepeatableRead,
                    CommandTimeout = 300
                };
                Config.Default = new Adapters.Object.SqlClient.Database(configuration);

                Console.WriteLine("Please select an option:\n");
                foreach (var option in Enum.GetValues(typeof(Options)))
                {
                    Console.WriteLine((int)option + ". " + Enum.GetName(typeof(Options), option));
                }

                Console.WriteLine();

                try
                {
                    var key = Console.ReadKey(true).KeyChar.ToString(CultureInfo.InvariantCulture);
                    Options option;
                    if (Enum.TryParse(key, out option))
                    {
                        Console.WriteLine("-> " + (int)option + ". " + Enum.GetName(typeof(Options), option));
                        Console.WriteLine();

                        switch (option)
                        {
                            case Options.Save:
                                Save();
                                break;

                            case Options.Load:
                                Load();
                                break;

                            case Options.Upgrade:
                                Upgrade();
                                break;

                            case Options.Custom:
                                Custom();
                                break;

                            case Options.Populate:
                                Populate();
                                break;

                            case Options.Exit:
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown option");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                }
                finally
                {
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey(false);
                }
            }

            private static void Save()
            {
                using (var writer = new XmlTextWriter(PopulationFileName, System.Text.Encoding.UTF8))
                {
                    Config.Default.Save(writer);
                }

                Console.WriteLine("Saved");
            }

            private static void Load()
            {
                using (var reader = new XmlTextReader(PopulationFileName))
                {
                    Config.Default.Load(reader);
                }

                Console.WriteLine("Loaded");
            }

            private static void Upgrade()
            {
                var database = Config.Default;

                var excludedRelationTypes = new HashSet<Guid> { new Guid("f6338fcf-0aec-4658-84ac-d45d38bf4098") };

                var objectTypesNotLoaded = new HashSet<Guid>();
                var relationTypesNotLoaded = new HashSet<Guid>();

                try
                {
                    Console.WriteLine("Upgrade started: " + DateTime.Now.ToLongTimeString());

                    using (var reader = new XmlTextReader(PopulationFileName))
                    {
                        database.ObjectNotLoaded += delegate (object sender, ObjectNotLoadedEventArgs args)
                        {
                            if (!objectTypesNotLoaded.Contains(args.ObjectTypeId))
                            {
                                objectTypesNotLoaded.Add(args.ObjectTypeId);
                                Console.WriteLine("ObjectType not loaded: " + args.ObjectTypeId);
                            }
                        };

                        database.RelationNotLoaded += delegate (object sender, RelationNotLoadedEventArgs args)
                        {
                            if (!relationTypesNotLoaded.Contains(args.RelationTypeId))
                            {
                                if (!excludedRelationTypes.Contains(args.RelationTypeId))
                                {
                                    throw new Exception("RelationType " + args.RelationTypeId + " can not be loaded but should be.");
                                }

                                Console.WriteLine("RelationType not loaded: " + args.RelationTypeId);
                                relationTypesNotLoaded.Add(args.RelationTypeId);
                            }
                        };

                        try
                        {
                            database.Load(reader);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }

                    using (var session = database.CreateSession())
                    {
                        //new Upgrade(session).Execute();
                    }

                    Console.WriteLine("Upgrade finished: " + DateTime.Now.ToLongTimeString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Upgrade failed: " + DateTime.Now.ToLongTimeString());
                    Console.WriteLine(e);

                    Console.WriteLine();
                    Console.WriteLine("Please correct errors or restore backup");
                }
            }

            private static void Populate()
            {
                Console.WriteLine("Are you sure, all current data will be destroyed? (Y/N)\n");
                var confirmationKey = Console.ReadKey(true).KeyChar.ToString(CultureInfo.InvariantCulture);
                if (confirmationKey.ToLower().Equals("y"))
                {
                    var database = Config.Default;
                    database.Init();

                    using (var session = database.CreateSession())
                    {
                        new Setup(session, DataPath).Apply();

                        var validation = session.Derive();
                        if (validation.HasErrors)
                        {
                            foreach (var error in validation.Errors)
                            {
                                Console.WriteLine(error.ToString());
                            }

                            Console.WriteLine("Rollback");
                        }
                        else
                        {
                            Console.WriteLine("Commit");
                            session.Commit();
                        }
                    }
                }
            }

            private static DirectoryInfo DataPath
            {
                get
                {
                    var dataPathString = ConfigurationManager.AppSettings["dataPath"];
                    return dataPathString != null ? new DirectoryInfo(dataPathString) : null;
                }
            }

            private static void Custom()
            {
                var database = Config.Default;

                using (var session = database.CreateSession())
                {
                    //var derivation = new Derivation(session, persons);
                    //var validation = derivation.Derive();

                    //if (validation.HasErrors)
                    //{
                    //    foreach (var error in validation.Errors)
                    //    {
                    //        Console.WriteLine(error.Message);
                    //    }

                    //    session.Rollback();
                    //    Console.WriteLine("Rollback");
                    //}
                    //else
                    //{
                    //    session.Commit();
                    //    Console.WriteLine("Commit");
                    //}
                }
            }
        }
    }
}
