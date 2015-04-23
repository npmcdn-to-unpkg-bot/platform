namespace Allors.Domain
{
	public interface Object 
	{
	}
	public interface AccessControlledObject  : Object 
	{
					Permission DeniedPermissions {set;}

					SecurityToken SecurityTokens {set;}

	}
	public interface Commentable  : Object 
	{
					global::System.String Comment {set;}

	}
	public interface Deletable  : Object 
	{
	}
	public interface Enumeration  : Object, AccessControlledObject, UniquelyIdentifiable 
	{
					LocalisedText LocalisedNames {set;}

					global::System.String Name {set;}

					global::System.Boolean IsActive {set;}

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
	public interface Printable  : Object, AccessControlledObject, UniquelyIdentifiable 
	{
					global::System.String PrintContent {set;}

	}
	public interface SecurityTokenOwner  : Object 
	{
					SecurityToken OwnerSecurityToken {set;}

	}
	public interface Transitional  : Object, AccessControlledObject 
	{
	}
	public interface UniquelyIdentifiable  : Object 
	{
					global::System.Guid UniqueId {set;}

	}
    public interface User : Object, SecurityTokenOwner, Localised 
	{
					global::System.Boolean? UserEmailConfirmed {set;}

					global::System.String UserName {set;}

					global::System.String UserEmail {set;}

					global::System.String UserPasswordHash {set;}

	}
	public interface Address  : Object 
	{
					Place Place {set;}

	}
	public interface DerivationLogI12  : Object 
	{
					global::System.Guid? UniqueId {set;}

	}
	public interface I1  : Object, I12, S1 
	{
					I1 I1I1Many2One {set;}

					I12 I1I12Many2Manies {set;}

					I2 I1I2Many2Manies {set;}

					I2 I1I2Many2One {set;}

					global::System.String I1AllorsString {set;}

					I12 I1I12Many2One {set;}

					global::System.DateTime? I1AllorsDateTime {set;}

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
	public interface I12  : Object 
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

					global::System.DateTime? I12AllorsDateTime {set;}

	}
	public interface I2  : Object, I12 
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

					global::System.DateTime? I2AllorsDateTime {set;}

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
	public interface S1  : Object 
	{
	}
	public interface Shared  : Object 
	{
	}
    public interface AccessControl : Object, Deletable 
	{
					UserGroup SubjectGroups {set;}

					User Subjects {set;}

					SecurityToken Objects {set;}

					Role Role {set;}

	}
	public interface Counter  : Object, UniquelyIdentifiable 
	{
					global::System.Int32 Value {set;}

	}
	public interface Country  : Object, AccessControlledObject 
	{
					Currency Currency {set;}

					global::System.String Name {set;}

					LocalisedText LocalisedNames {set;}

					global::System.String IsoCode {set;}

	}
	public interface Currency  : Object, AccessControlledObject 
	{
					global::System.String IsoCode {set;}

					global::System.String Name {set;}

					global::System.String Symbol {set;}

					LocalisedText LocalisedNames {set;}

	}
	public interface Image  : Object, Deletable 
	{
					Media Original {set;}

					Media Responsive {set;}

					global::System.String OriginalFilename {set;}

	}
	public interface Language  : Object, AccessControlledObject 
	{
					global::System.String Name {set;}

					global::System.String IsoCode {set;}

					LocalisedText LocalisedNames {set;}

	}
	public interface Locale  : Object, AccessControlledObject 
	{
					global::System.String Name {set;}

					Language Language {set;}

					Country Country {set;}

	}
	public interface LocalisedText  : Object, AccessControlledObject, Localised 
	{
					global::System.String Text {set;}

	}
	public interface Login  : Object, Deletable 
	{
					global::System.String Key {set;}

					global::System.String Provider {set;}

					User User {set;}

	}
	public interface Media  : Object, UniquelyIdentifiable, AccessControlledObject, Deletable 
	{
					MediaType MediaType {set;}

					MediaContent MediaContent {set;}

	}
	public interface MediaContent  : Object 
	{
					global::System.Byte[] Value {set;}

					global::System.String Hash {set;}

	}
	public interface MediaType  : Object, AccessControlledObject 
	{
					global::System.String DefaultFileExtension {set;}

					global::System.String Name {set;}

	}
    public interface Permission : Object, Deletable 
	{
					global::System.Guid OperandTypePointer {set;}

					global::System.Guid ConcreteClassPointer {set;}

					global::System.Int32 OperationEnum {set;}

	}
	public interface Person  : Object, User, AccessControlledObject, UniquelyIdentifiable, Printable, Deletable 
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

					global::System.DateTime? BirthDate {set;}

					global::System.Decimal? Weight {set;}

					Media Photo {set;}

					Address Addresses {set;}

	}
	public interface PrintQueue  : Object, AccessControlledObject, UniquelyIdentifiable 
	{
					Printable Printables {set;}

					global::System.String Name {set;}

	}
    public interface Role : Object, UniquelyIdentifiable 
	{
					Permission Permissions {set;}

					global::System.String Name {set;}

	}
	public interface SecurityToken  : Object, Deletable 
	{
	}
	public interface Singleton  : Object, AccessControlledObject 
	{
					PrintQueue DefaultPrintQueue {set;}

					Locale DefaultLocale {set;}

					Locale Locales {set;}

					SecurityToken AdministratorSecurityToken {set;}

					User Guest {set;}

					SecurityToken DefaultSecurityToken {set;}

					StringTemplate PersonTemplate {set;}

	}
	public interface StringTemplate  : Object, UniquelyIdentifiable, Localised 
	{
					global::System.String Body {set;}

					global::System.String Name {set;}

	}
	public interface Transition  : Object 
	{
					ObjectState FromStates {set;}

					ObjectState ToState {set;}

	}
    public interface UserGroup : Object, UniquelyIdentifiable 
	{
					Role Role {set;}

					User Members {set;}

					UserGroup Parent {set;}

					global::System.String Name {set;}

	}
	public interface BadUI  : Object 
	{
					Person PersonsMany {set;}

					Organisation CompanyOne {set;}

					Person PersonOne {set;}

					Organisation CompanyMany {set;}

					global::System.String AllorsString {set;}

	}
	public interface C1  : Object, I1, AccessControlledObject 
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

					global::System.DateTime? C1AllorsDateTime {set;}

					global::System.Double? C1AllorsFloat {set;}

					C2 C1C2Many2Manies {set;}

					global::System.Int32? C1AllorsInteger {set;}

	}
	public interface C2  : Object, I2 
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

					global::System.DateTime? C2AllorsDateTime {set;}

					I2 C2I2Many2One {set;}

					C1 C2C1One2Manies {set;}

					global::System.Byte[] C2AllorsBinary {set;}

	}
	public interface ClassWithoutRoles  : Object 
	{
	}
	public interface Dependee  : Object 
	{
					Subdependee Subdependee {set;}

					global::System.Int32? Subcounter {set;}

					global::System.Int32? Counter {set;}

					global::System.Boolean? DeleteDependent {set;}

	}
	public interface Dependent  : Object, Deletable 
	{
					Dependee Dependee {set;}

					global::System.Int32? Counter {set;}

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
					Second Second {set;}

					global::System.Boolean? CreateCycle {set;}

					global::System.Boolean? IsDerived {set;}

	}
	public interface Four  : Object, Shared 
	{
	}
	public interface From  : Object 
	{
					To Tos {set;}

	}
	public interface Gender  : Object, Enumeration 
	{
	}
	public interface HomeAddress  : Object, Address 
	{
					global::System.String Street {set;}

					global::System.String HouseNumber {set;}

	}
	public interface MailboxAddress  : Object, Address 
	{
					global::System.String PoBox {set;}

	}
	public interface One  : Object, Shared 
	{
					Two Two {set;}

	}
	public interface Organisation  : Object, UniquelyIdentifiable, AccessControlledObject 
	{
					Person Shareholders {set;}

					global::System.String Name {set;}

					global::System.String Description {set;}

					Person Employees {set;}

					global::System.Boolean? Incorporated {set;}

					global::System.Boolean? IsSupplier {set;}

					global::System.DateTime? IncorporationDate {set;}

					Address Addresses {set;}

					Person Owner {set;}

					Media Logo {set;}

					global::System.String Size {set;}

	}
	public interface Place  : Object 
	{
					Country Country {set;}

					global::System.String City {set;}

					global::System.String PostalCode {set;}

	}
	public interface Second  : Object 
	{
					Third Third {set;}

					global::System.Boolean? IsDerived {set;}

	}
	public interface SimpleJob  : Object 
	{
					global::System.Int32? Index {set;}

	}
	public interface StatefulCompany  : Object 
	{
					Person Employee {set;}

					global::System.String Name {set;}

					Person Manager {set;}

	}
	public interface Subdependee  : Object 
	{
					global::System.Int32? Subcounter {set;}

	}
	public interface Third  : Object 
	{
					global::System.Boolean? IsDerived {set;}

	}
	public interface Three  : Object, Shared 
	{
					Four Four {set;}

					global::System.String AllorsString {set;}

	}
	public interface To  : Object 
	{
					global::System.String Name {set;}

	}
	public interface Two  : Object, Shared 
	{
					Shared Shared {set;}

	}
	public interface Unit  : Object, AccessControlledObject 
	{
					global::System.Byte[] AllorsBinary {set;}

					global::System.DateTime? AllorsDateTime {set;}

					global::System.Boolean? AllorsBoolean {set;}

					global::System.Double? AllorsFloat {set;}

					global::System.Int32? AllorsInteger {set;}

					global::System.String AllorsString {set;}

					global::System.Guid? AllorsUnique {set;}

					global::System.Decimal? AllorsDecimal {set;}

	}
}