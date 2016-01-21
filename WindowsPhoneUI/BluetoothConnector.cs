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
    public class BluetoothConnector
    {
        public void Connect() 
        { 
        }
            
        public async Task<IEnumerable<String>> getPeers()
        {
            PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
            var peerList = await PeerFinder.FindAllPeersAsync();
            return peerList.Select(i => i.DisplayName);
        }
    }
}
