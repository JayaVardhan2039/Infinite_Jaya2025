--Table creation
create table employee_details (
    empid int identity(1,1) primary key,
    name nvarchar(100),
    salary float,
    netsalary float,
    gender nvarchar(10)
);

drop table employee_details;

--inserting Procedure
create or alter procedure proc_employeedetails_insert
    @name nvarchar(100),
    @gender nvarchar(10),
    @givensalary float,
    @empid int output,
    @salary float output
as
begin
    declare @netsalary float;
    set @salary = @givensalary;
    set @netsalary = @salary * 0.9;

    insert into employee_details (name, salary, netsalary, gender)
    values (@name, @salary, @netsalary, @gender);

    set @empid = (select max(empid) from employee_details);
end;

--inserting procedure testing
declare @empid int, @salary float;

exec proc_employeedetailsinsert 
    @name = 'jayavardhan', 
    @gender = 'male', 
    @givensalary = 50000, 
    @empid = @empid output, 
    @salary = @salary output;

select * from employee_details;






--Updating procedure
create or alter procedure proc_salarybyempid_update
    @empid int,
    @updatedsalary float output
as
begin
    update employee_details
    set salary = salary + 100,
        netsalary = (salary + 100) * 0.9
    where empid = @empid;

    select @updatedsalary = salary
    from employee_details
    where empid = @empid;
end;

--updating procedure testing by giving empid=2
--before updating 
select * from employee_details
declare @updatedsalary float;


exec proc_salarybyempid_update 
    @empid = 2, 
    @updatedsalary = @updatedsalary output;
--after updating
select * from employee_details;

