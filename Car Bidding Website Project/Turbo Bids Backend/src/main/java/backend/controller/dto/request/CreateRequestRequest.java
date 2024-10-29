package backend.controller.dto.request;

import backend.service.domain.enums.RequestEnum;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class CreateRequestRequest {
    @NotNull
    private Long vehicleId;

    @NotNull
    private Long sellerId;

    @NotNull
    private Integer milage;

    @NotBlank
    private String details;

    @NotNull
    private RequestEnum status;

    @NotNull
    private LocalDateTime start;

    @NotNull
    private LocalDateTime end;

    @NotNull
    private Integer maxPrice;

}
