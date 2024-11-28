create database Printing_ServiceData;
GO 

use Printing_ServiceData;
go

create table SPSO(
	SPSO_ID char(6) primary key
);
go

create table Student(
	Student_ID char(6) primary key,
	Sname varchar(50),
	Remain_page int,
	SPSO_ID char(6),
	constraint a foreign key (SPSO_ID) references SPSO(SPSO_ID)
);
go

create table Printer(
	Printer_ID char(6) primary key,
	Paper_exist int,
	PlaceAt char(2),
	IsDisable int
);
go

create table Document(
	Document_ID char(6) primary key,
	A3Page int,
	Pages int,
	Type varchar(6),
	Ratio float,
	DName varchar(50)
);
go

create table Printing(
	Printer_ID char(6),
	Student_ID char(6),
	Document_ID char(6),
    primary key(Document_ID, Student_ID),
	constraint b foreign key (Printer_ID) references Printer(Printer_ID),
	constraint c foreign key (Student_ID) references Student(Student_ID),
	constraint d foreign key (Document_ID) references Document(Document_ID)
);
go

create table Paper_transaction(
	Transaction_ID char(6) primary key,
	NumberPage int,
	BuyTime datetime,
	Student_ID char(6),
	constraint e foreign key (Student_ID) references Student(Student_ID)
);
go

DECLARE @sql NVARCHAR(MAX) = '';

-- Generate a script to disable constraints for all user tables
SELECT @sql += 'ALTER TABLE [' + OBJECT_SCHEMA_NAME(parent_object_id) + '].[' + OBJECT_NAME(parent_object_id) + '] NOCHECK CONSTRAINT [' + name + '];' + CHAR(13)
FROM sys.foreign_keys -- Includes only foreign key constraints
WHERE is_ms_shipped = 0;

-- Disable check constraints as well
SELECT @sql += 'ALTER TABLE [' + SCHEMA_NAME(t.schema_id) + '].[' + t.name + '] NOCHECK CONSTRAINT [' + c.name + '];' + CHAR(13)
FROM sys.check_constraints c
JOIN sys.tables t ON c.parent_object_id = t.object_id
WHERE t.is_ms_shipped = 0;

-- Execute the generated SQL
EXEC sp_executesql @sql;

INSERT INTO Printer (Printer_ID, Paper_exist, PlaceAt, IsDisable)
VALUES 
    ('PR0001', 100, 'B1', 0), 
    ('PR0002', 200, 'C1', 1), 
    ('PR0003', 50,  'A1', 1),
    ('PR0004', 75,  'B2', 0),
    ('PR0005', 120, 'C2', 1),
    ('PR0006', 90,  'A2', 0),
    ('PR0007', 300, 'B3', 0),
    ('PR0008', 0,   'C3', 1),
    ('PR0009', 45,  'A3', 0),
    ('PR0010', 150, 'B4', 1);
GO
-- Insert 10 sample records into the Student table
INSERT INTO Student (Student_ID, Sname, Remain_page, SPSO_ID)
VALUES 
    ('ST0001', 'Huong', 120, 'SPSO01'),
    ('ST0002', 'Minh',  90,  'SPSO02'),
    ('ST0003', 'Loc', 75,  'SPSO01'),
    ('ST0004', 'Long', 60,  'SPSO03'),
    ('ST0005', 'Hao',  130, 'SPSO02'),
    ('ST0006', 'Quan', 45,  'SPSO01'),
    ('ST0007', 'Grace', 80,  'SPSO03'),
    ('ST0008', 'Hank',  100, 'SPSO02'),
    ('ST0009', 'Ivy',  110, 'SPSO01'),
    ('ST0010', 'John', 95,  'SPSO03');
GO

INSERT INTO SPSO(SPSO_ID)
VALUES 
	('SPSO01'),
	('SPSO02'),
	('SPSO03');
GO

INSERT INTO Document (Document_ID, A3Page, Pages, Type, Ratio, DName)
VALUES
    ('D00001', 1, 2, '.png', 1.5, 'DiscreteStructure'),
    ('D00002', 0, 4, '.pdf',  2.0, 'DataStructure'), 
    ('D00003', 1, 2, '.txt',   1.2, 'SoftwareEngineer'),
    ('D00004', 0, 4, '.docx',   1.8, 'DataWork'),
    ('D00005', 1, 1, '.pdf',  1.3, 'FreeTime');
GO
INSERT INTO Printing (Printer_ID, Student_ID, Document_ID)
VALUES
    ('PR0001', 'ST0001', 'D00001');
GO
INSERT INTO Printing (Printer_ID, Student_ID, Document_ID)
VALUES
    ('PR0005', 'ST0003', 'D00005');
GO
INSERT INTO Printing (Printer_ID, Student_ID, Document_ID)
VALUES
    ('PR0004', 'ST0002', 'D00004');
GO
-- Insert sample data into the Paper_transaction table
INSERT INTO Paper_transaction (Transaction_ID, NumberPage, BuyTime, Student_ID)
VALUES
    ('TR0001', 50, '2024-11-01 10:30:00', 'ST0001'),
    ('TR0002', 75, '2024-11-02 14:00:00', 'ST0002'),
    ('TR0003', 30, '2024-11-03 09:15:00', 'ST0003');
GO
INSERT INTO Paper_transaction (Transaction_ID, NumberPage, BuyTime, Student_ID)
VALUES
    ('TR0004', 120, '2024-11-04 11:45:00', 'ST0001');
GO
select * from student;
go
create trigger UpdateRemainPage
on Paper_transaction
after insert
as
begin
    update Student
    set Remain_page = Remain_page + i.NumberPage
    from Student s
    inner join inserted i on s.Student_ID = i.Student_ID;
END;
GO


CREATE TRIGGER trg_UpdateRemainPageBeforeInsert
ON Printing
INSTEAD OF INSERT
AS
BEGIN
    -- Declare variables to hold the values from the inserted row
    DECLARE @Document_ID CHAR(6), @Student_ID CHAR(6), @Printer_ID CHAR(6), @Pages INT;

    -- Get the values from the inserted row
    SELECT 
        @Document_ID = i.Document_ID,
        @Student_ID = i.Student_ID,
        @Printer_ID = i.Printer_ID
    FROM inserted i;

    -- Get the number of pages from the Document table
    SELECT @Pages = Pages
    FROM Document
    WHERE Document_ID = @Document_ID;

    -- Check if there are enough pages for the student
    UPDATE Student
    SET Remain_page = Remain_page - @Pages
    WHERE Student_ID = @Student_ID;

    UPDATE Printer
    SET Paper_exist = Paper_exist - @Pages
    WHERE Printer_ID = @Printer_ID;

    INSERT INTO Printing (Printer_ID, Student_ID, Document_ID)
    SELECT @Printer_ID, @Student_ID, @Document_ID;
END;
GO
