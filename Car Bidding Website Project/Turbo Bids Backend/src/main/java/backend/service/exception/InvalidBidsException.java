package backend.service.exception;

public class InvalidBidsException extends Exception{

    public InvalidBidsException()
    {
        super("Bid cannot be placed!");
    }
}
