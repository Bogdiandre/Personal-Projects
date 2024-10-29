package backend.service;

import backend.service.domain.User;
import backend.service.exception.EmailAlreadyExistsException;
import backend.service.exception.InvalidUserException;
import backend.service.exception.InvalidUserIdException;

import java.util.List;
import java.util.Optional;

public interface UserService {
    Long createUser(User user) throws EmailAlreadyExistsException, InvalidUserException;
    void deleteUser(long userId);
    List<User> getUsers();

    List<User> getClients();

    List<User> getEmployees();
    List<User> getManagers();
    Optional<User> getUser(long userId);
    void updateUser(User user) throws InvalidUserException, InvalidUserIdException;


    String errorExample();
}
