namespace Lab_13;

public class RemoteCar
{
    public string Plate { get; }
    public bool IsOnline { get; private set; }
    public bool DoorsLocked { get; private set; } = true;
    public bool AlarmOn { get; private set; }
    public bool EngineBlocked { get; private set; }

    public RemoteCar(string plate, bool online = true)
    {
        Plate = plate;
        IsOnline = online;
    }

    public void SetConnection(bool online)
    {
        IsOnline = online;
        Log.Write($"Авто {Plate}", online ? "Зв'язок відновлено" : "Зв'язок втрачено");
    }

    public void LockDoors()
    {
        DoorsLocked = true;
        Log.Write($"Авто {Plate}", "Двері закрито");
    }

    public void UnlockDoors()
    {
        DoorsLocked = false;
        Log.Write($"Авто {Plate}", "Двері відкрито");
    }

    public void SetAlarm(bool on)
    {
        AlarmOn = on;
        Log.Write($"Авто {Plate}", on ? "Аварійну сигналізацію увімкнено" : "Аварійну сигналізацію вимкнено");
    }

    public void BlockEngine()
    {
        EngineBlocked = true;
        Log.Write($"Авто {Plate}", "Двигун заблоковано");
    }

    public void UnblockEngine()
    {
        EngineBlocked = false;
        Log.Write($"Авто {Plate}", "Двигун розблоковано");
    }
}