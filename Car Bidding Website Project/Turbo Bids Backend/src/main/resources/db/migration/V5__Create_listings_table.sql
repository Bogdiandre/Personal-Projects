CREATE TABLE listings (
                          id BIGINT AUTO_INCREMENT PRIMARY KEY,
                          request_id BIGINT NOT NULL,
                          buyer_id BIGINT,
                          status VARCHAR(255) NOT NULL,
                          FOREIGN KEY (request_id) REFERENCES requests(id),
                          FOREIGN KEY (buyer_id) REFERENCES users(id)
);
