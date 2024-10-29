package backend.service.exception;

public class InvalidUserIdException extends Exception {
    public InvalidUserIdException() {
        super("Invalid user ID.");
    }
}