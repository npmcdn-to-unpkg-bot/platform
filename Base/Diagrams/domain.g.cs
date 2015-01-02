namespace Allors.Domain
{
	public interface Object 
	{
	}
	public interface AccessControlledObject  : Object, Derivable 
	{
					Permission DeniedPermissions {set;}

					SecurityToken SecurityTokens {set;}

	}
	public interface Derivable  : Object 
	{
	}
	public interface Address  : Object, UserInterfaceable 
	{
					Place Place {set;}

	}
	public interface UserInterfaceable  : Object, AccessControlledObject 
	{
					global::System.String DisplayName {set;}

	}
	public interface Commentable  : Object 
	{
					global::System.String Comment {set;}

	}
	public interface Deletable  : Object 
	{
	}
	public interface DerivationLogI12  : Object 
	{
					global::System.Guid? Id {set;}

	}
	public interface Enumeration  : Object, UserInterfaceable, UniquelyIdentifiable 
	{
					global::System.Boolean IsActive {set;}

					LocalisedText LocalisedNames {set;}

					global::System.String Name {set;}

	}
	public interface UniquelyIdentifiable  : Object 
	{
					global::System.Guid UniqueId {set;}

	}
	public interface I1  : Object, I12, S1 
	{
					global::System.Byte[] I1AllorsBinary {set;}

					global::System.Boolean? I1AllorsBoolean {set;}

					global::System.DateTime? I1AllorsDateTime {set;}

					global::System.Decimal? I1AllorsDecimal {set;}

					global::System.Double? I1AllorsFloat {set;}

					global::System.Int32? I1AllorsInteger {set;}

					global::System.String I1AllorsString {set;}

					global::System.Guid? I1AllorsUnique {set;}

					C1 I1C1Many2Manies {set;}

					C1 I1C1Many2One {set;}

					C1 I1C1One2Manies {set;}

					C1 I1C1One2One {set;}

					C2 I1C2Many2Manies {set;}

					C2 I1C2Many2One {set;}

					C2 I1C2One2Manies {set;}

					C2 I1C2One2One {set;}

					I12 I1I12Many2Manies {set;}

					I12 I1I12Many2One {set;}

					I12 I1I12One2Manies {set;}

					I12 I1I12One2One {set;}

					I1 I1I1Many2Manies {set;}

					I1 I1I1Many2One {set;}

					I1 I1I1One2Manies {set;}

					I1 I1I1One2One {set;}

					I2 I1I2Many2Manies {set;}

					I2 I1I2Many2One {set;}

					I2 I1I2One2Manies {set;}

					I2 I1I2One2One {set;}

	}
	public interface I12  : Object, UserInterfaceable 
	{
					I12 Dependencies {set;}

					global::System.Byte[] I12AllorsBinary {set;}

					global::System.Boolean? I12AllorsBoolean {set;}

					global::System.DateTime? I12AllorsDateTime {set;}

					global::System.Decimal? I12AllorsDecimal {set;}

					global::System.Double? I12AllorsFloat {set;}

					global::System.Int32? I12AllorsInteger {set;}

					global::System.String I12AllorsString {set;}

					global::System.Guid? I12AllorsUnique {set;}

					C1 I12C1Many2Manies {set;}

					C1 I12C1Many2One {set;}

					C1 I12C1One2Manies {set;}

					C1 I12C1One2One {set;}

					C2 I12C2Many2Manies {set;}

					C2 I12C2Many2One {set;}

					C2 I12C2One2One {set;}

					I12 I12I12Many2Manies {set;}

					I12 I12I12Many2One {set;}

					I12 I12I12One2Manies {set;}

					I12 I12I12One2One {set;}

					I1 I12I1Many2Manies {set;}

					I1 I12I1Many2One {set;}

					I1 I12I1One2Manies {set;}

					I1 I12I1One2One {set;}

					I2 I12I2Many2Manies {set;}

					I2 I12I2Many2One {set;}

					I2 I12I2One2Manies {set;}

					I2 I12I2One2One {set;}

					global::System.String Name {set;}

	}
	public interface S1  : Object 
	{
	}
	public interface I2  : Object, I12, UserInterfaceable 
	{
					global::System.Byte[] I2AllorsBinary {set;}

					global::System.Boolean? I2AllorsBoolean {set;}

					global::System.DateTime? I2AllorsDateTime {set;}

					global::System.Decimal? I2AllorsDecimal {set;}

					global::System.Double? I2AllorsFloat {set;}

					global::System.Int32? I2AllorsInteger {set;}

					global::System.String I2AllorsString {set;}

					global::System.Guid? I2AllorsUnique {set;}

					C1 I2C1Many2Manies {set;}

					C1 I2C1Many2One {set;}

					C1 I2C1One2Manies {set;}

					C1 I2C1One2One {set;}

					C2 I2C2Many2Manies {set;}

					C2 I2C2Many2One {set;}

					C2 I2C2One2Manies {set;}

					C2 I2C2One2One {set;}

					I12 I2I12Many2Manies {set;}

					I12 I2I12Many2One {set;}

					I12 I2I12One2Manies {set;}

					I12 I2I12One2One {set;}

					I1 I2I1Many2Manies {set;}

					I1 I2I1Many2One {set;}

					I1 I2I1One2Manies {set;}

					I1 I2I1One2One {set;}

					I2 I2I2Many2Manies {set;}

					I2 I2I2Many2One {set;}

					I2 I2I2One2Manies {set;}

					I2 I2I2One2One {set;}

	}
	public interface Localised  : Object 
	{
					Locale Locale {set;}

	}
	public interface ObjectState  : Object, UniquelyIdentifiable 
	{
					Permission DeniedPermissions {set;}

					global::System.String Name {set;}

	}
	public interface Period  : Object 
	{
					global::System.DateTime FromDate {set;}

					global::System.DateTime? ThroughDate {set;}

	}
	public interface Printable  : Object, UserInterfaceable, UniquelyIdentifiable 
	{
					global::System.String PrintContent {set;}

	}
	public interface Searchable  : Object 
	{
					SearchData SearchData {set;}

	}
	public interface SearchResult  : Object, UserInterfaceable 
	{
	}
	public interface SecurityTokenOwner  : Object 
	{
					SecurityToken OwnerSecurityToken {set;}

	}
	public interface Shared  : Object, UserInterfaceable 
	{
	}
	public interface Transitional  : Object, AccessControlledObject 
	{
	}
	public interface User  : Object, SecurityTokenOwner, UserInterfaceable, Localised 
	{
					global::System.String UserEmail {set;}

					global::System.Boolean? UserEmailConfirmed {set;}

					global::System.String UserName {set;}

					global::System.String UserPasswordHash {set;}

	}
	public interface StringTemplate  : Object, UniquelyIdentifiable, Localised 
	{
					global::System.String Body {set;}

					global::System.String Name {set;}

	}
	public interface AccessControl  : Object, Deletable, UserInterfaceable 
	{
					SecurityToken Objects {set;}

					Role Role {set;}

					User Subjects {set;}

					UserGroup SubjectGroups {set;}

	}
	public interface BadUI  : Object, UserInterfaceable 
	{
					global::System.String AllorsString {set;}

					Organisation CompanyMany {set;}

					Organisation CompanyOne {set;}

					Person PersonsMany {set;}

					Person PersonOne {set;}

	}
	public interface C1  : Object, UserInterfaceable, I1, AccessControlledObject, Searchable 
	{
					global::System.Byte[] C1AllorsBinary {set;}

					global::System.Boolean? C1AllorsBoolean {set;}

					global::System.DateTime? C1AllorsDateTime {set;}

					global::System.Decimal? C1AllorsDecimal {set;}

					global::System.Double? C1AllorsFloat {set;}

					global::System.Int32? C1AllorsInteger {set;}

					global::System.String C1AllorsString {set;}

					global::System.String AllorsStringMax {set;}

					global::System.Guid? C1AllorsUnique {set;}

					C1 C1C1Many2Manies {set;}

					C1 C1C1Many2One {set;}

					C1 C1C1One2Manies {set;}

					C1 C1C1One2One {set;}

					C2 C1C2Many2Manies {set;}

					C2 C1C2Many2One {set;}

					C2 C1C2One2Manies {set;}

					C2 C1C2One2One {set;}

					I12 C1I12Many2Manies {set;}

					I12 C1I12Many2One {set;}

					I12 C1I12One2Manies {set;}

					I12 C1I12One2One {set;}

					I1 C1I1Many2Manies {set;}

					I1 C1I1Many2One {set;}

					I1 C1I1One2Manies {set;}

					I1 C1I1One2One {set;}

					I2 C1I2Many2Manies {set;}

					I2 C1I2Many2One {set;}

					I2 C1I2One2Manies {set;}

					I2 C1I2One2One {set;}

	}
	public interface C2  : Object, Searchable, I2 
	{
					global::System.Byte[] C2AllorsBinary {set;}

					global::System.Boolean? C2AllorsBoolean {set;}

					global::System.DateTime? C2AllorsDateTime {set;}

					global::System.Decimal? C2AllorsDecimal {set;}

					global::System.Double? C2AllorsFloat {set;}

					global::System.Int32? C2AllorsInteger {set;}

					global::System.String C2AllorsString {set;}

					global::System.Guid? C2AllorsUnique {set;}

					C1 C2C1Many2Manies {set;}

					C1 C2C1Many2One {set;}

					C1 C2C1One2Manies {set;}

					C1 C2C1One2One {set;}

					C2 C2C2Many2Manies {set;}

					C2 C2C2Many2One {set;}

					C2 C2C2One2Manies {set;}

					C2 C2C2One2One {set;}

					I12 C2I12Many2Manies {set;}

					I12 C2I12Many2One {set;}

					I12 C2I12One2Manies {set;}

					I12 C2I12One2One {set;}

					I1 C2I1Many2Manies {set;}

					I1 C2I1Many2One {set;}

					I1 C2I1One2Manies {set;}

					I1 C2I1One2One {set;}

					I2 C2I2Many2Manies {set;}

					I2 C2I2Many2One {set;}

					I2 C2I2One2Manies {set;}

					I2 C2I2One2One {set;}

	}
	public interface ClassWithoutRoles  : Object, UserInterfaceable 
	{
	}
	public interface Counter  : Object, UniquelyIdentifiable 
	{
					global::System.Int32 Value {set;}

	}
	public interface Country  : Object, UserInterfaceable, Searchable 
	{
					Currency Currency {set;}

					global::System.String IsoCode {set;}

					LocalisedText LocalisedNames {set;}

					global::System.String Name {set;}

	}
	public interface Currency  : Object, UserInterfaceable 
	{
					global::System.String IsoCode {set;}

					LocalisedText LocalisedNames {set;}

					global::System.String Name {set;}

					global::System.String Symbol {set;}

	}
	public interface Dependee  : Object 
	{
					global::System.Int32? Counter {set;}

					global::System.Boolean? DeleteDependent {set;}

					global::System.Int32? Subcounter {set;}

					Subdependee Subdependee {set;}

	}
	public interface Dependent  : Object 
	{
					global::System.Int32? Counter {set;}

					Dependee Dependee {set;}

					global::System.Int32? Subcounter {set;}

	}
	public interface DerivationLogC1  : Object, DerivationLogI12 
	{
	}
	public interface DerivationLogC2  : Object, DerivationLogI12 
	{
	}
	public interface Extender  : Object 
	{
					global::System.String AllorsString {set;}

	}
	public interface First  : Object 
	{
					global::System.Boolean? CreateCycle {set;}

					global::System.Boolean? IsDerived {set;}

					Second Second {set;}

	}
	public interface Four  : Object, Shared, UserInterfaceable 
	{
	}
	public interface From  : Object, UserInterfaceable 
	{
					To Tos {set;}

	}
	public interface Gender  : Object, Enumeration 
	{
	}
	public interface HomeAddress  : Object, Searchable, Address 
	{
					global::System.String HouseNumber {set;}

					global::System.String Street {set;}

	}
	public interface Image  : Object, Deletable, Derivable 
	{
					Media Original {set;}

					global::System.String OriginalFilename {set;}

					Media Responsive {set;}

	}
	public interface Language  : Object, UserInterfaceable, Searchable 
	{
					global::System.String IsoCode {set;}

					LocalisedText LocalisedNames {set;}

					global::System.String Name {set;}

	}
	public interface Locale  : Object, UserInterfaceable 
	{
					Country Country {set;}

					Language Language {set;}

					global::System.String Name {set;}

	}
	public interface LocalisedText  : Object, Searchable, UserInterfaceable, Localised 
	{
					global::System.String Text {set;}

	}
	public interface Login  : Object, Derivable, Deletable 
	{
					global::System.String Key {set;}

					global::System.String Provider {set;}

					User User {set;}

	}
	public interface MailboxAddress  : Object, Searchable, Address 
	{
					global::System.String PoBox {set;}

	}
	public interface Media  : Object, UniquelyIdentifiable, UserInterfaceable, Deletable 
	{
					MediaContent MediaContent {set;}

					MediaType MediaType {set;}

	}
	public interface MediaContent  : Object, Derivable 
	{
					global::System.String Hash {set;}

					global::System.Byte[] Value {set;}

	}
	public interface MediaType  : Object, UserInterfaceable 
	{
					global::System.String DefaultFileExtension {set;}

					global::System.String Name {set;}

	}
	public interface One  : Object, Shared, UserInterfaceable 
	{
					Two Two {set;}

	}
	public interface Organisation  : Object, Searchable, UniquelyIdentifiable, UserInterfaceable, SearchResult 
	{
					Address Addresses {set;}

					Person Employees {set;}

					Person Owner {set;}

					Person Shareholders {set;}

					global::System.String Description {set;}

					global::System.Boolean? Incorporated {set;}

					global::System.DateTime? IncorporationDate {set;}

					global::System.Boolean? IsSupplier {set;}

					Media Logo {set;}

					global::System.String Name {set;}

					global::System.String Size {set;}

	}
	public interface Permission  : Object, Deletable, UserInterfaceable 
	{
					global::System.Guid ConcreteClassPointer {set;}

					global::System.Guid OperandTypePointer {set;}

					global::System.Int32 OperationEnum {set;}

	}
	public interface Person  : Object, User, AccessControlledObject, UniquelyIdentifiable, SearchResult, UserInterfaceable, Searchable, Printable 
	{
					Address Addresses {set;}

					global::System.Int32? Age {set;}

					global::System.DateTime? BirthDate {set;}

					global::System.String CKEditorText {set;}

					global::System.String FirstName {set;}

					global::System.String FullName {set;}

					Gender Gender {set;}

					global::System.Boolean? IsMarried {set;}

					global::System.Boolean? IsStudent {set;}

					global::System.String LastName {set;}

					MailboxAddress MailboxAddress {set;}

					Address MainAddress {set;}

					global::System.String MiddleName {set;}

					Media Photo {set;}

					global::System.Int32? ShirtSize {set;}

					global::System.String Text {set;}

					global::System.String TinyMCEText {set;}

					global::System.Decimal? Weight {set;}

	}
	public interface Place  : Object, UserInterfaceable, Searchable 
	{
					global::System.String City {set;}

					Country Country {set;}

					global::System.String PostalCode {set;}

	}
	public interface PrintQueue  : Object, AccessControlledObject, UserInterfaceable, UniquelyIdentifiable 
	{
					global::System.String Name {set;}

					Printable Printables {set;}

	}
	public interface Role  : Object, UserInterfaceable, UniquelyIdentifiable 
	{
					global::System.String Name {set;}

					Permission Permissions {set;}

	}
	public interface Search  : Object, Searchable, UserInterfaceable 
	{
					global::System.String Text {set;}

	}
	public interface SearchData  : Object, Derivable, Deletable 
	{
					global::System.String CharacterBoundaryText {set;}

					global::System.String PreviousCharacterBoundaryText {set;}

					global::System.String PreviousWordBoundaryText {set;}

					SearchFragment SearchFragments {set;}

					global::System.String WordBoundaryText {set;}

	}
	public interface SearchFragment  : Object, Derivable 
	{
					global::System.String LowerCaseText {set;}

	}
	public interface Second  : Object 
	{
					global::System.Boolean? IsDerived {set;}

					Third Third {set;}

	}
	public interface SecurityToken  : Object, Deletable, Derivable 
	{
	}
	public interface SimpleJob  : Object 
	{
					global::System.Int32? Index {set;}

	}
	public interface Singleton  : Object, UserInterfaceable 
	{
					SecurityToken AdministratorSecurityToken {set;}

					Locale DefaultLocale {set;}

					PrintQueue DefaultPrintQueue {set;}

					SecurityToken DefaultSecurityToken {set;}

					User Guest {set;}

					Locale Locales {set;}

					StringTemplate PersonTemplate {set;}

	}
	public interface StatefulCompany  : Object 
	{
					Person Employee {set;}

					Person Manager {set;}

					global::System.String Name {set;}

	}
	public interface Subdependee  : Object 
	{
					global::System.Int32? Subcounter {set;}

	}
	public interface Third  : Object 
	{
					global::System.Boolean? IsDerived {set;}

	}
	public interface Three  : Object, Shared, UserInterfaceable 
	{
					global::System.String AllorsString {set;}

					Four Four {set;}

	}
	public interface To  : Object, UserInterfaceable 
	{
					global::System.String Name {set;}

	}
	public interface Transition  : Object 
	{
					ObjectState FromStates {set;}

					ObjectState ToState {set;}

	}
	public interface Two  : Object, UserInterfaceable, Shared 
	{
					Shared Shared {set;}

	}
	public interface Unit  : Object, AccessControlledObject, UserInterfaceable 
	{
					global::System.Byte[] AllorsBinary {set;}

					global::System.Boolean? AllorsBoolean {set;}

					global::System.DateTime? AllorsDateTime {set;}

					global::System.Decimal? AllorsDecimal {set;}

					global::System.Double? AllorsFloat {set;}

					global::System.Int32? AllorsInteger {set;}

					global::System.String AllorsString {set;}

					global::System.Guid? AllorsUnique {set;}

	}
	public interface UserGroup  : Object, UniquelyIdentifiable, Searchable, UserInterfaceable 
	{
					User Members {set;}

					global::System.String Name {set;}

					UserGroup Parent {set;}

					Role Role {set;}

	}
}