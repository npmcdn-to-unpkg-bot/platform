namespace Allors.Domain
{
					public interface I1 
					{
									global::System.String I1AllorsString {set;}

					}
					public interface User 
					{
									global::System.String UserEmail {set;}

					}


					public interface C1 : I1 
					{
									global::System.Byte[] C1AllorsBinary {set;}


									global::System.String C1AllorsString {set;}


									C1 C1C1One2Manies {set;}


									C1 C1C1One2One {set;}

					}
					public interface Media 
					{
					}
					public interface Person : User 
					{
									Media Photo {set;}


									global::System.Decimal? Weight {set;}


									global::System.String FirstName {set;}


									global::System.String LastName {set;}


									global::System.String MiddleName {set;}

					}
					public interface Organisation 
					{
									Person Shareholders {set;}


									global::System.String Name {set;}


									Person Employees {set;}


									Person Owner {set;}


									Person Manager {set;}

					}

}