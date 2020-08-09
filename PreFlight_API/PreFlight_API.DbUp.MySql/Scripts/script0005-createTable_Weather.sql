CREATE TABLE IF NOT EXISTS `Weather` (
  `id` varchar(36) NOT NULL,
  `AirPressure` double NOT NULL, 
  `Temperature` double NOT NULL,
  `WeightValue` double NOT NULL,
  `RowVersion` datetime NOT NULL

  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
