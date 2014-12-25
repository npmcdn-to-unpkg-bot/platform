namespace Allors.Domain
{
	public interface PartSpecification  : UniquelyIdentifiable, Commentable, Transitional, UserInterfaceable 
	{
					PartSpecificationStatus PartSpecificationStatuses {set;}

					PartSpecificationObjectState CurrentObjectState {set;}

					global::System.DateTime? DocumentationDate {set;}

					PartSpecificationStatus CurrentPartSpecificationStatus {set;}

					PartSpecificationObjectState PreviousObjectState {set;}

					global::System.String Description {set;}

	}
	public interface Quote  : UserInterfaceable, SearchResult, Searchable 
	{
					global::System.DateTime? ValidFromDate {set;}

					QuoteTerm QuoteTerms {set;}

					Party Issuer {set;}

					global::System.DateTime? ValidThroughDate {set;}

					global::System.String Description {set;}

					Party Receiver {set;}

					global::System.Decimal? Amount {set;}

					global::System.DateTime? IssueDate {set;}

					QuoteItem QuoteItems {set;}

					global::System.String QuoteNumber {set;}

	}
	public interface PartyRelationship  : Period, Commentable, UserInterfaceable 
	{
					PartyRelationshipStatus PartyRelationshipStatus {set;}

					Agreement RelationshipAgreements {set;}

					PartyRelationshipPriority PartyRelationshipPriority {set;}

					global::System.Decimal? SimpleMovingAverage {set;}

					CommunicationEvent CommunicationEvents {set;}

	}
	public interface Service  : Product 
	{
	}
	public interface Document  : Printable, UserInterfaceable, Commentable, Searchable, SearchResult 
	{
					global::System.String Name {set;}

					global::System.String Description {set;}

					global::System.String Text {set;}

					global::System.String DocumentLocation {set;}

	}
	public interface FinancialAccount  : UserInterfaceable, SearchResult, Searchable 
	{
					FinancialAccountTransaction FinancialAccountTransactions {set;}

	}
	public interface Request  : UserInterfaceable, Searchable, SearchResult, Commentable 
	{
					global::System.String Description {set;}

					global::System.DateTime? RequiredResponseDate {set;}

					RequestItem RequestItems {set;}

					global::System.String RequestNumber {set;}

					RespondingParty RespondingParties {set;}

					Party Originator {set;}

	}
	public interface GeographicBoundary  : Searchable, GeoLocatable, UserInterfaceable 
	{
					global::System.String Abbreviation {set;}

	}
	public interface PriceComponent  : Period, Searchable, UserInterfaceable, Commentable 
	{
					GeographicBoundary GeographicBoundary {set;}

					global::System.Decimal? Rate {set;}

					RevenueValueBreak RevenueValueBreak {set;}

					PartyClassification PartyClassification {set;}

					OrderQuantityBreak OrderQuantityBreak {set;}

					PackageQuantityBreak PackageQuantityBreak {set;}

					Product Product {set;}

					RevenueQuantityBreak RevenueQuantityBreak {set;}

					Party SpecifiedFor {set;}

					ProductFeature ProductFeature {set;}

					AgreementPricingProgram AgreementPricingProgram {set;}

					global::System.String Description {set;}

					Currency Currency {set;}

					OrderKind OrderKind {set;}

					OrderValue OrderValue {set;}

					global::System.Decimal Price {set;}

					ProductCategory ProductCategory {set;}

					SalesChannel SalesChannel {set;}

	}
	public interface GeographicBoundaryComposite  : GeographicBoundary 
	{
					GeographicBoundary Associations {set;}

	}
	public interface Party  : Localised, UserInterfaceable, SearchResult, SecurityTokenOwner, UniquelyIdentifiable, Searchable 
	{
					global::System.Decimal YTDRevenue {set;}

					global::System.Decimal LastYearsRevenue {set;}

					TelecommunicationsNumber BillingInquiriesFax {set;}

					Qualification Qualifications {set;}

					ContactMechanism HomeAddress {set;}

					ContactMechanism SalesOffice {set;}

					TelecommunicationsNumber OrderInquiriesFax {set;}

					Person CurrentSalesReps {set;}

					PartyContactMechanism PartyContactMechanisms {set;}

					TelecommunicationsNumber ShippingInquiriesFax {set;}

					TelecommunicationsNumber ShippingInquiriesPhone {set;}

					BillingAccount BillingAccounts {set;}

					TelecommunicationsNumber OrderInquiriesPhone {set;}

					PartySkill PartySkills {set;}

					PartyClassification PartyClassifications {set;}

					global::System.Boolean? ExcludeFromDunning {set;}

					BankAccount BankAccounts {set;}

					ContactMechanism BillingAddress {set;}

					ShipmentMethod DefaultShipmentMethod {set;}

					Resume Resumes {set;}

					ContactMechanism HeadQuarter {set;}

					ElectronicAddress PersonalEmailAddress {set;}

					TelecommunicationsNumber CellPhoneNumber {set;}

					TelecommunicationsNumber BillingInquiriesPhone {set;}

					global::System.String PartyName {set;}

					ContactMechanism OrderAddress {set;}

					ElectronicAddress InternetAddress {set;}

					Media Contents {set;}

					CreditCard CreditCards {set;}

					PostalAddress ShippingAddress {set;}

					global::System.Decimal OpenOrderAmount {set;}

					TelecommunicationsNumber GeneralFaxNumber {set;}

					PaymentMethod DefaultPaymentMethod {set;}

					TelecommunicationsNumber GeneralPhoneNumber {set;}

					Currency PreferredCurrency {set;}

					VatRegime VatRegime {set;}

	}
	public interface CommunicationAttachment  : UserInterfaceable 
	{
	}
	public interface FixedAsset  : UserInterfaceable, SearchResult, Searchable 
	{
					global::System.String Name {set;}

					global::System.DateTime? LastServiceDate {set;}

					global::System.DateTime? AcquiredDate {set;}

					global::System.String Description {set;}

					global::System.Decimal? ProductionCapacity {set;}

					global::System.DateTime? NextServiceDate {set;}

	}
	public interface ServiceEntry  : Commentable, UserInterfaceable, Searchable, SearchResult 
	{
					global::System.DateTime? ThroughDateTime {set;}

					EngagementItem EngagementItem {set;}

					global::System.Boolean? IsBillable {set;}

					global::System.DateTime? FromDateTime {set;}

					global::System.String Description {set;}

					WorkEffort WorkEffort {set;}

	}
	public interface Agreement  : UserInterfaceable, Searchable, SearchResult, UniquelyIdentifiable, Period 
	{
					global::System.DateTime? AgreementDate {set;}

					Addendum Addenda {set;}

					global::System.String Description {set;}

					AgreementTerm AgreementTerms {set;}

					global::System.String Text {set;}

					AgreementItem AgreementItems {set;}

					global::System.String AgreementNumber {set;}

	}
	public interface FinancialAccountTransaction  : UserInterfaceable 
	{
					global::System.String Description {set;}

					global::System.DateTime? EntryDate {set;}

					global::System.DateTime? TransactionDate {set;}

	}
	public interface WorkEffort  : Searchable, UserInterfaceable, SearchResult, Transitional, UniquelyIdentifiable 
	{
					WorkEffortStatus CurrentWorkEffortStatus {set;}

					WorkEffort Precendencies {set;}

					Facility Facility {set;}

					Deliverable DeliverablesProduced {set;}

					WorkEffortInventoryAssignment InventoryItemsNeeded {set;}

					WorkEffort Children {set;}

					OrderItem OrderItemFulfillment {set;}

					WorkEffortStatus WorkEffortStatuses {set;}

					WorkEffortType WorkEffortType {set;}

					InventoryItem InventoryItemsProduced {set;}

					Requirement RequirementFulfillments {set;}

					global::System.String SpecialTerms {set;}

					WorkEffort Concurrencies {set;}

					global::System.Decimal? ActualHours {set;}

					global::System.String Description {set;}

					WorkEffortObjectState PreviousObjectState {set;}

					WorkEffortObjectState CurrentObjectState {set;}

					global::System.Decimal? EstimatedHours {set;}

	}
	public interface Product  : SearchResult, UniquelyIdentifiable, UserInterfaceable, Searchable 
	{
					ProductCategory PrimaryProductCategory {set;}

					global::System.DateTime? SupportDiscontinuationDate {set;}

					global::System.DateTime? SalesDiscontinuationDate {set;}

					global::System.String Description {set;}

					PriceComponent VirtualProductPriceComponents {set;}

					global::System.String IntrastatCode {set;}

					ProductCategory ProductCategoriesExpanded {set;}

					Product ProductComplement {set;}

					ProductFeature OptionalFeatures {set;}

					Party ManufacturedBy {set;}

					Product Variants {set;}

					global::System.String Name {set;}

					global::System.DateTime? IntroductionDate {set;}

					Document Documents {set;}

					ProductFeature StandardFeatures {set;}

					UnitOfMeasure UnitOfMeasure {set;}

					EstimatedProductCost EstimatedProductCosts {set;}

					Product ProductObsolescences {set;}

					ProductFeature SelectableFeatures {set;}

					VatRate VatRate {set;}

					PriceComponent BasePrices {set;}

					ProductCategory ProductCategories {set;}

					InternalOrganisation SoldBy {set;}

	}
	public interface InternalAccountingTransaction  : AccountingTransaction 
	{
					InternalOrganisation InternalOrganisation {set;}

	}
	public interface ElectronicAddress  : ContactMechanism 
	{
					global::System.String ElectronicAddressString {set;}

	}
	public interface InventoryItem  : Transitional, Searchable, UniquelyIdentifiable, UserInterfaceable 
	{
					InventoryItemVariance InventoryItemVariances {set;}

					Part Part {set;}

					Container Container {set;}

					global::System.String Name {set;}

					Lot Lot {set;}

					global::System.String Sku {set;}

					UnitOfMeasure UnitOfMeasure {set;}

					ProductCategory DerivedProductCategories {set;}

					Good Good {set;}

					Facility Facility {set;}

	}
	public interface ExternalAccountingTransaction  : AccountingTransaction 
	{
					Party FromParty {set;}

					Party ToParty {set;}

	}
	public interface AgreementTerm  : UserInterfaceable 
	{
					global::System.String TermValue {set;}

					TermType TermType {set;}

					global::System.String Description {set;}

	}
	public interface Part  : UserInterfaceable, Searchable, UniquelyIdentifiable, SearchResult 
	{
					InternalOrganisation OwnedByParty {set;}

					global::System.String Name {set;}

					PartSpecification PartSpecifications {set;}

					UnitOfMeasure UnitOfMeasure {set;}

					Document Documents {set;}

					global::System.String ManufacturerId {set;}

					global::System.Int32? ReorderLevel {set;}

					global::System.Int32? ReorderQuantity {set;}

					PriceComponent PriceComponents {set;}

					InventoryItemKind InventoryItemKind {set;}

	}
	public interface AccountingTransaction  : UserInterfaceable, SearchResult, Searchable 
	{
					AccountingTransactionDetail AccountingTransactionDetails {set;}

					global::System.String Description {set;}

					global::System.DateTime TransactionDate {set;}

					global::System.Decimal DerivedTotalAmount {set;}

					AccountingTransactionNumber AccountingTransactionNumber {set;}

					global::System.DateTime EntryDate {set;}

	}
	public interface Order  : UserInterfaceable, Printable, UniquelyIdentifiable, Transitional, Searchable, Commentable, Localised, SearchResult 
	{
					Currency CustomerCurrency {set;}

					global::System.Decimal TotalBasePriceCustomerCurrency {set;}

					global::System.Decimal TotalIncVatCustomerCurrency {set;}

					global::System.Decimal TotalDiscountCustomerCurrency {set;}

					global::System.String CustomerReference {set;}

					Fee Fee {set;}

					global::System.Decimal TotalExVat {set;}

					OrderTerm OrderTerms {set;}

					global::System.Decimal TotalVat {set;}

					global::System.Decimal TotalSurcharge {set;}

					OrderItem ValidOrderItems {set;}

					global::System.String OrderNumber {set;}

					global::System.Decimal TotalVatCustomerCurrency {set;}

					global::System.Decimal TotalDiscount {set;}

					global::System.String Message {set;}

					global::System.Decimal TotalShippingAndHandlingCustomerCurrency {set;}

					global::System.DateTime EntryDate {set;}

					DiscountAdjustment DiscountAdjustment {set;}

					OrderKind OrderKind {set;}

					global::System.Decimal TotalIncVat {set;}

					global::System.Decimal TotalSurchargeCustomerCurrency {set;}

					VatRegime VatRegime {set;}

					global::System.Decimal TotalFeeCustomerCurrency {set;}

					global::System.Decimal TotalShippingAndHandling {set;}

					ShippingAndHandlingCharge ShippingAndHandlingCharge {set;}

					global::System.DateTime OrderDate {set;}

					global::System.Decimal TotalExVatCustomerCurrency {set;}

					global::System.DateTime? DeliveryDate {set;}

					global::System.Decimal TotalBasePrice {set;}

					global::System.Decimal TotalFee {set;}

					SurchargeAdjustment SurchargeAdjustment {set;}

	}
	public interface AgreementItem  : UserInterfaceable 
	{
					global::System.String Text {set;}

					Addendum Addenda {set;}

					AgreementItem Children {set;}

					global::System.String Description {set;}

					AgreementTerm AgreementTerms {set;}

	}
	public interface GeoLocatable  : AccessControlledObject, UserInterfaceable, Searchable, UniquelyIdentifiable 
	{
					global::System.Decimal Latitude {set;}

					global::System.Decimal Longitude {set;}

	}
	public interface Shipment  : Printable, Transitional, Searchable, UniquelyIdentifiable, UserInterfaceable, SearchResult 
	{
					ShipmentMethod ShipmentMethod {set;}

					ContactMechanism BillToContactMechanism {set;}

					ShipmentPackage ShipmentPackages {set;}

					global::System.String ShipmentNumber {set;}

					Document Documents {set;}

					Party BillToParty {set;}

					Party ShipToParty {set;}

					ShipmentItem ShipmentItems {set;}

					InternalOrganisation BillFromInternalOrganisation {set;}

					ContactMechanism ReceiverContactMechanism {set;}

					PostalAddress ShipToAddress {set;}

					global::System.Decimal? EstimatedShipCost {set;}

					global::System.DateTime? EstimatedShipDate {set;}

					global::System.DateTime? LatestCancelDate {set;}

					Carrier Carrier {set;}

					ContactMechanism InquireAboutContactMechanism {set;}

					global::System.DateTime? EstimatedReadyDate {set;}

					PostalAddress ShipFromAddress {set;}

					ContactMechanism BillFromContactMechanism {set;}

					global::System.String HandlingInstruction {set;}

					Store Store {set;}

					Party ShipFromParty {set;}

					ShipmentRouteSegment ShipmentRouteSegments {set;}

					global::System.DateTime? EstimatedArrivalDate {set;}

	}
	public interface Container  : Searchable, SearchResult, UserInterfaceable 
	{
					Facility Facility {set;}

					global::System.String ContainerDescription {set;}

	}
	public interface Payment  : UserInterfaceable, SearchResult, Searchable, Commentable, UniquelyIdentifiable 
	{
					global::System.Decimal Amount {set;}

					PaymentMethod PaymentMethod {set;}

					global::System.DateTime EffectiveDate {set;}

					Party SendingParty {set;}

					PaymentApplication PaymentApplications {set;}

					global::System.String ReferenceNumber {set;}

					Party ReceivingParty {set;}

	}
	public interface Invoice  : UserInterfaceable, Localised, Transitional, SearchResult, Commentable, Searchable, Printable, UniquelyIdentifiable 
	{
					global::System.Decimal? TotalShippingAndHandlingCustomerCurrency {set;}

					Currency CustomerCurrency {set;}

					global::System.String Description {set;}

					ShippingAndHandlingCharge ShippingAndHandlingCharge {set;}

					global::System.Decimal TotalFeeCustomerCurrency {set;}

					Fee Fee {set;}

					global::System.Decimal TotalExVatCustomerCurrency {set;}

					global::System.String CustomerReference {set;}

					DiscountAdjustment DiscountAdjustment {set;}

					global::System.Decimal AmountPaid {set;}

					global::System.Decimal TotalDiscount {set;}

					BillingAccount BillingAccount {set;}

					global::System.Decimal TotalIncVat {set;}

					global::System.Decimal TotalSurcharge {set;}

					global::System.Decimal TotalBasePrice {set;}

					global::System.Decimal TotalVatCustomerCurrency {set;}

					global::System.DateTime InvoiceDate {set;}

					global::System.DateTime EntryDate {set;}

					global::System.Decimal TotalIncVatCustomerCurrency {set;}

					global::System.Decimal TotalShippingAndHandling {set;}

					global::System.Decimal TotalBasePriceCustomerCurrency {set;}

					SurchargeAdjustment SurchargeAdjustment {set;}

					global::System.Decimal TotalExVat {set;}

					InvoiceTerm InvoiceTerms {set;}

					global::System.Decimal TotalSurchargeCustomerCurrency {set;}

					global::System.String InvoiceNumber {set;}

					global::System.String Message {set;}

					VatRegime VatRegime {set;}

					global::System.Decimal TotalDiscountCustomerCurrency {set;}

					global::System.Decimal TotalVat {set;}

					global::System.Decimal TotalFee {set;}

	}
	public interface EngagementItem  : UserInterfaceable 
	{
					QuoteItem QuoteItem {set;}

					global::System.String Description {set;}

					global::System.DateTime? ExpectedStartDate {set;}

					global::System.DateTime? ExpectedEndDate {set;}

					WorkEffort EngagementWorkFulfillment {set;}

					EngagementRate EngagementRates {set;}

					EngagementRate CurrentEngagementRate {set;}

					EngagementItem OrderedWiths {set;}

					Person CurrentAssignedProfessional {set;}

					Product Product {set;}

					ProductFeature ProductFeature {set;}

	}
	public interface ContactMechanism  : UserInterfaceable, Searchable 
	{
					global::System.String Description {set;}

					ContactMechanism FollowTo {set;}

	}
	public interface CommunicationEvent  : Transitional, UserInterfaceable, SearchResult, Searchable, Commentable, UniquelyIdentifiable 
	{
					global::System.DateTime? ScheduledStart {set;}

					CommunicationEventStatus CommunicationEventStatuses {set;}

					Party InvolvedParties {set;}

					global::System.DateTime? InitialScheduledStartDate {set;}

					CommunicationEventObjectState CurrentObjectState {set;}

					CommunicationEventPurpose EventPurposes {set;}

					WorkEffort WorkEfforts {set;}

					global::System.String Description {set;}

					global::System.String Subject {set;}

					CommunicationEventObjectState PreviousObjectState {set;}

					Media Documents {set;}

					Case Case {set;}

					Person Owner {set;}

					CommunicationEventStatus CurrentCommunicationEventStatus {set;}

					global::System.DateTime? ActualStart {set;}

	}
	public interface IUnitOfMeasure  : UserInterfaceable, UniquelyIdentifiable, AccessControlledObject, Searchable 
	{
					global::System.String Description {set;}

					UnitOfMeasureConversion UnitOfMeasureConversions {set;}

					global::System.String Abbreviation {set;}

	}
	public interface CityBound  : UserInterfaceable 
	{
					City Cities {set;}

	}
	public interface OrderAdjustment  : UserInterfaceable 
	{
					global::System.Decimal Amount {set;}

					VatRate VatRate {set;}

					global::System.Decimal Percentage {set;}

	}
	public interface EstimatedProductCost  : Period, SearchResult, Searchable, UserInterfaceable 
	{
					global::System.Decimal? Cost {set;}

					Currency Currency {set;}

					Organisation Organisation {set;}

					global::System.String Description {set;}

					GeographicBoundary GeographicBoundary {set;}

	}
	public interface DeploymentUsage  : UserInterfaceable, Commentable, Period 
	{
					TimeFrequency TimeFrequency {set;}

	}
	public interface Facility  : UserInterfaceable, SearchResult, GeoLocatable, Searchable 
	{
					Facility MadeUpOf {set;}

					global::System.Decimal? SquareFootage {set;}

					global::System.String Description {set;}

					ContactMechanism FacilityContactMechanisms {set;}

					global::System.String Name {set;}

					InternalOrganisation Owner {set;}

	}
	public interface PartBillOfMaterial  : UserInterfaceable, Commentable, Period, Searchable 
	{
					Part Part {set;}

					global::System.String Instruction {set;}

					global::System.Int32? QuantityUsed {set;}

					Part ComponentPart {set;}

	}
	public interface ProductFeature  : Searchable, UniquelyIdentifiable, UserInterfaceable 
	{
					EstimatedProductCost EstimatedProductCosts {set;}

					PriceComponent BasePrices {set;}

					global::System.String Description {set;}

					ProductFeature DependentFeatures {set;}

					ProductFeature IncompatibleFeatures {set;}

					VatRate VatRate {set;}

	}
	public interface Requirement  : SearchResult, Transitional, UniquelyIdentifiable, UserInterfaceable, Searchable 
	{
					global::System.DateTime? RequiredByDate {set;}

					RequirementObjectState PreviousObjectState {set;}

					Party Authorizer {set;}

					RequirementStatus RequirementStatuses {set;}

					global::System.String Reason {set;}

					Requirement Children {set;}

					Party NeededFor {set;}

					Party Originator {set;}

					RequirementObjectState CurrentObjectState {set;}

					RequirementStatus CurrentRequirementStatus {set;}

					Facility Facility {set;}

					Party ServicedBy {set;}

					global::System.Decimal? EstimatedBudget {set;}

					global::System.String Description {set;}

					global::System.Int32? Quantity {set;}

	}
	public interface InvoiceItem  : UserInterfaceable, Transitional 
	{
					global::System.Decimal TotalIncVatCustomerCurrency {set;}

					AgreementTerm InvoiceTerms {set;}

					global::System.Decimal TotalVatCustomerCurrency {set;}

					global::System.Decimal TotalBasePrice {set;}

					global::System.Decimal TotalSurcharge {set;}

					global::System.Decimal TotalInvoiceAdjustment {set;}

					global::System.Decimal TotalExVatCustomerCurrency {set;}

					global::System.Decimal TotalDiscount {set;}

					InvoiceVatRateItem InvoiceVatRateItems {set;}

					global::System.Decimal? TotalDiscountAsPercentage {set;}

					global::System.Decimal CalculatedUnitPrice {set;}

					global::System.Decimal UnitDiscount {set;}

					VatRegime AssignedVatRegime {set;}

					global::System.Decimal TotalIncVat {set;}

					InvoiceItem AdjustmentFor {set;}

					global::System.Decimal UnitBasePrice {set;}

					global::System.Decimal TotalSurchargeCustomerCurrency {set;}

					SerializedInventoryItem SerializedInventoryItem {set;}

					PriceComponent CurrentPriceComponents {set;}

					DiscountAdjustment DiscountAdjustment {set;}

					global::System.Decimal? ActualUnitPrice {set;}

					global::System.String Message {set;}

					global::System.Decimal TotalInvoiceAdjustmentCustomerCurrency {set;}

					global::System.Decimal AmountPaid {set;}

					VatRate DerivedVatRate {set;}

					global::System.Decimal TotalDiscountCustomerCurrency {set;}

					global::System.Decimal UnitSurcharge {set;}

					global::System.Decimal TotalExVat {set;}

					global::System.Decimal Quantity {set;}

					global::System.Decimal? TotalSurchargeAsPercentage {set;}

					VatRegime VatRegime {set;}

					global::System.Decimal TotalBasePriceCustomerCurrency {set;}

					global::System.Decimal TotalVat {set;}

					SurchargeAdjustment SurchargeAdjustment {set;}

					global::System.Decimal UnitVat {set;}

					global::System.String Description {set;}

	}
	public interface CountryBound  : UserInterfaceable, Searchable 
	{
					Country Country {set;}

	}
	public interface Budget  : Period, Commentable, SearchResult, UniquelyIdentifiable, Transitional, UserInterfaceable, Searchable 
	{
					global::System.String Description {set;}

					BudgetRevision BudgetRevisions {set;}

					BudgetStatus BudgetStatuses {set;}

					global::System.String BudgetNumber {set;}

					BudgetObjectState CurrentObjectState {set;}

					BudgetObjectState PreviousObjectState {set;}

					BudgetReview BudgetReviews {set;}

					BudgetStatus CurrentBudgetStatus {set;}

					BudgetItem BudgetItems {set;}

	}
	public interface InventoryItemConfiguration  : Commentable, UserInterfaceable 
	{
					InventoryItem InventoryItem {set;}

					global::System.Int32? Quantity {set;}

					InventoryItem ComponentInventoryItem {set;}

	}
	public interface ProductAssociation  : Commentable, AccessControlledObject, UserInterfaceable, Period 
	{
	}
	public interface PaymentMethod  : UserInterfaceable, UniquelyIdentifiable, AccessControlledObject, Searchable 
	{
					global::System.Decimal? BalanceLimit {set;}

					global::System.Decimal CurrentBalance {set;}

					Journal Journal {set;}

					global::System.String Description {set;}

					OrganisationGlAccount GlPaymentInTransit {set;}

					global::System.String Remarks {set;}

					OrganisationGlAccount GeneralLedgerAccount {set;}

					SupplierRelationship Creditor {set;}

					global::System.Boolean? IsActive {set;}

	}
	public interface OrderItem  : UserInterfaceable, Commentable, Transitional 
	{
					global::System.Decimal TotalDiscountAsPercentage {set;}

					DiscountAdjustment DiscountAdjustment {set;}

					global::System.Decimal UnitVat {set;}

					global::System.Decimal TotalVatCustomerCurrency {set;}

					VatRegime VatRegime {set;}

					BudgetItem BudgetItem {set;}

					global::System.Decimal TotalVat {set;}

					global::System.Decimal UnitSurcharge {set;}

					global::System.Decimal UnitDiscount {set;}

					global::System.Decimal PreviousQuantity {set;}

					global::System.Decimal QuantityOrdered {set;}

					global::System.Decimal TotalExVatCustomerCurrency {set;}

					VatRate DerivedVatRate {set;}

					global::System.Decimal? ActualUnitPrice {set;}

					global::System.Decimal TotalIncVatCustomerCurrency {set;}

					global::System.String Description {set;}

					global::System.Decimal UnitBasePrice {set;}

					PurchaseOrder CorrespondingPurchaseOrder {set;}

					global::System.Decimal CalculatedUnitPrice {set;}

					global::System.Decimal TotalOrderAdjustmentCustomerCurrency {set;}

					global::System.Decimal TotalOrderAdjustment {set;}

					global::System.Decimal TotalSurchargeCustomerCurrency {set;}

					QuoteItem QuoteItem {set;}

					global::System.DateTime? AssignedDeliveryDate {set;}

					global::System.DateTime DeliveryDate {set;}

					global::System.Decimal TotalIncVat {set;}

					global::System.Decimal TotalSurchargeAsPercentage {set;}

					global::System.Decimal TotalDiscountCustomerCurrency {set;}

					global::System.Decimal TotalDiscount {set;}

					global::System.Decimal TotalSurcharge {set;}

					OrderTerm OrderTerms {set;}

					VatRegime AssignedVatRegime {set;}

					global::System.String ShippingInstruction {set;}

					global::System.Decimal TotalBasePrice {set;}

					OrderItem Associations {set;}

					global::System.Decimal TotalExVat {set;}

					global::System.Decimal TotalBasePriceCustomerCurrency {set;}

					PriceComponent CurrentPriceComponents {set;}

					SurchargeAdjustment SurchargeAdjustment {set;}

					global::System.String Message {set;}

	}
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
					global::System.DateTime FromDate {set;}

					global::System.DateTime ThroughDate {set;}

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
	public interface Derivable 
	{
	}
	public interface AccessControlledObject  : Derivable 
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
	public interface ProductFeatureApplicabilityRelationship  : UserInterfaceable 
	{
					Product AvailableFor {set;}

					ProductFeature UsedToDefine {set;}

	}
	public interface OrderShipment 
	{
					SalesOrderItem SalesOrderItem {set;}

					global::System.Boolean Picked {set;}

					ShipmentItem ShipmentItem {set;}

					global::System.Decimal Quantity {set;}

					PurchaseOrderItem PurchaseOrderItem {set;}

	}
	public interface ProductRequirement  : Requirement 
	{
					Product Product {set;}

					DesiredProductFeature DesiredProductFeatures {set;}

	}
	public interface RequestForProposal  : Request 
	{
	}
	public interface SalesInvoiceItemStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					SalesInvoiceItemObjectState SalesInvoiceItemObjectState {set;}

	}
	public interface QuoteItem  : Commentable, UserInterfaceable 
	{
					Party Authorizer {set;}

					Deliverable Deliverable {set;}

					Product Product {set;}

					global::System.DateTime? EstimatedDeliveryDate {set;}

					UnitOfMeasure UnitOfMeasure {set;}

					ProductFeature ProductFeature {set;}

					global::System.Decimal? UnitPrice {set;}

					Skill Skill {set;}

					WorkEffort WorkEffort {set;}

					QuoteTerm QuoteTerms {set;}

					global::System.Int32? Quantity {set;}

					RequestItem RequestItem {set;}

	}
	public interface SalesRepPartyProductCategoryRevenue  : UserInterfaceable 
	{
					global::System.Int32 Year {set;}

					Person SalesRep {set;}

					ProductCategory ProductCategory {set;}

					global::System.Int32 Month {set;}

					Party Party {set;}

					global::System.Decimal? Revenue {set;}

					Currency Currency {set;}

					InternalOrganisation InternalOrganisation {set;}

					global::System.String SalesRepName {set;}

	}
	public interface PayGrade  : UserInterfaceable, Commentable 
	{
					global::System.String Name {set;}

					SalaryStep SalarySteps {set;}

	}
	public interface PartyProductCategoryRevenueHistory  : UserInterfaceable 
	{
					ProductCategory ProductCategory {set;}

					Party Party {set;}

					global::System.Decimal? Quantity {set;}

					global::System.Decimal? Revenue {set;}

					InternalOrganisation InternalOrganisation {set;}

					Currency Currency {set;}

	}
	public interface DiscountAdjustment  : OrderAdjustment 
	{
	}
	public interface Position  : UserInterfaceable, SearchResult, Searchable 
	{
					Organisation Organisation {set;}

					global::System.Boolean? Temporary {set;}

					global::System.DateTime? EstimatedThroughDate {set;}

					global::System.DateTime? EstimatedFromDate {set;}

					PositionType PositionType {set;}

					global::System.Boolean? Fulltime {set;}

					global::System.Boolean? Salary {set;}

					PositionStatus PositionStatus {set;}

					BudgetItem ApprovedBudgetItem {set;}

					global::System.DateTime? ActualFromDate {set;}

					global::System.DateTime? ActualThroughDate {set;}

	}
	public interface LetterCorrespondence  : CommunicationEvent 
	{
					PostalAddress PostalAddresses {set;}

					Party Originator {set;}

					Party Receivers {set;}

	}
	public interface PurchaseOrder  : Order 
	{
					PurchaseOrderItem PurchaseOrderItems {set;}

					Party PreviousTakenViaSupplier {set;}

					PurchaseOrderStatus PaymentStatuses {set;}

					PurchaseOrderStatus CurrentPaymentStatus {set;}

					Party TakenViaSupplier {set;}

					PurchaseOrderObjectState CurrentObjectState {set;}

					PurchaseOrderStatus CurrentShipmentStatus {set;}

					ContactMechanism TakenViaContactMechanism {set;}

					PurchaseOrderStatus OrderStatuses {set;}

					ContactMechanism BillToContactMechanism {set;}

					PurchaseOrderStatus ShipmentStatuses {set;}

					InternalOrganisation ShipToBuyer {set;}

					PurchaseOrderStatus CurrentOrderStatus {set;}

					Facility Facility {set;}

					PostalAddress ShipToAddress {set;}

					PurchaseOrderObjectState PreviousObjectState {set;}

					InternalOrganisation BillToPurchaser {set;}

	}
	public interface GlBudgetAllocation  : UserInterfaceable 
	{
					GeneralLedgerAccount GeneralLedgerAccount {set;}

					BudgetItem BudgetItem {set;}

					global::System.Decimal AllocationPercentage {set;}

	}
	public interface RateType  : Enumeration 
	{
	}
	public interface Brand  : UserInterfaceable, AccessControlledObject, Searchable 
	{
					global::System.String Name {set;}

					ProductCategory ProductCategories {set;}

	}
	public interface SupplierOffering  : Commentable, Period, UserInterfaceable 
	{
					RatingType Rating {set;}

					global::System.Int32? StandardLeadTime {set;}

					ProductPurchasePrice ProductPurchasePrices {set;}

					Ordinal Preference {set;}

					global::System.Decimal? MinimalOrderQuantity {set;}

					Product Product {set;}

					Party Supplier {set;}

					global::System.String ReferenceNumber {set;}

					Part Part {set;}

	}
	public interface SalesAccountingTransaction  : ExternalAccountingTransaction 
	{
					Invoice Invoice {set;}

	}
	public interface Vehicle  : FixedAsset 
	{
	}
	public interface AccountingTransactionNumber  : AccessControlledObject, UserInterfaceable 
	{
					global::System.Int32? Number {set;}

					global::System.Int32? Year {set;}

					AccountingTransactionType AccountingTransactionType {set;}

	}
	public interface WorkEffortPartyAssignment  : Period, UserInterfaceable, Commentable 
	{
					WorkEffort Assignment {set;}

					Party Party {set;}

					Facility Facility {set;}

	}
	public interface ContactMechanismPurpose  : Enumeration 
	{
	}
	public interface TaxDocument  : Document 
	{
	}
	public interface Training  : UserInterfaceable, Searchable 
	{
					global::System.String Description {set;}

	}
	public interface PurchaseAgreement  : Agreement 
	{
	}
	public interface CostCenterCategory  : Searchable, UserInterfaceable, SearchResult, AccessControlledObject, UniquelyIdentifiable 
	{
					CostCenterCategory Parent {set;}

					CostCenterCategory Ancestors {set;}

					CostCenterCategory Children {set;}

					global::System.String Description {set;}

	}
	public interface BasePrice  : PriceComponent 
	{
	}
	public interface JournalEntry  : UserInterfaceable, Transitional, AccessControlledObject, Searchable 
	{
					global::System.String Description {set;}

					global::System.Int32? EntryNumber {set;}

					global::System.DateTime? EntryDate {set;}

					global::System.DateTime? JournalDate {set;}

					JournalEntryDetail JournalEntryDetails {set;}

	}
	public interface SubAgreement  : AgreementItem 
	{
	}
	public interface Skill  : Enumeration, Searchable 
	{
	}
	public interface EmploymentTermination  : Searchable, Enumeration 
	{
	}
	public interface FinancialAccountAdjustment  : FinancialAccountTransaction 
	{
	}
	public interface OperatingCondition  : PartSpecification 
	{
	}
	public interface TermType  : Enumeration 
	{
	}
	public interface LegalTerm  : AgreementTerm 
	{
	}
	public interface EngineeringBom  : PartBillOfMaterial 
	{
	}
	public interface PurchaseInvoiceItemType  : Enumeration 
	{
	}
	public interface Incentive  : AgreementTerm 
	{
	}
	public interface WorkEffortBilling 
	{
					WorkEffort WorkEffort {set;}

					global::System.Decimal? Percentage {set;}

					InvoiceItem InvoiceItem {set;}

	}
	public interface PurchaseReturnStatus  : UserInterfaceable 
	{
					PurchaseReturnObjectState PurchaseReturnObjectState {set;}

					global::System.DateTime? StartDateTime {set;}

	}
	public interface PartyRevenueHistory  : UserInterfaceable 
	{
					Currency Currency {set;}

					global::System.Decimal? Revenue {set;}

					Party Party {set;}

					InternalOrganisation InternalOrganisation {set;}

	}
	public interface PartSpecificationObjectState  : ObjectState 
	{
	}
	public interface PositionTypeRate  : AccessControlledObject, UserInterfaceable 
	{
					global::System.Decimal? Rate {set;}

					RateType RateType {set;}

					TimeFrequency TimeFrequency {set;}

	}
	public interface RatingType  : Enumeration 
	{
	}
	public interface PurchaseInvoiceType  : Enumeration 
	{
	}
	public interface GeneralLedgerAccount  : UniquelyIdentifiable, UserInterfaceable, Searchable, SearchResult 
	{
					Product DefaultCostUnit {set;}

					CostCenter DefaultCostCenter {set;}

					global::System.String Description {set;}

					GeneralLedgerAccountType GeneralLedgerAccountType {set;}

					global::System.Boolean CashAccount {set;}

					global::System.Boolean CostCenterAccount {set;}

					DebitCreditConstant Side {set;}

					global::System.Boolean BalanceSheetAccount {set;}

					global::System.Boolean ReconciliationAccount {set;}

					global::System.String Name {set;}

					global::System.Boolean CostCenterRequired {set;}

					global::System.Boolean CostUnitRequired {set;}

					GeneralLedgerAccountGroup GeneralLedgerAccountGroup {set;}

					CostCenter CostCentersAllowed {set;}

					global::System.Boolean CostUnitAccount {set;}

					global::System.String AccountNumber {set;}

					Product CostUnitsAllowed {set;}

					global::System.Boolean Protected {set;}

	}
	public interface ShippingAndHandlingComponent  : SearchResult, UserInterfaceable, Period, Searchable 
	{
					global::System.Decimal? Cost {set;}

					ShipmentMethod ShipmentMethod {set;}

					Carrier Carrier {set;}

					ShipmentValue ShipmentValue {set;}

					Currency Currency {set;}

					InternalOrganisation SpecifiedFor {set;}

					GeographicBoundary GeographicBoundary {set;}

	}
	public interface PersonalTitle  : Enumeration 
	{
	}
	public interface ReceiptAccountingTransaction  : ExternalAccountingTransaction 
	{
					Receipt Receipt {set;}

	}
	public interface TimeFrequency  : Enumeration, IUnitOfMeasure 
	{
	}
	public interface SubContractorAgreement  : Agreement 
	{
	}
	public interface PackagingContent  : UserInterfaceable 
	{
					ShipmentItem ShipmentItem {set;}

					global::System.Decimal Quantity {set;}

	}
	public interface PartySkill  : UserInterfaceable 
	{
					global::System.Decimal? YearsExperience {set;}

					global::System.DateTime? StartedUsingDate {set;}

					SkillRating SkillRating {set;}

					SkillLevel SkillLevel {set;}

					Skill Skill {set;}

	}
	public interface SerializedInventoryItemStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					SerializedInventoryItemObjectState SerializedInventoryItemObjectState {set;}

	}
	public interface FaxCommunication  : CommunicationEvent 
	{
					Party Originator {set;}

					Party Receiver {set;}

					TelecommunicationsNumber OutgoingFaxNumber {set;}

	}
	public interface PurchaseInvoiceItem  : InvoiceItem 
	{
					PurchaseInvoiceItemObjectState PreviousObjectState {set;}

					PurchaseInvoiceItemType PurchaseInvoiceItemType {set;}

					Part Part {set;}

					PurchaseInvoiceItemStatus CurrentInvoiceItemStatus {set;}

					PurchaseInvoiceItemStatus InvoiceItemStatuses {set;}

					PurchaseInvoiceItemObjectState CurrentObjectState {set;}

	}
	public interface OrderItemBilling 
	{
					OrderItem OrderItem {set;}

					SalesInvoiceItem SalesInvoiceItem {set;}

					global::System.Decimal? Amount {set;}

					global::System.Decimal? Quantity {set;}

	}
	public interface ProductDrawing  : Document 
	{
	}
	public interface PayHistory  : UserInterfaceable, Period 
	{
					Employment Employment {set;}

					TimeFrequency TimeFrequency {set;}

					SalaryStep SalaryStep {set;}

					global::System.Decimal? Amount {set;}

	}
	public interface ShipmentValue  : UserInterfaceable 
	{
					global::System.Decimal? ThroughAmount {set;}

					global::System.Decimal? FromAmount {set;}

	}
	public interface InternalOrganisationAccountingPreference  : AccessControlledObject, UserInterfaceable, Searchable 
	{
					GeneralLedgerAccount GeneralLedgerAccount {set;}

					InventoryItemKind InventoryItemKind {set;}

					PaymentMethod PaymentMethod {set;}

					Receipt Receipt {set;}

					InternalOrganisation InternalOrganisation {set;}

	}
	public interface PurchaseShipmentObjectState  : ObjectState 
	{
	}
	public interface SalesOrderItemObjectState  : ObjectState 
	{
	}
	public interface BankAccount  : FinancialAccount 
	{
					Bank Bank {set;}

					global::System.String NameOnAccount {set;}

					ContactMechanism ContactMechanisms {set;}

					Currency Currency {set;}

					global::System.String Iban {set;}

					global::System.String Branch {set;}

					Person ContactPersons {set;}

	}
	public interface ServiceEntryHeader  : Period, UserInterfaceable 
	{
					ServiceEntry ServiceEntries {set;}

					global::System.DateTime? SubmittedDate {set;}

					Person SubmittedBy {set;}

	}
	public interface PartRevision  : Period, UserInterfaceable 
	{
					global::System.String Reason {set;}

					Part SupersededByPart {set;}

					Part Part {set;}

	}
	public interface PurchaseReturnObjectState  : ObjectState 
	{
	}
	public interface ProductConfiguration  : ProductAssociation 
	{
					Product ProductsUsedIn {set;}

					Product Product {set;}

					global::System.Decimal? QuantityUsed {set;}

					global::System.String Description {set;}

	}
	public interface OwnCreditCard  : PaymentMethod, FinancialAccount 
	{
					Person Owner {set;}

					CreditCard CreditCard {set;}

	}
	public interface Dimension  : ProductFeature 
	{
					global::System.Decimal? Unit {set;}

					UnitOfMeasure UnitOfMeasure {set;}

	}
	public interface SalesInvoiceItemType  : Enumeration 
	{
	}
	public interface Model  : ProductFeature, Enumeration 
	{
	}
	public interface PickList  : UserInterfaceable, SearchResult, Printable, Transitional, Searchable, UniquelyIdentifiable 
	{
					CustomerShipment CustomerShipmentCorrection {set;}

					global::System.DateTime? CreationDate {set;}

					PickListItem PickListItems {set;}

					PickListObjectState CurrentObjectState {set;}

					PickListStatus CurrentPickListStatus {set;}

					Person Picker {set;}

					PickListStatus PickListStatuses {set;}

					PickListObjectState PreviousObjectState {set;}

					Party ShipToParty {set;}

					Store Store {set;}

	}
	public interface StoreRevenue  : UserInterfaceable 
	{
					InternalOrganisation InternalOrganisation {set;}

					global::System.Int32 Month {set;}

					Currency Currency {set;}

					Store Store {set;}

					global::System.String StoreName {set;}

					global::System.Decimal? Revenue {set;}

					global::System.Int32 Year {set;}

	}
	public interface AgreementExhibit  : AgreementItem 
	{
	}
	public interface ProductCategoryRevenueHistory  : UserInterfaceable 
	{
					Currency Currency {set;}

					global::System.Decimal? Revenue {set;}

					InternalOrganisation InternalOrganisation {set;}

					ProductCategory ProductCategory {set;}

	}
	public interface SalesRepRevenueHistory  : UserInterfaceable 
	{
					Currency Currency {set;}

					Person SalesRep {set;}

					InternalOrganisation InternalOrganisation {set;}

					global::System.Decimal? Revenue {set;}

	}
	public interface EstimatedLaborCost  : EstimatedProductCost 
	{
	}
	public interface CostCenter  : UserInterfaceable, Searchable, UniquelyIdentifiable, AccessControlledObject 
	{
					global::System.String Description {set;}

					OrganisationGlAccount InternalTransferGlAccount {set;}

					CostCenterCategory CostCenterCategories {set;}

					OrganisationGlAccount RedistributedCostsGlAccount {set;}

					global::System.String Name {set;}

					global::System.Boolean? Active {set;}

					global::System.Boolean? UseGlAccountOfBooking {set;}

	}
	public interface SupplierRelationship  : PartyRelationship 
	{
					Organisation Supplier {set;}

					global::System.Int32? SubAccountNumber {set;}

					global::System.DateTime? LastReminderDate {set;}

					DunningType DunningType {set;}

					InternalOrganisation InternalOrganisation {set;}

					global::System.DateTime? BlockedForDunning {set;}

	}
	public interface SkillRating  : Enumeration 
	{
	}
	public interface EventRegistration 
	{
					Person Person {set;}

					Event Event {set;}

					global::System.DateTime? AllorsDateTime {set;}

	}
	public interface Building  : Facility 
	{
	}
	public interface ServiceEntryBilling 
	{
					ServiceEntry ServiceEntry {set;}

					InvoiceItem InvoiceItem {set;}

	}
	public interface PurchaseShipment  : Shipment 
	{
					PurchaseShipmentObjectState CurrentObjectState {set;}

					Facility Facility {set;}

					PurchaseShipmentObjectState PreviousObjectState {set;}

					PurchaseShipmentStatus ShipmentStatuses {set;}

					PurchaseShipmentStatus CurrentShipmentStatus {set;}

					PurchaseOrder PurchaseOrder {set;}

	}
	public interface UnitOfMeasureConversion  : Searchable, UserInterfaceable 
	{
					IUnitOfMeasure ToUnitOfMeasure {set;}

					global::System.DateTime? StartDate {set;}

					global::System.Decimal ConversionFactor {set;}

	}
	public interface VatRateUsage  : Enumeration 
	{
	}
	public interface Project  : WorkEffort 
	{
	}
	public interface PaymentBudgetAllocation  : UserInterfaceable 
	{
					Payment Payment {set;}

					BudgetItem BudgetItem {set;}

					global::System.Decimal Amount {set;}

	}
	public interface Hobby  : Enumeration 
	{
	}
	public interface ProductRevenueHistory  : UserInterfaceable 
	{
					InternalOrganisation InternalOrganisation {set;}

					global::System.Decimal? Revenue {set;}

					Currency Currency {set;}

					Product Product {set;}

	}
	public interface OrderRequirementCommitment  : UserInterfaceable 
	{
					global::System.Int32? Quantity {set;}

					OrderItem OrderItem {set;}

					Requirement Requirement {set;}

	}
	public interface OrganisationRollUp  : PartyRelationship 
	{
					Organisation Parent {set;}

					OrganisationUnit RollupKind {set;}

					Organisation Child {set;}

	}
	public interface AccountingTransactionType  : Enumeration 
	{
	}
	public interface RevenueValueBreak  : UserInterfaceable 
	{
					global::System.Decimal? ThroughAmount {set;}

					global::System.Decimal? FromAmount {set;}

	}
	public interface Activity  : WorkEffort 
	{
	}
	public interface WorkEffortAssignment  : Period, UserInterfaceable, Commentable 
	{
					Person Professional {set;}

					WorkEffort Assignment {set;}

	}
	public interface SoftwareFeature  : ProductFeature, Enumeration 
	{
	}
	public interface FiscalYearInvoiceNumber 
	{
					global::System.Int32 NextSalesInvoiceNumber {set;}

					global::System.Int32? FiscalYear {set;}

	}
	public interface SalesOrderStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					SalesOrderObjectState SalesOrderObjectState {set;}

	}
	public interface BillingAccount  : UserInterfaceable 
	{
					global::System.String Description {set;}

					ContactMechanism ContactMechanism {set;}

	}
	public interface SalesChannelRevenue  : UserInterfaceable 
	{
					global::System.Int32 Year {set;}

					global::System.Int32 Month {set;}

					Currency Currency {set;}

					SalesChannel SalesChannel {set;}

					global::System.String SalesChannelName {set;}

					global::System.Decimal? Revenue {set;}

					InternalOrganisation InternalOrganisation {set;}

	}
	public interface AutomatedAgent  : User, Party 
	{
					global::System.String Name {set;}

					global::System.String Description {set;}

	}
	public interface SalesChannelRevenueHistory  : UserInterfaceable 
	{
					SalesChannel SalesChannel {set;}

					Currency Currency {set;}

					global::System.Decimal? Revenue {set;}

					InternalOrganisation InternalOrganisation {set;}

	}
	public interface Proposal  : Quote 
	{
	}
	public interface FinishedGood  : Part 
	{
	}
	public interface PerformanceSpecification  : PartSpecification 
	{
	}
	public interface ProductionRun  : WorkEffort 
	{
					global::System.Int32? QuantityProduced {set;}

					global::System.Int32? QuantityRejected {set;}

					global::System.Int32? QuantityToProduce {set;}

	}
	public interface Ordinal  : Enumeration 
	{
	}
	public interface Citizenship  : UserInterfaceable 
	{
					Passport Passports {set;}

					Country Country {set;}

	}
	public interface PartyProductRevenue  : UserInterfaceable 
	{
					global::System.Decimal? Revenue {set;}

					global::System.Int32 Month {set;}

					global::System.Int32 Year {set;}

					global::System.String PartyProductName {set;}

					global::System.Decimal? Quantity {set;}

					Currency Currency {set;}

					Party Party {set;}

					InternalOrganisation InternalOrganisation {set;}

					Product Product {set;}

	}
	public interface ShipmentMethod  : Enumeration 
	{
	}
	public interface Organisation  : Party 
	{
					LegalForm LegalForm {set;}

					global::System.String Name {set;}

					UserGroup CustomerContactUserGroup {set;}

					Person CurrentContacts {set;}

					Media LogoImage {set;}

					UserGroup PartnerContactUserGroup {set;}

					global::System.String TaxNumber {set;}

					UserGroup SupplierContactUserGroup {set;}

	}
	public interface Responsibility  : UserInterfaceable, Searchable 
	{
					global::System.String Description {set;}

	}
	public interface VatReturnBoxType  : AccessControlledObject, UserInterfaceable 
	{
					global::System.String Type {set;}

	}
	public interface DebitCreditConstant  : UniquelyIdentifiable, Enumeration 
	{
	}
	public interface WorkEffortFixedAssetAssignment  : Commentable, UserInterfaceable, Period 
	{
					AssetAssignmentStatus AssetAssignmentStatus {set;}

					WorkEffort Assignment {set;}

					global::System.Decimal? AllocatedCost {set;}

					FixedAsset FixedAsset {set;}

	}
	public interface VatCalculationMethod  : Enumeration 
	{
	}
	public interface InvoiceSequence  : Enumeration 
	{
	}
	public interface CustomerRelationship  : PartyRelationship 
	{
					global::System.Boolean? BlockedForDunning {set;}

					InternalOrganisation InternalOrganisation {set;}

					global::System.Decimal? AmountOverDue {set;}

					Party Customer {set;}

					DunningType DunningType {set;}

					global::System.Decimal AmountDue {set;}

					global::System.Decimal YTDRevenue {set;}

					global::System.DateTime? LastReminderDate {set;}

					global::System.Decimal? CreditLimit {set;}

					global::System.Int32? SubAccountNumber {set;}

					global::System.Decimal LastYearsRevenue {set;}

	}
	public interface PartyClassification  : UserInterfaceable, Searchable 
	{
					global::System.String Description {set;}

	}
	public interface PartyProductCategoryRevenue  : UserInterfaceable 
	{
					Party Party {set;}

					global::System.Decimal? Revenue {set;}

					global::System.Int32 Month {set;}

					Currency Currency {set;}

					global::System.Int32 Year {set;}

					global::System.String PartyProductCategoryName {set;}

					InternalOrganisation InternalOrganisation {set;}

					ProductCategory ProductCategory {set;}

					global::System.Decimal? Quantity {set;}

	}
	public interface PartyFixedAssetAssignment  : UserInterfaceable, Period, Commentable 
	{
					FixedAsset FixedAsset {set;}

					Party Party {set;}

					AssetAssignmentStatus AssetAssignmentStatus {set;}

					global::System.Decimal? AllocatedCost {set;}

	}
	public interface DunningType  : Enumeration 
	{
	}
	public interface CapitalBudget  : Budget 
	{
	}
	public interface AccountAdjustment  : FinancialAccountTransaction 
	{
	}
	public interface PositionStatus  : Enumeration 
	{
	}
	public interface MarketingPackage  : ProductAssociation 
	{
					global::System.String Instruction {set;}

					Product ProductsUsedIn {set;}

					Product Product {set;}

					global::System.String Description {set;}

					global::System.Int32? QuantityUsed {set;}

	}
	public interface ItemIssuance  : UserInterfaceable 
	{
					global::System.DateTime? IssuanceDateTime {set;}

					InventoryItem InventoryItem {set;}

					global::System.Decimal? Quantity {set;}

					ShipmentItem ShipmentItem {set;}

					PickListItem PickListItem {set;}

	}
	public interface ShipmentPackage  : UserInterfaceable, UniquelyIdentifiable, Printable 
	{
					PackagingContent PackagingContents {set;}

					Document Documents {set;}

					global::System.DateTime? CreationDate {set;}

					global::System.Int32 SequenceNumber {set;}

	}
	public interface PurchaseOrderObjectState  : ObjectState 
	{
	}
	public interface Size  : Enumeration, ProductFeature 
	{
	}
	public interface PerformanceNote  : Searchable, UserInterfaceable, Commentable, SearchResult 
	{
					global::System.String Description {set;}

					global::System.DateTime? CommunicationDate {set;}

					Person GivenByManager {set;}

					Person Employee {set;}

	}
	public interface DeliverableTurnover  : ServiceEntry 
	{
					global::System.Decimal? Amount {set;}

	}
	public interface ShipmentReceipt  : UserInterfaceable 
	{
					global::System.String ItemDescription {set;}

					NonSerializedInventoryItem InventoryItem {set;}

					global::System.String RejectionReason {set;}

					OrderItem OrderItem {set;}

					global::System.Decimal? QuantityRejected {set;}

					ShipmentItem ShipmentItem {set;}

					global::System.DateTime ReceivedDateTime {set;}

					global::System.Decimal? QuantityAccepted {set;}

	}
	public interface RequirementCommunication  : UserInterfaceable 
	{
					CommunicationEvent CommunicationEvent {set;}

					Requirement Requirement {set;}

					Person AssociatedProfessional {set;}

	}
	public interface GeneralLedgerAccountGroup  : AccessControlledObject, UserInterfaceable 
	{
					GeneralLedgerAccountGroup Parent {set;}

					global::System.String Description {set;}

	}
	public interface SerializedInventoryItem  : InventoryItem 
	{
					SerializedInventoryItemObjectState PreviousObjectState {set;}

					SerializedInventoryItemStatus InventoryItemStatuses {set;}

					global::System.String SerialNumber {set;}

					SerializedInventoryItemObjectState CurrentObjectState {set;}

					SerializedInventoryItemStatus CurrentInventoryItemStatus {set;}

	}
	public interface ItemVarianceAccountingTransaction  : AccountingTransaction 
	{
	}
	public interface RespondingParty  : UserInterfaceable 
	{
					global::System.DateTime? SendingDate {set;}

					ContactMechanism ContactMechanism {set;}

					Party Party {set;}

	}
	public interface SalesInvoiceItemObjectState  : ObjectState 
	{
	}
	public interface BudgetStatus  : UserInterfaceable 
	{
					global::System.DateTime StartDateTime {set;}

					BudgetObjectState BudgetObjectState {set;}

	}
	public interface Barrel  : Container 
	{
	}
	public interface PositionType  : Searchable, UserInterfaceable 
	{
					global::System.String Description {set;}

					Responsibility Responsibilities {set;}

					global::System.Decimal? BenefitPercentage {set;}

					global::System.String Title {set;}

					PositionTypeRate PositionTypeRate {set;}

	}
	public interface ProductPurchasePrice  : AccessControlledObject, Period, UserInterfaceable 
	{
					global::System.Decimal Price {set;}

					UnitOfMeasure UnitOfMeasure {set;}

					Currency Currency {set;}

	}
	public interface Carrier  : UniquelyIdentifiable, AccessControlledObject, UserInterfaceable, Searchable 
	{
					global::System.String Name {set;}

	}
	public interface Resume  : Searchable, UserInterfaceable 
	{
					global::System.DateTime? ResumeDate {set;}

					global::System.String ResumeText {set;}

	}
	public interface WebAddress  : ElectronicAddress 
	{
	}
	public interface ProjectRequirement  : Requirement 
	{
					Deliverable NeededDeliverables {set;}

	}
	public interface Deposit  : FinancialAccountTransaction 
	{
					Receipt Receipts {set;}

	}
	public interface LegalForm  : UserInterfaceable, Searchable 
	{
					global::System.String Description {set;}

	}
	public interface CostOfGoodsSoldMethod  : Enumeration 
	{
	}
	public interface StatementOfWork  : Quote 
	{
	}
	public interface SkillLevel  : Enumeration 
	{
	}
	public interface PickListStatus  : UserInterfaceable, AccessControlledObject 
	{
					PickListObjectState PickListObjectState {set;}

					global::System.DateTime? StartDateTime {set;}

	}
	public interface TaxDue  : ExternalAccountingTransaction 
	{
	}
	public interface OneTimeCharge  : PriceComponent 
	{
	}
	public interface Note  : ExternalAccountingTransaction 
	{
	}
	public interface PartBillOfMaterialSubstitute  : Period, UserInterfaceable, Commentable 
	{
					PartBillOfMaterial SubstitutionPartBillOfMaterial {set;}

					Ordinal Preference {set;}

					global::System.Int32? Quantity {set;}

					PartBillOfMaterial PartBillOfMaterial {set;}

	}
	public interface Receipt  : Payment 
	{
	}
	public interface RequirementBudgetAllocation  : Searchable, UserInterfaceable 
	{
					BudgetItem BudgetItem {set;}

					Requirement Requirement {set;}

					global::System.Decimal Amount {set;}

	}
	public interface OrganisationGlAccount  : UserInterfaceable, Period 
	{
					Product Product {set;}

					OrganisationGlAccount Parent {set;}

					Party Party {set;}

					global::System.Boolean HasBankStatementTransactions {set;}

					ProductCategory ProductCategory {set;}

					InternalOrganisation InternalOrganisation {set;}

					GeneralLedgerAccount GeneralLedgerAccount {set;}

	}
	public interface Maintenance  : WorkEffort 
	{
	}
	public interface NonSerializedInventoryItem  : InventoryItem 
	{
					NonSerializedInventoryItemObjectState CurrentObjectState {set;}

					global::System.Decimal QuantityCommittedOut {set;}

					NonSerializedInventoryItemStatus NonSerializedInventoryItemStatuses {set;}

					NonSerializedInventoryItemObjectState PreviousObjectState {set;}

					NonSerializedInventoryItemStatus CurrentInventoryItemStatus {set;}

					global::System.Decimal QuantityOnHand {set;}

					global::System.Decimal PreviousQuantityOnHand {set;}

					global::System.Decimal AvailableToPromise {set;}

					global::System.Decimal QuantityExpectedIn {set;}

	}
	public interface CreditLine  : ExternalAccountingTransaction 
	{
	}
	public interface BillOfLading  : Document 
	{
	}
	public interface UnitOfMeasure  : IUnitOfMeasure, UniquelyIdentifiable, UserInterfaceable, Searchable, Enumeration 
	{
	}
	public interface ServiceConfiguration  : InventoryItemConfiguration 
	{
	}
	public interface NeededSkill  : UserInterfaceable 
	{
					SkillLevel SkillLevel {set;}

					global::System.Decimal? YearsExperience {set;}

					Skill Skill {set;}

	}
	public interface Room  : Facility, Container 
	{
	}
	public interface Plant  : Facility 
	{
	}
	public interface SalesInvoice  : Invoice 
	{
					SalesInvoiceObjectState CurrentObjectState {set;}

					SalesInvoiceObjectState PreviousObjectState {set;}

					global::System.Decimal? TotalListPrice {set;}

					InternalOrganisation BilledFromInternalOrganisation {set;}

					ContactMechanism BillToContactMechanism {set;}

					Party PreviousBillToCustomer {set;}

					SalesInvoiceType SalesInvoiceType {set;}

					global::System.Decimal InitialProfitMargin {set;}

					PaymentMethod PaymentMethod {set;}

					SalesOrder SalesOrder {set;}

					global::System.Decimal InitialMarkupPercentage {set;}

					global::System.Decimal MaintainedMarkupPercentage {set;}

					Person SalesReps {set;}

					Shipment Shipment {set;}

					global::System.Decimal MaintainedProfitMargin {set;}

					SalesInvoiceStatus InvoiceStatuses {set;}

					Party PreviousShipToCustomer {set;}

					Party BillToCustomer {set;}

					SalesInvoiceStatus CurrentInvoiceStatus {set;}

					SalesInvoiceItem SalesInvoiceItems {set;}

					global::System.Decimal TotalListPriceCustomerCurrency {set;}

					Party ShipToCustomer {set;}

					ContactMechanism BilledFromContactMechanism {set;}

					global::System.Decimal? TotalPurchasePrice {set;}

					SalesChannel SalesChannel {set;}

					Party Customers {set;}

					PostalAddress ShipToAddress {set;}

					Store Store {set;}

	}
	public interface StandardServiceOrderItem  : EngagementItem 
	{
	}
	public interface PurchaseInvoiceStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					PurchaseInvoiceObjectState PurchaseInvoiceObjectState {set;}

	}
	public interface Region  : GeographicBoundaryComposite, UserInterfaceable 
	{
					global::System.String Name {set;}

	}
	public interface SalesTerritory  : UserInterfaceable, GeographicBoundaryComposite 
	{
					global::System.String Name {set;}

	}
	public interface TimeEntry  : ServiceEntry 
	{
					global::System.Decimal? Cost {set;}

					global::System.Decimal GrossMargin {set;}

					QuoteTerm QuoteTerm {set;}

					global::System.Decimal? BillingRate {set;}

					UnitOfMeasure UnitOfMeasure {set;}

					global::System.Decimal? AmountOfTime {set;}

	}
	public interface DepreciationMethod  : UserInterfaceable 
	{
					global::System.String Formula {set;}

					global::System.String Description {set;}

	}
	public interface AssetAssignmentStatus  : Enumeration 
	{
	}
	public interface StoreRevenueHistory  : UserInterfaceable 
	{
					InternalOrganisation InternalOrganisation {set;}

					Currency Currency {set;}

					Store Store {set;}

					global::System.Decimal? Revenue {set;}

	}
	public interface PersonTraining  : Period, UserInterfaceable 
	{
					Training Training {set;}

	}
	public interface DeductionType  : Enumeration 
	{
	}
	public interface DeliverableOrderItem  : EngagementItem 
	{
					global::System.Decimal? AgreedUponPrice {set;}

	}
	public interface PackagingSlip  : Document 
	{
	}
	public interface CustomerReturnObjectState  : ObjectState 
	{
	}
	public interface OrganisationGlAccountBalance  : UserInterfaceable 
	{
					OrganisationGlAccount OrganisationGlAccount {set;}

					global::System.Decimal Amount {set;}

					AccountingPeriod AccountingPeriod {set;}

	}
	public interface InternalOrganisationRevenueHistory  : UserInterfaceable 
	{
					InternalOrganisation InternalOrganisation {set;}

					global::System.Decimal? AllorsDecimal {set;}

					Currency Currency {set;}

					global::System.Decimal? Revenue {set;}

	}
	public interface ManufacturingBom  : PartBillOfMaterial 
	{
	}
	public interface Deliverable  : Searchable, UserInterfaceable 
	{
					global::System.String Name {set;}

					DeliverableType DeliverableType {set;}

	}
	public interface EmploymentApplication  : SearchResult, UserInterfaceable, Searchable 
	{
					global::System.DateTime ApplicationDate {set;}

					Position Position {set;}

					EmploymentApplicationStatus EmploymentApplicationStatus {set;}

					Person Person {set;}

					EmploymentApplicationSource EmploymentApplicationSource {set;}

	}
	public interface VatRegime  : Enumeration 
	{
					VatRate VatRate {set;}

					OrganisationGlAccount GeneralLedgerAccount {set;}

	}
	public interface PositionFulfillment  : Commentable, Period, UserInterfaceable 
	{
					Position Position {set;}

					Person Person {set;}

	}
	public interface Employment  : PartyRelationship 
	{
					InternalOrganisation Employer {set;}

					Person Employee {set;}

					PayrollPreference PayrollPreferences {set;}

					EmploymentTerminationReason EmploymentTerminationReason {set;}

					EmploymentTermination EmploymentTermination {set;}

	}
	public interface AccountingPeriod  : Budget, UserInterfaceable 
	{
					AccountingPeriod Parent {set;}

					global::System.Boolean Active {set;}

					global::System.Int32 PeriodNumber {set;}

					TimeFrequency TimeFrequency {set;}

	}
	public interface EngagementRate  : Period, UserInterfaceable 
	{
					global::System.Decimal? BillingRate {set;}

					RatingType RatingType {set;}

					global::System.Decimal? Cost {set;}

					PriceComponent GoverningPriceComponents {set;}

					global::System.String ChangeReason {set;}

					UnitOfMeasure UnitOfMeasure {set;}

	}
	public interface TelecommunicationsNumber  : ContactMechanism 
	{
					global::System.String AreaCode {set;}

					global::System.String CountryCode {set;}

					global::System.String ContactNumber {set;}

	}
	public interface SalesRepRelationship  : UserInterfaceable, Commentable, AccessControlledObject, Period, PartyRelationship 
	{
					Person SalesRepresentative {set;}

					global::System.Decimal LastYearsCommission {set;}

					ProductCategory ProductCategories {set;}

					InternalOrganisation InternalOrganisation {set;}

					global::System.Decimal YTDCommission {set;}

					Party Customer {set;}

	}
	public interface PurchaseInvoiceObjectState  : ObjectState 
	{
	}
	public interface ProductCategoryRevenue  : UserInterfaceable 
	{
					global::System.String ProductCategoryName {set;}

					global::System.Int32 Month {set;}

					InternalOrganisation InternalOrganisation {set;}

					ProductCategory ProductCategory {set;}

					global::System.Decimal? Revenue {set;}

					Currency Currency {set;}

					global::System.Int32 Year {set;}

	}
	public interface ChartOfAccounts  : UserInterfaceable, AccessControlledObject, UniquelyIdentifiable 
	{
					global::System.String Name {set;}

					GeneralLedgerAccount GeneralLedgerAccounts {set;}

	}
	public interface PartyRevenue  : UserInterfaceable 
	{
					Currency Currency {set;}

					global::System.String PartyName {set;}

					global::System.Int32 Month {set;}

					Party Party {set;}

					InternalOrganisation InternalOrganisation {set;}

					global::System.Int32 Year {set;}

					global::System.Decimal? Revenue {set;}

	}
	public interface MarketingMaterial  : Document 
	{
	}
	public interface PurchaseInvoiceItemStatus  : UserInterfaceable 
	{
					PurchaseInvoiceItemObjectState PurchaseInvoiceItemObjectState {set;}

					global::System.DateTime? StartDateTime {set;}

	}
	public interface InvoiceVatRateItem  : UserInterfaceable 
	{
					global::System.Decimal? BaseAmount {set;}

					VatRate VatRates {set;}

					global::System.Decimal? VatAmount {set;}

	}
	public interface CaseObjectState  : ObjectState 
	{
	}
	public interface SalaryStep  : UserInterfaceable 
	{
					global::System.DateTime? ModifiedDate {set;}

					global::System.Decimal? Amount {set;}

	}
	public interface DropShipmentStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					DropShipmentObjectState DropShipmentObjectState {set;}

	}
	public interface PaymentApplication  : UserInterfaceable 
	{
					global::System.Decimal AmountApplied {set;}

					InvoiceItem InvoiceItem {set;}

					Invoice Invoice {set;}

					BillingAccount BillingAccount {set;}

	}
	public interface NonSerializedInventoryItemStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					NonSerializedInventoryItemObjectState NonSerializedInventoryItemObjectState {set;}

	}
	public interface SurchargeAdjustment  : OrderAdjustment 
	{
	}
	public interface Depreciation  : InternalAccountingTransaction 
	{
					FixedAsset FixedAsset {set;}

	}
	public interface Territory  : UserInterfaceable, CityBound, GeographicBoundary, CountryBound 
	{
					global::System.String Name {set;}

	}
	public interface SalesOrder  : Order 
	{
					ContactMechanism TakenByContactMechanism {set;}

					SalesOrderStatus ShipmentStatuses {set;}

					SalesOrderStatus CurrentShipmentStatus {set;}

					SalesOrderStatus CurrentPaymentStatus {set;}

					Party ShipToCustomer {set;}

					Party BillToCustomer {set;}

					global::System.Decimal TotalPurchasePrice {set;}

					ShipmentMethod ShipmentMethod {set;}

					global::System.Decimal TotalListPriceCustomerCurrency {set;}

					global::System.Decimal MaintainedProfitMargin {set;}

					PostalAddress ShipToAddress {set;}

					Party PreviousShipToCustomer {set;}

					ContactMechanism BillToContactMechanism {set;}

					Person SalesReps {set;}

					global::System.Decimal InitialProfitMargin {set;}

					SalesOrderObjectState PreviousObjectState {set;}

					global::System.Decimal TotalListPrice {set;}

					global::System.Boolean? PartiallyShip {set;}

					SalesOrderStatus PaymentStatuses {set;}

					Party Customers {set;}

					Store Store {set;}

					global::System.Decimal MaintainedMarkupPercentage {set;}

					ContactMechanism BillFromContactMechanism {set;}

					PaymentMethod PaymentMethod {set;}

					ContactMechanism PlacingContactMechanism {set;}

					SalesOrderStatus CurrentOrderStatus {set;}

					Party PreviousBillToCustomer {set;}

					SalesChannel SalesChannel {set;}

					Party PlacingCustomer {set;}

					SalesOrderStatus OrderStatuses {set;}

					SalesInvoice ProformaInvoice {set;}

					SalesOrderItem SalesOrderItems {set;}

					SalesOrderObjectState CurrentObjectState {set;}

					global::System.Decimal InitialMarkupPercentage {set;}

					InternalOrganisation TakenByInternalOrganisation {set;}

	}
	public interface Warehouse  : Facility 
	{
	}
	public interface AgreementPricingProgram  : AgreementItem 
	{
	}
	public interface SalesRepRevenue  : UserInterfaceable 
	{
					global::System.Decimal? Revenue {set;}

					Currency Currency {set;}

					InternalOrganisation InternalOrganisation {set;}

					global::System.Int32 Month {set;}

					global::System.String SalesRepName {set;}

					global::System.Int32 Year {set;}

					Person SalesRep {set;}

	}
	public interface EmploymentApplicationSource  : Enumeration, Searchable 
	{
	}
	public interface Engagement  : UserInterfaceable, Searchable, SearchResult 
	{
					Agreement Agreement {set;}

					ContactMechanism PlacingContactMechanism {set;}

					global::System.Decimal? MaximumAmount {set;}

					ContactMechanism BillToContactMechanism {set;}

					global::System.String Description {set;}

					Party BillToParty {set;}

					Party PlacingParty {set;}

					InternalOrganisation TakenViaInternalOrganisation {set;}

					global::System.DateTime? StartDate {set;}

					ContactMechanism TakenViaContactMechanism {set;}

					global::System.Decimal? EstimatedAmount {set;}

					global::System.DateTime? EndDate {set;}

					global::System.DateTime? ContractDate {set;}

					EngagementItem EngagementItems {set;}

					global::System.String ClientPurchaseOrderNumber {set;}

					OrganisationContactRelationship TakenViaOrganisationContactRelationship {set;}

	}
	public interface InventoryItemKind  : Enumeration 
	{
	}
	public interface CustomEngagementItem  : EngagementItem 
	{
					global::System.String DescriptionOfWork {set;}

					global::System.Decimal? AgreedUponPrice {set;}

	}
	public interface SalesRepPartyRevenue  : UserInterfaceable 
	{
					global::System.Decimal? Revenue {set;}

					global::System.Int32 Year {set;}

					Person SalesRep {set;}

					global::System.String SalesRepName {set;}

					InternalOrganisation InternalOrganisation {set;}

					Party Party {set;}

					Currency Currency {set;}

					global::System.Int32 Month {set;}

	}
	public interface JournalType  : Enumeration 
	{
	}
	public interface PurchaseOrderItemStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					PurchaseOrderItemObjectState PurchaseOrderItemObjectState {set;}

	}
	public interface Addendum  : UserInterfaceable 
	{
					global::System.String Text {set;}

					global::System.DateTime? EffictiveDate {set;}

					global::System.String Description {set;}

					global::System.DateTime CreationDate {set;}

	}
	public interface Floor  : Facility 
	{
	}
	public interface WorkEffortType  : Searchable, UserInterfaceable 
	{
					WorkEffortFixedAssetStandard WorkEffortFixedAssetStandards {set;}

					WorkEffortGoodStandard WorkEffortGoodStandards {set;}

					WorkEffortType Children {set;}

					FixedAsset FixedAssetToRepair {set;}

					global::System.String Description {set;}

					WorkEffortType Dependencies {set;}

					WorkEffortTypeKind WorkEffortTypeKind {set;}

					WorkEffortPartStandard WorkEffortPartStandards {set;}

					WorkEffortSkillStandard WorkEffortSkillStandards {set;}

					global::System.Decimal? StandardWorkHours {set;}

					Product ProductToProduce {set;}

					Deliverable DeliverableToProduce {set;}

	}
	public interface SalesInvoiceStatus  : UserInterfaceable 
	{
					SalesInvoiceObjectState SalesInvoiceObjectState {set;}

					global::System.DateTime? StartDateTime {set;}

	}
	public interface SalesAgreement  : Agreement 
	{
	}
	public interface PurchaseInvoice  : Invoice 
	{
					PurchaseInvoiceObjectState PreviousObjectState {set;}

					PurchaseInvoiceItem PurchaseInvoiceItems {set;}

					InternalOrganisation BilledToInternalOrganisation {set;}

					PurchaseInvoiceStatus CurrentInvoiceStatus {set;}

					PurchaseInvoiceObjectState CurrentObjectState {set;}

					Party BilledFromParty {set;}

					PurchaseInvoiceType PurchaseInvoiceType {set;}

					PurchaseInvoiceStatus InvoiceStatuses {set;}

	}
	public interface CustomerReturn  : Shipment 
	{
					CustomerReturnStatus CurrentShipmentStatus {set;}

					CustomerReturnObjectState PreviousObjectState {set;}

					CustomerReturnStatus ShipmentStatuses {set;}

					CustomerReturnObjectState CurrentObjectState {set;}

	}
	public interface PartyPackageRevenueHistory  : UserInterfaceable 
	{
					Package Package {set;}

					InternalOrganisation InternalOrganisation {set;}

					Currency Currency {set;}

					Party Party {set;}

					global::System.Decimal? Revenue {set;}

	}
	public interface OrderKind  : Searchable, UserInterfaceable, UniquelyIdentifiable, AccessControlledObject 
	{
					global::System.String Description {set;}

					global::System.Boolean? ScheduleManually {set;}

	}
	public interface Amortization  : InternalAccountingTransaction 
	{
	}
	public interface PickListItem  : UserInterfaceable 
	{
					global::System.Decimal? RequestedQuantity {set;}

					InventoryItem InventoryItem {set;}

					global::System.Decimal? ActualQuantity {set;}

	}
	public interface SalesOrderItem  : OrderItem 
	{
					global::System.Decimal InitialProfitMargin {set;}

					SalesOrderItemStatus CurrentPaymentStatus {set;}

					global::System.Decimal QuantityShortFalled {set;}

					OrderItem OrderedWithFeatures {set;}

					global::System.Decimal MaintainedProfitMargin {set;}

					global::System.Decimal? RequiredProfitMargin {set;}

					SalesOrderItemStatus OrderItemStatuses {set;}

					SalesOrderItemStatus CurrentShipmentStatus {set;}

					NonSerializedInventoryItem PreviousReservedFromInventoryItem {set;}

					global::System.Decimal? RequiredMarkupPercentage {set;}

					global::System.Decimal QuantityShipped {set;}

					SalesOrderItemStatus CurrentOrderItemStatus {set;}

					PostalAddress ShipToAddress {set;}

					global::System.Decimal QuantityPicked {set;}

					Product PreviousProduct {set;}

					SalesOrderItemObjectState CurrentObjectState {set;}

					global::System.Decimal UnitPurchasePrice {set;}

					Party ShipToParty {set;}

					PostalAddress AssignedShipToAddress {set;}

					global::System.Decimal? QuantityReturned {set;}

					global::System.Decimal QuantityReserved {set;}

					Person SalesRep {set;}

					SalesOrderItemStatus ShipmentStatuses {set;}

					Party AssignedShipToParty {set;}

					global::System.Decimal QuantityPendingShipment {set;}

					global::System.Decimal MaintainedMarkupPercentage {set;}

					SalesOrderItemObjectState PreviousObjectState {set;}

					global::System.Decimal InitialMarkupPercentage {set;}

					NonSerializedInventoryItem ReservedFromInventoryItem {set;}

					Product Product {set;}

					ProductFeature ProductFeature {set;}

					global::System.Decimal QuantityRequestsShipping {set;}

					SalesOrderItemStatus PaymentStatuses {set;}

	}
	public interface SalesInvoiceType  : Enumeration 
	{
	}
	public interface WorkEffortGoodStandard  : UserInterfaceable 
	{
					Good Good {set;}

					global::System.Decimal? EstimatedCost {set;}

					global::System.Int32? EstimatedQuantity {set;}

	}
	public interface Passport  : UserInterfaceable, Searchable 
	{
					global::System.DateTime? IssueDate {set;}

					global::System.DateTime? ExpiriationDate {set;}

					global::System.String Number {set;}

	}
	public interface AmountDue  : AccessControlledObject, Searchable, UserInterfaceable 
	{
					global::System.Decimal? Amount {set;}

					PaymentMethod PaymentMethod {set;}

					global::System.DateTime? TransactionDate {set;}

					global::System.DateTime? BlockedForDunning {set;}

					global::System.Decimal? AmountVat {set;}

					BankAccount BankAccount {set;}

					global::System.DateTime? ReconciliationDate {set;}

					global::System.String InvoiceNumber {set;}

					global::System.Int32? DunningStep {set;}

					global::System.Int32? SubAccountNumber {set;}

					global::System.String TransactionNumber {set;}

					DebitCreditConstant Side {set;}

					Currency Currency {set;}

					global::System.Boolean? BlockedForPayment {set;}

					global::System.DateTime? DateLastReminder {set;}

					global::System.String YourReference {set;}

					global::System.String OurReference {set;}

					global::System.String ReconciliationNumber {set;}

					global::System.DateTime? DueDate {set;}

	}
	public interface WorkEffortTypeKind  : Enumeration 
	{
	}
	public interface OrderTerm  : UserInterfaceable, Searchable 
	{
					global::System.String TermValue {set;}

					TermType TermType {set;}

	}
	public interface CreditCardCompany  : UserInterfaceable 
	{
					global::System.String Name {set;}

	}
	public interface RequestForQuote  : Request 
	{
	}
	public interface PurchaseShipmentStatus  : UserInterfaceable 
	{
					PurchaseShipmentObjectState PurchaseShipmentObjectState {set;}

					global::System.DateTime? StartDateTime {set;}

	}
	public interface Cash  : PaymentMethod 
	{
					Person PersonResponsible {set;}

	}
	public interface PerformanceReview  : Searchable, UserInterfaceable, Commentable, SearchResult, Period 
	{
					Person Manager {set;}

					PayHistory PayHistory {set;}

					PayCheck BonusPayCheck {set;}

					PerformanceReviewItem PerformanceReviewItems {set;}

					Person Employee {set;}

					Position ResultingPosition {set;}

	}
	public interface DropShipmentObjectState  : ObjectState 
	{
	}
	public interface InvestmentAccount  : FinancialAccount 
	{
					global::System.String Name {set;}

	}
	public interface Colour  : Enumeration, ProductFeature 
	{
	}
	public interface PackageRevenue  : UserInterfaceable 
	{
					global::System.Decimal? Revenue {set;}

					global::System.Int32 Year {set;}

					global::System.Int32 Month {set;}

					Currency Currency {set;}

					global::System.String PackageName {set;}

					InternalOrganisation InternalOrganisation {set;}

					Package Package {set;}

	}
	public interface SalesOrderObjectState  : ObjectState 
	{
	}
	public interface Benefit  : SearchResult, UserInterfaceable, Searchable 
	{
					global::System.Decimal? EmployerPaidPercentage {set;}

					global::System.String Description {set;}

					global::System.String Name {set;}

					global::System.Decimal? AvailableTime {set;}

	}
	public interface EngineeringDocument  : Document 
	{
	}
	public interface VatReturnBox  : UserInterfaceable, AccessControlledObject 
	{
					global::System.String HeaderNumber {set;}

					global::System.String Description {set;}

	}
	public interface CommunicationEventPurpose  : Enumeration 
	{
	}
	public interface ShipmentRouteSegment  : UserInterfaceable 
	{
					global::System.Decimal? EndKilometers {set;}

					Facility FromFacility {set;}

					global::System.Decimal? StartKilometers {set;}

					ShipmentMethod ShipmentMethod {set;}

					global::System.DateTime? EstimatedStartDateTime {set;}

					Facility ToFacility {set;}

					global::System.DateTime? EstimatedArrivalDateTime {set;}

					Vehicle Vehicle {set;}

					global::System.DateTime? ActualArrivalDateTime {set;}

					global::System.DateTime? ActualStartDateTime {set;}

					Organisation Carrier {set;}

	}
	public interface VarianceReason  : Enumeration 
	{
	}
	public interface Phase  : WorkEffort 
	{
	}
	public interface WorkEffortStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					WorkEffortObjectState WorkEffortObjectState {set;}

	}
	public interface Salutation  : UserInterfaceable, Enumeration, UniquelyIdentifiable, AccessControlledObject 
	{
	}
	public interface PurchaseOrderStatus  : UserInterfaceable 
	{
					PurchaseOrderObjectState PurchaseOrderObjectState {set;}

					global::System.DateTime? StartDateTime {set;}

	}
	public interface PayrollPreference  : UserInterfaceable 
	{
					global::System.Decimal? Percentage {set;}

					global::System.String AccountNumber {set;}

					PaymentMethod PaymentMethod {set;}

					TimeFrequency TimeFrequency {set;}

					DeductionType DeductionType {set;}

					global::System.Decimal? Amount {set;}

	}
	public interface CustomerShipment  : Shipment 
	{
					CustomerShipmentStatus CurrentShipmentStatus {set;}

					CustomerShipmentObjectState PreviousObjectState {set;}

					global::System.Boolean? ReleasedManually {set;}

					CustomerShipmentObjectState CurrentObjectState {set;}

					CustomerShipmentStatus ShipmentStatuses {set;}

					PaymentMethod PaymentMethod {set;}

					global::System.Boolean? WithoutCharges {set;}

					global::System.Boolean? HeldManually {set;}

					global::System.Decimal ShipmentValue {set;}

	}
	public interface InternalOrganisationRevenue  : UserInterfaceable 
	{
					global::System.Int32 Month {set;}

					global::System.Int32 Year {set;}

					global::System.Decimal? Revenue {set;}

					Currency Currency {set;}

					global::System.String PartyName {set;}

					InternalOrganisation InternalOrganisation {set;}

	}
	public interface Package  : UniquelyIdentifiable, UserInterfaceable, Searchable 
	{
					global::System.String Name {set;}

	}
	public interface HazardousMaterialsDocument  : Document 
	{
	}
	public interface EmailCommunication  : CommunicationEvent 
	{
					EmailAddress Originator {set;}

					EmailAddress Addressees {set;}

					EmailAddress CarbonCopies {set;}

					EmailAddress BlindCopies {set;}

					EmailTemplate EmailTemplate {set;}

	}
	public interface CreditCard  : FinancialAccount, UserInterfaceable, Searchable 
	{
					global::System.String NameOnCard {set;}

					CreditCardCompany CreditCardCompany {set;}

					global::System.Int32 ExpirationYear {set;}

					global::System.Int32 ExpirationMonth {set;}

					global::System.String CardNumber {set;}

	}
	public interface OrganisationContactRelationship  : PartyRelationship 
	{
					Person Contact {set;}

					Organisation Organisation {set;}

					OrganisationContactKind ContactKind {set;}

	}
	public interface OrganisationContactKind  : UserInterfaceable, UniquelyIdentifiable 
	{
					global::System.String Description {set;}

	}
	public interface CustomerReturnStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					ShipmentReceipt ShipmentReceipt {set;}

					CustomerReturnObjectState CustomerReturnObjectState {set;}

	}
	public interface PerformanceReviewItem  : Commentable, UserInterfaceable 
	{
					RatingType RatingType {set;}

					PerformanceReviewItemType PerformanceReviewItemType {set;}

	}
	public interface UtilizationCharge  : PriceComponent 
	{
					global::System.Decimal? Quantity {set;}

					UnitOfMeasure UnitOfMeasure {set;}

	}
	public interface PartyPackageRevenue  : UserInterfaceable 
	{
					global::System.Int32 Month {set;}

					Package Package {set;}

					Party Party {set;}

					global::System.Decimal? Revenue {set;}

					global::System.Int32 Year {set;}

					global::System.String PartyPackageName {set;}

					Currency Currency {set;}

					InternalOrganisation InternalOrganisation {set;}

	}
	public interface PartyRelationshipStatus  : Enumeration 
	{
	}
	public interface ServiceTerritory  : UserInterfaceable, GeographicBoundaryComposite 
	{
					global::System.String Name {set;}

	}
	public interface DeliverableBasedService  : Service 
	{
	}
	public interface ProductModel  : Document 
	{
	}
	public interface Shelf  : Container 
	{
	}
	public interface RawMaterial  : Part 
	{
	}
	public interface EstimatedOtherCost  : EstimatedProductCost 
	{
	}
	public interface BudgetRevision  : Searchable, UserInterfaceable 
	{
					global::System.DateTime RevisionDate {set;}

	}
	public interface WorkEffortFixedAssetStandard  : UserInterfaceable 
	{
					global::System.Decimal? EstimatedCost {set;}

					global::System.Decimal? EstimatedDuration {set;}

					FixedAsset FixedAsset {set;}

					global::System.Int32? EstimatedQuantity {set;}

	}
	public interface PostalCode  : UserInterfaceable, GeographicBoundary 
	{
					global::System.String Code {set;}

	}
	public interface NonSerializedInventoryItemObjectState  : ObjectState 
	{
	}
	public interface ProfessionalAssignment  : UserInterfaceable, Period 
	{
					Person Professional {set;}

					EngagementItem EngagementItem {set;}

	}
	public interface TransferObjectState  : ObjectState 
	{
	}
	public interface PackageRevenueHistory  : UserInterfaceable 
	{
					InternalOrganisation InternalOrganisation {set;}

					global::System.Decimal? Revenue {set;}

					Package Package {set;}

					Currency Currency {set;}

	}
	public interface JournalEntryDetail  : AccessControlledObject, UserInterfaceable 
	{
					OrganisationGlAccount GeneralLedgerAccount {set;}

					global::System.Decimal? Amount {set;}

					global::System.Boolean? Debit {set;}

	}
	public interface TestingRequirement  : PartSpecification 
	{
	}
	public interface Case  : Searchable, UserInterfaceable, Transitional, UniquelyIdentifiable, SearchResult 
	{
					CaseStatus CurrentCaseStatus {set;}

					CaseStatus CaseStatuses {set;}

					global::System.DateTime? StartDate {set;}

					CaseObjectState CurrentObjectState {set;}

					global::System.String Description {set;}

					CaseObjectState PreviousObjectState {set;}

	}
	public interface Capitalization  : InternalAccountingTransaction 
	{
	}
	public interface PurchaseReturn  : Shipment 
	{
					PurchaseReturnStatus CurrentShipmentStatus {set;}

					PurchaseReturnObjectState CurrentObjectState {set;}

					PurchaseReturnStatus ShipmentStatuses {set;}

					PurchaseReturnObjectState PreviousObjectState {set;}

	}
	public interface WorkEffortPartStandard  : UserInterfaceable 
	{
					Part Part {set;}

					global::System.Decimal? EstimatedCost {set;}

					global::System.Int32? EstimatedQuantity {set;}

	}
	public interface SurchargeComponent  : PriceComponent 
	{
					global::System.Decimal Percentage {set;}

	}
	public interface Bank  : UserInterfaceable, Searchable 
	{
					Media Logo {set;}

					global::System.String Bic {set;}

					global::System.String SwiftCode {set;}

					Country Country {set;}

					global::System.String Name {set;}

	}
	public interface ProductRevenue  : UserInterfaceable 
	{
					global::System.Decimal? Revenue {set;}

					global::System.String ProductName {set;}

					Currency Currency {set;}

					global::System.Int32 Year {set;}

					Product Product {set;}

					global::System.Int32 Month {set;}

					InternalOrganisation InternalOrganisation {set;}

	}
	public interface DisbursementAccountingTransaction  : ExternalAccountingTransaction 
	{
					Disbursement Disbursement {set;}

	}
	public interface OrderValue  : UserInterfaceable 
	{
					global::System.Decimal? ThroughAmount {set;}

					global::System.Decimal? FromAmount {set;}

	}
	public interface VatTariff  : Enumeration 
	{
	}
	public interface Obligation  : ExternalAccountingTransaction 
	{
	}
	public interface SalesInvoiceObjectState  : ObjectState 
	{
	}
	public interface VatRate  : UserInterfaceable 
	{
					VatCalculationMethod VatCalculationMethod {set;}

					VatReturnBox VatReturnBoxes {set;}

					global::System.Decimal Rate {set;}

					OrganisationGlAccount VatPayableAccount {set;}

					Organisation TaxAuthority {set;}

					VatRateUsage VatRateUsage {set;}

					VatRatePurchaseKind VatRatePurchaseKind {set;}

					VatTariff VatTariff {set;}

					TimeFrequency PaymentFrequency {set;}

					OrganisationGlAccount VatToPayAccount {set;}

					EuSalesListType EuSalesListType {set;}

					OrganisationGlAccount VatToReceiveAccount {set;}

					OrganisationGlAccount VatReceivableAccount {set;}

					global::System.Boolean? ReverseCharge {set;}

	}
	public interface ProfessionalServicesRelationship  : PartyRelationship 
	{
					Person Professional {set;}

					Organisation ProfessionalServicesProvider {set;}

	}
	public interface RecurringCharge  : PriceComponent 
	{
					TimeFrequency TimeFrequency {set;}

	}
	public interface FinancialTerm  : AgreementTerm 
	{
	}
	public interface RequirementStatus  : UserInterfaceable 
	{
					RequirementObjectState RequirementObjectState {set;}

					global::System.DateTime? StartDateTime {set;}

	}
	public interface PurchaseInvoiceItemObjectState  : ObjectState 
	{
	}
	public interface InvoiceTerm  : UserInterfaceable, AgreementTerm 
	{
	}
	public interface DropShipment  : Shipment 
	{
					DropShipmentStatus ShipmentStatuses {set;}

					DropShipmentStatus CurrentShipmentStatus {set;}

					DropShipmentObjectState PreviousObjectState {set;}

					DropShipmentObjectState CurrentObjectState {set;}

	}
	public interface SalesInvoiceItem  : InvoiceItem 
	{
					ProductFeature ProductFeature {set;}

					SalesInvoiceItemObjectState CurrentObjectState {set;}

					global::System.Decimal? RequiredProfitMargin {set;}

					global::System.Decimal InitialMarkupPercentage {set;}

					global::System.Decimal MaintainedMarkupPercentage {set;}

					Product Product {set;}

					global::System.Decimal UnitPurchasePrice {set;}

					SalesInvoiceItemStatus InvoiceItemStatuses {set;}

					SalesOrderItem SalesOrderItem {set;}

					SalesInvoiceItemType SalesInvoiceItemType {set;}

					Person SalesRep {set;}

					global::System.Decimal InitialProfitMargin {set;}

					SalesInvoiceItemStatus CurrentInvoiceItemStatus {set;}

					global::System.Decimal MaintainedProfitMargin {set;}

					TimeEntry TimeEntries {set;}

					global::System.Decimal? RequiredMarkupPercentage {set;}

					SalesInvoiceItemObjectState PreviousObjectState {set;}

	}
	public interface OrderQuantityBreak  : UserInterfaceable 
	{
					global::System.Decimal? ThroughAmount {set;}

					global::System.Decimal? FromAmount {set;}

	}
	public interface Event 
	{
					global::System.Boolean? RegistrationRequired {set;}

					global::System.String Link {set;}

					global::System.String Location {set;}

					global::System.String Text {set;}

					global::System.String AnnouncementText {set;}

					global::System.DateTime? From {set;}

					Locale Locale {set;}

					global::System.String Title {set;}

					Media Photo {set;}

					global::System.Boolean? Announce {set;}

					global::System.DateTime? To {set;}

	}
	public interface ClientRelationship  : PartyRelationship 
	{
					Party Client {set;}

					InternalOrganisation InternalOrganisation {set;}

	}
	public interface PurchaseOrderItem  : OrderItem 
	{
					PurchaseOrderItemStatus OrderItemStatuses {set;}

					PurchaseOrderItemObjectState CurrentObjectState {set;}

					PurchaseOrderItemStatus ShipmentStatuses {set;}

					PurchaseOrderItemStatus PaymentStatuses {set;}

					global::System.Decimal QuantityReceived {set;}

					PurchaseOrderItemStatus CurrentShipmentStatus {set;}

					Product Product {set;}

					PurchaseOrderItemStatus CurrentOrderItemStatus {set;}

					PurchaseOrderItemStatus CurrentPaymentStatus {set;}

					PurchaseOrderItemObjectState PreviousObjectState {set;}

					Part Part {set;}

	}
	public interface WorkEffortAssignmentRate  : UserInterfaceable 
	{
					RateType RateType {set;}

					WorkEffortPartyAssignment WorkEffortPartyAssignment {set;}

	}
	public interface EuSalesListType  : Enumeration 
	{
	}
	public interface PurchaseOrderItemObjectState  : ObjectState 
	{
	}
	public interface Province  : CityBound, GeographicBoundary, CountryBound, UserInterfaceable 
	{
					global::System.String Name {set;}

	}
	public interface InventoryItemVariance  : Searchable, UserInterfaceable, Commentable 
	{
					global::System.Int32? Quantity {set;}

					ItemVarianceAccountingTransaction ItemVarianceAccountingTransaction {set;}

					global::System.DateTime? InventoryDate {set;}

					VarianceReason Reason {set;}

	}
	public interface PositionResponsibility  : Commentable, UserInterfaceable 
	{
					Position Position {set;}

					Responsibility Responsibility {set;}

	}
	public interface DeliverableType  : Enumeration 
	{
	}
	public interface SubAssembly  : Part 
	{
	}
	public interface RequirementObjectState  : ObjectState 
	{
	}
	public interface WorkFlow  : WorkEffort 
	{
	}
	public interface Task  : WorkEffort 
	{
	}
	public interface ResourceRequirement  : Requirement 
	{
					global::System.String Duties {set;}

					global::System.Decimal? NumberOfPositions {set;}

					global::System.DateTime? RequiredStartDate {set;}

					NeededSkill NeededSkills {set;}

					global::System.DateTime? RequiredEndDate {set;}

	}
	public interface BudgetItem  : UserInterfaceable 
	{
					global::System.String Purpose {set;}

					global::System.String Justification {set;}

					BudgetItem Children {set;}

					global::System.Decimal Amount {set;}

	}
	public interface InternalRequirement  : Requirement 
	{
	}
	public interface PositionReportingStructure  : UserInterfaceable, Commentable 
	{
					global::System.Boolean? Primary {set;}

					Position ManagedByPosition {set;}

					Position Position {set;}

	}
	public interface Partnership  : PartyRelationship 
	{
					InternalOrganisation InternalOrganisation {set;}

					Organisation Partner {set;}

	}
	public interface OperatingBudget  : Budget 
	{
	}
	public interface Bin  : Container 
	{
	}
	public interface ManufacturingConfiguration  : InventoryItemConfiguration 
	{
	}
	public interface ProfessionalPlacement  : EngagementItem 
	{
	}
	public interface SalesRepCommission  : UserInterfaceable 
	{
					global::System.Decimal? Commission {set;}

					InternalOrganisation InternalOrganisation {set;}

					global::System.String SalesRepName {set;}

					global::System.Int32? Month {set;}

					global::System.Int32? Year {set;}

					Currency Currency {set;}

					Person SalesRep {set;}

	}
	public interface Deduction  : UserInterfaceable 
	{
					DeductionType DeductionType {set;}

					global::System.Decimal Amount {set;}

	}
	public interface CaseStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					CaseObjectState CaseObjectState {set;}

	}
	public interface DiscountComponent  : PriceComponent 
	{
					global::System.Decimal Percentage {set;}

	}
	public interface OrganisationUnit  : Enumeration 
	{
	}
	public interface PartSubstitute  : Commentable, UserInterfaceable 
	{
					Part SubstitutionPart {set;}

					Ordinal Preference {set;}

					global::System.DateTime? FromDate {set;}

					global::System.Int32? Quantity {set;}

					Part Part {set;}

	}
	public interface GoodOrderItem  : EngagementItem 
	{
					global::System.Decimal? Price {set;}

					global::System.Int32? Quantity {set;}

	}
	public interface VolumeUsage  : DeploymentUsage 
	{
					global::System.Decimal? Quantity {set;}

					UnitOfMeasure UnitOfMeasure {set;}

	}
	public interface ProductQuote  : Quote 
	{
	}
	public interface TransferStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					TransferObjectState TransferObjectState {set;}

	}
	public interface State  : CityBound, GeographicBoundary, AccessControlledObject, CountryBound 
	{
					global::System.String Name {set;}

	}
	public interface JournalEntryNumber  : UserInterfaceable, AccessControlledObject 
	{
					JournalType JournalType {set;}

					global::System.Int32? Number {set;}

					global::System.Int32? Year {set;}

	}
	public interface Tolerance  : PartSpecification 
	{
	}
	public interface EngineeringChange  : Searchable, Transitional, UserInterfaceable, SearchResult 
	{
					Person Requestor {set;}

					Person Authorizer {set;}

					global::System.String Description {set;}

					EngineeringChangeObjectState PreviousObjectState {set;}

					Person Designer {set;}

					PartSpecification PartSpecifications {set;}

					PartBillOfMaterial PartBillOfMaterials {set;}

					EngineeringChangeObjectState CurrentObjectState {set;}

					EngineeringChangeStatus EngineeringChangeStatuses {set;}

					Person Tester {set;}

					EngineeringChangeStatus CurrentEngineeringChangeStatus {set;}

	}
	public interface VatRatePurchaseKind  : Enumeration 
	{
	}
	public interface EmailTemplate  : UserInterfaceable 
	{
					global::System.String Description {set;}

					global::System.String BodyTemplate {set;}

					global::System.String SubjectTemplate {set;}

	}
	public interface Threshold  : AgreementTerm 
	{
	}
	public interface EmploymentApplicationStatus  : Enumeration 
	{
	}
	public interface Qualification  : Enumeration, Searchable 
	{
	}
	public interface InternalOrganisation  : Party 
	{
					global::System.String PurchaseOrderNumberPrefix {set;}

					global::System.String TransactionReferenceNumber {set;}

					global::System.Int32 NextPurchaseInvoiceNumber {set;}

					global::System.Int32 NextQuoteNumber {set;}

					JournalEntryNumber JournalEntryNumbers {set;}

					Country EuListingState {set;}

					AccountingPeriod ActualAccountingPeriod {set;}

					InvoiceSequence InvoiceSequence {set;}

					PaymentMethod ActivePaymentMethods {set;}

					StringTemplate PurchaseShipmentTemplates {set;}

					global::System.Decimal? MaximumAllowedPaymentDifference {set;}

					Media LogoImage {set;}

					CostCenterSplitMethod CostCenterSplitMethod {set;}

					LegalForm LegalForm {set;}

					AccountingPeriod AccountingPeriods {set;}

					GeneralLedgerAccount SalesPaymentDifferencesAccount {set;}

					global::System.String Name {set;}

					global::System.Int32 NextPurchaseOrderNumber {set;}

					global::System.String PurchaseTransactionReferenceNumber {set;}

					global::System.Int32 FiscalYearStartMonth {set;}

					StringTemplate PurchaseOrderTemplates {set;}

					global::System.Int32 NextIncomingShipmentNumber {set;}

					CostOfGoodsSoldMethod CostOfGoodsSoldMethod {set;}

					global::System.Int32 NextSubAccountNumber {set;}

					Role EmployeeRoles {set;}

					global::System.Boolean? VatDeactivated {set;}

					global::System.Int32 FiscalYearStartDay {set;}

					GeneralLedgerAccount GeneralLedgerAccounts {set;}

					GeneralLedgerAccount RetainedEarningsAccount {set;}

					StringTemplate PackagingSlipTemplates {set;}

					Party Customers {set;}

					global::System.String PurchaseInvoiceNumberPrefix {set;}

					StringTemplate PickListTemplates {set;}

					GeneralLedgerAccount SalesPaymentDiscountDifferencesAccount {set;}

					AccountingTransactionNumber AccountingTransactionNumbers {set;}

					StringTemplate QuoteTemplates {set;}

					global::System.String TransactionReferenceNumberPrefix {set;}

					Currency PreviousCurrency {set;}

					GeneralLedgerAccount PurchasePaymentDifferencesAccount {set;}

					GeneralLedgerAccount SuspenceAccount {set;}

					GeneralLedgerAccount NetIncomeAccount {set;}

					global::System.Boolean? DoAccounting {set;}

					Facility DefaultFacility {set;}

					GeneralLedgerAccount PurchasePaymentDiscountDifferencesAccount {set;}

					Party Suppliers {set;}

					global::System.Int32? NextAccountingTransactionNumber {set;}

					global::System.String QuoteNumberPrefix {set;}

					global::System.String PurchaseTransactionReferenceNumberPrefix {set;}

					global::System.String TaxNumber {set;}

					GeneralLedgerAccount CalculationDifferencesAccount {set;}

					PaymentMethod PaymentMethods {set;}

					global::System.String IncomingShipmentNumberPrefix {set;}

	}
	public interface OwnBankAccount  : PaymentMethod, FinancialAccount 
	{
					BankAccount BankAccount {set;}

	}
	public interface PartyContactMechanism  : Commentable, UserInterfaceable 
	{
					ContactMechanismPurpose ContactPurpose {set;}

					ContactMechanism ContactMechanism {set;}

					global::System.Boolean UseAsDefault {set;}

					global::System.Boolean? NonSolicitationIndicator {set;}

	}
	public interface PartyRelationshipPriority  : Enumeration 
	{
	}
	public interface CostCenterSplitMethod  : Enumeration 
	{
	}
	public interface EstimatedMaterialCost  : EstimatedProductCost 
	{
	}
	public interface QuoteTerm  : Searchable, UserInterfaceable 
	{
					global::System.String TermValue {set;}

					TermType TermType {set;}

	}
	public interface Transfer  : Shipment 
	{
					TransferObjectState PreviousObjectState {set;}

					TransferObjectState CurrentObjectState {set;}

					TransferStatus CurrentShipmentStatus {set;}

					TransferStatus ShipmentStatuses {set;}

	}
	public interface RevenueQuantityBreak  : UserInterfaceable 
	{
					global::System.Decimal? Through {set;}

					global::System.Decimal? From {set;}

	}
	public interface GeneralLedgerAccountType  : UserInterfaceable, AccessControlledObject 
	{
					global::System.String Description {set;}

	}
	public interface SerializedInventoryItemObjectState  : ObjectState 
	{
	}
	public interface FaceToFaceCommunication  : CommunicationEvent 
	{
					PostalAddress Location {set;}

					Person Participants {set;}

	}
	public interface BudgetReview  : Searchable, Commentable, UserInterfaceable 
	{
					global::System.DateTime ReviewDate {set;}

					global::System.String Description {set;}

	}
	public interface EngineeringChangeStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					EngineeringChangeObjectState EngineeringChangeObjectState {set;}

	}
	public interface ProductQuality  : ProductFeature, Enumeration 
	{
	}
	public interface Disbursement  : Payment 
	{
	}
	public interface Research  : WorkEffort 
	{
	}
	public interface Journal  : Searchable, UserInterfaceable, AccessControlledObject 
	{
					global::System.Boolean? UseAsDefault {set;}

					OrganisationGlAccount GlPaymentInTransit {set;}

					JournalType JournalType {set;}

					global::System.String Description {set;}

					global::System.Boolean? BlockUnpaidTransactions {set;}

					OrganisationGlAccount ContraAccount {set;}

					InternalOrganisation InternalOrganisation {set;}

					JournalType PreviousJournalType {set;}

					OrganisationGlAccount PreviousContraAccount {set;}

					JournalEntry JournalEntries {set;}

					global::System.Boolean? CloseWhenInBalance {set;}

	}
	public interface ShipmentItem  : UserInterfaceable 
	{
					global::System.Decimal? Quantity {set;}

					Part Part {set;}

					global::System.String ContentsDescription {set;}

					Document Documents {set;}

					global::System.Decimal QuantityShipped {set;}

					ShipmentItem InResponseToShipmentItems {set;}

					InventoryItem InventoryItems {set;}

					ProductFeature ProductFeatures {set;}

					InvoiceItem InvoiceItems {set;}

					Good Good {set;}

	}
	public interface EmploymentAgreement  : Agreement 
	{
	}
	public interface ManufacturerSuggestedRetailPrice  : PriceComponent 
	{
	}
	public interface NewsItem  : Searchable, SearchResult, UserInterfaceable 
	{
					global::System.Boolean? IsPublished {set;}

					global::System.String Title {set;}

					global::System.Int32? DisplayOrder {set;}

					Locale Locale {set;}

					global::System.String LongText {set;}

					global::System.String Text {set;}

					global::System.DateTime? Date {set;}

					global::System.Boolean? Announcement {set;}

	}
	public interface PartyBenefit  : UserInterfaceable 
	{
					TimeFrequency TimeFrequency {set;}

					global::System.Decimal? Cost {set;}

					global::System.Decimal? ActualEmployerPaidPercentage {set;}

					Benefit Benefit {set;}

					global::System.Decimal? ActualAvailableTime {set;}

					Employment Employment {set;}

	}
	public interface PostalAddress  : ContactMechanism, AccessControlledObject, GeoLocatable 
	{
					GeographicBoundary GeographicBoundaries {set;}

					global::System.String Address3 {set;}

					Country Country {set;}

					global::System.String Address2 {set;}

					City City {set;}

					global::System.String Address1 {set;}

					PostalBoundary PostalBoundary {set;}

					PostalCode PostalCode {set;}

					global::System.String Directions {set;}

					global::System.String FormattedFullAddress {set;}

	}
	public interface PackageQuantityBreak  : UserInterfaceable 
	{
					global::System.Int32? From {set;}

					global::System.Int32? Through {set;}

	}
	public interface SubContractorRelationship  : PartyRelationship 
	{
					Party Contractor {set;}

					Party SubContractor {set;}

	}
	public interface ClientAgreement  : Agreement 
	{
	}
	public interface Store  : UniquelyIdentifiable, UserInterfaceable 
	{
					global::System.Decimal? ShipmentThreshold {set;}

					StringTemplate SalesInvoiceTemplates {set;}

					global::System.String OutgoingShipmentNumberPrefix {set;}

					global::System.String SalesInvoiceNumberPrefix {set;}

					global::System.Int32 PaymentGracePeriod {set;}

					Media LogoImage {set;}

					global::System.Int32 PaymentNetDays {set;}

					Facility DefaultFacility {set;}

					global::System.String Name {set;}

					StringTemplate SalesOrderTemplates {set;}

					global::System.Decimal? CreditLimit {set;}

					ShipmentMethod DefaultShipmentMethod {set;}

					Carrier DefaultCarrier {set;}

					global::System.Decimal? OrderThreshold {set;}

					PaymentMethod DefaultPaymentMethod {set;}

					global::System.Int32 NextSalesOrderNumber {set;}

					InternalOrganisation Owner {set;}

					FiscalYearInvoiceNumber FiscalYearInvoiceNumbers {set;}

					PaymentMethod PaymentMethods {set;}

					global::System.String SalesOrderNumberPrefix {set;}

					global::System.Int32? NextOutgoingShipmentNumber {set;}

					StringTemplate CustomerShipmentTemplates {set;}

					global::System.Int32 NextSalesInvoiceNumber {set;}

	}
	public interface Lot  : Searchable, UserInterfaceable 
	{
					global::System.DateTime? ExpirationDate {set;}

					global::System.Int32? Quantity {set;}

					global::System.String LotNumber {set;}

	}
	public interface WorkEffortSkillStandard  : UserInterfaceable 
	{
					Skill Skill {set;}

					global::System.Int32? EstimatedNumberOfPeople {set;}

					global::System.Decimal? EstimatedDuration {set;}

					global::System.Decimal? EstimatedCost {set;}

	}
	public interface TimeAndMaterialsService  : Service 
	{
	}
	public interface Equipment  : FixedAsset 
	{
	}
	public interface RequestItem  : UserInterfaceable, Commentable 
	{
					global::System.String Description {set;}

					global::System.Int32? Quantity {set;}

					Requirement Requirements {set;}

					Deliverable Deliverable {set;}

					ProductFeature ProductFeature {set;}

					NeededSkill NeededSkill {set;}

					Product Product {set;}

					global::System.Decimal? MaximumAllowedPrice {set;}

					global::System.DateTime? RequiredByDate {set;}

	}
	public interface SalesChannel  : Searchable, Enumeration 
	{
	}
	public interface CustomerRequirement  : Requirement 
	{
	}
	public interface Property  : FixedAsset 
	{
	}
	public interface ConstraintSpecification  : PartSpecification 
	{
	}
	public interface DesiredProductFeature  : UserInterfaceable, Searchable 
	{
					global::System.Boolean? Required {set;}

					ProductFeature ProductFeature {set;}

	}
	public interface SalesOrderItemStatus  : UserInterfaceable 
	{
					SalesOrderItemObjectState SalesOrderItemObjectState {set;}

					global::System.DateTime? StartDateTime {set;}

	}
	public interface ActivityUsage  : DeploymentUsage 
	{
					global::System.Decimal? Quantity {set;}

					UnitOfMeasure UnitOfMeasure {set;}

	}
	public interface Program  : WorkEffort 
	{
	}
	public interface CommunicationEventStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					CommunicationEventObjectState CommunicationEventObjectState {set;}

	}
	public interface AgreementSection  : AgreementItem 
	{
	}
	public interface Good  : Product 
	{
					global::System.Decimal AvailableToPromise {set;}

					Media Thumbnail {set;}

					InventoryItemKind InventoryItemKind {set;}

					global::System.String BarCode {set;}

					FinishedGood FinishedGood {set;}

					global::System.String Sku {set;}

					global::System.String ArticleNumber {set;}

					global::System.String ManufacturerId {set;}

					Product ProductSubstitutions {set;}

					Product ProductIncompatibilities {set;}

					Media Photo {set;}

	}
	public interface EngineeringChangeObjectState  : ObjectState 
	{
	}
	public interface AccountingTransactionDetail  : UserInterfaceable 
	{
					AccountingTransactionDetail AssociatedWith {set;}

					OrganisationGlAccountBalance OrganisationGlAccountBalance {set;}

					global::System.Decimal Amount {set;}

					global::System.Boolean Debit {set;}

	}
	public interface County  : GeographicBoundary, CityBound, UserInterfaceable 
	{
					global::System.String Name {set;}

					State State {set;}

	}
	public interface ShippingAndHandlingCharge  : OrderAdjustment 
	{
	}
	public interface PerformanceReviewItemType  : Enumeration 
	{
	}
	public interface PostalBoundary  : UserInterfaceable 
	{
					global::System.String PostalCode {set;}

					global::System.String Locality {set;}

					Country Country {set;}

					global::System.String Region {set;}

	}
	public interface ProductCategory  : UserInterfaceable, Searchable, SearchResult, UniquelyIdentifiable 
	{
					Package Package {set;}

					global::System.String Code {set;}

					Media NoImageAvailableImage {set;}

					ProductCategory Parents {set;}

					ProductCategory Children {set;}

					global::System.String Description {set;}

					global::System.String Name {set;}

					Media CategoryImage {set;}

					ProductCategory Ancestors {set;}

	}
	public interface RequestForInformation  : Request 
	{
	}
	public interface VatForm  : AccessControlledObject, UserInterfaceable 
	{
					global::System.String Name {set;}

					VatReturnBox VatReturnBoxes {set;}

	}
	public interface BudgetRevisionImpact  : UserInterfaceable 
	{
					BudgetItem BudgetItem {set;}

					global::System.String Reason {set;}

					global::System.Boolean? Deleted {set;}

					global::System.Boolean? Added {set;}

					global::System.Decimal? RevisedAmount {set;}

					BudgetRevision BudgetRevision {set;}

	}
	public interface TemplatePurpose  : Enumeration 
	{
	}
	public interface WebSiteCommunication  : CommunicationEvent 
	{
					Party Originator {set;}

					Party Receiver {set;}

	}
	public interface Withdrawal  : FinancialAccountTransaction 
	{
					Disbursement Disbursement {set;}

	}
	public interface Deployment  : Searchable, UserInterfaceable, Period, SearchResult 
	{
					Good ProductOffering {set;}

					DeploymentUsage DeploymentUsage {set;}

					SerializedInventoryItem SerializedInventoryItem {set;}

	}
	public interface PayCheck  : Payment 
	{
					Deduction Deductions {set;}

					Employment Employment {set;}

	}
	public interface MaritalStatus  : Enumeration 
	{
	}
	public interface Manifest  : Document 
	{
	}
	public interface ExportDocument  : Document 
	{
	}
	public interface CustomerShipmentStatus  : UserInterfaceable 
	{
					CustomerShipmentObjectState CustomerShipmentObjectState {set;}

					global::System.DateTime? StartDateTime {set;}

	}
	public interface ExpenseEntry  : ServiceEntry 
	{
					global::System.Decimal? Amount {set;}

	}
	public interface PartSpecificationStatus  : UserInterfaceable 
	{
					global::System.DateTime? StartDateTime {set;}

					PartSpecificationObjectState PartSpecificationObjectState {set;}

	}
	public interface DistributionChannelRelationship  : PartyRelationship 
	{
					InternalOrganisation InternalOrganisation {set;}

					Organisation Distributor {set;}

	}
	public interface CustomerShipmentObjectState  : ObjectState 
	{
	}
	public interface GenderType  : Enumeration 
	{
	}
	public interface Office  : Facility 
	{
	}
	public interface EmailAddress  : ElectronicAddress 
	{
	}
	public interface WorkEffortInventoryAssignment  : UserInterfaceable 
	{
					WorkEffort Assignment {set;}

					InventoryItem InventoryItem {set;}

					global::System.Int32? Quantity {set;}

	}
	public interface CommunicationEventObjectState  : ObjectState 
	{
	}
	public interface City  : GeographicBoundary, UserInterfaceable, CountryBound 
	{
					global::System.String Name {set;}

					State State {set;}

	}
	public interface PickListObjectState  : ObjectState 
	{
	}
	public interface MaterialsUsage  : ServiceEntry 
	{
					global::System.Decimal? Amount {set;}

	}
	public interface EmploymentTerminationReason  : Enumeration, Searchable 
	{
	}
	public interface WorkEffortObjectState  : ObjectState 
	{
	}
	public interface TimePeriodUsage  : DeploymentUsage 
	{
	}
	public interface BudgetObjectState  : ObjectState 
	{
	}
	public interface WorkRequirement  : Requirement 
	{
					FixedAsset FixedAsset {set;}

					Deliverable Deliverable {set;}

					Product Product {set;}

	}
	public interface Fee  : OrderAdjustment 
	{
	}
	public interface PhoneCommunication  : CommunicationEvent 
	{
					Person Receivers {set;}

					Person Caller {set;}

	}
	public interface ProductDeliverySkillRequirement  : UserInterfaceable 
	{
					global::System.DateTime? StartedUsingDate {set;}

					Service Service {set;}

					Skill Skill {set;}

	}
	public interface SalesRepProductCategoryRevenue  : UserInterfaceable 
	{
					global::System.Int32 Month {set;}

					global::System.String SalesRepName {set;}

					InternalOrganisation InternalOrganisation {set;}

					ProductCategory ProductCategory {set;}

					Currency Currency {set;}

					global::System.Decimal? Revenue {set;}

					global::System.Int32 Year {set;}

					Person SalesRep {set;}

	}
	public interface ServiceFeature  : Enumeration, ProductFeature 
	{
	}
	public interface PartyProductRevenueHistory  : UserInterfaceable 
	{
					global::System.Decimal? Revenue {set;}

					Party Party {set;}

					Product Product {set;}

					global::System.Decimal? Quantity {set;}

					InternalOrganisation InternalOrganisation {set;}

					Currency Currency {set;}

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

					TemplatePurpose TemplatePurpose {set;}

	}
	public interface Singleton  : UserInterfaceable 
	{
					PrintQueue DefaultPrintQueue {set;}

					Locale DefaultLocale {set;}

					Locale Locales {set;}

					SecurityToken AdministratorSecurityToken {set;}

					User Guest {set;}

					SecurityToken DefaultSecurityToken {set;}

					Currency DefaultCurrency {set;}

					Media NoImageAvailableImage {set;}

					InternalOrganisation DefaultInternalOrganisation {set;}

	}
	public interface Locale  : UserInterfaceable 
	{
					global::System.String Name {set;}

					Language Language {set;}

					Country Country {set;}

	}
	public interface SearchFragment  : Derivable 
	{
					global::System.String LowerCaseText {set;}

	}
	public interface Language  : UserInterfaceable, Searchable 
	{
					global::System.String Name {set;}

					global::System.String IsoCode {set;}

					LocalisedText LocalisedNames {set;}

	}
	public interface SearchData  : Derivable 
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

					Party Party {set;}

	}
	public interface MediaContent  : Derivable 
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
	public interface Login  : Derivable 
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
	public interface Country  : UserInterfaceable, Searchable, GeographicBoundary, CityBound 
	{
					Currency Currency {set;}

					global::System.String Name {set;}

					LocalisedText LocalisedNames {set;}

					global::System.String IsoCode {set;}

					VatRate VatRates {set;}

					global::System.Int32? IbanLength {set;}

					global::System.Boolean? EuMemberState {set;}

					global::System.String TelephoneCode {set;}

					global::System.String IbanRegex {set;}

					VatForm VatForm {set;}

					global::System.String UriExtension {set;}

	}
	public interface AccessControl  : UserInterfaceable 
	{
					UserGroup SubjectGroups {set;}

					User Subjects {set;}

					SecurityToken Objects {set;}

					Role Role {set;}

	}
	public interface Person  : User, AccessControlledObject, UniquelyIdentifiable, SearchResult, UserInterfaceable, Searchable, Party 
	{
					global::System.String LastName {set;}

					global::System.String MiddleName {set;}

					global::System.String FirstName {set;}

					Salutation Salutation {set;}

					global::System.Decimal YTDCommission {set;}

					Citizenship Citizenship {set;}

					Employment CurrentEmployment {set;}

					global::System.Decimal LastYearsCommission {set;}

					PersonalTitle Titles {set;}

					global::System.String MothersMaidenName {set;}

					global::System.DateTime? BirthDate {set;}

					global::System.Decimal? Height {set;}

					PersonTraining PersonTrainings {set;}

					GenderType Gender {set;}

					global::System.Int32? Weight {set;}

					Hobby Hobbies {set;}

					global::System.Int32? TotalYearsWorkExperience {set;}

					Passport Passports {set;}

					MaritalStatus MaritalStatus {set;}

					Media Picture {set;}

					global::System.String SocialSecurityNumber {set;}

					global::System.DateTime? DeceasedDate {set;}

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
	public interface Currency  : UserInterfaceable, IUnitOfMeasure 
	{
					global::System.String IsoCode {set;}

					global::System.String Name {set;}

					global::System.String Symbol {set;}

					LocalisedText LocalisedNames {set;}

	}
}