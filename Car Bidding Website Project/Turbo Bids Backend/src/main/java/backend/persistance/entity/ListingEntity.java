package backend.persistance.entity;

import backend.service.domain.enums.ListingEnum;
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
@Table(name = "listings")
public class ListingEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Long id;

    @OneToOne
    @NotNull(message = "Request cannot be null!")
    @JoinColumn(name = "request_id")
    private RequestEntity requestEntity;

    @OneToOne
    @JoinColumn(name = "buyer_id")
    private UserEntity buyerEntity;

    @NotNull(message = "Status cannot be null!")
    @Enumerated(EnumType.STRING)
    @Column(name = "status")
    private ListingEnum status;

//    @OneToMany(mappedBy = "listingEntity", cascade = CascadeType.ALL, orphanRemoval = true)
//    private Set<BidEntity> bids;
//
//    @OneToMany(mappedBy = "listingEntity", cascade = CascadeType.ALL, orphanRemoval = true)
//    private Set<NotificationEntity> notifications;
//
}
