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

-- card table
create table Card (CardId int primary key auto_increment, Effect varchar(64));
insert into Card (Name,Description) values ("Lose100","Your relative died and you have to spend 100$ to travel expenses."),("Lose200","You have mismanaged your taxation and have to pay 200$."),("Gain100","Stockmarkets have raised and you gain 100$."),("Gain200","You gain 200$ because of you are such a nice person.");
