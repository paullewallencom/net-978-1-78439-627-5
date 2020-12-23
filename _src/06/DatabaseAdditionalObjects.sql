-- Run this script after you create database by running the project
-- to create additional database artifacts
IF EXISTS(SELECT * FROM sys.views WHERE name = 'PersonView')
DROP VIEW PersonView
GO

CREATE VIEW [dbo].[PersonView]
AS
SELECT 
	dbo.People.PersonId, 
	dbo.People.FirstName, 
	dbo.People.LastName, 
	dbo.PersonTypes.TypeName
FROM     
	dbo.People 
INNER JOIN dbo.PersonTypes 
	ON dbo.People.PersonTypeId = dbo.PersonTypes.PersonTypeId

GO

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'UpdateCompanies')
DROP PROCEDURE UpdateCompanies
GO

CREATE PROCEDURE dbo.UpdateCompanies
	@dateAdded as DateTime,
	@activeFlag as Bit
AS
BEGIN
	UPDATE Companies
	Set DateAdded = @dateAdded,
		IsActive = @activeFlag
END

GO

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'SelectCompanies')
DROP PROCEDURE SelectCompanies
GO
CREATE PROCEDURE [dbo].[SelectCompanies]
	@dateAdded as DateTime
AS
BEGIN
	SET NOCOUNT ON
	SELECT CompanyId, CompanyName FROM Companies
	WHERE DateAdded > @dateAdded
END

GO

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'CompanyUpdate')
DROP PROCEDURE CompanyUpdate
GO
CREATE PROCEDURE [dbo].[CompanyUpdate]
	@companyId int,
	@companyName nvarchar(max),
	@dateAdded DateTime,
	@isActive Bit
AS
BEGIN
	Update Companies
	Set 
		CompanyName = @companyName,
		DateAdded = @dateAdded,
		IsActive = @isActive
	WHERE CompanyId = @companyId
END
GO

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'CompanyInsert')
DROP PROCEDURE CompanyInsert
GO
CREATE PROCEDURE [dbo].[CompanyInsert]
	@companyId int OUTPUT,
	@companyName nvarchar(max),
	@dateAdded DateTime,
	@isActive Bit
AS
BEGIN
	Insert Into Companies
	(CompanyName, DateAdded, IsActive) 
	Values (@companyName, @dateAdded, @isActive)
	Select @companyId = SCOPE_IDENTITY()
END
GO

IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'CompanyDelete')
DROP PROCEDURE CompanyDelete
GO
CREATE PROCEDURE [dbo].[CompanyDelete]
	@companyId int 
AS
BEGIN
	Delete From Companies
	WHERE CompanyId = @companyId
END
GO
