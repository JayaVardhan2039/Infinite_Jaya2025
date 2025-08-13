
-- drop all foreign key constraints
declare @sql nvarchar(max) = ''
select @sql += 'alter table [' + object_name(parent_object_id) + '] drop constraint [' + name + '];'
from sys.foreign_keys
exec sp_executesql @sql
-- drop all triggers
declare @triggers nvarchar(max) = ''
select @triggers += 'drop trigger [' + name + '];'
from sys.triggers
exec sp_executesql @triggers

-- drop all tables
declare @tables nvarchar(max) = ''
select @tables += 'drop table [' + name + '];'
from sys.tables
exec sp_executesql @tables
-- drop all foreign key constraints
declare @sql nvarchar(max) = ''
select @sql += 'alter table [' + object_name(parent_object_id) + '] drop constraint [' + name + '];'
from sys.foreign_keys
exec sp_executesql @sql
-- drop all triggers
declare @triggers nvarchar(max) = ''
select @triggers += 'drop trigger [' + name + '];'
from sys.triggers
exec sp_executesql @triggers

-- drop all tables
declare @tables nvarchar(max) = ''
select @tables += 'drop table [' + name + '];'
from sys.tables
exec sp_executesql @tables


use Mini_Project

create table trains (
    tno int primary key,
    tname varchar(50),
    [from] varchar(50),
    dest varchar(50),
    price varchar(100),
    classes_of_travel varchar(100),
    train_status varchar(10),
    seats_available int,
    total_seats int
);

create table users (
    userid int primary key identity,
    username varchar(50),
    pasword varchar(50),
    firstname varchar(50),
    lastname varchar(50),
    phone varchar(15) check (len(phone) = 10 and phone not like '%[^0-9]%'),
    email varchar(100) check (charindex('@', email) > 1),
    roles varchar(10),
    blocked bit default 0
);

create table trainclasses (
    class_id int primary key identity,
    tno int,
    class_name varchar(20),
    seats_available int,
    price decimal(10,2),
    foreign key (tno) references trains(tno)
);

create table bookings (
    booking_id int primary key identity,
    tno int,
    userid int,
    seats_booked int,
    booking_date datetime,
    total_amount decimal(10,2),
    travel_date date,
    berth_allotment varchar(20),
    class_name varchar(20),
    deleted bit default 0,
    foreign key (tno) references trains(tno),
    foreign key (userid) references users(userid)
);

create table cancellations (
    cancellation_id int primary key identity,
    booking_id int,
    seats_cancelled int,
    cancellation_date datetime,
    refund_amount decimal(10,2),
    refund_reason varchar(100),
    deleted bit default 0,
    foreign key (booking_id) references bookings(booking_id)
);

create table mails (
    mail_id int primary key identity,
    sender_id int,
    receiver_id int,
    sender_role varchar(10),
    receiver_role varchar(10),
    message_text nvarchar(max),
    sent_at datetime default getdate(),
    foreign key (sender_id) references users(userid),
    foreign key (receiver_id) references users(userid)
);

ALTER TABLE mails ADD read_status BIT DEFAULT 0;

CREATE TABLE PassengerDetails (
    passenger_id INT PRIMARY KEY IDENTITY,
    booking_id INT,
    passenger_name VARCHAR(100),
    passenger_age INT,
    FOREIGN KEY (booking_id) REFERENCES bookings(booking_id)
);
ALTER TABLE PassengerDetails
ADD status VARCHAR(10) DEFAULT 'booked';

create trigger trg_softdeletetrain
on trains
instead of delete
as
begin
    update trains
    set train_status = 'inactive'
    where tno in (select tno from deleted);
    print 'train marked as inactive instead of being deleted.';
end;

-- sample users
insert into users (username, pasword, firstname, lastname, phone, email, roles) values
('bindu', 'bindu123', 'bindu', 'rao', '9876543210', 'bindu@example.com', 'admin'),
('supraja', 'sup@345', 'supraja', 'devi', '9123456780', 'supraja@example.com', 'user'),
('vyshnavi', 'vyshu@567', 'vyshnavi', 'kumar', '9988776655', 'vyshnavi@example.com', 'user');
insert into users (username, pasword, firstname, lastname, phone, email, roles) values('chinnu','chinnu123','chinnu','babu','9876512340','chinnu@eg.com','user')
-- sample trains with 5 sources and 5 destinations
insert into trains values
(10001, 'vande bharat', 'chennai', 'bangalore', '1500,2500', 'sleeper,2ac', 'active', 60, 60),
(10002, 'rajdhani express', 'chennai', 'delhi', '3500,4500', '1ac,2ac', 'active', 40, 40),
(10003, 'godavari express', 'visakhapatnam', 'secunderabad', '1200,3000', 'sleeper,2ac', 'active', 54, 54),
(10004, 'coromandel express', 'kolkata', 'chennai', '1800,3200', 'sleeper,2ac', 'active', 70, 70),
(10005, 'konark express', 'mumbai', 'visakhapatnam', '2000,3500', 'sleeper,2ac', 'active', 65, 65),
(10006, 'garib rath', 'delhi', 'mumbai', '1000,2200', 'sleeper,3ac', 'active', 80, 80),
(10007, 'duronto express', 'bangalore', 'mumbai', '1600,2800', 'sleeper,2ac', 'active', 75, 75),
(10008, 'east coast express', 'visakhapatnam', 'kolkata', '1700,2900', 'sleeper,2ac', 'active', 68, 68),
(10009, 'chennai express', 'mumbai', 'chennai', '1900,3100', 'sleeper,2ac', 'active', 72, 72),
(10010, 'hyderabad express', 'secunderabad', 'delhi', '2100,3300', 'sleeper,2ac', 'active', 66, 66);

-- train classes for all trains
insert into trainclasses (tno, class_name, seats_available, price) values
(10001, 'sleeper', 30, 1500),
(10001, '2ac', 30, 2500),
(10002, '1ac', 20, 3500),
(10002, '2ac', 20, 4500),
(10003, 'sleeper', 30, 1200),
(10003, '2ac', 24, 3000),
(10004, 'sleeper', 40, 1800),
(10004, '2ac', 30, 3200),
(10005, 'sleeper', 35, 2000),
(10005, '2ac', 30, 3500),
(10006, 'sleeper', 40, 1000),
(10006, '3ac', 40, 2200),
(10007, 'sleeper', 35, 1600),
(10007, '2ac', 40, 2800),
(10008, 'sleeper', 38, 1700),
(10008, '2ac', 30, 2900),
(10009, 'sleeper', 36, 1900),
(10009, '2ac', 36, 3100),
(10010, 'sleeper', 33, 2100),
(10010, '2ac', 33, 3300);



select * from Users
select * from Trains
select * from TrainClasses
select * from Bookings
select * from PassengerDetails
select * from Cancellations
select * from mails






INSERT INTO PassengerDetails (booking_id, passenger_name, passenger_age) VALUES
(1, 'Ravi Kumar', 34),
(2, 'Sneha Reddy', 28),
(3, 'Arjun Das', 45),
(4, 'Meena Iyer', 52),
(5, 'Karthik Rao', 30),
(6, 'Anjali Sharma', 26),
(3, 'Priya Nair', 32),     -- Multiple passengers for booking_id 3
(4, 'Vikram Joshi', 40),   -- Multiple passengers for booking_id 4
(5, 'Neha Verma', 29),     -- Multiple passengers for booking_id 5
(6, 'Rohan Mehta', 35);    -- Multiple passengers for booking_id 6


--to get confirmed booking of logged user
CREATE OR ALTER PROCEDURE sp_GetConfirmedBookings
    @userid INT
AS
BEGIN
    SELECT 
        b.booking_id, 
        b.tno, 
        b.class_name, 
        b.seats_booked, 
        b.travel_date,
        p.passenger_name,
        p.passenger_age
    FROM Bookings b
    LEFT JOIN PassengerDetails p 
        ON b.booking_id = p.booking_id AND p.status = 'booked'
    WHERE b.userid = @userid 
      AND b.seats_booked > 0 
      AND b.deleted = 0
    ORDER BY b.travel_date DESC
END




--checking user is blocked or not
create or alter procedure sp_isuserblocked
    @userid int,
    @isblocked bit output
as
begin
    select @isblocked = blocked
    from users
    where userid = @userid;
end;



--sendind mails from user to all admins
create or alter procedure sp_sendmailtoadmins
    @sender_id int,
    @message_text nvarchar(max)
as
begin
    insert into mails (sender_id, receiver_id, sender_role, receiver_role, message_text)
    select @sender_id, userid, 'user', 'admin', @message_text
    from users
    where roles = 'admin';
end;


--sending mails from admin to user
create or alter procedure sp_sendmailtouser
    @sender_id int,
    @receiver_id int,
    @message_text nvarchar(max)
as
begin
    insert into mails (sender_id, receiver_id, sender_role, receiver_role, message_text)
    values (@sender_id, @receiver_id, 'admin', 'user', @message_text);
end;



--viewing inbox mails
create or alter procedure sp_viewinbox
    @user_id int,
    @role varchar(10)
as
begin
    select sender_id, sender_role, message_text, sent_at
    from mails
    where receiver_id = @user_id and receiver_role = @role
    order by sent_at desc;
	
UPDATE mails
    SET read_status = 1
    WHERE receiver_id = @user_id AND receiver_role = @role AND read_status = 0;

end;





--source cities
create or alter procedure sp_getdistinctsourcecities
as
begin
    select distinct [from] as sourcecity
    from trains
    where train_status = 'active';
end;





--cities sorting for destinations for source
create or alter procedure sp_getdestinationcitiesforsource
    @source varchar(50)
as
begin
    select distinct dest as destinationcity
    from trains
    where [from] = @source and train_status = 'active';
end;





--available trains
create or alter procedure sp_getavailabletrainswithclasses
    @source varchar(50),
    @destination varchar(50)
as
begin
    select 
        t.tno,
        t.tname,
        t.[from],
        t.dest,
        string_agg(tc.class_name, ', ') as classes
    from trains t
    join trainclasses tc on t.tno = tc.tno
    where t.train_status = 'active' and t.[from] = @source and t.dest = @destination
    group by t.tno, t.tname, t.[from], t.dest;
end;




--booking tickets
create or alter procedure sp_booktickets
    @userid int,
    @tno int,
    @class_name varchar(20),
    @seats_to_book int,
    @travel_date date,
    @berth_allotment varchar(20),
    @booking_id int output,
	@total_amount_bill decimal(10,2) output
as
begin
    declare @price decimal(10,2);
    declare @classseatsavailable int;
    declare @trainseatsavailable int;

    select @classseatsavailable = seats_available, @price = price
    from trainclasses
    where tno = @tno and class_name = @class_name;

    if @classseatsavailable is null or @classseatsavailable < @seats_to_book
    begin
        raiserror('Not enough class seats available.', 16, 1);
        return;
    end

    select @trainseatsavailable = seats_available
    from trains
    where tno = @tno;

    if @trainseatsavailable is null or @trainseatsavailable < @seats_to_book
    begin
        raiserror('Not enough train seats available.', 16, 1);
        return;
    end

    declare @total_amount decimal(10,2) = @price * @seats_to_book;
	set @total_amount_bill=@price * @seats_to_book;

    insert into bookings (tno, userid, seats_booked, booking_date, total_amount, travel_date, berth_allotment, class_name)
    values (@tno, @userid, @seats_to_book, getdate(), @total_amount, @travel_date, @berth_allotment, @class_name);

    set @booking_id = scope_identity();

    update trainclasses
    set seats_available = seats_available - @seats_to_book
    where tno = @tno and class_name = @class_name;

    update trains
    set seats_available = seats_available - @seats_to_book
    where tno = @tno;
end;

create or alter procedure sp_booktickets
    @userid int,
    @tno int,
    @class_name varchar(20),
    @seats_to_book int,
    @travel_date date,
    @berth_allotment varchar(20),
    @booking_id int output,
    @total_amount_bill decimal(10,2) output
as
begin
    declare @price decimal(10,2);
    declare @classseatsavailable int;
    declare @trainseatsavailable int;
    declare @train_status varchar(20);

    -- Check train status
    select @train_status = train_status
    from trains
    where tno = @tno;

    if @train_status is null or @train_status <> 'active'
    begin
        raiserror('Train is not active.', 16, 1);
        return;
    end

    -- Check class seat availability and price
    select @classseatsavailable = seats_available, @price = price
    from trainclasses
    where tno = @tno and class_name = @class_name;

    if @classseatsavailable is null or @classseatsavailable < @seats_to_book
    begin
        raiserror('Not enough class seats available.', 16, 1);
        return;
    end

    -- Check overall train seat availability
    select @trainseatsavailable = seats_available
    from trains
    where tno = @tno;

    if @trainseatsavailable is null or @trainseatsavailable < @seats_to_book
    begin
        raiserror('Not enough train seats available.', 16, 1);
        return;
    end

    -- Calculate total amount
    declare @total_amount decimal(10,2) = @price * @seats_to_book;
    set @total_amount_bill = @total_amount;

    -- Insert booking
    insert into bookings (tno, userid, seats_booked, booking_date, total_amount, travel_date, berth_allotment, class_name)
    values (@tno, @userid, @seats_to_book, getdate(), @total_amount, @travel_date, @berth_allotment, @class_name);

    set @booking_id = scope_identity();

    -- Update seat availability
    update trainclasses
    set seats_available = seats_available - @seats_to_book
    where tno = @tno and class_name = @class_name;

    update trains
    set seats_available = seats_available - @seats_to_book
    where tno = @tno;
end;





--cancelling tickets
create or alter procedure sp_canceltickets
    @booking_id int,
    @seats_to_cancel int,
    @refund_amount decimal(10,2) output,
    @refund_reason varchar(100) output
as
begin
    declare @tno int, @seats_booked int, @booking_date datetime, @class_name varchar(20);

    select @tno = tno, @seats_booked = seats_booked, @booking_date = booking_date, @class_name = class_name
    from bookings
    where booking_id = @booking_id and deleted = 0;

    if @seats_booked is null
    begin
        raiserror('Invalid Booking ID.', 16, 1);
        return;
    end

    if @seats_to_cancel > @seats_booked
    begin
        raiserror('Cannot cancel more seats than booked.', 16, 1);
        return;
    end

    declare @days_before_travel int = datediff(day, getdate(), @booking_date);
    declare @refund_rate decimal(4,2);

    if @days_before_travel > 90
begin
    set @refund_rate = 0.50;
    set @refund_reason = 'Cancelled > 90 days before travel';
end
else if @days_before_travel > 30 and @days_before_travel <= 90
begin
    set @refund_rate = 0.25;
    set @refund_reason = 'Cancelled between 31 and 90 days before travel';
end
else
begin
    set @refund_rate = 0.00;
    set @refund_reason = 'Cancelled ≤ 30 days before travel';
end


    set @refund_amount = @seats_to_cancel * @refund_rate * 100;

    insert into cancellations (booking_id, seats_cancelled, cancellation_date, refund_amount, refund_reason)
    values (@booking_id, @seats_to_cancel, getdate(), @refund_amount, @refund_reason);

    update trainclasses
    set seats_available = seats_available + @seats_to_cancel
    where tno = @tno and class_name = @class_name;

    update trains
    set seats_available = seats_available + @seats_to_cancel
    where tno = @tno;

    update bookings
    set seats_booked = seats_booked - @seats_to_cancel
    where booking_id = @booking_id;
end;


CREATE OR ALTER PROCEDURE sp_inactivate_train_and_refund
    @tno INT,
    @admin_id INT
AS
BEGIN
    DECLARE @booking_id INT, @user_id INT, @amount DECIMAL(10,2), @message NVARCHAR(MAX);

    -- Step 1: Mark train as inactive
    UPDATE trains SET train_status = 'inactive' WHERE tno = @tno;

    -- Step 2: Process bookings
    DECLARE booking_cursor CURSOR FOR
    SELECT booking_id, userid, total_amount FROM bookings WHERE tno = @tno AND deleted = 0;

    OPEN booking_cursor;
    FETCH NEXT FROM booking_cursor INTO @booking_id, @user_id, @amount;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Mark booking as deleted
        UPDATE bookings SET deleted = 1 WHERE booking_id = @booking_id;

        -- Insert cancellation record
        INSERT INTO cancellations (booking_id, seats_cancelled, cancellation_date, refund_amount, refund_reason)
        SELECT booking_id, seats_booked, GETDATE(), total_amount, 'Train Inactivated - Full Refund'
        FROM bookings WHERE booking_id = @booking_id;

        -- Send mail to user
        SET @message = 'Your booking (ID: ' + CAST(@booking_id AS VARCHAR) + ') for train ' + CAST(@tno AS VARCHAR) +
                       ' has been cancelled due to train inactivation. Full refund of ₹' + CAST(@amount AS VARCHAR) +
                       ' has been processed.';

        INSERT INTO mails (sender_id, receiver_id, sender_role, receiver_role, message_text)
        VALUES (@admin_id, @user_id, 'admin', 'user', @message);

        FETCH NEXT FROM booking_cursor INTO @booking_id, @user_id, @amount;
    END

    CLOSE booking_cursor;
    DEALLOCATE booking_cursor;

    -- Step 3: Restore seat counts
    UPDATE trains SET seats_available = total_seats WHERE tno = @tno;

    UPDATE tc
    SET tc.seats_available = original.seats_available
    FROM trainclasses tc
    JOIN (
        SELECT class_id, seats_available
        FROM trainclasses
        WHERE tno = @tno
    ) AS original ON tc.class_id = original.class_id
    WHERE tc.tno = @tno;
END;


CREATE OR ALTER PROCEDURE sp_AddPassengerDetails
    @booking_id INT,
    @passenger_name VARCHAR(100),
    @passenger_age INT,
    @status VARCHAR(10)
AS
BEGIN
    INSERT INTO PassengerDetails (booking_id, passenger_name, passenger_age, status)
    VALUES (@booking_id, @passenger_name, @passenger_age, @status);
END;






