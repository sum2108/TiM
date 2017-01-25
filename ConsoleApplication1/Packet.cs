using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    struct Header
    {
        public int src;
        public int dst;
        public Header(int src, int dst)
        {
            this.src = src;
            this.dst = dst;
        }
    }
    enum PacketType { ICMP_ECHO_REQUEST, ICMP_ECHO_REPLY, TCP, UDP }
    class Packet
    {
        PacketType packetType;
        Object data;
        Header header;
        public Packet(PacketType packetType, Header header, Object data)
        {
            this.packetType = packetType;
            this.data = data;
            this.header = header;
        }

        public PacketType Type
        {
            get
            {
                return this.packetType;
            }
        }

        public Header Header
        {
            get
            {
                return this.header;
            }
        }

        public Object Data
        {
            get
            {
                return this.data;
            }
        }

        public override string ToString()
        {
            return "Packet: Type = " + this.packetType.ToString() + " Data = " + this.data;
        }
    }
}
