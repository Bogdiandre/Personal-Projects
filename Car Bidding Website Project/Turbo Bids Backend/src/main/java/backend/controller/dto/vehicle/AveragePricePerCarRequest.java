package backend.controller.dto.vehicle;

import jakarta.validation.constraints.NotBlank;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class AveragePricePerCarRequest {

    @NotBlank
    private String maker;

    @NotBlank
    private String model;
}
