package backend.service.impl;

import backend.configuration.security.token.AccessTokenEncoder;
import backend.configuration.security.token.impl.AccessTokenImpl;
import backend.controller.dto.login.LoginRequest;
import backend.controller.dto.login.LoginResponse;
import backend.persistance.UsersRepository;
import backend.persistance.entity.UserEntity;
import backend.service.domain.enums.RolesEnum;
import backend.service.exception.InvalidCredentialsException;
import backend.service.exception.InvalidUserException;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.springframework.security.crypto.password.PasswordEncoder;

import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

class LoginServiceImplTest {

    @Mock
    private UsersRepository usersRepository;

    @Mock
    private PasswordEncoder passwordEncoder;

    @Mock
    private AccessTokenEncoder accessTokenEncoder;

    @InjectMocks
    private LoginServiceImpl loginService;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.openMocks(this);
    }

    @Test
    void testLoginSuccess() throws InvalidUserException {
        // Arrange
        LoginRequest loginRequest = new LoginRequest();
        loginRequest.setEmail("test@example.com");
        loginRequest.setPassword("password");

        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        userEntity.setEmail("test@example.com");
        userEntity.setPassword("encodedPassword");
        userEntity.setRole(RolesEnum.CLIENT);

        when(usersRepository.findByEmail(loginRequest.getEmail())).thenReturn(Optional.of(userEntity));
        when(passwordEncoder.matches(loginRequest.getPassword(), userEntity.getPassword())).thenReturn(true);
        when(accessTokenEncoder.encode(any(AccessTokenImpl.class))).thenReturn("mockedAccessToken");

        // Act
        LoginResponse loginResponse = loginService.login(loginRequest);

        // Assert
        assertNotNull(loginResponse);
        assertEquals("mockedAccessToken", loginResponse.getAccessToken());
    }

    @Test
    void testLoginThrowsInvalidCredentialsExceptionWhenUserNotFound() {
        // Arrange
        LoginRequest loginRequest = new LoginRequest();
        loginRequest.setEmail("test@example.com");
        loginRequest.setPassword("password");

        when(usersRepository.findByEmail(loginRequest.getEmail())).thenReturn(Optional.empty());

        // Act & Assert
        assertThrows(InvalidCredentialsException.class, () -> loginService.login(loginRequest));
    }

    @Test
    void testLoginThrowsInvalidCredentialsExceptionWhenPasswordMismatch() {
        // Arrange
        LoginRequest loginRequest = new LoginRequest();
        loginRequest.setEmail("test@example.com");
        loginRequest.setPassword("password");

        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        userEntity.setEmail("test@example.com");
        userEntity.setPassword("encodedPassword");
        userEntity.setRole(RolesEnum.CLIENT);

        when(usersRepository.findByEmail(loginRequest.getEmail())).thenReturn(Optional.of(userEntity));
        when(passwordEncoder.matches(loginRequest.getPassword(), userEntity.getPassword())).thenReturn(false);

        // Act & Assert
        assertThrows(InvalidCredentialsException.class, () -> loginService.login(loginRequest));
    }

    @Test
    void testLoginThrowsInvalidUserExceptionForNonClientRole() {
        // Arrange
        LoginRequest loginRequest = new LoginRequest();
        loginRequest.setEmail("test@example.com");
        loginRequest.setPassword("password");

        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        userEntity.setEmail("test@example.com");
        userEntity.setPassword("encodedPassword");
        userEntity.setRole(RolesEnum.EMPLOYEE);

        when(usersRepository.findByEmail(loginRequest.getEmail())).thenReturn(Optional.of(userEntity));

        // Act & Assert
        assertThrows(InvalidUserException.class, () -> loginService.login(loginRequest));
    }

    @Test
    void testLoginWorkerSuccess() throws InvalidUserException {
        // Arrange
        LoginRequest loginRequest = new LoginRequest();
        loginRequest.setEmail("worker@example.com");
        loginRequest.setPassword("password");

        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        userEntity.setEmail("worker@example.com");
        userEntity.setPassword("encodedPassword");
        userEntity.setRole(RolesEnum.EMPLOYEE);

        when(usersRepository.findByEmail(loginRequest.getEmail())).thenReturn(Optional.of(userEntity));
        when(passwordEncoder.matches(loginRequest.getPassword(), userEntity.getPassword())).thenReturn(true);
        when(accessTokenEncoder.encode(any(AccessTokenImpl.class))).thenReturn("mockedAccessToken");

        // Act
        LoginResponse loginResponse = loginService.loginWorker(loginRequest);

        // Assert
        assertNotNull(loginResponse);
        assertEquals("mockedAccessToken", loginResponse.getAccessToken());
    }

    @Test
    void testLoginWorkerThrowsInvalidCredentialsExceptionWhenUserNotFound() {
        // Arrange
        LoginRequest loginRequest = new LoginRequest();
        loginRequest.setEmail("worker@example.com");
        loginRequest.setPassword("password");

        when(usersRepository.findByEmail(loginRequest.getEmail())).thenReturn(Optional.empty());

        // Act & Assert
        assertThrows(InvalidCredentialsException.class, () -> loginService.loginWorker(loginRequest));
    }

    @Test
    void testLoginWorkerThrowsInvalidCredentialsExceptionWhenPasswordMismatch() {
        // Arrange
        LoginRequest loginRequest = new LoginRequest();
        loginRequest.setEmail("worker@example.com");
        loginRequest.setPassword("password");

        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        userEntity.setEmail("worker@example.com");
        userEntity.setPassword("encodedPassword");
        userEntity.setRole(RolesEnum.EMPLOYEE);

        when(usersRepository.findByEmail(loginRequest.getEmail())).thenReturn(Optional.of(userEntity));
        when(passwordEncoder.matches(loginRequest.getPassword(), userEntity.getPassword())).thenReturn(false);

        // Act & Assert
        assertThrows(InvalidCredentialsException.class, () -> loginService.loginWorker(loginRequest));
    }

    @Test
    void testLoginWorkerThrowsInvalidUserExceptionForClientRole() {
        // Arrange
        LoginRequest loginRequest = new LoginRequest();
        loginRequest.setEmail("client@example.com");
        loginRequest.setPassword("password");

        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        userEntity.setEmail("client@example.com");
        userEntity.setPassword("encodedPassword");
        userEntity.setRole(RolesEnum.CLIENT);

        when(usersRepository.findByEmail(loginRequest.getEmail())).thenReturn(Optional.of(userEntity));

        // Act & Assert
        assertThrows(InvalidUserException.class, () -> loginService.loginWorker(loginRequest));
    }

    @Test
    void testGenerateAccessToken() {
        // Arrange
        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        userEntity.setEmail("test@example.com");
        userEntity.setRole(RolesEnum.CLIENT);

        when(accessTokenEncoder.encode(any(AccessTokenImpl.class))).thenReturn("mockedAccessToken");

        // Act
        String accessToken = loginService.generateAccessToken(userEntity);

        // Assert
        assertEquals("mockedAccessToken", accessToken);
    }
}
