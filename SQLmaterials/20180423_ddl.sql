-- die rolled flag to game's players
set sql_safe_updates = 0;
alter table GameSession_has_player add column (DieRolled bool);
update GameSession_has_player set DieRolled = False where DieRolled is null;
set sql_safe_updates = 1;

-- tracks current player in game
alter table GameSession add column (CurrentPlayerId int);

-- cash property, doens't use Cash-table
alter table GameSession_has_player add column PlayerCash int;
update GameSession_has_player set PlayerCash = 1500 where PlayerId in (1,2,3);
update GameSession_has_player set PlayerCash = 1000 where PlayerId in (1);
