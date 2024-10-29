package backend.controller.dto.bid;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class CreateBidResponse {

    private Long bidId;
}
