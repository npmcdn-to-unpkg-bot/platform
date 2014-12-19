namespace Allors.Domain
{
	public interface UniquelyIdentifiable 
	{
					global::System.Guid UniqueId {set;}

	}
	public interface Printable  : UserInterfaceable, UniquelyIdentifiable 
	{
					global::System.String PrintContent {set;}

	}
	public interface Localised 
	{
					Locale Locale {set;}

	}
	public interface Period 
	{
					global::System.DateTime? FromDate {set;}

					global::System.DateTime? ThroughDate {set;}

	}
	public interface User  : SecurityTokenOwner, UserInterfaceable, Localised 
	{
					global::System.Boolean? UserEmailConfirmed {set;}

					global::System.String UserName {set;}

					global::System.String UserEmail {set;}

					global::System.String UserPasswordHash {set;}

	}
	public interface SearchResult  : UserInterfaceable 
	{
	}
	public interface SecurityTokenOwner 
	{
					SecurityToken OwnerSecurityToken {set;}

	}
	public interface Transitional  : AccessControlledObject 
	{
	}
	public interface Enumeration  : UserInterfaceable, UniquelyIdentifiable 
	{
					LocalisedText LocalisedNames {set;}

					global::System.String Name {set;}

					global::System.Boolean IsActive {set;}

	}
	public interface AccessControlledObject 
	{
					Permission DeniedPermissions {set;}

					SecurityToken SecurityTokens {set;}

	}
	public interface UserInterfaceable  : AccessControlledObject 
	{
					global::System.String DisplayName {set;}

	}
	public interface ObjectState  : UniquelyIdentifiable 
	{
					Permission DeniedPermissions {set;}

					global::System.String Name {set;}

	}
	public interface Commentable 
	{
					global::System.String Comment {set;}

	}
	public interface Searchable 
	{
					SearchData SearchData {set;}

	}
	public interface I1  : I12 
	{
					I1 I1I1Many2One {set;}

					I12 I1I12Many2Manies {set;}

					I2 I1I2Many2Manies {set;}

					I2 I1I2Many2One {set;}

					global::System.String I1AllorsString {set;}

					I12 I1I12Many2One {set;}

					I2 I1I2One2Manies {set;}

					C2 I1C2One2Manies {set;}

					C1 I1C1One2One {set;}

					global::System.Int32? I1AllorsInteger {set;}

					C2 I1C2Many2Manies {set;}

					I1 I1I1One2Manies {set;}

					I1 I1I1Many2Manies {set;}

					global::System.Boolean? I1AllorsBoolean {set;}

					global::System.Decimal? I1AllorsDecimal {set;}

					I12 I1I12One2One {set;}

					I2 I1I2One2One {set;}

					C2 I1C2One2One {set;}

					C1 I1C1One2Manies {set;}

					global::System.Byte[] I1AllorsBinary {set;}

					C1 I1C1Many2Manies {set;}

					global::System.Double? I1AllorsFloat {set;}

					I1 I1I1One2One {set;}

					C1 I1C1Many2One {set;}

					I12 I1I12One2Manies {set;}

					C2 I1C2Many2One {set;}

					global::System.Guid? I1AllorsUnique {set;}

	}
	public interface I2  : I12 
	{
					I2 I2I2Many2One {set;}

					C1 I2C1Many2One {set;}

					I12 I2I12Many2One {set;}

					global::System.Boolean? I2AllorsBoolean {set;}

					C1 I2C1One2Manies {set;}

					C1 I2C1One2One {set;}

					global::System.Decimal? I2AllorsDecimal {set;}

					I2 I2I2Many2Manies {set;}

					global::System.Byte[] I2AllorsBinary {set;}

					global::System.Guid? I2AllorsUnique {set;}

					I1 I2I1Many2One {set;}

					I12 I2I12One2Manies {set;}

					I12 I2I12One2One {set;}

					C2 I2C2Many2Manies {set;}

					I1 I2I1Many2Manies {set;}

					C2 I2C2Many2One {set;}

					global::System.String I2AllorsString {set;}

					C2 I2C2One2Manies {set;}

					I1 I2I1One2One {set;}

					I1 I2I1One2Manies {set;}

					I12 I2I12Many2Manies {set;}

					I2 I2I2One2One {set;}

					global::System.Int32? I2AllorsInteger {set;}

					I2 I2I2One2Manies {set;}

					C1 I2C1Many2Manies {set;}

					C2 I2C2One2One {set;}

					global::System.Double? I2AllorsFloat {set;}

	}
	public interface I12  : UserInterfaceable, AccessControlledObject, Searchable 
	{
					global::System.Byte[] I12AllorsBinary {set;}

					C2 I12C2One2One {set;}

					global::System.Double? I12AllorsFloat {set;}

					I1 I12I1Many2One {set;}

					global::System.String I12AllorsString {set;}

					I12 I12I12Many2Manies {set;}

					global::System.Decimal? I12AllorsDecimal {set;}

					I2 I12I2Many2Manies {set;}

					C2 I12C2Many2Manies {set;}

					I1 I12I1Many2Manies {set;}

					I12 I12I12One2Manies {set;}

					global::System.String Name {set;}

					C1 I12C1Many2Manies {set;}

					I2 I12I2Many2One {set;}

					global::System.Guid? I12AllorsUnique {set;}

					global::System.Int32? I12AllorsInteger {set;}

					I1 I12I1One2Manies {set;}

					C1 I12C1One2One {set;}

					I12 I12I12One2One {set;}

					I2 I12I2One2One {set;}

					I12 Dependencies {set;}

					I2 I12I2One2Manies {set;}

					C2 I12C2Many2One {set;}

					I12 I12I12Many2One {set;}

					global::System.Boolean? I12AllorsBoolean {set;}

					I1 I12I1One2One {set;}

					C1 I12C1One2Manies {set;}

					C1 I12C1Many2One {set;}

	}
	public interface DerivationLogI12 
	{
					global::System.Guid? UniqueId {set;}

	}
	public interface Address  : UserInterfaceable 
	{
					Place Place {set;}

	}
	public interface Shared  : UserInterfaceable 
	{
	}
	public interface LocalisedText  : Searchable, UserInterfaceable, Localised 
	{
					global::System.String Text {set;}

	}
	public interface Counter  : UniquelyIdentifiable 
	{
					global::System.Int32 Value {set;}

	}
	public interface StringTemplate  : UniquelyIdentifiable, Localised 
	{
					global::System.String Body {set;}

					global::System.String Name {set;}

	}
	public interface Singleton  : UserInterfaceable 
	{
					PrintQueue DefaultPrintQueue {set;}

					Locale DefaultLocale {set;}

					Locale Locales {set;}

					SecurityToken AdministratorSecurityToken {set;}

					User Guest {set;}

					SecurityToken DefaultSecurityToken {set;}

					StringTemplate PersonTemplate {set;}

	}
	public interface Locale  : UserInterfaceable 
	{
					global::System.String Name {set;}

					Language Language {set;}

					Country Country {set;}

	}
	public interface SearchFragment 
	{
					global::System.String LowerCaseText {set;}

	}
	public interface Language  : UserInterfaceable, Searchable 
	{
					global::System.String Name {set;}

					global::System.String IsoCode {set;}

					LocalisedText LocalisedNames {set;}

	}
	public interface SearchData 
	{
					global::System.String CharacterBoundaryText {set;}

					global::System.String PreviousCharacterBoundaryText {set;}

					SearchFragment SearchFragments {set;}

					global::System.String PreviousWordBoundaryText {set;}

					global::System.String WordBoundaryText {set;}

	}
	public interface UserGroup  : UniquelyIdentifiable, Searchable, UserInterfaceable 
	{
					Role Role {set;}

					User Members {set;}

					UserGroup Parent {set;}

					global::System.String Name {set;}

	}
	public interface MediaContent 
	{
					global::System.Byte[] Value {set;}

					global::System.String Hash {set;}

	}
	public interface Permission  : UserInterfaceable 
	{
					global::System.Guid OperandTypePointer {set;}

					global::System.Guid ConcreteClassPointer {set;}

					global::System.Int32 OperationEnum {set;}

	}
	public interface SecurityToken 
	{
	}
	public interface Transition 
	{
					ObjectState FromStates {set;}

					ObjectState ToState {set;}

	}
	public interface MediaType  : UserInterfaceable 
	{
					global::System.String DefaultFileExtension {set;}

					global::System.String Name {set;}

	}
	public interface Login 
	{
					global::System.String Key {set;}

					global::System.String Provider {set;}

					User User {set;}

	}
	public interface Role  : UserInterfaceable, UniquelyIdentifiable 
	{
					Permission Permissions {set;}

					global::System.String Name {set;}

	}
	public interface PrintQueue  : AccessControlledObject, UserInterfaceable, UniquelyIdentifiable 
	{
					Printable Printables {set;}

					global::System.String Name {set;}

	}
	public interface Country  : UserInterfaceable, Searchable 
	{
					Currency Currency {set;}

					global::System.String Name {set;}

					LocalisedText LocalisedNames {set;}

					global::System.String IsoCode {set;}

	}
	public interface AccessControl  : UserInterfaceable 
	{
					UserGroup SubjectGroups {set;}

					User Subjects {set;}

					SecurityToken Objects {set;}

					Role Role {set;}

	}
	public interface Person  : User, AccessControlledObject, UniquelyIdentifiable, SearchResult, UserInterfaceable, Searchable, Printable 
	{
					global::System.String LastName {set;}

					global::System.String MiddleName {set;}

					global::System.String FirstName {set;}

					Address MainAddress {set;}

					global::System.String TinyMCEText {set;}

					global::System.String Text {set;}

					global::System.Int32? Age {set;}

					global::System.Boolean? IsStudent {set;}

					MailboxAddress MailboxAddress {set;}

					Gender Gender {set;}

					global::System.String FullName {set;}

					global::System.Int32? ShirtSize {set;}

					global::System.String CKEditorText {set;}

					global::System.Boolean? IsMarried {set;}

					global::System.Decimal? Weight {set;}

					Media Photo {set;}

					Address Addresses {set;}

	}
	public interface Image 
	{
					Media Original {set;}

					Media Responsive {set;}

					global::System.String OriginalFilename {set;}

	}
	public interface Media  : UniquelyIdentifiable, UserInterfaceable 
	{
					MediaType MediaType {set;}

					MediaContent MediaContent {set;}

	}
	public interface Currency  : UserInterfaceable 
	{
					global::System.String IsoCode {set;}

					global::System.String Name {set;}

					global::System.String Symbol {set;}

					LocalisedText LocalisedNames {set;}

	}
	public interface C1  : I1 
	{
					I1 C1I1One2One {set;}

					global::System.String C1AllorsString {set;}

					C2 C1C2Many2One {set;}

					I2 C1I2One2One {set;}

					C1 C1C1One2One {set;}

					I1 C1I1Many2One {set;}

					I1 C1I1Many2Manies {set;}

					I2 C1I2One2Manies {set;}

					global::System.Decimal? C1AllorsDecimal {set;}

					C1 C1C1Many2Manies {set;}

					I12 C1I12Many2Manies {set;}

					global::System.Byte[] C1AllorsBinary {set;}

					I12 C1I12One2Manies {set;}

					C2 C1C2One2Manies {set;}

					C1 C1C1One2Manies {set;}

					global::System.String AllorsStringMax {set;}

					C1 C1C1Many2One {set;}

					global::System.Boolean? C1AllorsBoolean {set;}

					I12 C1I12One2One {set;}

					I12 C1I12Many2One {set;}

					I2 C1I2Many2Manies {set;}

					global::System.Guid? C1AllorsUnique {set;}

					I2 C1I2Many2One {set;}

					I1 C1I1One2Manies {set;}

					C2 C1C2One2One {set;}

					global::System.Double? C1AllorsFloat {set;}

					C2 C1C2Many2Manies {set;}

					global::System.Int32? C1AllorsInteger {set;}

	}
	public interface C2  : I2 
	{
					global::System.Decimal? C2AllorsDecimal {set;}

					C1 C2C1One2One {set;}

					C2 C2C2Many2One {set;}

					global::System.Guid? C2AllorsUnique {set;}

					I12 C2I12Many2One {set;}

					I12 C2I12One2One {set;}

					I1 C2I1Many2Manies {set;}

					global::System.Double? C2AllorsFloat {set;}

					I1 C2I1One2Manies {set;}

					I2 C2I2One2One {set;}

					global::System.Int32? C2AllorsInteger {set;}

					I2 C2I2Many2Manies {set;}

					I12 C2I12Many2Manies {set;}

					C2 C2C2One2Manies {set;}

					global::System.Boolean? C2AllorsBoolean {set;}

					I1 C2I1Many2One {set;}

					I1 C2I1One2One {set;}

					C1 C2C1Many2Manies {set;}

					I12 C2I12One2Manies {set;}

					I2 C2I2One2Manies {set;}

					C2 C2C2One2One {set;}

					global::System.String C2AllorsString {set;}

					C1 C2C1Many2One {set;}

					C2 C2C2Many2Manies {set;}

					I2 C2I2Many2One {set;}

					C1 C2C1One2Manies {set;}

					global::System.Byte[] C2AllorsBinary {set;}

	}
	public interface DerivationLogC1  : DerivationLogI12 
	{
	}
	public interface DerivationLogC2  : DerivationLogI12 
	{
	}
	public interface Dependent 
	{
					Dependee Dependee {set;}

					global::System.Int32? Counter {set;}

					global::System.Int32? Subcounter {set;}

	}
	public interface Four  : Shared, UserInterfaceable 
	{
	}
	public interface First 
	{
					Second Second {set;}

					global::System.Boolean? CreateCycle {set;}

					global::System.Boolean? IsDerived {set;}

	}
	public interface HomeAddress  : Searchable, Address 
	{
					global::System.String Street {set;}

					global::System.String HouseNumber {set;}

	}
	public interface Place  : UserInterfaceable, Searchable 
	{
					Country Country {set;}

					global::System.String City {set;}

					global::System.String PostalCode {set;}

	}
	public interface Gender  : Enumeration 
	{
	}
	public interface Dependee 
	{
					Subdependee Subdependee {set;}

					global::System.Int32? Subcounter {set;}

					global::System.Int32? Counter {set;}

					global::System.Boolean? DeleteDependent {set;}

	}
	public interface Third 
	{
					global::System.Boolean? IsDerived {set;}

	}
	public interface Organisation  : Searchable, UniquelyIdentifiable, UserInterfaceable, SearchResult 
	{
					Person Shareholders {set;}

					global::System.String Name {set;}

					global::System.String Description {set;}

					Person Employees {set;}

					global::System.Boolean? Incorporated {set;}

					global::System.Boolean? IsSupplier {set;}

					Address Addresses {set;}

					Person Owner {set;}

					Media Logo {set;}

					global::System.String Size {set;}

	}
	public interface Subdependee 
	{
					global::System.Int32? Subcounter {set;}

	}
	public interface Unit  : AccessControlledObject, UserInterfaceable 
	{
					global::System.Byte[] AllorsBinary {set;}

					global::System.Boolean? AllorsBoolean {set;}

					global::System.Double? AllorsFloat {set;}

					global::System.Int32? AllorsInteger {set;}

					global::System.String AllorsString {set;}

					global::System.Guid? AllorsUnique {set;}

					global::System.Decimal? AllorsDecimal {set;}

	}
	public interface One  : Shared, UserInterfaceable 
	{
					Two Two {set;}

	}
	public interface From  : UserInterfaceable 
	{
					To Tos {set;}

	}
	public interface StatefulCompany 
	{
					Person Employee {set;}

					global::System.String Name {set;}

					Person Manager {set;}

	}
	public interface To  : UserInterfaceable 
	{
					global::System.String Name {set;}

	}
	public interface MailboxAddress  : Searchable, Address 
	{
					global::System.String PoBox {set;}

	}
	public interface Extender 
	{
					global::System.String AllorsString {set;}

	}
	public interface Two  : UserInterfaceable, Shared 
	{
					Shared Shared {set;}

	}
	public interface SearchTest  : Searchable, UserInterfaceable 
	{
					global::System.String Text {set;}

	}
	public interface BadUI  : UserInterfaceable 
	{
					Person PersonsMany {set;}

					Organisation CompanyOne {set;}

					Person PersonOne {set;}

					Organisation CompanyMany {set;}

					global::System.String AllorsString {set;}

	}
	public interface Three  : Shared, UserInterfaceable 
	{
					Four Four {set;}

					global::System.String AllorsString {set;}

	}
	public interface Second 
	{
					Third Third {set;}

					global::System.Boolean? IsDerived {set;}

	}
	public interface ClassWithoutRoles  : UserInterfaceable 
	{
	}
}