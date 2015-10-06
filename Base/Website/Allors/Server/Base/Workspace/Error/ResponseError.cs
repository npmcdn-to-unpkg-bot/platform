namespace Allors.Web
{
    using System.Collections.Generic;
    using System.Linq;

    using Allors.Domain;

    public abstract class ResponseError
    {
        protected ResponseError()
        {
            this.VersionErrors = new List<string>();
            this.AccessErrors = new List<string>();
            this.MissingErrors = new List<string>();
            this.DerivationErrors = new List<ResponseDerivationError>();            
        }

        public bool HasErrors => this.VersionErrors.Count > 0 || this.AccessErrors.Count > 0 || this.MissingErrors.Count > 0 || this.DerivationErrors.Count > 0;

        public List<string> VersionErrors { get; set; }

        public List<string> AccessErrors { get; set; }

        public List<string> MissingErrors { get; set; }

        public List<ResponseDerivationError> DerivationErrors { get; set; }

        public void AddVersionError(IObject obj)
        {
            this.VersionErrors.Add(obj.Id.ToString());
        }

        public void AddAccessError(IObject obj)
        {
            this.AccessErrors.Add(obj.Id.ToString());
        }

        public void AddMissingError(string id)
        {
            this.AccessErrors.Add(id);
        }

        public void AddDerivationErrors(DerivationLog derivationLog)
        {
            foreach (IDerivationError derivationError in derivationLog.Errors)
            {
                this.DerivationErrors.Add(new ResponseDerivationError
                                              {
                                                  M = derivationError.Message,  
                                                  R = derivationError.Relations.Select(x => new string[] { x.Association.Id.ToString(), x.RoleType.Name }).ToArray()
                                              });
            }
        }
    }
}