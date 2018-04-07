-- Player table
-- two players
INSERT INTO Player (PlayerName) VALUES ("Hannu");
INSERT INTO Player (PlayerName) VALUES ("Antti");


-- Cash table
-- 30 x 500
INSERT INTO Cash (Value) VALUES (500),(500),(500),(500),(500);
INSERT INTO Cash (Value) VALUES (500),(500),(500),(500),(500);
INSERT INTO Cash (Value) VALUES (500),(500),(500),(500),(500);
INSERT INTO Cash (Value) VALUES (500),(500),(500),(500),(500);
INSERT INTO Cash (Value) VALUES (500),(500),(500),(500),(500);
INSERT INTO Cash (Value) VALUES (500),(500),(500),(500),(500);
-- 30x 100
INSERT INTO Cash (Value) VALUES (100),(100),(100),(100),(100);
INSERT INTO Cash (Value) VALUES (100),(100),(100),(100),(100);
INSERT INTO Cash (Value) VALUES (100),(100),(100),(100),(100);
INSERT INTO Cash (Value) VALUES (100),(100),(100),(100),(100);
INSERT INTO Cash (Value) VALUES (100),(100),(100),(100),(100);
INSERT INTO Cash (Value) VALUES (100),(100),(100),(100),(100);
-- 30x 50
INSERT INTO Cash (Value) VALUES (50),(50),(50),(50),(50);
INSERT INTO Cash (Value) VALUES (50),(50),(50),(50),(50);
INSERT INTO Cash (Value) VALUES (50),(50),(50),(50),(50);
INSERT INTO Cash (Value) VALUES (50),(50),(50),(50),(50);
INSERT INTO Cash (Value) VALUES (50),(50),(50),(50),(50);
INSERT INTO Cash (Value) VALUES (50),(50),(50),(50),(50);
-- total value of cash at this point: 19500


-- Building table
-- 40 buildings
INSERT INTO Building (Type, Price) VALUES ("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100);
INSERT INTO Building (Type, Price) VALUES ("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100);
INSERT INTO Building (Type, Price) VALUES ("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100);
INSERT INTO Building (Type, Price) VALUES ("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100);
INSERT INTO Building (Type, Price) VALUES ("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100);
INSERT INTO Building (Type, Price) VALUES ("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100);
INSERT INTO Building (Type, Price) VALUES ("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100);
INSERT INTO Building (Type, Price) VALUES ("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100),("Hostel", 100);
-- 10 hotels
INSERT INTO Building (Type, Price) VALUES ("Hotel", 200),("Hotel", 200),("Hotel", 200),("Hotel", 200),("Hotel", 200);
INSERT INTO Building (Type, Price) VALUES ("Hotel", 200),("Hotel", 200),("Hotel", 200),("Hotel", 200),("Hotel", 200);


-- CellType table
-- maksa tulovero, sahkolaitos, vesilaitos ja maksa lisavero  not implemented
INSERT INTO CellType (Name) values ("Start"),("Properties"),("Community"),("Transportation"),("Chance"),("Prison"),("Parking"),("Sentence");


-- Cell table
-- 1-10 
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Start",0,0,0,1);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Korkeavuorenkatu",60,10,1,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Community Chest",0,0,0,3);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Kasarmikatu",60,10,1,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Pasilan asema",200,20,2,4);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Rantatie",100,15,3,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Chance",0,0,4,5);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Kauppatori",100,15,3,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Esplanadi",120,20,3,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Prison",0,0,0,6);
-- 11-19
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Hameentie",140,25,4,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Siltasaari",140,25,4,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Kaisaniemenkatu",160,30,4,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Sornaisten asema",200,20,2,4);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Liisankatu",180,35,5,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Community Chest",0,0,0,3);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Snellmaninkatu",180,35,5,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Unioninkatu",200,40,5,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Free parking",0,0,0,6);
-- 20-28
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Lonnrotinkatu",220,45,6,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Chance",0,0,4,5);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Annankatu",220,45,6,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Simonkatu",240,50,6,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Rautatieasema",200,20,2,4);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Mikonkatu",260,55,7,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Aleksanterinkatu",260,55,7,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Keskuskatu",280,60,7,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Go to Jail",0,0,0,6);
-- 29-38
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Tehtaankatu",300,65,8,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Eira",300,65,8,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Community Chest",0,0,0,3);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Bulevardi",320,70,8,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Tavara-asema",200,20,2,4);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Mannerheimintie",350,75,9,2);
INSERT INTO Cell (Name,Price,Rent,SerieId,CellTypeId) VALUES ("Erottaja",400,85,9,2);


-- Initializing the first game
-- gamesession
INSERT INTO GameSession (WinnerId, WinnerCash, WinnerCell) VALUES (null, null, null);

-- players to the gamesession. CellId is players' current position
INSERT INTO GameSession_has_player (PlayerId,GameSessionId,CellId) VALUES (1,1,1),(2,1,1);

-- cash to players
INSERT INTO Player_has_Cash (PlayerId,CashId,GameSessionId) VALUES (1,1,1),(2,2,1);

-- tokens
INSERT INTO Token (Name) VALUES ("Battleship"),("Cannon"),("Cat"),("Iron"),("Penguin"),("Racecar");

-- tokens to players
INSERT INTO Player_has_Token (PlayerId,TokenId,GameSessionId) VALUES (1,1,1),(2,2,1);