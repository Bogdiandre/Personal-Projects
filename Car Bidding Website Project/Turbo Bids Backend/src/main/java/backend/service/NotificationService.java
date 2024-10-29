package backend.service;

import backend.service.domain.Listing;
import backend.service.domain.Notification;
import backend.service.domain.Bid;

import java.util.List;

public interface NotificationService {

    Notification saveNotification(Notification notification);

    List<Notification> getNotificationsForUser(String recipient);

    List<Notification> getUnseenNotificationsForUser(String recipient);

    void sendNotificationsForListing(Listing listing, String message);

    void createAndSendBidNotificationForBid(Bid bid, Listing listing);

    void createAndSendBidNotificationForBuyOut(Long userId, Listing listing);

    void markNotificationAsSeen(Long notificationId);
}
