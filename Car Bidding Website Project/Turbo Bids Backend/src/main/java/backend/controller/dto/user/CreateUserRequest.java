package backend.controller.dto.user;

import jakarta.validation.constraints.NotBlank;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class CreateUserRequest {


    @NotBlank
    private String lastName;

    @NotBlank
    private String firstName;

    @NotBlank
    private String email;

    @NotBlank
    private String password;

    @NotBlank
    private String role;
}
