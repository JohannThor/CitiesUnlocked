using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Proximity;
using Windows.Foundation;
using Windows.Networking.Sockets;

namespace WindowsPhoneUI
{
    class BluethoothConnector
    {
        public void Connect() {
               
        }

        public List<string> getAllPeers() {
            PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
            var peerList = await PeerFinder.FindAllPeersAsync();
            if (peerList.Count > 0)
            {
                return peerList.Select(i => i.displayName);
            }
            else return new list<string>();
        }
    }
}
