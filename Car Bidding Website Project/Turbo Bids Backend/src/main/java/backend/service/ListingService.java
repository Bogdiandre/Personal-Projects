package backend.service;

import backend.service.domain.Bid;
import backend.service.domain.Listing;
import backend.service.domain.enums.MakerEnum;
import backend.service.exception.InvalidBidsException;
import backend.service.exception.InvalidListingException;
import backend.service.exception.InvalidListingIdException;
import backend.service.exception.InvalidUserIdException;

import java.util.List;
import java.util.Optional;

public interface ListingService {

    void deleteListing(long listingId);
    Optional<Listing> getListing(long listingId);
    void updateListing(Listing listing) throws InvalidListingException, InvalidListingIdException;
    void openListing(long listingId) throws InvalidListingException, InvalidListingIdException;
    void buyOutListing(long listingId, long buyerId) throws InvalidListingException, InvalidListingIdException, InvalidUserIdException;
    Long addBid(Bid bid, long listingId) throws InvalidBidsException, InvalidListingException, InvalidListingIdException;
    List<Bid> getAllBidsForListing(long listingId) throws InvalidListingIdException;
    List<Listing> getAllListings();
    List<Listing> getAllOpenListings();
    Bid getHighestBidForListing(long listingId) throws InvalidListingIdException;

    List<Listing> getOpenListingsByMakerAndModel(MakerEnum maker, String model);
    int getAverageSoldPrice(String maker, String model);

    List<Listing> getPersonalOpenListings(Long sellerId);
}
