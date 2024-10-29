package backend.service.converters;

import backend.persistance.entity.UserEntity;
import backend.service.domain.User;

import java.util.List;
import java.util.stream.Collectors;

public class UserConverter {

    private UserConverter() {
        // Private constructor to prevent instantiation
    }

    public static User convertToDomain(UserEntity userEntity) {
        if (userEntity == null) {
            return null;
        }

        return User.builder()
                .id(userEntity.getId())
                .lastName(userEntity.getLastName())
                .firstName(userEntity.getFirstName())
                .email(userEntity.getEmail())
                .password(userEntity.getPassword())
                .role(userEntity.getRole())
                .build();
    }

    public static UserEntity convertToEntity(User user) {
        if (user == null) {
            return null;
        }

        return UserEntity.builder()
                .id(user.getId())
                .lastName(user.getLastName())
                .firstName(user.getFirstName())
                .email(user.getEmail())
                .password(user.getPassword())
                .role(user.getRole())
                .build();
    }

    public static List<User> convertToDomainList(List<UserEntity> userEntities) {
        return userEntities.stream()
                .map(UserConverter::convertToDomain)
                .collect(Collectors.toList());
    }
}
