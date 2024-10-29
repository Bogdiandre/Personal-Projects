package backend.persistance;

import backend.persistance.entity.ImageEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.List;
import java.util.Optional;

public interface ImageRepository extends JpaRepository<ImageEntity, Long> {

    @Override
    ImageEntity save(ImageEntity image);

    @Override
    void deleteById(Long imageId);

    @Override
    List<ImageEntity> findAll();

    @Override
    Optional<ImageEntity> findById(Long imageId);

    @Query("SELECT i FROM ImageEntity i WHERE i.requestEntity.id = :requestId")
    Optional<ImageEntity> findByRequestId(@Param("requestId") Long requestId);
}


