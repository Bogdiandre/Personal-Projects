package backend.service.converters;

import backend.persistance.entity.VehicleEntity;
import backend.service.domain.Vehicle;

public final class VehicleConverter {

    private VehicleConverter() {
        // Private constructor to prevent instantiation
    }

    public static Vehicle convertToDomain(VehicleEntity vehicleEntity) {
        if (vehicleEntity == null) {
            return null;
        }
        return Vehicle.builder()
                .id(vehicleEntity.getId())
                .model(vehicleEntity.getModel())
                .maker(vehicleEntity.getMaker())
                .type(vehicleEntity.getType())
                .build();
    }

    public static VehicleEntity convertToEntity(Vehicle vehicle) {
        if (vehicle == null) {
            return null;
        }
        return VehicleEntity.builder()
                .id(vehicle.getId())
                .model(vehicle.getModel())
                .maker(vehicle.getMaker())
                .type(vehicle.getType())
                .build();
    }
}
