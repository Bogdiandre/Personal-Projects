package backend.persistance;

import backend.persistance.entity.NotificationEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface NotificationRepository extends JpaRepository<NotificationEntity, Long> {

    List<NotificationEntity> findByRecipient(String recipient);

    // Custom query to find all unseen notifications for a recipient
    @Query("SELECT n FROM NotificationEntity n WHERE n.recipient = :recipient AND n.seen = false")
    List<NotificationEntity> findUnseenNotificationsByRecipient(@Param("recipient") String recipient);
}
