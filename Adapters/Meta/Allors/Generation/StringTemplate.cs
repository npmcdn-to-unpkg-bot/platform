// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringTemplate.cs" company="Allors bvba">
//   Copyright 2002-2009 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>
//   Defines the Default type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Development.Repository.Generation
{
    using System;
    using System.IO;
    using System.Xml;

    using Allors.Meta;

    using Antlr4.StringTemplate;
    using Antlr4.StringTemplate.Misc;

    using Storage;

    public class StringTemplate
    {
        private const string TemplateId = "TemplateId";
        private const string TemplateName = "TemplateName";
        private const string TemplateVersion = "TemplateVersion";
        private const string TemplateConfiguration = "TemplateConfiguration";

        private const string TemplateKey = "template";
        private const string MetaKey = "meta";
        private const string InputKey = "input";
        private const string OutputKey = "output";
        private const string GenerationKey = "generation";
        private const string InheritanceKey = "inheritance";
        private const string ObjectTypeKey = "objectType";
        private const string RelationTypeKey = "relationType";
        
        private readonly FileInfo fileInfo;
        private readonly Guid id;
        private readonly string name;
        private readonly string version;

        internal StringTemplate(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;

            this.fileInfo.Refresh();
            if (!this.fileInfo.Exists)
            {
                throw new Exception("Template file not found: " + fileInfo.FullName);
            }
            
            TemplateGroup group = new TemplateGroupFile(this.fileInfo.FullName, '$', '$');

            this.id = Render(group, TemplateId) != null ? new Guid(Render(group, TemplateId)) : Guid.Empty;
            this.name = Render(group, TemplateName);
            this.version = Render(group, TemplateVersion);

            if (this.id == Guid.Empty)
            {
                throw new Exception("Template has no id");
            }
        }

        public Guid Id
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public string Version
        {
            get { return this.version; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        internal void Generate(MetaPopulation metaPopulation, DirectoryInfo outputDirectory, Log log)
        {
            if (!metaPopulation.IsValid)
            {
                log.Error(this, "Meta population " + metaPopulation + " has validation errors.");
                return;
            }

            try
            {
                TemplateGroup group = new TemplateGroupFile(this.fileInfo.FullName, '$', '$');

                group.ErrorManager = new ErrorManager(new LogAdapter(log));
                
                var configurationTemplate = group.GetInstanceOf(TemplateConfiguration);
                configurationTemplate.Add(MetaKey, metaPopulation);

                var configurationXml = new XmlDocument();
                configurationXml.LoadXml(configurationTemplate.Render());

                var location = new Location(outputDirectory);
                foreach (XmlElement generation in configurationXml.DocumentElement.SelectNodes(GenerationKey))
                {
                    var templateName = generation.GetAttribute(TemplateKey);
                    var template = group.GetInstanceOf(templateName);
                    var output = generation.GetAttribute(OutputKey);

                    if (template == null)
                    {
                        System.Console.WriteLine(0);
                    }

                    template.Add(MetaKey, metaPopulation);
                    if (generation.HasAttribute(InputKey))
                    {
                        var input = new Guid(generation.GetAttribute(InputKey));
                        var objectType = metaPopulation.Find(input) as ObjectType;
                        if (objectType != null)
                        {
                            template.Add(ObjectTypeKey, objectType);
                        }
                        else
                        {
                            var relationType = metaPopulation.Find(input) as RelationType;
                            if (relationType != null)
                            {
                                template.Add(RelationTypeKey, relationType);
                            }
                            else
                            {
                                var inheritance = metaPopulation.Find(input) as Inheritance;
                                if (inheritance != null)
                                {
                                    template.Add(InheritanceKey, inheritance);
                                }
                                else
                                {
                                    throw new ArgumentException(input + " was not found");
                                }
                            }
                        }
                        //TODO: Super Domains
                    }

                    var result = template.Render();
                    location.Save(output, result);
                }
            }
            catch (Exception e)
            {
                log.Error(this, "Generation error : " + e.Message + "\n" + e.StackTrace);
            }
        }

        private static string Render(TemplateGroup templateGroup, string templateName)
        {
            var template = templateGroup.GetInstanceOf(templateName);
            if (template != null)
            {
                return template.Render();
            }

            return null;
        }

        private class LogAdapter : ITemplateErrorListener
        {
            private readonly Log log;

            public LogAdapter(Log log)
            {
                this.log = log;
            }

            public void CompiletimeError(TemplateMessage msg)
            {
                this.log.Error(msg, msg.ToString());
            }

            public void RuntimeError(TemplateMessage msg)
            {
                this.log.Error(msg, msg.ToString());
            }

            public void IOError(TemplateMessage msg)
            {
                this.log.Error(msg, msg.ToString());
            }

            public void InternalError(TemplateMessage msg)
            {
                this.log.Error(msg, msg.ToString());
            }
        }
    }
}