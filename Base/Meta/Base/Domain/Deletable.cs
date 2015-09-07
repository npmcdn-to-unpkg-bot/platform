namespace Allors.Meta
{
	#region Allors
	[Id("9279e337-c658-4086-946d-03c75cdb1ad3")]
	#endregion
  	public partial class DeletableInterface: Interface
	{
        #region Allors
        [Id("962677D5-C9AE-44AB-88FB-C7A0AA960C0B")]
        #endregion
        public MethodType Delete;

        public static DeletableInterface Instance {get; internal set;}

		internal DeletableInterface() : base(MetaPopulation.Instance)
        {
        }
	}
}