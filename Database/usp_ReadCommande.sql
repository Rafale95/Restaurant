USE [Restaurant1]
GO
/****** Object:  StoredProcedure [dbo].[USP_ReadCommande]    Script Date: 17-05-24 23:11:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[USP_ReadCommande]
    @CommandeId INTEGER
AS
BEGIN
    SELECT * FROM Commande
    WHERE IdCommande = @CommandeId;
END
