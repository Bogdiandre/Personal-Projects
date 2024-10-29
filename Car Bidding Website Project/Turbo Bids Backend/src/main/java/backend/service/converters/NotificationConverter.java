package backend.service.converters;

import backend.persistance.entity.NotificationEntity;
import backend.service.domain.Notification;

public class NotificationConverter {


    private NotificationConverter() {
    }

    public static Notification convertToDomain(NotificationEntity notificationEntity) {
        if (notificationEntity == null) {
            return null;
        }
        return Notification.builder()
                .id(notificationEntity.getId())
                .message(notificationEntity.getMessage())
                .recipient(notificationEntity.getRecipient())
                .seen(notificationEntity.isSeen())
                .listing(ListingConverter.convertToDomain(notificationEntity.getListingEntity()))
                .build();
    }

    public static NotificationEntity convertToEntity(Notification notification) {
        if (notification == null) {
            return null;
        }
        return NotificationEntity.builder()
                .id(notification.getId())
                .message(notification.getMessage())
                .recipient(notification.getRecipient())
                .seen(notification.isSeen())
                .listingEntity(ListingConverter.convertToEntity(notification.getListing()))
                .build();
    }
}
