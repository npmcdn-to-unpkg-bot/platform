// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringTemplate.cs" company="Allors bvba">
//   Copyright 2002-2010 Allors bvba.
//
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
//
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
//
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using Antlr4.StringTemplate;
    using Antlr4.StringTemplate.Misc;

    public partial class StringTemplate
    {
        private TemplateGroup TemplateGroup
        {
            get
            {
                // TODO: Setting or removing Body should flush caches
                var templateGroup = (TemplateGroup)this.Strategy.Session[this.Id.Key];

                if (templateGroup == null)
                {
                    templateGroup = new TemplateGroupString("allors", this.Body, '$', '$') { Listener = new ErrorTemplateListener() };
                    templateGroup.RegisterRenderer(typeof(string), new StringRenderer());
                    this.Strategy.Session[this.Id.Key] = templateGroup;
                }

                return templateGroup;
            }

            set
            {
                this.Strategy.Session[this.Id.Key] = value;
            }
        }

        public void Load(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException("Assembly " + assembly + " does not contain resource " + resourceName);
                }

                var reader = new StreamReader(stream);
                this.Body = reader.ReadToEnd();
            }
        }

        public string Apply(Dictionary<string, object> objects)
        {
            var template = this.GetTemplate();
            foreach (var dictionaryItem in objects)
            {
                template.Add(dictionaryItem.Key, dictionaryItem.Value);
            }

            var listener = (ErrorTemplateListener)template.Group.Listener;
            try
            {
                listener.Reset();
                if (this.ExistLocale && this.Locale.ExistCultureInfo)
                {
                    return template.Render(this.Locale.CultureInfo);
                }

                return template.Render();
            }
            finally
            {
                listener.Assert();
            }
        }

        public void BaseDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertIsUnique(this, StringTemplates.Meta.UniqueId);
        }

        private Template GetTemplate()
        {
            var templateGroup = this.TemplateGroup;
            var listener = (ErrorTemplateListener)templateGroup.Listener;
            try
            {
                listener.Reset();
                return templateGroup.GetInstanceOf("main");
            }
            finally
            {
                listener.Assert();
            }
        }

        private class ErrorTemplateListener : ITemplateErrorListener
        {
            private IList<TemplateMessage> templateMessages;

            internal ErrorTemplateListener()
            {
                this.templateMessages = new List<TemplateMessage>();
            }

            public void CompiletimeError(TemplateMessage msg)
            {
                this.templateMessages.Add(msg);
            }

            public void RuntimeError(TemplateMessage msg)
            {
                this.templateMessages.Add(msg);
            }

            public void IOError(TemplateMessage msg)
            {
                this.templateMessages.Add(msg);
            }

            public void InternalError(TemplateMessage msg)
            {
                this.templateMessages.Add(msg);
            }

            internal void Reset()
            {
                this.templateMessages = new List<TemplateMessage>();
            }

            internal void Assert()
            {
                if (this.templateMessages.Count > 0)
                {
                    var message = "StringTemplate Errors\n";
                    foreach (var templateMessage in this.templateMessages)
                    {
                        message += templateMessage.ToString();
                    }

                    throw new Exception(message);
                }
            }
        }
    }
}