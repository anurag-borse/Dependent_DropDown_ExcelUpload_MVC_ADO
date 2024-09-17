create database Task

use Task

create table Employee (
Id int identity(1,1),
EmployeeName varchar(30),
EmployeeDept varchar(30),
EmployeeSalary int ,
EmployeeState varchar(30),
EmployeeTalukha varchar(30),
EmployeeCity varchar(30)
);



create procedure getEmployees
as
begin  
	select Id,EmployeeName, EmployeeDept,EmployeeSalary,EmployeeState,EmployeeTalukha,EmployeeCity from Employee
end

exec getEmployees


create procedure addEmployee(
@EmployeeName varchar(30),
@EmployeeDept varchar(30),
@EmployeeSalary int ,
@EmployeeState varchar(30) =NULL,
@EmployeeTalukha varchar(30) = NULL,
@EmployeeCity varchar(30) = NULL
)
as 
begin 
insert into Employee(EmployeeName,EmployeeDept ,EmployeeSalary ,EmployeeState,EmployeeTalukha ,EmployeeCity)
values (@EmployeeName,@EmployeeDept,@EmployeeSalary,@EmployeeState,@EmployeeTalukha,@EmployeeCity);
end

EXEC addEmployee 
    @EmployeeName = 'John Doe',
    @EmployeeSalary = 30,
    @EmployeeDept = 'HR',
    @EmployeeState = 'Maharashtra',
    @EmployeeTalukha = 'Pune',
    @EmployeeCity = 'Pune';




CREATE TABLE States (
    StateID INT PRIMARY KEY IDENTITY(1,1),
    StateName NVARCHAR(100) NOT NULL
);

CREATE TABLE Talukhas (
    TalukhaID INT PRIMARY KEY IDENTITY(1,1),
    StateID INT,
    TalukhaName NVARCHAR(100) NOT NULL,
    FOREIGN KEY (StateID) REFERENCES States(StateID)
);

CREATE TABLE Cities (
    CityID INT PRIMARY KEY IDENTITY(1,1),
    TalukhaID INT,
    CityName NVARCHAR(100) NOT NULL,
    FOREIGN KEY (TalukhaID) REFERENCES Talukhas(TalukhaID)
);


select * from States
select * from Talukhas
select * from Cities

CREATE PROCEDURE GetStates
AS	
BEGIN
    SELECT StateID, StateName FROM States;
END

CREATE PROCEDURE GetTalukhasByState
    @StateID INT
AS
BEGIN
    SELECT TalukhaID, TalukhaName FROM Talukhas WHERE StateID = @StateID;
END

CREATE PROCEDURE GetCitiesByTalukha
    @TalukhaID INT
AS
BEGIN
    SELECT CityID, CityName FROM Cities WHERE TalukhaID = @TalukhaID;
END

INSERT INTO States (StateName) VALUES 
('Maharashtra'),
('Karnataka'),
('Tamil Nadu'),
('Gujarat'),
('Rajasthan'),
('Uttar Pradesh'),
('West Bengal'),
('Madhya Pradesh'),
('Andhra Pradesh'),
('Kerala');


-- Maharashtra
INSERT INTO Talukhas (StateID, TalukhaName) VALUES 
(1, 'Pune'),
(1, 'Nashik'),
(1, 'Nagpur'),
(1, 'Thane'),
(1, 'Aurangabad');

-- Karnataka
INSERT INTO Talukhas (StateID, TalukhaName) VALUES 
(2, 'Bengaluru Urban'),
(2, 'Mysuru'),
(2, 'Belagavi'),
(2, 'Dakshina Kannada'),
(2, 'Tumakuru');

-- Tamil Nadu
INSERT INTO Talukhas (StateID, TalukhaName) VALUES 
(3, 'Chennai'),
(3, 'Coimbatore'),
(3, 'Madurai'),
(3, 'Salem'),
(3, 'Tiruchirappalli');

-- Gujarat
INSERT INTO Talukhas (StateID, TalukhaName) VALUES 
(4, 'Ahmedabad'),
(4, 'Surat'),
(4, 'Vadodara'),
(4, 'Rajkot'),
(4, 'Bhavnagar');

-- Rajasthan
INSERT INTO Talukhas (StateID, TalukhaName) VALUES 
(5, 'Jaipur'),
(5, 'Jodhpur'),
(5, 'Udaipur'),
(5, 'Ajmer'),
(5, 'Kota');

-- Uttar Pradesh
INSERT INTO Talukhas (StateID, TalukhaName) VALUES 
(6, 'Lucknow'),
(6, 'Kanpur'),
(6, 'Varanasi'),
(6, 'Agra'),
(6, 'Meerut');

-- West Bengal
INSERT INTO Talukhas (StateID, TalukhaName) VALUES 
(7, 'Kolkata'),
(7, 'Howrah'),
(7, 'Darjeeling'),
(7, 'Bardhaman'),
(7, 'Hooghly');

-- Madhya Pradesh
INSERT INTO Talukhas (StateID, TalukhaName) VALUES 
(8, 'Bhopal'),
(8, 'Indore'),
(8, 'Gwalior'),
(8, 'Jabalpur'),
(8, 'Ujjain');

-- Andhra Pradesh
INSERT INTO Talukhas (StateID, TalukhaName) VALUES 
(9, 'Visakhapatnam'),
(9, 'Vijayawada'),
(9, 'Guntur'),
(9, 'Tirupati'),
(9, 'Kurnool');

-- Kerala
INSERT INTO Talukhas (StateID, TalukhaName) VALUES 
(10, 'Thiruvananthapuram'),
(10, 'Ernakulam'),
(10, 'Kozhikode'),
(10, 'Thrissur'),
(10, 'Kollam');


select * from States
select * from Talukhas

-- Maharashtra
INSERT INTO Cities (TalukhaID, CityName) VALUES 
(1, 'Pune'),
(1, 'Pimpri-Chinchwad'),
(1, 'Talegaon'),
(1, 'Lonavala'),
(1, 'Alandi'),
(2, 'Nashik'),
(2, 'Igatpuri'),
(2, 'Sinnar'),
(2, 'Trimbak'),
(2, 'Malegaon'),
(3, 'Nagpur'),
(3, 'Kamptee'),
(3, 'Hingna'),
(3, 'Umred'),
(3, 'Katol'),
(4, 'Thane'),
(4, 'Kalyan'),
(4, 'Ulhasnagar'),
(4, 'Bhiwandi'),
(4, 'Ambernath'),
(5, 'Aurangabad'),
(5, 'Paithan'),
(5, 'Sillod'),
(5, 'Kannad'),
(5, 'Vaijapur');

-- Karnataka
INSERT INTO Cities (TalukhaID, CityName) VALUES 
(6, 'Bengaluru'),
(6, 'Yelahanka'),
(6, 'Kengeri'),
(6, 'Anekal'),
(6, 'Krishnarajapura'),
(7, 'Mysuru'),
(7, 'Nanjangud'),
(7, 'Hunsur'),
(7, 'T. Narasipura'),
(7, 'K.R. Nagar'),
(8, 'Belagavi'),
(8, 'Gokak'),
(8, 'Chikkodi'),
(8, 'Khanapur'),
(8, 'Ramdurg'),
(9, 'Mangaluru'),
(9, 'Bantwal'),
(9, 'Puttur'),
(9, 'Sullia'),
(9, 'Belthangady'),
(10, 'Tumakuru'),
(10, 'Sira'),
(10, 'Tiptur'),
(10, 'Gubbi'),
(10, 'Kunigal');

-- Tamil Nadu
INSERT INTO Cities (TalukhaID, CityName) VALUES 
(11, 'Chennai'),
(11, 'Ambattur'),
(11, 'Madhavaram'),
(11, 'Sholinganallur'),
(11, 'Alandur'),
(12, 'Coimbatore'),
(12, 'Pollachi'),
(12, 'Mettupalayam'),
(12, 'Tiruppur'),
(12, 'Valparai'),
(13, 'Madurai'),
(13, 'Usilampatti'),
(13, 'Melur'),
(13, 'Vadipatti'),
(13, 'Thirumangalam'),
(14, 'Salem'),
(14, 'Attur'),
(14, 'Mettur'),
(14, 'Omalur'),
(14, 'Sankagiri'),
(15, 'Tiruchirappalli'),
(15, 'Srirangam'),
(15, 'Manapparai'),
(15, 'Thuraiyur'),
(15, 'Lalgudi');

-- Gujarat
INSERT INTO Cities (TalukhaID, CityName) VALUES 
(16, 'Ahmedabad'),
(16, 'Dholka'),
(16, 'Sanand'),
(16, 'Bavla'),
(16, 'Viramgam'),
(17, 'Surat'),
(17, 'Bardoli'),
(17, 'Mandvi'),
(17, 'Olpad'),
(17, 'Kamrej'),
(18, 'Vadodara'),
(18, 'Padra'),
(18, 'Savli'),
(18, 'Dabhoi'),
(18, 'Karjan'),
(19, 'Rajkot'),
(19, 'Gondal'),
(19, 'Jetpur'),
(19, 'Dhoraji'),
(19, 'Upleta'),
(20, 'Bhavnagar'),
(20, 'Mahuva'),
(20, 'Talaja'),
(20, 'Palitana'),
(20, 'Gariadhar');

-- Rajasthan
INSERT INTO Cities (TalukhaID, CityName) VALUES 
(21, 'Jaipur'),
(21, 'Sanganer'),
(21, 'Amer'),
(21, 'Chomu'),
(21, 'Kotputli'),
(22, 'Jodhpur'),
(22, 'Osian'),
(22, 'Phalodi'),
(22, 'Bilara'),
(22, 'Shergarh'),
(23, 'Udaipur'),
(23, 'Girwa'),
(23, 'Kherwara'),
(23, 'Salumbar'),
(23, 'Vallabhnagar'),
(24, 'Ajmer'),
(24, 'Kishangarh'),
(24, 'Beawar'),
(24, 'Kekri'),
(24, 'Nasirabad'),
(25, 'Kota'),
(25, 'Ladpura'),
(25, 'Ramganj Mandi'),
(25, 'Sangod'),
(25, 'Itawa');

-- Uttar Pradesh
INSERT INTO Cities (TalukhaID, CityName) VALUES 
(26, 'Lucknow'),
(26, 'Malihabad'),
(26, 'Mohanlalganj'),
(26, 'Bakshi Ka Talab'),
(26, 'Sarojini Nagar'),
(27, 'Kanpur'),
(27, 'Bilhaur'),
(27, 'Ghatampur'),
(27, 'Sarsaul'),
(27, 'Bithoor'),
(28, 'Varanasi'),
(28, 'Pindra'),
(28, 'Cholapur'),
(28, 'Arajiline'),
(28, 'Sewapuri'),
(29, 'Agra'),
(29, 'Kiraoli'),
(29, 'Fatehabad'),
(29, 'Kheragarh'),
(29, 'Bah'),
(30, 'Meerut'),
(30, 'Sardhana'),
(30, 'Mawana'),
(30, 'Rohta'),
(30, 'Parikshitgarh');

-- West Bengal
INSERT INTO Cities (TalukhaID, CityName) VALUES 
(31, 'Kolkata'),
(31, 'Alipore'),
(31, 'Behala'),
(31, 'Jadavpur')



exec GetTalukhasByState @StateID =1

exec GetCitiesByTalukha @TalukhaID = 9



create PROCEDURE UpdateEmployeeLocation
    @EmployeeID INT,
    @StateName NVARCHAR(100),
    @TalukhaName NVARCHAR(100),
    @CityName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Employee
    SET 
        EmployeeState = @StateName,
        EmployeeTalukha = @TalukhaName,
        EmployeeCity = @CityName
    WHERE 
        Id = @EmployeeID;
END



select * from Talukhas
select * from Cities where TalukhaID = 29


UPDATE Employee
    SET 
        EmployeeDept = 'KnockOut Chocker'
    WHERE 
        Id = 2;






create table Orders
(
Id int identity(1,1),
OrderID int,
OrderDate date,
OrderQuantity int,
Sales int ,
ShipMode varchar(40),
Profit int,
UnitPrice int,
CustomerName varchar(40),
CustomerSegment varchar(40),
ProductCategory varchar(40)
);

--OrderID	OrderDate	OrderQuantity	Sales	ShipMode	Profit	UnitPrice	CustomerName	CustomerSegment	ProductCategory
insert into Orders values(
3	,'13-Oct-2010',	6,	261.54,	'Regular Air',	-213.25,	38.94,	'Muhammed MacIntyre',	'Small Business',	'Office Supplies'

);

select * from Orders
DELETE FROM Orders;
DBCC CHECKIDENT ('Orders', RESEED, 0);



CREATE PROCEDURE InsertOrder
    @OrderID INT,
    @OrderDate DATE,
    @OrderQuantity INT,
    @Sales INT,
    @ShipMode VARCHAR(40),
    @Profit INT,
    @UnitPrice INT,
    @CustomerName VARCHAR(40),
    @CustomerSegment VARCHAR(40),
    @ProductCategory VARCHAR(40)
AS
BEGIN
    INSERT INTO Orders (OrderID, OrderDate, OrderQuantity, Sales, ShipMode, Profit, UnitPrice, CustomerName, CustomerSegment, ProductCategory)
    VALUES (@OrderID, @OrderDate, @OrderQuantity, @Sales, @ShipMode, @Profit, @UnitPrice, @CustomerName, @CustomerSegment, @ProductCategory)
END

CREATE PROCEDURE GetOrder
as 
begin
 select OrderID, OrderDate, OrderQuantity, Sales, ShipMode, Profit, UnitPrice, CustomerName, CustomerSegment, ProductCategory 
 from  Orders
end

exec GetOrder

