using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;
using System.Collections.Generic;
namespace UnitTestProject1
{
    [TestClass]
    public class MonitoringSystemTest
    {
        [TestMethod]
        public void MachinesFindTest()
        {
            MonitoringSystem monitoringSystem = new MonitoringSystem();
            Network network = new WirelessNetwork();
            Adapter a1, a2, a3;
            a1 = new WifiAdapter(network, 0x7FFFFFF0, 0x7FFFFFF6);
            a2 = new WifiAdapter(network, 0x7FFFFFF0, 0x7FFFFFF7);
            a3 = new WifiAdapter(network, 0x7FFFFFF0, 0x7FFFFFF8);
            monitoringSystem.AddNetwork(network, a1);
            monitoringSystem.CurrentNetwork = monitoringSystem.Networks[0];
            Service service = new Service();
            Machine machineA = new Machine(service, new List<Adapter>(new Adapter[] { a2 }));
            Machine machineB = new Machine(service, new List<Adapter>(new Adapter[] { a3 }));
            network.AddWatchers(new List<IWatcher>(new IWatcher[] { a1, a2, a3 }));
            a1.AddWatcher(monitoringSystem);
            a2.AddWatcher(machineA);
            a3.AddWatcher(machineB);
            Assert.IsTrue(monitoringSystem.Machines.Count > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "Packet was received")]
        public void SendPacketTest()
        {
            MonitoringSystem monitoringSystem = new MonitoringSystem();
            Network network = new WirelessNetwork();
            Adapter a1, a2, a3;
            a1 = new WifiAdapter(network, 0x7FFFFFF0, 0x7FFFFFF6);
            a2 = new WifiAdapter(network, 0x7FFFFFF0, 0x7FFFFFF7);
            a3 = new WifiAdapter(network, 0x7FFFFFF0, 0x7FFFFFF8);
            monitoringSystem.AddNetwork(network, a1);
            monitoringSystem.CurrentNetwork = monitoringSystem.Networks[0];
            Service service = new Service();
            Machine machineA = new Machine(service, new List<Adapter>(new Adapter[] { a2 }));
            Machine machineB = new Machine(service, new List<Adapter>(new Adapter[] { a3 }));
            machineA.ThrowByPacketReceive = true;
            machineB.ThrowByPacketReceive = true;
            network.AddWatchers(new List<IWatcher>(new IWatcher[] { a1, a2, a3 }));
            a1.AddWatcher(monitoringSystem);
            a2.AddWatcher(machineA);
            a3.AddWatcher(machineB);
            monitoringSystem.SendPacket(PacketType.UDP, new Object(), monitoringSystem.Machines[0]);
        }
    }
}
