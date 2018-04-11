-- DO NOT run the whole file. Some of the material is just for reference ie. how to set isolation levels

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
