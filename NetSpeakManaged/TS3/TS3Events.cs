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

        public void callConfigure(IntPtr handle, IntPtr qParent)
        {
            if (configure != null)
                configure(handle, qParent);
        }

        public int callProcessCommand(ulong serverConnectionHandlerID, IntPtr command)
        {
            int result = 1;// 1 = not handled
            if (processCommand != null)
                processCommand(serverConnectionHandlerID, command.UTF8(), ref result);
            return result;
        }

        public IntPtr callInfoTitle()
        {
            string result = null;
            if (infoTitle != null)
                infoTitle(ref result);
            return result.UTF8();//Allocates unmanaged memory, TS3 will call ts3plugin_freeMemory which assumes the plugin only uses HGlobal
        }

        public IntPtr callInfoData(ulong serverConnectionHandlerID, ulong id, PluginItemType type )
        {
            string data = null;
            if (infoData != null)
                infoData(serverConnectionHandlerID, id, type, ref data);
            return data.UTF8();//Allocates unmanaged memory, TS3 will call ts3plugin_freeMemory which assumes the plugin only uses HGlobal
        }

        public void callCurrentServerConnectionChanged(ulong serverConnectionHandlerID)
        {
            if (currentServerConnectionChanged != null)
                currentServerConnectionChanged(serverConnectionHandlerID);
        }

        public void callOnClientSelfVariableUpdateEvent(ulong serverConnectionHandlerID, int flag, IntPtr oldValue, IntPtr newValue)
        {
            if(onClientSelfVariableUpdateEvent != null)
                onClientSelfVariableUpdateEvent(serverConnectionHandlerID, flag, oldValue.UTF8(), newValue.UTF8());
        }
    }
}
