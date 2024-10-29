package backend.persistance.entity;

import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Builder
@Data
@AllArgsConstructor
@NoArgsConstructor
@Entity
@Table(name = "bids")
public class BidEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Long id;

    @ManyToOne
    @NotNull(message = "Account cannot be null!")
    @JoinColumn(name = "account_id", referencedColumnName = "id")
    private UserEntity accountEntity;

    @ManyToOne
    @NotNull(message = "Listing cannot be null!")
    @JoinColumn(name = "listing_id", referencedColumnName = "id")
    private ListingEntity listingEntity;

    @NotNull(message = "Amount cannot be null!")
    @Column(name = "amount")
    private double amount;
}
