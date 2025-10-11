Use Lab2DB;
GO

CREATE TABLE tblUsers (
	UserID INT PRIMARY KEY IDENTITY(1,1),
	Username NVARCHAR(50) NOT NULL,
	Password NVARCHAR(255) NOT NULL,
);

INSERT INTO tblUsers(Username, Password) VALUES('user1', 'password1');
INSERT INTO tblUsers(Username, Password) VALUES('user2', 'password2');
