USE LabAssignment2;

INSERT INTO Users(UserName,Email,Password) VALUES
('admin','admin@lab.com','admin123'),
('user1','user1@lab.com','pass123'),
('user2','user2@lab.com','pass123');

INSERT INTO Item(ItemName,Size,Price) VALUES
(N'Áo thun cổ tròn','S',80000),(N'Áo thun cổ tròn','M',85000),
(N'Áo thun cổ tròn','L',90000),(N'Quần jean skinny','29',250000),
(N'Quần jean skinny','30',260000),(N'Áo khoác bomber','M',450000),
(N'Áo khoác bomber','L',470000),(N'Giày sneaker trắng','41',500000),
(N'Giày sneaker trắng','42',510000),(N'Mũ lưỡi trai','Free',120000),
(N'Túi tote canvas','One',95000),(N'Vớ cotton','Free',25000),
(N'Đầm floral','S',320000),(N'Đầm floral','M',330000),
(N'Áo polo','L',175000);

INSERT INTO Agent(AgentName,Address) VALUES
(N'Đại lý Nguyễn Văn A',N'123 Lê Lợi, Q1, TP.HCM'),
(N'Đại lý Trần Thị B',N'45 Nguyễn Huệ, Q1, TP.HCM'),
(N'Đại lý Phạm Quốc C',N'67 Điện Biên Phủ, Bình Thạnh'),
(N'Đại lý Lê Hoàng D',N'89 Võ Văn Tần, Q3, TP.HCM'),
(N'Đại lý Hồ Minh E',N'11 Trần Hưng Đạo, Q5, TP.HCM');

INSERT INTO [Order](OrderDate,AgentID) VALUES
('2025-01-10',1),('2025-01-15',2),('2025-02-01',3),
('2025-02-10',1),('2025-03-05',4);

INSERT INTO OrderDetail(OrderID,ItemID,Quantity,UnitAmount) VALUES
(1,1,10,80000),(1,4,5,250000),(1,10,20,120000),
(2,2,8,85000),(2,6,3,450000),
(3,3,15,90000),(3,8,4,500000),(3,12,50,25000),
(4,5,12,260000),(4,11,10,95000),
(5,7,6,470000),(5,13,8,320000),(5,15,10,175000);