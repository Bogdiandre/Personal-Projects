package backend.controller.dto.listing;

import backend.controller.dto.request.GetSingleRequestResponse;
import backend.service.domain.enums.ListingEnum;
import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class GetSingleListingResponse {

    private Long id;

    private GetSingleRequestResponse request;

    private Long buyerId;

    private ListingEnum status;
}
