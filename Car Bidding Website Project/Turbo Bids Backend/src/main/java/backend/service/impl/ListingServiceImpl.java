package backend.service.impl;

import backend.persistance.BidRepository;
import backend.persistance.ListingRepository;
import backend.persistance.entity.BidEntity;
import backend.persistance.entity.ListingEntity;
import backend.service.ListingService;
import backend.service.UserService;
import backend.service.converters.BidConverter;
import backend.service.converters.ListingConverter;
import backend.service.domain.Bid;
import backend.service.domain.Listing;
import backend.service.domain.User;
import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.ListingEnum;
import backend.service.exception.*;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class ListingServiceImpl implements ListingService {

    private final ListingRepository listingRepository;
    private final BidRepository bidRepository;
    private final UserService userService;

    public ListingServiceImpl(ListingRepository listingRepository, BidRepository bidRepository, UserService userService) {
        this.listingRepository = listingRepository;
        this.userService = userService;
        this.bidRepository = bidRepository;
    }

    @Override
    public void deleteListing(long listingId) {
        listingRepository.deleteById(listingId);
    }

    @Override
    public Optional<Listing> getListing(long listingId) {
        Optional<ListingEntity> optionalListingEntity = listingRepository.findById(listingId);
        if (optionalListingEntity.isPresent()) {
            Listing listing = ListingConverter.convertToDomain(optionalListingEntity.get());
            return Optional.of(listing);
        } else {
            return Optional.empty();
        }
    }

    @Override
    public void updateListing(Listing listing) throws InvalidListingException, InvalidListingIdException {
        if (listing.getId() == null || listingRepository.findById(listing.getId()).isEmpty()) {
            throw new InvalidListingIdException();
        }
        ListingEntity listingEntity = ListingConverter.convertToEntity(listing);
        listingRepository.save(listingEntity);
    }

    @Override
    public void openListing(long listingId) throws InvalidListingException, InvalidListingIdException {
        if (listingRepository.findById(listingId).isEmpty()) {
            throw new InvalidListingIdException();
        } else {
            Listing listing = getListing(listingId).orElseThrow();
            listing.open();
            updateListing(listing);
        }
    }

    @Override
    public void buyOutListing(long listingId, long buyerId) throws InvalidListingException, InvalidListingIdException, InvalidUserIdException {
        if (listingRepository.findById(listingId).isEmpty()) {
            throw new InvalidListingIdException();
        } else {
            Listing listing = getListing(listingId).orElseThrow();
            User buyer = userService.getUser(buyerId).orElseThrow();
            listing.buyOut(buyer);
            updateListing(listing);
        }
    }

    @Override
    public Long addBid(Bid bid, long listingId) throws InvalidBidsException, InvalidListingException, InvalidListingIdException {
        if (listingRepository.findById(listingId).isEmpty()) {
            throw new InvalidListingIdException();
        } else {
            Listing listing = getListing(listingId).orElseThrow();
            listing.setBids(BidConverter.convertToDomainList(bidRepository.getAllBidsForListing(listingId)));
            listing.addBid(bid);
            updateListing(listing);
            ListingEntity listingEntity = listingRepository.findById(listingId).orElseThrow(); // I am not sure I need this one
            BidEntity savedBid = bidRepository.save(BidConverter.convertToEntity(bid, listingEntity));
            return savedBid.getId();
        }
    }

    @Override
    public List<Bid> getAllBidsForListing(long listingId) throws InvalidListingIdException {
        if (listingRepository.findById(listingId).isEmpty()) {
            throw new InvalidListingIdException();
        } else {
            return BidConverter.convertToDomainList(bidRepository.getAllBidsForListing(listingId));
        }
    }

    @Override
    public List<Listing> getAllListings() {
        List<ListingEntity> listingEntities = listingRepository.findAll();
        return listingEntities.stream()
                .map(this::convertAndSetBids)
                .toList();
    }

    private Listing convertAndSetBids(ListingEntity listingEntity) {
        Listing listing = ListingConverter.convertToDomain(listingEntity);
        try {
            listing.setBids(getAllBidsForListing(listing.getId()));
        } catch (InvalidListingIdException e) {
            listing.setBids(null);
        }
        return listing;
    }

    @Override
    public List<Listing> getAllOpenListings() {
        List<ListingEntity> listingEntities = listingRepository.findAll();
        List<Listing> listings = new ArrayList<>();

        for (int i = 0; i < listingEntities.size(); i++) {
            Listing listing = ListingConverter.convertToDomain(listingEntities.get(i));
            try {
                listing.setBids(getAllBidsForListing(listing.getId()));
            } catch (InvalidListingIdException e) {
                listing.setBids(null);
            }
            checkAndUpdateStatus(listing);
            if (listing.getStatus() == ListingEnum.OPEN) {
                listings.add(listing);
            }
        }

        return listings;
    }

    @Override
    public Bid getHighestBidForListing(long listingId) throws InvalidListingIdException {
        if (getListing(listingId) == null || listingRepository.findById(listingId).isEmpty()) {
            throw new InvalidListingIdException();
        } else {
            BidEntity bidEntity = bidRepository.getHighestBidForListing(listingId);

            return BidConverter.convertToDomain(bidEntity);
        }
    }

    @Override
    public int getAverageSoldPrice(String maker, String model) {
        MakerEnum makerEnum = MakerEnum.fromString(maker);
        return listingRepository.findAveragePriceOfSoldVehicle(makerEnum, model);
    }

    private void checkAndUpdateStatus(Listing listing) {
        listing.checkStatus();
        listingRepository.save(ListingConverter.convertToEntity(listing)); // Update the repository
    }

    @Override
    public List<Listing> getOpenListingsByMakerAndModel(MakerEnum maker, String model) {
        List<ListingEntity> listingEntities = listingRepository.findOpenListingsByMakerAndModel(maker, model);
        return listingEntities.stream()
                .map(this::convertAndSetBids)
                .toList();
    }

    @Override
    public List<Listing> getPersonalOpenListings(Long sellerId) {
        return listingRepository.findAllListingsBySellerId(sellerId)
                .stream()
                .map(ListingConverter::convertToDomain)
                .toList();
    }
}
