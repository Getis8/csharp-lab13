namespace Lab_13;

public class TripAdmissionService
{
    private readonly Dictionary<VehicleCategory, CheckHandler> _chains = new();

    // Склад і порядок задаються тут, під час роботи програми
    public void SetChain(VehicleCategory category, params CheckHandler[] handlers)
    {
        if (handlers.Length == 0)
            throw new ArgumentException("Ланцюг не може бути порожнім", nameof(handlers));

        for (int i = 0; i < handlers.Length - 1; i++)
            handlers[i].SetNext(handlers[i + 1]);

        _chains[category] = handlers[0];
    }

    public CheckResult Admit(TripRequest request)
    {
        if (!_chains.TryGetValue(request.Vehicle.Category, out var first))
            throw new InvalidOperationException($"Ланцюг для {request.Vehicle.Category} не налаштовано");

        Log.Write("Допуск",
            $"Запит {request.Client.Name} на {request.Vehicle.Model} ({request.Vehicle.Category}) передано в ланцюг, перша ланка: «{first.Name}»");

        var result = first.Handle(request);

        Log.Write("Допуск", result.Passed
            ? $"{request.Client.Name} допущено до поїздки"
            : $"{request.Client.Name} не допущено: зупинено на перевірці «{result.StoppedBy}», причина: {result.Reason}");

        return result;
    }
}