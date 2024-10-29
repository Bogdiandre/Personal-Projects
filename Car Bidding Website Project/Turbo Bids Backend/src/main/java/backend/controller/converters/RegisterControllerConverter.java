package backend.controller.converters;

import backend.controller.dto.register.CreateClientRequest;
import backend.service.domain.User;
import backend.service.domain.enums.RolesEnum;

public class RegisterControllerConverter {

    private RegisterControllerConverter() {}

    public static User convertFromCreateClientRequest(CreateClientRequest request) {
        return User.builder()
                .email(request.getEmail())
                .lastName(request.getLastName())
                .firstName(request.getFirstName())
                .password(request.getPassword())
                .role(RolesEnum.CLIENT)
                .build();
    }
}
