using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;
using System.Collections.Generic;
namespace UnitTestProject1
{
    [TestClass]
    public class ServiceTest
    {

        [TestMethod]
        public void TestWhere()
        {
            Service service = new Service();
            //Тестируем сервис добавляем тестовый адапетр и тестовый пакет должен вернуть один адаптер
            List<Adapter> adapters = service.Where(new List<Adapter>(new Adapter[] { new WifiAdapter(new WirelessNetwork() ,0xFFFFFF0, 0xFFFFFF6) }), new Packet(PacketType.ICMP_ECHO_REQUEST, new Header(0x7FFFFFF3, 0x7FFFFFF4), new Object()));
            Assert.IsTrue(adapters.Count > 0);
        }
    }
}
