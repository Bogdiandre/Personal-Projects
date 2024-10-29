package backend.service;

import backend.service.exception.InvalidRequestIdException;

import java.io.IOException;

public interface ImageService {
    String saveImage(org.springframework.web.multipart.MultipartFile image, Long requestId) throws IOException, InvalidRequestIdException;
    String getImageNameByRequestId(Long requestId) throws IOException;
}
