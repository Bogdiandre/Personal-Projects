package backend.controller.dto.request;

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
public class UpdateRequestRequest {


    @NotNull
    private Long sellerId;

    @NotNull
    private String maker;

    @NotNull
    private String model;

    @NotNull
    private Integer milage;

    @NotNull
    private Integer maxPrice;

    @NotBlank
    private String details;

    @NotNull
    private LocalDateTime start;

    @NotNull
    private LocalDateTime end;
}
