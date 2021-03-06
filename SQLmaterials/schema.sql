-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema SCHEMA10
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema SCHEMA10
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `SCHEMA10` DEFAULT CHARACTER SET utf8 ;
USE `SCHEMA10` ;

-- -----------------------------------------------------
-- Table `SCHEMA10`.`Player`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SCHEMA10`.`Player` (
  `PlayerId` INT NOT NULL AUTO_INCREMENT,
  `PlayerName` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`PlayerId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SCHEMA10`.`GameSession`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SCHEMA10`.`GameSession` (
  `GameSessionId` INT NOT NULL AUTO_INCREMENT,
  `WinnerId` INT NULL,
  `WinnerCash` INT NULL,
  `WinnerCell` INT NULL,
  PRIMARY KEY (`GameSessionId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SCHEMA10`.`Cash`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SCHEMA10`.`Cash` (
  `CashId` INT NOT NULL AUTO_INCREMENT,
  `Value` INT NOT NULL,
  PRIMARY KEY (`CashId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SCHEMA10`.`CellType`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SCHEMA10`.`CellType` (
  `CellTypeId` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`CellTypeId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SCHEMA10`.`Cell`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SCHEMA10`.`Cell` (
  `CellId` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  `Rent` INT NULL,
  `Price` INT NULL,
  `SerieId` INT NULL,
  `CellTypeId` INT NOT NULL,
  PRIMARY KEY (`CellId`),
  INDEX `fk_Cell_CellType1_idx` (`CellTypeId` ASC),
  CONSTRAINT `fk_Cell_CellType1`
    FOREIGN KEY (`CellTypeId`)
    REFERENCES `SCHEMA10`.`CellType` (`CellTypeId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SCHEMA10`.`Building`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SCHEMA10`.`Building` (
  `BuildingId` INT NOT NULL AUTO_INCREMENT,
  `Type` VARCHAR(45) NOT NULL,
  `Price` INT NOT NULL,
  PRIMARY KEY (`BuildingId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SCHEMA10`.`Player_has_Cell`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SCHEMA10`.`Player_has_Cell` (
  `PlayerId` INT NOT NULL,
  `CellId` INT NOT NULL,
  `GameSessionId` INT NOT NULL,
  PRIMARY KEY (`PlayerId`, `CellId`, `GameSessionId`),
  INDEX `fk_Player_has_Cell_Cell1_idx` (`CellId` ASC),
  INDEX `fk_Player_has_Cell_Player1_idx` (`PlayerId` ASC),
  INDEX `fk_Player_has_Cell_Attendance1_idx` (`GameSessionId` ASC),
  UNIQUE INDEX `unique_game_cell` (`CellId` ASC, `GameSessionId` ASC),
  CONSTRAINT `fk_Player_has_Cell_Player1`
    FOREIGN KEY (`PlayerId`)
    REFERENCES `SCHEMA10`.`Player` (`PlayerId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Cell_Cell1`
    FOREIGN KEY (`CellId`)
    REFERENCES `SCHEMA10`.`Cell` (`CellId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Cell_Attendance1`
    FOREIGN KEY (`GameSessionId`)
    REFERENCES `SCHEMA10`.`GameSession` (`GameSessionId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SCHEMA10`.`GameSession_has_player`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SCHEMA10`.`GameSession_has_player` (
  `PlayerId` INT NOT NULL,
  `GameSessionId` INT NOT NULL,
  `CellId` INT NOT NULL DEFAULT 0 COMMENT 'Player position.',
  PRIMARY KEY (`PlayerId`, `GameSessionId`),
  INDEX `fk_Player_has_Attendance_Attendance1_idx` (`GameSessionId` ASC),
  INDEX `fk_Player_has_Attendance_Player1_idx` (`PlayerId` ASC),
  INDEX `fk_GameSession_has_player_Cell1_idx` (`CellId` ASC),
  CONSTRAINT `fk_Player_has_Attendance_Player1`
    FOREIGN KEY (`PlayerId`)
    REFERENCES `SCHEMA10`.`Player` (`PlayerId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Attendance_Attendance1`
    FOREIGN KEY (`GameSessionId`)
    REFERENCES `SCHEMA10`.`GameSession` (`GameSessionId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_GameSession_has_player_Cell1`
    FOREIGN KEY (`CellId`)
    REFERENCES `SCHEMA10`.`Cell` (`CellId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SCHEMA10`.`Player_has_Cash`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SCHEMA10`.`Player_has_Cash` (
  `PlayerId` INT NOT NULL,
  `CashId` INT NOT NULL,
  `GameSessionId` INT NOT NULL,
  PRIMARY KEY (`PlayerId`, `CashId`, `GameSessionId`),
  INDEX `fk_Player_has_Cash_Cash1_idx` (`CashId` ASC),
  INDEX `fk_Player_has_Cash_Player1_idx` (`PlayerId` ASC),
  INDEX `fk_Player_has_Cash_Attendance1_idx` (`GameSessionId` ASC),
  UNIQUE INDEX `unique_game_cash` (`CashId` ASC, `GameSessionId` ASC),
  CONSTRAINT `fk_Player_has_Cash_Player1`
    FOREIGN KEY (`PlayerId`)
    REFERENCES `SCHEMA10`.`Player` (`PlayerId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Cash_Cash1`
    FOREIGN KEY (`CashId`)
    REFERENCES `SCHEMA10`.`Cash` (`CashId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Cash_Attendance1`
    FOREIGN KEY (`GameSessionId`)
    REFERENCES `SCHEMA10`.`GameSession` (`GameSessionId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SCHEMA10`.`Player_has_Building`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SCHEMA10`.`Player_has_Building` (
  `PlayerId` INT NOT NULL,
  `BuildingId` INT NOT NULL,
  `GameSessionId` INT NOT NULL,
  PRIMARY KEY (`PlayerId`, `BuildingId`, `GameSessionId`),
  INDEX `fk_Player_has_Building_Building1_idx` (`BuildingId` ASC),
  INDEX `fk_Player_has_Building_Player1_idx` (`PlayerId` ASC),
  INDEX `fk_Player_has_Building_Attendance1_idx` (`GameSessionId` ASC),
  UNIQUE INDEX `unique_building_game` (`BuildingId` ASC, `GameSessionId` ASC),
  CONSTRAINT `fk_Player_has_Building_Player1`
    FOREIGN KEY (`PlayerId`)
    REFERENCES `SCHEMA10`.`Player` (`PlayerId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Building_Building1`
    FOREIGN KEY (`BuildingId`)
    REFERENCES `SCHEMA10`.`Building` (`BuildingId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Building_Attendance1`
    FOREIGN KEY (`GameSessionId`)
    REFERENCES `SCHEMA10`.`GameSession` (`GameSessionId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SCHEMA10`.`Token`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SCHEMA10`.`Token` (
  `TokenId` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`TokenId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SCHEMA10`.`Player_has_Token`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SCHEMA10`.`Player_has_Token` (
  `PlayerId` INT NOT NULL,
  `TokenId` INT NOT NULL,
  `GameSessionId` INT NOT NULL,
  PRIMARY KEY (`PlayerId`, `TokenId`, `GameSessionId`),
  INDEX `fk_Player_has_Token_Token1_idx` (`TokenId` ASC),
  INDEX `fk_Player_has_Token_Player1_idx` (`PlayerId` ASC),
  INDEX `fk_Player_has_Token_GameSession1_idx` (`GameSessionId` ASC),
  UNIQUE INDEX `unique_token_game` (`TokenId` ASC, `GameSessionId` ASC),
  CONSTRAINT `fk_Player_has_Token_Player1`
    FOREIGN KEY (`PlayerId`)
    REFERENCES `SCHEMA10`.`Player` (`PlayerId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Token_Token1`
    FOREIGN KEY (`TokenId`)
    REFERENCES `SCHEMA10`.`Token` (`TokenId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Player_has_Token_GameSession1`
    FOREIGN KEY (`GameSessionId`)
    REFERENCES `SCHEMA10`.`GameSession` (`GameSessionId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;


-- MANUAL CLAUSES, NOT FORWARD ENGINEERED
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

-- DELIMITER
-- prevents buying start (CellId1), jail etc. common and shared cells
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

-- same for update
DELIMITER $$

CREATE TRIGGER unbyuable_cells 
  BEFORE UPDATE ON Player_has_Cell
  FOR EACH ROW 
BEGIN
  IF (NEW.CellId IN (1,3,7,10,16,19,21,28,31,34)) THEN
    CALL `The cell is unbuyable`;
  END IF;
END$$

DELIMITER ;


-- isolation level to serializable
SET tx_isolation = 'SERIALIZABLE';
