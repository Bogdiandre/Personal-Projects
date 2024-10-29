package backend.service;

import backend.controller.dto.login.LoginRequest;
import backend.controller.dto.login.LoginResponse;
import backend.service.exception.InvalidUserException;

public interface LoginService {
     LoginResponse login(LoginRequest loginRequest) throws InvalidUserException;

     LoginResponse loginWorker(LoginRequest loginRequest) throws InvalidUserException;
}
