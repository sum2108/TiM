using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class MonitoringSystem : IWatcher
    {
        Network currentNetwork;
        Dictionary<Network, Adapter> networks;
        List<Machine> machines;
        public void AddNetwork(Network network, Adapter adapter)
        {
            networks.Add(network, adapter);
            adapter.AddWatcher(this);
        }

        public Network CurrentNetwork
        {
            set
            {
                if (networks.Keys.Contains(value))
                {
                    currentNetwork = value;
                }
            }
            get
            {
                return this.currentNetwork;
            }
        }

        public List<Network> Networks 
        {
            get
            {
                return this.networks.Keys.ToList();
            }
        }

        public List<Machine> Machines
        {
            get
            {
                machines = new List<Machine>();
                int nextHost = networks[currentNetwork].Mask + 1;
                while (nextHost < int.MaxValue)
                {
                    networks[currentNetwork].SendPacket(new Packet(PacketType.ICMP_ECHO_REQUEST, new Header(networks[currentNetwork].IP, nextHost++), "Hello"));
                }
                //------------------
                return this.machines;
            }
        }
        public MonitoringSystem()
        {
            networks = new Dictionary<Network, Adapter>();
        }

        public void Notify(Observable observable, object data)
        {
            Packet packet = data as Packet;
            if (observable is Adapter && packet.Data is Machine)
            {
                machines.Add(packet.Data as Machine);
            }
            else if (observable.GetType() == typeof(Network))
            {

            }
        }

        public void SendPacket(PacketType packetType, object data, Machine machine)
        {
            Packet packet = new Packet(packetType, new Header(), data);
            machine.Adapters.ForEach((adapter) => 
            {
                packet.Header = new Header(networks[currentNetwork].IP, adapter.IP);
            });
            networks[currentNetwork].SendPacket(packet);
        }
    }
}
