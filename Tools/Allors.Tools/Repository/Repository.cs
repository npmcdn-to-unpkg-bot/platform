﻿namespace Allors.Tools.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Allors.Repository.Domain;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class Repository : IRepository
    {
        public IEnumerable<IAssembly> Assemblies => this.AssemblyByName.Values;

        public IEnumerable<IInterface> Interfaces => this.InterfaceByName.Values;

        public Dictionary<string, Assembly> AssemblyByName { get; }

        public Dictionary<string, Interface> InterfaceByName { get; }

        public Dictionary<string, Class> ClassByName { get; }

        public Dictionary<string, Type> TypeByName { get; }

        public Repository(Project project)
        {
            this.AssemblyByName = new Dictionary<string, Assembly>();
            this.InterfaceByName = new Dictionary<string, Interface>();
            this.ClassByName = new Dictionary<string, Class>();
            this.TypeByName = new Dictionary<string, Type>();

            var projectInfo = new ProjectInfo(project);

            this.CreateAssemblies(projectInfo);
            this.CreateAssemblyExtensions(projectInfo);
            this.CreateTypes(projectInfo);
            this.CreateMembers(projectInfo);
            
            this.AddAttributes(projectInfo);
        }

        private void CreateAssemblies(ProjectInfo projectInfo)
        {
            foreach (var syntaxTree in projectInfo.DocumentBySyntaxTree.Keys)
            {
                var document = projectInfo.DocumentBySyntaxTree[syntaxTree];
                if (document.Folders.Count == 1)
                {
                    var attributeLists =
                        syntaxTree.GetRoot()
                            .DescendantNodes()
                            .OfType<AttributeListSyntax>()
                            .Where(x => "assembly".Equals(x.Target?.Identifier.Text))
                            .ToArray();

                    var assemblyName = document.Folders[0];
                    if (attributeLists.Length > 0 && !this.AssemblyByName.ContainsKey(assemblyName))
                    {
                        var semanticModel = projectInfo.SemanticModelBySyntaxTree[syntaxTree];
                        foreach (var attributeList in attributeLists)
                        {
                            if (attributeList.Attributes.Count > 0)
                            {
                                var attribute = attributeList.Attributes[0];
                                var attributeType = semanticModel.GetSymbolInfo(attribute).Symbol.ContainingType;

                                if (attributeType.Equals(projectInfo.ExtendAttributeType))
                                {
                                    this.AssemblyByName[assemblyName] = new Assembly(assemblyName);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CreateAssemblyExtensions(ProjectInfo projectInfo)
        {
            foreach (var syntaxTree in projectInfo.DocumentBySyntaxTree.Keys)
            {
                var document = projectInfo.DocumentBySyntaxTree[syntaxTree];
                if (document.Folders.Count == 1)
                {
                    var attributeLists =
                        syntaxTree.GetRoot()
                            .DescendantNodes()
                            .OfType<AttributeListSyntax>()
                            .Where(x => "assembly".Equals(x.Target?.Identifier.Text))
                            .ToArray();

                    var assemblyName = document.Folders[0];
                    if (attributeLists.Length > 0)
                    {
                        var semanticModel = projectInfo.SemanticModelBySyntaxTree[syntaxTree];
                        foreach (var attributeList in attributeLists)
                        {
                            if (attributeList.Attributes.Count > 0)
                            {
                                var attribute = attributeList.Attributes[0];
                                var symbol = semanticModel.GetSymbolInfo(attribute).Symbol;
                                var attributeType = symbol.ContainingType;

                                if (attributeType.Equals(projectInfo.ExtendAttributeType))
                                {
                                    var baseDomainName = attribute.ArgumentList.Arguments[0].Expression.GetFirstToken().Value.ToString();

                                    if (!string.IsNullOrWhiteSpace(baseDomainName))
                                    {
                                        var assembly = this.AssemblyByName[assemblyName];
                                        var baseAssembly = this.AssemblyByName[baseDomainName];

                                        assembly.Extend(baseAssembly);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CreateTypes(ProjectInfo projectInfo)
        {
            foreach (var syntaxTree in projectInfo.DocumentBySyntaxTree.Keys)
            {
                var document = projectInfo.DocumentBySyntaxTree[syntaxTree];
                if (document.Folders.Count == 1)
                {
                    var semanticModel = projectInfo.SemanticModelBySyntaxTree[syntaxTree];

                    var assemblyName = document.Folders[0];
                    if (this.AssemblyByName.ContainsKey(assemblyName))
                    {
                        var assembly = this.AssemblyByName[assemblyName];

                        var root = syntaxTree.GetRoot();
                        
                        var interfaceDeclaration = root.DescendantNodes().OfType<InterfaceDeclarationSyntax>().SingleOrDefault();
                        if (interfaceDeclaration != null)
                        {
                            var symbol = semanticModel.GetDeclaredSymbol(interfaceDeclaration);
                            var interfaceName = symbol.Name;

                            var partialInterface = new PartialInterface(interfaceName);
                            assembly.PartialInterfaceByName[interfaceName] = partialInterface;
                            assembly.PartialTypeByName[interfaceName] = partialInterface;

                            Interface @interface;
                            if (!this.InterfaceByName.TryGetValue(interfaceName, out @interface))
                            {
                                @interface = new Interface(interfaceName);
                                this.InterfaceByName[interfaceName] = @interface;
                                this.TypeByName[interfaceName] = @interface;
                            }

                            @interface.PartialByAssemblyName[assemblyName] = partialInterface;
                        }

                        var classDeclaration = root.DescendantNodes().OfType<ClassDeclarationSyntax>().SingleOrDefault();
                        if (classDeclaration != null)
                        {
                            var symbol = semanticModel.GetDeclaredSymbol(classDeclaration);
                            var className = symbol.Name;

                            var partialClass = new PartialClass(className);
                            assembly.PartialClassByName[className] = partialClass;
                            assembly.PartialTypeByName[className] = partialClass;
                            
                            Class @class;
                            if (!this.ClassByName.TryGetValue(className, out @class))
                            {
                                @class = new Class(className);
                                this.ClassByName[className] = @class;
                                this.TypeByName[className] = @class;
                            }

                            @class.PartialByAssemblyName[assemblyName] = partialClass;
                        }
                    }
                }
            }
        }

        private void CreateMembers(ProjectInfo projectInfo)
        {
            foreach (var syntaxTree in projectInfo.DocumentBySyntaxTree.Keys)
            {
                var document = projectInfo.DocumentBySyntaxTree[syntaxTree];
                if (document.Folders.Count == 1)
                {
                    var semanticModel = projectInfo.SemanticModelBySyntaxTree[syntaxTree];

                    var assemblyName = document.Folders[0];
                    if (this.AssemblyByName.ContainsKey(assemblyName))
                    {
                        var assembly = this.AssemblyByName[assemblyName];

                        var root = syntaxTree.GetRoot();

                        var typeDeclaration = root.DescendantNodes().SingleOrDefault(v => v is InterfaceDeclarationSyntax || v is ClassDeclarationSyntax);
                        if (typeDeclaration != null)
                        {
                            var typeSymbol = semanticModel.GetDeclaredSymbol(typeDeclaration);
                            var typeName = typeSymbol.Name;
                            var type = assembly.PartialTypeByName[typeName];

                            var propertyDeclarations = typeDeclaration.DescendantNodes().OfType<PropertyDeclarationSyntax>();
                            foreach (var propertyDeclaration in propertyDeclarations)
                            {
                                var propertySymbol = semanticModel.GetDeclaredSymbol(propertyDeclaration);
                                var propertyName = propertySymbol.Name;
                                
                                var property = new Property(propertyName);
                                type.PropertyByName[propertyName] = property;
                            }

                            var methodDeclarations = typeDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>();
                            foreach (var methodDeclaration in methodDeclarations)
                            {
                                var methodSymbol = semanticModel.GetDeclaredSymbol(methodDeclaration);
                                var methodName = methodSymbol.Name;

                                var method = new Method(methodName);
                                type.MethodByName[methodName] = method;
                            }
                        }
                    }
                }
            }
        }

        private void AddAttributes(ProjectInfo projectInfo)
        {
            var definedTypeByName = projectInfo.Assembly.DefinedTypes.Where(v=>"Allors.Repository.Domain".Equals(v.Namespace)).ToDictionary(v => v.Name);

            foreach (var type in this.TypeByName.Values)
            {
                var definedType = definedTypeByName[type.Name];
                var attributesByTypeName = definedType.GetCustomAttributes(false).Cast<System.Attribute>().GroupBy(v => v.GetType().Name);

                foreach (var group in attributesByTypeName)
                {
                    var typeName = group.Key;
                    if (typeName.ToLowerInvariant().EndsWith("attribute"))
                    {
                        typeName = typeName.Substring(0, typeName.Length - "attribute".Length);
                    }

                    type.AttributeByName[typeName] = group.First();
                }
            }
        }
    }
}