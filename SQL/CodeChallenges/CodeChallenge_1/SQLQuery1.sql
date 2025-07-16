create database CodeChallenges

use CodeChallenges
--.................................Query 1................................................

/*Create a book table with id as primary key and provide the 
appropriate data type to other attributes .isbn no should be unique for each book

Write a query to fetch the details of the books written by author whose name ends with er.*/

--table books creation
create table books (
id int,
title varchar(50),
author varchar(40),
isbn varchar(20) unique,
published_date datetime,
constraint p1key primary key(id));

select * from books

--table books data insertion
insert into books(id,title,author,isbn,published_date)values
(1,'My First SQL book','Mary Parker','981483029127','2012-02-22 12:08:17'),
(2,'My Second SQL book','John Mayer','857300923713','1972-07-03 09:22:45'),
(3,'My Third SQL book','Cary Flint','523120967812','2015-10-18 14:05:44')

--Query 1
select * from books where author like '%er';





--.................................Query 2................................................
/*
Display the Title ,Author and ReviewerName for all the books from the above table
*/
--table reviews creation
create table reviews (
id int,
book_id int,
reviewer_name varchar(50),
content varchar(200),
rating int,
published_date datetime,
constraint p2key primary key (id),
constraint fkey foreign key (book_id) references books(id));

--inserting reviews
insert into reviews(id,book_id,reviewer_name,content,rating,published_date)values
(1,1,'John Smith','My first review',4,'2017-12-10 05:50:11'),
(2,2,'John Smith','My second review',5,'2017-10-13 15:05:12'),
(3,2,'Alice Walker','Another review',1,'2017-10-22 23:47:10')

select * from reviews

--query2
select books.title,books.author,reviews.reviewer_name
from books,reviews
where books.id = reviews.book_id;












--.................................Query 3................................................
/*
Display the reviewer name who reviewed more than one boo
*/
--query3

select reviewer_name from reviews 
group by reviewer_name
having count(distinct book_id) > 1












--.................................Query 4................................................

/*
Display the Name for the customer from above customer table who live in same address which has character o anywhere in address
*/
--table customers creation
create table customers(
ID int,
NAME varchar(50),
AGE int,
ADDRESS varchar(100),
SALARY float,
constraint p3key primary key(ID));

--inserting values customers
insert into customers(ID,NAME,AGE,ADDRESS,SALARY)values
(1,'Ramesh',32,'Ahmedabad',2000.00),
(2,'Khilan',25,'Delhi',1500.00),
(3,'kaushik',23,'Kota',2000.00),
(4,'Chaitali',25,'Mumbai',6500.00),
(5,'Hardik',27,'Bhopal',8500.00),
(6,'Komal',22,'MP',4500.00),
(7,'Muffy',24,'Indore',10000.00);

select * from customers

--query 4
select NAME,ADDRESS from customers where address like '%o%'
















--.................................Query 5................................................

/*
Write a query to display the Date,Total no of customer placed order on same Date
*/
--table creation orders
create table orders(
oid int,
date datetime,
customer_id int,
amount float);

select * from orders

--inserting values into the table
insert into orders(oid,date,customer_id,amount)values
(102,'2009-10-08 00:00:00',3,3000),
(100,'2009-10-08 00:00:00',3,1500),
(101,'2009-11-20 00:00:00',2,1560),
(103,'2009-05-20 00:00:00',4,2060);

--query5
select DATE,count(distinct customer_id) as TOTAL_CUSTOMERS from orders
group by date;


--.................................Query 6................................................

/*
Write a query to display the Date,Total no of customer placed order on same Date
*/

--table creation employee


create table employee(
id int,
name varchar(50),
age int,
address varchar(100),
salary float null,
constraint p4key primary key(id));

--insertion

insert into employee(id,name,age,address,salary)values
(1,'ramesh',32,'ahmedabad',2000.00),
(2,'khilan',25,'delhi',1500.00),
(3,'kaushik',23,'kota',2000.00),
(4,'chaitali',25,'mumbai',6500.00),
(5,'hardik',27,'bhopal',8500.00),
(6,'komal',22,'mp',null),
(7,'muffy',24,'indore',null)


select * from employee
drop table employee
--query6
select name,salary from employee where salary is null;



--.................................query 7................................................
/*

*/
--table creation
create table studentdetails(
id int,
registerno int,
name varchar(50),
age int,
qualification varchar(50),
mobileno varchar(15),
mail_id varchar(100),
location varchar(50),
gender varchar(10),
constraint p5key primary key(registerno));

--inserting values
insert into studentdetails(id,registerno,name,age,qualification,mobileno,mail_id,location,gender)values
(1,2,'Sai',22,'B.E','9952836777','sai@mail.com','chennai','M'),
(2,3,'Kumar',20,'BSC','7890125648','kumar@mail.com','madurai','M'),
(3,4,'Selvi',22,'B.Tech','8904567342','selvi@mail.com','silam','F'),
(4,5,'Nish',25,'M.E','7834672310','nisha@mail.com','theni','F'),
(5,6,'SaiSaran',21,'B.A','7890345678','saran@mail.com','madurai','F'),
(6,7,'Tom',23,'BCA','8901234675','tom@mail.com','pune','M')

select * from studentdetails
--query7
select gender,count(*) as total from studentdetails group by gender;
