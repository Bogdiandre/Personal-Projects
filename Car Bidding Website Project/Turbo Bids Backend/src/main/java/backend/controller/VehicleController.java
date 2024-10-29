package backend.controller;

import backend.controller.converters.VehicleControllerConverter;
import backend.controller.dto.vehicle.CreateVehicleRequest;
import backend.controller.dto.vehicle.CreateVehicleResponse;
import backend.controller.dto.vehicle.GetSingleVehicleResponse;
import backend.controller.dto.vehicle.GetVehiclesResponse;
import backend.service.VehicleService;
import backend.service.domain.Vehicle;
import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.VehicleTypeEnum;
import backend.service.exception.InvalidVehicleException;
import backend.service.exception.InvalidVehicleIdException;
import jakarta.annotation.security.RolesAllowed;
import jakarta.validation.Valid;
import lombok.AllArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;


@RestController
@AllArgsConstructor
@RequestMapping("/vehicles")
@CrossOrigin("http://TurboBids_db_staging:5173/")
public class VehicleController {

    private final VehicleService vehicleService;

    @RolesAllowed("EMPLOYEE")
    @GetMapping
    public ResponseEntity<GetVehiclesResponse> getVehicles() {
        List<Vehicle> vehiclesList = vehicleService.getVehicles();
        GetVehiclesResponse response = VehicleControllerConverter.getVehiclesResponseFromDomain(vehiclesList);
        return ResponseEntity.ok(response);
    }
    @RolesAllowed("EMPLOYEE")
    @DeleteMapping("/{vehicleId}")
    public ResponseEntity<Void> deleteVehicle(@PathVariable long vehicleId) {
        vehicleService.deleteVehicle(vehicleId);
        return ResponseEntity.noContent().build();
    }

    @GetMapping("/{vehicleId}")
    public ResponseEntity<GetSingleVehicleResponse> getVehicleById(@PathVariable long vehicleId) throws InvalidVehicleIdException {
        Vehicle vehicle = vehicleService.getVehicle(vehicleId)
                .orElseThrow(() -> new InvalidVehicleIdException());
        GetSingleVehicleResponse response = VehicleControllerConverter.getSingleVehicleResponse(vehicle);
        return ResponseEntity.ok(response);
    }
    @RolesAllowed("EMPLOYEE")
    @PostMapping()
    public ResponseEntity<Object> createVehicle(@RequestBody @Valid CreateVehicleRequest request) {
        Vehicle vehicle = VehicleControllerConverter.convertFromCreateVehicleRequest(request);
        try {
            Long vehicleId = vehicleService.createVehicle(vehicle);
            CreateVehicleResponse response = CreateVehicleResponse.builder().vehicleId(vehicleId).build();
            return ResponseEntity.status(HttpStatus.CREATED).body(response);
        } catch (InvalidVehicleException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage());
        }
    }

    @PostMapping("/bulk")
    public ResponseEntity<Object> createVehicles(@RequestBody @Valid List<CreateVehicleRequest> requests) {
        try {
            for (CreateVehicleRequest request : requests) {
                Vehicle vehicle = VehicleControllerConverter.convertFromCreateVehicleRequest(request);
                vehicleService.createVehicle(vehicle);
            }
            return ResponseEntity.status(HttpStatus.CREATED).build();
        } catch (InvalidVehicleException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage());
        }
    }

    @GetMapping("/makers")
    public ResponseEntity<List<String>> getAllMakers() {
        List<String> makers = Arrays.stream(MakerEnum.values())
                .map(Enum::name)
                .toList();
        return ResponseEntity.ok(makers);
    }

    @GetMapping("/filterByType")
    public ResponseEntity<GetVehiclesResponse> getVehiclesByType(@RequestParam String type) {
        try {
            VehicleTypeEnum vehicleTypeEnum = VehicleTypeEnum.fromString(type);
            Optional<List<Vehicle>> vehicles = vehicleService.filterVehiclesByType(vehicleTypeEnum);
            return vehicles.map(vehicleList -> ResponseEntity.ok(VehicleControllerConverter.getVehiclesResponseFromDomain(vehicleList)))
                    .orElseGet(() -> ResponseEntity.status(HttpStatus.NOT_FOUND).body(null));
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(null);
        }
    }

    @GetMapping("/filterByMaker")
    public ResponseEntity<GetVehiclesResponse> getVehiclesByMaker(@RequestParam String maker) {
        try {
            MakerEnum makerEnum = MakerEnum.fromString(maker);
            Optional<List<Vehicle>> vehicles = vehicleService.filterVehiclesByMaker(makerEnum);
            return vehicles.map(vehicleList -> ResponseEntity.ok(VehicleControllerConverter.getVehiclesResponseFromDomain(vehicleList)))
                    .orElseGet(() -> ResponseEntity.status(HttpStatus.NOT_FOUND).body(null));
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(null);
        }
    }

    @GetMapping("/modelsByMaker")
    public ResponseEntity<List<String>> getModelsByMaker(@RequestParam String maker) {
        try {
            MakerEnum makerEnum = MakerEnum.fromString(maker);
            Optional<List<Vehicle>> vehicles = vehicleService.filterVehiclesByMaker(makerEnum);
            List<String> models = vehicles.orElseThrow().stream().map(Vehicle::getModel).toList();
            return ResponseEntity.ok(models);
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(null);
        }
    }

    @GetMapping("/vehicleTypes")
    public ResponseEntity<List<String>> getAllVehicleTypes() {
        List<String> types = Arrays.stream(VehicleTypeEnum.values())
                .map(Enum::name)
                .toList();
        return ResponseEntity.ok(types);
    }


}
