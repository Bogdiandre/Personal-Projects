# CREATE TABLE images (
#                         id BIGINT AUTO_INCREMENT PRIMARY KEY,
#                         image_name VARCHAR(255) NOT NULL,
#                         request_id BIGINT NOT NULL UNIQUE,
#                         FOREIGN KEY (request_id) REFERENCES requests(id)
# );
