package backend.controller.dto.listing;

import lombok.Builder;
import lombok.Data;

import java.util.List;

@Data
@Builder
public class GetAllListingsResponse {

    List<GetSingleListingResponse> listings;

}
