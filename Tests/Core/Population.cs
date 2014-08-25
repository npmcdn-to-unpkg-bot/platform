// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Population.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Meta.Static
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using AllorsGenerated;

    public class Population
    {
        private readonly AllorsEmbeddedSession allorsSession;

        public Population()
        {
            this.Domain = MetaDomain.Create();
            this.Domain.Name = "Domain";

            this.allorsSession = this.Domain.AllorsSession;

            var validationReport = this.Domain.Validate();
            if (validationReport.ContainsErrors)
            {
                throw new Exception("Domain invalid");
            }

            this.GetUnits();
        }

        public MetaObject BinaryType { get; set; }

        public MetaObject BooleanType { get; set; }

        public MetaObject C1 { get; set; }

        public MetaObject C2 { get; set; }

        public MetaObject C3 { get; set; }

        public MetaObject C4 { get; set; }

        public MetaObject[] Classes { get; set; }

        public MetaObject[] CompositeClasses { get; set; }

        public MetaObject[] CompositeConcreteClasses { get; set; }

        public MetaObject[] CompositeInterfaces { get; set; }

        public MetaObject[] CompositeTypes
        {
            get
            {
                var compositeTypes = new List<MetaObject>();
                foreach (MetaObject type in this.Types)
                {
                    if (type.IsComposite)
                    {
                        compositeTypes.Add(type);
                    }
                }

                return compositeTypes.ToArray();
            }
        }

        public MetaObject[] Composites { get; set; }

        public MetaObject DateTimeType { get; set; }

        public MetaObject DecimalType { get; set; }

        public MetaObject DoubleType { get; set; }

        public MetaObject I1 { get; set; }

        public MetaObject I12 { get; set; }

        public MetaObject I2 { get; set; }

        public MetaObject I3 { get; set; }

        public MetaObject I34 { get; set; }

        public MetaObject I4 { get; set; }

        public MetaObject IntegerType { get; set; }

        public MetaObject[] Interfaces { get; set; }

        public MetaObject LongType { get; set; }

        public MetaObject StringType { get; set; }

        public MetaObject UniqueType { get; set; }

        public MetaObject[] UnitTypes
        {
            get
            {
                var unitTypes = new List<MetaObject>();
                foreach (MetaObject type in this.Types)
                {
                    if (type.IsUnit)
                    {
                        unitTypes.Add(type);
                    }
                }

                return unitTypes.ToArray();
            }
        }

        internal MetaDomain Domain { get; set; }

        internal AllorsEmbeddedObject[] Inheritances
        {
            get
            {
                return this.allorsSession.Extent(AllorsEmbeddedDomain.Inheritance);
            }
        }

        internal AllorsEmbeddedObject[] Relations
        {
            get
            {
                return this.allorsSession.Extent(AllorsEmbeddedDomain.RelationType);
            }
        }

        internal Hashtable RelationsById
        {
            get
            {
                var results = new Hashtable();
                foreach (MetaRelation relation in this.Relations)
                {
                    results.Add(relation.Id, relation);
                }

                return results;
            }
        }

        internal AllorsEmbeddedObject[] Associations
        {
            get
            {
                return this.allorsSession.Extent(AllorsEmbeddedDomain.AssociationType);
            }
        }

        internal AllorsEmbeddedObject[] Roles
        {
            get
            {
                return this.allorsSession.Extent(AllorsEmbeddedDomain.RoleType);
            }
        }

        internal MetaDomain SuperDomain { get; set; }

        internal AllorsEmbeddedObject[] Types
        {
            get
            {
                return this.allorsSession.Extent(AllorsEmbeddedDomain.ObjectType);
            }
        }

        internal Hashtable TypesById
        {
            get
            {
                var results = new Hashtable();
                foreach (MetaObject allorsType in this.Types)
                {
                    results.Add(allorsType.Id, allorsType);
                }

                return results;
            }
        }

        public static MetaObject CreateClass(MetaDomain domain, string name)
        {
            var type = CreateType(domain, name);
            return type;
        }

        public static MetaObject CreateInterface(MetaDomain domain, string name)
        {
            var type = CreateType(domain, name);
            type.IsInterface = true;
            return type;
        }

        public static MetaObject CreateMultiple(MetaDomain domain, string name)
        {
            var type = CreateType(domain, name);
            type.IsInterface = true;
            return type;
        }

        public static MetaObject CreateType(MetaDomain domain, string name)
        {
            var type = domain.AddDeclaredObjectType(Guid.NewGuid());
            type.SingularName = name;
            type.PluralName = name + "s";
            return type;
        }
        
        internal void Populate()
        {
            this.PopulateSuperDomain();

            // interfaces
            this.I12 = CreateInterface(this.Domain, "i12");
            this.I34 = CreateInterface(this.Domain, "i34");
            this.I1 = CreateInterface(this.Domain, "i1");
            this.I2 = CreateInterface(this.Domain, "i2");
            this.I3 = CreateInterface(this.Domain, "i3");
            this.I4 = CreateInterface(this.Domain, "i4");

            // classes
            this.C1 = CreateClass(this.Domain, "c1");
            this.C2 = CreateClass(this.Domain, "c2");
            this.C3 = CreateClass(this.Domain, "c3");
            this.C4 = CreateClass(this.Domain, "c4");

            this.CreateInheritance();

            this.CreateLists();

            this.Domain.Validate();

            if (!this.Domain.IsValid)
            {
                throw new Exception("Domain invalid");
            }
        }

        internal void PopulateWithSuperDomains()
        {
            this.PopulateSuperDomain();

            // interfaces
            this.I12 = CreateInterface(this.SuperDomain, "i12");
            this.I34 = CreateInterface(this.Domain, "i34");
            this.I1 = CreateInterface(this.SuperDomain, "i1");
            this.I2 = CreateInterface(this.SuperDomain, "i2");
            this.I3 = CreateInterface(this.Domain, "i3");
            this.I4 = CreateInterface(this.Domain, "i4");

            // classes
            this.C1 = CreateClass(this.Domain, "c1");
            this.C2 = CreateClass(this.Domain, "c2");
            this.C3 = CreateClass(this.Domain, "c3");
            this.C4 = CreateClass(this.Domain, "c4");

            this.CreateInheritance();

            this.CreateLists();

            this.Domain.Validate();

            if (!this.Domain.IsValid)
            {
                throw new Exception("Domain invalid");
            }
        }

        private void PopulateSuperDomain()
        {
            this.SuperDomain = this.Domain.AddDirectSuperDomain(Guid.NewGuid());
            this.SuperDomain.Name = "SuperDomain";
        }

        private void CreateInheritance()
        {
            this.C1.AddDirectSupertype(this.I1);
            this.C1.AddDirectSupertype(this.I12);

            this.C2.AddDirectSupertype(this.I2);
            this.C2.AddDirectSupertype(this.I12);

            this.C3.AddDirectSupertype(this.I3);
            this.C3.AddDirectSupertype(this.I34);

            this.C4.AddDirectSupertype(this.I4);
            this.C4.AddDirectSupertype(this.I34);
        }

        private void CreateLists()
        {
            var compositeConcreteClassList = new ArrayList { this.C1, this.C2, this.C3, this.C4 };

            this.CompositeConcreteClasses = (MetaObject[])compositeConcreteClassList.ToArray(typeof(MetaObject));

            var compositeInterfaceList = new ArrayList { this.I1, this.I2, this.I3, this.I4, this.I12, this.I34 };
            this.CompositeInterfaces = (MetaObject[])compositeInterfaceList.ToArray(typeof(MetaObject));
            this.Interfaces = this.CompositeInterfaces;

            var compositeClassList = new ArrayList();
            compositeClassList.AddRange(this.CompositeConcreteClasses);
            this.CompositeClasses = (MetaObject[])compositeClassList.ToArray(typeof(MetaObject));

            var compositeList = new ArrayList();
            compositeList.AddRange(this.CompositeClasses);
            compositeList.AddRange(this.CompositeInterfaces);
            this.Composites = (MetaObject[])compositeClassList.ToArray(typeof(MetaObject));

            var classList = new ArrayList();
            classList.AddRange(this.UnitTypes);
            classList.AddRange(this.CompositeClasses);
            this.Classes = (MetaObject[])classList.ToArray(typeof(MetaObject));
        }

        private void GetUnits()
        {
            this.BinaryType = (MetaObject)this.Domain.MetaDomain.Find(MetaUnitIds.BinaryId);
            this.BooleanType = (MetaObject)this.Domain.MetaDomain.Find(MetaUnitIds.BooleanId);
            this.DateTimeType = (MetaObject)this.Domain.MetaDomain.Find(MetaUnitIds.DatetimeId);
            this.DecimalType = (MetaObject)this.Domain.MetaDomain.Find(MetaUnitIds.DecimalId);
            this.DoubleType = (MetaObject)this.Domain.MetaDomain.Find(MetaUnitIds.DoubleId);
            this.IntegerType = (MetaObject)this.Domain.MetaDomain.Find(MetaUnitIds.IntegerId);
            this.LongType = (MetaObject)this.Domain.MetaDomain.Find(MetaUnitIds.LongId);
            this.StringType = (MetaObject)this.Domain.MetaDomain.Find(MetaUnitIds.StringId);
            this.UniqueType = (MetaObject)this.Domain.MetaDomain.Find(MetaUnitIds.Unique);
        }
    }
}