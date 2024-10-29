package backend.controller.dto.request;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class CreateRequestResponse {
    private Long requestId;
}
