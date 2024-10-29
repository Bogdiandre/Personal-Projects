package backend.controller;

import backend.controller.dto.user.CreateUserRequest;
import backend.controller.dto.user.CreateUserResponse;
import backend.controller.dto.user.GetSingleUserResponse;
import backend.controller.dto.user.GetUsersResponse;
import backend.service.UserService;
import backend.service.domain.User;
import backend.service.domain.enums.RolesEnum;
import backend.service.exception.EmailAlreadyExistsException;
import backend.service.exception.InvalidUserException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.security.test.context.support.WithMockUser;
import org.springframework.test.web.servlet.MockMvc;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.ArgumentMatchers.eq;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@SpringBootTest
@AutoConfigureMockMvc
public class UserControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private UserService userService;

    @Autowired
    private ObjectMapper objectMapper;

    @Test
    @WithMockUser(roles = "MANAGER")
    void getUsers_shouldReturnAllUsers() throws Exception {
        User user1 = User.builder().id(1L).firstName("John").lastName("Doe").email("john@example.com").role(RolesEnum.CLIENT).build();
        User user2 = User.builder().id(2L).firstName("Jane").lastName("Doe").email("jane@example.com").role(RolesEnum.EMPLOYEE).build();
        List<User> users = Arrays.asList(user1, user2);

        when(userService.getUsers()).thenReturn(users);

        mockMvc.perform(get("/users"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.users").isArray())
                .andExpect(jsonPath("$.users[0].firstName").value("John"))
                .andExpect(jsonPath("$.users[1].firstName").value("Jane"));
    }

    @Test
    @WithMockUser(roles = "MANAGER")
    void createUser_shouldReturnConflict_WhenEmailAlreadyExists() throws Exception {
        CreateUserRequest request = CreateUserRequest.builder()
                .firstName("John")
                .lastName("Doe")
                .email("john@example.com")
                .password("password123")
                .role("CLIENT")
                .build();

        when(userService.createUser(any(User.class))).thenThrow(new EmailAlreadyExistsException());

        mockMvc.perform(post("/users")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(objectMapper.writeValueAsString(request)))
                .andExpect(status().isConflict())
                .andExpect(content().string("Email already exists."));
    }



    @Test
    @WithMockUser(roles = "MANAGER")
    void deleteUser_shouldReturnNoContent() throws Exception {
        mockMvc.perform(delete("/users/1"))
                .andExpect(status().isNoContent());
    }

    @Test
    @WithMockUser(roles = {"EMPLOYEE", "MANAGER"})
    void getUserById_shouldReturnUser_WhenUserExists() throws Exception {
        User user = User.builder().id(1L).firstName("John").lastName("Doe").email("john@example.com").role(RolesEnum.CLIENT).build();
        when(userService.getUser(1L)).thenReturn(Optional.of(user));

        mockMvc.perform(get("/users/1"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.firstName").value("John"));
    }

    @Test
    @WithMockUser(roles = {"EMPLOYEE", "MANAGER"})
    void getUserById_shouldReturnNotFound_WhenUserDoesNotExist() throws Exception {
        when(userService.getUser(1L)).thenReturn(Optional.empty());

        mockMvc.perform(get("/users/1"))
                .andExpect(status().isNotFound())
                .andExpect(content().string("User not found"));
    }

    @Test
    @WithMockUser(roles = "MANAGER")
    void getAllRoles_shouldReturnListOfRoles() throws Exception {
        mockMvc.perform(get("/users/getAllRoles"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$").isArray())
                .andExpect(jsonPath("$[0]").value("EMPLOYEE"));
    }

    @Test
    @WithMockUser(roles = {"EMPLOYEE", "MANAGER"})
    void getManagers_shouldReturnAllManagers() throws Exception {
        User manager1 = User.builder().id(1L).firstName("John").lastName("Doe").email("john@example.com").role(RolesEnum.MANAGER).build();
        User manager2 = User.builder().id(2L).firstName("Jane").lastName("Doe").email("jane@example.com").role(RolesEnum.MANAGER).build();
        List<User> managers = Arrays.asList(manager1, manager2);

        when(userService.getManagers()).thenReturn(managers);

        mockMvc.perform(get("/users/managers"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.users").isArray())
                .andExpect(jsonPath("$.users[0].firstName").value("John"))
                .andExpect(jsonPath("$.users[1].firstName").value("Jane"));
    }

    @Test
    @WithMockUser(roles = {"EMPLOYEE", "MANAGER"})
    void getClients_shouldReturnAllClients() throws Exception {
        User client1 = User.builder().id(1L).firstName("John").lastName("Doe").email("john@example.com").role(RolesEnum.CLIENT).build();
        User client2 = User.builder().id(2L).firstName("Jane").lastName("Doe").email("jane@example.com").role(RolesEnum.CLIENT).build();
        List<User> clients = Arrays.asList(client1, client2);

        when(userService.getClients()).thenReturn(clients);

        mockMvc.perform(get("/users/clients"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.users").isArray())
                .andExpect(jsonPath("$.users[0].firstName").value("John"))
                .andExpect(jsonPath("$.users[1].firstName").value("Jane"));
    }

    @Test
    @WithMockUser(roles = {"EMPLOYEE", "MANAGER"})
    void getEmployees_shouldReturnAllEmployees() throws Exception {
        User employee1 = User.builder().id(1L).firstName("John").lastName("Doe").email("john@example.com").role(RolesEnum.EMPLOYEE).build();
        User employee2 = User.builder().id(2L).firstName("Jane").lastName("Doe").email("jane@example.com").role(RolesEnum.EMPLOYEE).build();
        List<User> employees = Arrays.asList(employee1, employee2);

        when(userService.getEmployees()).thenReturn(employees);

        mockMvc.perform(get("/users/employees"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.users").isArray())
                .andExpect(jsonPath("$.users[0].firstName").value("John"))
                .andExpect(jsonPath("$.users[1].firstName").value("Jane"));
    }
}
