namespace Allors.Meta
{
    public partial class DependeeClass : Class
    {
        #region Allors
        [Id("1b8e0350-c446-48dc-85c0-71130cc1490e")]
        [AssociationId("97c6a03f-f0c7-4c7d-b40f-1353e34431bd")]
        [RoleId("89b8f5f6-5589-42ad-ac9e-1d984c02f7ea")]
        #endregion
        [Indexed]
        [Type(typeof(SubdependeeClass))]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType Subdependee;

        #region Allors
        [Id("c1e86449-e5a8-4911-97c7-b03de9142f98")]
        [AssociationId("2786b8ca-2d71-44cc-8e1e-1896ac5e6c5c")]
        [RoleId("af75f294-b20d-4304-8804-32ef9c0a324a")]
        #endregion
        [Type(typeof(AllorsIntegerUnit))]
        [Plural("Subcounters")]
        public RelationType Subcounter;

        #region Allors
        [Id("d58d1f28-3abd-4294-abde-885bdd16f466")]
        [AssociationId("9a867244-8ea3-402b-9a9c-a78727dbee78")]
        [RoleId("5f570211-688e-4050-bf54-997d22a529d5")]
        #endregion
        [Type(typeof(AllorsIntegerUnit))]
        [Plural("Counters")]
        public RelationType Counter;

        #region Allors
        [Id("e73b8fc5-0148-486a-9379-cfb051b303d2")]
        [AssociationId("db615c1c-3d08-4faa-b19f-740bd7102fbd")]
        [RoleId("bde110ae-8242-4d98-bdc3-feeed8fde742")]
        #endregion
        [Type(typeof(AllorsBooleanUnit))]
        [Plural("DeleteDependents")]
        public RelationType DeleteDependent;
    }
}