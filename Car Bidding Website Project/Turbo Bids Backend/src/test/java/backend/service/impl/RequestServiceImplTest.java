package backend.service.impl;

import backend.persistance.ListingRepository;
import backend.persistance.RequestRepository;
import backend.persistance.entity.ListingEntity;
import backend.persistance.entity.RequestEntity;
import backend.service.converters.RequestConverter;
import backend.service.domain.Request;
import backend.service.domain.User;
import backend.service.domain.Vehicle;
import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.RequestEnum;
import backend.service.domain.enums.RolesEnum;
import backend.service.domain.enums.VehicleTypeEnum;
import backend.service.exception.InvalidRequestException;
import backend.service.exception.InvalidRequestIdException;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;
import java.util.Arrays;
import java.util.List;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

class RequestServiceImplTest {

    @Mock
    private RequestRepository requestRepository;

    @Mock
    private ListingRepository listingRepository;

    @InjectMocks
    private RequestServiceImpl requestService;

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

    private Vehicle createVehicle(Long id, String model) {
        return Vehicle.builder()
                .id(id)
                .model(model)
                .maker(MakerEnum.TESLA)
                .type(VehicleTypeEnum.SUV)
                .build();
    }

    @BeforeEach
    public void setUp() {
        MockitoAnnotations.openMocks(this);
    }

    @Test
    void testCreateRequestSuccess() throws InvalidRequestException {
        User seller = createUser(1L, RolesEnum.CLIENT);
        Vehicle vehicle = createVehicle(1L, "Model X");
        Request request = Request.builder()
                .vehicle(vehicle)
                .seller(seller)
                .start(LocalDateTime.now())
                .end(LocalDateTime.now().plusDays(1))
                .build();

        RequestEntity requestEntity = RequestConverter.convertToEntity(request);
        requestEntity.setId(1L);

        when(requestRepository.save(any(RequestEntity.class))).thenReturn(requestEntity);

        Long requestId = requestService.createRequest(request);
        assertEquals(1L, requestId);
    }

    @Test
    void testCreateRequestThrowsInvalidRequestException() {
        User seller = createUser(1L, RolesEnum.CLIENT);
        Request request = Request.builder()
                .seller(seller)
                .build();

        assertThrows(InvalidRequestException.class, () -> requestService.createRequest(request));

        request.setVehicle(createVehicle(1L, "Model X"));
        request.setSeller(createUser(1L, RolesEnum.MANAGER));
        assertThrows(InvalidRequestException.class, () -> requestService.createRequest(request));

        request.setSeller(null);
        assertThrows(InvalidRequestException.class, () -> requestService.createRequest(request));
    }

    @Test
    void testDeleteRequest() {
        requestService.deleteRequest(1L);
        verify(requestRepository, times(1)).deleteById(1L);
    }

    @Test
    void testGetRequest() {
        Vehicle vehicle = createVehicle(1L, "Model X");
        User seller = createUser(1L, RolesEnum.CLIENT);
        Request request = Request.builder()
                .id(1L)
                .vehicle(vehicle)
                .seller(seller)
                .start(LocalDateTime.now())
                .end(LocalDateTime.now().plusDays(1))
                .build();
        RequestEntity requestEntity = RequestConverter.convertToEntity(request);

        when(requestRepository.findById(1L)).thenReturn(Optional.of(requestEntity));

        Optional<Request> foundRequest = requestService.getRequest(1L);
        assertTrue(foundRequest.isPresent());
        assertEquals("Model X", foundRequest.get().getVehicle().getModel());
    }

    @Test
    void testGetRequestNotFound() {
        when(requestRepository.findById(1L)).thenReturn(Optional.empty());

        Optional<Request> request = requestService.getRequest(1L);
        assertFalse(request.isPresent());
    }

    @Test
    void testUpdateRequestSuccess() throws InvalidRequestException, InvalidRequestIdException {
        User seller = createUser(1L, RolesEnum.CLIENT);
        Vehicle vehicle = createVehicle(1L, "Model X");
        Request request = Request.builder()
                .id(1L)
                .vehicle(vehicle)
                .seller(seller)
                .start(LocalDateTime.now())
                .end(LocalDateTime.now().plusDays(1))
                .build();

        RequestEntity requestEntity = RequestConverter.convertToEntity(request);

        when(requestRepository.findById(1L)).thenReturn(Optional.of(requestEntity));
        when(requestRepository.save(any(RequestEntity.class))).thenReturn(requestEntity);

        requestService.updateRequest(request);
        verify(requestRepository, times(1)).save(any(RequestEntity.class));
    }

    @Test
    void testUpdateRequestThrowsInvalidRequestIdException() {
        User seller = createUser(1L, RolesEnum.CLIENT);
        Vehicle vehicle = createVehicle(1L, "Model X");
        Request request = Request.builder()
                .id(1L)
                .vehicle(vehicle)
                .seller(seller)
                .start(LocalDateTime.now())
                .end(LocalDateTime.now().plusDays(1))
                .build();

        when(requestRepository.findById(1L)).thenReturn(Optional.empty());

        assertThrows(InvalidRequestIdException.class, () -> requestService.updateRequest(request));
    }

    @Test
    void testUpdateRequestThrowsInvalidRequestException() {
        User seller = createUser(1L, RolesEnum.CLIENT);
        Request request = Request.builder()
                .id(1L)
                .vehicle(null)
                .seller(seller)
                .start(LocalDateTime.now())
                .end(LocalDateTime.now().plusDays(1))
                .build();

        RequestEntity requestEntity = RequestConverter.convertToEntity(request);
        when(requestRepository.findById(1L)).thenReturn(Optional.of(requestEntity));

        assertThrows(InvalidRequestException.class, () -> requestService.updateRequest(request));
    }

    @Transactional
    @Test
    void testAcceptRequestSuccess() throws InvalidRequestIdException, InvalidRequestException {
        User seller = createUser(1L, RolesEnum.CLIENT);
        Vehicle vehicle = createVehicle(1L, "Model X");
        Request request = Request.builder()
                .id(1L)
                .vehicle(vehicle)
                .seller(seller)
                .status(RequestEnum.PENDING)
                .start(LocalDateTime.now())
                .end(LocalDateTime.now().plusDays(1))
                .build();
        RequestEntity requestEntity = RequestConverter.convertToEntity(request);

        when(requestRepository.findById(1L)).thenReturn(Optional.of(requestEntity));
        when(listingRepository.save(any(ListingEntity.class))).thenAnswer(invocation -> {
            ListingEntity listingEntity = invocation.getArgument(0);
            listingEntity.setId(1L);  // Simulate ID generation
            return listingEntity;
        });
        when(requestRepository.save(any(RequestEntity.class))).thenReturn(requestEntity);

        Long listingId = requestService.acceptRequest(1L);
        assertNotNull(listingId);
        assertEquals(1L, listingId);
    }

    @Test
    void testAcceptRequestThrowsInvalidRequestIdException() {
        when(requestRepository.findById(1L)).thenReturn(Optional.empty());

        assertThrows(InvalidRequestIdException.class, () -> requestService.acceptRequest(1L));
    }

    @Transactional
    @Test
    void testDeclineRequestSuccess() throws InvalidRequestIdException, InvalidRequestException {
        User seller = createUser(1L, RolesEnum.CLIENT);
        Vehicle vehicle = createVehicle(1L, "Model X");
        Request request = Request.builder()
                .id(1L)
                .vehicle(vehicle)
                .seller(seller)
                .status(RequestEnum.PENDING)
                .start(LocalDateTime.now())
                .end(LocalDateTime.now().plusDays(1))
                .build();
        RequestEntity requestEntity = RequestConverter.convertToEntity(request);

        when(requestRepository.findById(1L)).thenReturn(Optional.of(requestEntity));
        when(requestRepository.save(any(RequestEntity.class))).thenReturn(requestEntity);

        requestService.declineRequest(1L);
        verify(requestRepository, times(1)).save(any(RequestEntity.class));
    }

    @Test
    void testDeclineRequestThrowsInvalidRequestIdException() {
        when(requestRepository.findById(1L)).thenReturn(Optional.empty());

        assertThrows(InvalidRequestIdException.class, () -> requestService.declineRequest(1L));
    }

    @Test
    void testDeclineRequestThrowsInvalidRequestException() {
        User seller = createUser(1L, RolesEnum.CLIENT);
        Vehicle vehicle = createVehicle(1L, "Model X");
        Request request = Request.builder()
                .id(1L)
                .vehicle(vehicle)
                .seller(seller)
                .status(RequestEnum.ACCEPTED)
                .start(LocalDateTime.now())
                .end(LocalDateTime.now().plusDays(1))
                .build();
        RequestEntity requestEntity = RequestConverter.convertToEntity(request);

        when(requestRepository.findById(1L)).thenReturn(Optional.of(requestEntity));

        assertThrows(InvalidRequestException.class, () -> requestService.declineRequest(1L));
    }

    @Test
    void testGetAllRequests() {
        User seller = createUser(1L, RolesEnum.CLIENT);
        Vehicle vehicle = createVehicle(1L, "Model X");
        Request request = Request.builder()
                .id(1L)
                .vehicle(vehicle)
                .seller(seller)
                .start(LocalDateTime.now())
                .end(LocalDateTime.now().plusDays(1))
                .build();
        RequestEntity requestEntity = RequestConverter.convertToEntity(request);

        when(requestRepository.findAll()).thenReturn(Arrays.asList(requestEntity));

        List<Request> requests = requestService.getAllRequests();
        assertEquals(1, requests.size());
        assertEquals("Model X", requests.get(0).getVehicle().getModel());
    }

    @Test
    void testGetAllPendingRequests() {
        User seller = createUser(1L, RolesEnum.CLIENT);
        Vehicle vehicle = createVehicle(1L, "Model X");
        Request request = Request.builder()
                .id(1L)
                .vehicle(vehicle)
                .seller(seller)
                .status(RequestEnum.PENDING)
                .start(LocalDateTime.now())
                .end(LocalDateTime.now().plusDays(1))
                .build();
        RequestEntity requestEntity = RequestConverter.convertToEntity(request);

        when(requestRepository.findRequestsByStatusPending()).thenReturn(Arrays.asList(requestEntity));

        List<Request> requests = requestService.getAllPendingRequests();
        assertEquals(1, requests.size());
        assertEquals("Model X", requests.get(0).getVehicle().getModel());
    }

    @Test
    void testGetAllRequestsByUser() {
        User seller = createUser(1L, RolesEnum.CLIENT);
        Vehicle vehicle = createVehicle(1L, "Model X");
        Request request = Request.builder()
                .id(1L)
                .vehicle(vehicle)
                .seller(seller)
                .start(LocalDateTime.now())
                .end(LocalDateTime.now().plusDays(1))
                .build();
        RequestEntity requestEntity = RequestConverter.convertToEntity(request);

        when(requestRepository.findRequestBySellerId(1L)).thenReturn(Arrays.asList(requestEntity));

        List<Request> requests = requestService.getAllRequestsByUser(1L);
        assertEquals(1, requests.size());
        assertEquals("Model X", requests.get(0).getVehicle().getModel());
    }
}
