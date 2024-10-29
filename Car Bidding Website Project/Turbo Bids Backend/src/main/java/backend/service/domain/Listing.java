package backend.service.domain;

import backend.service.domain.enums.ListingEnum;
import backend.service.exception.InvalidBidsException;
import backend.service.exception.InvalidListingException;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Builder
@AllArgsConstructor
@NoArgsConstructor
@Data
public class Listing {

    private static final Logger logger = LoggerFactory.getLogger(Listing.class);

    private Long id;
    private Request request;
    private User buyer;
    @Builder.Default
    private List<Bid> bids = new ArrayList<>();
    private ListingEnum status;

    public boolean compareTimeToPresent(LocalDateTime time) {
        LocalDateTime currentTime = LocalDateTime.now();
        return currentTime.compareTo(time) > 0;
    }

    public void checkStatus() {
        if (this.status == ListingEnum.PENDING) {
            if (compareTimeToPresent(this.request.getStart())) {
                this.status = ListingEnum.OPEN;
            }
            if (compareTimeToPresent(this.request.getEnd()) && bids.isEmpty()) {
                this.status = ListingEnum.NOTBOUGHT;
            }
        }

        if (this.status == ListingEnum.OPEN) {
            if (compareTimeToPresent(this.request.getEnd()) && bids.isEmpty()) {
                this.status = ListingEnum.NOTBOUGHT;
            }

            if (compareTimeToPresent(this.request.getEnd()) && !bids.isEmpty()) {
                this.status = ListingEnum.BIDBUY;
                this.buyer = bids.get(bids.size() - 1).getAccount();
            }
        }
    }

    public Listing(Request request) {
        this.request = request;
        if (compareTimeToPresent(request.getStart())) {
            this.status = ListingEnum.OPEN;
        } else {
            this.status = ListingEnum.PENDING;
        }
        this.bids = new ArrayList<>();
    }

    public void addBid(Bid bid) throws InvalidBidsException, InvalidListingException {
        if (this.status == ListingEnum.OPEN) {
            if (bids == null) {
                bids = new ArrayList<>();
            }
            if (bids.isEmpty() || bid.getAmount() > bids.get(bids.size() - 1).getAmount()) {
                bids.add(bid);

                if (bid.getAmount() >= this.request.getMaxPrice()) {
                    this.status = ListingEnum.BUYOUT;
                    this.buyer = bid.getAccount();
                }
            } else {
                throw new InvalidBidsException();
            }
        } else {
            throw new InvalidListingException();
        }
    }

    public void open() throws InvalidListingException {
        if (this.status == ListingEnum.PENDING) {
            this.status = ListingEnum.OPEN;
        } else {
            throw new InvalidListingException();
        }
    }

    public void buyOut(User user) throws InvalidListingException {
        if (this.status == ListingEnum.OPEN) {
            this.status = ListingEnum.BUYOUT;
            this.buyer = user;
        } else {
            throw new InvalidListingException();
        }
    }

    public void bidBuy(User user) throws InvalidListingException {
        if (this.status == ListingEnum.OPEN) {
            this.status = ListingEnum.BIDBUY;
            this.buyer = user;
        } else {
            throw new InvalidListingException();
        }
    }
}
