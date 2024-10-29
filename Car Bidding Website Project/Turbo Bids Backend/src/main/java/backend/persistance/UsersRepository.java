package backend.persistance;

import backend.persistance.entity.UserEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Repository
public interface UsersRepository extends JpaRepository<UserEntity, Long> {

    @Override
    List<UserEntity> findAll();

    @Override
    Optional<UserEntity> findById(Long userId);

    @Query("SELECT u FROM UserEntity u WHERE u.email = :email")
    Optional<UserEntity> findByEmail(@Param("email") String email);

    @Transactional
    @Modifying
    @Query("DELETE FROM UserEntity u WHERE u.id = :userId")
    void deleteById(@Param("userId") Long userId);

    @Query("SELECT u FROM UserEntity u WHERE u.role = 'CLIENT'")
    List<UserEntity> findAllClients();
}
