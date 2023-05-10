-- This SQL script is used to create a table named 'pdfs'

CREATE TABLE `pdfs` (
    -- Column 'id' is of type 'int', is not nullable, and auto increments
                        `id` int(11) NOT NULL AUTO_INCREMENT,

    -- Column 'text' is of type 'longtext' and is nullable
                        `text` longtext DEFAULT NULL,

    -- Column 'filepath' is of type 'varchar' and is nullable with a maximum length of 255
                        `filepath` varchar(255) DEFAULT NULL,

    -- The primary key of the table is 'id'
                        PRIMARY KEY (`id`)

-- The storage engine for the table is InnoDB
) ENGINE=InnoDB 

-- The auto increment counter for the table starts from 0
AUTO_INCREMENT=0 

-- The character set for the table is utf8mb4
DEFAULT CHARSET=utf8mb4 

-- The collation for the table is utf8mb4_general_ci
COLLATE=utf8mb4_general_ci;
