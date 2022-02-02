DROP DATABASE IF EXISTS Automate;
CREATE DATABASE Automate DEFAULT CHARACTER SET utf8;
Use Automate;
--
-- Table  'Afpa_Seuils'
--


CREATE TABLE Afpa_Seuils(
   IdSeuil INT AUTO_INCREMENT PRIMARY KEY,
   SeuilBas INT,
   SeuilHaut INT,
   DateSeuil DATE,
   Temps TIME
)ENGINE=InnoDB;


--
-- Table  'Afpa_Temperatures'
--


CREATE TABLE Afpa_Temperatures(
   IdTemperature INT AUTO_INCREMENT PRIMARY KEY,
   ValeurTemperature DECIMAL(3,1),
   DateTemperature DATETIME
)ENGINE=InnoDB;


--
-- Table  'Afpa_Sons'
--


CREATE TABLE Afpa_Sons(
   IdSon INT AUTO_INCREMENT PRIMARY KEY,
   ValeurSon INT,
   DateSon DATETIME
)ENGINE=InnoDB;


--
-- Table  'Afpa_Lumieres'
--


CREATE TABLE Afpa_Lumieres(
   IdLumiere INT AUTO_INCREMENT PRIMARY KEY,
   ValeurLumiere INT,
   DateLumiere DATETIME
)ENGINE=InnoDB;

--
-- Table  'Afpa_Couleurs'
--


CREATE TABLE Afpa_Couleurs(
   IdCouleur INT AUTO_INCREMENT PRIMARY KEY,
   Red INT,
   Green INT,
   Blue INT
)ENGINE=InnoDB;


--
-- Table  'Afpa_Erreurs'
--


CREATE TABLE Afpa_Erreurs(
   IdErreur INT AUTO_INCREMENT PRIMARY KEY,
   MessageErreur TEXT
 
)ENGINE=InnoDB;

--
-- Table  'Afpa_Objectifs'
--


CREATE TABLE Afpa_Objectifs(
   IdObjectif INT AUTO_INCREMENT PRIMARY KEY,
   Randement INT,
   MaxNombreArretTemperature INT,
   MaxNombreArretDecibel INT,
   MaxPourcentDeclasses INT
)ENGINE=InnoDB;

--
-- Table  'Afpa_Cadances'
--


CREATE TABLE Afpa_Cadances(
   IdCadance INT AUTO_INCREMENT PRIMARY KEY,
   NbProduit INT,
   DateCadance DATETIME
)ENGINE=InnoDB;


--
-- Table  'Afpa_Anomalies'
--


CREATE TABLE Afpa_Anomalies(
   IdAnomalie INT AUTO_INCREMENT PRIMARY KEY,
   DateAnomalie DATETIME,
   TypeAnomalie VARCHAR(50),
   NbDelaisses INT,
   IdErreur INT NOT NULL
  
)ENGINE=InnoDB;


ALTER TABLE Afpa_Anomalies 
ADD CONSTRAINT Afpa_Anomalies_Afpa_Erreurs FOREIGN KEY(IdErreur) REFERENCES Afpa_Erreurs(IdErreur);




INSERT INTO `afpa_couleurs` (`IdCouleur`, `Red`, `Green`, `Blue`) VALUES
(1, 198, 8, 0),
(2, 158, 253, 56),
(3, 254, 27, 0);

INSERT INTO `afpa_erreurs` (`IdErreur`, `MessageErreur`) VALUES
(1, 'Luminosité trop basse '),
(2, 'Son trop haut '),
(3, 'Luminosité trop faible'),
(4, 'Son trop bas'),
(5, 'Température trop élevé. '),
(6, 'Température trop basse. '),
(7, 'Son ne fonctionne pas '),
(8, 'Lumière ne fonctionne pas '),
(9, 'Température ne fonctionne pas '),
(10, 'Lumière saccadée '),
(11, 'Son grésillement '),
(12, 'Température Instable');

INSERT INTO `afpa_lumieres` (`IdLumiere`, `ValeurLumiere`, `DateLumiere`) VALUES
(1, 350, '2022-02-02 14:08:16'),
(2, 120, '2022-02-01 14:08:16');


INSERT INTO `afpa_seuils` (`IdSeuil`, `SeuilBas`, `SeuilHaut`, `DateSeuil`) VALUES
(1, 10, 30, '2022-02-01'),
(2, 40, 150, '2022-02-02'),
(3, 100, 1000, '2022-02-25');

INSERT INTO `afpa_sons` (`IdSon`, `ValeurSon`, `DateSon`) VALUES
(1, 120, '2022-02-01 13:58:44'),
(2, 100, '2022-02-02 13:58:44');

INSERT INTO `afpa_temperatures` (`IdTemperature`, `ValeurTemperature`, `DateTemperature`) VALUES
(1, '21.0', '2022-02-01 13:57:57'),
(2, '-3.0', '2022-02-01 14:57:57');


INSERT INTO `afpa_objectifs` (`IdObjectif`, `Randement`, `MaxNombreArretTemperature`,`MaxNombreArretDecibel`,`MaxPourcentDeclasses`) VALUES
(1, 100, 4,5,60 ),
(2, 200, 5,4,70);

INSERT INTO `afpa_cadances` (`IdCadance`, `NbProduit`, `DateCadance`) VALUES
(1, 100, '2022-02-01 14:20:30'),
(2, 150, '2022-02-01 14:22:30');


INSERT INTO `afpa_anomalies` (`IdAnomalie`, `DateAnomalie`, `TypeAnomalie`, `IdErreur`) VALUES
(1, '2022-02-01 14:20:30', 'Lumière ', 3),
(2, '2022-02-01 14:21:52', 'Son', 2);
