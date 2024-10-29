package backend.service.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class Notification {
    private Long id;
    private String message;
    private String recipient;
    private boolean seen;
    private Listing listing;
}
