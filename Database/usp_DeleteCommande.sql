USE [Restaurant1]
GO
/****** Object:  StoredProcedure [dbo].[USP_DeleteCommande]    Script Date: 17-05-24 23:10:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[USP_DeleteCommande]
    @CommandeId INTEGER
AS
BEGIN
    DELETE FROM Commande
    WHERE IdCommande = @CommandeId;
END
