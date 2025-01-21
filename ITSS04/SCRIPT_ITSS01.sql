create database ITSS04DATA
use ITSS04DATA

create table suppliers
(
	id varchar(20) primary key,
	name varchar(50)
)

insert into suppliers values ('s1','company abc')
insert into suppliers values ('s2','company 124')
insert into suppliers values ('s3','company dfg')
select * from suppliers


create table warehouses 
(
	id varchar(20) primary key,
	name varchar(50)
)
insert into warehouses values ('w1','wed warehouses ')
insert into warehouses values ('w2','Vowd warehouses ')
insert into warehouses values ('w3','dfgd warehouses ')
select * from warehouses


create table transactiontypes
(
	id varchar(20) primary key,
	name varchar(50)
)
insert into transactiontypes values('t1','Purchase Order')
insert into transactiontypes values('t2','Warehouse Management')
insert into transactiontypes values('t3','Purchase')
select * from transactiontypes


create table parts
(
	id  int identity(1,1)  primary key,
	name varchar(20),
	effectivelife int,
	batchnumberhasrequired int,
	minimumamount float
)
insert into parts(name,effectivelife,batchnumberhasrequired,minimumamount) values ('P02',3,1,4)
insert into parts(name,effectivelife,batchnumberhasrequired,minimumamount) values ('P03',4,0,23)
insert into parts(name,effectivelife,batchnumberhasrequired,minimumamount) values ('P04',9,1,4)
select * from parts

update parts set minimumamount=1

create table orders
(
	id varchar(20) primary key,
	transactiontypeid varchar(20),
	supplierid varchar(20),
	sourcewarehouseid varchar(20),
	destinationwarehouseid varchar(20),
	dates date,
	constraint fk_or_tran foreign key (transactiontypeid) references transactiontypes(id),
	constraint fk_or_sup foreign key (supplierid) references suppliers(id),
	constraint fk_or_w foreign key (sourcewarehouseid) references warehouses(id),
	constraint fk_or_w2 foreign key (destinationwarehouseid) references warehouses(id)
)
insert into orders values ('o1','t1','s1','w1','w2','2/3/2021')
insert into orders values ('o2','t2','s2','w2','w3','4/3/2021')
insert into orders values ('o3','t3','s3','w3','w2','2/5/2021')
insert into orders values ('o4','t2','s2','w1','w3','2/5/2021')
insert into orders values ('o5','t2','s2','w2','w1','2/5/2021')
insert into orders values ('o6','t2','s2','w3','w2','5/7/2021')
select * from orders
select * from transactiontypes
create table orderitems
(
	id varchar(20) primary key,
	orderid varchar(20),
	partid int,
	batchnumber varchar(50),
	amount float,
	constraint fk_ord_or foreign key (orderid) references orders(id),
	constraint fk_ord_par foreign key (partid) references parts(id)
)
insert into orderitems values ('or1','o1',1,'',3)
insert into orderitems values ('or2','o2',2,'',5)
insert into orderitems values ('or3','o3',3,'5',3)
insert into orderitems values ('or4','o4',3,'BA12354',3)
insert into orderitems values ('or5','o5',2,'',2)
insert into orderitems values ('or6','o6',3,'BA12354',2)
select p.name,t.name,(select cast(od.dates as date)) as datee,odt.amount,(select w2.name from warehouses w2 where w2.id=od.sourcewarehouseid ),(select w2.name from warehouses w2 where w2.id=od.destinationwarehouseid )
                    from orders od,orderitems odt,parts p,transactiontypes t 
                    where od.id=odt.orderid and odt.partid=p.id and od.transactiontypeid=t.id order by datee asc,t.id asc
