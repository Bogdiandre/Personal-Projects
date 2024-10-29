package backend.service.impl;

import backend.persistance.UsersRepository;
import backend.persistance.entity.UserEntity;
import backend.service.UserService;
import backend.service.converters.UserConverter;
import backend.service.domain.User;
import backend.service.domain.enums.RolesEnum;
import backend.service.exception.EmailAlreadyExistsException;
import backend.service.exception.InvalidUserException;
import backend.service.exception.InvalidUserIdException;
import lombok.RequiredArgsConstructor;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class UserServiceImpl implements UserService {

    private static final Logger logger = LoggerFactory.getLogger(UserServiceImpl.class);

    private final UsersRepository userRepository;
    private final PasswordEncoder passwordEncoder;

    @Override
    public Long createUser(User user) throws EmailAlreadyExistsException, InvalidUserException {
        logger.debug("Creating user: {}", user);
        if (userRepository.findByEmail(user.getEmail()).isPresent()) {
            throw new EmailAlreadyExistsException();
        }
        if (user.getEmail() == null || user.getPassword() == null) {
            throw new InvalidUserException();
        }
        user.setPassword(passwordEncoder.encode(user.getPassword()));
        UserEntity userEntity = UserConverter.convertToEntity(user);
        UserEntity savedUser = userRepository.save(userEntity);
        return savedUser.getId();
    }

    @Override
    public void deleteUser(long userId) {
        logger.debug("Deleting user with ID: {}", userId);
        this.userRepository.deleteById(userId);
    }

    @Override
    public List<User> getUsers() {
        logger.debug("Fetching all users");
        List<UserEntity> userEntities = userRepository.findAll();
        return userEntities.stream()
                .map(UserConverter::convertToDomain)
                .toList();
    }

    @Override
    public List<User> getClients() {
        logger.debug("Fetching all clients");
        List<UserEntity> userEntities = userRepository.findAll();
        return userEntities.stream()
                .filter(entity -> entity.getRole() == RolesEnum.CLIENT)
                .map(UserConverter::convertToDomain)
                .toList();
    }

    @Override
    public List<User> getEmployees() {
        logger.debug("Fetching all employees");
        List<UserEntity> userEntities = userRepository.findAll();
        return userEntities.stream()
                .filter(entity -> entity.getRole() == RolesEnum.EMPLOYEE)
                .map(UserConverter::convertToDomain)
                .toList();
    }

    @Override
    public List<User> getManagers() {
        logger.debug("Fetching all managers");
        List<UserEntity> userEntities = userRepository.findAll();
        return userEntities.stream()
                .filter(entity -> entity.getRole() == RolesEnum.MANAGER)
                .map(UserConverter::convertToDomain)
                .toList();
    }

    @Override
    public Optional<User> getUser(long userId) {
        logger.debug("Fetching user with ID: {}", userId);
        Optional<UserEntity> optionalUserEntity = userRepository.findById(userId);
        return optionalUserEntity.map(UserConverter::convertToDomain);
    }

    @Override
    public void updateUser(User user) throws InvalidUserException, InvalidUserIdException {
        logger.debug("Updating user: {}", user);
        if (user.getId() == null || userRepository.findById(user.getId()).isEmpty()) {
            throw new InvalidUserIdException();
        }
        if (user.getEmail() == null || user.getPassword() == null) {
            throw new InvalidUserException();
        }
        user.setPassword(passwordEncoder.encode(user.getPassword()));
        UserEntity userEntity = UserConverter.convertToEntity(user);
        userRepository.save(userEntity);
    }

    @Override
    public String errorExample() {
        return null;
    }
}
