package backend.service.exception;

public class InvalidVehicleException extends Exception {
    public InvalidVehicleException() {
        super("Invalid vehicle.");
    }
}

