namespace Lab_13;

public class LicenseCheck : CheckHandler
{
    public override string Name => "посвідчення водія";

    protected override string? Validate(TripRequest r) =>
        r.Client.LicenseValid ? null : "Посвідчення водія не підтверджено";
}

public class ExperienceCheck : CheckHandler
{
    private readonly int _minYears;
    public ExperienceCheck(int minYears) => _minYears = minYears;

    public override string Name => "стаж водіння";

    protected override string? Validate(TripRequest r) =>
        r.Client.ExperienceYears >= _minYears
            ? null
            : $"Стаж {r.Client.ExperienceYears} р., потрібно щонайменше {_minYears} р.";
}

public class AgeCheck : CheckHandler
{
    private readonly int _minAge;
    public AgeCheck(int minAge) => _minAge = minAge;

    public override string Name => "вік";

    protected override string? Validate(TripRequest r) =>
        r.Client.Age >= _minAge
            ? null
            : $"Вік {r.Client.Age} р., потрібно щонайменше {_minAge} р.";
}

public class DebtCheck : CheckHandler
{
    public override string Name => "заборгованість";

    protected override string? Validate(TripRequest r) =>
        r.Client.Debt <= 0 ? null : $"Є заборгованість {r.Client.Debt:F0} грн";
}

public class FundsCheck : CheckHandler
{
    public override string Name => "кошти на картці";

    protected override string? Validate(TripRequest r) =>
        r.Client.CardBalance >= r.Deposit
            ? null
            : $"На картці {r.Client.CardBalance:F0} грн, потрібно {r.Deposit:F0} грн";
}

public class BlockCheck : CheckHandler
{
    public override string Name => "блокування за порушення";

    protected override string? Validate(TripRequest r) =>
        r.Client.IsBlocked ? $"Клієнта заблоковано: {r.Client.BlockReason}" : null;
}