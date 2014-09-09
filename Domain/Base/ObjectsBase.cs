// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectsBase.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
//
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
//
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
//
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors
{
    using Allors.Meta;
    
    using Allors.Domain;

    public abstract partial class ObjectsBase<T> : IObjects where T : IObject
    {
        private readonly ISession session;

        protected ObjectsBase(ISession session)
        {
            this.session = session;
        }

        public abstract Composite ObjectType { get; }

        public ISession Session
        {
            get { return this.session; }
        }

        public Extent<T> Extent()
        {
            return this.Session.Extent<T>();
        }

        public T FindBy(RoleType roleType, object parameter)
        {
            var workspaceSession = this.Session as IWorkspaceSession;
            if (workspaceSession != null)
            {
                var workspaceExtent = workspaceSession.LocalExtent(this.ObjectType);
                workspaceExtent.Filter.AddEquals(roleType, parameter);

                object workspaceObject = workspaceExtent.First;

                if (workspaceObject == null)
                {
                    var databaseExtent = ((IWorkspaceSession)this.Session).DatabaseSession.Extent(this.ObjectType);
                    databaseExtent.Filter.AddEquals(roleType, parameter);

                    foreach (IObject allDatabaseObject in databaseExtent)
                    {
                        if (allDatabaseObject != null)
                        {
                            var existInWorkspace = false;
                            var allWorkspaceObjects = workspaceSession.LocalExtent(this.ObjectType);
                            foreach (IObject allWorkspaceObject in allWorkspaceObjects)
                            {
                                if (allWorkspaceObject.Id.Equals(allDatabaseObject.Id))
                                {
                                    existInWorkspace = true;
                                    break;
                                }
                            }

                            if (!existInWorkspace)
                            {
                                workspaceObject = workspaceSession.Instantiate(allDatabaseObject);
                                break;
                            }
                        }
                    }
                }

                return (T)workspaceObject;
            }

            var extent = this.Session.Extent(this.ObjectType);
            extent.Filter.AddEquals(roleType, parameter);
            return (T)extent.First;
        }

        protected virtual void CorePrepare(Setup config)
        {
            config.Add(this);
        }

        protected virtual void CoreSetup(Setup config)
        {
        }

        protected virtual void CoreSecure(Security config)
        {
        }
    }
}