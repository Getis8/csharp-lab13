namespace Lab_13;

public class CommandCenter
{
    private readonly Stack<ICarCommand> _history = new();
    private readonly Dictionary<string, Queue<ICarCommand>> _pending = new();
    private readonly List<string> _journal = new();

    public IReadOnlyList<string> Journal => _journal;

    public void Send(ICarCommand command)
    {
        Log.Write("Центр команд",
            $"{command.Sender} надіслав «{command.Name}» для {command.Target.Plate}");

        if (!command.Target.IsOnline)
        {
            var queue = GetQueue(command.Target.Plate);
            queue.Enqueue(command);
            Log.Write("Центр команд",
                $"Авто {command.Target.Plate} поза зв'язком: «{command.Name}» поставлено в чергу (у черзі: {queue.Count})");
            return;
        }

        Run(command);
    }

    // Зміна зв'язку: коли авто повернулося, виконуємо накопичене
    public void SetConnection(RemoteCar car, bool online)
    {
        car.SetConnection(online);
        if (!online) return;

        var queue = GetQueue(car.Plate);
        if (queue.Count > 0)
            Log.Write("Центр команд", $"Виконуємо відкладені команди для {car.Plate}: {queue.Count}");

        while (queue.Count > 0)
            Run(queue.Dequeue());
    }

    public void UndoLast()
    {
        if (!_history.TryPeek(out var command))
        {
            Log.Write("Центр команд", "Немає команд для скасування");
            return;
        }

        if (!command.Target.IsOnline)
        {
            Log.Write("Центр команд",
                $"Не можна скасувати «{command.Name}»: авто {command.Target.Plate} поза зв'язком");
            return;
        }

        _history.Pop();
        Log.Write("Центр команд", $"Скасовуємо останню команду «{command.Name}» для {command.Target.Plate}");
        command.Undo();
        AddJournal($"{command.Sender}: «{command.Name}» для {command.Target.Plate} - СКАСОВАНО");
    }

    public void PrintJournal()
    {
        Log.Write("Журнал", "Журнал команд:");
        foreach (var entry in _journal)
            Log.Write("Журнал", entry);
    }

    private void Run(ICarCommand command)
    {
        Log.Write("Центр команд", $"Виконуємо «{command.Name}» на {command.Target.Plate}");
        command.Execute();
        _history.Push(command);
        AddJournal($"{command.Sender}: «{command.Name}» для {command.Target.Plate} - виконано");
    }

    private Queue<ICarCommand> GetQueue(string plate)
    {
        if (!_pending.TryGetValue(plate, out var queue))
            _pending[plate] = queue = new Queue<ICarCommand>();
        return queue;
    }

    private void AddJournal(string text) => _journal.Add($"{_journal.Count + 1}. {text}");
}