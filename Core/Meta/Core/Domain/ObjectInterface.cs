namespace Allors.Meta
{
	using System;

	public class ObjectInterface: Interface
	{
		public static readonly ObjectInterface Instance;

		static ObjectInterface()
		{
		    Instance = new ObjectInterface
		                   {
		                       SingularName = "Object", 
                               PluralName = "Objects"
		                   };
		}

		private ObjectInterface() : base(CoreDomain.Instance, new Guid("f8a7cd0e-bd7f-4ce9-ab5d-8c1629cd883d"))
        {
        }
	}

}