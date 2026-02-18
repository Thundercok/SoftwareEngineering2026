CREATE TABLE Customers (
Id INT PRIMARY KEY IDENTITY(1,1),
Name NVARCHAR(100),
Email NVARCHAR(100)
);

INSERT INTO Customers (Name, Email) VALUES ('John Doe','john.doe@tdtu.com');
INSERT INTO Customers (Name, Email) VALUES ('Jane Smith','jane.smith@tdtu.com');
INSERT INTO Customers (Name, Email) VALUES ('Trung Pham','trung.pham@tdtu.com');
INSERT INTO Customers (Name, Email) VALUES ('Emily Davis','emily.davis@tdtu.com');
INSERT INTO Customers (Name, Email) VALUES ('Thai Pham','thai.pham@etdtu.com');