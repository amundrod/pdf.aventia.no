CREATE TABLE `pdfs` (
                        `id` int(11) NOT NULL AUTO_INCREMENT,
                        `text` longtext DEFAULT NULL,
                        `filepath` varchar(255) DEFAULT NULL,
                        PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
