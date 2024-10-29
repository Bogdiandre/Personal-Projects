package backend.service.impl;

import backend.persistance.VehicleRepository;
import backend.persistance.entity.VehicleEntity;
import backend.service.VehicleService;
import backend.service.converters.VehicleConverter;
import backend.service.domain.Vehicle;
import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.VehicleTypeEnum;
import backend.service.exception.InvalidVehicleException;
import backend.service.exception.InvalidVehicleIdException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class VehicleServiceImpl implements VehicleService {

    private final VehicleRepository vehicleRepository;

    @Autowired
    public VehicleServiceImpl(VehicleRepository vehicleRepository) {
        this.vehicleRepository = vehicleRepository;
    }

    @Override
    public Long createVehicle(Vehicle vehicle) throws InvalidVehicleException {
        if (vehicle == null || vehicle.getModel() == null || vehicle.getMaker() == null || vehicle.getType() == null) {
            throw new InvalidVehicleException();
        }

        VehicleEntity vehicleEntity = VehicleConverter.convertToEntity(vehicle);
        VehicleEntity savedVehicle = vehicleRepository.save(vehicleEntity);
        return savedVehicle.getId();
    }

    @Override
    public void deleteVehicle(long vehicleId) {
        vehicleRepository.deleteById(vehicleId);
    }

    @Override
    public List<Vehicle> getVehicles() {
        return vehicleRepository.findAll().stream()
                .map(VehicleConverter::convertToDomain)
                .toList();
    }

    @Override
    public Optional<Vehicle> getVehicle(long vehicleId) {
        return vehicleRepository.findById(vehicleId)
                .map(VehicleConverter::convertToDomain);
    }

    @Override
    public void updateVehicle(Vehicle vehicle) throws InvalidVehicleException, InvalidVehicleIdException {
        if (vehicle.getModel() == null || vehicle.getMaker() == null || vehicle.getType() == null) {
            throw new InvalidVehicleException();
        }
        if (vehicle.getId() == null || vehicleRepository.findById(vehicle.getId()).isEmpty()) {
            throw new InvalidVehicleIdException();
        }
        vehicleRepository.save(VehicleConverter.convertToEntity(vehicle));
    }

    @Override
    public Optional<List<Vehicle>> filterVehiclesByMaker(MakerEnum maker) {
        return Optional.of(vehicleRepository.findByMakerNative(maker.toString()))
                .map(list -> list.stream()
                        .map(VehicleConverter::convertToDomain)
                        .toList());
    }

    @Override
    public Optional<List<Vehicle>> filterVehiclesByType(VehicleTypeEnum type) {
        return Optional.of(vehicleRepository.findByTypeNative(type.toString()))
                .map(list -> list.stream()
                        .map(VehicleConverter::convertToDomain)
                        .toList());
    }


    @Override
    public Optional<Vehicle> getVehicleByMakerAndModel(MakerEnum maker, String model) {
        return vehicleRepository.findByMakerAndModelNative(maker.toString(), model)
                .map(VehicleConverter::convertToDomain);
    }
}
