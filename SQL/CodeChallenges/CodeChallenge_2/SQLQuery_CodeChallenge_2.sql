use CodeChallenges
/*
1.	Write a query to display your birthday( day of week)
*/

declare @birthdate date = '2003-09-09'
select datename(weekday, @birthdate) as Day_Of_Week;










 
/*2.	Write a query to display your age in days*/



declare @birthdate date = '2003-09-09'
select datediff(day,@birthdate,getdate()) as Age_in_days









 /*
3.	Write a query to display all employees information those who joined before 5 years in the current month
 
(Hint : If required update some HireDates in your EMP table of the assignment)
 */
 
select * from employees
--employees table from assignments with updated hiredate or doj column
create table employees(
empno int,
ename varchar(50),
job varchar(50),
salary int,
deptno int,
hiredate_or_doj date)


insert into employees values
(7001,'sandeep','analyst',25000,10,'2018-07-10'),
(7002,'rajesh','designer',30000,10,'2017-07-15'),
(7003,'madhav','developer',40000,20,'2020-03-20'),
(7004,'manoj','developer',40000,20,'2019-07-05'),
(7005,'abhay','designer',35000,10,'2021-11-25'),
(7006,'uma','tester',30000,30,'2022-07-01'),
(7007,'gita','tech.writer',30000,40,'2016-07-23'),
(7008,'priya','tester',35000,30,'2023-02-10'),
(7009,'nutan','developer',45000,20,'2015-07-18'),
(7010,'smita','analyst',20000,10,'2019-07-30'),
(7011,'anand','project mgr',65000,10,'2014-07-12');

-----------Query 3--------------
select *,datename(month,getdate()) as current_month,datename(month,hiredate_or_doj) as 'joined_month',
datediff(year,hiredate_or_doj,getdate()) as 'years_difference(>5)'
from employees
where datediff(year,hiredate_or_doj,getdate())>=5 and month(hiredate_or_doj)=month(getdate());





/*4.	Create table Employee with empno, ename, sal, doj columns or use your emp table and perform the following operations in a single transaction
	a. First insert 3 rows 
	b. Update the second row sal with 15% increment  
        c. Delete first row.
After completing above all actions, recall the deleted row without losing increment of second row.
 */
 --using employees table
 select * from employees
-- a. inserting 3 rows
insert into employees(empno,ename,job,salary,deptno,hiredate_or_doj) values
(2001,'jaya','developer',30000,20,'2018-06-10'),
(2002,'lakshmi','tester',35000,30,'2019-07-15'),
(2003,'vardhini','designer',40000,10,'2020-08-20');







-- 4 b. updating second row (empno=2002) with 15% increment
update employees set salary=salary*1.15 where empno=2002;
--35000 to 40250
select * from employees







--4.cDelete first row.After completing above all actions, recall the deleted row without losing increment of second row.
begin tran
--before
select * from employees;
delete from employees where empno = 2001
select * from employees where empno = 2001
rollback;
--after
select * from employees;











/*5.      Create a user defined function calculate Bonus for all employees of a  given dept using 	following conditions
	a.     For Deptno 10 employees 15% of sal as bonus.
	b.     For Deptno 20 employees  20% of sal as bonus
	c      For Others employees 5%of sal as bonus
*/ 
create or alter function calculate_bonus(@deptno int,@sal int)
returns int
as
begin
    declare @bonus int;

    if @deptno = 10
	 begin
        set @bonus=@sal*0.15;
	 end
    else if @deptno=20
	 begin
        set @bonus=@sal*0.20;
	 end
    else
     begin
        set @bonus=@sal*0.05;
	 end
    return @bonus
end
--------------Query5-------------------
select empno, ename, deptno, salary, dbo.calculate_bonus(deptno, salary) as bonus
from employees order by deptno







/*

6. Create a procedure to update the salary of employee by 500 whose dept name is Sales 
and current salary is below 1500 (use emp table)
*/
select * from employees
--changing some values for the question
update employees set salary=1200 where empno=7001;
update employees set salary=1300 where empno=7002;
update employees set salary=1400 where empno=7005;

--dept table from assignments
create table departments(deptno int,dname varchar(20),loc varchar(20));

insert into departments values(10,'design','pune');
insert into departments values(20,'development','pune');
insert into departments values(30,'testing','mumbai');
insert into departments values(40,'document','mumbai');
insert into departments values(50,'sales','delhi');

select * from departments

update employees set deptno=50 where empno=7010;
update employees set deptno=50 where empno=7002;
update employees set deptno=50 where empno=7001;

--procedure
create or alter procedure update_sales_salary as
begin
update employees set salary=salary+500
where deptno=(select deptno from departments where dname='sales')
and salary<1500;
end;

--execution done
update_sales_salary
--employee 7001 and 7002 got updated from 1200 and 1300 to 1700 and 1800
select * from employees