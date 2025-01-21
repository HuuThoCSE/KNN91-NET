create database ITSS04DATA
go
use ITSS04DATA
go
-- Create tables
CREATE TABLE Suppliers (
    ID INT PRIMARY KEY identity,
    Name VARCHAR(100)
);

CREATE TABLE Warehouses (
    ID INT PRIMARY KEY identity,
    Name VARCHAR(100)
);

CREATE TABLE TransactionTypes (
    ID INT PRIMARY KEY identity,
    Name VARCHAR(100)
);

CREATE TABLE Parts (
    ID INT PRIMARY KEY identity,
    Name VARCHAR(100),
    EffectiveLife INT,
    BatchNumberHasRequired BIT,
    MinimumAmount INT
);

CREATE TABLE Orders (
    ID INT PRIMARY KEY Identity,
    TransactionTypeID INT,
    SupplierID INT,
    SourceWarehouseID INT,
    DestinationWarehouseID INT,
    Date DATE,
    FOREIGN KEY (TransactionTypeID) REFERENCES TransactionTypes(ID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(ID),
    FOREIGN KEY (SourceWarehouseID) REFERENCES Warehouses(ID),
    FOREIGN KEY (DestinationWarehouseID) REFERENCES Warehouses(ID)
);

CREATE TABLE OrderItems (
    ID INT PRIMARY KEY identity,
    OrderID INT,
    PartID INT,
    BatchNumber VARCHAR(50),
    Amount INT,
    FOREIGN KEY (OrderID) REFERENCES Orders(ID),
    FOREIGN KEY (PartID) REFERENCES Parts(ID)
);

-- Insert sample data
INSERT INTO Suppliers (Name) VALUES
('Supplier A'),
('Supplier B');

INSERT INTO Warehouses (Name) VALUES
('Warehouse X'),
('Warehouse Y');

INSERT INTO TransactionTypes (Name) VALUES
('Purchase'),
('Transfer');

INSERT INTO Parts (Name, EffectiveLife, BatchNumberHasRequired, MinimumAmount) VALUES
('Part A', 24, 1, 50),
('Part B', 12, 0, 30);

INSERT INTO Orders (TransactionTypeID, SupplierID, SourceWarehouseID, DestinationWarehouseID, Date) VALUES
(1, 1, NULL, 2, '2025-01-01'),
(2, NULL, 1, 2, '2025-01-02');

INSERT INTO OrderItems (OrderID, PartID, BatchNumber, Amount) VALUES
(1, 1, 'B001', 100),
(1, 2, 'B002', 50),
(2, 1, NULL, 70);


select
	p.Name,
	tt.Name,
	format(o.Date,'yyyy-MM-dd'),
	oi.Amount,
	coalesce(s.Name,w1.Name),
	w2.Name
from Orders as o
left join OrderItems as oi on o.ID=oi.OrderID
left join Parts as p on p.ID=oi.PartID
left join TransactionTypes as tt on tt.ID=o.TransactionTypeID
left join Suppliers as s on s.ID=o.SupplierID
left join Warehouses as w1 on w1.ID=o.SourceWarehouseID
left join Warehouses as w2 on w2.ID=o.DestinationWarehouseID