package backend.service.converters;

import backend.persistance.entity.ListingEntity;
import backend.service.domain.Listing;
import backend.service.domain.User;

public class ListingConverter {






    private ListingConverter() {

    }

    public static Listing convertToDomain(ListingEntity listingEntity) {
        if (listingEntity == null) {
            return null;
        }

        User buyer = null;
        if (listingEntity.getBuyerEntity() != null) {
            buyer = UserConverter.convertToDomain(listingEntity.getBuyerEntity());
        }




        return Listing.builder()
                .id(listingEntity.getId())
                .request(RequestConverter.convertToDomain(listingEntity.getRequestEntity()))
                .buyer(buyer)
                //.bids(bids)
                .status(listingEntity.getStatus())
                .build();
    }


    public static ListingEntity convertToEntity(Listing listing) {
        if (listing == null) {
            return null;
        }
        return ListingEntity.builder()
                .id(listing.getId())
                .requestEntity(RequestConverter.convertToEntity(listing.getRequest()))
                .buyerEntity(listing.getBuyer() != null ? UserConverter.convertToEntity(listing.getBuyer()) : null)
                .status(listing.getStatus())
                .build();
    }
}

