package backend.service.domain;

import lombok.Builder;
import lombok.Data;

@Builder
@Data
public class Bid {

    private Long id;

    private User account;

    private double amount;
}
