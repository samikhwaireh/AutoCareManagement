use AutoCareDb;

-- Create the Customers table
CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(50) NOT NULL,
    PlateNo VARCHAR(20) NOT NULL,
    PlateType VARCHAR(10) NOT NULL,
    AddedTime DATETIME NOT NULL,
    AddedBy VARCHAR(50) NOT NULL,
    Phone VARCHAR(20) NOT NULL
);

-- Create the Services table
CREATE TABLE Services (
    Id INT PRIMARY KEY IDENTITY,
    CustomerId INT NOT NULL,
    AddedBy VARCHAR(50) NOT NULL,
    AddedTime DATETIME NOT NULL,
    TotalPaid FLOAT NOT NULL,
    Note VARCHAR(100),
    Borc FLOAT NOT NULL,
    Other FLOAT NOT NULL,
    CONSTRAINT FK_Services_Customers FOREIGN KEY (CustomerId) REFERENCES Customers (Id)
);

-- Create the ServiceDetails table
CREATE TABLE service_details (
    Id INT PRIMARY KEY IDENTITY,
    ParentServiceId INT NOT NULL,
    Name VARCHAR(50) NOT NULL,
    Price FLOAT NOT NULL,
    PriceFromSupplier FLOAT NOT NULL,
    PriceFromShop FLOAT NOT NULL,
    CONSTRAINT FK_ServiceDetails_Services FOREIGN KEY (ParentServiceId) REFERENCES Services (Id)
);

-- Create the Items table
CREATE TABLE Items (
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(50) NOT NULL,
    Qnt INT NOT NULL,
    No INT NOT NULL,
    Price FLOAT NOT NULL,
    SalePrice FLOAT NOT NULL
);
