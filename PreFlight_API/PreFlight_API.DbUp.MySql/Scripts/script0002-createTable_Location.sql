CREATE TABLE IF NOT EXISTS `Location` (
  `id` varchar(36) NOT NULL,
  `Name` varchar(45) NOT NULL, 
  `Street` varchar(45) NOT NULL,
  `Zip` varchar(45) NOT NULL,
  `City` varchar(45) NOT NULL,
  `Country` tinyint(1) NOT NULL,

  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

