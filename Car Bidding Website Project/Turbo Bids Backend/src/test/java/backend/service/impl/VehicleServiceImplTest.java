package backend.service.impl;

import backend.persistance.VehicleRepository;
import backend.persistance.entity.VehicleEntity;
import backend.service.converters.VehicleConverter;
import backend.service.domain.Vehicle;
import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.VehicleTypeEnum;
import backend.service.exception.InvalidVehicleException;
import backend.service.exception.InvalidVehicleIdException;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

class VehicleServiceImplTest {

    @Mock
    private VehicleRepository vehicleRepository;

    @InjectMocks
    private VehicleServiceImpl vehicleService;

    @BeforeEach
    public void setUp() {
        MockitoAnnotations.openMocks(this);
    }

    @Test
    void testCreateVehicleSuccess() throws InvalidVehicleException {
        // Arrange
        Vehicle vehicle = new Vehicle();
        vehicle.setModel("Model X");
        vehicle.setMaker(MakerEnum.TESLA);
        vehicle.setType(VehicleTypeEnum.SEDAN);

        VehicleEntity vehicleEntity = VehicleConverter.convertToEntity(vehicle);
        vehicleEntity.setId(1L);

        when(vehicleRepository.save(any(VehicleEntity.class))).thenReturn(vehicleEntity);

        // Act
        Long vehicleId = vehicleService.createVehicle(vehicle);

        // Assert
        assertEquals(1L, vehicleId);
    }

    @Test
    void testCreateVehicleThrowsInvalidVehicleException() {
        // Arrange
        Vehicle vehicle = new Vehicle();

        // Act & Assert
        assertThrows(InvalidVehicleException.class, () -> vehicleService.createVehicle(vehicle));
    }

    @Test
    void testDeleteVehicle() {
        // Act
        vehicleService.deleteVehicle(1L);

        // Assert
        verify(vehicleRepository, times(1)).deleteById(1L);
    }

    @Test
    void testGetVehicles() {
        // Arrange
        VehicleEntity vehicleEntity = new VehicleEntity();
        vehicleEntity.setId(1L);
        vehicleEntity.setModel("Model X");
        vehicleEntity.setMaker(MakerEnum.TESLA);
        vehicleEntity.setType(VehicleTypeEnum.SEDAN);

        when(vehicleRepository.findAll()).thenReturn(Arrays.asList(vehicleEntity));

        // Act
        List<Vehicle> vehicles = vehicleService.getVehicles();

        // Assert
        assertEquals(1, vehicles.size());
        assertEquals("Model X", vehicles.get(0).getModel());
    }

    @Test
    void testGetVehicle() {
        // Arrange
        VehicleEntity vehicleEntity = new VehicleEntity();
        vehicleEntity.setId(1L);
        vehicleEntity.setModel("Model X");

        when(vehicleRepository.findById(1L)).thenReturn(Optional.of(vehicleEntity));

        // Act
        Optional<Vehicle> vehicle = vehicleService.getVehicle(1L);

        // Assert
        assertTrue(vehicle.isPresent());
        assertEquals("Model X", vehicle.get().getModel());
    }

    @Test
    void testGetVehicleNotFound() {
        // Arrange
        when(vehicleRepository.findById(1L)).thenReturn(Optional.empty());

        // Act
        Optional<Vehicle> vehicle = vehicleService.getVehicle(1L);

        // Assert
        assertFalse(vehicle.isPresent());
    }

    @Test
    void testUpdateVehicleSuccess() throws InvalidVehicleException, InvalidVehicleIdException {
        // Arrange
        Vehicle vehicle = new Vehicle();
        vehicle.setId(1L);
        vehicle.setModel("Model S");
        vehicle.setMaker(MakerEnum.TESLA);
        vehicle.setType(VehicleTypeEnum.SEDAN);

        VehicleEntity vehicleEntity = VehicleConverter.convertToEntity(vehicle);

        when(vehicleRepository.findById(1L)).thenReturn(Optional.of(vehicleEntity));
        when(vehicleRepository.save(any(VehicleEntity.class))).thenReturn(vehicleEntity);

        // Act
        vehicleService.updateVehicle(vehicle);

        // Assert
        verify(vehicleRepository, times(1)).save(any(VehicleEntity.class));
    }

    @Test
    void testUpdateVehicleThrowsInvalidVehicleIdException() {
        // Arrange
        Vehicle vehicle = new Vehicle();
        vehicle.setId(1L);
        vehicle.setModel("Model S");
        vehicle.setMaker(MakerEnum.TESLA);
        vehicle.setType(VehicleTypeEnum.SEDAN);

        when(vehicleRepository.findById(1L)).thenReturn(Optional.empty());

        // Act & Assert
        assertThrows(InvalidVehicleIdException.class, () -> vehicleService.updateVehicle(vehicle));
    }

    @Test
    void testUpdateVehicleThrowsInvalidVehicleException() {
        // Arrange
        Vehicle vehicle = new Vehicle();
        vehicle.setId(1L);

        VehicleEntity vehicleEntity = new VehicleEntity();
        vehicleEntity.setId(1L);
        when(vehicleRepository.findById(1L)).thenReturn(Optional.of(vehicleEntity));

        // Act & Assert
        assertThrows(InvalidVehicleException.class, () -> vehicleService.updateVehicle(vehicle));
    }

    @Test
    void testFilterVehiclesByMaker() {
        // Arrange
        VehicleEntity vehicleEntity = new VehicleEntity();
        vehicleEntity.setId(1L);
        vehicleEntity.setModel("Model X");
        vehicleEntity.setMaker(MakerEnum.TESLA);

        when(vehicleRepository.findByMakerNative("TESLA")).thenReturn(Arrays.asList(vehicleEntity));

        // Act
        Optional<List<Vehicle>> vehicles = vehicleService.filterVehiclesByMaker(MakerEnum.TESLA);

        // Assert
        assertTrue(vehicles.isPresent());
        assertFalse(vehicles.get().isEmpty());
        assertEquals("Model X", vehicles.get().get(0).getModel());
    }

    @Test
    void testFilterVehiclesByType() {
        // Arrange
        VehicleEntity vehicleEntity = new VehicleEntity();
        vehicleEntity.setId(1L);
        vehicleEntity.setModel("Model X");
        vehicleEntity.setType(VehicleTypeEnum.SEDAN);

        when(vehicleRepository.findByTypeNative("SEDAN")).thenReturn(Arrays.asList(vehicleEntity));

        // Act
        Optional<List<Vehicle>> vehicles = vehicleService.filterVehiclesByType(VehicleTypeEnum.SEDAN);

        // Assert
        assertTrue(vehicles.isPresent());
        assertFalse(vehicles.get().isEmpty());
        assertEquals("Model X", vehicles.get().get(0).getModel());
    }

    @Test
    void testFilterVehiclesByMakerEmpty() {
        // Arrange
        when(vehicleRepository.findByMakerNative("TESLA")).thenReturn(List.of());

        // Act
        Optional<List<Vehicle>> vehicles = vehicleService.filterVehiclesByMaker(MakerEnum.TESLA);

        // Assert
        assertTrue(vehicles.isPresent());
        assertTrue(vehicles.get().isEmpty());
    }

    @Test
    void testFilterVehiclesByTypeEmpty() {
        // Arrange
        when(vehicleRepository.findByTypeNative("SEDAN")).thenReturn(List.of());

        // Act
        Optional<List<Vehicle>> vehicles = vehicleService.filterVehiclesByType(VehicleTypeEnum.SEDAN);

        // Assert
        assertTrue(vehicles.isPresent());
        assertTrue(vehicles.get().isEmpty());
    }

    @Test
    void testGetVehicleByMakerAndModel() {
        // Arrange
        VehicleEntity vehicleEntity = new VehicleEntity();
        vehicleEntity.setId(1L);
        vehicleEntity.setMaker(MakerEnum.TESLA);
        vehicleEntity.setModel("Model S");

        when(vehicleRepository.findByMakerAndModelNative("TESLA", "Model S")).thenReturn(Optional.of(vehicleEntity));

        // Act
        Optional<Vehicle> vehicle = vehicleService.getVehicleByMakerAndModel(MakerEnum.TESLA, "Model S");

        // Assert
        assertTrue(vehicle.isPresent());
        assertEquals("Model S", vehicle.get().getModel());
    }

    @Test
    void testGetVehicleByMakerAndModelNotFound() {
        // Arrange
        when(vehicleRepository.findByMakerAndModelNative("TESLA", "Model S")).thenReturn(Optional.empty());

        // Act
        Optional<Vehicle> vehicle = vehicleService.getVehicleByMakerAndModel(MakerEnum.TESLA, "Model S");

        // Assert
        assertFalse(vehicle.isPresent());
    }
}
