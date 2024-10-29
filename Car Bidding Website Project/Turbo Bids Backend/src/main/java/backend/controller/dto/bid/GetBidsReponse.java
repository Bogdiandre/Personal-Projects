package backend.controller.dto.bid;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;

import java.util.List;
@Data
@Builder
@AllArgsConstructor
public class GetBidsReponse {

    List<GetSingleBidResponse> bids;

}
