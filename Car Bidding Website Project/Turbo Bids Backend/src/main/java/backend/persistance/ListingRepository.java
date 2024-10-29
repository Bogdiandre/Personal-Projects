package backend.persistance;

import backend.persistance.entity.ListingEntity;
import backend.persistance.entity.UserEntity;
import backend.service.domain.enums.MakerEnum;
import backend.service.domain.enums.ListingEnum;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface ListingRepository extends JpaRepository<ListingEntity, Long> {

    @Override
    ListingEntity save(ListingEntity listing);

    @Override
    List<ListingEntity> findAll();

    @Query("SELECT l FROM ListingEntity l WHERE l.id = :listingId")
    Optional<ListingEntity> findById(@Param("listingId") Long listingId);

    @Query("SELECT l.requestEntity.sellerEntity FROM ListingEntity l WHERE l.id = :listingId")
    Optional<UserEntity> getSeller(@Param("listingId") Long listingId);

    @Query("SELECT AVG(CASE WHEN l.status = backend.service.domain.enums.ListingEnum.BIDBUY THEN " +
            "(SELECT MAX(b.amount) FROM BidEntity b WHERE b.listingEntity.id = l.id) " +
            "WHEN l.status = backend.service.domain.enums.ListingEnum.BUYOUT THEN l.requestEntity.maxPrice END) " +
            "FROM ListingEntity l " +
            "WHERE l.requestEntity.vehicleEntity.maker = :maker " +
            "AND l.requestEntity.vehicleEntity.model = :model " +
            "AND l.status IN (backend.service.domain.enums.ListingEnum.BIDBUY, backend.service.domain.enums.ListingEnum.BUYOUT)")
    int findAveragePriceOfSoldVehicle(@Param("maker") MakerEnum maker, @Param("model") String model);

    @Query("SELECT DISTINCT b.accountEntity FROM BidEntity b WHERE b.listingEntity.id = :listingId")
    List<UserEntity> findUniqueBiddersByListingId(@Param("listingId") Long listingId);

    @Query("SELECT l FROM ListingEntity l WHERE l.requestEntity.vehicleEntity.maker = :maker AND l.requestEntity.vehicleEntity.model = :model AND l.status = backend.service.domain.enums.ListingEnum.OPEN")
    List<ListingEntity> findOpenListingsByMakerAndModel(@Param("maker") MakerEnum maker, @Param("model") String model);

    // New method to find OPEN listings by sellerId
    // Updated method to find all listings by sellerId
    @Query("SELECT l FROM ListingEntity l WHERE l.requestEntity.sellerEntity.id = :sellerId")
    List<ListingEntity> findAllListingsBySellerId(@Param("sellerId") Long sellerId);
}
