package backend.persistance.entity;

import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Entity
@Table(name = "notifications")
@Data
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class NotificationEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Long id;

    @NotNull(message = "Message cannot be null!")
    @Column(name = "message")
    private String message;

    @NotNull(message = "Recipient cannot be null!")
    @Column(name = "recipient")
    private String recipient;

    @NotNull(message = "Seen cannot be null!")
    @Column(name = "seen")
    private boolean seen;

    @NotNull(message = "Listing cannot be null!")
    @ManyToOne
    @JoinColumn(name = "listing_id")
    private ListingEntity listingEntity;
}
