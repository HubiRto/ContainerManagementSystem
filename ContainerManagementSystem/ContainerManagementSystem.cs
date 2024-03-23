namespace ContainerManagementSystem;

public class ContainerManagementSystem
{
    public abstract class Container
    {
        public string SerialNumber { get; set; }
        public double LoadCapacity { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public double CurrentLoad { get; protected set; }

        public abstract void LoadCargo(double cargoWeight);
        public abstract void UnloadCargo();
        public string GetInfo()
        {
            return $"Numer seryjny: {SerialNumber}, Masa ładunku: {Weight} kg";
        }
    }

    public class LiquidContainer : Container
    {
        public double MaxCapacity { get; set; }

        public override void LoadCargo(double cargoWeight)
        {
            if (cargoWeight > MaxCapacity)
                throw new OverfillException();

            CurrentLoad += cargoWeight;
        }

        public override void UnloadCargo()
        {
            CurrentLoad = 0.05 * MaxCapacity;
        }
    }
    
    public class GasContainer : Container, IHazardNotifier
    {
        public double Pressure { get; set; }

        public override void LoadCargo(double cargoWeight)
        {
            if (cargoWeight > LoadCapacity)
                throw new OverfillException();

            CurrentLoad += cargoWeight;
        }

        public override void UnloadCargo()
        {
            CurrentLoad *= 0.95;
        }

        public void NotifyDanger(string containerNumber)
        {
            Console.WriteLine($"Container {containerNumber} has encountered a hazardous situation.");
        }
    }
    
    public class RefrigeratedContainer : Container
    {
        public string ProductType { get; set; }
        public double Temperature { get; set; }

        public override void LoadCargo(double cargoWeight)
        {
            if (cargoWeight > LoadCapacity)
                throw new OverfillException();

            CurrentLoad += cargoWeight;
        }

        public override void UnloadCargo()
        {
            CurrentLoad = 0;
        }
    }
    
    public interface IHazardNotifier
    {
        void NotifyDanger(string containerNumber);
    }
    
    public class Ship
    {
        public List<Container> Containers { get; set; }
        public double MaxLoadWeight { get; set; }

        public Ship()
        {
            Containers = new List<Container>();
        }

        public void LoadContainer(Container container)
        {
            double currentLoadWeight = 0;
            foreach (var c in Containers)
            {
                currentLoadWeight += c.CurrentLoad;
            }

            if (currentLoadWeight + container.CurrentLoad > MaxLoadWeight)
                throw new OverloadException();

            Containers.Add(container);
        }

        public void UnloadContainer(Container container)
        {
            Containers.Remove(container);
        }
        public void ReplaceContainer(Container oldContainer, Container newContainer)
        {
            int index = Containers.IndexOf(oldContainer);
            if (index != -1)
            {
                Containers[index] = newContainer;
            }
        }
        public string GetInfo()
        {
            string info = $"Maksymalna waga statku: {MaxLoadWeight} kg\n";
            info += "Lista kontenerów na statku:\n";
            foreach (var container in Containers)
            {
                info += container.GetInfo() + "\n";
            }
            return info;
        }
    }
    
    public class OverloadException : Exception
    {
        public OverloadException() : base("Ship overload!")
        {
        }
    }
    
    public class OverfillException : Exception
    {
        public OverfillException() : base("Container overfill!")
        {
        }
    }
}