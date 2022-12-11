namespace OnlineShop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OnlineShop.Common.Constants;
    using OnlineShop.Models.Products.Components;
    using OnlineShop.Models.Products.Computers;
    using OnlineShop.Models.Products.Peripherals;

    public class Controller : IController
    {
        private List<IComputer> computers;
        private List<IComponent> components;
        private List<IPeripheral> peripherals;

        public Controller()
        {
            computers = new List<IComputer>();
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            var computer = GetComputer(computerId);

            if (components.Any(x => x.Id == id))
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);

            IComponent component = componentType switch
            {
                nameof(CentralProcessingUnit) => new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation),
                nameof(Motherboard) => new Motherboard(id, manufacturer, model, price, overallPerformance, generation),
                nameof(PowerSupply) => new PowerSupply(id, manufacturer, model, price, overallPerformance, generation),
                nameof(RandomAccessMemory) => new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation),
                nameof(SolidStateDrive) => new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation),
                nameof(VideoCard) => new VideoCard(id, manufacturer, model, price, overallPerformance, generation),
                _ => throw new ArgumentException(ExceptionMessages.InvalidComponentType)
            };

            computer.AddComponent(component);
            components.Add(component);

            return string.Format(SuccessMessages.AddedComponent, component.GetType().Name, component.Id, computer.Id);
        }

        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            if (computers.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }

            IComputer computer = computerType switch
            {
                nameof(DesktopComputer) => new DesktopComputer(id, manufacturer, model, price),
                nameof(Laptop) => new Laptop(id, manufacturer, model, price),
                _ => throw new ArgumentException(ExceptionMessages.InvalidComputerType)
            };

            computers.Add(computer);

            return string.Format(SuccessMessages.AddedComputer, id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            var pc = GetComputer(computerId);

            if (peripherals.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }

            IPeripheral per = peripheralType switch
            {
                nameof(Headset) => new Headset(id, manufacturer, model, price, overallPerformance, connectionType),
                nameof(Keyboard) => new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType),
                nameof(Monitor) => new Monitor(id, manufacturer, model, price, overallPerformance, connectionType),
                nameof(Mouse) => new Mouse(id, manufacturer, model, price, overallPerformance, connectionType),
                _ => throw new ArgumentException(ExceptionMessages.InvalidPeripheralType)
            };

            pc.AddPeripheral(per);
            peripherals.Add(per);

            return string.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }

        public string BuyBest(decimal budget)
        {
            var bestPC = computers
                .OrderByDescending(x => x.OverallPerformance)
                .Where(x => x.Price <= budget)
                .FirstOrDefault();

            if (bestPC == null)
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));

            computers.Remove(bestPC);

            return bestPC.ToString();
        }

        public string BuyComputer(int id)
        {
            var pc = GetComputer(id);

            computers.Remove(pc);

            return pc.ToString();
        }

        public string GetComputerData(int id)
        {
            var pc = GetComputer(id);

            return pc.ToString();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            var computer = GetComputer(computerId);

            var component = computer.RemoveComponent(componentType);

            components.Remove(component);

            return string.Format(SuccessMessages.RemovedComponent, componentType, component.Id);

        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            var pc = GetComputer(computerId);

            var per = pc.RemovePeripheral(peripheralType);

            peripherals.Remove(per);

            return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, per.Id);
        }

        private IComputer GetComputer(int id)
        {
            var computer = computers.FirstOrDefault(x => x.Id == id);

            if (computer == null)
                    throw new ArgumentException(ExceptionMessages.NotExistingComputerId);

            return computer;
        }
    }
}
