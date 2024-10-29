package backend.controller;

import backend.controller.converters.UserControllerConverter;
import backend.controller.dto.user.CreateUserRequest;
import backend.controller.dto.user.CreateUserResponse;
import backend.controller.dto.user.GetUsersResponse;
import backend.controller.dto.user.GetSingleUserResponse;
import backend.service.UserService;
import backend.service.domain.User;
import backend.service.domain.enums.RolesEnum;
import backend.service.exception.EmailAlreadyExistsException;
import backend.service.exception.InvalidUserException;
import jakarta.annotation.security.RolesAllowed;
import jakarta.validation.Valid;
import lombok.AllArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;


@RestController
@AllArgsConstructor
@RequestMapping("/users")
@CrossOrigin("http://TurboBids_db_staging:5173/")
public class UserController {

    private final UserService userService;

    @RolesAllowed({"MANAGER"})
    @GetMapping
    public ResponseEntity<GetUsersResponse> getUsers() {
        List<User> userList = userService.getUsers();
        GetUsersResponse response = UserControllerConverter.getUsersResponseFromDomain(userList);
        return ResponseEntity.ok(response);
    }
    @RolesAllowed({"MANAGER"})
    @DeleteMapping("/{userId}")
    public ResponseEntity<Void> deleteUser(@PathVariable long userId) {
        userService.deleteUser(userId);
        return ResponseEntity.noContent().build();
    }
    @RolesAllowed({"MANAGER"})
    @PostMapping()
    public ResponseEntity<Object> createUser(@RequestBody @Valid CreateUserRequest request) {
        User user = UserControllerConverter.convertFromCreateUserRequest(request);
        try {
            Long userId = userService.createUser(user);
            user.setId(userId);
            CreateUserResponse response = CreateUserResponse.builder().userId(userId).build();
            return ResponseEntity.status(HttpStatus.CREATED).body(response);
        } catch (EmailAlreadyExistsException e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body("Email already exists.");
        } catch (InvalidUserException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Invalid user details.");
        } catch (Exception e) {
             // This will log the stack trace to the server logs
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("An error occurred while creating the user: " + e.getMessage());
        }
    }

    @RolesAllowed({"MANAGER"})
    @GetMapping("/{userId}")
    public ResponseEntity<Object> getUserById(@PathVariable long userId) {
        Optional<User> user = userService.getUser(userId);
        if (user.isPresent()) {
            GetSingleUserResponse response = UserControllerConverter.getSingleUserResponseFromDomain(user.get());
            return ResponseEntity.ok(response);
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("User not found");
        }
    }
    @RolesAllowed({"MANAGER"})
    @GetMapping("/getAllRoles")
    public ResponseEntity<List<String>> getAllRoles() {
        List<String> roles = Arrays.stream(RolesEnum.values())
                .map(Enum::name)
                .toList();
        return ResponseEntity.ok(roles);
    }
    @RolesAllowed({"MANAGER"})
    @GetMapping("/managers")
    public ResponseEntity<GetUsersResponse> getManagers() {
        List<User> managerList = userService.getManagers();
        GetUsersResponse response = UserControllerConverter.getUsersResponseFromDomain(managerList);
        return ResponseEntity.ok(response);
    }
    @RolesAllowed({"MANAGER"})
    @GetMapping("/clients")
    public ResponseEntity<GetUsersResponse> getClients() {
        List<User> clientList = userService.getClients();
        GetUsersResponse response = UserControllerConverter.getUsersResponseFromDomain(clientList);
        return ResponseEntity.ok(response);
    }
    @RolesAllowed({"MANAGER"})
    @GetMapping("/employees")
    public ResponseEntity<GetUsersResponse> getEmployees() {
        List<User> employeesList = userService.getEmployees();
        GetUsersResponse response = UserControllerConverter.getUsersResponseFromDomain(employeesList);
        return ResponseEntity.ok(response);
    }
}
