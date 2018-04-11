-- DO NOT RUN THE WHOLE FILE!
-- Some of the material is just for reference ie. how to set isolation levels.
-- Some is added to schema.sql

-- DDL
alter table Cell modify column Name varchar(256);
alter table Cell modify column CellId int auto_increment;

-- simple queries
-- number of games per player
SELECT PlayerName AS Player,COUNT(DISTINCT(GameSessionId)) AS Games
FROM Player 
INNER JOIN GameSession_has_player 
ON Player.PlayerId = GameSession_has_player.PlayerId
GROUP BY PlayerName
;


-- VIEW CREATION
-- query verification
SELECT GameSessionId,PlayerName,SUM(Value) as TotalCash 
FROM Player
INNER JOIN Player_has_Cash
ON Player.PlayerId = Player_has_Cash.PlayerId
INNER JOIN Cash
ON Player_has_Cash.CashId = Cash.CashId
GROUP BY GameSessionId,PlayerName
;

-- ADDED TO SCHEMA.SQL
-- creating the view
CREATE VIEW CashPerPlayerPerGame
AS
SELECT GameSessionId,PlayerName,SUM(Value) as TotalCash 
FROM Player
INNER JOIN Player_has_Cash
ON Player.PlayerId = Player_has_Cash.PlayerId
INNER JOIN Cash
ON Player_has_Cash.CashId = Cash.CashId
GROUP BY GameSessionId,PlayerName
;


-- using the view
SELECT * FROM CashPerPlayerPerGame;

-- ADDED TO SCHEMA.SQL
-- DELIMITER
-- prevents buying start, jail etc. common and shared cells
DELIMITER $$

CREATE TRIGGER unbyuable_cells 
  BEFORE INSERT ON Player_has_Cell
  FOR EACH ROW 
BEGIN
  IF (NEW.CellId IN (1,3,7,10,16,19,21,28,31,34)) THEN
    CALL `The cell is unbuyable`;
  END IF;
END$$

DELIMITER ;


-- TRANSACTION
-- view autocommit value
SELECT @@autocommit;

-- set autocommit
SET AUTOCOMMIT=0;
SET AUTOCOMMIT=on;

-- view isolation level
SELECT @@tx_isolation;
SET tx_isolation = 'READ-UNCOMMITTED';
SET tx_isolation = 'READ-COMMITTED';
SET tx_isolation = 'REPEATABLE-READ';
SET tx_isolation = 'SERIALIZABLE';
