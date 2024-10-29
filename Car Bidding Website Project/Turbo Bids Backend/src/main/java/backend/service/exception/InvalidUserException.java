package backend.service.exception;

public class InvalidUserException extends Exception {
    public InvalidUserException() {
        super("Invalid user.");
    }
}