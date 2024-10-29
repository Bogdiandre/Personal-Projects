package backend.controller.converters;

import backend.controller.dto.bid.CreateBidRequest;
import backend.controller.dto.bid.CreateBidResponse;
import backend.controller.dto.bid.GetSingleBidResponse;
import backend.controller.dto.bid.GetBidsReponse;
import backend.controller.dto.bid.GetHighestBidResponse; // Import the DTO
import backend.service.domain.Bid;
import backend.service.domain.User;

import java.util.List;

public class BidControllerConverter {

    private BidControllerConverter(){

    }
    public static GetBidsReponse getBidsResponseFromDomain(List<Bid> bidList) {
        List<GetSingleBidResponse> responseBidList = bidList.stream()
                .map(bid -> GetSingleBidResponse.builder()
                        .id(bid.getId())
                        .account(UserControllerConverter.getSingleUserResponseFromDomain(bid.getAccount()))
                        .amount(bid.getAmount())
                        .build())
                .toList();

        return GetBidsReponse.builder()
                .bids(responseBidList)
                .build();
    }

    public static Bid convertFromCreateBidRequest(CreateBidRequest request, User account) {
        return Bid.builder()
                .account(account)
                .amount(request.getAmount())
                .build();
    }

    public static CreateBidResponse convertToCreateBidResponse(Long bidId) {
        return CreateBidResponse.builder()
                .bidId(bidId)
                .build();
    }

    public static GetSingleBidResponse getBidResponseFromDomain(Bid bid) {
        return GetSingleBidResponse.builder()
                .id(bid.getId())
                .account(UserControllerConverter.getSingleUserResponseFromDomain(bid.getAccount()))
                .amount(bid.getAmount())
                .build();
    }

    public static GetHighestBidResponse convertToGetHighestBidResponse(Bid bid) {
        User user = bid.getAccount();
        return GetHighestBidResponse.builder()
                .firstName(user.getFirstName())
                .lastName(user.getLastName())
                .amount(bid.getAmount())
                .build();
    }
}
