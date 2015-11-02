namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("253b0d71-9eaa-4d87-9094-3b549d8446b3")]
	#endregion
	[Plural("S1s")]
  	public partial class S1Interface: Interface
	{
        [Id("709F5C76-D6C6-4F96-9C24-1E85956D5491")]
        public MethodType SuperinterfaceMethod;

        public static S1Interface Instance {get; internal set;}

		internal S1Interface() : base(MetaPopulation.Instance)
        {
        }
	}
}