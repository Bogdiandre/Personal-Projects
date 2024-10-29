package backend.controller;

import backend.controller.converters.RegisterControllerConverter;
import backend.controller.dto.register.CreateClientRequest;
import backend.controller.dto.user.CreateUserResponse;
import backend.service.LoginService;
import backend.controller.dto.login.LoginRequest;
import backend.controller.dto.login.LoginResponse;
import backend.service.UserService;
import backend.service.domain.User;
import backend.service.exception.EmailAlreadyExistsException;
import backend.service.exception.InvalidCredentialsException;
import backend.service.exception.InvalidUserException;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@Slf4j
@RestController
@RequestMapping("/auth")
@CrossOrigin(origins = "http://localhost:5173")
@RequiredArgsConstructor
public class AuthController {

    private final LoginService loginService;
    private final UserService userService;

    @PostMapping("/login")
    public ResponseEntity<LoginResponse> login(@RequestBody @Valid LoginRequest loginRequest) {
        try {
            LoginResponse loginResponse = loginService.login(loginRequest);
            return ResponseEntity.status(HttpStatus.CREATED).body(loginResponse);
        } catch (InvalidCredentialsException e) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(null);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(null);
        }
    }

    @PostMapping("/login/worker-login")
    public ResponseEntity<LoginResponse> loginWorker(@RequestBody @Valid LoginRequest loginRequest) {
        try {
            LoginResponse loginResponse = loginService.loginWorker(loginRequest);
            return ResponseEntity.status(HttpStatus.CREATED).body(loginResponse);
        } catch (InvalidCredentialsException e) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(null);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(null);
        }
    }

    @PostMapping("/register/client")
    public ResponseEntity<Object> registerClient(@RequestBody @Valid CreateClientRequest request) {
        // Log the entire request object
        log.info("Received registration request: {}", request);

        // Log individual fields

        User user = RegisterControllerConverter.convertFromCreateClientRequest(request);
        try {
            Long userId = userService.createUser(user);
            user.setId(userId);
            LoginRequest loginRequest = LoginRequest.builder()
                    .email(request.getEmail())
                    .password(request.getPassword())
                    .build();
            LoginResponse loginResponse = loginService.login(loginRequest);
            return ResponseEntity.status(HttpStatus.CREATED).body(loginResponse);

        } catch (EmailAlreadyExistsException e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body("Email already exists.");
        } catch (InvalidUserException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Invalid user details.");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("An error occurred while creating the user: " + e.getMessage());
        }
    }

}
