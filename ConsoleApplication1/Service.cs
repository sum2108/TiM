using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Service
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
