namespace Lab_13;

public static partial class DemoRunner
{
    public static void RunChain()
    {
        Log.Section("Завдання 2. Chain of Responsibility: допуск клієнта до поїздки");

        var service = new TripAdmissionService();

        // Налаштування під час виконання: різний склад і порядок
        service.SetChain(VehicleCategory.Standard,
            new LicenseCheck(), new DebtCheck(), new FundsCheck(), new BlockCheck());

        service.SetChain(VehicleCategory.Premium,
            new LicenseCheck(), new ExperienceCheck(3), new DebtCheck(), new FundsCheck(), new BlockCheck());

        var standardCar = new Vehicle("CE1234AA", "Renault Zoe", VehicleCategory.Standard);
        var premiumCar = new Vehicle("CE7777BB", "BMW 5", VehicleCategory.Premium);

        // 1. Усі перевірки для звичайного авто
        Log.Write("Сценарій", "1. Клієнт проходить усі перевірки");
        var good = new Client("Ткачук", true, 5, 30, 0m, 1500m, false);
        service.Admit(new TripRequest(good, standardCar, 500m));

        // 2. Заборгованість: зупинка на DebtCheck, наступні не виконуються
        Log.Write("Сценарій", "2. Клієнт із заборгованістю");
        var debtor = new Client("Мельник", true, 6, 35, 120m, 1500m, false);
        service.Admit(new TripRequest(debtor, standardCar, 500m));

        // 3. Стаж 1 рік, преміальне авто: відмова на стажі
        Log.Write("Сценарій", "3. Клієнт зі стажем 1 рік бере преміальне авто");
        var novice = new Client("Шевченко", true, 1, 24, 0m, 5000m, false);
        service.Admit(new TripRequest(novice, premiumCar, 2000m));

        // 4. Додаємо нову перевірку (вік для спорткарів): існуючі класи не змінюються
        Log.Write("Сценарій", "4. Нова перевірка віку для спорткарів, існуючі класи не змінено");
        service.SetChain(VehicleCategory.Sports,
            new LicenseCheck(), new AgeCheck(25), new ExperienceCheck(3),
            new DebtCheck(), new FundsCheck(), new BlockCheck());

        var sportsCar = new Vehicle("CE0001SP", "Porsche 911", VehicleCategory.Sports);
        var young = new Client("Коваленко", true, 4, 22, 0m, 9000m, false);
        service.Admit(new TripRequest(young, sportsCar, 3000m));
    }
}