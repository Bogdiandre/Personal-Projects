package backend.service.converters;

import backend.persistance.entity.RequestEntity;
import backend.service.domain.Request;

public class RequestConverter {

    private RequestConverter(){

    }

    public static Request convertToDomain(RequestEntity requestEntity) {
        if (requestEntity == null) {
            return null;
        }
        return Request.builder()
                .id(requestEntity.getId())
                .vehicle(VehicleConverter.convertToDomain(requestEntity.getVehicleEntity()))
                .seller(UserConverter.convertToDomain(requestEntity.getSellerEntity()))
                .milage(requestEntity.getMilage())
                .details(requestEntity.getDetails())
                .status(requestEntity.getStatus())
                .start(requestEntity.getStart())
                .end(requestEntity.getEnd())
                .maxPrice(requestEntity.getMaxPrice())
                .build();
    }

    public static RequestEntity convertToEntity(Request request) {
        if (request == null) {
            return null;
        }
        return RequestEntity.builder()
                .id(request.getId())
                .vehicleEntity(VehicleConverter.convertToEntity(request.getVehicle()))
                .sellerEntity(UserConverter.convertToEntity(request.getSeller()))
                .milage(request.getMilage())
                .details(request.getDetails())
                .status(request.getStatus())
                .start(request.getStart())
                .end(request.getEnd())
                .maxPrice(request.getMaxPrice())
                .build();
    }
}
