--create database SalesSystemPDI2024;
--drop database SalesSystemPDI2024;

create table AddressTypes(
	AddressTypeID bigint identity(1,1),
	Description varchar(200) NOT NULL,
	CONSTRAINT PK_AddressTypes primary key(AddressTypeID)
);

create table Addresses(
	AddressID bigint identity(1,1),
	AddressNickname varchar(50) NOT NULL,
	AddressTypeID bigint,
	Street varchar(150) NOT NULL,
	Number varchar(20) NOT NULL,
	Complement varchar(150),
	City varchar(100) NOT NULL,
	StateOrDistrict varchar(100) NOT NULL,
	PostalCode varchar(50) NOT NULL,
	Country varchar(100) NOT NULL,
	CONSTRAINT PK_Addresses primary key(AddressID),
	CONSTRAINT FK_Addresses_AddressTypes foreign key(AddressTypeID) REFERENCES AddressTypes(AddressTypeID)
);

create table CustomersSuppliers(
	CustomersSuppliersID bigint identity(1,1),
	FiscalID varchar(20) NOT NULL,
	Name varchar(200) NOT NULL,
	Email varchar(300) NOT NULL,
	Phone varchar(20) NOT NULL,
	IsCustomer bit,
	IsSupplier bit,
	IsCustomerRepresentative bit,
	IsSuplierRepresentative bit,
	IsParent bit,
	IsSubsidiary bit,
	IsParentRepresentative bit,
	IsSubsidiaryRepresentative bit,
	CONSTRAINT PK_CustomersSuppliers primary key(CustomersSuppliersID)
);

create table NaturalPeople(
	CustomersSuppliersID bigint,
	IdentityCard VARCHAR(20) NOT NULL,
	DateOfBirth date NOT NULL,
	Occupation VARCHAR(100),
	CONSTRAINT PK_NaturalPeople primary key(CustomersSuppliersID),
	CONSTRAINT FK_NaturalPeople_CustomersSuppliers foreign key(CustomersSuppliersID) REFERENCES CustomersSuppliers(CustomersSuppliersID)
);

create table CorporateEntities(
	CustomersSuppliersID bigint,
	FoundationDate date,
	BusinessKindDescription varchar(200) NOT NULL,
	LegalRepresentativeFiscalID bigint NOT NULL,
	CONSTRAINT PK_CorporateEntities primary key(CustomersSuppliersID),
	CONSTRAINT FK_CorporateEntities_CustomersSuppliers foreign key(CustomersSuppliersID) REFERENCES CustomersSuppliers(CustomersSuppliersID),
	CONSTRAINT FK_CorporateEntities_CustomersSuppliers_LegalRepresentativeFiscalID foreign key(LegalRepresentativeFiscalID) REFERENCES CustomersSuppliers(CustomersSuppliersID)
);

create table AddressesList(
	AddressesListID bigint identity(1,1),
	CustomersSuppliersID bigint NOT NULL,
	AddressID bigint NOT NULL,
	CONSTRAINT UC_AddressesListAddressID UNIQUE (AddressID),
	CONSTRAINT FK_AddressesList_CustomersSuppliers foreign key (CustomersSuppliersID) REFERENCES CustomersSuppliers(CustomersSuppliersID),
	CONSTRAINT FK_AddressesList_Addresses foreign key (AddressID) REFERENCES Addresses(AddressID)
);

create table ProductsListItemStatuses(
	StatusID bigint identity(1,1),
	Description varchar(200) NOT NULL,
	CONSTRAINT PK_ProductsListItemStatuses primary key(StatusID)
);

create table OrderStatuses(
	StatusID bigint identity(1,1),
	Description varchar(200) NOT NULL,
	CONSTRAINT PK_OrderStatuses primary key(StatusID)
);

create table ShipmentStatuses(
	StatusID bigint identity(1,1),
	Description varchar(200) NOT NULL,
	CONSTRAINT PK_ShipmentStatuses primary key(StatusID)
);

create table BaseUnitsOfMeasurement(
	BaseUnitOfMeasurementID bigint identity(1,1),
	UnitName varchar(200) NOT NULL,
	UnitAbbreviation varchar(5),
	CONSTRAINT PK_BaseUnitsOfMeasurement primary key(BaseUnitOfMeasurementID)
);

create table BaseCurrencies(
	BaseCurrencyID bigint identity(1,1),
	CurrencyName varchar(200) NOT NULL,
	CurrencyAbbreviation varchar(5),
	CONSTRAINT PK_BaseCurrencies primary key(BaseCurrencyID)
);

create table VariableUnitsOfMeasurement(
	VariableUnitOfMeasurementID bigint identity(1,1),
	UnitName varchar(200) NOT NULL,
	UnitAbbreviation varchar(5),
	BaseUnitOfMeasurementID bigint NOT NULL,
	QuantityOfBaseUnits decimal(19,4) NOT NULL,  ---PRECISA SER PONTO FLUTUANTE AQUI
	CONSTRAINT PK_VariableUnitsOfMeasurement primary key(VariableUnitOfMeasurementID),
	CONSTRAINT FK_VariableUnitsOfMeasurement_BaseUnitsOfMeasurement foreign key (BaseUnitOfMeasurementID) REFERENCES BaseUnitsOfMeasurement(BaseUnitOfMeasurementID)
);

create table CurrencyQuotations(
	CurrencyQuotationID bigint identity(1,1),
	CurrencyName varchar(200) NOT NULL,
	CurencyAbbreviation varchar(5),
	BaseCurrencyID bigint NOT NULL,
	QuantityOfBaseCurrency decimal(19,4) NOT NULL, ---PRECISA SER PONTO FLUTUANTE AQUI
	QuotationDate date NOT NULL,
	CONSTRAINT PK_VariableCurrencies primary key (CurrencyQuotationID),
	CONSTRAINT FK_VariableCurrencies_BaseCurrencies foreign key (BaseCurrencyID) REFERENCES BaseCurrencies(BaseCurrencyID)
);

create table Products(
	ProductID bigint identity(1,1),
	ProductName varchar(200) NOT NULL,
	BaseUnitOfMeasurementID bigint NOT NULL,
	BaseCurrencyID bigint NOT NULL,
	Price decimal(19,4) NOT NULL,  ---PRECISA SER PONTO FLUTUANTE AQUI
	CONSTRAINT PK_Products primary key(ProductID),
	CONSTRAINT FK_Products_BaseUnitsOfMeasurement foreign key(BaseUnitOfMeasurementID) REFERENCES BaseUnitsOfMeasurement(BaseUnitOfMeasurementID),
	CONSTRAINT FK_Products_BaseCurrencies foreign key(BaseCurrencyID) REFERENCES BaseCurrencies(BaseCurrencyID)
);

create table Inventory(
	InventoryID bigint identity(1,1),
	ProductID bigint NOT NULL,
	QuantityPerBaseUnityOfMeasurement decimal(19,4) NOT NULL,   ---PRECISA SER PONTO FLUTUANTE AQUI
	ParentOrSubsidiaryStokID bigint,
	CONSTRAINT UC_Inventory UNIQUE (ProductID, ParentOrSubsidiaryStokID),
	CONSTRAINT PK_Inventory primary key(InventoryID),
	CONSTRAINT FK_Inventory_Products foreign key(ProductID) REFERENCES Products(ProductID),
	CONSTRAINT FK_Inventory_CustomersSuppliers foreign key(ParentOrSubsidiaryStokID) REFERENCES CustomersSuppliers(CustomersSuppliersID)
);

create table Orders(
	OrderID bigint identity(1,1),
	CustomersSuppliersID bigint NOT NULL,
	IsSale bit NOT NULL,
	OrderOpeningDate date NOT NULL,
	OrderClosingDate date,
	CONSTRAINT PK_Orders primary key(OrderID),
	CONSTRAINT FK_Orders_CustomersSuppliers_CustomersSuppliers foreign key(CustomersSuppliersID) REFERENCES CustomersSuppliers(CustomersSuppliersID),
);

create table ProductListItems(
	ProductListID bigint identity(1,1),
	OrderID bigint,
	ProductID bigint,
	UnitOfMeasurementUsedOnTheOrderID bigint,
	QuantityOfUnitMesasurement bigint,
	CurrencyUsedOnTheOrderID bigint,
	PricePerUnitOfMeasurement decimal(19,4), ---PRECISA SER PONTO FLUTUANTE AQUI
	ShippingAddressID bigint,
	ParentOrSubsidiaryStokID bigint,
	CONSTRAINT PK_ProductListItems primary key(ProductListID),
	CONSTRAINT FK_ProductListItems_Orders foreign key(OrderID) REFERENCES Orders(OrderID),
	CONSTRAINT FK_ProductListItems_Products foreign key(ProductID) REFERENCES Products(ProductID),
	CONSTRAINT FK_ProductListItems_VariableUnitsOfMeasurement foreign key(UnitOfMeasurementUsedOnTheOrderID) REFERENCES VariableUnitsOfMeasurement(VariableUnitOfMeasurementID),
	CONSTRAINT FK_ProductListItems_CurrencyQuotations foreign key(CurrencyUsedOnTheOrderID) REFERENCES CurrencyQuotations(CurrencyQuotationID),
	CONSTRAINT FK_ProductListItems_Addresses foreign key(ShippingAddressID) REFERENCES Addresses(AddressID),
	CONSTRAINT FK_ProductListItems_CustomersSuppliers foreign key(ParentOrSubsidiaryStokID) REFERENCES CustomersSuppliers(CustomersSuppliersID)
);

create table Shipments(
	ShipmentID bigint identity(1,1),
	ProductListID bigint NOT NULL,
	ExpectedDateOfShipment date,
	ShipmentDepartureDate date,
	ShipmentArrivalDate date,
	CONSTRAINT PK_Shipments primary key(ShipmentID),
	CONSTRAINT FK_Shipments_ProductListItems foreign key(ProductListID) REFERENCES ProductListItems(ProductListID)
);

create table ShipmentStatusesChanges(
	ShipmentID bigint,
	StatusID bigint NOT NULL,
	ChangeDate varchar(200) NOT NULL,
	Description varchar(200) NOT NULL,
	CONSTRAINT PK_ShipmentStatusesChanges primary key(ShipmentID, StatusID),
	CONSTRAINT FK_AddressesList_Shipments foreign key (ShipmentID) REFERENCES Shipments(ShipmentID),
	CONSTRAINT FK_AddressesList_ShipmentStatuses foreign key (StatusID) REFERENCES ShipmentStatuses(StatusID)
);

create table OrderStatusesChanges(
	OrderID bigint,
	StatusID bigint NOT NULL,
	ChangeDate varchar(200) NOT NULL,
	Description varchar(200) NOT NULL,
	CONSTRAINT PK_OrderStatusesChanges primary key(OrderID, StatusID),
	CONSTRAINT FK_AddressesList_Orders foreign key (OrderID) REFERENCES Orders(OrderID),
	CONSTRAINT FK_AddressesList_OrderStatuses foreign key (StatusID) REFERENCES OrderStatuses(StatusID)
);

create table ProductsListItemStatusesChanges(
	ProductListID bigint,
	StatusID bigint NOT NULL,
	ChangeDate varchar(200) NOT NULL,
	Description varchar(200) NOT NULL,
	CONSTRAINT PK_ProductsListItemChanges primary key(ProductListID, StatusID),
	CONSTRAINT FK_AddressesList_ProductListItems foreign key (ProductListID) REFERENCES ProductListItems(ProductListID),
	CONSTRAINT FK_AddressesList_ProductsListItemStatuses foreign key (StatusID) REFERENCES ProductsListItemStatuses(StatusID)
);

--select IDENT_CURRENT('AddressTypes')
--DBCC CHECKIDENT ('OrderStatuses', RESEED, 0); 
--delete from OrderStatuses
--select * from OrderStatuses

insert into AddressTypes(Description)
values('Residencial')
insert into AddressTypes(Description)
values('Comercial')
insert into AddressTypes(Description)
values('Industrial')
insert into AddressTypes(Description)
values('Rural')
insert into AddressTypes(Description)
values('Temporário')

insert into Addresses(AddressNickname,AddressTypeID,Street,Number,Complement,City,StateOrDistrict,PostalCode,Country)
values ('House of Felipe',1,'Avenue of testing','123','house at the back','São Paulo','SP','12345-123','Brazil');
insert into Addresses(AddressNickname,AddressTypeID,Street,Number,Complement,City,StateOrDistrict,PostalCode,Country)
values ('Sítio Felipe',4,'Washington Luís road','321','Sítio Cantinho Feliz','Uru','SP','45678-321','Brazil');
insert into Addresses(AddressNickname,AddressTypeID,Street,Number,Complement,City,StateOrDistrict,PostalCode,Country)
values ('comertial Felipe',2,'Avenue of comerce','456','Terceiro andar','Campinas','SP','18765-432','Brazil');
insert into Addresses(AddressNickname,AddressTypeID,Street,Number,Complement,City,StateOrDistrict,PostalCode,Country)
values ('House of Mickey Mouse',1,'Avenue of bugs','789','','Chicago','IL','78945-123','USA');
insert into Addresses(AddressNickname,AddressTypeID,Street,Number,Complement,City,StateOrDistrict,PostalCode,Country)
values ('Comercial Mickey Mouse',3,'Avenue of test automation','123','Quinto andar','New York','NY','13579-234','USA');
insert into Addresses(AddressNickname,AddressTypeID,Street,Number,Complement,City,StateOrDistrict,PostalCode,Country)
values ('São Paulo Central Unit Address',3,'São Paulo Avenue','111','','São Paulo','SP','12389-111','Brazil');
insert into Addresses(AddressNickname,AddressTypeID,Street,Number,Complement,City,StateOrDistrict,PostalCode,Country)
values ('Campinas Subsidiary Unit Address',3,'Campinas Avenue','222','','Campinas','SP','23456-222','Brazil');

insert into CustomersSuppliers(FiscalID,Name,Email,Phone,IsCustomer,IsSupplier,IsCustomerRepresentative,IsSuplierRepresentative,IsParent,IsSubsidiary,IsParentRepresentative,IsSubsidiaryRepresentative)
values ('123.456.789-00','Felipe Capelli','felipe.capelli@fakeemail.com','+551199912-3456',1,0,0,0,0,0,0,0);
insert into CustomersSuppliers(FiscalID,Name,Email,Phone,IsCustomer,IsSupplier,IsCustomerRepresentative,IsSuplierRepresentative,IsParent,IsSubsidiary,IsParentRepresentative,IsSubsidiaryRepresentative)
values ('12.345.678/0001-12','Mickey Mouse Company','mickey.mouse@fakeemail.com','+551199912-4567',0,1,0,0,0,0,0,0);
insert into CustomersSuppliers(FiscalID,Name,Email,Phone,IsCustomer,IsSupplier,IsCustomerRepresentative,IsSuplierRepresentative,IsParent,IsSubsidiary,IsParentRepresentative,IsSubsidiaryRepresentative)
values ('987.654.321-00','Donald Duck','Donald.Duck@fakeemail.com','+551199912-7539',0,0,0,1,0,0,0,0);
insert into CustomersSuppliers(FiscalID,Name,Email,Phone,IsCustomer,IsSupplier,IsCustomerRepresentative,IsSuplierRepresentative,IsParent,IsSubsidiary,IsParentRepresentative,IsSubsidiaryRepresentative)
values ('11.111.111/0001-11','São Paulo Central Unit','sp.central@fakeemail.com','+551199912-7777',0,0,0,0,1,0,0,0);
insert into CustomersSuppliers(FiscalID,Name,Email,Phone,IsCustomer,IsSupplier,IsCustomerRepresentative,IsSuplierRepresentative,IsParent,IsSubsidiary,IsParentRepresentative,IsSubsidiaryRepresentative)
values ('22.222.222/0001-22','Campinas Subsidiary Unit','cp.subsidiary@fakeemail.com','+551199912-8888',0,0,0,0,0,1,0,0);
insert into CustomersSuppliers(FiscalID,Name,Email,Phone,IsCustomer,IsSupplier,IsCustomerRepresentative,IsSuplierRepresentative,IsParent,IsSubsidiary,IsParentRepresentative,IsSubsidiaryRepresentative)
values ('111.111.111-01','Goofy','sp.central.goofy@fakeemail.com','+551199912-1111',0,0,0,0,0,0,1,0);
insert into CustomersSuppliers(FiscalID,Name,Email,Phone,IsCustomer,IsSupplier,IsCustomerRepresentative,IsSuplierRepresentative,IsParent,IsSubsidiary,IsParentRepresentative,IsSubsidiaryRepresentative)
values ('222.222.222-02','Pluto','cp.subsidiary.pluto@fakeemail.com','+551199912-2222',0,0,0,0,0,0,0,1);

insert into NaturalPeople(CustomersSuppliersID,IdentityCard,DateOfBirth,Occupation)
values(1,'12.345.678-9','01/01/1901','Tester');
insert into NaturalPeople(CustomersSuppliersID,IdentityCard,DateOfBirth,Occupation)
values(3,'45.678.910-1','01/10/1910','Comercial representative');
insert into NaturalPeople(CustomersSuppliersID,IdentityCard,DateOfBirth,Occupation)
values(6,'11.111.111-1','01/10/1911','SP central representative');
insert into NaturalPeople(CustomersSuppliersID,IdentityCard,DateOfBirth,Occupation)
values(7,'22.222.222-2','01/10/1922','Campinas subsidiary representative');

insert into CorporateEntities(CustomersSuppliersID, FoundationDate,BusinessKindDescription,LegalRepresentativeFiscalID)
values(2,'2004-10-05','Commodities seler', 3);
insert into CorporateEntities(CustomersSuppliersID, FoundationDate,BusinessKindDescription,LegalRepresentativeFiscalID)
values(4,'2001-11-21','Central', 6);
insert into CorporateEntities(CustomersSuppliersID, FoundationDate,BusinessKindDescription,LegalRepresentativeFiscalID)
values(5,'2002-02-22','Campinas Subsidiary', 7);

insert into AddressesList(CustomersSuppliersID,AddressID)
values(1,1);
insert into AddressesList(CustomersSuppliersID,AddressID)
values(1,2);
insert into AddressesList(CustomersSuppliersID,AddressID)
values(1,3);
insert into AddressesList(CustomersSuppliersID,AddressID)
values(2,4);
insert into AddressesList(CustomersSuppliersID,AddressID)
values(2,5);
insert into AddressesList(CustomersSuppliersID,AddressID)
values(6,6);
insert into AddressesList(CustomersSuppliersID,AddressID)
values(7,7);

insert into ShipmentStatuses(Description)
values('Packing')
insert into ShipmentStatuses(Description)
values('Sent')
insert into ShipmentStatuses(Description)
values('Delivered')

insert into OrderStatuses(Description)
values('Open')
insert into OrderStatuses(Description)
values('Processing')
insert into OrderStatuses(Description)
values('Payment')
insert into OrderStatuses(Description)
values('Closed')

insert into ProductsListItemStatuses(Description)
values('Billing')
insert into ProductsListItemStatuses(Description)
values('Shipment')
insert into ProductsListItemStatuses(Description)
values('Awaiting Payment')
insert into ProductsListItemStatuses(Description)
values('Paymet Done')

insert into BaseUnitsOfMeasurement(UnitName,UnitAbbreviation)
values('Kilogram','KG');
insert into BaseUnitsOfMeasurement(UnitName,UnitAbbreviation)
values('Pound','LB');
insert into BaseUnitsOfMeasurement(UnitName,UnitAbbreviation)
values('Unit','UN');

insert into BaseCurrencies(CurrencyName,CurrencyAbbreviation)
values('US Dollar','US$');
insert into BaseCurrencies(CurrencyName,CurrencyAbbreviation)
values('Euro','€');

insert into VariableUnitsOfMeasurement(UnitName,UnitAbbreviation,BaseUnitOfMeasurementID,QuantityOfBaseUnits)
values('Corn Bushel','CB',2,56);
insert into VariableUnitsOfMeasurement(UnitName,UnitAbbreviation,BaseUnitOfMeasurementID,QuantityOfBaseUnits)
values('Wheat Bushel','WB',2,60);
insert into VariableUnitsOfMeasurement(UnitName,UnitAbbreviation,BaseUnitOfMeasurementID,QuantityOfBaseUnits)
values('Saco 50KG','SC60',1,50);
insert into VariableUnitsOfMeasurement(UnitName,UnitAbbreviation,BaseUnitOfMeasurementID,QuantityOfBaseUnits)
values('Saco 60KG','SC60',1,60);
insert into VariableUnitsOfMeasurement(UnitName,UnitAbbreviation,BaseUnitOfMeasurementID,QuantityOfBaseUnits)
values('Ton','TON',1,1000);
insert into VariableUnitsOfMeasurement(UnitName,UnitAbbreviation,BaseUnitOfMeasurementID,QuantityOfBaseUnits)
values('Dúzia','DZ',3,12);

insert into CurrencyQuotations(CurrencyName,CurencyAbbreviation,BaseCurrencyID,QuantityOfBaseCurrency, QuotationDate)
values('Brazilian Reais','R$', 1,5, '2024-10-23');

insert into Products(ProductName,BaseUnitOfMeasurementID,BaseCurrencyID,Price)
values('Corn',1,1,2);
insert into Products(ProductName,BaseUnitOfMeasurementID,BaseCurrencyID,Price)
values('Wheat',1,1,4);

insert into Inventory(ProductID,QuantityPerBaseUnityOfMeasurement,ParentOrSubsidiaryStokID)
values(1,1020,4);
insert into Inventory(ProductID,QuantityPerBaseUnityOfMeasurement,ParentOrSubsidiaryStokID)
values(2,2460,5);

insert into Orders(CustomersSuppliersID,IsSale,OrderOpeningDate,OrderClosingDate)
values(2,0,'10/01/2024','10/03/2024');
insert into Orders(CustomersSuppliersID,IsSale,OrderOpeningDate,OrderClosingDate)
values(1,1,'10/22/2024','10/23/2024');
insert into Orders(CustomersSuppliersID,IsSale,OrderOpeningDate,OrderClosingDate)
values(2,0,'01/01/2024','01/10/2024');
insert into Orders(CustomersSuppliersID,IsSale,OrderOpeningDate,OrderClosingDate)
values(1,1,'02/02/2024','02/04/2024');

insert into ProductListItems(OrderID,ProductID,UnitOfMeasurementUsedOnTheOrderID,QuantityOfUnitMesasurement,CurrencyUsedOnTheOrderID,PricePerUnitOfMeasurement,ShippingAddressID,ParentOrSubsidiaryStokID)
values(1,1,4,1320,1,30,5,4);
insert into ProductListItems(OrderID,ProductID,UnitOfMeasurementUsedOnTheOrderID,QuantityOfUnitMesasurement,CurrencyUsedOnTheOrderID,PricePerUnitOfMeasurement,ShippingAddressID,ParentOrSubsidiaryStokID)
values(2,1,4,300,1,50,2,4);
insert into ProductListItems(OrderID,ProductID,UnitOfMeasurementUsedOnTheOrderID,QuantityOfUnitMesasurement,CurrencyUsedOnTheOrderID,PricePerUnitOfMeasurement,ShippingAddressID,ParentOrSubsidiaryStokID)
values(3,1,4,3000,1,30,5,5);
insert into ProductListItems(OrderID,ProductID,UnitOfMeasurementUsedOnTheOrderID,QuantityOfUnitMesasurement,CurrencyUsedOnTheOrderID,PricePerUnitOfMeasurement,ShippingAddressID,ParentOrSubsidiaryStokID)
values(4,1,4,540,1,50,2,5);

insert into Shipments(ProductListID,ExpectedDateOfShipment,ShipmentDepartureDate,ShipmentArrivalDate)
values(1,'10/01/2024','10/02/2024','10/03/2024');
insert into Shipments(ProductListID,ExpectedDateOfShipment,ShipmentDepartureDate,ShipmentArrivalDate)
values(2,'10/22/2024','10/22/2024','10/23/2024');
insert into Shipments(ProductListID,ExpectedDateOfShipment,ShipmentDepartureDate,ShipmentArrivalDate)
values(3,'01/01/2024','01/02/2024','01/10/2024');
insert into Shipments(ProductListID,ExpectedDateOfShipment,ShipmentDepartureDate,ShipmentArrivalDate)
values(4,'02/02/2024','02/03/2024','02/04/2024');

insert into ShipmentStatusesChanges(ShipmentID,StatusID,ChangeDate,Description) values (1,1,'10/02/2024','Packing description');
insert into ShipmentStatusesChanges(ShipmentID,StatusID,ChangeDate,Description) values (1,2,'10/02/2024','Sent description');
insert into ShipmentStatusesChanges(ShipmentID,StatusID,ChangeDate,Description) values (1,3,'10/03/2024','Delivered description');
insert into ShipmentStatusesChanges(ShipmentID,StatusID,ChangeDate,Description) values (2,1,'10/22/2024','Packing description');
insert into ShipmentStatusesChanges(ShipmentID,StatusID,ChangeDate,Description) values (2,2,'10/22/2024','Sent description');
insert into ShipmentStatusesChanges(ShipmentID,StatusID,ChangeDate,Description) values (2,3,'10/23/2024','Delivered description');
insert into ShipmentStatusesChanges(ShipmentID,StatusID,ChangeDate,Description) values (3,1,'01/02/2024','Packing description');
insert into ShipmentStatusesChanges(ShipmentID,StatusID,ChangeDate,Description) values (3,2,'01/02/2024','Sent description');
insert into ShipmentStatusesChanges(ShipmentID,StatusID,ChangeDate,Description) values (3,3,'01/10/2024','Delivered description');
insert into ShipmentStatusesChanges(ShipmentID,StatusID,ChangeDate,Description) values (4,1,'02/03/2024','Packing description');
insert into ShipmentStatusesChanges(ShipmentID,StatusID,ChangeDate,Description) values (4,2,'02/03/2024','Sent description');
insert into ShipmentStatusesChanges(ShipmentID,StatusID,ChangeDate,Description) values (4,3,'02/04/2024','Delivered description');

insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (1,1,'10/01/2024','Open description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (1,2,'10/01/2024','Processing description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (1,3,'10/03/2024','Payment description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (1,4,'10/03/2024','Closed description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (2,1,'10/22/2024','Open description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (2,2,'10/22/2024','Processing description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (2,3,'10/23/2024','Payment description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (2,4,'10/23/2024','Closed description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (3,1,'01/01/2024','Open description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (3,2,'01/01/2024','Processing description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (3,3,'01/10/2024','Payment description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (3,4,'01/10/2024','Closed description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (4,1,'02/02/2024','Open description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (4,2,'02/02/2024','Processing description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (4,3,'02/04/2024','Payment description');
insert into OrderStatusesChanges(OrderID,StatusID,ChangeDate,Description) values (4,4,'02/04/2024','Closed description');

insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (1,1,'10/01/2024','Billing description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (1,2,'10/02/2024','Shipment description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (1,3,'10/02/2024','Awaiting Payment description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (1,4,'10/03/2024','Paymet Done description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (2,1,'10/22/2024','Billing description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (2,2,'10/22/2024','Shipment description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (2,3,'10/22/2024','Awaiting Payment description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (2,4,'10/23/2024','Paymet Done description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (3,1,'01/01/2024','Billing description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (3,2,'01/02/2024','Shipment description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (3,3,'01/09/2024','Awaiting Payment description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (3,4,'01/10/2024','Paymet Done description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (4,1,'02/02/2024','Billing description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (4,2,'02/03/2024','Shipment description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (4,3,'02/03/2024','Awaiting Payment description');
insert into ProductsListItemStatusesChanges(ProductListID,StatusID,ChangeDate,Description) values (4,4,'02/04/2024','Paymet Done description');