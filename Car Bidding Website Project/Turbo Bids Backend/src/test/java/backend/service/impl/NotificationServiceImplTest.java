package backend.service.impl;

import backend.persistance.ListingRepository;
import backend.persistance.NotificationRepository;
import backend.persistance.UsersRepository;
import backend.persistance.entity.NotificationEntity;
import backend.persistance.entity.UserEntity;
import backend.service.converters.NotificationConverter;
import backend.service.converters.UserConverter;
import backend.service.domain.*;
import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.RolesEnum;
import backend.service.exception.InvalidNotificationException;
import backend.service.exception.InvalidNotificationRecipientException;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.springframework.messaging.simp.SimpMessagingTemplate;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.ArgumentMatchers.anyLong;
import static org.mockito.ArgumentMatchers.anyString;
import static org.mockito.Mockito.*;

class NotificationServiceImplTest {

    @Mock
    private ListingRepository listingRepository;

    @Mock
    private UsersRepository usersRepository;

    @Mock
    private NotificationRepository notificationRepository;

    @Mock
    private SimpMessagingTemplate messagingTemplate;

    @InjectMocks
    private NotificationServiceImpl notificationService;

    @BeforeEach
    public void setUp() {
        MockitoAnnotations.openMocks(this);
        notificationService = new NotificationServiceImpl(listingRepository, usersRepository, notificationRepository, messagingTemplate);
    }

    @Test
    void testSaveNotification() {
        // Arrange
        Notification notification = Notification.builder()
                .message("Test message")
                .recipient("testuser")
                .seen(false)
                .listing(Listing.builder().id(1L).build())
                .build();
        NotificationEntity notificationEntity = NotificationConverter.convertToEntity(notification);
        when(notificationRepository.save(any(NotificationEntity.class))).thenReturn(notificationEntity);

        // Act
        Notification savedNotification = notificationService.saveNotification(notification);

        // Assert
        assertEquals(notification.getMessage(), savedNotification.getMessage());
        verify(notificationRepository, times(1)).save(any(NotificationEntity.class));
    }

    @Test
    void testSaveNotificationThrowsException() {
        // Arrange
        Notification notification = Notification.builder().build();

        // Act & Assert
        assertThrows(InvalidNotificationException.class, () -> notificationService.saveNotification(notification));
    }

    @Test
    void testGetNotificationsForUser() {
        // Arrange
        NotificationEntity notificationEntity = new NotificationEntity();
        notificationEntity.setMessage("Test message");
        notificationEntity.setRecipient("testuser");
        when(notificationRepository.findByRecipient(anyString())).thenReturn(Arrays.asList(notificationEntity));

        // Act
        List<Notification> notifications = notificationService.getNotificationsForUser("testuser");

        // Assert
        assertEquals(1, notifications.size());
        assertEquals("Test message", notifications.get(0).getMessage());
    }

    @Test
    void testGetNotificationsForUserThrowsException() {
        // Act & Assert
        assertThrows(InvalidNotificationRecipientException.class, () -> notificationService.getNotificationsForUser(null));
    }

    @Test
    void testSendNotificationsForListing() {
        // Arrange
        User user = User.builder().firstName("John").lastName("Doe").build();
        Bid bid = Bid.builder().account(user).amount(100.0).build();
        Listing listing = Listing.builder().bids(Arrays.asList(bid)).build();

        // Act
        notificationService.sendNotificationsForListing(listing, "Test message");

        // Assert
        verify(messagingTemplate, times(1)).convertAndSendToUser(anyString(), anyString(), any(Notification.class));
    }

    @Test
    void testSendNotificationsForListingThrowsException() {
        // Act & Assert
        assertThrows(InvalidNotificationException.class, () -> notificationService.sendNotificationsForListing(null, "Test message"));
        assertThrows(InvalidNotificationException.class, () -> notificationService.sendNotificationsForListing(Listing.builder().build(), null));
    }

    @Test
    void testCreateAndSendBidNotificationForBid() {
        // Arrange
        User user = User.builder().id(1L).firstName("John").lastName("Doe").build();
        User anotherUser = User.builder().id(2L).firstName("Jane").lastName("Doe").build();
        Bid bid = Bid.builder().account(user).amount(100.0).build();
        Vehicle vehicle = Vehicle.builder().maker(MakerEnum.TOYOTA).model("Camry").build();
        Listing listing = Listing.builder().id(1L).bids(Arrays.asList(bid)).request(Request.builder().vehicle(vehicle).build()).build();

        when(listingRepository.findUniqueBiddersByListingId(anyLong())).thenReturn(Arrays.asList(UserConverter.convertToEntity(anotherUser)));

        // Act
        notificationService.createAndSendBidNotificationForBid(bid, listing);

        // Assert
        verify(messagingTemplate, times(1)).convertAndSendToUser(anyString(), anyString(), any(Notification.class));
    }

    @Test
    void testCreateAndSendBidNotificationForBidThrowsException() {
        // Act & Assert
        assertThrows(InvalidNotificationException.class, () -> notificationService.createAndSendBidNotificationForBid(null, Listing.builder().build()));
        assertThrows(InvalidNotificationException.class, () -> notificationService.createAndSendBidNotificationForBid(Bid.builder().build(), null));
    }



    @Test
    void testCreateAndSendBidNotificationForBuyOutThrowsException() {
        // Act & Assert
        assertThrows(InvalidNotificationException.class, () -> notificationService.createAndSendBidNotificationForBuyOut(null, Listing.builder().build()));
        assertThrows(InvalidNotificationException.class, () -> notificationService.createAndSendBidNotificationForBuyOut(1L, null));
    }
}
