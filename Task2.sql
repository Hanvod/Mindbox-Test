CREATE TABLE Categories (
	Id INT IDENTITY NOT NULL PRIMARY KEY,
	Name NVARCHAR(255) NOT NULL
);

CREATE TABLE Products (
	Id INT IDENTITY NOT NULL PRIMARY KEY,
	Name NVARCHAR(255) NOT NULL,
);

CREATE TABLE CategoriesForProducts (
	ProductId INT NOT NULL FOREIGN KEY REFERENCES Products,
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories,
	PRIMARY KEY (ProductId, CategoryId)
);

INSERT INTO Categories (Name) VALUES (N'Для дома'), (N'Электроприборы'), (N'Мебель'), (N'Посуда');

INSERT INTO Products (Name) VALUES (N'Лампа');
INSERT INTO CategoriesForProducts (ProductId, CategoryId) VALUES (1, 1), (1, 2), (1, 3);

INSERT INTO Products (Name) VALUES (N'Шкаф');
INSERT INTO CategoriesForProducts (ProductId, CategoryId) VALUES (2, 1), (2, 3);

INSERT INTO Products (Name) VALUES (N'Походная тарелка');
INSERT INTO CategoriesForProducts (ProductId, CategoryId) VALUES (3, 4);

INSERT INTO Products (Name) VALUES (N'Палатка');

SELECT 
	Products.Name AS Product,
	Categories.Name AS Category
FROM
	Products
	LEFT JOIN 
	CategoriesForProducts 
	ON Products.Id = CategoriesForProducts.ProductId
	LEFT JOIN Categories 
	ON CategoriesForProducts.CategoryId = Categories.Id;