namespace Allors.Meta
{
    public partial class I12Interface : Interface
    {
        #region Allors
        [Id("042d1311-1c06-4d7c-b68e-eb734f9c7327")]
        [AssociationId("0d3f0f95-aaa2-47bb-9e2b-654d2747b2b1")]
        [RoleId("f7809a25-1b10-4eb0-9309-aeea6efcd7cb")]
        #endregion
        [Type(typeof(AllorsBinaryUnit))]
        [Size(-1)]
        public RelationType AllorsBinary;

        #region Allors
        [Id("107c212d-cc1c-41b2-9c1d-b40c0102072c")]
        [AssociationId("0a1b3b66-6bb2-4062-b3bb-991987dd5194")]
        [RoleId("4c448b25-b56c-4486-b0c8-ac04a3249677")]
        #endregion
        [Indexed]
        [Type(typeof(C2Class))]
        [Plural("C2One2Ones")]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType C2One2One;

        #region Allors
        [Id("1611cb5d-4676-4e85-bfc5-5572e8ff1138")]
        [AssociationId("4af20cc8-a610-4493-9420-7cd110cc6755")]
        [RoleId("5f2eff86-71bf-480d-a6ad-1c93fc68b08d")]
        #endregion
        [Type(typeof(AllorsFloatUnit))]
        public RelationType AllorsDouble;

        #region Allors
        [Id("167b53c0-644c-467e-9f7c-fcb9415d02c6")]
        [AssociationId("d039c8f9-217a-46cc-b428-7480d4991e1e")]
        [RoleId("2e3dc9b9-3700-4090-bafa-2c60050d52d5")]
        #endregion
        [Indexed]
        [Type(typeof(I1Interface))]
        [Plural("I1Many2Ones")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType I1Many2One;

        #region Allors
        [Id("199a84c4-c7cb-4f23-8b6c-078b14525e18")]
        [AssociationId("65ed1ff6-eb81-410d-8817-62d61765408a")]
        [RoleId("c778c7a7-9cf7-4a7e-8408-e4eb1ca94ce8")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType AllorsString;

        #region Allors
        [Id("1bf2abe0-9273-4fb9-b491-020320f1f8db")]
        [AssociationId("732fc964-194e-4ece-bd39-bb3c47b83ff9")]
        [RoleId("b311c57d-9565-48c1-80d8-1d3cf5a498ea")]
        #endregion
        [Indexed]
        [Type(typeof(I12Interface))]
        [Plural("I12Many2Manies")]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType I12Many2Many;

        #region Allors
        [Id("41a74fec-cfbc-43ca-a6e7-890f0dd1eddb")]
        [AssociationId("7293e939-ad0b-4b62-935d-44a5309f2515")]
        [RoleId("295a4e46-3133-4aff-a1dc-5101e584fb8a")]
        #endregion
        [Type(typeof(AllorsDecimalUnit))]
        [Precision(19)]
        [Scale(2)]
        public RelationType AllorsDecimal;

        #region Allors
        [Id("4a2b2f43-037d-4149-8a1e-401e5df963ba")]
        [AssociationId("cd90d290-95da-4137-aaf1-bcb59f10e9cb")]
        [RoleId("f266759c-34c5-49a8-8d92-e2bbcb41c86a")]
        #endregion
        [Indexed]
        [Type(typeof(I2Interface))]
        [Plural("I2Many2Manies")]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType I2Many2Many;

        #region Allors
        [Id("51ebb024-c847-4165-b216-b3b6e8883961")]
        [AssociationId("04bca123-7c45-43f4-a5ed-8691b0cbb0e3")]
        [RoleId("f5928b47-5a57-4be8-a0a9-a729e8e467bb")]
        #endregion
        [Indexed]
        [Type(typeof(C2Class))]
        [Plural("C2Many2Manies")]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType C2Many2Many;

        #region Allors
        [Id("59ae05e3-573c-4ea4-9181-2c545236ed1e")]
        [AssociationId("064f5e1b-b5c8-45ee-baf1-094f6a723ede")]
        [RoleId("397b339e-0277-4700-a5d1-d9d0ac23c362")]
        #endregion
        [Indexed]
        [Type(typeof(I1Interface))]
        [Plural("I1Many2Manies")]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType I1Many2Many;

        #region Allors
        [Id("5e473f63-b1d7-4530-b64f-26435fb5063c")]
        [AssociationId("83e23750-52eb-4b3f-a675-bfe32570357b")]
        [RoleId("d786aeb4-03bb-419a-90c9-e6ddaa940e93")]
        #endregion
        [Indexed]
        [Type(typeof(I12Interface))]
        [Plural("I12One2Manies")]
        [Multiplicity(Multiplicity.OneToMany)]
        public RelationType I12One2Many;

        #region Allors
        [Id("6daafb16-1bc3-4f15-8e25-1a982c5bb3c5")]
        [AssociationId("d39d3782-71a6-4b63-aaeb-0a6da0db153d")]
        [RoleId("a89707e2-e3e1-4d24-9c56-180671e3409c")]
        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        [Plural("Names")]
        public RelationType Name;

        #region Allors
        [Id("7827af95-147f-4803-865a-b418d567da68")]
        [AssociationId("7e707f89-6dd2-44a4-8f85-e00666af4d00")]
        [RoleId("a4c1f678-a3ae-4707-81e9-b3f3411a5d93")]
        #endregion
        [Indexed]
        [Type(typeof(C1Class))]
        [Plural("C1Many2Manies")]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType C1Many2Many;

        #region Allors
        [Id("7f6fdb73-3e19-40e7-8feb-6ddbdf2e745a")]
        [AssociationId("644f55c6-8d39-4602-89bb-5797c9c8e1fd")]
        [RoleId("2073096f-8918-4432-8fa2-42f4fd1a53a1")]
        #endregion
        [Indexed]
        [Type(typeof(I2Interface))]
        [Plural("I2Many2Ones")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType I2Many2One;

        #region Allors
        [Id("93a59d0a-278d-435b-967e-551523f0cb85")]
        [AssociationId("9c700ad0-e33e-4731-ac3a-4063c2da655b")]
        [RoleId("839c7aaa-f044-4b93-97aa-00beeed8f3eb")]
        #endregion
        [Type(typeof(AllorsUniqueUnit))]
        public RelationType AllorsUnique;

        #region Allors
        [Id("95551e3a-bad2-4136-923f-c8e5f0f2aec7")]
        [AssociationId("f57afc9e-3832-4ae1-b3a0-659d7f00604c")]
        [RoleId("cbd73ad2-a4cd-4b65-a3cd-55bb7c6f52bc")]
        #endregion
        [Type(typeof(AllorsIntegerUnit))]
        public RelationType AllorsInteger;

        #region Allors
        [Id("95c77a0f-7f4c-4142-a93f-f688cfd554af")]
        [AssociationId("870af1ab-075f-4e19-a283-6e6875d362bb")]
        [RoleId("29f38fb4-8e6a-4f70-9ee9-f6819b9d759e")]
        #endregion
        [Indexed]
        [Type(typeof(I1Interface))]
        [Plural("I1One2Manies")]
        [Multiplicity(Multiplicity.OneToMany)]
        public RelationType I1One2Many;

        #region Allors
        [Id("9aefdda0-e547-4c9b-bf28-431669f8ea2e")]
        [AssociationId("f4399c8b-3394-4c2a-9ff0-16b2ece87fdf")]
        [RoleId("ee9379c4-ef6a-4c6e-8190-bc71c36ac009")]
        #endregion
        [Indexed]
        [Type(typeof(C1Class))]
        [Plural("C1One2Ones")]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType C1One2One;

        #region Allors
        [Id("a89b4c06-bba5-4b05-bd6f-c32bc195c32f")]
        [AssociationId("8dd3e2b6-805f-4c93-98d8-4864e6807760")]
        [RoleId("e68fba09-6113-4b49-a6fa-a09e46a004f1")]
        #endregion
        [Indexed]
        [Type(typeof(I12Interface))]
        [Plural("I12One2Ones")]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType I12One2One;

        #region Allors
        [Id("ac920d1d-290b-484b-9283-3829337182bc")]
        [AssociationId("991e5b73-a9b0-40a4-8230-b3fb7cc46761")]
        [RoleId("07702752-2c97-4b44-8c43-7c1f2a5e3d0d")]
        #endregion
        [Indexed]
        [Type(typeof(I2Interface))]
        [Plural("I2One2Ones")]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType I2One2One;

        #region Allors
        [Id("b2e3ddda-0cc3-4cfd-a114-9040882ec58a")]
        [AssociationId("014cf60e-595f-42d5-9146-e7d860396f4d")]
        [RoleId("d5c22b99-6984-4042-98fd-93fe60dfe5d7")]
        #endregion
        [Indexed]
        [Type(typeof(I12Interface))]
        [Plural("Dependencies")]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType Dependency;

        #region Allors
        [Id("b2f568a1-51ba-4b6b-a1f1-b82bdec382b5")]
        [AssociationId("6f37656a-21d0-4574-8eac-5342f7c6850d")]
        [RoleId("09a2a7a1-4713-4c5c-828d-8be40f33d1ae")]
        #endregion
        [Indexed]
        [Type(typeof(I2Interface))]
        [Plural("I2One2Manies")]
        [Multiplicity(Multiplicity.OneToMany)]
        public RelationType I2One2Many;

        #region Allors
        [Id("c018face-b292-455c-a2c0-8f71377fb6cb")]
        [AssociationId("3239eb17-dc55-465f-854c-1d2d024bca94")]
        [RoleId("2ff52878-3ade-4afe-9961-8f79336bb0a2")]
        #endregion
        [Indexed]
        [Type(typeof(C2Class))]
        [Plural("C2Many2Ones")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType C2Many2One;

        #region Allors
        [Id("c6ecc142-0fbd-48b7-98ae-994fa9b5b814")]
        [AssociationId("c7469ffd-ffd7-4913-962c-8a7a0b4df3dd")]
        [RoleId("1d091625-ec4a-486d-a9be-8f87fe300967")]
        #endregion
        [Indexed]
        [Type(typeof(I12Interface))]
        [Plural("I12Many2Ones")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType I12Many2One;

        #region Allors
        [Id("ccdd1ae2-263e-4221-9841-4cff1907ee8d")]
        [AssociationId("55be99e6-71fd-4483-b211-c3080e6ffa05")]
        [RoleId("79723949-b9ad-40bf-baee-96d001942855")]
        #endregion
        [Type(typeof(AllorsBooleanUnit))]
        public RelationType AllorsBoolean;

        #region Allors
        [Id("ce0f7d58-b415-43f3-989b-9d8b34754e4b")]
        [AssociationId("33bd508e-d754-4533-9ecd-9c8ce8d10c88")]
        [RoleId("72545574-d138-467c-8f21-0c5d15b1d793")]
        #endregion
        [Indexed]
        [Type(typeof(I1Interface))]
        [Plural("I1One2Ones")]
        [Multiplicity(Multiplicity.OneToOne)]
        public RelationType I1One2One;

        #region Allors
        [Id("f302dd07-1abc-409e-aa71-ec9f7ac439aa")]
        [AssociationId("99b3bf26-3437-4b5b-a786-28c095975a48")]
        [RoleId("ee291df6-6a3e-4d92-a779-879679e1b688")]
        #endregion
        [Indexed]
        [Type(typeof(C1Class))]
        [Plural("C1One2Manies")]
        [Multiplicity(Multiplicity.OneToMany)]
        public RelationType C1One2Many;

        #region Allors
        [Id("f6436bc9-e307-4001-8f1f-5b37553ab3c6")]
        [AssociationId("63297178-60c1-4cbc-a68d-2842385ba266")]
        [RoleId("6e5b98b0-1af3-4e99-8781-37ea97792a24")]
        #endregion
        [Indexed]
        [Type(typeof(C1Class))]
        [Plural("C1Many2Ones")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType C1Many2One;

        #region Allors
        [Id("fa6656dc-3a7a-4701-bc6b-3cd06aaa4483")]
        [AssociationId("6e4d05f3-52e3-4937-b8d2-8d9d52e7c8bf")]
        [RoleId("823e8329-0a90-49ed-9b2c-4bfb9db2ee02")]
        #endregion
        [Type(typeof(AllorsDateTimeUnit))]
        public RelationType AllorsDateTime;
    }
}