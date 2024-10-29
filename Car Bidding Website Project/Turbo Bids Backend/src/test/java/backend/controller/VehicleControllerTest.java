package backend.controller;

import backend.service.VehicleService;
import backend.service.domain.Vehicle;
import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.VehicleTypeEnum;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.jupiter.api.Test;
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
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@SpringBootTest
@AutoConfigureMockMvc
public class VehicleControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private VehicleService vehicleService;

    @Autowired
    private ObjectMapper objectMapper;

    @Test
    @WithMockUser(roles = "EMPLOYEE")
    void getVehicles_shouldReturnAllVehicles() throws Exception {
        Vehicle vehicle1 = Vehicle.builder().id(1L).model("Model 1").maker(MakerEnum.TOYOTA).type(VehicleTypeEnum.SEDAN).build();
        Vehicle vehicle2 = Vehicle.builder().id(2L).model("Model 2").maker(MakerEnum.HONDA).type(VehicleTypeEnum.SUV).build();
        List<Vehicle> vehicles = Arrays.asList(vehicle1, vehicle2);

        when(vehicleService.getVehicles()).thenReturn(vehicles);

        mockMvc.perform(get("/vehicles"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.vehicles").isArray())
                .andExpect(jsonPath("$.vehicles[0].model").value("Model 1"))
                .andExpect(jsonPath("$.vehicles[1].model").value("Model 2"));
    }



    @Test
    @WithMockUser
    void getVehicleById_shouldReturnVehicle_WhenVehicleExists() throws Exception {
        Vehicle vehicle = Vehicle.builder().id(1L).model("Model 1").maker(MakerEnum.TOYOTA).type(VehicleTypeEnum.SEDAN).build();
        when(vehicleService.getVehicle(1L)).thenReturn(Optional.of(vehicle));

        mockMvc.perform(get("/vehicles/1"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.model").value("Model 1"));
    }



    @Test
    @WithMockUser(roles = "EMPLOYEE")
    void deleteVehicle_shouldReturnNoContent() throws Exception {
        mockMvc.perform(delete("/vehicles/1"))
                .andExpect(status().isNoContent());
    }

    @Test
    @WithMockUser
    void getAllMakers_shouldReturnListOfMakers() throws Exception {
        mockMvc.perform(get("/vehicles/makers"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$").isArray())
                .andExpect(jsonPath("$[0]").value("ABARTH"));
    }

    @Test
    @WithMockUser
    void getAllVehicleTypes_shouldReturnListOfVehicleTypes() throws Exception {
        mockMvc.perform(get("/vehicles/vehicleTypes"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$").isArray())
                .andExpect(jsonPath("$[0]").value("SEDAN"));
    }

    @Test
    @WithMockUser
    void getVehiclesByType_shouldReturnVehicles_WhenTypeExists() throws Exception {
        Vehicle vehicle = Vehicle.builder().id(1L).model("Model 1").maker(MakerEnum.TOYOTA).type(VehicleTypeEnum.SEDAN).build();
        when(vehicleService.filterVehiclesByType(VehicleTypeEnum.SEDAN)).thenReturn(Optional.of(Arrays.asList(vehicle)));

        mockMvc.perform(get("/vehicles/filterByType").param("type", "Sedan"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.vehicles").isArray())
                .andExpect(jsonPath("$.vehicles[0].model").value("Model 1"));
    }

    @Test
    @WithMockUser
    void getVehiclesByType_shouldReturnBadRequest_WhenTypeInvalid() throws Exception {
        mockMvc.perform(get("/vehicles/filterByType").param("type", "InvalidType"))
                .andExpect(status().isBadRequest());
    }

    @Test
    @WithMockUser
    void getVehiclesByMaker_shouldReturnVehicles_WhenMakerExists() throws Exception {
        Vehicle vehicle = Vehicle.builder().id(1L).model("Model 1").maker(MakerEnum.TOYOTA).type(VehicleTypeEnum.SEDAN).build();
        when(vehicleService.filterVehiclesByMaker(MakerEnum.TOYOTA)).thenReturn(Optional.of(Arrays.asList(vehicle)));

        mockMvc.perform(get("/vehicles/filterByMaker").param("maker", "TOYOTA"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.vehicles").isArray())
                .andExpect(jsonPath("$.vehicles[0].model").value("Model 1"));
    }

    @Test
    @WithMockUser
    void getVehiclesByMaker_shouldReturnBadRequest_WhenMakerInvalid() throws Exception {
        mockMvc.perform(get("/vehicles/filterByMaker").param("maker", "InvalidMaker"))
                .andExpect(status().isBadRequest());
    }

    @Test
    @WithMockUser
    void getModelsByMaker_shouldReturnModels_WhenMakerExists() throws Exception {
        Vehicle vehicle1 = Vehicle.builder().id(1L).model("Model 1").maker(MakerEnum.TOYOTA).type(VehicleTypeEnum.SEDAN).build();
        Vehicle vehicle2 = Vehicle.builder().id(2L).model("Model 2").maker(MakerEnum.TOYOTA).type(VehicleTypeEnum.SUV).build();
        when(vehicleService.filterVehiclesByMaker(MakerEnum.TOYOTA)).thenReturn(Optional.of(Arrays.asList(vehicle1, vehicle2)));

        mockMvc.perform(get("/vehicles/modelsByMaker").param("maker", "TOYOTA"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$").isArray())
                .andExpect(jsonPath("$[0]").value("Model 1"))
                .andExpect(jsonPath("$[1]").value("Model 2"));
    }

    @Test
    @WithMockUser
    void getModelsByMaker_shouldReturnBadRequest_WhenMakerInvalid() throws Exception {
        mockMvc.perform(get("/vehicles/modelsByMaker").param("maker", "InvalidMaker"))
                .andExpect(status().isBadRequest());
    }
}
