USE [Restaurant1]
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertCommande]    Script Date: 17-05-24 23:11:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[USP_InsertCommande]
    @CommandeDateTime INTEGER,
    @FkTable1 INTEGER
AS
BEGIN
    INSERT INTO Commande (CommandeDateTime, FkTable1)
    VALUES (@CommandeDateTime, @FkTable1);
END
