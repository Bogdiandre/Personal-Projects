package backend.persistance.entity;

import backend.service.domain.enums.RequestEnum;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

@Builder
@Data
@AllArgsConstructor
@NoArgsConstructor
@Entity
@Table(name = "requests")
public class RequestEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Long id;

    @ManyToOne
    @NotNull(message = "Vehicle cannot be null!")
    @JoinColumn(name = "vehicle_id")
    private VehicleEntity vehicleEntity;

    @ManyToOne
    @NotNull(message = "Seller cannot be null!")
    @JoinColumn(name = "seller_id")
    private UserEntity sellerEntity;

    @NotNull(message = "Milage cannot be null!")
    @Column(name = "milage")
    private Integer milage;

    @NotNull(message = "Details cannot be null!")
    @Column(name = "details")
    private String details;

    @NotNull(message = "Status cannot be null!")
    @Enumerated(EnumType.STRING)
    @Column(name = "status")
    private RequestEnum status;

    @NotNull(message = "Start cannot be null!")
    @Column(name = "start")
    private LocalDateTime start;

    @NotNull(message = "End cannot be null!")
    @Column(name = "end")
    private LocalDateTime end;

    @NotNull(message = "Max price cannot be null!")
    @Column(name = "max_price")
    private Integer maxPrice;

//    @OneToOne(mappedBy = "requestEntity", cascade = CascadeType.ALL, orphanRemoval = true)
//    private ImageEntity image;
//
//    @OneToOne(mappedBy = "requestEntity", cascade = CascadeType.ALL, orphanRemoval = true)
//    private ListingEntity listing;
}
