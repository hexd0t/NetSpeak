using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSpeakManaged.TS3.EH;

namespace NetSpeakManaged.TS3
{
    public class TS3Events : IEvents
    {
        public event configureEH configure;
        public event processCommandEH processCommand;
        public event currentServerConnectionChangedEH currentServerConnectionChanged;
        public event infoTitleEH infoTitle;
        public event infoDataEH infoData;
        public event initMenusEH initMenus;
        public event initHotkeysEH initHotkeys;

        public event onClientSelfVariableUpdateEventEH onClientSelfVariableUpdateEvent;

        public void callOnClientSelfVariableUpdateEvent(ulong serverConnectionHandlerID, int flag, IntPtr oldValue, IntPtr newValue)
        {
            if(onClientSelfVariableUpdateEvent != null)
                onClientSelfVariableUpdateEvent(serverConnectionHandlerID, flag, oldValue.UTF8(), newValue.UTF8());
        }
    }
}
