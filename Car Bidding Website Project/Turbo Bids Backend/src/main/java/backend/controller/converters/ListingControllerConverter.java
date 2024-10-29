package backend.controller.converters;

import backend.controller.dto.listing.CreateListingRequest;
import backend.controller.dto.listing.CreateListingResponse;
import backend.controller.dto.listing.GetAllListingsResponse;
import backend.controller.dto.listing.GetSingleListingResponse;
import backend.service.domain.Listing;
import backend.service.domain.enums.ListingEnum;
import backend.persistance.RequestRepository;
import backend.service.converters.RequestConverter;
import backend.persistance.entity.RequestEntity;

import java.util.List;


public class ListingControllerConverter {

    private static RequestRepository requestRepository;

    private ListingControllerConverter() {}


    public static CreateListingResponse convertToCreateListingResponse(Long listingId) {
        return CreateListingResponse.builder()
                .listingId(listingId)
                .build();
    }

    public static Listing convertFromCreateListingRequest(CreateListingRequest request) {
        RequestEntity requestEntity = requestRepository.findById(request.getRequestId()).orElseThrow();
        return Listing.builder()
                .request(RequestConverter.convertToDomain(requestEntity))
                .status(ListingEnum.PENDING)
                .build();
    }

    public static GetAllListingsResponse getAllListingsResponseFromDomain(List<Listing> listings) {
        List<GetSingleListingResponse> responseListings = listings.stream()
                .map(listing -> GetSingleListingResponse.builder()
                        .id(listing.getId())
                        .request(RequestControllerConverter.convertToGetSingleRequestResponse(listing.getRequest()))
                        .buyerId(listing.getBuyer() != null ? listing.getBuyer().getId() : null)
                        .status(listing.getStatus())
                        .build())
                .toList();
        return GetAllListingsResponse.builder()
                .listings(responseListings)
                .build();
    }

    public static GetSingleListingResponse getSingleListingResponseFromDomain(Listing listing) {
        return GetSingleListingResponse.builder()
                .id(listing.getId())
                .request(RequestControllerConverter.convertToGetSingleRequestResponse(listing.getRequest()))
                .buyerId(listing.getBuyer() != null ? listing.getBuyer().getId() : null)
                .status(listing.getStatus())
                .build();
    }
}
