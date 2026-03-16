CREATE TABLE tblStudents (
	StudentID		INT PRIMARY KEY IDENTITY(1, 1),
	StudentName		NVARCHAR(50) NOT NULL,
	DateOfBirth		DATE NOT NULL,
	City			NVARCHAR(50) NOT NULL,
	Age				INT NOT NULL,
	YearOfEnroll	INT NOT NULL,
	Major			NVARCHAR(50),
	GPA				DECIMAL(3,2)
);

INSERT INTO tblStudents (StudentName, DateOfBirth, City, Age, YearOfEnroll, Major, GPA) VALUES
	('Trung Pham', '2000-01-15', 'HCMC', 24, 2018, 'Computer Science', 3.5),
	('Jane Smith', '1999-05-22', 'Los Angeles', 25, 2017, 'Mathematics', 3.8),
	('Alice Johnson', '2001-03-10', 'Chicago', 23, 2019, 'Physics', 3.6),
	('Bob Brown', '2002-07-30', 'Houston', 22, 2020, 'Chemistry', 3.7);
