package backend.service.impl;

import backend.persistance.BidRepository;
import backend.persistance.ListingRepository;
import backend.persistance.entity.BidEntity;
import backend.persistance.entity.ListingEntity;
import backend.service.UserService;
import backend.service.converters.ListingConverter;
import backend.service.domain.Bid;
import backend.service.domain.Listing;
import backend.service.domain.Request;
import backend.service.domain.User;
import backend.service.domain.enums.ListingEnum;
import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.RolesEnum;
import backend.service.exception.InvalidBidsException;
import backend.service.exception.InvalidListingException;
import backend.service.exception.InvalidListingIdException;
import backend.service.exception.InvalidUserIdException;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

class ListingServiceImplTest {

    @Mock
    private ListingRepository listingRepository;

    @Mock
    private BidRepository bidRepository;

    @Mock
    private UserService userService;

    @InjectMocks
    private ListingServiceImpl listingService;

    private User createUser(Long id, RolesEnum role) {
        return User.builder()
                .id(id)
                .lastName("Doe")
                .firstName("John")
                .email("john.doe@example.com")
                .password("password")
                .role(role)
                .build();
    }

    private Request createRequest(Long id) {
        return Request.builder()
                .id(id)
                .seller(createUser(1L, RolesEnum.CLIENT))
                .vehicle(null)
                .milage(10000)
                .details("Test details")
                .status(null)
                .start(LocalDateTime.now().minusDays(1))
                .end(LocalDateTime.now().plusDays(1))
                .maxPrice(10000)
                .build();
    }

    private Listing createListingFromRequest(Request request) {
        return new Listing(request);
    }

    private Bid createBid(Long id, User user, double amount) {
        return Bid.builder()
                .id(id)
                .account(user)
                .amount(amount)
                .build();
    }

    @BeforeEach
    void setUp() {
        MockitoAnnotations.openMocks(this);
    }

    @Test
    void testDeleteListing() {
        listingService.deleteListing(1L);
        verify(listingRepository, times(1)).deleteById(1L);
    }

    @Test
    void testGetListing() {
        ListingEntity listingEntity = ListingEntity.builder().id(1L).build();
        when(listingRepository.findById(1L)).thenReturn(Optional.of(listingEntity));

        Optional<Listing> listing = listingService.getListing(1L);
        assertTrue(listing.isPresent());
        assertEquals(1L, listing.get().getId());
    }

    @Test
    void testGetListingNotFound() {
        when(listingRepository.findById(1L)).thenReturn(Optional.empty());

        Optional<Listing> listing = listingService.getListing(1L);
        assertFalse(listing.isPresent());
    }

    @Test
    void testUpdateListing() throws InvalidListingException, InvalidListingIdException {
        Request request = createRequest(1L);
        Listing listing = createListingFromRequest(request);
        listing.setId(1L);
        ListingEntity listingEntity = ListingConverter.convertToEntity(listing);

        when(listingRepository.findById(1L)).thenReturn(Optional.of(listingEntity));
        when(listingRepository.save(any(ListingEntity.class))).thenReturn(listingEntity);

        listingService.updateListing(listing);
        verify(listingRepository, times(1)).save(any(ListingEntity.class));
    }

    @Test
    void testUpdateListingThrowsInvalidListingIdException() {
        Request request = createRequest(1L);
        Listing listing = createListingFromRequest(request);
        listing.setId(1L);

        when(listingRepository.findById(1L)).thenReturn(Optional.empty());

        assertThrows(InvalidListingIdException.class, () -> listingService.updateListing(listing));
    }

    @Test
    void testOpenListing() throws InvalidListingException, InvalidListingIdException {
        Request request = createRequest(1L);
        Listing listing = createListingFromRequest(request);
        listing.setId(1L);
        listing.setStatus(ListingEnum.PENDING);
        ListingEntity listingEntity = ListingConverter.convertToEntity(listing);

        when(listingRepository.findById(1L)).thenReturn(Optional.of(listingEntity));
        when(listingRepository.save(any(ListingEntity.class))).thenReturn(listingEntity);

        listingService.openListing(1L);
        verify(listingRepository, times(1)).save(any(ListingEntity.class));
    }

    @Test
    void testOpenListingThrowsInvalidListingIdException() {
        when(listingRepository.findById(1L)).thenReturn(Optional.empty());

        assertThrows(InvalidListingIdException.class, () -> listingService.openListing(1L));
    }

    @Test
    void testBuyOutListing() throws InvalidListingException, InvalidListingIdException, InvalidUserIdException {
        Request request = createRequest(1L);
        Listing listing = createListingFromRequest(request);
        listing.setId(1L);
        listing.setStatus(ListingEnum.OPEN);
        ListingEntity listingEntity = ListingConverter.convertToEntity(listing);
        User buyer = createUser(1L, RolesEnum.CLIENT);

        when(listingRepository.findById(1L)).thenReturn(Optional.of(listingEntity));
        when(userService.getUser(1L)).thenReturn(Optional.of(buyer));
        when(listingRepository.save(any(ListingEntity.class))).thenReturn(listingEntity);

        listingService.buyOutListing(1L, 1L);
        verify(listingRepository, times(1)).save(any(ListingEntity.class));
    }

    @Test
    void testBuyOutListingThrowsInvalidListingIdException() {
        when(listingRepository.findById(1L)).thenReturn(Optional.empty());

        assertThrows(InvalidListingIdException.class, () -> listingService.buyOutListing(1L, 1L));
    }

    @Test
    void testAddBid() throws InvalidBidsException, InvalidListingException, InvalidListingIdException {
        Request request = createRequest(1L);
        Listing listing = createListingFromRequest(request);
        listing.setId(1L);
        listing.setStatus(ListingEnum.OPEN);
        ListingEntity listingEntity = ListingConverter.convertToEntity(listing);
        Bid bid = createBid(1L, createUser(1L, RolesEnum.CLIENT), 5000);

        when(listingRepository.findById(1L)).thenReturn(Optional.of(listingEntity));
        when(listingRepository.save(any(ListingEntity.class))).thenReturn(listingEntity);
        when(bidRepository.save(any(BidEntity.class))).thenAnswer(invocation -> {
            BidEntity bidEntity = invocation.getArgument(0);
            bidEntity.setId(1L); // Simulate ID generation
            return bidEntity;
        });

        Long bidId = listingService.addBid(bid, 1L);
        assertNotNull(bidId);
        assertEquals(1L, bidId);
    }

    @Test
    void testAddBidThrowsInvalidListingIdException() {
        Bid bid = createBid(1L, createUser(1L, RolesEnum.CLIENT), 5000);

        when(listingRepository.findById(1L)).thenReturn(Optional.empty());

        assertThrows(InvalidListingIdException.class, () -> listingService.addBid(bid, 1L));
    }

    @Test
    void testGetAllBidsForListing() throws InvalidListingIdException {
        ListingEntity listingEntity = ListingEntity.builder().id(1L).build();
        BidEntity bidEntity = BidEntity.builder().id(1L).amount(5000).build();
        when(listingRepository.findById(1L)).thenReturn(Optional.of(listingEntity));
        when(bidRepository.getAllBidsForListing(1L)).thenReturn(List.of(bidEntity));

        List<Bid> bids = listingService.getAllBidsForListing(1L);
        assertEquals(1, bids.size());
        assertEquals(5000, bids.get(0).getAmount());
    }

    @Test
    void testGetAllBidsForListingThrowsInvalidListingIdException() {
        when(listingRepository.findById(1L)).thenReturn(Optional.empty());

        assertThrows(InvalidListingIdException.class, () -> listingService.getAllBidsForListing(1L));
    }

    @Test
    void testGetAllListings() {
        ListingEntity listingEntity = ListingEntity.builder().id(1L).build();
        when(listingRepository.findAll()).thenReturn(List.of(listingEntity));
        when(bidRepository.getAllBidsForListing(1L)).thenReturn(List.of());

        List<Listing> listings = listingService.getAllListings();
        assertEquals(1, listings.size());
        assertEquals(1L, listings.get(0).getId());
    }

    @Test
    void testGetAllOpenListings() {
        Request request = createRequest(1L);
        Listing listing = createListingFromRequest(request);
        listing.setId(1L);
        listing.setStatus(ListingEnum.OPEN);
        ListingEntity listingEntity = ListingConverter.convertToEntity(listing);

        when(listingRepository.findAll()).thenReturn(List.of(listingEntity));
        when(bidRepository.getAllBidsForListing(1L)).thenReturn(List.of());

        listing = ListingConverter.convertToDomain(listingEntity);
        listing.setRequest(request);
        checkAndUpdateStatus(listing);

        List<Listing> listings = listingService.getAllOpenListings();
        assertEquals(1, listings.size());
        assertEquals(1L, listings.get(0).getId());
        assertEquals(ListingEnum.OPEN, listings.get(0).getStatus());
    }

    @Test
    void testGetHighestBidForListing() throws InvalidListingIdException {
        ListingEntity listingEntity = ListingEntity.builder().id(1L).build();
        BidEntity bidEntity = BidEntity.builder().id(1L).amount(5000).build();
        when(listingRepository.findById(1L)).thenReturn(Optional.of(listingEntity));
        when(bidRepository.getHighestBidForListing(1L)).thenReturn(bidEntity);

        Bid highestBid = listingService.getHighestBidForListing(1L);
        assertNotNull(highestBid);
        assertEquals(5000, highestBid.getAmount());
    }

    @Test
    void testGetHighestBidForListingThrowsInvalidListingIdException() {
        when(listingRepository.findById(1L)).thenReturn(Optional.empty());

        assertThrows(InvalidListingIdException.class, () -> listingService.getHighestBidForListing(1L));
    }

    @Test
    void testGetAverageSoldPrice() {
        when(listingRepository.findAveragePriceOfSoldVehicle(MakerEnum.TESLA, "Model X")).thenReturn(60000);

        int averagePrice = listingService.getAverageSoldPrice("TESLA", "Model X");
        assertEquals(60000, averagePrice);
    }

    // Private method from ListingServiceImpl to be used in the test
    private void checkAndUpdateStatus(Listing listing) {
        listing.checkStatus();
        listingRepository.save(ListingConverter.convertToEntity(listing)); // Update the repository
    }
}
