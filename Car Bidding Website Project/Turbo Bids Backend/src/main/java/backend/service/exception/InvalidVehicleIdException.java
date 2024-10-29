package backend.service.exception;

public class InvalidVehicleIdException extends Exception {
    public InvalidVehicleIdException() {
        super("Invalid vehicle ID.");
    }
}

