using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Network network = new WirelessNetwork();
            MonitoringSystem monitoringSystem = new MonitoringSystem();
            Adapter adapter = new WifiAdapter(network, 0x7FFFFFF0, 0x7FFFFFF1);//сеть,маска,ip
            Service service = new Service();
            monitoringSystem.AddNetwork(network, adapter);//к monit.system добавляем сети которую можно прослушать
            monitoringSystem.CurrentNetwork = monitoringSystem.Networks[0];//устанавливаем текущую сеть для прослушивания (1ю из всех доступных)

            //создаем 2 машины, устанавливаем сервис, который будет выбирать адаптеры для передачи пакета и список адптеров машин
            Machine machineA = new Machine(service, new List<Adapter>(new Adapter[] { new WifiAdapter(network, 0x7FFFFFF0, 0x7FFFFFF2) }));
            Machine machineB = new Machine(service, new List<Adapter>(new Adapter[] { new WifiAdapter(network, 0x7FFFFFF0, 0x7FFFFFF3) }));
            //к сети подключаем адаптеры (для машины A и B это список всех доступных адаптеров
            network.AddWatchers(new List<IWatcher>(machineA.Adapters.OfType<IWatcher>()));
            network.AddWatchers(new List<IWatcher>(machineB.Adapters.OfType<IWatcher>()));
            //и адаптер для mon.system
            network.AddWatcher(adapter);
            List<Machine> machines = monitoringSystem.Machines;//получаем список машины из сети (по сути кидаем пинг по списку всех достпуных адресов в сети)
            Console.WriteLine("Machines in network:");
            machines.ForEach((machine) => { Console.WriteLine(machine); });
            Console.ReadLine();

            
        }
    }
}
