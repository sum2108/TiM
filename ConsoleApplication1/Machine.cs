using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Machine : IWatcher
    {
        Service service;
        List<Adapter> adapters;

        public List<Adapter> Adapters
        {
            get
            {
                return this.adapters;
            }
        }
        public Machine(Service service, List<Adapter> adapters)
        {
            this.service = service;
            this.adapters = adapters;
            this.adapters.ForEach((adapter) => { adapter.AddWatcher(this); });
        }

        public void HandlePacket(Adapter adapter, Packet packet)
        {
            Console.WriteLine(this + " handle Packet" + packet);
            switch (packet.Type)
            {
                case PacketType.ICMP_ECHO_REQUEST:
                    adapter.SendPacket(new Packet(PacketType.ICMP_ECHO_REPLY, new Header(adapter.IP, packet.Header.src), this));
                    break;
                default:
                    break;
            }
        }

        public void SendPacket(Packet packet)
        {
            //--------------------------------------
            this.service.Where(this.adapters, packet);
        }

        public override String ToString()
        {
            return "Machine: " + this.GetHashCode();
        }

        public void Notify(Observable observable, object data)
        {
            HandlePacket(observable as Adapter, data as Packet);
        }
    }
}
