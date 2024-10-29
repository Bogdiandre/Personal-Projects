package backend.configuration.security.token;

public interface AccessToken {
    String getSubject();

    Long getUserId();

    String getFirstName();

    String getLastName();

    String getRole();

    boolean hasRole(String roleName);
}
