package backend.persistance;

import backend.persistance.entity.BidEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface BidRepository extends JpaRepository<BidEntity, Long> {

    @Override
    List<BidEntity> findAll();

    @Override
    BidEntity save(BidEntity bid);

    void deleteById(Long bidId);

    @Query("SELECT b FROM BidEntity b WHERE b.listingEntity.id = :listingId ORDER BY b.amount DESC")
    List<BidEntity> getAllBidsForListing(@Param("listingId") Long listingId);

    @Query(value = "SELECT * FROM bids b WHERE b.listing_id = :listingId ORDER BY b.amount DESC LIMIT 1", nativeQuery = true)
    BidEntity getHighestBidForListing(@Param("listingId") Long listingId);

}
