use master
go
create database ITSS05DATA
go
use ITSS05DATA
go

-- Create schema for the database
CREATE TABLE Departments (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE Locations (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE DepartmentLocations (
    ID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentID INT NOT NULL,
    LocationID INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(ID),
    FOREIGN KEY (LocationID) REFERENCES Locations(ID)
);

CREATE TABLE Suppliers (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE TransactionTypes (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE Parts (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    EffectiveLife INT NOT NULL,
    MinimumQuantity INT NOT NULL,
    BatchNumberHasRequired BIT NOT NULL
);

CREATE TABLE Warehouses (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE Orders (
    ID INT PRIMARY KEY IDENTITY(1,1),
    TransactionTypeID INT NOT NULL,
    SupplierID INT,
    EmergencyMaintenancesID INT,
    SourceWarehouseID INT,
    DestinationWarehouseID INT,
    Date DATE NOT NULL,
    Time TIME NOT NULL,
    FOREIGN KEY (TransactionTypeID) REFERENCES TransactionTypes(ID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(ID),
    FOREIGN KEY (SourceWarehouseID) REFERENCES Warehouses(ID),
    FOREIGN KEY (DestinationWarehouseID) REFERENCES Warehouses(ID)
);

CREATE TABLE OrderItems (
    ID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT NOT NULL,
    PartID INT NOT NULL,
    Amount INT NOT NULL,
    UnitPrice money NOT NULL,
    BatchNumber VARCHAR(100),
    Stock INT NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(ID),
    FOREIGN KEY (PartID) REFERENCES Parts(ID)
);

CREATE TABLE Assets (
    ID INT PRIMARY KEY IDENTITY(1,1),
    AssetSN VARCHAR(100) NOT NULL,
    AssetName VARCHAR(100) NOT NULL,
    DepartmentLocationID INT NOT NULL,
    EmployeeID INT,
    AssetGroupID INT,
    Description TEXT,
    WarrantyDate DATE,
    FOREIGN KEY (DepartmentLocationID) REFERENCES DepartmentLocations(ID)
);

CREATE TABLE EmergencyMaintenances (
    ID INT PRIMARY KEY IDENTITY(1,1),
    AssetID INT NOT NULL,
    Priority INT NOT NULL,
    DescriptionEmergency TEXT NOT NULL,
    OtherConsiderations TEXT,
    EMRequestDate DATE NOT NULL,
    EMStartDate DATE,
    EMEndDate DATE,
    EMTechnicianNote TEXT,
    FOREIGN KEY (AssetID) REFERENCES Assets(ID)
);

-- Insert sample data into the database
INSERT INTO Departments (Name) VALUES
('HR'),
('IT'),
('Finance'),
('Operations'),
('Marketing');

INSERT INTO Locations (Name) VALUES
('New York'),
('San Francisco'),
('Chicago'),
('Los Angeles'),
('Houston');

INSERT INTO DepartmentLocations (DepartmentID, LocationID, StartDate, EndDate) VALUES
(1, 1, '2022-01-01', NULL),
(2, 2, '2023-01-01', NULL),
(3, 3, '2021-06-01', NULL),
(4, 4, '2020-03-01', '2024-12-31'),
(5, 5, '2022-09-01', NULL);

INSERT INTO Suppliers (Name) VALUES
('Tech Supplies Co'),
('Office Supplies Ltd'),
('Global Hardware'),
('Industrial Parts Inc.'),
('ElectroTech Solutions');

INSERT INTO TransactionTypes (Name) VALUES
('Purchase'),
('Transfer'),
('Repair'),
('Maintenance'),
('Disposal');

INSERT INTO Parts (Name, EffectiveLife, MinimumQuantity, BatchNumberHasRequired) VALUES
('Hard Drive', 5, 10, 1),
('Keyboard', 3, 20, 0),
('Mouse', 2, 15, 0),
('Monitor', 7, 5, 1),
('Printer Cartridge', 1, 50, 1);

INSERT INTO Warehouses (Name) VALUES
('Warehouse A'),
('Warehouse B'),
('Central Storage'),
('Regional Depot'),
('Overflow Storage');

INSERT INTO Orders (TransactionTypeID, SupplierID, EmergencyMaintenancesID, SourceWarehouseID, DestinationWarehouseID, Date, Time) VALUES
(1, 1, 1, 1, 2, '2024-01-01', '08:00:00'),
(2, 2, 2, 2, 3, '2023-12-15', '09:30:00'),
(3, 3, 1, 2, 4, '2023-11-20', '14:00:00'),
(4, 4, 2, 3, 4, '2023-10-10', '10:45:00'),
(5, 5, 3, 3, 5, '2023-09-05', '13:15:00'),
(4, 4, 2, 3, 4, '2023-10-11', '10:46:00');

INSERT INTO OrderItems (OrderID, PartID, Amount, UnitPrice, BatchNumber, Stock) VALUES
(1, 1, 5, 100.00, 'B123', 5),
(2, 2, 10, 50.00, 'B456', 10),
(3, 3, 20, 20.00, 'B789', 20),
(4, 4, 7, 200.00, 'C123', 7),
(5, 5, 50, 15.00, 'C456', 50),
(6, 5, 50, 15.00, 'C456', 50),
(2, 3, 10, 221, 'B456', 10);

INSERT INTO Assets (AssetSN, AssetName, DepartmentLocationID, EmployeeID, AssetGroupID, Description, WarrantyDate) VALUES
('SN001', 'Laptop', 1, NULL, NULL, 'Dell Latitude', '2025-12-31'),
('SN002', 'Desktop Computer', 2, NULL, NULL, 'HP ProDesk', '2026-11-15'),
('SN003', 'Printer', 3, NULL, NULL, 'Canon Inkjet', '2024-10-05'),
('SN004', 'Server', 4, NULL, NULL, 'Dell PowerEdge', '2027-01-01'),
('SN005', 'Projector', 5, NULL, NULL, 'Epson Full HD', '2023-08-20');

INSERT INTO EmergencyMaintenances (AssetID, Priority, DescriptionEmergency, OtherConsiderations, EMRequestDate, EMStartDate, EMEndDate, EMTechnicianNote) VALUES
(1, 1, 'Battery Issue', 'Replace ASAP', '2024-12-01', '2024-12-02', '2024-12-03', 'Replaced battery'),
(2, 2, 'Overheating', 'Check fan and ventilation', '2023-11-20', '2023-11-21', '2023-11-22', 'Replaced cooling fan'),
(3, 3, 'Paper Jam', 'Inspect feeder', '2023-10-10', '2023-10-11', '2023-10-12', 'Cleaned and repaired'),
(4, 1, 'Hard Drive Failure', 'Recover data if possible', '2023-09-05', '2023-09-06', '2023-09-07', 'Replaced hard drive'),
(5, 2, 'Lamp Burnout', 'Replace projector lamp', '2023-08-15', '2023-08-16', '2023-08-17', 'Installed new lamp');

INSERT INTO Orders (TransactionTypeID, SupplierID, EmergencyMaintenancesID, SourceWarehouseID, DestinationWarehouseID, Date, Time) VALUES
(4, 1, 4, 1, 2, '2023-09-06', '08:00:00'),
(4, 2, 5, 2, 3, '2023-08-16', '09:30:00');

-- Additional OrderItems
INSERT INTO OrderItems (OrderID, PartID, Amount, UnitPrice, BatchNumber, Stock) VALUES
(7, 1, 5, 100.00, 'B123', 5),
(8, 2, 10, 50.00, 'B456', 10);

-- Additional EmergencyMaintenances
INSERT INTO EmergencyMaintenances (AssetID, Priority, DescriptionEmergency, OtherConsiderations, EMRequestDate, EMStartDate, EMEndDate, EMTechnicianNote) VALUES
(1, 1, 'Battery Issue', 'Replace ASAP', '2023-09-05', '2023-09-06', '2023-09-07', 'Replaced battery again'),
(2, 2, 'Overheating', 'Check fan and ventilation', '2023-08-15', '2023-08-16', '2023-08-17', 'Replaced cooling fan again');

-- Additional Assets
INSERT INTO Assets (AssetSN, AssetName, DepartmentLocationID, EmployeeID, AssetGroupID, Description, WarrantyDate) VALUES
('SN006', 'Router', 1, NULL, NULL, 'Cisco Router', '2025-12-31'),
('SN007', 'Switch', 2, NULL, NULL, 'Netgear Switch', '2026-11-15');

-- Additional EmergencyMaintenances for new assets
INSERT INTO EmergencyMaintenances (AssetID, Priority, DescriptionEmergency, OtherConsiderations, EMRequestDate, EMStartDate, EMEndDate, EMTechnicianNote) VALUES
(6, 1, 'Firmware Issue', 'Update firmware', '2023-09-05', '2023-09-06', '2023-09-07', 'Updated firmware'),
(7, 2, 'Port Failure', 'Replace port', '2023-08-15', '2023-08-16', '2023-08-17', 'Replaced port');

select format(o.Date,'yyyy-MM') as date from Orders o group by format(o.Date,'yyyy-MM') order by date desc

select
	format(o.Date,'yyyy-MM') as date,
	d.Name,
	format(sum(oi.UnitPrice),'0') as cost
from Orders o
join OrderItems oi on oi.OrderID=o.ID
left join EmergencyMaintenances em on em.ID=o.EmergencyMaintenancesID
left join Assets a on a.ID=em.AssetID
left join DepartmentLocations dl on dl.ID=a.DepartmentLocationID
left join Departments d on d.ID=dl.DepartmentID
group by format(o.Date,'yyyy-MM'),d.Name
order by date desc


select
	format(o.Date,'yyyy-MM') as date,
	p.Name,
	cast(sum(oi.UnitPrice) as int) as cost
from Orders o
join OrderItems oi on oi.OrderID=o.ID
left join EmergencyMaintenances em on em.ID=o.EmergencyMaintenancesID
left join Assets a on a.ID=em.AssetID
left join DepartmentLocations dl on dl.ID=a.DepartmentLocationID
left join Departments d on d.ID=dl.DepartmentID
left join Parts p on p.ID=oi.PartID
group by format(o.Date,'yyyy-MM'),p.Name
order by date desc,cost desc


select
	format(o.Date,'yyyy-MM') as date,
	p.Name,
	count(p.name) as count
from Orders o
join OrderItems oi on oi.OrderID=o.ID
left join EmergencyMaintenances em on em.ID=o.EmergencyMaintenancesID
left join Assets a on a.ID=em.AssetID
left join DepartmentLocations dl on dl.ID=a.DepartmentLocationID
left join Departments d on d.ID=dl.DepartmentID
left join Parts p on p.ID=oi.PartID
group by format(o.Date,'yyyy-MM'),p.Name
order by date desc,count desc


select
	format(o.Date,'yyyy-MM') as date,
	a.AssetName,
	d.Name,
	count(a.AssetName) as count
from Orders o
left join EmergencyMaintenances em on em.ID=o.EmergencyMaintenancesID
left join Assets a on a.ID=em.AssetID
left join DepartmentLocations dl on dl.ID=a.DepartmentLocationID
left join Departments d on d.ID=dl.DepartmentID
group by format(o.Date,'yyyy-MM'),a.AssetName,d.Name
order by date desc,count desc

--- 

select format(o.Date, 'yyyy-MM') as date
	from Orders o
	group by date
	order by date desc
--> Nó sẽ không group trùng nhau

select format(o.Date, 'yyyy-MM') as date
	from Orders o
	group by format(o.Date, 'yyyy-MM')
	order by date desc

select
	format(o.Date, 'yyyy-MM') as date,
	d.Name,
	format(sum(oi.UnitPrice), '0') as cost
	
	from Orders o
	join OrderItems oi on oi.OrderID = o.ID
	left join EmergencyMaintenances em on em.ID = o.EmergencyMaintenancesID
	left join Assets a on a.ID = em.AssetID
	left join DepartmentLocations dl on dl.ID = a.DepartmentLocationID
	left join Departments d on d.ID = dl.DepartmentID
	group by format(o.Date, 'yyyy-MM'), d.Name
	order by date desc

---

select
	format(o.Date, 'yyyy-MM') as date,
	p.Name,
	format(sum(oi.UnitPrice), '0') as cost
	
	from Orders o
	join OrderItems oi on oi.OrderID = o.ID
	left join EmergencyMaintenances em on em.ID = o.EmergencyMaintenancesID
	left join Assets a on a.ID = em.AssetID
	left join DepartmentLocations dl on dl.ID = a.DepartmentLocationID
	left join Departments d on d.ID = dl.DepartmentID
	left join Parts p on p.ID = oi.PartID
	group by format(o.Date, 'yyyy-MM'), p.Name
	order by date desc, cost desc

---
select
	format(o.Date, 'yyyy-MM') as date,
	p.Name,
	count(p.Name) as count
	
	from Orders o
	join OrderItems oi on oi.OrderID = o.ID
	left join EmergencyMaintenances em on em.ID = o.EmergencyMaintenancesID
	left join Assets a on a.ID = em.AssetID
	left join DepartmentLocations dl on dl.ID = a.DepartmentLocationID
	left join Departments d on d.ID = dl.DepartmentID
	left join Parts p on p.ID = oi.PartID
	group by format(o.Date, 'yyyy-MM'), p.Name
	order by date desc, count desc

---
select
	format(o.Date, 'yyyy-MM') as date,
	a.AssetName,
	d.Name,
	count(a.AssetName) as count
	
	from Orders o
	join OrderItems oi on oi.OrderID = o.ID
	left join EmergencyMaintenances em on em.ID = o.EmergencyMaintenancesID
	left join Assets a on a.ID = em.AssetID
	left join DepartmentLocations dl on dl.ID = a.DepartmentLocationID
	left join Departments d on d.ID = dl.DepartmentID
	left join Parts p on p.ID = oi.PartID
	group by format(o.Date, 'yyyy-MM'), a.AssetName, d.Name
	order by date desc, count desc