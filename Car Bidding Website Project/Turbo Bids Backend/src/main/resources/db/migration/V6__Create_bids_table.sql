CREATE TABLE bids (
                      id BIGINT AUTO_INCREMENT PRIMARY KEY,
                      account_id BIGINT NOT NULL,
                      listing_id BIGINT NOT NULL,
                      amount DOUBLE NOT NULL,
                      FOREIGN KEY (account_id) REFERENCES users(id),
                      FOREIGN KEY (listing_id) REFERENCES listings(id)
);
