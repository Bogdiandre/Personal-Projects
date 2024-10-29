package backend.configuration.security.token.impl;

import backend.configuration.security.token.AccessToken;
import lombok.EqualsAndHashCode;
import lombok.Getter;

@EqualsAndHashCode
@Getter
public class AccessTokenImpl implements AccessToken {
    private final String subject;
    private final Long userId;
    private final String role;
    private final String firstName;
    private final String lastName;

    public AccessTokenImpl(String subject, Long userId, String role, String firstName, String lastName) {
        this.subject = subject;
        this.userId = userId;
        this.role = role;
        this.firstName = firstName;
        this.lastName = lastName;
    }

    @Override
    public boolean hasRole(String roleName) {
        return this.role.equals(roleName);
    }
}
