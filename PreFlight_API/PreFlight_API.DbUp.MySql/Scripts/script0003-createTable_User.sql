CREATE TABLE IF NOT EXISTS `Users` (
  `id` varchar(36) NOT NULL,
  `FirstName` varchar(45) NOT NULL, 
  `LastName` varchar(45) NOT NULL, 
  `Email` varchar(45) NOT NULL, 
  `Comment` varchar(1000) NOT NULL, 
  `RowVersion` datetime(45) NOT NULL, 
  `JoinedDate` datetime(45) NOT NULL,
  `ExitDate` datetime,
  `Weathers` collection NOT NULL,
  `Password` password NOT NULL
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
