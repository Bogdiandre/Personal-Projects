package backend.controller.dto.vehicle;

import lombok.Builder;
import lombok.Data;

import java.util.List;



@Data
@Builder
public class GetVehiclesResponse {
    private List<GetSingleVehicleResponse> vehicles;


}
