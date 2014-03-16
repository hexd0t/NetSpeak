using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace NetSpeakManaged.TS3
{
    public enum PluginConfigureOffer {
	    NO_CONFIGURE = 0,      /* Plugin does not implement ts3plugin_configure */
	    CONFIGURE_NEW_THREAD,  /* Plugin does implement ts3plugin_configure and requests to run this function in an own thread */
	    CONFIGURE_QT_THREAD    /* Plugin does implement ts3plugin_configure and requests to run this function in the Qt GUI thread */
    };

    public enum PluginMessageTarget {
	    PLUGIN_MESSAGE_TARGET_SERVER = 0,
	    PLUGIN_MESSAGE_TARGET_CHANNEL
    };

    public enum PluginItemType {
	    PLUGIN_SERVER = 0,
	    PLUGIN_CHANNEL,
	    PLUGIN_CLIENT
    };

    public enum PluginMenuType {
	    PLUGIN_MENU_TYPE_GLOBAL = 0,
	    PLUGIN_MENU_TYPE_CHANNEL,
	    PLUGIN_MENU_TYPE_CLIENT
    };

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PluginMenuItem {
	    PluginMenuType type;
	    int id;
	    fixed char text[128];//PLUGIN_MENU_BUFSZ
	    fixed char icon[128];//PLUGIN_MENU_BUFSZ
    };
    
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PluginHotkey {
	    fixed char keyword[128];//PLUGIN_HOTKEY_BUFSZ
	    fixed char description[128];//PLUGIN_HOTKEY_BUFSZ
    };

    //ToDo: do union using layoutkind.explicit
    /*struct PluginBookmarkList {}
    struct PluginBookmarkItem {
	    char*         name;
	    unsigned char isFolder;
	    unsigned char reserved[3];
	    union{
		    char*         uuid;
		    struct PluginBookmarkList* folder;
	    };
    };

    struct PluginBookmarkList{
	    int itemcount;
	    struct PluginBookmarkItem items[1]; //should be 0 but compiler complains
    };*/

    public enum PluginGuiProfile{
	    PLUGIN_GUI_SOUND_CAPTURE = 0,
	    PLUGIN_GUI_SOUND_PLAYBACK,
	    PLUGIN_GUI_HOTKEY,
	    PLUGIN_GUI_SOUNDPACK,
	    PLUGIN_GUI_IDENTITY
    };

    public enum PluginConnectTab{
	    PLUGIN_CONNECT_TAB_NEW = 0,
	    PLUGIN_CONNECT_TAB_CURRENT,
	    PLUGIN_CONNECT_TAB_NEW_IF_CURRENT_CONNECTED
    };
}
