package backend.controller.dto.vehicle;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class GetAllModelsResponse {

    private String model;
}
