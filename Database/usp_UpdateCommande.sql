USE [Restaurant1]
GO
/****** Object:  StoredProcedure [dbo].[USP_UpdateCommande]    Script Date: 17-05-24 23:11:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[USP_UpdateCommande]
    @CommandeId INTEGER,
    @CommandeDateTime INTEGER,
    @FkTable1 INTEGER
AS
BEGIN
    UPDATE Commande
    SET CommandeDateTime = @CommandeDateTime,
        FkTable1 = @FkTable1
    WHERE IdCommande = @CommandeId;
END
