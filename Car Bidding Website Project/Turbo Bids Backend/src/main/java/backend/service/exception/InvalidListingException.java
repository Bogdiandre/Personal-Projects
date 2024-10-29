package backend.service.exception;

public class InvalidListingException extends Exception{

    public InvalidListingException() {
        super("Invalid listing.");
    }
}
