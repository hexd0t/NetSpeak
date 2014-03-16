using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSpeakManaged.TS3;

namespace NetSpeakManaged
{
    public partial class Plugin : IDisposable
    {
        public Plugin(TS3Functions functions, IEvents events)
        {
            TSFuncs = functions;
            TSEvents = events;

            TSEvents.onClientSelfVariableUpdateEvent += TSEvents_onClientSelfVariableUpdateEvent;
        }

        void TSEvents_onClientSelfVariableUpdateEvent(ulong serverConnectionHandlerID, int flag, string oldValue, string newValue)
        {
            MessageBox.Show(newValue);
        }

        public void Dispose()
        {

        }
    }
}
