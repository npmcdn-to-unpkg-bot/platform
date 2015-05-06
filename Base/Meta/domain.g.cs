namespace Allors.Meta
{
	using System;
	public partial class BaseDomain : Domain
	{
		public static BaseDomain Instance { get; internal set; }

		private BaseDomain(MetaPopulation metaPopulation) : base(metaPopulation, new Guid("770538dd-7b19-4694-bdce-cf04dcf9cf62"))
        {
			this.Name = "Base";
        }

		internal override void Build()
		{
			this.AddDirectSuperdomain(CoreDomain.Instance);
		}
	}

	public partial class TestsDomain : Domain
	{
		public static TestsDomain Instance { get; internal set; }

		private TestsDomain(MetaPopulation metaPopulation) : base(metaPopulation, new Guid("af96e2b7-3bb5-4cd1-b02c-39a67c99a11a"))
        {
			this.Name = "Tests";
        }

		internal override void Build()
		{
			this.AddDirectSuperdomain(CoreDomain.Instance);
				this.AddDirectSuperdomain(BaseDomain.Instance);

		}

	}
}