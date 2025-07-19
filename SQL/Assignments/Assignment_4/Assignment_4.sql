--1.	Write a T-SQL Program to find the factorial of a given number.


declare @number int
set @number=5
declare @factresult int
set @factresult=1


while @number>=2
begin
 set @factresult = @factresult*@number
 set @number=@number-1
end

print 'The Factorial of the number is ' +  cast(@factresult as varchar(20))
















--2.	Create a stored procedure to generate multiplication table that accepts a number and generates up to a given number. 

create or alter proc mult_tab @multnumber int,@uptonumber int
as
begin
declare @temp int
set @temp=1
while @temp<=@uptonumber
 begin
  print cast(@multnumber as varchar(20)) + ' * ' + cast(@temp as varchar(20)) + ' = ' + cast(@multnumber*@temp as varchar(20))
  set @temp = @temp +1
 end
end

mult_tab 2,20









--3. Create a function to calculate the status of the student. If student score >=50 then pass, else fail. Display the data neatly

/*student table

Sid       Sname   
1         Jack
2         Rithvik
3         Jaspreeth
4         Praveen
5         Bisa
6         Suraj

Marks table

Mid      Sid     Score
1        1        23
2        6        95
3        4        98
4        2        17
5        3        53
6        5        13*/



-- create student table
create table student (
    sid int,
    sname varchar(20)
);

insert into student (sid, sname) values
(1, 'jack'),
(2, 'rithvik'),
(3, 'jaspreeth'),
(4, 'praveen'),
(5, 'bisa'),
(6, 'suraj');

create table marks (
    mid int,
    sid int,
    score int
);

insert into marks (mid, sid, score) values
(1, 1, 23),
(2, 6, 95),
(3, 4, 98),
(4, 2, 17),
(5, 3, 53),
(6, 5, 13);



create function func_calac(@marks int)
returns varchar(20)
as 
begin
 declare @stat varchar(20)
 if @marks>=50
 begin
  set @stat='pass'
 end
 else
 begin
  set @stat='fail'
 end
 return @stat
end




select s.sname as 'Studentname',m.score as 'Score',dbo.func_calac(m.score) as 'Status of student' from student s join marks m on s.sid=m.mid



