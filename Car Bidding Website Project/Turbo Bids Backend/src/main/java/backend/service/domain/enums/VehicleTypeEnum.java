package backend.service.domain.enums;

public enum VehicleTypeEnum {
    SEDAN,
    SUV,
    TRUCK,
    COUPE,
    CONVERTIBLE,
    VAN,
    WAGON,
    SPORT,
    OTHER;

    public static VehicleTypeEnum fromString(String type) {
        for (VehicleTypeEnum vt : VehicleTypeEnum.values()) {
            if (vt.name().equalsIgnoreCase(type)) {
                return vt;
            }
        }
        throw new IllegalArgumentException("No constant with text " + type + " found");
    }
}
