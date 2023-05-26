CREATE TABLE Osoba (
	ID INT AUTO_INCREMENT PRIMARY KEY,
	nick VARCHAR(20),
	Imie VARCHAR(20) NOT NULL,
	Nazwisko VARCHAR(20) NOT NULL
);

CREATE TABLE Przepis (
	ID INT AUTO_INCREMENT PRIMARY KEY,
	nazwa VARCHAR(30) NOT NULL
);

CREATE TABLE Pomiar (
	ID INT AUTO_INCREMENT PRIMARY KEY,
	dataPomiaru DATE NOT NULL,
	komentarz VARCHAR(255),
	osobaID INT NOT NULL,
	przepisID INT,
	FOREIGN KEY (osobaID) REFERENCES Osoba(ID),
	FOREIGN KEY (przepisID) REFERENCES Przepis(ID)
);

CREATE TABLE Tag (
	nazwa VARCHAR(30) PRIMARY KEY,
	is_active BIT(1) DEFAULT 0
);

/* tabela asocjacyjna pomiedzy Pomiarami a Tagami */
CREATE TABLE TagiPomiaru (
	pomiarID INT NOT NULL,
	tag VARCHAR(30) NOT NULL,
	PRIMARY KEY (pomiarID, tag),
	FOREIGN KEY (pomiarID) REFERENCES Pomiar(ID),
	FOREIGN KEY (tag) REFERENCES Tag(nazwa)
);

CREATE TABLE Projekt (
	ID INT AUTO_INCREMENT PRIMARY KEY,
	nazwa VARCHAR(30) NOT NULL,
	is_active BIT(1) DEFAULT 0
);

/* tabela asocjacyjna pomiedzy Projektami a Pomiarami */
CREATE TABLE PomiaryProjektu (
	projektID INT NOT NULL,
	pomiarID INT NOT NULL,
	PRIMARY KEY (projektID, pomiarID),
	FOREIGN KEY (projektID) REFERENCES Projekt(ID),
	FOREIGN KEY (pomiarID) REFERENCES Pomiar(ID)
);

CREATE TABLE EtapPrzepisu (
	ID INT AUTO_INCREMENT PRIMARY KEY,
	przepisID INT NOT NULL,
	komentarz VARCHAR(255),
	FOREIGN KEY (przepisID) REFERENCES Przepis(ID)
);

CREATE TABLE Parametr (
	nazwa VARCHAR(20) PRIMARY KEY,
	is_active BIT(1) DEFAULT 0,
	jednostka VARCHAR(10) NOT NULL
);

CREATE TABLE Etap (
	ID INT AUTO_INCREMENT PRIMARY KEY,
	komentarz VARCHAR(255),
	pomiarID INT NOT NULL,
	FOREIGN KEY (pomiarID) REFERENCES Pomiar(ID)
);

CREATE TABLE SlownikWartosci (
	ID INT AUTO_INCREMENT PRIMARY KEY,
	wartosc VARCHAR(20) NOT NULL,
	is_active BIT(1),
	parametr VARCHAR(20),
	FOREIGN KEY (parametr) REFERENCES Parametr(nazwa)
);

/* tabela asocjacyjna pomiedzy Etapami a SlownikiemWartosci */
CREATE TABLE EtapySlownik(
	etapID INT NOT NULL,
	slownikID INT NOT NULL,
	PRIMARY KEY (etapID, slownikID),
	FOREIGN KEY (etapID) REFERENCES Etap(ID),
	FOREIGN KEY (slownikID) REFERENCES SlownikWartosci(ID)
);

/* tabela asocjacyjna pomiedzy EtapamiPrzepisu a SlownikiemWartosci */
CREATE TABLE EtapyPrzepisuSlownik (
	etapPrzepisuID INT NOT NULL,
	slownikID INT NOT NULL,
	PRIMARY KEY (etapPrzepisuID, slownikID),
	FOREIGN KEY (etapPrzepisuID) REFERENCES EtapPrzepisu(ID),
	FOREIGN KEY (slownikID) REFERENCES SlownikWartosci(ID)
);

/*
TODO:
1. dodac defaultowe wartosci, np do 'is_active' #to check
2. dodac NOT NULL tam gdzie trzeba #to check
3. relacja pomiedzy dat� a osob� w tabeli Pomiar?
*/