// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public  partial class Sandbox : SandboxAllors 
	{
		public Sandbox(Allors.R1.IStrategy allors) : base(allors) {}

		public static Sandbox Instantiate (Allors.R1.ISession allorsSession, string allorsObjectId)
		{
			return (Sandbox) allorsSession.Instantiate(allorsObjectId);		
		}
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class SandboxAllors :  Allors.R1.ObjectBase 
	{
		protected SandboxAllors(Allors.R1.IStrategy allors) : base(allors){}


		virtual public Allors.R1.Extent<global::Domain.Sandbox> InvisibleManies
		{ 
			get
			{
				return Strategy.GetCompositeRoles(SandboxMeta.InvisibleMany);
			}
			set
			{
				Strategy.SetCompositeRoles(SandboxMeta.InvisibleMany, value);
			}
		}

		virtual public void AddInvisibleMany (global::Domain.Sandbox value)
		{
			Strategy.AddCompositeRole(SandboxMeta.InvisibleMany, value);
		}

		virtual public void RemoveInvisibleMany (global::Domain.Sandbox value)
		{
			Strategy.RemoveCompositeRole(SandboxMeta.InvisibleMany,value);
		}

		virtual public bool ExistInvisibleManies
		{
			get
			{
				return Strategy.ExistCompositeRoles(SandboxMeta.InvisibleMany);
			}
		}

		virtual public void RemoveInvisibleManies()
		{
			Strategy.RemoveCompositeRoles(SandboxMeta.InvisibleMany);
		}


		virtual public global::Domain.Sandbox InvisibleOne
		{ 
			get
			{
				return (global::Domain.Sandbox) Strategy.GetCompositeRole(SandboxMeta.InvisibleOne);
			}
			set
			{
				Strategy.SetCompositeRole(SandboxMeta.InvisibleOne ,value);
			}
		}

		virtual public bool ExistInvisibleOne
		{
			get
			{
				return Strategy.ExistCompositeRole(SandboxMeta.InvisibleOne);
			}
		}

		virtual public void RemoveInvisibleOne()
		{
			Strategy.RemoveCompositeRole(SandboxMeta.InvisibleOne);
		}



		virtual public global::System.String InvisibleValue 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(SandboxMeta.InvisibleValue);
			}
			set
			{
				Strategy.SetUnitRole(SandboxMeta.InvisibleValue, value);
			}
		}

		virtual public bool ExistInvisibleValue{
			get
			{
				return Strategy.ExistUnitRole(SandboxMeta.InvisibleValue);
			}
		}

		virtual public void RemoveInvisibleValue()
		{
			Strategy.RemoveUnitRole(SandboxMeta.InvisibleValue);
		}



		virtual public global::System.String Test 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(SandboxMeta.Test);
			}
			set
			{
				Strategy.SetUnitRole(SandboxMeta.Test, value);
			}
		}

		virtual public bool ExistTest{
			get
			{
				return Strategy.ExistUnitRole(SandboxMeta.Test);
			}
		}

		virtual public void RemoveTest()
		{
			Strategy.RemoveUnitRole(SandboxMeta.Test);
		}



		virtual public global::System.String AllorsString 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(SandboxMeta.AllorsString);
			}
			set
			{
				Strategy.SetUnitRole(SandboxMeta.AllorsString, value);
			}
		}

		virtual public bool ExistAllorsString{
			get
			{
				return Strategy.ExistUnitRole(SandboxMeta.AllorsString);
			}
		}

		virtual public void RemoveAllorsString()
		{
			Strategy.RemoveUnitRole(SandboxMeta.AllorsString);
		}



		virtual public Allors.R1.Extent<global::Domain.Sandbox> SandboxesWhereInvisibleMany
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(SandboxMeta.SandboxesWhereInvisibleMany);
			}
		}

		virtual public bool ExistSandboxesWhereInvisibleMany
		{
			get
			{
				return Strategy.ExistCompositeAssociations(SandboxMeta.SandboxesWhereInvisibleMany);
			}
		}


		virtual public global::Domain.Sandbox SandboxWhereInvisibleOne
		{ 
			get
			{
				return (global::Domain.Sandbox) Strategy.GetCompositeAssociation(SandboxMeta.SandboxWhereInvisibleOne);
			}
		} 

		virtual public bool ExistSandboxWhereInvisibleOne
		{
			get
			{
				return Strategy.ExistCompositeAssociation(SandboxMeta.SandboxWhereInvisibleOne);
			}
		}

	}
}