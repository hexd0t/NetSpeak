using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetSpeakManaged.TS3.EH;

namespace NetSpeakManaged.TS3.EH
{
    public delegate void configureEH(IntPtr handle, IntPtr qParentWidget);
    public delegate int processCommandEH(ulong serverConnectionHandlerID, string command, ref int returnVal);
    public delegate void currentServerConnectionChangedEH(ulong serverConnectionHandlerID);
    public delegate void infoTitleEH(ref string returnVal);
    public delegate void infoDataEH(ulong serverConnectionHandlerID, ulong id, PluginItemType type, ref string data);
    public delegate void initMenusEH(ref List<PluginMenuItem> menuItems, ref string menuIcon);
    public delegate void initHotkeysEH(ref List<PluginHotkey> hotkeys);
    
/* Clientlib */
    public delegate void onConnectStatusChangeEvent(ulong serverConnectionHandlerID, int newStatus, uint errorNumber);
    public delegate void onNewChannelEvent(ulong serverConnectionHandlerID, ulong channelID, ulong channelParentID);
    public delegate void onNewChannelCreatedEvent(ulong serverConnectionHandlerID, ulong channelID, ulong channelParentID, ushort invokerID, string invokerName, string invokerUniqueIdentifier);
    public delegate void onDelChannelEvent(ulong serverConnectionHandlerID, ulong channelID, ushort invokerID, string invokerName, string invokerUniqueIdentifier);
    public delegate void onChannelMoveEvent(ulong serverConnectionHandlerID, ulong channelID, ulong newChannelParentID, ushort invokerID, string invokerName, string invokerUniqueIdentifier);
    public delegate void onUpdateChannelEvent(ulong serverConnectionHandlerID, ulong channelID);
    public delegate void onUpdateChannelEditedEvent(ulong serverConnectionHandlerID, ulong channelID, ushort invokerID, string invokerName, string invokerUniqueIdentifier);
    public delegate void onUpdateClientEvent(ulong serverConnectionHandlerID, ushort clientID, ushort invokerID, string invokerName, string invokerUniqueIdentifier);
    public delegate void onClientMoveEvent(ulong serverConnectionHandlerID, ushort clientID, ulong oldChannelID, ulong newChannelID, int visibility, string moveMessage);
    public delegate void onClientMoveSubscriptionEvent(ulong serverConnectionHandlerID, ushort clientID, ulong oldChannelID, ulong newChannelID, int visibility);
    public delegate void onClientMoveTimeoutEvent(ulong serverConnectionHandlerID, ushort clientID, ulong oldChannelID, ulong newChannelID, int visibility, string timeoutMessage);
    public delegate void onClientMoveMovedEvent(ulong serverConnectionHandlerID, ushort clientID, ulong oldChannelID, ulong newChannelID, int visibility, ushort moverID, string moverName, string moverUniqueIdentifier, string moveMessage);
    public delegate void onClientKickFromChannelEvent(ulong serverConnectionHandlerID, ushort clientID, ulong oldChannelID, ulong newChannelID, int visibility, ushort kickerID, string kickerName, string kickerUniqueIdentifier, string kickMessage);
    public delegate void onClientKickFromServerEvent(ulong serverConnectionHandlerID, ushort clientID, ulong oldChannelID, ulong newChannelID, int visibility, ushort kickerID, string kickerName, string kickerUniqueIdentifier, string kickMessage);
    public delegate void onClientIDsEvent(ulong serverConnectionHandlerID, string uniqueClientIdentifier, ushort clientID, string clientName);
    public delegate void onClientIDsFinishedEvent(ulong serverConnectionHandlerID);
    public delegate void onServerEditedEvent(ulong serverConnectionHandlerID, ushort editerID, string editerName, string editerUniqueIdentifier);
    public delegate void onServerUpdatedEvent(ulong serverConnectionHandlerID);
    public delegate void onServerErrorEvent(ulong serverConnectionHandlerID, string errorMessage, uint error, string returnCode, string extraMessage, ref int returnVal);
    public delegate void onServerStopEvent(ulong serverConnectionHandlerID, string shutdownMessage);
    public delegate void onTextMessageEvent(ulong serverConnectionHandlerID, ushort targetMode, ushort toID, ushort fromID, string fromName, string fromUniqueIdentifier, string message, int ffIgnored, ref int retunVal);
    public delegate void onTalkStatusChangeEvent(ulong serverConnectionHandlerID, int status, int isReceivedWhisper, ushort clientID);
    public delegate void onConnectionInfoEvent(ulong serverConnectionHandlerID, ushort clientID);
    public delegate void onServerConnectionInfoEvent(ulong serverConnectionHandlerID);
    public delegate void onChannelSubscribeEvent(ulong serverConnectionHandlerID, ulong channelID);
    public delegate void onChannelSubscribeFinishedEvent(ulong serverConnectionHandlerID);
    public delegate void onChannelUnsubscribeEvent(ulong serverConnectionHandlerID, ulong channelID);
    public delegate void onChannelUnsubscribeFinishedEvent(ulong serverConnectionHandlerID);
    public delegate void onChannelDescriptionUpdateEvent(ulong serverConnectionHandlerID, ulong channelID);
    public delegate void onChannelPasswordChangedEvent(ulong serverConnectionHandlerID, ulong channelID);
    public delegate void onPlaybackShutdownCompleteEvent(ulong serverConnectionHandlerID);
    public delegate void onSoundDeviceListChangedEvent(string modeID, int playOrCap);
    unsafe public delegate void onEditPlaybackVoiceDataEvent(ulong serverConnectionHandlerID, ushort clientID, short* samples, int sampleCount, int channels);
    unsafe public delegate void onEditPostProcessVoiceDataEvent(ulong serverConnectionHandlerID, ushort clientID, short* samples, int sampleCount, int channels, uint[] channelSpeakerArray, uint* channelFillMask);
    unsafe public delegate void onEditMixedPlaybackVoiceDataEvent(ulong serverConnectionHandlerID, short* samples, int sampleCount, int channels, uint[] channelSpeakerArray, uint* channelFillMask);
    unsafe public delegate void onEditCapturedVoiceDataEvent(ulong serverConnectionHandlerID, short* samples, int sampleCount, int channels, int* edited);
    public delegate void onCustom3dRolloffCalculationClientEvent(ulong serverConnectionHandlerID, ushort clientID, float distance, ref float volume);
    public delegate void onCustom3dRolloffCalculationWaveEvent(ulong serverConnectionHandlerID, ulong waveHandle, float distance, ref float volume);
    public delegate void onUserLoggingMessageEvent(string logMessage, int logLevel, string logChannel, ulong logID, string logTime, string completeLogString);

/* Clientlib rare */
    public delegate void onClientBanFromServerEvent(ulong serverConnectionHandlerID, ushort clientID, ulong oldChannelID, ulong newChannelID, int visibility, ushort kickerID, string kickerName, string kickerUniqueIdentifier, ulong time, string kickMessage);
    public delegate void onClientPokeEvent(ulong serverConnectionHandlerID, ushort fromClientID, string pokerName, string pokerUniqueIdentity, string message, int ffIgnored, ref int returnVal);
    public delegate void onClientSelfVariableUpdateEventEH(ulong serverConnectionHandlerID, int flag, string oldValue, string newValue);
    public delegate void onFileListEvent(ulong serverConnectionHandlerID, ulong channelID, string path, string name, ulong size, ulong datetime, int type, ulong incompletesize, string returnCode);
    public delegate void onFileListFinishedEvent(ulong serverConnectionHandlerID, ulong channelID, string path);
    public delegate void onFileInfoEvent(ulong serverConnectionHandlerID, ulong channelID, string name, ulong size, ulong datetime);
    public delegate void onServerGroupListEvent(ulong serverConnectionHandlerID, ulong serverGroupID, string name, int type, int iconID, int saveDB);
    public delegate void onServerGroupListFinishedEvent(ulong serverConnectionHandlerID);
    public delegate void onServerGroupByClientIDEvent(ulong serverConnectionHandlerID, string name, ulong serverGroupList, ulong clientDatabaseID);
    public delegate void onServerGroupPermListEvent(ulong serverConnectionHandlerID, ulong serverGroupID, uint permissionID, int permissionValue, int permissionNegated, int permissionSkip);
    public delegate void onServerGroupPermListFinishedEvent(ulong serverConnectionHandlerID, ulong serverGroupID);
    public delegate void onServerGroupClientListEvent(ulong serverConnectionHandlerID, ulong serverGroupID, ulong clientDatabaseID, string clientNameIdentifier, string clientUniqueID);
    public delegate void onChannelGroupListEvent(ulong serverConnectionHandlerID, ulong channelGroupID, string name, int type, int iconID, int saveDB);
    public delegate void onChannelGroupListFinishedEvent(ulong serverConnectionHandlerID);
    public delegate void onChannelGroupPermListEvent(ulong serverConnectionHandlerID, ulong channelGroupID, uint permissionID, int permissionValue, int permissionNegated, int permissionSkip);
    public delegate void onChannelGroupPermListFinishedEvent(ulong serverConnectionHandlerID, ulong channelGroupID);
    public delegate void onChannelPermListEvent(ulong serverConnectionHandlerID, ulong channelID, uint permissionID, int permissionValue, int permissionNegated, int permissionSkip);
    public delegate void onChannelPermListFinishedEvent(ulong serverConnectionHandlerID, ulong channelID);
    public delegate void onClientPermListEvent(ulong serverConnectionHandlerID, ulong clientDatabaseID, uint permissionID, int permissionValue, int permissionNegated, int permissionSkip);
    public delegate void onClientPermListFinishedEvent(ulong serverConnectionHandlerID, ulong clientDatabaseID);
    public delegate void onChannelClientPermListEvent(ulong serverConnectionHandlerID, ulong channelID, ulong clientDatabaseID, uint permissionID, int permissionValue, int permissionNegated, int permissionSkip);
    public delegate void onChannelClientPermListFinishedEvent(ulong serverConnectionHandlerID, ulong channelID, ulong clientDatabaseID);
    public delegate void onClientChannelGroupChangedEvent(ulong serverConnectionHandlerID, ulong channelGroupID, ulong channelID, ushort clientID, ushort invokerClientID, string invokerName, string invokerUniqueIdentity);
    public delegate void ts3plugin_onServerPermissionErrorEvent(ulong serverConnectionHandlerID, string errorMessage, uint error, string returnCode, uint failedPermissionID, ref int returnVal);
    public delegate void onPermissionListGroupEndIDEvent(ulong serverConnectionHandlerID, uint groupEndID);
    public delegate void onPermissionListEvent(ulong serverConnectionHandlerID, uint permissionID, string permissionName, string permissionDescription);
    public delegate void onPermissionListFinishedEvent(ulong serverConnectionHandlerID);
    public delegate void onPermissionOverviewEvent(ulong serverConnectionHandlerID, ulong clientDatabaseID, ulong channelID, int overviewType, ulong overviewID1, ulong overviewID2, uint permissionID, int permissionValue, int permissionNegated, int permissionSkip);
    public delegate void onPermissionOverviewFinishedEvent(ulong serverConnectionHandlerID);
    public delegate void onServerGroupClientAddedEvent(ulong serverConnectionHandlerID, ushort clientID, string clientName, string clientUniqueIdentity, ulong serverGroupID, ushort invokerClientID, string invokerName, string invokerUniqueIdentity);
    public delegate void onServerGroupClientDeletedEvent(ulong serverConnectionHandlerID, ushort clientID, string clientName, string clientUniqueIdentity, ulong serverGroupID, ushort invokerClientID, string invokerName, string invokerUniqueIdentity);
    public delegate void onClientNeededPermissionsEvent(ulong serverConnectionHandlerID, uint permissionID, int permissionValue);
    public delegate void onClientNeededPermissionsFinishedEvent(ulong serverConnectionHandlerID);
    public delegate void onFileTransferStatusEvent(ushort transferID, uint status, string statusMessage, ulong remotefileSize, ulong serverConnectionHandlerID);
    public delegate void onClientChatClosedEvent(ulong serverConnectionHandlerID, ushort clientID, string clientUniqueIdentity);
    public delegate void onClientChatComposingEvent(ulong serverConnectionHandlerID, ushort clientID, string clientUniqueIdentity);
    public delegate void onServerLogEvent(ulong serverConnectionHandlerID, string logMsg);
    public delegate void onServerLogFinishedEvent(ulong serverConnectionHandlerID, ulong lastPos, ulong fileSize);
    public delegate void onMessageListEvent(ulong serverConnectionHandlerID, ulong messageID, string fromClientUniqueIdentity, string subject, ulong timestamp, int flagRead);
    public delegate void onMessageGetEvent(ulong serverConnectionHandlerID, ulong messageID, string fromClientUniqueIdentity, string subject, string message, ulong timestamp);
    public delegate void onClientDBIDfromUIDEvent(ulong serverConnectionHandlerID, string uniqueClientIdentifier, ulong clientDatabaseID);
    public delegate void onClientNamefromUIDEvent(ulong serverConnectionHandlerID, string uniqueClientIdentifier, ulong clientDatabaseID, string clientNickName);
    public delegate void onClientNamefromDBIDEvent(ulong serverConnectionHandlerID, string uniqueClientIdentifier, ulong clientDatabaseID, string clientNickName);
    public delegate void onComplainListEvent(ulong serverConnectionHandlerID, ulong targetClientDatabaseID, string targetClientNickName, ulong fromClientDatabaseID, string fromClientNickName, string complainReason, ulong timestamp);
    public delegate void onBanListEvent(ulong serverConnectionHandlerID, ulong banid, string ip, string name, string uid, ulong creationTime, ulong durationTime, string invokerName, ulong invokercldbid, string invokeruid, string reason, int numberOfEnforcements, string lastNickName);
    public delegate void onClientServerQueryLoginPasswordEvent(ulong serverConnectionHandlerID, string loginPassword);
    public delegate void onPluginCommandEvent(ulong serverConnectionHandlerID, string pluginName, string pluginCommand);
    public delegate void onIncomingClientQueryEvent(ulong serverConnectionHandlerID, string commandText);
    public delegate void onServerTemporaryPasswordListEvent(ulong serverConnectionHandlerID, string clientNickname, string uniqueClientIdentifier, string description, string password, ulong timestampStart, ulong timestampEnd, ulong targetChannelID, string targetChannelPW);

/* Client UI callbacks */
    public delegate void onAvatarUpdated(ulong serverConnectionHandlerID, ushort clientID, string avatarPath);
    public delegate void onMenuItemEvent(ulong serverConnectionHandlerID, PluginMenuType type, int menuItemID, ulong selectedItemID);
    public delegate void onHotkeyEvent(string keyword);
    public delegate void onHotkeyRecordedEvent(string keyword, string key);
    public delegate void onClientDisplayNameChanged(ulong serverConnectionHandlerID, ushort clientID, string displayName, string uniqueClientIdentifier);
}

namespace NetSpeakManaged.TS3
{
    public interface IEvents
    {
        /// <summary>
        /// Show config dialog
        /// <remarks>See also offersConfigure in Plugin.Interop.cs</remarks>
        /// </summary>
        event configureEH configure;
        /// <summary>
        /// Plugin processes console command. Return 0 if plugin handled the command, 1 if not handled.
        /// </summary>
        event processCommandEH processCommand;
        /// <summary>
        /// Client changed current server connection handler
        /// </summary>
        event currentServerConnectionChangedEH currentServerConnectionChanged;
        /// <summary>
        /// Static title shown in the left column in the info frame
        /// </summary>
        event infoTitleEH infoTitle;
        /// <summary>
        /// Dynamic content shown in the right column in the info frame.
        /// Check the parameter "type" if you want to implement this feature only for specific item types. Set the parameter
        /// "data" to null to have the client ignore the info data.
        /// </summary>
        event infoDataEH infoData;
        event initMenusEH initMenus;
        event initHotkeysEH initHotkeys;

        event onClientSelfVariableUpdateEventEH onClientSelfVariableUpdateEvent;
    }
}
