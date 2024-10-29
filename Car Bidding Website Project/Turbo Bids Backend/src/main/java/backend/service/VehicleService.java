package backend.service;

import backend.service.domain.Vehicle;
import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.VehicleTypeEnum;
import backend.service.exception.InvalidVehicleException;
import backend.service.exception.InvalidVehicleIdException;

import java.util.List;
import java.util.Optional;

public interface VehicleService {

    Long createVehicle(Vehicle vehicle) throws InvalidVehicleException;

    void deleteVehicle(long vehicleId);

    List<Vehicle> getVehicles();

    Optional<Vehicle> getVehicle(long vehicleId);

    void updateVehicle(Vehicle vehicle) throws InvalidVehicleException, InvalidVehicleIdException;

    Optional<List<Vehicle>> filterVehiclesByMaker(MakerEnum maker);

    Optional<List<Vehicle>> filterVehiclesByType(VehicleTypeEnum type);

    Optional<Vehicle> getVehicleByMakerAndModel(MakerEnum maker, String model);
}
