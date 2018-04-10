-- BEFORE ON UPDATE ON DELETE MOD
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema L2912_2
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema L2912_2
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `L2912_2` DEFAULT CHARACTER SET utf8 ;
USE `L2912_2` ;

-- -----------------------------------------------------
-- Table `L2912_2`.`Player`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `L2912_2`.`Player` (
  `PlayerId` INT NOT NULL AUTO_INCREMENT,
  `PlayerName` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`PlayerId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `L2912_2`.`GameSession`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `L2912_2`.`GameSession` (
  `GameSessionId` INT NOT NULL AUTO_INCREMENT,
  `WinnerId` INT NULL,
  `WinnerCash` INT NULL,
  `WinnerCell` INT NULL,
  PRIMARY KEY (`GameSessionId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `L2912_2`.`Cash`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `L2912_2`.`Cash` (
  `CashId` INT NOT NULL AUTO_INCREMENT,
  `Value` INT NOT NULL,
  PRIMARY KEY (`CashId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `L2912_2`.`CellType`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `L2912_2`.`CellType` (
  `CellTypeId` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`CellTypeId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `L2912_2`.`Cell`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `L2912_2`.`Cell` (
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
    REFERENCES `L2912_2`.`CellType` (`CellTypeId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `L2912_2`.`Building`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `L2912_2`.`Building` (
  `BuildingId` INT NOT NULL AUTO_INCREMENT,
  `Type` VARCHAR(45) NOT NULL,
  `Price` INT NOT NULL,
  PRIMARY KEY (`BuildingId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `L2912_2`.`Player_has_Cell`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `L2912_2`.`Player_has_Cell` (
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
    REFERENCES `L2912_2`.`Player` (`PlayerId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Player_has_Cell_Cell1`
    FOREIGN KEY (`CellId`)
    REFERENCES `L2912_2`.`Cell` (`CellId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Player_has_Cell_Attendance1`
    FOREIGN KEY (`GameSessionId`)
    REFERENCES `L2912_2`.`GameSession` (`GameSessionId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `L2912_2`.`GameSession_has_player`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `L2912_2`.`GameSession_has_player` (
  `PlayerId` INT NOT NULL,
  `GameSessionId` INT NOT NULL,
  `CellId` INT NOT NULL DEFAULT 0 COMMENT 'Player position.',
  PRIMARY KEY (`PlayerId`, `GameSessionId`),
  INDEX `fk_Player_has_Attendance_Attendance1_idx` (`GameSessionId` ASC),
  INDEX `fk_Player_has_Attendance_Player1_idx` (`PlayerId` ASC),
  INDEX `fk_GameSession_has_player_Cell1_idx` (`CellId` ASC),
  CONSTRAINT `fk_Player_has_Attendance_Player1`
    FOREIGN KEY (`PlayerId`)
    REFERENCES `L2912_2`.`Player` (`PlayerId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Player_has_Attendance_Attendance1`
    FOREIGN KEY (`GameSessionId`)
    REFERENCES `L2912_2`.`GameSession` (`GameSessionId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_GameSession_has_player_Cell1`
    FOREIGN KEY (`CellId`)
    REFERENCES `L2912_2`.`Cell` (`CellId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `L2912_2`.`Player_has_Cash`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `L2912_2`.`Player_has_Cash` (
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
    REFERENCES `L2912_2`.`Player` (`PlayerId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Player_has_Cash_Cash1`
    FOREIGN KEY (`CashId`)
    REFERENCES `L2912_2`.`Cash` (`CashId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Player_has_Cash_Attendance1`
    FOREIGN KEY (`GameSessionId`)
    REFERENCES `L2912_2`.`GameSession` (`GameSessionId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `L2912_2`.`Player_has_Building`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `L2912_2`.`Player_has_Building` (
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
    REFERENCES `L2912_2`.`Player` (`PlayerId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Player_has_Building_Building1`
    FOREIGN KEY (`BuildingId`)
    REFERENCES `L2912_2`.`Building` (`BuildingId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Player_has_Building_Attendance1`
    FOREIGN KEY (`GameSessionId`)
    REFERENCES `L2912_2`.`GameSession` (`GameSessionId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `L2912_2`.`Token`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `L2912_2`.`Token` (
  `TokenId` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`TokenId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `L2912_2`.`Player_has_Token`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `L2912_2`.`Player_has_Token` (
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
    REFERENCES `L2912_2`.`Player` (`PlayerId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Player_has_Token_Token1`
    FOREIGN KEY (`TokenId`)
    REFERENCES `L2912_2`.`Token` (`TokenId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Player_has_Token_GameSession1`
    FOREIGN KEY (`GameSessionId`)
    REFERENCES `L2912_2`.`GameSession` (`GameSessionId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
