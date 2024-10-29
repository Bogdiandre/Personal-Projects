package backend.service.exception;

public class InvalidRoleException extends Exception {

    public InvalidRoleException(){ super("The role is invalid!");}
}
