

CREATE TABLE PostTypes
(
 PostType     int NOT NULL ,
 PostCategory varchar(20) NOT NULL ,


 CONSTRAINT PK_posttypes PRIMARY KEY CLUSTERED (PostType ASC)
);
GO
--------------------------------------------------
CREATE TABLE RarityTypes
(
 RarityId       int NOT NULL ,
 RarityCategory varchar(20) NOT NULL ,


 CONSTRAINT PK_raritytypes PRIMARY KEY CLUSTERED (RarityId ASC)
);
GO
--------------------------------------------------

CREATE TABLE PokemonCards
(
 PokemonId       int NOT NULL ,
 RarityId        int NOT NULL ,
 SpriteLink      varchar(125) NOT NULL ,
 SpriteLinkShiny varchar(125) NOT NULL ,
 PokemonName     varchar(20) NOT NULL ,


 CONSTRAINT PK_table_70 PRIMARY KEY CLUSTERED (PokemonId ASC),
 CONSTRAINT FK_76 FOREIGN KEY (RarityId)  REFERENCES RarityTypes(RarityId)
);
GO


CREATE NONCLUSTERED INDEX fkIdx_77 ON PokemonCards 
 (
  RarityId ASC
 )

GO
--------------------------------------------------
CREATE TABLE Posts
(
 PostId          int NOT NULL IDENTITY(1, 1),
 PokemonId       int NULL ,
 PostTime        datetime NOT NULL ,
 PostDescription varchar(200) NULL ,
 Price           int NULL ,
 StillAvailable  bit NOT NULL ,
 IsShiny  	 bit NULL ,


 CONSTRAINT PK_posts PRIMARY KEY CLUSTERED (PostId ASC),
 CONSTRAINT FK_113 FOREIGN KEY (PokemonId)  REFERENCES PokemonCards(PokemonId)
);
GO


CREATE NONCLUSTERED INDEX fkIdx_114 ON Posts 
 (
  PokemonId ASC
 )

GO
--------------------------------------------------

CREATE TABLE Users
(
 UserId           int NOT NULL IDENTITY(1, 1),
 FirstName        varchar(50) NOT NULL ,
 LastName         varchar(50) NOT NULL ,
 UserName         varchar(50) NOT NULL ,
 Password         varchar(50) NOT NULL ,
 Email            varchar(50) NOT NULL ,
 CoinBalance      int NOT NULL ,
 AccountLevel     int NOT NULL ,
 TotalCoinsEarned int NOT NULL ,


  CONSTRAINT PK_customers PRIMARY KEY CLUSTERED (UserId ASC),
  CONSTRAINT unique_username UNIQUE(UserName),
  CONSTRAINT unique_email UNIQUE(Email),
);
GO
--------------------------------------------------
CREATE TABLE CardCollection
(
 PokemonId      int NOT NULL ,
 UserId         int NOT NULL ,
 QuantityNormal int NOT NULL ,
 QuantityShiny  int NOT NULL ,


 CONSTRAINT PK_orders PRIMARY KEY CLUSTERED (PokemonId ASC, UserId ASC),
 CONSTRAINT FK_89 FOREIGN KEY (PokemonId)  REFERENCES PokemonCards(PokemonId),
 CONSTRAINT FK_92 FOREIGN KEY (UserId)  REFERENCES Users(UserId)
);
GO


CREATE NONCLUSTERED INDEX fkIdx_90 ON CardCollection 
 (
  PokemonId ASC
 )

GO

CREATE NONCLUSTERED INDEX fkIdx_93 ON CardCollection 
 (
  UserId ASC
 )

GO

----------------------------------------------------------------------------------

CREATE TABLE DisplayBoard
(
 PostId   int NOT NULL ,
 UserId   int NOT NULL ,
 PostType int NOT NULL ,


 CONSTRAINT PK_displayboard PRIMARY KEY CLUSTERED (PostId ASC),
 CONSTRAINT FK_101 FOREIGN KEY (UserId)  REFERENCES Users(UserId),
 CONSTRAINT FK_108 FOREIGN KEY (PostId)  REFERENCES Posts(PostId),
 CONSTRAINT FK_98 FOREIGN KEY (PostType)  REFERENCES PostTypes(PostType)
);
GO


CREATE NONCLUSTERED INDEX fkIdx_102 ON DisplayBoard 
 (
  UserId ASC
 )

GO

CREATE NONCLUSTERED INDEX fkIdx_109 ON DisplayBoard 
 (
  PostId ASC
 )

GO

CREATE NONCLUSTERED INDEX fkIdx_99 ON DisplayBoard 
 (
  PostType ASC
 )

GO

