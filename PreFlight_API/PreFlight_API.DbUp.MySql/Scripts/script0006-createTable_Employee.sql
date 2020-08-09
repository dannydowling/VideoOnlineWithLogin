CREATE TABLE IF NOT EXISTS `Employee` (
  `id` varchar(36) NOT NULL,
  `Email` varchar(45) NOT NULL, 
  `FirstName` varchar(45) NOT NULL,
  `LastName` varchar(45) NOT NULL,
  `BirthDate` datetime NOT NULL,
  `JobCategoryId` tinyint(1) NOT NULL,
  `PhoneNumber` varchar(45) NOT NULL,
  `employeeLocations` collection NOT NULL,
  `employeeWeatherList` collection NOT NULL,
  `JoinedDate` datetime NOT NULL,
  `ExitDate` datetime,
  `Password` password NOT NULL,
  `RowVersion` int(36) NOT NULL

  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
