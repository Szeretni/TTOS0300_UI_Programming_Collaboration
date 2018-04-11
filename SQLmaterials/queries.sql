-- view creation
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
