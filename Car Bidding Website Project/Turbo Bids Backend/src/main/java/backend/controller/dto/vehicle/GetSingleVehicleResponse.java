package backend.controller.dto.vehicle;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;

@Data
@Builder
@AllArgsConstructor
public class GetSingleVehicleResponse {
    private Long id;

    private String model;

    private String maker;

    private String type;
}
