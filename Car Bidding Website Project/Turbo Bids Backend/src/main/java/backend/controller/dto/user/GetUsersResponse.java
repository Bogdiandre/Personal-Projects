package backend.controller.dto.user;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;

import java.util.List;

@Data
@Builder
public class GetUsersResponse {
    private List<User> users;

    @Data
    @Builder
    @AllArgsConstructor
    public static class User {
        private Long id;
        private String lastName;
        private String firstName;
        private String email;
        private String password;
        private String role;
    }
}
