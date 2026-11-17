namespace Lab_13;

public enum VehicleCategory { Standard, Premium, Sports }

public record Vehicle(string Plate, string Model, VehicleCategory Category);

public record Client(
    string Name,
    bool LicenseValid,
    int ExperienceYears,
    int Age,
    decimal Debt,
    decimal CardBalance,
    bool IsBlocked,
    string BlockReason = "");

public record TripRequest(Client Client, Vehicle Vehicle, decimal Deposit);

public record CheckResult(bool Passed, string Reason = "", string StoppedBy = "");