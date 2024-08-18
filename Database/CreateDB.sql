------------------------------------------------------------------
-- Création Database
------------------------------------------------------------------
CREATE DATABASE Restaurant
GO
USE Restaurant
GO
------------------------------------------------------------------
-- Définiation des tables
------------------------------------------------------------------
CREATE TABLE Restaurant(
	IdRestaurant INT IDENTITY(1,1) NOT NULL,
	NomRestaurant VARCHAR(50) NOT NULL,
	LocRestaurant VARCHAR(255) NOT NULL
	PRIMARY KEY(IdRestaurant)
)
GO

CREATE TABLE Serveur(
	IdServeur INT IDENTITY(1,1) NOT NULL,
	Prenom VARCHAR(50) NOT NULL,
	Nom VARCHAR(50) NOT NULL,
	Telephone VARCHAR(12) NOT NULL UNIQUE,
	Email VARCHAR(50) NOT NULL UNIQUE,
	FkRestaurant INT NOT NULL
	PRIMARY KEY (IdServeur),
	FOREIGN KEY (FkRestaurant) REFERENCES Restaurant(IdRestaurant)
)
GO

CREATE TABLE Article(
	IdArticle INT IDENTITY(1,1) NOT NULL,
	TypeArticle VARCHAR(50) NOT NULL,
	NomArticle VARCHAR(50) NOT NULL,
	PrixArticle FLOAT,
	PRIMARY KEY (IdArticle)
)
GO

CREATE TABLE Restaurant_Article(
	IdRestaurant INT NOT NULL,
	IdArticle INT NOT NULL,
	PRIMARY KEY (IdRestaurant, IdArticle),
	FOREIGN KEY (IdRestaurant) REFERENCES Restaurant(IdRestaurant),
	FOREIGN KEY (IdArticle) REFERENCES Article(IdArticle)
)
GO

CREATE TABLE RestaurantTable (
	IdTable INT IDENTITY(1,1) NOT NULL,
	NumeroTable TINYINT,
	EnUtilisation TINYINT NOT NULL,
	FkRestaurant INT NOT NULL,
	PRIMARY KEY (IdTable),
	FOREIGN KEY (FkRestaurant) REFERENCES Restaurant(IdRestaurant)
)
GO

CREATE TABLE Commande (
	IdCommande INT IDENTITY(1,1) NOT NULL,
	CommandeDate DATETIME NOT NULL,
	ModePaiement VARCHAR(50),
	FkTable INT NOT NULL,
	PRIMARY KEY (IdCommande),
	FOREIGN KEY (FkTable) REFERENCES RestaurantTable(IdTable)
)
GO

CREATE TABLE Article_Commande(
	IdCommande INT NOT NULL,
	IdArticle INT NOT NULL,
	PRIMARY KEY (IdCommande, IdArticle),
	FOREIGN KEY (IdCommande) REFERENCES Commande(IdCommande),
	FOREIGN KEY (IdArticle) REFERENCES Article(IdArticle)
)
GO

------------------------------------------------------------------
-- Définiation des Stored Procedures
------------------------------------------------------------------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE USP_InsertRestaurant
	@NomRestaurant VARCHAR(50), 
	@RestaurantLoc VARCHAR(255)
AS
BEGIN
	INSERT INTO Restaurant(NomRestaurant, LocRestaurant) VALUES (@NomRestaurant, @RestaurantLoc)
END
GO

CREATE PROCEDURE USP_InsertServeur
	@Prenom VARCHAR(50),
	@Nom VARCHAR(50),
	@Tel VARCHAR(12),
	@Mail VARCHAR(50),
	@Restaurant INT
AS
BEGIN
	INSERT INTO Serveur(Prenom, Nom, Telephone, Email, FkRestaurant) VALUES (@Prenom, @Nom, @Tel, @Mail, @Restaurant)
END
GO

CREATE PROCEDURE USP_InsertArticle
	@Categorie VARCHAR(50),
	@Nom VARCHAR(50),
	@Prix FLOAT
AS
BEGIN
	INSERT INTO Article(TypeArticle, NomArticle, PrixArticle) VALUES (@Categorie, @Nom, @Prix)
END
GO

CREATE PROCEDURE USP_InsertTable
	@Numero VARCHAR(50),
	@EnUtilisation VARCHAR(50),
	@Restaurant INT
AS
BEGIN
	INSERT INTO RestaurantTable(NumeroTable, EnUtilisation, FkRestaurant) VALUES (@Numero, @EnUtilisation, @Restaurant)
END
GO

CREATE PROCEDURE USP_AddArticleInRestaurant
	@Restaurant INT,
	@Article INT
AS
BEGIN
	INSERT INTO Restaurant_Article(IdArticle, IdRestaurant) VALUES (@Article, @Restaurant)
END
GO

CREATE PROCEDURE USP_AddArticleToCommande
	@Commande INT,
	@Article INT
AS
BEGIN
	INSERT INTO Article_Commande(IdArticle, IdCommande) VALUES (@Article, @Commande)
END
GO

CREATE PROCEDURE USP_AddCommandeToTable
	@Table INT
AS
BEGIN
	INSERT INTO Commande(CommandeDate, FkTable) VALUES (GETDATE(), @Table)
END
GO

CREATE PROCEDURE USP_DeleteRestaurant
	@IdRestaurant INT
AS
BEGIN
	DELETE FROM Restaurant_Article WHERE IdRestaurant=@IdRestaurant

	DELETE Article_Commande FROM Article_Commande 
		INNER JOIN Commande ON Article_Commande.IdCommande = Commande.IdCommande
		INNER JOIN RestaurantTable ON Commande.FkTable = RestaurantTable.IdTable
	WHERE RestaurantTable.FkRestaurant = @IdRestaurant;
	
	DELETE Commande FROM Commande
		INNER JOIN RestaurantTable ON Commande.FkTable = RestaurantTable.IdTable
	WHERE RestaurantTable.FkRestaurant = @IdRestaurant;
	
	DELETE FROM RestaurantTable WHERE FkRestaurant = @IdRestaurant;

	DELETE FROM Restaurant WHERE IdRestaurant=@IdRestaurant;
END
GO

CREATE PROCEDURE USP_DeleteServeur
	@IdServeur INT
AS
BEGIN
	DELETE FROM Serveur WHERE IdServeur=@IdServeur
END
GO

CREATE PROCEDURE USP_UpdateRestaurant
	@IdRestaurant INT,
	@NomRestaurant VARCHAR, 
	@RestaurantLoc VARCHAR
AS
BEGIN
	UPDATE Restaurant SET NomRestaurant=@NomRestaurant, LocRestaurant=@RestaurantLoc WHERE IdRestaurant=@IdRestaurant
END
GO