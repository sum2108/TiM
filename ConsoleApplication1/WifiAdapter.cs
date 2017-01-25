using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class WifiAdapter : Adapter
    {
        public WifiAdapter(Network network, int mask, int ip) : base(network, mask, ip) { }
        public WifiAdapter(Network network) : base(network) { }
    }
}
