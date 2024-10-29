package backend.service.impl;

import backend.persistance.ListingRepository;
import backend.persistance.NotificationRepository;
import backend.persistance.UsersRepository;
import backend.persistance.entity.NotificationEntity;
import backend.service.NotificationService;
import backend.service.converters.NotificationConverter;
import backend.service.converters.UserConverter;
import backend.service.domain.Bid;
import backend.service.domain.Listing;
import backend.service.domain.Notification;
import backend.service.domain.User;
import backend.service.domain.Vehicle;
import backend.service.exception.InvalidNotificationException;
import backend.service.exception.InvalidNotificationRecipientException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.simp.SimpMessagingTemplate;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class NotificationServiceImpl implements NotificationService {

    private final ListingRepository listingRepository;
    private final UsersRepository usersRepository;
    private final NotificationRepository notificationRepository;
    private final SimpMessagingTemplate messagingTemplate;

    @Autowired
    public NotificationServiceImpl(ListingRepository listingRepository,
                                   UsersRepository usersRepository,
                                   NotificationRepository notificationRepository,
                                   SimpMessagingTemplate messagingTemplate) {
        this.listingRepository = listingRepository;
        this.usersRepository = usersRepository;
        this.notificationRepository = notificationRepository;
        this.messagingTemplate = messagingTemplate;
    }

    @Override
    public Notification saveNotification(Notification notification) {
        if (notification == null || notification.getRecipient() == null || notification.getMessage() == null) {
            throw new InvalidNotificationException("Invalid notification data");
        }
        NotificationEntity entity = NotificationConverter.convertToEntity(notification);
        NotificationEntity savedEntity = notificationRepository.save(entity);
        return NotificationConverter.convertToDomain(savedEntity);
    }

    @Override
    public List<Notification> getNotificationsForUser(String recipient) {
        if (recipient == null || recipient.isEmpty()) {
            throw new InvalidNotificationRecipientException("Recipient cannot be null or empty");
        }
        List<NotificationEntity> entities = notificationRepository.findByRecipient(recipient);
        return entities.stream()
                .map(NotificationConverter::convertToDomain)
                .collect(Collectors.toList());
    }

    @Override
    public List<Notification> getUnseenNotificationsForUser(String recipient) {
        if (recipient == null || recipient.isEmpty()) {
            throw new InvalidNotificationRecipientException("Recipient cannot be null or empty");
        }
        List<NotificationEntity> entities = notificationRepository.findUnseenNotificationsByRecipient(recipient);
        return entities.stream()
                .map(NotificationConverter::convertToDomain)
                .collect(Collectors.toList());
    }

    @Override
    public void sendNotificationsForListing(Listing listing, String message) {
        if (listing == null || message == null) {
            throw new InvalidNotificationException("Listing or message cannot be null");
        }
        List<Bid> bids = listing.getBids();
        for (Bid bid : bids) {
            String recipient = formatRecipientName(bid.getAccount());
            Notification notification = Notification.builder()
                    .message(message)
                    .recipient(recipient)
                    .listing(listing)
                    .seen(false)
                    .build();
            saveNotification(notification);

            // Send notification via WebSocket
            messagingTemplate.convertAndSendToUser(recipient, "/queue/notifications", notification);
        }
    }

    @Override
    public void createAndSendBidNotificationForBid(Bid bid, Listing listing) {
        if (bid == null || listing == null) {
            throw new InvalidNotificationException("Bid or listing cannot be null");
        }
        User user = bid.getAccount();
        Vehicle vehicle = listing.getRequest().getVehicle();
        String message = String.format("%s %s bidded %.2f on the %s %s listing.",
                user.getFirstName(), user.getLastName(), bid.getAmount(),
                vehicle.getMaker(), vehicle.getModel());

        // Notify all users who have placed a bid on this listing
        NotifyAllUser(user,message,listing);

        // Notify the owner
        String messageForOwner = String.format("%s %s bidded %.2f on your %s %s .",
                user.getFirstName(), user.getLastName(),bid.getAmount(),
                listing.getRequest().getVehicle().getMaker(), listing.getRequest().getVehicle().getModel());

        String recipient = listing.getRequest().getSeller().getId().toString();
        Notification notification = Notification.builder()
                .message(messageForOwner)
                .recipient(recipient)
                .listing(listing)
                .seen(false)
                .build();
        saveNotification(notification);
        messagingTemplate.convertAndSendToUser(recipient, "/queue/notifications", notification);
    }

    @Override
    public void createAndSendBidNotificationForBuyOut(Long userId, Listing listing) {
        if (userId == null || listing == null) {
            throw new InvalidNotificationException("User ID or listing cannot be null");
        }
        User user = UserConverter.convertToDomain(usersRepository.findById(userId).orElseThrow());
        Vehicle vehicle = listing.getRequest().getVehicle();
        String message = String.format("%s %s bought out for %.2f the %s %s listing.",
                user.getFirstName(), user.getLastName(), listing.getRequest().getMaxPrice(),
                vehicle.getMaker(), vehicle.getModel());

        // Notify all users who have placed a bid on this listing
        NotifyAllUser(user,message,listing);

        // Notify the owner
        String messageForOwner = String.format("%s %s bought out your %s %s .",
                user.getFirstName(), user.getLastName(),
                listing.getRequest().getVehicle().getMaker(), listing.getRequest().getVehicle().getModel());

        String recipient = listing.getRequest().getSeller().getId().toString();
        Notification notification = Notification.builder()
                .message(messageForOwner)
                .recipient(recipient)
                .listing(listing)
                .seen(false)
                .build();
        saveNotification(notification);
        messagingTemplate.convertAndSendToUser(recipient, "/queue/notifications", notification);
    }

    @Override
    public void markNotificationAsSeen(Long notificationId) {
        NotificationEntity notificationEntity = notificationRepository.findById(notificationId)
                .orElseThrow(() -> new InvalidNotificationException("Notification not found"));
        notificationEntity.setSeen(true);
        notificationRepository.save(notificationEntity);
    }

    private void NotifyAllUser(User user, String message, Listing listing)
    {
        List<User> uniqueBidders = UserConverter.convertToDomainList(listingRepository.findUniqueBiddersByListingId(listing.getId()));
        for (User bidder : uniqueBidders) {
            if (!user.getId().equals(bidder.getId())) {
                String recipient = bidder.getId().toString();
                Notification notification = Notification.builder()
                        .message(message)
                        .recipient(recipient)
                        .listing(listing)
                        .seen(false)
                        .build();
                saveNotification(notification);
                messagingTemplate.convertAndSendToUser(recipient, "/queue/notifications", notification);
            }
        }


    }

    private String formatRecipientName(User user) {
        if (user == null || user.getFirstName() == null || user.getLastName() == null) {
            throw new InvalidNotificationRecipientException("Invalid user data");
        }
        return user.getFirstName() + " " + user.getLastName();
    }
}
