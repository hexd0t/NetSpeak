using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSpeakManaged
{
    public partial class Plugin
    {
        //////////////////////////////////////////////////// Plugin info
        public static String PluginName { get { return "Charms Mute"; } }
        public static String PluginAuthor { get { return "A. Müller"; } }
        public static String PluginVersion { get { return "0.1"; } }
        public static String PluginDescription { get { return "Adds a mute button to the Windows 8 charms bar"; } }
        /// <summary>
        /// Plugin command keyword. Return NULL or "" if not used.
        /// </summary>
        public static String PluginKeyword { get { return null; } }
        /// <summary>
        /// Tell client if plugin offers a configuration window.
        /// </summary>
        public static TS3.PluginConfigureOffer OffersConfigure { get { return TS3.PluginConfigureOffer.NO_CONFIGURE; } }
        /// <summary>
        /// 
        /// </summary>
        public static bool RequestAutoload { get { return false; } }

        //////////////////////////////////////////////////// Plugin resources

        /// <summary>
        /// Gets your pluginID. Dont set manually
        /// </summary>
        public IntPtr PluginID { get; set; }

        protected readonly TS3.TS3Functions TSFuncs;

        protected readonly TS3.IEvents TSEvents;
    }
}
