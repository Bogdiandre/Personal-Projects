package backend.controller.converters;

import backend.controller.dto.vehicle.CreateVehicleRequest;
import backend.controller.dto.vehicle.GetSingleVehicleResponse;
import backend.controller.dto.vehicle.GetVehiclesResponse;
import backend.service.domain.Vehicle;
import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.VehicleTypeEnum;

import java.util.ArrayList;
import java.util.List;

public class VehicleControllerConverter {

    private VehicleControllerConverter() {
    }

    public static GetSingleVehicleResponse getSingleVehicleResponse(Vehicle vehicle) {


        return GetSingleVehicleResponse.builder()
                .id(vehicle.getId())
                .model(vehicle.getModel())
                .maker(vehicle.getMaker().toString())
                .type(vehicle.getType().toString())
                .build();
    }

    public static GetVehiclesResponse getVehiclesResponseFromDomain(List<Vehicle> vehicleList) {
        List<GetSingleVehicleResponse> responseVehicleList = new ArrayList<>();

        for (Vehicle vehicle : vehicleList) {
            GetSingleVehicleResponse responseVehicle = GetSingleVehicleResponse.builder()
                    .id(vehicle.getId())
                    .model(vehicle.getModel())
                    .maker(vehicle.getMaker().toString())
                    .type(vehicle.getType().toString())
                    .build();

            responseVehicleList.add(responseVehicle);
        }

        return GetVehiclesResponse.builder()
                .vehicles(responseVehicleList)
                .build();
    }

    public static Vehicle convertFromCreateVehicleRequest(CreateVehicleRequest request) {
        MakerEnum makerEnum;
        VehicleTypeEnum vehicleTypeEnum;

        try {
            makerEnum = MakerEnum.fromString(request.getMaker());
        } catch (IllegalArgumentException e) {
            throw new RuntimeException("Invalid maker provided: " + request.getMaker());
        }

        try {
            vehicleTypeEnum = VehicleTypeEnum.fromString(request.getType());
        } catch (IllegalArgumentException e) {
            throw new RuntimeException("Invalid vehicle type provided: " + request.getType());
        }

        return Vehicle.builder()
                .model(request.getModel())
                .maker(makerEnum)
                .type(vehicleTypeEnum)
                .build();
    }

    public static List<String> getAllModelsResponseFromDomain(List<Vehicle> vehicleList) {
        List<String> responseModelList = new ArrayList<>();

        for (Vehicle vehicle : vehicleList) {
            responseModelList.add(vehicle.getModel());
        }

        return responseModelList;
    }
}
