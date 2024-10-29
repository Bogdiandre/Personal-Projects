package backend.controller;

import backend.controller.converters.BidControllerConverter;
import backend.controller.converters.ListingControllerConverter;
import backend.controller.dto.bid.CreateBidRequest;
import backend.controller.dto.bid.CreateBidResponse;
import backend.controller.dto.bid.GetBidsReponse;
import backend.controller.dto.bid.GetHighestBidResponse;
import backend.controller.dto.listing.GetAllListingsResponse;
import backend.controller.dto.listing.GetSingleListingResponse;
import backend.service.ListingService;
import backend.service.NotificationService;
import backend.service.UserService;
import backend.service.domain.Bid;
import backend.service.domain.Listing;
import backend.service.domain.User;
import backend.service.domain.enums.MakerEnum;
import backend.service.exception.InvalidBidsException;
import backend.service.exception.InvalidListingException;
import backend.service.exception.InvalidListingIdException;
import backend.service.exception.InvalidUserIdException;
import jakarta.annotation.security.RolesAllowed;
import jakarta.validation.Valid;
import lombok.AllArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@AllArgsConstructor
@RequestMapping("/listings")
@CrossOrigin(origins = "http://localhost:5173")
public class ListingController {

    private final ListingService listingService;
    private final UserService userService;
    private final NotificationService notificationService;

    @RolesAllowed("EMPLOYEE")
    @GetMapping
    public ResponseEntity<GetAllListingsResponse> getAllListings() {
        List<Listing> listings = listingService.getAllListings();
        GetAllListingsResponse response = ListingControllerConverter.getAllListingsResponseFromDomain(listings);
        return ResponseEntity.ok(response);
    }

    @GetMapping("/{listingId}")
    public ResponseEntity<GetSingleListingResponse> getListingById(@PathVariable long listingId) {
        Listing listing = listingService.getListing(listingId).orElseThrow();
        GetSingleListingResponse response = ListingControllerConverter.getSingleListingResponseFromDomain(listing);
        return ResponseEntity.ok(response);
    }

    @RolesAllowed("EMPLOYEE")
    @DeleteMapping("/{listingId}")
    public ResponseEntity<Void> deleteListing(@PathVariable long listingId) {
        listingService.deleteListing(listingId);
        return ResponseEntity.noContent().build();
    }

    @RolesAllowed("CLIENT")
    @PostMapping("/buyout/{listingId}/buyer/{buyerId}")
    public ResponseEntity<Void> buyOutListing(@PathVariable long listingId, @PathVariable long buyerId) {
        try {
            listingService.buyOutListing(listingId, buyerId);

            // Fetch the listing again after adding the bid
            Listing listing = listingService.getListing(listingId).orElseThrow();
            notificationService.createAndSendBidNotificationForBuyOut(buyerId, listing);

            return ResponseEntity.ok().build();
        } catch (InvalidListingException | InvalidListingIdException | InvalidUserIdException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).build();
        }
    }

    @RolesAllowed("EMPLOYEE")
    @GetMapping("/{listingId}/bids")
    public ResponseEntity<GetBidsReponse> getBidsForListing(@PathVariable long listingId) {
        try {
            List<Bid> bids = listingService.getAllBidsForListing(listingId);
            GetBidsReponse response = BidControllerConverter.getBidsResponseFromDomain(bids);
            return ResponseEntity.ok(response);
        } catch (InvalidListingIdException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).build();
        }
    }

    @RolesAllowed("CLIENT")
    @PostMapping("/{listingId}/addBid")
    public ResponseEntity<CreateBidResponse> addBid(@RequestBody @Valid CreateBidRequest bidRequest, @PathVariable long listingId) {
        try {
            User user = userService.getUser(bidRequest.getAccountId()).orElseThrow();
            Bid bid = BidControllerConverter.convertFromCreateBidRequest(bidRequest, user);
            Long bidId = listingService.addBid(bid, listingId);
            CreateBidResponse response = BidControllerConverter.convertToCreateBidResponse(bidId);

            // Fetch the listing again after adding the bid
            Listing listing = listingService.getListing(listingId).orElseThrow();
            notificationService.createAndSendBidNotificationForBid(bid, listing);

            return ResponseEntity.ok(response);
        } catch (InvalidBidsException | InvalidListingException | InvalidListingIdException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).build();
        }
    }

    @GetMapping("/{listingId}/highestBid")
    public ResponseEntity<GetHighestBidResponse> getHighestBidForListing(@PathVariable long listingId) {
        try {
            Bid highestBid = listingService.getHighestBidForListing(listingId);
            if (highestBid == null) {
                // Return a default response or a 204 No Content status
                return ResponseEntity.noContent().build();
            }
            GetHighestBidResponse response = BidControllerConverter.convertToGetHighestBidResponse(highestBid);
            return ResponseEntity.ok(response);
        } catch (InvalidListingIdException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).build();
        }
    }

    @GetMapping("/open")
    public ResponseEntity<GetAllListingsResponse> getAllOpenListings() {
        List<Listing> openListings = listingService.getAllOpenListings();
        GetAllListingsResponse response = ListingControllerConverter.getAllListingsResponseFromDomain(openListings);
        return ResponseEntity.ok(response);
    }

    @RolesAllowed("MANAGER")
    @GetMapping("/averagePriceForVehicle")
    public ResponseEntity<Integer> getAveragePriceForVehicle(@RequestParam String maker, @RequestParam String model) {
        int averagePrice = listingService.getAverageSoldPrice(maker, model);
        return ResponseEntity.ok(averagePrice);
    }

    @GetMapping("/open/listingsByMakerAndModel")
    public ResponseEntity<GetAllListingsResponse> getOpenListingsByMakerAndModel(@RequestParam MakerEnum maker, @RequestParam String model) {
        List<Listing> listings = listingService.getOpenListingsByMakerAndModel(maker, model);
        GetAllListingsResponse response = ListingControllerConverter.getAllListingsResponseFromDomain(listings);
        return ResponseEntity.ok(response);
    }

    @RolesAllowed("CLIENT")
    @GetMapping("/personal/{sellerId}")
    public ResponseEntity<GetAllListingsResponse> getPersonalOpenListings(@PathVariable Long sellerId) {
        List<Listing> listings = listingService.getPersonalOpenListings(sellerId);
        GetAllListingsResponse response = ListingControllerConverter.getAllListingsResponseFromDomain(listings);
        return ResponseEntity.ok(response);
    }
}
