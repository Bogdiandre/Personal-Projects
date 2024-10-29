CREATE TABLE vehicles (
                          id BIGINT AUTO_INCREMENT PRIMARY KEY,
                          maker VARCHAR(255) NOT NULL,
                          model VARCHAR(255) NOT NULL UNIQUE,
                          type VARCHAR(255) NOT NULL,
                          UNIQUE (maker, model)
);
