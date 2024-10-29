package backend.service.exception;

public class InvalidRequestIdException extends Exception {
    public InvalidRequestIdException() {
        super("Invalid request ID.");
    }
}

