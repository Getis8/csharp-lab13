namespace Lab_13;

public abstract class CheckHandler
{
    private CheckHandler? _next;

    public abstract string Name { get; }

    public CheckHandler SetNext(CheckHandler next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        return next;
    }

    // null = перевірку пройдено, інакше причина відмови
    protected abstract string? Validate(TripRequest request);

    public CheckResult Handle(TripRequest request)
    {
        string? failure = Validate(request);

        if (failure is not null)
        {
            Log.Write($"Перевірка: {Name}", $"{request.Client.Name}: ВІДМОВА. {failure}");
            if (_next is not null)
                Log.Write($"Перевірка: {Name}", "Ланцюг перервано, наступні перевірки не виконуються");
            return new CheckResult(false, failure, Name);
        }

        Log.Write($"Перевірка: {Name}", $"{request.Client.Name}: пройдено");
        return _next?.Handle(request) ?? new CheckResult(true);
    }
}