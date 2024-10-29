package backend.controller.converters;

import backend.controller.dto.request.*;
import backend.persistance.UsersRepository;
import backend.persistance.VehicleRepository;
import backend.persistance.entity.UserEntity;
import backend.persistance.entity.VehicleEntity;
import backend.service.converters.UserConverter;
import backend.service.converters.VehicleConverter;
import backend.service.domain.Request;
import backend.service.domain.enums.RequestEnum;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;

@Component
public class RequestControllerConverter {

    private static UsersRepository usersRepository;
    private static VehicleRepository vehicleRepository;

    private RequestControllerConverter() {}

    public static GetRequestsResponse getRequestResponseFromDomain(List<Request> requestList) {
        List<GetSingleRequestResponse> responseRequestList = new ArrayList<>();

        for (Request request : requestList) {
            GetSingleRequestResponse responseRequest = GetSingleRequestResponse.builder()
                    .id(request.getId())
                    .details(request.getDetails())
                    .vehicle(VehicleControllerConverter.getSingleVehicleResponse(request.getVehicle()))
                    .sellerId(UserControllerConverter.getSingleUserResponseFromDomain(request.getSeller()))
                    .end(request.getEnd())
                    .start(request.getStart())
                    .status(request.getStatus().toString())
                    .milage(request.getMilage())
                    .maxPrice(request.getMaxPrice())
                    .build();

            responseRequestList.add(responseRequest);
        }

        return GetRequestsResponse.builder()
                .requests(responseRequestList)
                .build();
    }

    public static Request convertFromCreateRequestRequest(CreateRequestRequest request) {
        UserEntity user = usersRepository.findById(request.getSellerId()).orElseThrow();
        VehicleEntity vehicle = vehicleRepository.findById(request.getVehicleId()).orElseThrow();
        return Request.builder()
                .seller(UserConverter.convertToDomain(user))
                .vehicle(VehicleConverter.convertToDomain(vehicle))
                .end(request.getEnd())
                .start(request.getStart())
                .details(request.getDetails())
                .milage(request.getMilage())
                .status(request.getStatus())
                .maxPrice(request.getMaxPrice())
                .build();
    }

    public static Request convertFromCreateRequestFromWebRequest(CreateRequestFromWebRequest request) {

        return Request.builder()
                .end(request.getEnd())
                .start(request.getStart())
                .details(request.getDetails())
                .milage(request.getMilage())
                .maxPrice(request.getMaxPrice())
                .build();
    }

    public static GetSingleRequestResponse convertToGetSingleRequestResponse(Request request) {
        return GetSingleRequestResponse.builder()
                .id(request.getId())
                .vehicle(VehicleControllerConverter.getSingleVehicleResponse(request.getVehicle()))
                .sellerId(UserControllerConverter.getSingleUserResponseFromDomain(request.getSeller()))
                .milage(request.getMilage())
                .details(request.getDetails())
                .status(request.getStatus().toString())
                .start(request.getStart())
                .end(request.getEnd())
                .maxPrice(request.getMaxPrice())
                .build();
    }

    public static Request convertFromUpdateRequest(UpdateRequestRequest request) {

        return Request.builder()

                .end(request.getEnd())
                .start(request.getStart())
                .details(request.getDetails())
                .status(RequestEnum.PENDING)
                .milage(request.getMilage())
                .maxPrice(request.getMaxPrice())
                .build();
    }
}
