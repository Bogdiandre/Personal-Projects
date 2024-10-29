package backend.service.converters;

import backend.persistance.entity.BidEntity;
import backend.persistance.entity.ListingEntity;
import backend.service.domain.Bid;

import java.util.ArrayList;
import java.util.List;

public class BidConverter {


    private BidConverter() {

    }

    public static Bid convertToDomain(BidEntity bidEntity) {
        if (bidEntity == null) {
            return null;
        }
        return Bid.builder()
                .id(bidEntity.getId())
                .account(UserConverter.convertToDomain(bidEntity.getAccountEntity()))
                .amount(bidEntity.getAmount())
                .build();
    }

    public static BidEntity convertToEntity(Bid bid, ListingEntity listingEntity) {
        if (bid == null) {
            return null;
        }
        return BidEntity.builder()
                .id(bid.getId())
                .listingEntity(listingEntity)
                .accountEntity(UserConverter.convertToEntity(bid.getAccount()))
                .amount(bid.getAmount())
                .build();
    }

    public static List<Bid> convertToDomainList(List<BidEntity> bidEntities) {
        List<Bid> bids = new ArrayList<>();
        for (BidEntity bidEntity : bidEntities) {
            bids.add(convertToDomain(bidEntity));
        }
        return bids;
    }


}
