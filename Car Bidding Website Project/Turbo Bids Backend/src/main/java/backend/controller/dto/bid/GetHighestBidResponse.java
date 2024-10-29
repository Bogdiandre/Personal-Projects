package backend.controller.dto.bid;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;

@Data
@Builder
@AllArgsConstructor
public class GetHighestBidResponse {
    private String firstName;
    private String lastName;
    private Double amount;
}
