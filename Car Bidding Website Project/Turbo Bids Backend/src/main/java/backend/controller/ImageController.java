package backend.controller;

import backend.service.ImageService;
import backend.service.exception.InvalidRequestIdException;
import lombok.AllArgsConstructor;
import org.springframework.core.io.ByteArrayResource;
import org.springframework.core.io.Resource;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.client.RestTemplate;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;

@RestController
@AllArgsConstructor
@RequestMapping("/images")
@CrossOrigin(origins = "http://localhost:5173")
public class ImageController {

    private final ImageService imageService;

    @PostMapping("/upload")
    public ResponseEntity<String> uploadImage(@RequestParam("image") MultipartFile image, @RequestParam("requestId") Long requestId) {
        try {
            String filename = imageService.saveImage(image, requestId);
            return ResponseEntity.status(HttpStatus.CREATED).body(filename);
        } catch (IOException e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error uploading image");
        } catch (InvalidRequestIdException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Invalid request ID: " + requestId);
        }
    }

    @GetMapping("/request/{requestId}")
    public ResponseEntity<Resource> getImageByRequestId(@PathVariable Long requestId) {
        try {
            String imageName = imageService.getImageNameByRequestId(requestId);
            String imageUrl = "http://localhost:8080/images/" + imageName;

            RestTemplate restTemplate = new RestTemplate();
            ResponseEntity<byte[]> response = restTemplate.getForEntity(imageUrl, byte[].class);

            if (response.getStatusCode() == HttpStatus.OK) {
                ByteArrayResource resource = new ByteArrayResource(response.getBody());

                HttpHeaders headers = new HttpHeaders();
                headers.setContentDisposition(ContentDisposition.builder("attachment")
                        .filename(imageName)
                        .build());
                headers.setContentType(MediaType.IMAGE_JPEG);

                return new ResponseEntity<>(resource, headers, HttpStatus.OK);
            } else {
                throw new RuntimeException("Failed to retrieve the image from URL: " + imageUrl);
            }
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(null);
        }
    }
}
