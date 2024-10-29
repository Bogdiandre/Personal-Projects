package backend.controller.dto.listing;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class CreateListingResponse {

    private Long listingId;
}
