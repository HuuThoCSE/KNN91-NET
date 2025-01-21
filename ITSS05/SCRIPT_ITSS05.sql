CREATE DATABASE ITSS05DATA
GO 
USE ITSS05DATA
GO

create table locations
(
	id int primary key,
	name varchar(100)
)
go
create table departments
(
	id int primary key,
	name varchar(100)
)
go
create table deparmentlocations
(
	id int primary key,
	departmentid int,
	locationid int,
	startdate date,
	enddate date,

	constraint fk_dl_de foreign key (departmentid)
		references departments(id),
	constraint fk_dl_lo foreign key (locationid)
		references locations(id),
)
go
create table transactiontypes
(
	id int primary key,
	name varchar(8)
)
go
create table suppliers
(
	id int primary key,
	name varchar(8)
)
go
create table warehouses
(
	id int primary key,
	name varchar(50)
)
go
create table parts
(
	id int primary key,
	name varchar(8),
	effectivelife varchar(10),
	minimumquantity int,
	batchnumberhasrequired bit
)
go
create table assets(
	id int primary key,
	assetsn varchar(20),
	assetname varchar(50),
	departmentlocationid int,
	employeeid int,
	assetgroupid int,
	description varchar(200),
	warrantydate date,

	constraint fk_as_de foreign key(departmentlocationid)
		references deparmentlocations(id)
)
go
create table emergencymaintenances
(
	id int primary key,
	assetid int,
	descriptionemergency varchar(200),
	ortherconsiderations varchar(200),
	emrequestdate date,
	emstartdate date,
	emenddate date,
	emtechniciannote varchar(200),

	constraint fk_em_as foreign key(assetid)
		references assets(id)
)
go
create table orders
(
	id int identity(1,1) primary key,
	transactiontypeid int,
	supplierid int,
	emergencymaintanancesid int,
	sourcewasehouseid int,
	destinationwasehouseid int,
	date date,
	time time,

	constraint fk_or_tr foreign key(transactiontypeid)
		references transactiontypes(id),
	constraint fk_or_sup foreign key(supplierid)
		references suppliers(id),
	constraint fk_or_em foreign key(emergencymaintanancesid)
		references emergencymaintenances(id),
	constraint fk_or_sw foreign key(sourcewasehouseid)
		references warehouses(id),
	constraint fk_or_dw foreign key(destinationwasehouseid)
		references warehouses(id)
)
go
create table orderitems
(
	id int identity(1,1) primary key,
	orderid int,
	partid int,
	amout float,
	unitprice int,
	batchnumber int,
	stock int,
	constraint fk_ori_or foreign key(orderid)
		references orders(id),
	constraint fk_ori_p foreign key(partid)
		references parts(id)
)

INSERT INTO departments VALUES
(1, 'Yolja'),
(2, 'Office Center'),
(3, 'Kazan 554')
go
insert into locations values 
(1,'US'),
(2,'JP')
go
set dateformat dmy
insert into deparmentlocations values
(1,1,1,'22/2/2019','22/12/2019'),
(2,2,1,'12/3/2019','22/6/2019'),
(3,2,1,'22/2/2019','22/12/2019'),
(4,3,1,'12/3/2019','22/6/2019')
go
insert into suppliers values
(1,'com1'),
(2,'com2')
go
insert into transactiontypes values 
(1,'tran1'),
(2,'tran2')
go
insert into parts values
(1,'ball1','high',20,1),
(2,'ball2','high',20,1),
(3,'ball3','normal',20,0)
go 

insert into warehouses values
(1,'central wasehouse'),
(2,'north wasehouse'),
(3,'west wasehouse')
go

set dateformat dmy
insert into assets values
(1,'123','car1',1,1,2,'none','22/2/2026'),
(2,'124','car2',2,1,2,'none','22/5/2026'),
(3,'125','car3',3,1,2,'none','22/7/2026'),
(4,'125','car3',4,1,2,'none','22/7/2026')
go

set dateformat dmy
insert into emergencymaintenances values 
(1, 1, 'none','none','11/2/2019','12/2/2019','20/2/2019','none'),
(2, 2, 'none','none','11/3/2019','12/3/2019','20/3/2019','none'),
(3, 3, 'none','none','11/2/2019','12/2/2019','12/6/2019','none'),
(4, 4, 'none','none','11/2/2019','12/2/2019',null,'none')
go

set dateformat dmy
insert into orders values
(1,1,1,1,1,'1/8/2019','09:30:00'),
(2,2,2,2,2,'1/7/2019','09:30:00'),
(2,1,3,2,1,'1/3/2019','09:30:00'),
(1,2,4,3,2,'1/12/2018','09:30:00')
go

select * from orders
go

select * from orderitems
go

insert into orderitems values
(5, 1, 10, 120,123,12),
(5, 2, 10, 130,123,12),
(6, 1, 10, 150,123,12),
(6, 3, 15, 150,null,12),
(7, 1, 10, 120,123,12),
(7, 3, 15, 200,null,12),
(8, 1, 10, 100,123,12),
(8, 3, 15, 130,null,12)

---------------------- Section 1 ------------------------
-- select những tháng/năm trong order
select distinct
convert(varchar,MONTH(date))+'/'+convert(varchar,year(date))
from orders od
--  JOIN emergencymaintenances em ON em.id = od.id
--where emenddate is not null
-- select những department và chi tiêu trong tháng trên
	-- dữ liệu có enddate ! = null
	SELECT dp.name,
    SUM(CASE WHEN YEAR(od.date) = 2019 AND MONTH(od.date) = 3 THEN odi.amout * odi.unitprice ELSE 0 END) AS sum_september
FROM
    departments dp
    JOIN deparmentlocations dpl ON dpl.departmentid = dp.id
    JOIN assets ass ON ass.departmentlocationid = dpl.id
    JOIN emergencymaintenances em ON em.assetid = ass.id
    JOIN orders od ON em.id = od.emergencymaintanancesid
    JOIN orderitems odi ON odi.orderid = od.id
WHERE
    enddate IS NOT NULL
GROUP BY
    dp.name
--------------- Section 2 ------------------
-- thống kê part nào có cost cao nhất và part được sử dụng nhiều nhất trong tháng  
---- part được thanh toán/sử dụng (nằm trong order item) và enddate !=null
--- Lấy tháng trong order
select distinct
convert(varchar,MONTH(date))+'/'+convert(varchar,year(date))
from orders od
-- lấy part có highest cost
SELECT top(1) p.name,
   sum(CASE WHEN YEAR(od.date) = 2019 AND MONTH(od.date) = 3 THEN odi.amout * odi.unitprice ELSE 0 END) AS sum_cost
FROM parts p
	JOIN orderitems odi ON odi.partid = p.id
	JOIN orders od ON odi.orderid = od.id
    JOIN emergencymaintenances em ON em.id = od.emergencymaintanancesid
WHERE
    em.emenddate IS NOT NULL
GROUP BY p.name
order by  sum_cost desc
-- lấy part sử dụng nhiều nhất
SELECT  p.name,
   sum(CASE WHEN YEAR(od.date) = 2019 AND MONTH(od.date) = 9 then odi.amout ELSE 0 END) AS count_part
FROM parts p
	JOIN orderitems odi ON odi.partid = p.id
	JOIN orders od ON odi.orderid = od.id
    JOIN emergencymaintenances em ON em.id = od.emergencymaintanancesid
	where emenddate is not null
GROUP BY p.name
order by count_part desc

--------- ----section 3 ---------------------
-- asset và department được thanh toán nhiều nhất trong tháng

---- ASSET 
SELECT top(1) ass.assetname,
   sum(CASE WHEN YEAR(od.date) = 2019 AND MONTH(od.date) = 3 THEN odi.amout * odi.unitprice ELSE 0 END) AS sum_cost
FROM assets ass
	JOIN emergencymaintenances em ON em.assetid = ass.id
    JOIN orders od ON em.id = od.emergencymaintanancesid
    JOIN orderitems odi ON odi.orderid = od.id
WHERE
    em.emenddate IS NOT NULL
GROUP BY ass.assetname
order by  sum_cost desc

-----department
SELECT dp.name,
    SUM(CASE WHEN YEAR(od.date) = 2019 AND MONTH(od.date) = 3 THEN odi.amout * odi.unitprice ELSE 0 END) AS sum_september
FROM departments dp
    JOIN deparmentlocations dpl ON dpl.departmentid = dp.id
    JOIN assets ass ON ass.departmentlocationid = dpl.id
    JOIN emergencymaintenances em ON em.assetid = ass.id
    JOIN orders od ON em.id = od.emergencymaintanancesid
    JOIN orderitems odi ON odi.orderid = od.id
WHERE
    enddate IS NOT NULL
GROUP BY
    dp.name

----
select format(Date, 'yyyy-MM') 
	from Orders
	group by format(Date, 'yyyy-MM')
	order by format(Date, 'yyyy-MM')
	desc
go

select *
	from OrderItems

select 
	o.ID
	from Orders o
	join OrderItems oi on oi.OrderID = o.ID
	left join 