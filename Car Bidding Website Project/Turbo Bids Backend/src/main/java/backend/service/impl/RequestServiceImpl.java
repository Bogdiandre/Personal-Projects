package backend.service.impl;

import backend.persistance.ListingRepository;
import backend.persistance.RequestRepository;
import backend.persistance.entity.ListingEntity;
import backend.persistance.entity.RequestEntity;
import backend.service.RequestService;
import backend.service.converters.ListingConverter;
import backend.service.converters.RequestConverter;
import backend.service.domain.Request;
import backend.service.domain.enums.RequestEnum;
import backend.service.domain.enums.RolesEnum;
import backend.service.domain.enums.VehicleTypeEnum;
import backend.service.exception.InvalidRequestException;
import backend.service.exception.InvalidRequestIdException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Service
public class RequestServiceImpl implements RequestService {

    private final RequestRepository requestRepository;
    private final ListingRepository listingRepository;

    @Autowired
    public RequestServiceImpl(RequestRepository requestRepository, ListingRepository listingRepository) {
        this.requestRepository = requestRepository;
        this.listingRepository = listingRepository;
    }

    @Override
    public Long createRequest(Request request) throws InvalidRequestException {
        if (request.getVehicle() == null) {
            throw new InvalidRequestException();
        }
        if (request.getSeller() == null) {
            throw new InvalidRequestException();
        }
        if (request.getSeller().getRole() != RolesEnum.CLIENT) {
            throw new InvalidRequestException();
        }
        RequestEntity requestEntity = RequestConverter.convertToEntity(request);
        RequestEntity savedRequest = requestRepository.save(requestEntity);
        if (savedRequest == null) {
            throw new InvalidRequestException();
        }
        return savedRequest.getId();
    }

    @Override
    public void deleteRequest(Long requestId) {
        requestRepository.deleteById(requestId);
    }

    @Override
    public Optional<Request> getRequest(Long requestId) {
        return requestRepository.findById(requestId)
                .map(RequestConverter::convertToDomain);
    }

    @Override
    public void updateRequest(Request request) throws InvalidRequestException, InvalidRequestIdException {
        if (request.getVehicle() == null) {
            throw new InvalidRequestException();
        }
        if (request.getSeller() == null) {
            throw new InvalidRequestException();
        }
        if (request.getSeller().getRole() != RolesEnum.CLIENT) {
            throw new InvalidRequestException();
        }
        Optional<RequestEntity> existingRequestEntity = requestRepository.findById(request.getId());
        if (!existingRequestEntity.isPresent()) {
            throw new InvalidRequestIdException();
        }
        RequestEntity requestEntity = RequestConverter.convertToEntity(request);
        requestRepository.save(requestEntity);
    }

    //@Transactional
    @Override
    public Long acceptRequest(Long requestId) throws InvalidRequestIdException, InvalidRequestException {
        Optional<RequestEntity> optionalRequestEntity = requestRepository.findById(requestId);
        if (!optionalRequestEntity.isPresent()) {
            throw new InvalidRequestIdException();
        }

        RequestEntity requestEntity = optionalRequestEntity.get();
        Request request = RequestConverter.convertToDomain(requestEntity);
        ListingEntity listingEntity = ListingConverter.convertToEntity(request.accept());

        ListingEntity saveListing = listingRepository.save(listingEntity);


        requestRepository.save(RequestConverter.convertToEntity(request));

        return saveListing.getId();
    }

    //@Transactional
    @Override
    public void declineRequest(Long requestId) throws InvalidRequestIdException, InvalidRequestException {
        Optional<RequestEntity> optionalRequestEntity = requestRepository.findById(requestId);
        if (!optionalRequestEntity.isPresent()) {
            throw new InvalidRequestIdException();
        }

        RequestEntity requestEntity = optionalRequestEntity.get();
        if (requestEntity.getStatus() != RequestEnum.PENDING) {
            throw new InvalidRequestException();
        }

        requestEntity.setStatus(RequestEnum.UNACCEPTED);
        requestRepository.save(requestEntity);
    }

    @Override
    public List<Request> getAllRequests() {
        return requestRepository.findAll()
                .stream()
                .map(RequestConverter::convertToDomain)
                .toList();
    }

    @Override
    public List<Request> getAllPendingRequests() {
        return requestRepository.findRequestsByStatusPending()
                .stream()
                .map(RequestConverter::convertToDomain)
                .toList();
    }

    @Override
    public List<Request> getAllRequestsByUser(Long userId) {
        return requestRepository.findRequestBySellerId(userId)
                .stream()
                .map(RequestConverter::convertToDomain)
                .toList();
    }

    @Override
    public List<Request> getPendingRequestsBySellerId(Long sellerId) {
        return requestRepository.findPendingRequestsBySellerId(sellerId)
                .stream()
                .map(RequestConverter::convertToDomain)
                .toList();
    }

    // New method implementation
    @Override
    public List<Request> getAllPendingRequestsWithEndAfterNow() {
        return requestRepository.findPendingRequestsWithEndAfterNow()
                .stream()
                .map(RequestConverter::convertToDomain)
                .toList();
    }

    @Override
    public List<Request> getPendingRequestsByVehicleType(VehicleTypeEnum vehicleType) {
        return requestRepository.findPendingRequestsByVehicleType(vehicleType)
                .stream()
                .map(RequestConverter::convertToDomain)
                .toList();
    }
}
