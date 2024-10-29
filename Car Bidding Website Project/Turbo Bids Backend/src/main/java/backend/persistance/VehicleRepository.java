package backend.persistance;

import backend.persistance.entity.VehicleEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface VehicleRepository extends JpaRepository<VehicleEntity, Long> {

    @Override
    VehicleEntity save(VehicleEntity vehicle);

    @Override
    void deleteById(Long vehicleId);

    @Override
    List<VehicleEntity> findAll();

    @Override
    Optional<VehicleEntity> findById(Long vehicleId);

    @Query(value = "SELECT * FROM vehicles WHERE maker = :maker", nativeQuery = true)
    List<VehicleEntity> findByMakerNative(@Param("maker") String maker);

    @Query(value = "SELECT * FROM vehicles WHERE type = :type", nativeQuery = true)
    List<VehicleEntity> findByTypeNative(@Param("type") String type);

    @Query(value = "SELECT * FROM vehicles WHERE maker = :maker AND model = :model", nativeQuery = true)
    Optional<VehicleEntity> findByMakerAndModelNative(@Param("maker") String maker, @Param("model") String model);
}
