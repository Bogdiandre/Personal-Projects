CREATE TABLE requests (
                          id BIGINT AUTO_INCREMENT PRIMARY KEY,
                          vehicle_id BIGINT NOT NULL,
                          seller_id BIGINT NOT NULL,
                          milage INT NOT NULL,
                          details TEXT NOT NULL,
                          status VARCHAR(255) NOT NULL,
                          start TIMESTAMP NOT NULL,
                          end TIMESTAMP NOT NULL,
                          max_price INT NOT NULL,
                          FOREIGN KEY (vehicle_id) REFERENCES vehicles(id),
                          FOREIGN KEY (seller_id) REFERENCES users(id)
);
