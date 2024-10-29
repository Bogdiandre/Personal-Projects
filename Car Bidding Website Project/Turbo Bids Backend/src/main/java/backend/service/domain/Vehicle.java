package backend.service.domain;

import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.VehicleTypeEnum;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Builder
@Data
@NoArgsConstructor
@AllArgsConstructor
public class Vehicle {

    private Long id;

    private String model;

    private MakerEnum maker;

    private VehicleTypeEnum type;

}



