package backend.controller.dto.request;

import backend.controller.dto.user.GetSingleUserResponse;
import backend.controller.dto.vehicle.GetSingleVehicleResponse;
import lombok.Builder;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@Builder
public class GetSingleRequestResponse {
    private Long id;
    private GetSingleVehicleResponse vehicle;
    private GetSingleUserResponse sellerId;
    private Integer milage;
    private String details;
    private String status;
    private LocalDateTime start;
    private LocalDateTime end;
    private Integer maxPrice;

}
