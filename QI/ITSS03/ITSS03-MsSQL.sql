create database ITSS03DATA
go
use ITSS03DATA
go
-- Create tables based on the provided ERD

CREATE TABLE Departments (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);

CREATE TABLE Locations (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);

CREATE TABLE DepartmentLocations (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentID INT NOT NULL,
    LocationID INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(ID),
    FOREIGN KEY (LocationID) REFERENCES Locations(ID)
);

CREATE TABLE AssetGroups (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);

CREATE TABLE Employees (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(20),
    isAdmin BIT NOT NULL,
    Username NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL
);

CREATE TABLE Priorities (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);

CREATE TABLE Parts (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    EffectiveLife INT NOT NULL
);

CREATE TABLE Assets (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    AssetSN NVARCHAR(255) NOT NULL,
    AssetName NVARCHAR(255) NOT NULL,
    DepartmentLocationID INT NOT NULL,
    EmployeeID INT,
    AssetGroupID INT,
    Description NVARCHAR(255),
    WarrantyDate DATE,
    FOREIGN KEY (DepartmentLocationID) REFERENCES DepartmentLocations(ID),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(ID),
    FOREIGN KEY (AssetGroupID) REFERENCES AssetGroups(ID)
);

CREATE TABLE EmergencyMaintenances (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    AssetID INT NOT NULL,
    PriorityID INT NOT NULL,
    DescriptionEmergency NVARCHAR(255),
    OtherConsiderations NVARCHAR(255),
    EMReportDate DATE NOT NULL,
    EMStartDate DATE,
    EMEndDate DATE,
    EMTechnicianNote NVARCHAR(255),
    FOREIGN KEY (AssetID) REFERENCES Assets(ID),
    FOREIGN KEY (PriorityID) REFERENCES Priorities(ID)
);

CREATE TABLE ChangedParts (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    EmergencyMaintenanceID INT NOT NULL,
    PartID INT NOT NULL,
    Amount INT NOT NULL,
    FOREIGN KEY (EmergencyMaintenanceID) REFERENCES EmergencyMaintenances(ID),
    FOREIGN KEY (PartID) REFERENCES Parts(ID)
);

-- Insert sample data
INSERT INTO Departments (Name) VALUES ('IT'), ('HR');
INSERT INTO Locations (Name) VALUES ('Head Office'), ('Branch Office');
INSERT INTO DepartmentLocations (DepartmentID, LocationID, StartDate, EndDate) VALUES
(1, 1, '2023-01-01', NULL),
(2, 2, '2023-01-01', NULL);
INSERT INTO AssetGroups (Name) VALUES ('Electronics'), ('Furniture');
INSERT INTO Employees (FirstName, LastName, Phone, isAdmin, Username, Password) VALUES
('John', 'Doe', '123456789', 1, 'johndoe', 'password123'),
('Jane', 'Smith', '987654321', 0, 'janesmith', 'password456');
INSERT INTO Priorities (Name) VALUES ('High'), ('Medium'), ('Low');
INSERT INTO Parts (Name, EffectiveLife) VALUES ('Hard Drive', 5), ('Monitor', 3);
INSERT INTO Assets (AssetSN, AssetName, DepartmentLocationID, EmployeeID, AssetGroupID, Description, WarrantyDate) VALUES
('SN001', 'Laptop', 1, 1, 1, 'Dell Laptop', '2025-01-01'),
('SN002', 'Chair', 2, 2, 2, 'Office Chair', '2026-01-01');
INSERT INTO EmergencyMaintenances (AssetID, PriorityID, DescriptionEmergency, OtherConsiderations, EMReportDate, EMStartDate, EMEndDate, EMTechnicianNote) VALUES
(1, 1, 'Overheating', 'Replace thermal paste', '2024-01-01', '2024-01-02', '2024-01-03', 'Replaced thermal paste'),
(1, 1, 'Overheating', 'Replace thermal paste', '2024-01-01', '2024-01-02', '2024-01-04', 'Replaced thermal paste');
INSERT INTO ChangedParts (EmergencyMaintenanceID, PartID, Amount) VALUES
(1, 1, 1);
update ChangedParts set Amount
/*select Username from Employees where Username='johndoe' and Password='password123'
select * from EmergencyMaintenances
select count(ID),max(EMEndDate) from EmergencyMaintenances where AssetID='1'
select Assets.ID,AssetSN,AssetName,max(EMEndDate),count(EmergencyMaintenances.ID) from Assets,EmergencyMaintenances,Employees
where Assets.EmployeeID=Employees.ID and Assets.ID=EmergencyMaintenances.AssetID
group by Assets.ID,AssetSN,AssetName

select
	Assets.ID,
	WarrantyDate,
	AssetSN,
	AssetName,
	format(max(EMEndDate),'yyyy-MM-dd'),
	count(em.ID)
from Assets
join Employees as e on e.ID=Assets.EmployeeID
left join EmergencyMaintenances as em on em.AssetID = Assets.ID
where e.ID='1'
group by Assets.ID,AssetSN,AssetName,WarrantyDate

select * from Locations

select Assets.AssetSN,Assets.AssetName,concat(Departments.Name,' ',Locations.Name)
from Assets,DepartmentLocations,Departments, Locations
where Assets.DepartmentLocationID=DepartmentLocations.ID and
	DepartmentLocations.DepartmentID=Departments.ID and
	DepartmentLocations.LocationID=Locations.ID and
	Assets.ID='1'

INSERT INTO Assets (AssetSN, AssetName, DepartmentLocationID, EmployeeID, AssetGroupID, Description, WarrantyDate) VALUES
('SN001', 'Laptop', 1, 1, 1, 'Dell Laptop', '2026-01-01')

select * from Assets*/

select
	em.ID,
	a.AssetSN,
	a.AssetName,
	em.EMReportDate,
	concat(e.FirstName,' ',e.LastName),
	d.Name
from Assets as a
join Employees as e on e.ID=a.EmployeeID
left join EmergencyMaintenances as em on em.AssetID=a.ID
left join DepartmentLocations as dl on dl.ID=a.DepartmentLocationID
join Departments as d on d.ID=dl.DepartmentID
group by em.ID,a.AssetSN,a.AssetName,em.EMReportDate,concat(e.FirstName,' ',e.LastName),d.Name


select
	p.ID,
	p.Name,
	cp.Amount
from EmergencyMaintenances as em
left join ChangedParts as cp on cp.EmergencyMaintenanceID=em.ID
join Parts as p on p.ID=cp.PartID
where em.ID='1'

select * from Parts

select
	a.AssetSN,
	a.AssetName,
	d.Name,
	em.EMStartDate,
	em.EMEndDate,
	em.EMTechnicianNote
from EmergencyMaintenances as em
join Assets as a on a.ID=em.AssetID
join DepartmentLocations as dl on dl.ID=a.DepartmentLocationID
join Departments as d on d.ID=dl.DepartmentID
where em.ID='1'



INSERT INTO ChangedParts (EmergencyMaintenanceID, PartID, Amount) VALUES
(2, 2, 2);

select * from EmergencyMaintenances

select * from ChangedParts where EmergencyMaintenanceID='1'
select Amount from ChangedParts where EmergencyMaintenanceID='2' and PartID='1'