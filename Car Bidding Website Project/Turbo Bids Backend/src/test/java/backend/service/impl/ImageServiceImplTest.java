package backend.service.impl;

import backend.persistance.ImageRepository;
import backend.persistance.RequestRepository;
import backend.persistance.entity.ImageEntity;
import backend.persistance.entity.RequestEntity;
import backend.service.exception.InvalidRequestIdException;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.io.InputStream;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Optional;
import java.util.UUID;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.*;

class ImageServiceImplTest {

    @Mock
    private ImageRepository imageRepository;

    @Mock
    private RequestRepository requestRepository;

    @Mock
    private MultipartFile multipartFile;

    @InjectMocks
    private ImageServiceImpl imageService;

    private static final Long REQUEST_ID = 1L;
    private static final String IMAGE_NAME = "test_image.jpg";
    private static final String UPLOAD_DIR = "src/main/resources/static/images";

    @BeforeEach
    void setUp() {
        MockitoAnnotations.openMocks(this);
    }

    @Test
    void testSaveImageSuccess() throws IOException, InvalidRequestIdException {
        // Arrange
        RequestEntity requestEntity = new RequestEntity();
        requestEntity.setId(REQUEST_ID);
        String filename = UUID.randomUUID().toString() + "_" + IMAGE_NAME;
        Path filepath = Paths.get(UPLOAD_DIR, filename);

        when(requestRepository.findById(REQUEST_ID)).thenReturn(Optional.of(requestEntity));
        when(multipartFile.getOriginalFilename()).thenReturn(IMAGE_NAME);
        when(multipartFile.getInputStream()).thenReturn(mock(InputStream.class));

        // Mock Files.createDirectories and Files.copy to avoid actual file system interaction
        try (var mockedFiles = mockStatic(Files.class)) {
            mockedFiles.when(() -> Files.createDirectories(filepath.getParent())).thenReturn(filepath.getParent());
            mockedFiles.when(() -> Files.copy(any(InputStream.class), eq(filepath))).thenReturn(100L);

            // Act
            String savedFilename = imageService.saveImage(multipartFile, REQUEST_ID);

            // Assert
            assertNotNull(savedFilename);
            verify(imageRepository, times(1)).save(any(ImageEntity.class));
        }
    }

    @Test
    void testSaveImageInvalidRequestId() {
        // Arrange
        when(requestRepository.findById(REQUEST_ID)).thenReturn(Optional.empty());
        when(multipartFile.getOriginalFilename()).thenReturn(IMAGE_NAME);

        // Act & Assert
        assertThrows(InvalidRequestIdException.class, () -> {
            imageService.saveImage(multipartFile, REQUEST_ID);
        });
    }

    @Test
    void testGetImageNameByRequestIdSuccess() throws IOException {
        // Arrange
        ImageEntity imageEntity = new ImageEntity();
        imageEntity.setImageName(IMAGE_NAME);

        when(imageRepository.findByRequestId(REQUEST_ID)).thenReturn(Optional.of(imageEntity));

        // Act
        String imageName = imageService.getImageNameByRequestId(REQUEST_ID);

        // Assert
        assertEquals(IMAGE_NAME, imageName);
    }

    @Test
    void testGetImageNameByRequestIdNotFound() {
        // Arrange
        when(imageRepository.findByRequestId(REQUEST_ID)).thenReturn(Optional.empty());

        // Act & Assert
        assertThrows(IOException.class, () -> {
            imageService.getImageNameByRequestId(REQUEST_ID);
        });
    }
}
