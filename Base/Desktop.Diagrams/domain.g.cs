namespace Allors.Domain
{
					public interface Enumeration : UniquelyIdentifiable 
					{
					}
					public interface ObjectState : UniquelyIdentifiable 
					{
									global::System.String Name {set;}

					}
					public interface UniquelyIdentifiable 
					{
									global::System.Guid UniqueId {set;}

					}
					public interface User 
					{
									global::System.String UserName {set;}


									global::System.String UserEmail {set;}

					}
					public interface I1 
					{
									global::System.String I1AllorsString {set;}

					}


					public interface Counter : UniquelyIdentifiable 
					{
					}
					public interface Media : UniquelyIdentifiable 
					{
									global::System.Guid? Revision {set;}


									global::System.String InDataUri {set;}

					}
					public interface Person : User, UniquelyIdentifiable 
					{
									global::System.String FirstName {set;}


									global::System.String LastName {set;}


									global::System.String MiddleName {set;}


									global::System.DateTime? BirthDate {set;}


									global::System.Boolean? IsStudent {set;}


									Media Photo {set;}


									global::System.Decimal? Weight {set;}


									Organisation CycleOne {set;}


									Organisation CycleMany {set;}

					}
					public interface Role : UniquelyIdentifiable 
					{
					}
					public interface UserGroup : UniquelyIdentifiable 
					{
					}
					public interface C1 : I1 
					{
									global::System.Byte[] C1AllorsBinary {set;}


									global::System.String C1AllorsString {set;}


									C1 C1C1One2Manies {set;}


									C1 C1C1One2One {set;}

					}
					public interface Gender : Enumeration 
					{
					}
					public interface OrderObjectState : ObjectState 
					{
					}
					public interface Organisation : UniquelyIdentifiable 
					{
									Person Employees {set;}


									Person Manager {set;}


									global::System.String Name {set;}


									Person Owner {set;}


									Person Shareholders {set;}


									Person CycleOne {set;}


									Person CycleMany {set;}

					}

}