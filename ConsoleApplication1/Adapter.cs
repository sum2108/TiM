using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    abstract class Adapter : Observable, IWatcher
    {
        private Network network;
        private int mask;
        private int ipAddr;

        public int Mask
        {
            get
            {
                return this.mask;
            }
        }

        public int IP
        {
            get
            {
                return this.ipAddr;
            }
        }

        public Adapter(Network network)
        {
            this.network = network;
        }

        public Adapter(Network network, int mask, int ip)
        {
            this.network = network;
            this.mask = mask;
            this.ipAddr = ip;
        }

        public void Notify(Observable observable, Object data)
        {
            Packet packet = data as Packet;
            if (packet.Header.dst == this.ipAddr)
            {
                CallWatchers(data);
            }
        }

        public void SendPacket(Packet packet)
        {
            network.GetPacket(packet);
        }

        public void Configure(int ip, int mask)
        {
            this.ipAddr = ip;
            this.mask = mask;
        }

    }
}
