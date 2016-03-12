namespace Allors.Domain
{
					public interface ObjectState 
					{
									global::System.String Name {set;}

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


					public interface Media 
					{
					}
					public interface Person : User 
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
					public interface C1 : I1 
					{
									global::System.Byte[] C1AllorsBinary {set;}


									global::System.String C1AllorsString {set;}


									C1 C1C1One2Manies {set;}


									C1 C1C1One2One {set;}

					}
					public interface OrderObjectState : ObjectState 
					{
					}
					public interface Organisation 
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