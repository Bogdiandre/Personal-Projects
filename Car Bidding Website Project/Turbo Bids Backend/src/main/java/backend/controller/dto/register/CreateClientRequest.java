package backend.controller.dto.register;

import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotEmpty;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class CreateClientRequest {

    @NotEmpty(message = "Email is required")
    @Email(message = "Email should be valid")
    private String email;

    @NotEmpty(message = "Last name is required")
    private String lastName;

    @NotEmpty(message = "First name is required")
    private String firstName;

    @NotEmpty(message = "Password is required")
    private String password;
}
