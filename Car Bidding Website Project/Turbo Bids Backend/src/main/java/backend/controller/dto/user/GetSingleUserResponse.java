package backend.controller.dto.user;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;

@Data
@Builder
@AllArgsConstructor
public class GetSingleUserResponse {
    private Long id;
    private String lastName;
    private String firstName;
    private String password;
    private String email;
    private String role;
}
