package backend.service.exception;

public class InvalidRequestException extends Exception {
    public InvalidRequestException() {
        super("Invalid request.");
    }
}
