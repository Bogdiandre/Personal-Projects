package backend.service.exception;

public class InvalidListingIdException extends Exception{
    public InvalidListingIdException() {
        super("Invalid listing ID.");
    }
}
