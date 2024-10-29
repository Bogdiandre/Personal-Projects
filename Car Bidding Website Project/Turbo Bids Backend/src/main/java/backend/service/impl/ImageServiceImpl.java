package backend.service.impl;

import backend.persistance.ImageRepository;
import backend.persistance.RequestRepository;
import backend.persistance.entity.ImageEntity;
import backend.persistance.entity.RequestEntity;
import backend.service.ImageService;
import backend.service.exception.InvalidRequestIdException;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.UUID;

@Service
public class ImageServiceImpl implements ImageService {

    private String uploadDir = "src/main/resources/static/images";

    private final ImageRepository imageRepository;
    private final RequestRepository requestRepository;

    public ImageServiceImpl(ImageRepository imageRepository, RequestRepository requestRepository) {
        this.imageRepository = imageRepository;
        this.requestRepository = requestRepository;
    }

    @Override
    public String saveImage(MultipartFile image, Long requestId) throws IOException, InvalidRequestIdException {
        try {
            // Create a unique filename
            String filename = UUID.randomUUID().toString() + "_" + image.getOriginalFilename();
            // Create the file path
            Path filepath = Paths.get(uploadDir, filename);

            // Ensure the directory exists
            Files.createDirectories(filepath.getParent());

            // Save the file to the directory
            Files.copy(image.getInputStream(), filepath);

            // Find the associated request entity
            RequestEntity requestEntity = requestRepository.findById(requestId)
                    .orElseThrow(() -> new InvalidRequestIdException());

            // Create and save the image entity
            ImageEntity imageEntity = ImageEntity.builder()
                    .imageName(filename)
                    .requestEntity(requestEntity)
                    .build();

            imageRepository.save(imageEntity);

            return filename;
        } catch (NullPointerException e) {
            throw new InvalidRequestIdException();
        }
    }

    @Override
    public String getImageNameByRequestId(Long requestId) throws IOException {
        return imageRepository.findByRequestId(requestId)
                .map(ImageEntity::getImageName)
                .orElseThrow(() -> new IOException("Image not found for the given request ID"));
    }
}
