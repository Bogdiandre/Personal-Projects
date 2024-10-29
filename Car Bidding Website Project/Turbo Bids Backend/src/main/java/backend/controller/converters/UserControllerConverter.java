package backend.controller.converters;

import backend.controller.dto.user.CreateUserRequest;
import backend.controller.dto.user.GetSingleUserResponse;
import backend.controller.dto.user.GetUsersResponse;
import backend.service.domain.User;
import backend.service.domain.enums.RolesEnum;

import java.util.ArrayList;
import java.util.List;

public class UserControllerConverter {

    private UserControllerConverter() {}

    public static GetUsersResponse getUsersResponseFromDomain(List<User> userList) {
        List<GetUsersResponse.User> responseUserList = new ArrayList<>();

        for (User user : userList) {
            GetUsersResponse.User responseUser = GetUsersResponse.User.builder()
                    .id(user.getId())  // Changed to 'id'
                    .email(user.getEmail())
                    .lastName(user.getLastName())
                    .firstName(user.getFirstName())
                    .password(user.getPassword())
                    .role(user.getRole().toString())
                    .build();

            responseUserList.add(responseUser);
        }

        return GetUsersResponse.builder()
                .users(responseUserList)
                .build();
    }

    public static GetSingleUserResponse getSingleUserResponseFromDomain(User user) {
        return GetSingleUserResponse.builder()
                .id(user.getId())
                .email(user.getEmail())
                .lastName(user.getLastName())
                .firstName(user.getFirstName())
                .password(user.getPassword())
                .role(user.getRole().toString())
                .build();
    }

    public static User convertFromCreateUserRequest(CreateUserRequest request) {
        return User.builder()
                .email(request.getEmail())
                .lastName(request.getLastName())
                .firstName(request.getFirstName())
                .password(request.getPassword())
                .role(RolesEnum.valueOf(request.getRole()))
                .build();
    }
}
