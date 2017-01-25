using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Service
    {
        public List<Adapter> Where(List<Adapter> adapters, Packet packet)
        {
            return adapters.FindAll((adapter) =>
            {
                return (adapter.Mask & packet.Header.src) == adapter.Mask;
            });
        }
    }
}
