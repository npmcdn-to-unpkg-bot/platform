namespace Allors.Web.Desktop
{
    using System;

    public class Button : System.Web.UI.WebControls.Button
    {
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }

        protected override void RaisePostBackEvent(string eventArgument)
        {
            base.RaisePostBackEvent(eventArgument);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
