INSERT INTO Player (PlayerName) values ("Player1");
INSERT INTO Player (PlayerName) values ("Player2");
INSERT INTO Cash (Value) values (100);
INSERT INTO Cash (Value) values (100);
INSERT INTO Building (Type, Price) values ("Hotel", 100);

INSERT INTO Attendance (GameId) values (1);
INSERT INTO GameSession (WinnerId, WinnerWealth, WinnerRealEstates, WinnerBuildings) values (null, null, null, null);
INSERT INTO GameSession (WinnerId, WinnerWealth, WinnerRealEstates, WinnerBuildings) values (null, null, null, null);
INSERT INTO GameSession (WinnerWealth, WinnerRealEstates, WinnerBuildings) values (null, null, null);

INSERT INTO Player_has_Cash (Player_PlayerId, Cash_CashId, Attendance_AttendanceId) values (1,1,1);
INSERT INTO Player_has_Cash (Player_PlayerId, Cash_CashId, Attendance_AttendanceId) values (2,1,1);
INSERT INTO Attendance (GameId) values (2);
INSERT INTO Player_has_Cash (Player_PlayerId, Cash_CashId, Attendance_AttendanceId) values (2,1,2);

 ALTER TABLE Player_has_Cash drop index Cash_CashId;