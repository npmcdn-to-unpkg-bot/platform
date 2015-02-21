namespace Allors.Meta
{
	using System;

	public partial class FaceToFaceCommunicationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Participant.IsRequired = true;
		}
	}
}