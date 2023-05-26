CREATE TABLE Osoba (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	nick VARCHAR(20),
	Imie VARCHAR(20) NOT NULL,
	Nazwisko VARCHAR(20) NOT NULL,
);

CREATE TABLE Przepis (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	nazwa VARCHAR(30) NOT NULL
);

CREATE TABLE Pomiar (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	dataPomiaru DATE NOT NULL,
	komentarz VARCHAR(255),
	osobaID INT FOREIGN KEY REFERENCES Osoba(ID) NOT NULL,
	przepisID INT FOREIGN KEY REFERENCES Przepis(ID)
);

CREATE TABLE Tag (
	nazwa VARCHAR(30) PRIMARY KEY,
	is_active BIT DEFAULT 0
);

/* tabela asocjacyjna pomiedzy Pomiarami a Tagami */
CREATE TABLE TagiPomiaru (
	pomiarID INT FOREIGN KEY REFERENCES Pomiar(ID) NOT NULL,
	tag VARCHAR(30) FOREIGN KEY REFERENCES Tag(nazwa) NOT NULL,
	PRIMARY KEY (pomiarID, tag)
);

CREATE TABLE Projekt (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	nazwa VARCHAR(30) NOT NULL,
	is_active BIT DEFAULT 0
);

/* tabela asocjacyjna pomiedzy Projektami a Pomiarami */
CREATE TABLE PomiaryProjektu (
	projektID INT FOREIGN KEY REFERENCES Projekt(ID) NOT NULL,
	pomiarID INT FOREIGN KEY REFERENCES Pomiar(ID) NOT NULL,
	PRIMARY KEY (projektID, pomiarID)
);

CREATE TABLE EtapPrzepisu (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	przepisID INT FOREIGN KEY REFERENCES Przepis(ID) NOT NULL,
	komentarz VARCHAR(255)
);

CREATE TABLE Parametr (
	nazwa VARCHAR(20) PRIMARY KEY,
	is_active BIT DEFAULT 0,
	jednostka VARCHAR(10) NOT NULL
);

CREATE TABLE Etap (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	komentarz VARCHAR(255),
	pomiarID INT FOREIGN KEY REFERENCES Pomiar(ID) NOT NULL
);

CREATE TABLE SlownikWartosci (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	wartosc VARCHAR(20) NOT NULL, 
	is_active BIT,
	parametr VARCHAR(20) FOREIGN KEY REFERENCES Parametr(nazwa) NOT NULL
);

/* tabela asocjacyjna pomiedzy Etapami a SlownikiemWartosci */
CREATE TABLE EtapySlownik(
	etapID INT FOREIGN KEY REFERENCES Etap(ID) NOT NULL,
	slownikID INT FOREIGN KEY REFERENCES SlownikWartosci(ID) NOT NULL,
	PRIMARY KEY (etapID, slownikID)
);

/* tabela asocjacyjna pomiedzy EtapamiPrzepisu a SlownikiemWartosci */
CREATE TABLE EtapyPrzepisuSlownik (
	etapPrzepisuID INT FOREIGN KEY REFERENCES EtapPrzepisu(ID) NOT NULL,
	slownikID INT FOREIGN KEY REFERENCES SlownikWartosci(ID) NOT NULL,
	PRIMARY KEY (etapPrzepisuID, slownikID)
);

/*
TODO:
1. dodac defaultowe wartosci, np do 'is_active' #to check
2. dodac NOT NULL tam gdzie trzeba #to check
3. relacja pomiedzy dat� a osob� w tabeli Pomiar?
*/