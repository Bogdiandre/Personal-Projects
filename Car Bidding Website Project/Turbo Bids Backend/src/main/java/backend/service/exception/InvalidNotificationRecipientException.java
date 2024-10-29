package backend.service.exception;

public class InvalidNotificationRecipientException extends RuntimeException {
    public InvalidNotificationRecipientException(String message) {
        super(message);
    }
}