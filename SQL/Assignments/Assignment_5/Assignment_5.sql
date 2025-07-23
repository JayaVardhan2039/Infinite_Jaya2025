/*
1. Write a T-Sql based procedure to generate complete payslip of a given employee with respect to the following condition

   a) HRA as 10% of Salary
   b) DA as 20% of Salary
   c) PF as 8% of Salary
   d) IT as 5% of Salary
   e) Deductions as sum of PF and IT
   f) Gross Salary as sum of Salary, HRA, DA
   g) Net Salary as Gross Salary - Deductions

Print the payslip neatly with all details
*/


create or alter procedure proc_slip(@empid int)
as
begin
  --all variables
  declare @salary int, @hra int, @da int, @pf int, @it int
  declare @deductions int, @grosssalary int, @netsalary int
  select @salary = salary from emp where empno = @empid
  if @salary is null
   begin
    print 'Employee not found.'
    return
   end
  set @hra = 0.1 * @salary
  set @da = 0.2 * @salary
  set @pf = 0.08 * @salary
  set @it = 0.05 * @salary
  set @deductions = @pf+@it
  set @grosssalary = @salary+@hra+@da
  set @netsalary = @grosssalary-@deductions
  if @grosssalary<@deductions
  begin
   print 'Not possible'
  end
  --printing
    print'YOUR PAY SLIP'
    print'.............................................'
    print'Employee ID     :'+cast(@empid as varchar(20))
    print'Basic Salary    :'+cast(@salary as varchar(20))
    print'HRA (10%)       :'+cast(@hra as varchar(20))
    print'DA (20%)        :'+cast(@da as varchar(20))
    print'PF (8%)         :'+cast(@pf as varchar(20))
    print'IT (5%)         :'+cast(@it as varchar(20))
    print'Deductions      :'+cast(@deductions as varchar(20))
    print'Gross Salary    :'+cast(@grosssalary as varchar(20))
    print'Net Salary      :'+cast(@netsalary as varchar(20))
end


exec proc_slip @empid = 7369;










/*
2.  Create a trigger to restrict data manipulation on EMP table during General holidays. Display the error message like “Due to Independence day you cannot manipulate data” or "Due To Diwali", you cannot manipulate" etc

Note: Create holiday table with (holiday_date,Holiday_name). Store at least 4 holiday details. try to match and stop manipulation 
*/

select * from emp

--holidays table
create table holiday (
    holiday_date date,
    holiday_name varchar(20))

insert into holiday values 
('2025-08-15', 'Independence Day'),
('2025-12-25', 'Christmas'),
('2025-10-21', 'Diwali'),
('2025-01-26', 'Republic Day')

--trigger
create or alter trigger holidays_trig
on emp
instead of insert, update, delete
as
begin
	declare @today date = '2025-12-25';
    declare @holiday_name varchar(50);

    select @holiday_name = holiday_name 
    from holiday 
    where holiday_date = @today;

    if @holiday_name is not null
    begin
        raiserror('Due to %s, you cannot manipulate data', 16, 1, @holiday_name);
        rollback;
    end
end

--testing manipulation on emp
insert into emp values (2, 'Jaya', 'developer', null, '2025-07-23', 10000, null, 20);

/*
Output message
Msg 50000, Level 16, State 1, Procedure holidays_trig, Line 16 [Batch Start Line 118]
Due to Christmas, you cannot manipulate data
Msg 3609, Level 16, State 1, Line 119
The transaction ended in the trigger. The batch has been aborted.
*/