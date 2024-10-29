CREATE TABLE users (
                       id BIGINT AUTO_INCREMENT PRIMARY KEY,
                       last_name VARCHAR(255) NOT NULL,
                       first_name VARCHAR(255) NOT NULL,
                       email VARCHAR(255) NOT NULL UNIQUE,
                       password VARCHAR(255) NOT NULL,
                       role VARCHAR(255) NOT NULL,
                       UNIQUE (role, email)
);
