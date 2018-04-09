-- Query for player cash in every gamesession
Select PlayerName, Cash.Value, GameSessionId FROM Player 
INNER JOIN Player_has_Cash ON Player_has_Cash.PlayerId = Player.PlayerId 
INNER JOIN Cash ON Cash.CashId = Player_has_Cash.CashId;
