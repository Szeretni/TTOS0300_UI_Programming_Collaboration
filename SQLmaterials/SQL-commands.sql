-- Query for player cash in every gamesession
Select PlayerName, Cash.Value, GameSessionId FROM Player 
INNER JOIN Player_has_Cash ON Player_has_Cash.PlayerId = Player.PlayerId 
INNER JOIN Cash ON Cash.CashId = Player_has_Cash.CashId;

-- Query for total player cash in every gamesession
SELECT PlayerName, GameSessionId, sum(Value) AS Cash FROM Player 
INNER JOIN Player_has_Cash ON Player_has_Cash.PlayerId = Player.PlayerId 
INNER JOIN Cash ON Cash.CashId = Player_has_Cash.CashId 
GROUP BY PlayerName;
