CREATE PROCEDURE [dbo].[spInventory_GetAll]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [ProductId], [Quantity], [PurchasePrice], [PurchasedDate] FROM dbo.Inventory;
END
