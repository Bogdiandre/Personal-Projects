package backend.controller.dto.bid;

import backend.controller.dto.user.GetSingleUserResponse;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;

@Data
@Builder
@AllArgsConstructor
public class GetSingleBidResponse {
    private Long id;

    private GetSingleUserResponse account;

    private double amount;
}
