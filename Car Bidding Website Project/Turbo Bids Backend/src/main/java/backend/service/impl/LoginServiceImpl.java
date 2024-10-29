package backend.service.impl;

import backend.configuration.security.token.AccessTokenEncoder;
import backend.configuration.security.token.impl.AccessTokenImpl;
import backend.persistance.UsersRepository;
import backend.persistance.entity.UserEntity;
import backend.service.LoginService;
import backend.controller.dto.login.LoginRequest;
import backend.controller.dto.login.LoginResponse;
import backend.service.domain.enums.RolesEnum;
import backend.service.exception.InvalidCredentialsException;
import backend.service.exception.InvalidUserException;
import lombok.RequiredArgsConstructor;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
@RequiredArgsConstructor
public class LoginServiceImpl implements LoginService {

    private final UsersRepository usersRepository;
    private final PasswordEncoder passwordEncoder;
    private final AccessTokenEncoder accessTokenEncoder;

    @Override
    public LoginResponse login(LoginRequest loginRequest) throws InvalidUserException {
        Optional<UserEntity> optionalUser = usersRepository.findByEmail(loginRequest.getEmail());
        UserEntity user = optionalUser.orElseThrow(InvalidCredentialsException::new);
        if(user.getRole() == RolesEnum.CLIENT) {
            if (!passwordEncoder.matches(loginRequest.getPassword(), user.getPassword())) {
                throw new InvalidCredentialsException();
            }

            String accessToken = generateAccessToken(user);
            return LoginResponse.builder().accessToken(accessToken).build();
        }
        else
        {
            throw new InvalidUserException();
        }
    }

    @Override
    public LoginResponse loginWorker(LoginRequest loginRequest) throws InvalidUserException {
        Optional<UserEntity> optionalUser = usersRepository.findByEmail(loginRequest.getEmail());
        UserEntity user = optionalUser.orElseThrow(InvalidCredentialsException::new);
        if(user.getRole() != RolesEnum.CLIENT) {
            if (!passwordEncoder.matches(loginRequest.getPassword(), user.getPassword())) {
                throw new InvalidCredentialsException();
            }

            String accessToken = generateAccessToken(user);
            return LoginResponse.builder().accessToken(accessToken).build();
        }
        else
        {
            throw new InvalidUserException();
        }
    }

    String generateAccessToken(UserEntity user) {
        Long userId = user.getId();
        String role = user.getRole().toString();
        String firstName = user.getFirstName().toString();
        String lastName = user.getLastName().toString();

        return accessTokenEncoder.encode(
                new AccessTokenImpl(user.getEmail(), userId, role,firstName,lastName));
    }
}
