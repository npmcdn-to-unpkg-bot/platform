namespace Allors.Meta
{
    public partial class ImageClass : Class
    {
        #region Allors
        [Id("5EE8B041-5400-4A9A-8154-2539CCA8FD64")]
        #endregion
        public MethodType CreateResponsive;

        #region Allors
        [Id("D523E7D7-4A98-4763-9A17-C906D774699A")]
        #endregion
        public MethodType CreateThumbnail;

        #region Allors
        [Id("366410a7-7d51-4d7c-82fd-3444bdc0b3f7")]
        [AssociationId("9d45e17e-962b-4f9b-a029-c1c1562e5260")]
        [RoleId("9ed94fa8-e01e-4f63-9932-d56134616474")]
        #endregion
        [Indexed]
        [Type(typeof(MediaClass))]
        [Plural("Originals")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Original;

        #region Allors
        [Id("59689164-7a45-45d4-98fa-f8cf50c62899")]
        [AssociationId("386c7cfc-4bec-4564-a7c4-b2c1bccf6ebe")]
        [RoleId("ce4c0fbb-5bdb-4c7f-a70a-b930c1020624")]
        #endregion
        [Indexed]
        [Type(typeof(MediaClass))]
        [Plural("Responsives")]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType Responsive;

        #region Allors
        [Id("d149b012-1dc2-4bd1-a650-26b7c6f9024b")]
        [AssociationId("75fccc6e-1c89-4e0f-88c2-527eb3b0d71d")]
        [RoleId("2f1c8149-f94a-448b-a832-4994f635c48f")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        [Plural("OriginalFilenames")]
        public RelationType OriginalFilename;

        #region Allors
        [Id("d54405bf-efa0-4b64-a086-1c85ae0c5b2f")]
        [AssociationId("2af16928-df35-42c6-a468-15e4bae5e035")]
        [RoleId("55f44481-d670-4812-a168-e96509a70e25")]
        #endregion
        [Indexed]
        [Type(typeof(MediaClass))]
        [Plural("Thumbnails")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Thumbnail;
    }
}