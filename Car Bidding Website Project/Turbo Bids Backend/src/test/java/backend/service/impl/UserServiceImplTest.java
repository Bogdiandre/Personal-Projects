package backend.service.impl;

import backend.persistance.UsersRepository;
import backend.persistance.entity.UserEntity;
import backend.service.converters.UserConverter;
import backend.service.domain.User;
import backend.service.domain.enums.RolesEnum;
import backend.service.exception.EmailAlreadyExistsException;
import backend.service.exception.InvalidUserException;
import backend.service.exception.InvalidUserIdException;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.springframework.security.crypto.password.PasswordEncoder;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

class UserServiceImplTest {

    @Mock
    private UsersRepository userRepository;

    @Mock
    private PasswordEncoder passwordEncoder;

    @InjectMocks
    private UserServiceImpl userService;

    @BeforeEach
    public void setUp() {
        MockitoAnnotations.openMocks(this);
    }

    @Test
    void testCreateUserSuccess() throws EmailAlreadyExistsException, InvalidUserException {
        User user = User.builder()
                .email("test@example.com")
                .password("password")
                .build();

        UserEntity userEntity = UserConverter.convertToEntity(user);
        userEntity.setId(1L);

        when(userRepository.findByEmail(user.getEmail())).thenReturn(Optional.empty());
        when(passwordEncoder.encode(user.getPassword())).thenReturn("encodedPassword");
        when(userRepository.save(any(UserEntity.class))).thenReturn(userEntity);

        Long userId = userService.createUser(user);
        assertEquals(1L, userId);
    }

    @Test
    void testCreateUserThrowsEmailAlreadyExistsException() {
        User user = User.builder()
                .email("test@example.com")
                .build();

        when(userRepository.findByEmail(user.getEmail())).thenReturn(Optional.of(new UserEntity()));

        assertThrows(EmailAlreadyExistsException.class, () -> userService.createUser(user));
    }

    @Test
    void testCreateUserThrowsInvalidUserException() {
        User user = User.builder()
                .email("test@example.com")
                .build();

        assertThrows(InvalidUserException.class, () -> userService.createUser(user));

        user.setEmail(null);
        user.setPassword("password");
        assertThrows(InvalidUserException.class, () -> userService.createUser(user));
    }

    @Test
    void testDeleteUser() {
        userService.deleteUser(1L);
        verify(userRepository, times(1)).deleteById(1L);
    }

    @Test
    void testGetUsers() {
        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        userEntity.setEmail("test@example.com");

        when(userRepository.findAll()).thenReturn(Arrays.asList(userEntity));

        List<User> users = userService.getUsers();
        assertEquals(1, users.size());
        assertEquals("test@example.com", users.get(0).getEmail());
    }

    @Test
    void testGetClients() {
        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        userEntity.setEmail("client@example.com");
        userEntity.setRole(RolesEnum.CLIENT);

        when(userRepository.findAll()).thenReturn(Arrays.asList(userEntity));

        List<User> clients = userService.getClients();
        assertEquals(1, clients.size());
        assertEquals("client@example.com", clients.get(0).getEmail());
    }

    @Test
    void testGetEmployees() {
        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        userEntity.setEmail("employee@example.com");
        userEntity.setRole(RolesEnum.EMPLOYEE);

        when(userRepository.findAll()).thenReturn(Arrays.asList(userEntity));

        List<User> employees = userService.getEmployees();
        assertEquals(1, employees.size());
        assertEquals("employee@example.com", employees.get(0).getEmail());
    }

    @Test
    void testGetManagers() {
        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        userEntity.setEmail("manager@example.com");
        userEntity.setRole(RolesEnum.MANAGER);

        when(userRepository.findAll()).thenReturn(Arrays.asList(userEntity));

        List<User> managers = userService.getManagers();
        assertEquals(1, managers.size());
        assertEquals("manager@example.com", managers.get(0).getEmail());
    }

    @Test
    void testGetUser() {
        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        userEntity.setEmail("test@example.com");

        when(userRepository.findById(1L)).thenReturn(Optional.of(userEntity));

        Optional<User> user = userService.getUser(1L);
        assertTrue(user.isPresent());
        assertEquals("test@example.com", user.get().getEmail());
    }

    @Test
    void testGetUserNotFound() {
        when(userRepository.findById(1L)).thenReturn(Optional.empty());

        Optional<User> user = userService.getUser(1L);
        assertFalse(user.isPresent());
    }

    @Test
    void testUpdateUserSuccess() throws InvalidUserException, InvalidUserIdException {
        User user = User.builder()
                .id(1L)
                .email("test@example.com")
                .password("password")
                .build();

        UserEntity userEntity = UserConverter.convertToEntity(user);

        when(userRepository.findById(1L)).thenReturn(Optional.of(userEntity));
        when(passwordEncoder.encode(user.getPassword())).thenReturn("encodedPassword");
        when(userRepository.save(any(UserEntity.class))).thenReturn(userEntity);

        userService.updateUser(user);
        verify(userRepository, times(1)).save(any(UserEntity.class));
    }

    @Test
    void testUpdateUserThrowsInvalidUserIdException() {
        User user = User.builder()
                .id(1L)
                .email("test@example.com")
                .password("password")
                .build();

        when(userRepository.findById(1L)).thenReturn(Optional.empty());

        assertThrows(InvalidUserIdException.class, () -> userService.updateUser(user));
    }

    @Test
    void testUpdateUserThrowsInvalidUserException() {
        User user = new User();
        user.setId(1L);

        UserEntity userEntity = new UserEntity();
        userEntity.setId(1L);
        when(userRepository.findById(1L)).thenReturn(Optional.of(userEntity));

        assertThrows(InvalidUserException.class, () -> userService.updateUser(user));
    }

    @Test
    void testErrorExample() {
        String result = userService.errorExample();
        assertNull(result);
    }
}
