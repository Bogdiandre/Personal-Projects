package backend.persistance;

import backend.persistance.entity.RequestEntity;
import backend.service.domain.enums.VehicleTypeEnum;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.List;
import java.util.Optional;

public interface RequestRepository extends JpaRepository<RequestEntity, Long> {

    @Override
    RequestEntity save(RequestEntity request);

    @Override
    void deleteById(Long requestId);

    @Override
    List<RequestEntity> findAll();

    @Override
    Optional<RequestEntity> findById(Long requestId);

    @Query("SELECT r FROM RequestEntity r WHERE r.vehicleEntity.id = :vehicleId")
    List<RequestEntity> findRequestByVehicleId(@Param("vehicleId") Long vehicleId);

    @Query("SELECT r FROM RequestEntity r WHERE r.sellerEntity.id = :sellerId AND r.sellerEntity.role = 'CLIENT'")
    List<RequestEntity> findRequestBySellerId(@Param("sellerId") Long sellerId);

    @Query("SELECT r FROM RequestEntity r WHERE r.status = 'Pending'")
    List<RequestEntity> findRequestsByStatusPending();

    @Query("SELECT r FROM RequestEntity r WHERE r.status = 'PENDING' AND r.end > CURRENT_TIMESTAMP")
    List<RequestEntity> findPendingRequestsWithEndAfterNow();

    @Query("SELECT r FROM RequestEntity r WHERE r.sellerEntity.id = :sellerId AND r.status = 'PENDING'")
    List<RequestEntity> findPendingRequestsBySellerId(@Param("sellerId") Long sellerId);

    // New method to get pending requests by vehicle type
    @Query("SELECT r FROM RequestEntity r WHERE r.status = 'PENDING' AND r.end > CURRENT_TIMESTAMP AND r.vehicleEntity.type = :vehicleType")
    List<RequestEntity> findPendingRequestsByVehicleType(@Param("vehicleType") VehicleTypeEnum vehicleType);
}
