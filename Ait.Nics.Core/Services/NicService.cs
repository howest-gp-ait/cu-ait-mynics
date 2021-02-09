using System;
using System.Collections.Generic;
using System.Text;
using Ait.Nics.Core.Entities;
using System.Net.NetworkInformation;

namespace Ait.Nics.Core.Services
{
    public class NicService
    {
        private List<Nic> allNics;
        public List<Nic> AllNics
        {
            get { return allNics; }
        }
        public NicService()
        {
            allNics = new List<Nic>();
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                allNics.Add(new Nic(networkInterface));
            }
        }

    }
}
