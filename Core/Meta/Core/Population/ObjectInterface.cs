namespace Allors.Meta
{
	using System;

	public partial class ObjectInterface: Interface
	{
        public static ObjectInterface Instance { get; internal set; }

		private ObjectInterface() : base(CoreDomain.Instance, new Guid("12504F04-02C6-4778-98FE-04EBA12EF8B2"))
		{
		    this.SingularName = "Object";
		    this.PluralName = "Objects";
		}

	    internal override void BuildInheritances()
	    {
	    }

	    internal override void BuildRelationTypes()
	    {
	    }

	    internal override void SetRelationTypes()
	    {
}
	}

}