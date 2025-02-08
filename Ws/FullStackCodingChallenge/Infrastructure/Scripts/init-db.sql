
CREATE DATABASE FullStackDB;
GO

USE FullStackDB;
GO

CREATE TABLE Items (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NOT NULL
);
GO


CREATE PROCEDURE GetAllItems
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Description
    FROM Items;
END;





CREATE PROCEDURE GetItemById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Description
    FROM Items
    WHERE Id = @Id;
END;





CREATE PROCEDURE AddItem
    @Name NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @InsertedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Items (Name, Description)
    VALUES (@Name, @Description);

    SET @InsertedId = SCOPE_IDENTITY();
END;






CREATE PROCEDURE UpdateItem
    @Id INT,
    @Name NVARCHAR(255),
    @Description NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Items
    SET Name = @Name, Description = @Description
    WHERE Id = @Id;
END;

CREATE PROCEDURE DeleteItem
	@Id INT
AS
BEGIN
	DELETE FROM Items
	WHERE Id = @Id;
END

