package backend.persistance.entity;

import backend.service.domain.enums.RolesEnum;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.Set;

@Builder
@Data
@AllArgsConstructor
@NoArgsConstructor
@Entity
@Table(name = "users", uniqueConstraints = {
        @UniqueConstraint(columnNames = {"role", "email"})
})
public class UserEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Long id;

    @NotNull(message = "Last name cannot be null!")
    @Column(name = "last_name")
    private String lastName;

    @NotNull(message = "First name cannot be null!")
    @Column(name = "first_name")
    private String firstName;

    @NotNull(message = "Email cannot be null!")
    @Column(name = "email", unique = true)
    private String email;

    @NotNull(message = "Password cannot be null!")
    @Column(name = "password")
    private String password;

    @NotNull(message = "Role cannot be null!")
    @Enumerated(EnumType.STRING)
    @Column(name = "role")
    private RolesEnum role;

//    @OneToMany(mappedBy = "sellerEntity", cascade = CascadeType.ALL, orphanRemoval = true)
//    private Set<RequestEntity> requests;
}
