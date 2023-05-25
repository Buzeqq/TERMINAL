CREATE DATABASE `TERMINAL`;

CREATE TABLE `Projekt` (
  `ID` ID PRIMARY KEY,
  `nazwa` string,
  `is_active` bool
);

CREATE TABLE `Osoba` (
  `ID` ID PRIMARY KEY,
  `nick` string,
  `Imie` string,
  `Nazwisko` string
);

CREATE TABLE `Tagi` (
  `tag` string PRIMARY KEY,
  `is_active` bool
);

CREATE TABLE `Etap` (
  `ID` ID PRIMARY KEY,
  `komentarz` string,
  `pomiar` ID
);

CREATE TABLE `Przepis` (
  `ID` ID PRIMARY KEY,
  `nazwa` string
);

CREATE TABLE `Parametr` (
  `nazwa` string PRIMARY KEY,
  `is_active` bool,
  `jednosta` string
);

CREATE TABLE `SlownikWartosci` (
  `ID` ID,
  `wartosc` string,
  `is_active` bool,
  `parametr` str,
  PRIMARY KEY (`ID`, `wartosc`)
);

CREATE TABLE `EtapPrzepisu` (
  `ID` ID PRIMARY KEY,
  `komentarz` string,
  `przepis` ID
);

CREATE TABLE `Pomiar` (
  `ID` ID PRIMARY KEY,
  `date` date,
  `komentarz` string,
  `osoba` ID,
  `przepis` ID
);

ALTER TABLE `Etap` ADD FOREIGN KEY (`pomiar`) REFERENCES `Pomiar` (`ID`);

CREATE TABLE `Etap_SlownikWartosci` (
  `Etap_ID` ID,
  `SlownikWartosci_ID` ID,
  PRIMARY KEY (`Etap_ID`, `SlownikWartosci_ID`)
);

ALTER TABLE `Etap_SlownikWartosci` ADD FOREIGN KEY (`Etap_ID`) REFERENCES `Etap` (`ID`);

ALTER TABLE `Etap_SlownikWartosci` ADD FOREIGN KEY (`SlownikWartosci_ID`) REFERENCES `SlownikWartosci` (`ID`);


ALTER TABLE `SlownikWartosci` ADD FOREIGN KEY (`parametr`) REFERENCES `Parametr` (`nazwa`);

ALTER TABLE `EtapPrzepisu` ADD FOREIGN KEY (`przepis`) REFERENCES `Przepis` (`ID`);

CREATE TABLE `EtapPrzepisu_SlownikWartosci` (
  `EtapPrzepisu_ID` ID,
  `SlownikWartosci_ID` ID,
  PRIMARY KEY (`EtapPrzepisu_ID`, `SlownikWartosci_ID`)
);

ALTER TABLE `EtapPrzepisu_SlownikWartosci` ADD FOREIGN KEY (`EtapPrzepisu_ID`) REFERENCES `EtapPrzepisu` (`ID`);

ALTER TABLE `EtapPrzepisu_SlownikWartosci` ADD FOREIGN KEY (`SlownikWartosci_ID`) REFERENCES `SlownikWartosci` (`ID`);


ALTER TABLE `Pomiar` ADD FOREIGN KEY (`osoba`) REFERENCES `Osoba` (`ID`);

ALTER TABLE `Pomiar` ADD FOREIGN KEY (`przepis`) REFERENCES `Przepis` (`ID`);

CREATE TABLE `Pomiar_Projekt` (
  `Pomiar_ID` ID,
  `Projekt_ID` ID,
  PRIMARY KEY (`Pomiar_ID`, `Projekt_ID`)
);

ALTER TABLE `Pomiar_Projekt` ADD FOREIGN KEY (`Pomiar_ID`) REFERENCES `Pomiar` (`ID`);

ALTER TABLE `Pomiar_Projekt` ADD FOREIGN KEY (`Projekt_ID`) REFERENCES `Projekt` (`ID`);


CREATE TABLE `Pomiar_Tagi` (
  `Pomiar_ID` ID,
  `Tagi_tag` string,
  PRIMARY KEY (`Pomiar_ID`, `Tagi_tag`)
);

ALTER TABLE `Pomiar_Tagi` ADD FOREIGN KEY (`Pomiar_ID`) REFERENCES `Pomiar` (`ID`);

ALTER TABLE `Pomiar_Tagi` ADD FOREIGN KEY (`Tagi_tag`) REFERENCES `Tagi` (`tag`);


ALTER TABLE `Pomiar` ADD FOREIGN KEY (`date`) REFERENCES `Pomiar` (`osoba`);
