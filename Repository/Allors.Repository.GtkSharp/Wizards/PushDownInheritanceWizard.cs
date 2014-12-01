﻿//------------------------------------------------------------------------------------------------- 
// <copyright file="PushDownInheritanceWizard.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
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
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta.GtkSharp.Wizards
{
    using System;

    using Allors.Meta.Commands;
    using Allors.Meta;

    using Gtk;

    public class PushDownInheritanceWizard : Dialog
    {
        private readonly Entry namespaceEntry;
        private readonly DependencyTextView dependencyTextView;

        private readonly XmlRepository repository;
        private readonly Inheritance inheritance;

        private PushDownInheritance pushdown;

        public PushDownInheritanceWizard(XmlRepository repository, Inheritance inheritance)
        {
            this.repository = repository;
            this.inheritance = inheritance;
            
            this.Title = Mono.Unix.Catalog.GetString("Push Inheritance Down Wizard");
            this.Icon = Gdk.Pixbuf.LoadFromResource("Allors.R1.Development.GtkSharp.Icons.allors.ico");
            this.DefaultWidth = 640;
            this.DefaultHeight = 400;
            
            var headerBox = new VBox
                                 {
                                     Spacing = 10, 
                                     BorderWidth = 10
                                 };
            this.VBox.PackStart(headerBox, false, false, 0);

            headerBox.PackStart(new HtmlLabel("<span size=\"large\">Welcome to the Allors Push Inheritance Down Wizard</span>", 0.5f));
            headerBox.PackStart(new HtmlLabel("This wizard allows you to push a namespace down from a super domain.", 0.5f));
           
            var form = new Form();
            this.VBox.PackStart(form);

            var namespaceNameLabel = new HtmlLabel("Inheritance");
            this.namespaceEntry = new Entry { Sensitive = false, Text = this.inheritance.ToString() };

            var dependencyLabel = new HtmlLabel("Dependencies");
            this.dependencyTextView = new DependencyTextView();
            var scrolledDepenceyTextView = new ScrolledWindow { this.dependencyTextView };

            var buttonCancel = new Button
                                    {
                                        CanDefault = true,
                                        UseStock = true,
                                        UseUnderline = true,
                                        Label = "gtk-cancel"
                                    };
            this.AddActionWidget(buttonCancel, -6);
            
            var buttonOk = new Button
                                {
                                    CanDefault = true,
                                    Name = "buttonOk",
                                    UseStock = true,
                                    UseUnderline = true,
                                    Label = "gtk-ok"
                                };
            buttonOk.Clicked += this.OnButtonOkClicked;
            this.ActionArea.PackStart(buttonOk);

            // Layout
            form.Attach(namespaceNameLabel, 0, 1, 0, 1, AttachOptions.Fill, AttachOptions.Shrink, 0, 0);
            form.Attach(this.namespaceEntry, 0, 1, 1, 2, AttachOptions.Fill | AttachOptions.Expand, AttachOptions.Shrink, 0, 0);

            form.Attach(dependencyLabel, 0, 1, 2, 3, AttachOptions.Fill, AttachOptions.Shrink, 0, 0);
            form.Attach(scrolledDepenceyTextView, 0, 1, 3, 4, AttachOptions.Fill | AttachOptions.Expand, AttachOptions.Fill | AttachOptions.Expand, 0, 0);

            this.ShowAll();

            this.pushdown = this.repository.PushDown(this.inheritance);
            this.dependencyTextView.Update(this.pushdown);
        }

        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            var domain = this.repository.Domain;
            var validationReport = domain.Validate();

            if (!validationReport.ContainsErrors)
            {
                this.pushdown.Execute();
                this.Respond(ResponseType.Ok);
            }
            else
            {
                var message = string.Join("\n", validationReport.Messages);
                var md = new MessageDialog(
                    this,
                    DialogFlags.DestroyWithParent,
                    MessageType.Error,
                    ButtonsType.Close,
                    message);
                md.Run();
                md.Destroy();
            }

            this.Resize(this.DefaultWidth, this.DefaultHeight);
        }
    }
}