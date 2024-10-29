package backend.controller;

import backend.controller.converters.RequestControllerConverter;
import backend.controller.dto.listing.CreateListingResponse;
import backend.controller.dto.request.*;
import backend.service.RequestService;
import backend.service.UserService;
import backend.service.VehicleService;
import backend.service.domain.Request;
import backend.service.domain.User;
import backend.service.domain.Vehicle;
import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.RequestEnum;
import backend.service.domain.enums.VehicleTypeEnum;
import backend.service.exception.InvalidRequestException;
import backend.service.exception.InvalidRequestIdException;
import jakarta.annotation.security.RolesAllowed;
import jakarta.validation.Valid;
import lombok.AllArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@RestController
@AllArgsConstructor
@RequestMapping("/requests")
@CrossOrigin(origins = "http://localhost:5173")
public class RequestController {

    private final RequestService requestService;
    private final UserService userService;
    private final VehicleService vehicleService;

    @RolesAllowed({"EMPLOYEE"})
    @GetMapping
    public ResponseEntity<GetRequestsResponse> getRequests() {
        List<Request> requestList = requestService.getAllRequests();
        GetRequestsResponse response = RequestControllerConverter.getRequestResponseFromDomain(requestList);
        return ResponseEntity.ok(response);
    }

    @RolesAllowed({"EMPLOYEE"})
    @GetMapping("/{requestId}")
    public ResponseEntity<GetSingleRequestResponse> getRequestById(@PathVariable long requestId) {
        Request request = requestService.getRequest(requestId).orElseThrow();
        GetSingleRequestResponse response = RequestControllerConverter.convertToGetSingleRequestResponse(request);
        return ResponseEntity.ok(response);
    }

    @RolesAllowed({"CLIENT"})
    @DeleteMapping("/{requestId}")
    public ResponseEntity<Void> deleteRequest(@PathVariable long requestId) {
        requestService.deleteRequest(requestId);
        return ResponseEntity.noContent().build();
    }

    @RolesAllowed({"EMPLOYEE"})
    @PostMapping
    public ResponseEntity<Object> createRequest(@RequestBody @Valid CreateRequestRequest request) {
        Request request1 = RequestControllerConverter.convertFromCreateRequestRequest(request);
        Optional<User> user = userService.getUser(request.getSellerId());
        Optional<Vehicle> vehicle = vehicleService.getVehicle(request.getVehicleId());

        try {
            request1.setSeller(user.orElseThrow());
            request1.setVehicle(vehicle.orElseThrow());
            Long requestId = requestService.createRequest(request1);
            request1.setId(requestId);
            CreateRequestResponse response = CreateRequestResponse.builder().requestId(requestId).build();
            return ResponseEntity.status(HttpStatus.CREATED).body(response);
        } catch (InvalidRequestException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage());
        }
    }

    @RolesAllowed({"CLIENT"})
    @PutMapping("/{requestId}")
    public ResponseEntity<Void> updateRequest(@PathVariable long requestId, @RequestBody @Valid UpdateRequestRequest request) {
        try {
            Request updatedRequest = RequestControllerConverter.convertFromUpdateRequest(request);
            updatedRequest.setId(requestId);

            Optional<User> user = userService.getUser(request.getSellerId());
            MakerEnum maker = MakerEnum.fromString(request.getMaker());
            Optional<Vehicle> vehicle = vehicleService.getVehicleByMakerAndModel(maker, request.getModel());

            updatedRequest.setSeller(user.orElseThrow());
            updatedRequest.setVehicle(vehicle.orElseThrow());

            requestService.updateRequest(updatedRequest);
            return ResponseEntity.ok().build();
        } catch (InvalidRequestException | InvalidRequestIdException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).build();
        }
    }

    @RolesAllowed({"EMPLOYEE"})
    @PostMapping("/accept/{requestId}")
    public ResponseEntity<Object> acceptRequest(@PathVariable long requestId) {
        try {
            Long listingId = requestService.acceptRequest(requestId);
            CreateListingResponse response = CreateListingResponse.builder().listingId(listingId).build();
            return ResponseEntity.status(HttpStatus.CREATED).body(response);
        } catch (InvalidRequestIdException | InvalidRequestException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).build();
        }
    }

    @RolesAllowed({"EMPLOYEE"})
    @PutMapping("/decline/{requestId}")
    public ResponseEntity<Void> declineRequest(@PathVariable long requestId) {
        try {
            requestService.declineRequest(requestId);
            return ResponseEntity.ok().build();
        } catch (InvalidRequestIdException | InvalidRequestException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).build();
        }
    }

    @RolesAllowed({"EMPLOYEE"})
    @GetMapping("/status/pending")
    public ResponseEntity<GetRequestsResponse> getPendingRequests() {
        List<Request> pendingRequests = requestService.getAllPendingRequests();
        GetRequestsResponse response = RequestControllerConverter.getRequestResponseFromDomain(pendingRequests);
        return ResponseEntity.ok(response);
    }

    @RolesAllowed({"EMPLOYEE"})
    @GetMapping("/status/pending/current")
    public ResponseEntity<List<GetSingleRequestResponse>> getPendingRequestsWithEndAfterNow() {
        List<Request> pendingRequests = requestService.getAllPendingRequestsWithEndAfterNow();
        List<GetSingleRequestResponse> response = pendingRequests.stream()
                .map(RequestControllerConverter::convertToGetSingleRequestResponse)
                .toList();
        return ResponseEntity.ok(response);
    }

    @RolesAllowed({"CLIENT"})
    @GetMapping("/user/{userId}")
    public ResponseEntity<GetRequestsResponse> getRequestsByUserId(@PathVariable long userId) {
        List<Request> userRequests = requestService.getAllRequestsByUser(userId);
        GetRequestsResponse response = RequestControllerConverter.getRequestResponseFromDomain(userRequests);
        return ResponseEntity.ok(response);
    }

    @RolesAllowed({"CLIENT"})
    @PostMapping("/webPost")
    public ResponseEntity<Object> createRequestFromWebsite(@RequestBody @Valid CreateRequestFromWebRequest request) {
        Request request1 = RequestControllerConverter.convertFromCreateRequestFromWebRequest(request);
        Optional<User> user = userService.getUser(request.getSellerId());
        MakerEnum maker = MakerEnum.fromString(request.getMaker());
        Optional<Vehicle> vehicle = vehicleService.getVehicleByMakerAndModel(maker, request.getModel());

        try {
            request1.setSeller(user.orElseThrow());
            request1.setVehicle(vehicle.orElseThrow());
            request1.setStatus(RequestEnum.PENDING);
            Long requestId = requestService.createRequest(request1);
            request1.setId(requestId);
            CreateRequestResponse response = CreateRequestResponse.builder().requestId(requestId).build();
            return ResponseEntity.status(HttpStatus.CREATED).body(response);
        } catch (InvalidRequestException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage());
        }
    }

    @RolesAllowed({"CLIENT"})
    @GetMapping("/seller/{sellerId}/pending")
    public ResponseEntity<List<GetSingleRequestResponse>> getPendingRequestsBySellerId(@PathVariable Long sellerId) {
        List<Request> requests = requestService.getPendingRequestsBySellerId(sellerId);
        List<GetSingleRequestResponse> response = requests.stream()
                .map(RequestControllerConverter::convertToGetSingleRequestResponse)
                .toList();
        return ResponseEntity.ok(response);
    }

    @RolesAllowed({"EMPLOYEE"})
    @GetMapping("/status/pending/vehicleType")
    public ResponseEntity<GetRequestsResponse> getPendingRequestsByVehicleType(@RequestParam VehicleTypeEnum vehicleType) {
        List<Request> pendingRequests = requestService.getPendingRequestsByVehicleType(vehicleType);
        GetRequestsResponse response = RequestControllerConverter.getRequestResponseFromDomain(pendingRequests);
        return ResponseEntity.ok(response);
    }

}