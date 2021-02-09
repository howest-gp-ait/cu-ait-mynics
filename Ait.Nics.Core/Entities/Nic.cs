using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;

namespace Ait.Nics.Core.Entities
{
    public class Nic
    {
        // props
        private NetworkInterface networkCard;
        public NetworkInterface NetwokCard
        {
            get { return networkCard; }
            set { networkCard = value; }
        }
        public string Speed
        {
            get
            {
                long speed = networkCard.Speed / 1000000;
                return speed.ToString("#,##0");
            }
        }
        public string MacAddress
        {
            get
            {
                return networkCard.GetPhysicalAddress().ToString();
            }
        }
        public string IPv4Info
        {
            get
            {
                string retour = "";
                IPInterfaceProperties iPInterfaceProperties = networkCard.GetIPProperties();
                IPv4InterfaceProperties iPv4Properties = iPInterfaceProperties.GetIPv4Properties();
                bool isDHCP = iPv4Properties.IsDhcpEnabled;
                retour += $"DCHP enabled = {isDHCP} \n\n";

                foreach (UnicastIPAddressInformation ip in iPInterfaceProperties.UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        retour += $"IP = {ip.Address.ToString()} \n";
                        retour += $"Subnet = {ip.IPv4Mask.ToString()} \n\n";

                    }
                }
                int gwCounter = 1;
                foreach (GatewayIPAddressInformation gwadres in iPInterfaceProperties.GatewayAddresses)
                {
                    if (gwadres.Address.IsIPv4MappedToIPv6)
                        retour += $"DNS {gwCounter} = {gwadres.Address.MapToIPv4().ToString()} \n";
                    else
                        retour += $"DNS {gwCounter} = {gwadres.Address.ToString()} \n";
                    gwCounter++;
                }
                int dnsCounter = 1;
                foreach (IPAddress dnsadres in iPInterfaceProperties.DnsAddresses)
                {
                    if (dnsadres.IsIPv4MappedToIPv6)
                        retour += $"DNS {dnsCounter} = {dnsadres.MapToIPv4().ToString()} \n";
                    else
                        retour += $"DNS {dnsCounter} = {dnsadres.ToString()} \n";
                    dnsCounter++;
                }
                int dhcpCounter = 1;
                foreach (IPAddress dhcpadres in iPInterfaceProperties.DhcpServerAddresses)
                {
                    retour += $"DHCP {dhcpCounter} = {dhcpadres.ToString()} \n";
                    dhcpCounter++;
                }
                return retour;
            }
        }
        public string iPv6Info
        {
            get
            {
                string retour = "-";
                if (networkCard.Supports(NetworkInterfaceComponent.IPv6))
                {
                    IPInterfaceProperties iPInterfaceProperties = networkCard.GetIPProperties();

                    foreach (UnicastIPAddressInformation ip in iPInterfaceProperties.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        {
                            retour += $"{ip.Address.ToString()} \n";
                        }
                    }
                }
                return retour;
            }
        }
        public string Description
        {
            get
            {
                return networkCard.Description;
            }
        }
        public string Id
        {
            get
            {
                return networkCard.Id;
            }
        }
        public string OperationalStatus
        {
            get
            {
                return networkCard.OperationalStatus.ToString();
            }
        }
        public string NetworkInterfaceType
        {
            get
            {
                return networkCard.NetworkInterfaceType.ToString(); ;
            }
        }
        // constructor
        public Nic(NetworkInterface networkInterface)
        {
            networkCard = networkInterface;
        }
        // override tostring
        public override string ToString()
        {
            return networkCard.Name;
        }
    }
}
