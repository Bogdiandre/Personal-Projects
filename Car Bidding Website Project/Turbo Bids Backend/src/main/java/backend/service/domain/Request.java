package backend.service.domain;

import backend.service.domain.enums.RequestEnum;
import backend.service.exception.InvalidRequestException;
import lombok.Builder;
import lombok.Data;

import java.time.LocalDateTime;

@Builder
@Data
public class Request {

    private Long id;

    private User seller;

    private Vehicle vehicle;

    private Integer milage;

    private String details;

    private RequestEnum status;

    private LocalDateTime start;

    private LocalDateTime end;

    private Integer maxPrice;

    public void setStartWithSpecificHour(int year, int month, int dayOfMonth, int hour, int minute, int second) {
        this.start = LocalDateTime.of(year, month, dayOfMonth, hour, minute, second);
    }

    public void setEndWithSpecificHour(int year, int month, int dayOfMonth, int hour, int minute, int second) {
        this.end = LocalDateTime.of(year, month, dayOfMonth, hour, minute, second);
    }

    public Listing accept() throws InvalidRequestException {
        if (this.status == RequestEnum.PENDING) {
            this.status = RequestEnum.ACCEPTED;
            return new Listing(this);
        } else {
            throw new InvalidRequestException();
        }
    }

    public boolean decline() throws InvalidRequestException {
        if (this.status == RequestEnum.PENDING) {
            this.status = RequestEnum.UNACCEPTED;
            return true;
        } else {
            throw new InvalidRequestException();
        }
    }
}
