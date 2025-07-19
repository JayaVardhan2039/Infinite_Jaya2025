use Assignments

--SET II  (Using the same tables as Assignment 2)

select * from emp
select * from dept
-- 1. Retrieve a list of MANAGERS.
--select e.mgr_id as 'ManagerId',(select ename from emp where empno=e.mgr_id) as 'Manager',e.ename as 'Manager of' from emp e join emp f on e.mgr_id=f.empno
select * from emp where job='manager'



-- 2. Find out the names and salaries of all employees earning more than 1000 per month.
select ename,salary from emp where salary>1000



-- 3. Display the names and salaries of all employees except JAMES.
select ename,salary from emp where ename<>'james'



-- 4. Find out the details of employees whose names begin with ‘S’.
select * from emp where ename like 'S%'



-- 5. Find out the names of all employees that have ‘A’ anywhere in their name.
select * from emp where ename like '%A%'



-- 6. Find out the names of all employees that have ‘L’ as their third character in their name.
select * from emp where ename like '__L%'




-- 7. Compute daily salary of JONES.
select ename,(salary*12)/365 as 'Daily salary' from emp where ename='Jones'



-- 8. Calculate the total monthly salary of all employees.
select sum(salary) as 'TotalMonthlySalary' from emp



-- 9. Print the average annual salary.
select avg(salary*12) as 'AverageAnnualSalary' from emp





-- 10. Select the name, job, salary, department number of all employees except SALESMAN from department number 30.
select ename,job,salary,deptno from emp where not (job='salesman' and deptno=30)



-- 11. List unique departments of the EMP table.
select distinct(e.deptno),d.dname from emp e join dept d on e.deptno=d.deptno





-- 12. List the name and salary of employees who earn more than 1500 and are in department 10 or 30. Label the columns Employee and Monthly Salary respectively.
select ename as 'Employee',salary as 'Monthly Salary' from emp where salary>1500 and deptno in(10,30)




-- 13. Display the name, job, and salary of all the employees whose job is MANAGER or ANALYST and their salary is not equal to 1000, 3000, or 5000.
select ename,job,salary from emp where job in ('manager','analyst') and salary not in (1000,3000,5000)




-- 14. Display the name, salary and commission for all employees whose commission amount is greater than their salary increased by 10%.
select ename,salary,comm as 'commission' from emp where comm>(salary * 1.10)




-- 15. Display the name of all employees who have two Ls in their name and are in department 30 or their manager is 7782.
select ename from emp where ename like '%L%L%' and (deptno=30 or mgr_id=7782)


-- 16. Display the names of employees with experience of over 30 years and under 40 yrs. Count the total number of employees.
--i took 1st january 2025 as the end date
select count(*) as 'TotalEmployees' from emp where datediff(year,hire_date,'01-Jan-2025')>30 and datediff(year,hire_date,'01-Jan-2025')<40
select ename from emp where datediff(year,hire_date,'01-Jan-2025')>30 and datediff(year,hire_date,'01-Jan-2025')<40



-- 17. Retrieve the names of departments in ascending order and their employees in descending order.
select emp.ename,dept.dname from emp join dept on emp.deptno=dept.deptno order by dname ASC,ename DESC


-- 18. Find out experience of MILLER.
--i took 1st january 2025 as the end date
select ename,datediff(year,hire_date,'01-Jan-2025') as 'experience' from emp where ename='miller'
