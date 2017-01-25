using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Network network = new WirelessNetwork();
            MonitoringSystem monitoringSystem = new MonitoringSystem();
            Adapter adapter = new WifiAdapter(network, 0x7FFFFFF0, 0x7FFFFFF1);
            Service service = new Service();
            monitoringSystem.AddNetwork(network, adapter);
            monitoringSystem.CurrentNetwork = monitoringSystem.Networks[0];
            Machine machineA = new Machine(service, new List<Adapter>(new Adapter[] { new WifiAdapter(network, 0x7FFFFFF0, 0x7FFFFFF2) }));
            Machine machineB = new Machine(service, new List<Adapter>(new Adapter[] { new WifiAdapter(network, 0x7FFFFFF0, 0x7FFFFFF3) }));
            network.AddWatchers(new List<IWatcher>(machineA.Adapters.OfType<IWatcher>()));
            network.AddWatchers(new List<IWatcher>(machineB.Adapters.OfType<IWatcher>()));
            network.AddWatcher(adapter);
            List<Machine> machines = monitoringSystem.Machines;

            Console.WriteLine("Machines in network:");
            machines.ForEach((machine) => { Console.WriteLine(machine); });
            Console.ReadLine();

            
        }
    }
}
