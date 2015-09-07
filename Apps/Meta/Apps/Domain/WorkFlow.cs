namespace Allors.Meta
{
	using System;

	#region Allors
	[Id("b2a169ce-4e2a-48fc-aa39-dfc783ecb401")]
	#endregion
	[Inherit(typeof(WorkEffortInterface))]

	[Plural("WorkFlows")]
	public partial class WorkFlowClass : Class
	{

		public static WorkFlowClass Instance {get; internal set;}

		internal WorkFlowClass() : base(MetaPopulation.Instance)
        {
        }
	}
}