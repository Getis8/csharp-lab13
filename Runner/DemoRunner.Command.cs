namespace Lab_13;

public static partial class DemoRunner
{
    public static void RunCommand()
    {
        Log.Section("Завдання 1. Command: віддалені команди авто");

        var car = new RemoteCar("CE1234AA");
        var center = new CommandCenter();

        // 1. «відкрити» і «заблокувати двигун» → виконані
        center.Send(new OpenDoorsCommand(car, "Клієнт Ткачук"));
        center.Send(new BlockEngineCommand(car, "Служба підтримки"));

        // 2. скасувати блокування двигуна
        center.UndoLast();

        // 3. зв'язок втрачено, 2 команди → у черзі
        center.SetConnection(car, false);
        center.Send(new CloseDoorsCommand(car, "Клієнт Ткачук"));
        center.Send(new AlarmCommand(car, "Служба підтримки"));

        // 4. зв'язок повернувся → команди виконано
        center.SetConnection(car, true);

        // 5. журнал
        center.PrintJournal();
    }
}