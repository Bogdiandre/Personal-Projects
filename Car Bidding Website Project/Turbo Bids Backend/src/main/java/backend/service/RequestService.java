package backend.service;

import backend.service.domain.Request;
import backend.service.domain.enums.VehicleTypeEnum;
import backend.service.exception.InvalidRequestException;
import backend.service.exception.InvalidRequestIdException;

import java.util.List;
import java.util.Optional;

public interface RequestService {
    Long createRequest(Request request) throws InvalidRequestException;

    void deleteRequest(Long requestId);

    Optional<Request> getRequest(Long requestId);

    void updateRequest(Request request) throws InvalidRequestException, InvalidRequestIdException;

    Long acceptRequest(Long requestId) throws InvalidRequestIdException, InvalidRequestException;

    void declineRequest(Long requestId) throws InvalidRequestIdException, InvalidRequestException;

    List<Request> getAllRequestsByUser(Long userId);
    List<Request> getAllRequests();

    List<Request> getAllPendingRequests();

    List<Request> getAllPendingRequestsWithEndAfterNow();

    List<Request> getPendingRequestsBySellerId(Long sellerId);

    List<Request> getPendingRequestsByVehicleType(VehicleTypeEnum vehicleType);
}
