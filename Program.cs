using Lab_13;

Console.OutputEncoding = System.Text.Encoding.UTF8;

while (true)
{
    Console.WriteLine("\n=== Лабораторна робота №13 (варіант 8, поведінкові шаблони) ===");
    Console.WriteLine("1 - Command: віддалені команди авто");
    Console.WriteLine("2 - Chain of Responsibility: допуск клієнта до поїздки");
    Console.WriteLine("0 - Вихід");
    Console.Write("Ваш вибір: ");

    switch (Console.ReadLine()?.Trim())
    {
        case "1": DemoRunner.RunCommand(); break;
        case "2": DemoRunner.RunChain(); break;
        case "0": return;
        default: Console.WriteLine("Невірний вибір"); break;
    }
}