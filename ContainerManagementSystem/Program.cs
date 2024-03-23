class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Witaj w aplikacji do zarządzania załadunkiem kontenerów!");
        ContainerManagementSystem.ContainerManagementSystem.Ship ship =
            new ContainerManagementSystem.ContainerManagementSystem.Ship();
        ship.MaxLoadWeight = 10000;
        
        while (true)
        {
            Console.WriteLine("\nWybierz opcję:");
            Console.WriteLine("1. Stworzenie kontenera danego typu");
            Console.WriteLine("2. Załadowanie ładunku do danego kontenera");
            Console.WriteLine("3. Załadowanie kontenera na statek");
            Console.WriteLine("4. Załadowanie listy kontenerów na statek");
            Console.WriteLine("5. Usunięcie kontenera ze statku");
            Console.WriteLine("6. Rozładowanie kontenera");
            Console.WriteLine("7. Zastąpienie kontenera na statku o danym numerze innym kontenerem");
            Console.WriteLine("8. Możliwość przeniesienie kontenera między dwoma statkami");
            Console.WriteLine("9. Wypisanie informacji o danym kontenerze");
            Console.WriteLine("10. Wypisanie informacji o danym statku i jego ładunku");
            Console.WriteLine("11. Zakończ program");

            int option;
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Wprowadzono nieprawidłową opcję.");
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine("Wybierz typ kontenera (L - na płyny, G - na gaz, C - chłodniczy):");
                    string containerType = Console.ReadLine().ToUpper();
                    ContainerManagementSystem.ContainerManagementSystem.Container container;
                    switch (containerType)
                    {
                        case "L":
                            container = new ContainerManagementSystem.ContainerManagementSystem.LiquidContainer();
                            break;
                        case "G":
                            container = new ContainerManagementSystem.ContainerManagementSystem.GasContainer();
                            break;
                        case "C":
                            container = new ContainerManagementSystem.ContainerManagementSystem.RefrigeratedContainer();
                            break;
                        default:
                            Console.WriteLine("Nieznany typ kontenera.");
                            continue;
                    }

                    Console.WriteLine("Podaj masę ładunku:");
                    double cargoWeight;
                    if (!double.TryParse(Console.ReadLine(), out cargoWeight))
                    {
                        Console.WriteLine("Wprowadzono nieprawidłową masę ładunku.");
                        continue;
                    }

                    container.LoadCargo(cargoWeight);
                    Console.WriteLine($"Kontener utworzony: {container.SerialNumber}");
                    break;
                case 2:
                    Console.WriteLine("Podaj numer seryjny kontenera:");
                    string containerNumber = Console.ReadLine();
                    Console.WriteLine("Podaj masę ładunku:");
                    double cargoWeight2;
                    if (!double.TryParse(Console.ReadLine(), out cargoWeight2))
                    {
                        Console.WriteLine("Wprowadzono nieprawidłową masę ładunku.");
                        continue;
                    }
                    
                    ContainerManagementSystem.ContainerManagementSystem.Container selectedContainer =
                        ship.Containers.Find(c => c.SerialNumber == containerNumber);
                    if (selectedContainer == null)
                    {
                        Console.WriteLine("Nie znaleziono kontenera o podanym numerze.");
                        continue;
                    }

                    selectedContainer.LoadCargo(cargoWeight2);
                    Console.WriteLine($"Ładunek załadowany do kontenera {containerNumber}.");
                    break;
                case 3:
                    Console.WriteLine("Podaj numer seryjny kontenera:");
                    string containerNumber3 = Console.ReadLine();
                    ContainerManagementSystem.ContainerManagementSystem.Container selectedContainer3 =
                        ship.Containers.Find(c => c.SerialNumber == containerNumber3);
                    if (selectedContainer3 == null)
                    {
                        Console.WriteLine("Nie znaleziono kontenera o podanym numerze.");
                        continue;
                    }

                    try
                    {
                        ship.LoadContainer(selectedContainer3);
                        Console.WriteLine($"Kontener {containerNumber3} załadowany na statek.");
                    }
                    catch (ContainerManagementSystem.ContainerManagementSystem.OverloadException)
                    {
                        Console.WriteLine("Przekroczono maksymalną wagę statku!");
                    }

                    break;
                case 4:
                    Console.WriteLine("Podaj ilość kontenerów do załadowania:");
                    int count;
                    if (!int.TryParse(Console.ReadLine(), out count) || count < 1)
                    {
                        Console.WriteLine("Wprowadzono nieprawidłową ilość kontenerów.");
                        continue;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Kontener {i + 1}:");
                        Console.WriteLine("Podaj numer seryjny kontenera:");
                        string containerNumber4 = Console.ReadLine();
                        ContainerManagementSystem.ContainerManagementSystem.Container selectedContainer4 =
                            ship.Containers.Find(c => c.SerialNumber == containerNumber4);
                        if (selectedContainer4 == null)
                        {
                            Console.WriteLine(
                                $"Nie znaleziono kontenera o numerze {containerNumber4}. Kontynuowanie.");
                            continue;
                        }

                        try
                        {
                            ship.LoadContainer(selectedContainer4);
                            Console.WriteLine($"Kontener {containerNumber4} załadowany na statek.");
                        }
                        catch (ContainerManagementSystem.ContainerManagementSystem.OverloadException)
                        {
                            Console.WriteLine("Przekroczono maksymalną wagę statku! Kontynuowanie.");
                        }
                    }

                    break;
                case 5:
                    Console.WriteLine("Podaj numer seryjny kontenera:");
                    string containerNumber5 = Console.ReadLine();
                    ContainerManagementSystem.ContainerManagementSystem.Container selectedContainer5 =
                        ship.Containers.Find(c => c.SerialNumber == containerNumber5);
                    if (selectedContainer5 == null)
                    {
                        Console.WriteLine("Nie znaleziono kontenera o podanym numerze.");
                        continue;
                    }

                    ship.UnloadContainer(selectedContainer5);
                    Console.WriteLine($"Kontener {containerNumber5} usunięty ze statku.");
                    break;
                case 6:
                    Console.WriteLine("Podaj numer seryjny kontenera:");
                    string containerNumber6 = Console.ReadLine();
                    ContainerManagementSystem.ContainerManagementSystem.Container selectedContainer6 = ship.Containers.Find(c => c.SerialNumber == containerNumber6);
                    if (selectedContainer6 == null)
                    {
                        Console.WriteLine("Nie znaleziono kontenera o podanym numerze.");
                        continue;
                    }

                    ship.UnloadContainer(selectedContainer6);
                    Console.WriteLine($"Kontener {containerNumber6} rozładowany.");
                    break;
                case 7:
                    Console.WriteLine("Podaj numer seryjny kontenera na statku:");
                    string oldContainerNumber7 = Console.ReadLine();
                    ContainerManagementSystem.ContainerManagementSystem.Container oldContainer7 = ship.Containers.Find(c => c.SerialNumber == oldContainerNumber7);
                    if (oldContainer7 == null)
                    {
                        Console.WriteLine("Nie znaleziono kontenera o podanym numerze na statku.");
                        continue;
                    }

                    Console.WriteLine("Podaj numer seryjny nowego kontenera:");
                    string newContainerNumber7 = Console.ReadLine();
                    ContainerManagementSystem.ContainerManagementSystem.Container newContainer7 = ship.Containers.Find(c => c.SerialNumber == newContainerNumber7);
                    if (newContainer7 == null)
                    {
                        Console.WriteLine("Nie znaleziono nowego kontenera o podanym numerze.");
                        continue;
                    }

                    ship.ReplaceContainer(oldContainer7, newContainer7);
                    Console.WriteLine(
                        $"Kontener {oldContainerNumber7} został zastąpiony przez kontener {newContainerNumber7}.");
                    break;
                case 8:
                    Console.WriteLine(
                        "Funkcja przenoszenia kontenera między dwoma statkami w trakcie implementacji...");
                    break;
                case 9:
                    Console.WriteLine("Podaj numer seryjny kontenera:");
                    string containerNumber9 = Console.ReadLine();
                    ContainerManagementSystem.ContainerManagementSystem.Container selectedContainer9 =
                        ship.Containers.Find(c => c.SerialNumber == containerNumber9);
                    if (selectedContainer9 == null)
                    {
                        Console.WriteLine("Nie znaleziono kontenera o podanym numerze.");
                        continue;
                    }

                    Console.WriteLine(selectedContainer9.GetInfo());
                    break;
                case 10:
                    Console.WriteLine(ship.GetInfo());
                    break;
                case 11:
                    Console.WriteLine("Zakończono program.");
                    return;
                default:
                    Console.WriteLine("Wybrano nieprawidłową opcję.");
                    break;
            }
        }
    }
}