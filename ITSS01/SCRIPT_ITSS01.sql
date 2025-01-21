CREATE DATABASE ITS01DATA
GO 
USE ITS01DATA
GO

--select @@SERVERNAME
CREATE TABLE SUPPLIERS
(
	ID VARCHAR(8)  PRIMARY KEY,
	NAME VARCHAR(100)
)
GO
CREATE TABLE WAREHOUSES 
(
	ID VARCHAR(8)   PRIMARY KEY,
	NAME VARCHAR(100)
)
GO
CREATE TABLE PARTS 
(
	ID INT IDENTITY PRIMARY KEY,
	NAME VARCHAR(100),
	EFECTIVELIFE VARCHAR(50),
	BATCHNUMBERHASREQUIRED bit,
	MINIMUMAMOUNT INT
)
GO
CREATE TABLE TRANSACTIONTYPES
(
	ID VARCHAR(8)   PRIMARY KEY,
	NAME VARCHAR(100)
)
GO
CREATE TABLE ORDERS
(
	ID VARCHAR(8)   PRIMARY KEY,
	TRANSACTIONTYPE VARCHAR(8) ,
	SUPPLIERID VARCHAR(8) , 
	SOURCEWAREHOUSEID VARCHAR(8) ,
	DESTINATIONWAREHOUSEID VARCHAR(8) ,
	DATE DATE,
	CONSTRAINT FK_ORD_TRAN FOREIGN KEY (TRANSACTIONTYPE)
		REFERENCES TRANSACTIONTYPES (ID),
	CONSTRAINT FK_ORD_SUP FOREIGN KEY (SUPPLIERID)
		REFERENCES SUPPLIERS (ID),
	CONSTRAINT FK_ORD_SWH FOREIGN KEY (SOURCEWAREHOUSEID)
		REFERENCES WAREHOUSES (ID),
	CONSTRAINT FK_ORD_DWH FOREIGN KEY (DESTINATIONWAREHOUSEID)
		REFERENCES WAREHOUSES (ID),
)
GO
CREATE TABLE ORDERITEMS
(
	ID INT IDENTITY PRIMARY KEY,
	ORDERID VARCHAR(8) ,
	PARTID INT,
	BATCHNUMBER INT,
	AMOUNT FLOAT,

	CONSTRAINT FK_ORITE_ORDER FOREIGN KEY (ORDERID)
		REFERENCES ORDERS (ID),
	CONSTRAINT FK_ORITE_PART FOREIGN KEY (PARTID)
		REFERENCES PARTS (ID),
)
go

INSERT INTO WAREHOUSES VALUES
('wh01','Castrol'),
('wh02','Volka Warehouse'),
('wh03','Central Warehouse')
go

insert into TRANSACTIONTYPES values
('tran01','Purchase Order'),
('tran02','Warehouse Management')
go

--buoc1
insert into parts  values
('Engine Oil','asgsgda',1,20),
('Black ku','gda',0,60)
go
insert into SUPPLIERS values
('sup01','company 1'),
('sup02','company 2')
go
set dateformat dmy
--bước 2
insert into orders values
('ord01','tran01','sup01','wh01','wh02','8/1/2019'),
('ord02','tran02','sup02','wh02','wh03','8/5/2019'),
('ord03','tran01','sup02','wh01','wh03','8/5/2024'),
('ord04','tran02','sup02','wh02','wh03','8/6/2024'),
('ord05','tran02','sup02','wh02','wh03','12/12/2024')
go

--bước 3
insert into ORDERITEMS values
('ord05',1,269,20),
('ord01',1,265,12),
('ord02',2,269,20),
('ord03',2,260,20),
('ord04',1,269,20)



--Test th?
select p.NAME from [dbo].[PARTS] p
join ORDERITEMS on p.ID =  ORDERITEMS.PARTID
where  ORDERITEMS.PARTID = 1

--Làm th?
select p.name, tr.name, ord.date, ordi.amount,
(select name from WAREHOUSES where ID =ord.SOURCEWAREHOUSEID),
(select name from WAREHOUSES where ID =ord.DESTINATIONWAREHOUSEID)
from PARTS p
join  ORDERITEMS ordi on p.id = ordi.PARTID
join  ORDERS ord on ord.id = ordi.ORDERID
join  TRANSACTIONTYPES tr on ord.TRANSACTIONTYPE = tr.id


select p.NAME, tr.NAME, ord.DATE, ordi.AMOUNT, 
(select name from WAREHOUSES where id=ord.SOURCEWAREHOUSEID),
(select name from WAREHOUSES where id=ord.DESTINATIONWAREHOUSEID)
from  parts p
join ORDERITEMS ordi  on p.id = ordi.PARTID
join ORDERS ord on ord.id = ordi.ORDERID
join TRANSACTIONTYPES tr on ord.TRANSACTIONTYPE = tr.id
order by ord.DATE, case when tr.NAME = 'Purchase Order' then 0 
									else  1 end

delete ORDERITEMS where PARTID  = (select id from PARTS where NAME = ...)

delete ORDERS from ORDERITEMS,PARTS where ORDERS.ID = ORDERITEMS.ORDERID and PARTS.NAME = 
--(select ORDERID from ORDERITEMS 
--join PARTS on PARTS.ID = ORDERITEMS.ORDERID 
--where PARTS.NAME = ...)

--delete PARTS where NAME = 
