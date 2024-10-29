package backend.service.domain;

import backend.service.domain.enums.RolesEnum;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class User {
    private Long id;
    private String lastName;
    private String firstName;
    private String email;
    private String password;
    private RolesEnum role;
}
