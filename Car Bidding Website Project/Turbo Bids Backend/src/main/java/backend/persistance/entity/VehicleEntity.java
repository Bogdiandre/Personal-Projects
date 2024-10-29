package backend.persistance.entity;

import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.VehicleTypeEnum;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotBlank;
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
@Table(name = "vehicles", uniqueConstraints = {
        @UniqueConstraint(columnNames = {"maker", "model"})
})
public class VehicleEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Long id;

    @NotNull(message = "Maker cannot be null!")
    @Enumerated(EnumType.STRING)
    @Column(name = "maker")
    private MakerEnum maker;

    @NotBlank(message = "Model cannot be blank!")
    @Column(name = "model", unique = true)
    private String model;

    @NotNull(message = "Type cannot be null!")
    @Enumerated(EnumType.STRING)
    @Column(name = "type")
    private VehicleTypeEnum type;

//    @OneToMany(mappedBy = "vehicleEntity", cascade = CascadeType.ALL, orphanRemoval = true)
//    private Set<RequestEntity> requests;
}
