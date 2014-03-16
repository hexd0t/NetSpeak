using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace NetSpeakManaged.TS3
{
    /// <summary>
    /// Contains everything to get from native TS3Functions to .NET-style API:
    /// TS3FunctionPointers: copy of native struct containing pointers
    /// TS3FunctionDelegates: Managed delegates of native pointers
    /// TS3Functions: Managed wrappers of delegates - return results, throw on errors
    /// </summary>
    public class TS3Functions
    {
        public readonly TS3FunctionDelegates Delegates;
        public TS3Functions(IntPtr functionStructPtr)
        {
            var functionPointers = (TS3FunctionPointers)Marshal.PtrToStructure(functionStructPtr, typeof(TS3FunctionPointers));
            Delegates = new TS3FunctionDelegates(functionPointers);
        }

        /// <summary>
        /// Queries the complete Client Lib version string
        /// </summary>
        /// <returns>The complete Client Lib version string</returns>
        public string GetClientLibVersion()
        {
            IntPtr natresult;
            var err = Delegates.getClientLibVersion(out natresult);
            if (err != Error.ok)
                throw new TS3Exception(err);
            var result = natresult.UTF8();
            Delegates.freeMemory(natresult);
            return result;
        }
        /// <summary>
        /// Queries only the Client Lib version number, which is a part of the complete version string, as numeric value
        /// </summary>
        /// <returns>The Client Lib version number</returns>
        public ulong GetClientLibVersionNumber()
        {
            ulong result;
            var err = Delegates.getClientLibVersionNumber(out result);
            if (err != Error.ok)
                throw new TS3Exception(err);
            return result;
        }

        /// <summary>
        /// Creates a new server connection handler and receives its ID
        /// </summary>
        /// <param name="localport">Port the client should bind on. Specify zero to let the operating system choose any free port.</param>
        /// <returns>Server connection handler ID</returns>
        public ulong SpawnNewServerConnectionHandler(int localport = 0)
        {
            ulong result;
            var err = Delegates.spawnNewServerConnectionHandler(localport, out result);
            if (err != Error.ok)
                throw new TS3Exception(err);
            return result;
        }

        /// <summary>
        /// Destroys a server connection handler
        /// </summary>
        /// <param name="handlerID">ID of the server connection handler to destroy.</param>
        public void DestroyServerConnectionHandler(ulong handlerID)
        {
            var err = Delegates.destroyServerConnectionHandler(handlerID);
            if (err != Error.ok)
                throw new TS3Exception(err);
        }

        /// <summary>
        /// Writes to the TS3Client log
        /// </summary>
        /// <param name="message">Text written to log.</param>
        /// <param name="severity">The level of the message, warning or error.</param>
        /// <param name="channel">Custom text to categorize the message channel (i.e. "Client", "Sound").</param>
        /// <param name="serverConnectionID">Server connection handler ID to identify the current server connection when using multiple connections.</param>
        public void LogMessage(string message, LogLevel severity, string channel = null, ulong serverConnectionID = 0)
        {
            var msgNative = message.UTF8();
            if (channel == null)
                channel = "";
            var chNative = channel.UTF8();
            Error err;
            try
            {
                err = Delegates.logMessage(msgNative, severity, chNative, serverConnectionID);
            }
            finally
            {
                Marshal.FreeHGlobal(msgNative);
                Marshal.FreeHGlobal(chNative);
            }
            
            if (err != Error.ok)
                throw new TS3Exception(err);
        }

        ////////////////////


        [StructLayout(LayoutKind.Sequential)]
        public struct TS3FunctionPointers
        {
            public IntPtr getClientLibVersion;
            public IntPtr getClientLibVersionNumber;
            public IntPtr spawnNewServerConnectionHandler;
            public IntPtr destroyServerConnectionHandler;
            public IntPtr getErrorMessage;
            public IntPtr freeMemory;
            public IntPtr logMessage;
            public IntPtr getPlaybackDeviceList;
            public IntPtr getPlaybackModeList;
            public IntPtr getCaptureDeviceList;
            public IntPtr getCaptureModeList;
            public IntPtr getDefaultPlaybackDevice;
            public IntPtr getDefaultPlayBackMode;
            public IntPtr getDefaultCaptureDevice;
            public IntPtr getDefaultCaptureMode;
            public IntPtr openPlaybackDevice;
            public IntPtr openCaptureDevice;
            public IntPtr getCurrentPlaybackDeviceName;
            public IntPtr getCurrentPlayBackMode;
            public IntPtr getCurrentCaptureDeviceName;
            public IntPtr getCurrentCaptureMode;
            public IntPtr initiateGracefulPlaybackShutdown;
            public IntPtr closePlaybackDevice;
            public IntPtr closeCaptureDevice;
            public IntPtr activateCaptureDevice;
            public IntPtr playWaveFileHandle;
            public IntPtr pauseWaveFileHandle;
            public IntPtr closeWaveFileHandle;
            public IntPtr playWaveFile;
            public IntPtr registerCustomDevice;
            public IntPtr unregisterCustomDevice;
            public IntPtr processCustomCaptureData;
            public IntPtr acquireCustomPlaybackData;
            public IntPtr getPreProcessorInfoValueFloat;
            public IntPtr getPreProcessorConfigValue;
            public IntPtr setPreProcessorConfigValue;
            public IntPtr getEncodeConfigValue;
            public IntPtr getPlaybackConfigValueAsFloat;
            public IntPtr setPlaybackConfigValue;
            public IntPtr setClientVolumeModifier;
            public IntPtr startVoiceRecording;
            public IntPtr stopVoiceRecording;
            public IntPtr systemset3DListenerAttributes;
            public IntPtr set3DWaveAttributes;
            public IntPtr systemset3DSettings;
            public IntPtr channelset3DAttributes;
            public IntPtr startConnection;
            public IntPtr stopConnection;
            public IntPtr requestClientMove;
            public IntPtr requestClientVariables;
            public IntPtr requestClientKickFromChannel;
            public IntPtr requestClientKickFromServer;
            public IntPtr requestChannelDelete;
            public IntPtr requestChannelMove;
            public IntPtr requestSendPrivateTextMsg;
            public IntPtr requestSendChannelTextMsg;
            public IntPtr requestSendServerTextMsg;
            public IntPtr requestConnectionInfo;
            public IntPtr requestClientSetWhisperList;
            public IntPtr requestChannelSubscribe;
            public IntPtr requestChannelSubscribeAll;
            public IntPtr requestChannelUnsubscribe;
            public IntPtr requestChannelUnsubscribeAll;
            public IntPtr requestChannelDescription;
            public IntPtr requestMuteClients;
            public IntPtr requestUnmuteClients;
            public IntPtr requestClientPoke;
            public IntPtr requestClientIDs;
            public IntPtr clientChatClosed;
            public IntPtr clientChatComposing;
            public IntPtr requestServerTemporaryPasswordAdd;
            public IntPtr requestServerTemporaryPasswordDel;
            public IntPtr requestServerTemporaryPasswordList;
            public IntPtr getClientID;
            public IntPtr getClientSelfVariableAsInt;
            public IntPtr getClientSelfVariableAsString;
            public IntPtr setClientSelfVariableAsInt;
            public IntPtr setClientSelfVariableAsString;
            public IntPtr flushClientSelfUpdates;
            public IntPtr getClientVariableAsInt;
            public IntPtr getClientVariableAsUInt64;
            public IntPtr getClientVariableAsString;
            public IntPtr getClientList;
            public IntPtr getChannelOfClient;
            public IntPtr getChannelVariableAsInt;
            public IntPtr getChannelVariableAsUInt64;
            public IntPtr getChannelVariableAsString;
            public IntPtr getChannelIDFromChannelNames;
            public IntPtr setChannelVariableAsInt;
            public IntPtr setChannelVariableAsUInt64;
            public IntPtr setChannelVariableAsString;
            public IntPtr flushChannelUpdates;
            public IntPtr flushChannelCreation;
            public IntPtr getChannelList;
            public IntPtr getChannelClientList;
            public IntPtr getParentChannelOfChannel;
            public IntPtr getServerConnectionHandlerList;
            public IntPtr getServerVariableAsInt;
            public IntPtr getServerVariableAsUInt64;
            public IntPtr getServerVariableAsString;
            public IntPtr requestServerVariables;
            public IntPtr getConnectionStatus;
            public IntPtr getConnectionVariableAsUInt64;
            public IntPtr getConnectionVariableAsDouble;
            public IntPtr getConnectionVariableAsString;
            public IntPtr cleanUpConnectionInfo;
            public IntPtr requestClientDBIDfromUID;
            public IntPtr requestClientNamefromUID;
            public IntPtr requestClientNamefromDBID;
            public IntPtr requestClientEditDescription;
            public IntPtr requestClientSetIsTalker;
            public IntPtr requestIsTalker;
            public IntPtr requestSendClientQueryCommand;
            public IntPtr getTransferFileName;
            public IntPtr getTransferFilePath;
            public IntPtr getTransferFileSize;
            public IntPtr getTransferFileSizeDone;
            public IntPtr isTransferSender;
            public IntPtr getTransferStatus;
            public IntPtr getCurrentTransferSpeed;
            public IntPtr getAverageTransferSpeed;
            public IntPtr getTransferRunTime;
            public IntPtr sendFile;
            public IntPtr requestFile;
            public IntPtr haltTransfer;
            public IntPtr requestFileList;
            public IntPtr requestFileInfo;
            public IntPtr requestDeleteFile;
            public IntPtr requestCreateDirectory;
            public IntPtr requestRenameFile;
            public IntPtr requestMessageAdd;
            public IntPtr requestMessageDel;
            public IntPtr requestMessageGet;
            public IntPtr requestMessageList;
            public IntPtr requestMessageUpdateFlag;
            public IntPtr verifyServerPassword;
            public IntPtr verifyChannelPassword;
            public IntPtr banclient;
            public IntPtr banadd;
            public IntPtr banclientdbid;
            public IntPtr bandel;
            public IntPtr bandelall;
            public IntPtr requestBanList;
            public IntPtr requestComplainAdd;
            public IntPtr requestComplainDel;
            public IntPtr requestComplainDelAll;
            public IntPtr requestComplainList;
            public IntPtr requestServerGroupList;
            public IntPtr requestServerGroupAdd;
            public IntPtr requestServerGroupDel;
            public IntPtr requestServerGroupAddClient;
            public IntPtr requestServerGroupDelClient;
            public IntPtr requestServerGroupsByClientID;
            public IntPtr requestServerGroupAddPerm;
            public IntPtr requestServerGroupDelPerm;
            public IntPtr requestServerGroupPermList;
            public IntPtr requestServerGroupClientList;
            public IntPtr requestChannelGroupList;
            public IntPtr requestChannelGroupAdd;
            public IntPtr requestChannelGroupDel;
            public IntPtr requestChannelGroupAddPerm;
            public IntPtr requestChannelGroupDelPerm;
            public IntPtr requestChannelGroupPermList;
            public IntPtr requestSetClientChannelGroup;
            public IntPtr requestChannelAddPerm;
            public IntPtr requestChannelDelPerm;
            public IntPtr requestChannelPermList;
            public IntPtr requestClientAddPerm;
            public IntPtr requestClientDelPerm;
            public IntPtr requestClientPermList;
            public IntPtr requestChannelClientAddPerm;
            public IntPtr requestChannelClientDelPerm;
            public IntPtr requestChannelClientPermList;
            public IntPtr privilegeKeyUse;
            public IntPtr requestPermissionList;
            public IntPtr requestPermissionOverview;
            public IntPtr clientPropertyStringToFlag;
            public IntPtr channelPropertyStringToFlag;
            public IntPtr serverPropertyStringToFlag;
            public IntPtr getAppPath;
            public IntPtr getResourcesPath;
            public IntPtr getConfigPath;
            public IntPtr getPluginPath;
            public IntPtr getCurrentServerConnectionHandlerID;
            public IntPtr printMessage;
            public IntPtr printMessageToCurrentTab;
            public IntPtr urlsToBB;
            public IntPtr sendPluginCommand;
            public IntPtr getDirectories;
            public IntPtr getServerConnectInfo;
            public IntPtr getChannelConnectInfo;
            public IntPtr createReturnCode;
            public IntPtr requestInfoUpdate;
            public IntPtr getServerVersion;
            public IntPtr isWhispering;
            public IntPtr isReceivingWhisper;
            public IntPtr getAvatar;
            public IntPtr setPluginMenuEnabled;
            public IntPtr showHotkeySetup;
            public IntPtr requestHotkeyInputDialog;
            public IntPtr getHotkeyFromKeyword;
            public IntPtr getClientDisplayName;
            public IntPtr getBookmarkList;
            public IntPtr getProfileList;
            public IntPtr guiConnect;
            public IntPtr guiConnectBookmark;
            public IntPtr createBookmark;
            public IntPtr getPermissionIDByName;
            public IntPtr getClientNeededPermission;
        }
        public class TS3FunctionDelegates
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getClientLibVersionDel(out IntPtr result);
            public getClientLibVersionDel getClientLibVersion;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getClientLibVersionNumberDel(out ulong result);
            public getClientLibVersionNumberDel getClientLibVersionNumber;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error spawnNewServerConnectionHandlerDel(int port, out ulong resultHandlerID);
            public spawnNewServerConnectionHandlerDel spawnNewServerConnectionHandler;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error destroyServerConnectionHandlerDel(ulong handlerID);
            public destroyServerConnectionHandlerDel destroyServerConnectionHandler;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getErrorMessageDel(Error error, out IntPtr result);
            public getErrorMessageDel getErrorMessage;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error freeMemoryDel(IntPtr ptr);
            public freeMemoryDel freeMemory;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error logMessageDel(IntPtr message, LogLevel severity, IntPtr channel, ulong logID);
            public logMessageDel logMessage;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getPlaybackDeviceListDel();
            public getPlaybackDeviceListDel getPlaybackDeviceList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getPlaybackModeListDel();
            public getPlaybackModeListDel getPlaybackModeList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getCaptureDeviceListDel();
            public getCaptureDeviceListDel getCaptureDeviceList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getCaptureModeListDel();
            public getCaptureModeListDel getCaptureModeList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getDefaultPlaybackDeviceDel();
            public getDefaultPlaybackDeviceDel getDefaultPlaybackDevice;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getDefaultPlayBackModeDel();
            public getDefaultPlayBackModeDel getDefaultPlayBackMode;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getDefaultCaptureDeviceDel();
            public getDefaultCaptureDeviceDel getDefaultCaptureDevice;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getDefaultCaptureModeDel();
            public getDefaultCaptureModeDel getDefaultCaptureMode;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error openPlaybackDeviceDel();
            public openPlaybackDeviceDel openPlaybackDevice;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error openCaptureDeviceDel();
            public openCaptureDeviceDel openCaptureDevice;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getCurrentPlaybackDeviceNameDel();
            public getCurrentPlaybackDeviceNameDel getCurrentPlaybackDeviceName;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getCurrentPlayBackModeDel();
            public getCurrentPlayBackModeDel getCurrentPlayBackMode;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getCurrentCaptureDeviceNameDel();
            public getCurrentCaptureDeviceNameDel getCurrentCaptureDeviceName;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getCurrentCaptureModeDel();
            public getCurrentCaptureModeDel getCurrentCaptureMode;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error initiateGracefulPlaybackShutdownDel();
            public initiateGracefulPlaybackShutdownDel initiateGracefulPlaybackShutdown;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error closePlaybackDeviceDel();
            public closePlaybackDeviceDel closePlaybackDevice;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error closeCaptureDeviceDel();
            public closeCaptureDeviceDel closeCaptureDevice;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error activateCaptureDeviceDel();
            public activateCaptureDeviceDel activateCaptureDevice;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error playWaveFileHandleDel();
            public playWaveFileHandleDel playWaveFileHandle;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error pauseWaveFileHandleDel();
            public pauseWaveFileHandleDel pauseWaveFileHandle;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error closeWaveFileHandleDel();
            public closeWaveFileHandleDel closeWaveFileHandle;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error playWaveFileDel();
            public playWaveFileDel playWaveFile;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error registerCustomDeviceDel();
            public registerCustomDeviceDel registerCustomDevice;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error unregisterCustomDeviceDel();
            public unregisterCustomDeviceDel unregisterCustomDevice;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error processCustomCaptureDataDel();
            public processCustomCaptureDataDel processCustomCaptureData;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error acquireCustomPlaybackDataDel();
            public acquireCustomPlaybackDataDel acquireCustomPlaybackData;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getPreProcessorInfoValueFloatDel();
            public getPreProcessorInfoValueFloatDel getPreProcessorInfoValueFloat;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getPreProcessorConfigValueDel();
            public getPreProcessorConfigValueDel getPreProcessorConfigValue;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error setPreProcessorConfigValueDel();
            public setPreProcessorConfigValueDel setPreProcessorConfigValue;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getEncodeConfigValueDel();
            public getEncodeConfigValueDel getEncodeConfigValue;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getPlaybackConfigValueAsFloatDel();
            public getPlaybackConfigValueAsFloatDel getPlaybackConfigValueAsFloat;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error setPlaybackConfigValueDel();
            public setPlaybackConfigValueDel setPlaybackConfigValue;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error setClientVolumeModifierDel();
            public setClientVolumeModifierDel setClientVolumeModifier;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error startVoiceRecordingDel();
            public startVoiceRecordingDel startVoiceRecording;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error stopVoiceRecordingDel();
            public stopVoiceRecordingDel stopVoiceRecording;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error systemset3DListenerAttributesDel();
            public systemset3DListenerAttributesDel systemset3DListenerAttributes;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error set3DWaveAttributesDel();
            public set3DWaveAttributesDel set3DWaveAttributes;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error systemset3DSettingsDel();
            public systemset3DSettingsDel systemset3DSettings;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error channelset3DAttributesDel();
            public channelset3DAttributesDel channelset3DAttributes;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error startConnectionDel();
            public startConnectionDel startConnection;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error stopConnectionDel();
            public stopConnectionDel stopConnection;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientMoveDel();
            public requestClientMoveDel requestClientMove;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientVariablesDel();
            public requestClientVariablesDel requestClientVariables;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientKickFromChannelDel();
            public requestClientKickFromChannelDel requestClientKickFromChannel;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientKickFromServerDel();
            public requestClientKickFromServerDel requestClientKickFromServer;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelDeleteDel();
            public requestChannelDeleteDel requestChannelDelete;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelMoveDel();
            public requestChannelMoveDel requestChannelMove;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestSendPrivateTextMsgDel();
            public requestSendPrivateTextMsgDel requestSendPrivateTextMsg;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestSendChannelTextMsgDel();
            public requestSendChannelTextMsgDel requestSendChannelTextMsg;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestSendServerTextMsgDel();
            public requestSendServerTextMsgDel requestSendServerTextMsg;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestConnectionInfoDel();
            public requestConnectionInfoDel requestConnectionInfo;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientSetWhisperListDel();
            public requestClientSetWhisperListDel requestClientSetWhisperList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelSubscribeDel();
            public requestChannelSubscribeDel requestChannelSubscribe;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelSubscribeAllDel();
            public requestChannelSubscribeAllDel requestChannelSubscribeAll;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelUnsubscribeDel();
            public requestChannelUnsubscribeDel requestChannelUnsubscribe;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelUnsubscribeAllDel();
            public requestChannelUnsubscribeAllDel requestChannelUnsubscribeAll;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelDescriptionDel();
            public requestChannelDescriptionDel requestChannelDescription;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestMuteClientsDel();
            public requestMuteClientsDel requestMuteClients;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestUnmuteClientsDel();
            public requestUnmuteClientsDel requestUnmuteClients;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientPokeDel();
            public requestClientPokeDel requestClientPoke;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientIDsDel();
            public requestClientIDsDel requestClientIDs;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error clientChatClosedDel();
            public clientChatClosedDel clientChatClosed;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error clientChatComposingDel();
            public clientChatComposingDel clientChatComposing;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerTemporaryPasswordAddDel();
            public requestServerTemporaryPasswordAddDel requestServerTemporaryPasswordAdd;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerTemporaryPasswordDelDel();
            public requestServerTemporaryPasswordDelDel requestServerTemporaryPasswordDel;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerTemporaryPasswordListDel();
            public requestServerTemporaryPasswordListDel requestServerTemporaryPasswordList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getClientIDDel();
            public getClientIDDel getClientID;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getClientSelfVariableAsIntDel(ulong serverConnectionHandlerID, UIntPtr flag, out int result);
            public getClientSelfVariableAsIntDel getClientSelfVariableAsInt;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getClientSelfVariableAsStringDel(ulong serverConnectionHandlerID, UIntPtr flag, out IntPtr result);
            public getClientSelfVariableAsStringDel getClientSelfVariableAsString;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error setClientSelfVariableAsIntDel(ulong serverConnectionHandlerID, UIntPtr flag, int value);
            public setClientSelfVariableAsIntDel setClientSelfVariableAsInt;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error setClientSelfVariableAsStringDel(ulong serverConnectionHandlerID, UIntPtr flag, IntPtr value);
            public setClientSelfVariableAsStringDel setClientSelfVariableAsString;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error flushClientSelfUpdatesDel(ulong serverConnectionHandlerID, IntPtr returnCode);
            public flushClientSelfUpdatesDel flushClientSelfUpdates;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getClientVariableAsIntDel();
            public getClientVariableAsIntDel getClientVariableAsInt;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getClientVariableAsUInt64Del();
            public getClientVariableAsUInt64Del getClientVariableAsUInt64;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getClientVariableAsStringDel();
            public getClientVariableAsStringDel getClientVariableAsString;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getClientListDel();
            public getClientListDel getClientList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getChannelOfClientDel();
            public getChannelOfClientDel getChannelOfClient;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getChannelVariableAsIntDel();
            public getChannelVariableAsIntDel getChannelVariableAsInt;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getChannelVariableAsUInt64Del();
            public getChannelVariableAsUInt64Del getChannelVariableAsUInt64;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getChannelVariableAsStringDel();
            public getChannelVariableAsStringDel getChannelVariableAsString;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getChannelIDFromChannelNamesDel();
            public getChannelIDFromChannelNamesDel getChannelIDFromChannelNames;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error setChannelVariableAsIntDel();
            public setChannelVariableAsIntDel setChannelVariableAsInt;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error setChannelVariableAsUInt64Del();
            public setChannelVariableAsUInt64Del setChannelVariableAsUInt64;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error setChannelVariableAsStringDel();
            public setChannelVariableAsStringDel setChannelVariableAsString;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error flushChannelUpdatesDel();
            public flushChannelUpdatesDel flushChannelUpdates;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error flushChannelCreationDel();
            public flushChannelCreationDel flushChannelCreation;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getChannelListDel();
            public getChannelListDel getChannelList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getChannelClientListDel();
            public getChannelClientListDel getChannelClientList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getParentChannelOfChannelDel();
            public getParentChannelOfChannelDel getParentChannelOfChannel;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getServerConnectionHandlerListDel();
            public getServerConnectionHandlerListDel getServerConnectionHandlerList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getServerVariableAsIntDel();
            public getServerVariableAsIntDel getServerVariableAsInt;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getServerVariableAsUInt64Del();
            public getServerVariableAsUInt64Del getServerVariableAsUInt64;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getServerVariableAsStringDel();
            public getServerVariableAsStringDel getServerVariableAsString;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerVariablesDel();
            public requestServerVariablesDel requestServerVariables;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getConnectionStatusDel();
            public getConnectionStatusDel getConnectionStatus;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getConnectionVariableAsUInt64Del();
            public getConnectionVariableAsUInt64Del getConnectionVariableAsUInt64;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getConnectionVariableAsDoubleDel();
            public getConnectionVariableAsDoubleDel getConnectionVariableAsDouble;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getConnectionVariableAsStringDel();
            public getConnectionVariableAsStringDel getConnectionVariableAsString;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error cleanUpConnectionInfoDel();
            public cleanUpConnectionInfoDel cleanUpConnectionInfo;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientDBIDfromUIDDel();
            public requestClientDBIDfromUIDDel requestClientDBIDfromUID;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientNamefromUIDDel();
            public requestClientNamefromUIDDel requestClientNamefromUID;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientNamefromDBIDDel();
            public requestClientNamefromDBIDDel requestClientNamefromDBID;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientEditDescriptionDel();
            public requestClientEditDescriptionDel requestClientEditDescription;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientSetIsTalkerDel();
            public requestClientSetIsTalkerDel requestClientSetIsTalker;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestIsTalkerDel();
            public requestIsTalkerDel requestIsTalker;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestSendClientQueryCommandDel();
            public requestSendClientQueryCommandDel requestSendClientQueryCommand;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getTransferFileNameDel();
            public getTransferFileNameDel getTransferFileName;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getTransferFilePathDel();
            public getTransferFilePathDel getTransferFilePath;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getTransferFileSizeDel();
            public getTransferFileSizeDel getTransferFileSize;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getTransferFileSizeDoneDel();
            public getTransferFileSizeDoneDel getTransferFileSizeDone;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error isTransferSenderDel();
            public isTransferSenderDel isTransferSender;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getTransferStatusDel();
            public getTransferStatusDel getTransferStatus;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getCurrentTransferSpeedDel();
            public getCurrentTransferSpeedDel getCurrentTransferSpeed;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getAverageTransferSpeedDel();
            public getAverageTransferSpeedDel getAverageTransferSpeed;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getTransferRunTimeDel();
            public getTransferRunTimeDel getTransferRunTime;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error sendFileDel();
            public sendFileDel sendFile;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestFileDel();
            public requestFileDel requestFile;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error haltTransferDel();
            public haltTransferDel haltTransfer;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestFileListDel();
            public requestFileListDel requestFileList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestFileInfoDel();
            public requestFileInfoDel requestFileInfo;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestDeleteFileDel();
            public requestDeleteFileDel requestDeleteFile;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestCreateDirectoryDel();
            public requestCreateDirectoryDel requestCreateDirectory;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestRenameFileDel();
            public requestRenameFileDel requestRenameFile;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestMessageAddDel();
            public requestMessageAddDel requestMessageAdd;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestMessageDelDel();
            public requestMessageDelDel requestMessageDel;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestMessageGetDel();
            public requestMessageGetDel requestMessageGet;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestMessageListDel();
            public requestMessageListDel requestMessageList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestMessageUpdateFlagDel();
            public requestMessageUpdateFlagDel requestMessageUpdateFlag;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error verifyServerPasswordDel();
            public verifyServerPasswordDel verifyServerPassword;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error verifyChannelPasswordDel();
            public verifyChannelPasswordDel verifyChannelPassword;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error banclientDel();
            public banclientDel banclient;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error banaddDel();
            public banaddDel banadd;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error banclientdbidDel();
            public banclientdbidDel banclientdbid;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error bandelDel();
            public bandelDel bandel;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error bandelallDel();
            public bandelallDel bandelall;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestBanListDel();
            public requestBanListDel requestBanList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestComplainAddDel();
            public requestComplainAddDel requestComplainAdd;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestComplainDelDel();
            public requestComplainDelDel requestComplainDel;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestComplainDelAllDel();
            public requestComplainDelAllDel requestComplainDelAll;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestComplainListDel();
            public requestComplainListDel requestComplainList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerGroupListDel();
            public requestServerGroupListDel requestServerGroupList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerGroupAddDel();
            public requestServerGroupAddDel requestServerGroupAdd;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerGroupDelDel();
            public requestServerGroupDelDel requestServerGroupDel;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerGroupAddClientDel();
            public requestServerGroupAddClientDel requestServerGroupAddClient;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerGroupDelClientDel();
            public requestServerGroupDelClientDel requestServerGroupDelClient;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerGroupsByClientIDDel();
            public requestServerGroupsByClientIDDel requestServerGroupsByClientID;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerGroupAddPermDel();
            public requestServerGroupAddPermDel requestServerGroupAddPerm;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerGroupDelPermDel();
            public requestServerGroupDelPermDel requestServerGroupDelPerm;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerGroupPermListDel();
            public requestServerGroupPermListDel requestServerGroupPermList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestServerGroupClientListDel();
            public requestServerGroupClientListDel requestServerGroupClientList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelGroupListDel();
            public requestChannelGroupListDel requestChannelGroupList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelGroupAddDel();
            public requestChannelGroupAddDel requestChannelGroupAdd;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelGroupDelDel();
            public requestChannelGroupDelDel requestChannelGroupDel;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelGroupAddPermDel();
            public requestChannelGroupAddPermDel requestChannelGroupAddPerm;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelGroupDelPermDel();
            public requestChannelGroupDelPermDel requestChannelGroupDelPerm;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelGroupPermListDel();
            public requestChannelGroupPermListDel requestChannelGroupPermList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestSetClientChannelGroupDel();
            public requestSetClientChannelGroupDel requestSetClientChannelGroup;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelAddPermDel();
            public requestChannelAddPermDel requestChannelAddPerm;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelDelPermDel();
            public requestChannelDelPermDel requestChannelDelPerm;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelPermListDel();
            public requestChannelPermListDel requestChannelPermList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientAddPermDel();
            public requestClientAddPermDel requestClientAddPerm;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientDelPermDel();
            public requestClientDelPermDel requestClientDelPerm;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestClientPermListDel();
            public requestClientPermListDel requestClientPermList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelClientAddPermDel();
            public requestChannelClientAddPermDel requestChannelClientAddPerm;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelClientDelPermDel();
            public requestChannelClientDelPermDel requestChannelClientDelPerm;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestChannelClientPermListDel();
            public requestChannelClientPermListDel requestChannelClientPermList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error privilegeKeyUseDel();
            public privilegeKeyUseDel privilegeKeyUse;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestPermissionListDel();
            public requestPermissionListDel requestPermissionList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestPermissionOverviewDel();
            public requestPermissionOverviewDel requestPermissionOverview;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error clientPropertyStringToFlagDel();
            public clientPropertyStringToFlagDel clientPropertyStringToFlag;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error channelPropertyStringToFlagDel();
            public channelPropertyStringToFlagDel channelPropertyStringToFlag;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error serverPropertyStringToFlagDel();
            public serverPropertyStringToFlagDel serverPropertyStringToFlag;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getAppPathDel();
            public getAppPathDel getAppPath;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getResourcesPathDel();
            public getResourcesPathDel getResourcesPath;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getConfigPathDel();
            public getConfigPathDel getConfigPath;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getPluginPathDel();
            public getPluginPathDel getPluginPath;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getCurrentServerConnectionHandlerIDDel();
            public getCurrentServerConnectionHandlerIDDel getCurrentServerConnectionHandlerID;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error printMessageDel();
            public printMessageDel printMessage;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error printMessageToCurrentTabDel();
            public printMessageToCurrentTabDel printMessageToCurrentTab;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error urlsToBBDel();
            public urlsToBBDel urlsToBB;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error sendPluginCommandDel();
            public sendPluginCommandDel sendPluginCommand;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getDirectoriesDel();
            public getDirectoriesDel getDirectories;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getServerConnectInfoDel();
            public getServerConnectInfoDel getServerConnectInfo;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getChannelConnectInfoDel();
            public getChannelConnectInfoDel getChannelConnectInfo;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error createReturnCodeDel();
            public createReturnCodeDel createReturnCode;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestInfoUpdateDel();
            public requestInfoUpdateDel requestInfoUpdate;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getServerVersionDel();
            public getServerVersionDel getServerVersion;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error isWhisperingDel();
            public isWhisperingDel isWhispering;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error isReceivingWhisperDel();
            public isReceivingWhisperDel isReceivingWhisper;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getAvatarDel();
            public getAvatarDel getAvatar;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error setPluginMenuEnabledDel();
            public setPluginMenuEnabledDel setPluginMenuEnabled;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error showHotkeySetupDel();
            public showHotkeySetupDel showHotkeySetup;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error requestHotkeyInputDialogDel();
            public requestHotkeyInputDialogDel requestHotkeyInputDialog;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getHotkeyFromKeywordDel();
            public getHotkeyFromKeywordDel getHotkeyFromKeyword;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getClientDisplayNameDel();
            public getClientDisplayNameDel getClientDisplayName;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getBookmarkListDel();
            public getBookmarkListDel getBookmarkList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getProfileListDel();
            public getProfileListDel getProfileList;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error guiConnectDel();
            public guiConnectDel guiConnect;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error guiConnectBookmarkDel();
            public guiConnectBookmarkDel guiConnectBookmark;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error createBookmarkDel();
            public createBookmarkDel createBookmark;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getPermissionIDByNameDel();
            public getPermissionIDByNameDel getPermissionIDByName;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate Error getClientNeededPermissionDel();
            public getClientNeededPermissionDel getClientNeededPermission;

            public TS3FunctionDelegates(TS3FunctionPointers ptrs)
            {
                getClientLibVersion = (getClientLibVersionDel)Marshal.GetDelegateForFunctionPointer(
                    ptrs.getClientLibVersion, typeof(getClientLibVersionDel));

                getClientLibVersionNumber = (getClientLibVersionNumberDel)Marshal.GetDelegateForFunctionPointer(
ptrs.getClientLibVersionNumber, typeof(getClientLibVersionNumberDel));

                spawnNewServerConnectionHandler = (spawnNewServerConnectionHandlerDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.spawnNewServerConnectionHandler, typeof(spawnNewServerConnectionHandlerDel));

                destroyServerConnectionHandler = (destroyServerConnectionHandlerDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.destroyServerConnectionHandler, typeof(destroyServerConnectionHandlerDel));

                getErrorMessage = (getErrorMessageDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getErrorMessage, typeof(getErrorMessageDel));

                freeMemory = (freeMemoryDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.freeMemory, typeof(freeMemoryDel));

                logMessage = (logMessageDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.logMessage, typeof(logMessageDel));

                getPlaybackDeviceList = (getPlaybackDeviceListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getPlaybackDeviceList, typeof(getPlaybackDeviceListDel));

                getPlaybackModeList = (getPlaybackModeListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getPlaybackModeList, typeof(getPlaybackModeListDel));

                getCaptureDeviceList = (getCaptureDeviceListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getCaptureDeviceList, typeof(getCaptureDeviceListDel));

                getCaptureModeList = (getCaptureModeListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getCaptureModeList, typeof(getCaptureModeListDel));

                getDefaultPlaybackDevice = (getDefaultPlaybackDeviceDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getDefaultPlaybackDevice, typeof(getDefaultPlaybackDeviceDel));

                getDefaultPlayBackMode = (getDefaultPlayBackModeDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getDefaultPlayBackMode, typeof(getDefaultPlayBackModeDel));

                getDefaultCaptureDevice = (getDefaultCaptureDeviceDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getDefaultCaptureDevice, typeof(getDefaultCaptureDeviceDel));

                getDefaultCaptureMode = (getDefaultCaptureModeDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getDefaultCaptureMode, typeof(getDefaultCaptureModeDel));

                openPlaybackDevice = (openPlaybackDeviceDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.openPlaybackDevice, typeof(openPlaybackDeviceDel));

                openCaptureDevice = (openCaptureDeviceDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.openCaptureDevice, typeof(openCaptureDeviceDel));

                getCurrentPlaybackDeviceName = (getCurrentPlaybackDeviceNameDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getCurrentPlaybackDeviceName, typeof(getCurrentPlaybackDeviceNameDel));

                getCurrentPlayBackMode = (getCurrentPlayBackModeDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getCurrentPlayBackMode, typeof(getCurrentPlayBackModeDel));

                getCurrentCaptureDeviceName = (getCurrentCaptureDeviceNameDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getCurrentCaptureDeviceName, typeof(getCurrentCaptureDeviceNameDel));

                getCurrentCaptureMode = (getCurrentCaptureModeDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getCurrentCaptureMode, typeof(getCurrentCaptureModeDel));

                initiateGracefulPlaybackShutdown = (initiateGracefulPlaybackShutdownDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.initiateGracefulPlaybackShutdown, typeof(initiateGracefulPlaybackShutdownDel));

                closePlaybackDevice = (closePlaybackDeviceDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.closePlaybackDevice, typeof(closePlaybackDeviceDel));

                closeCaptureDevice = (closeCaptureDeviceDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.closeCaptureDevice, typeof(closeCaptureDeviceDel));

                activateCaptureDevice = (activateCaptureDeviceDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.activateCaptureDevice, typeof(activateCaptureDeviceDel));

                playWaveFileHandle = (playWaveFileHandleDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.playWaveFileHandle, typeof(playWaveFileHandleDel));

                pauseWaveFileHandle = (pauseWaveFileHandleDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.pauseWaveFileHandle, typeof(pauseWaveFileHandleDel));

                closeWaveFileHandle = (closeWaveFileHandleDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.closeWaveFileHandle, typeof(closeWaveFileHandleDel));

                playWaveFile = (playWaveFileDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.playWaveFile, typeof(playWaveFileDel));

                registerCustomDevice = (registerCustomDeviceDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.registerCustomDevice, typeof(registerCustomDeviceDel));

                unregisterCustomDevice = (unregisterCustomDeviceDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.unregisterCustomDevice, typeof(unregisterCustomDeviceDel));

                processCustomCaptureData = (processCustomCaptureDataDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.processCustomCaptureData, typeof(processCustomCaptureDataDel));

                acquireCustomPlaybackData = (acquireCustomPlaybackDataDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.acquireCustomPlaybackData, typeof(acquireCustomPlaybackDataDel));

                getPreProcessorInfoValueFloat = (getPreProcessorInfoValueFloatDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getPreProcessorInfoValueFloat, typeof(getPreProcessorInfoValueFloatDel));

                getPreProcessorConfigValue = (getPreProcessorConfigValueDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getPreProcessorConfigValue, typeof(getPreProcessorConfigValueDel));

                setPreProcessorConfigValue = (setPreProcessorConfigValueDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.setPreProcessorConfigValue, typeof(setPreProcessorConfigValueDel));

                getEncodeConfigValue = (getEncodeConfigValueDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getEncodeConfigValue, typeof(getEncodeConfigValueDel));

                getPlaybackConfigValueAsFloat = (getPlaybackConfigValueAsFloatDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getPlaybackConfigValueAsFloat, typeof(getPlaybackConfigValueAsFloatDel));

                setPlaybackConfigValue = (setPlaybackConfigValueDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.setPlaybackConfigValue, typeof(setPlaybackConfigValueDel));

                setClientVolumeModifier = (setClientVolumeModifierDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.setClientVolumeModifier, typeof(setClientVolumeModifierDel));

                startVoiceRecording = (startVoiceRecordingDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.startVoiceRecording, typeof(startVoiceRecordingDel));

                stopVoiceRecording = (stopVoiceRecordingDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.stopVoiceRecording, typeof(stopVoiceRecordingDel));

                systemset3DListenerAttributes = (systemset3DListenerAttributesDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.systemset3DListenerAttributes, typeof(systemset3DListenerAttributesDel));

                set3DWaveAttributes = (set3DWaveAttributesDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.set3DWaveAttributes, typeof(set3DWaveAttributesDel));

                systemset3DSettings = (systemset3DSettingsDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.systemset3DSettings, typeof(systemset3DSettingsDel));

                channelset3DAttributes = (channelset3DAttributesDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.channelset3DAttributes, typeof(channelset3DAttributesDel));

                startConnection = (startConnectionDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.startConnection, typeof(startConnectionDel));

                stopConnection = (stopConnectionDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.stopConnection, typeof(stopConnectionDel));

                requestClientMove = (requestClientMoveDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientMove, typeof(requestClientMoveDel));

                requestClientVariables = (requestClientVariablesDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientVariables, typeof(requestClientVariablesDel));

                requestClientKickFromChannel = (requestClientKickFromChannelDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientKickFromChannel, typeof(requestClientKickFromChannelDel));

                requestClientKickFromServer = (requestClientKickFromServerDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientKickFromServer, typeof(requestClientKickFromServerDel));

                requestChannelDelete = (requestChannelDeleteDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelDelete, typeof(requestChannelDeleteDel));

                requestChannelMove = (requestChannelMoveDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelMove, typeof(requestChannelMoveDel));

                requestSendPrivateTextMsg = (requestSendPrivateTextMsgDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestSendPrivateTextMsg, typeof(requestSendPrivateTextMsgDel));

                requestSendChannelTextMsg = (requestSendChannelTextMsgDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestSendChannelTextMsg, typeof(requestSendChannelTextMsgDel));

                requestSendServerTextMsg = (requestSendServerTextMsgDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestSendServerTextMsg, typeof(requestSendServerTextMsgDel));

                requestConnectionInfo = (requestConnectionInfoDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestConnectionInfo, typeof(requestConnectionInfoDel));

                requestClientSetWhisperList = (requestClientSetWhisperListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientSetWhisperList, typeof(requestClientSetWhisperListDel));

                requestChannelSubscribe = (requestChannelSubscribeDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelSubscribe, typeof(requestChannelSubscribeDel));

                requestChannelSubscribeAll = (requestChannelSubscribeAllDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelSubscribeAll, typeof(requestChannelSubscribeAllDel));

                requestChannelUnsubscribe = (requestChannelUnsubscribeDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelUnsubscribe, typeof(requestChannelUnsubscribeDel));

                requestChannelUnsubscribeAll = (requestChannelUnsubscribeAllDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelUnsubscribeAll, typeof(requestChannelUnsubscribeAllDel));

                requestChannelDescription = (requestChannelDescriptionDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelDescription, typeof(requestChannelDescriptionDel));

                requestMuteClients = (requestMuteClientsDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestMuteClients, typeof(requestMuteClientsDel));

                requestUnmuteClients = (requestUnmuteClientsDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestUnmuteClients, typeof(requestUnmuteClientsDel));

                requestClientPoke = (requestClientPokeDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientPoke, typeof(requestClientPokeDel));

                requestClientIDs = (requestClientIDsDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientIDs, typeof(requestClientIDsDel));

                clientChatClosed = (clientChatClosedDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.clientChatClosed, typeof(clientChatClosedDel));

                clientChatComposing = (clientChatComposingDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.clientChatComposing, typeof(clientChatComposingDel));

                requestServerTemporaryPasswordAdd = (requestServerTemporaryPasswordAddDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerTemporaryPasswordAdd, typeof(requestServerTemporaryPasswordAddDel));

                requestServerTemporaryPasswordDel = (requestServerTemporaryPasswordDelDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerTemporaryPasswordDel, typeof(requestServerTemporaryPasswordDelDel));

                requestServerTemporaryPasswordList = (requestServerTemporaryPasswordListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerTemporaryPasswordList, typeof(requestServerTemporaryPasswordListDel));

                getClientID = (getClientIDDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getClientID, typeof(getClientIDDel));

                getClientSelfVariableAsInt = (getClientSelfVariableAsIntDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getClientSelfVariableAsInt, typeof(getClientSelfVariableAsIntDel));

                getClientSelfVariableAsString = (getClientSelfVariableAsStringDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getClientSelfVariableAsString, typeof(getClientSelfVariableAsStringDel));

                setClientSelfVariableAsInt = (setClientSelfVariableAsIntDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.setClientSelfVariableAsInt, typeof(setClientSelfVariableAsIntDel));

                setClientSelfVariableAsString = (setClientSelfVariableAsStringDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.setClientSelfVariableAsString, typeof(setClientSelfVariableAsStringDel));

                flushClientSelfUpdates = (flushClientSelfUpdatesDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.flushClientSelfUpdates, typeof(flushClientSelfUpdatesDel));

                getClientVariableAsInt = (getClientVariableAsIntDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getClientVariableAsInt, typeof(getClientVariableAsIntDel));

                getClientVariableAsUInt64 = (getClientVariableAsUInt64Del)Marshal.GetDelegateForFunctionPointer(
                ptrs.getClientVariableAsUInt64, typeof(getClientVariableAsUInt64Del));

                getClientVariableAsString = (getClientVariableAsStringDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getClientVariableAsString, typeof(getClientVariableAsStringDel));

                getClientList = (getClientListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getClientList, typeof(getClientListDel));

                getChannelOfClient = (getChannelOfClientDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getChannelOfClient, typeof(getChannelOfClientDel));

                getChannelVariableAsInt = (getChannelVariableAsIntDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getChannelVariableAsInt, typeof(getChannelVariableAsIntDel));

                getChannelVariableAsUInt64 = (getChannelVariableAsUInt64Del)Marshal.GetDelegateForFunctionPointer(
                ptrs.getChannelVariableAsUInt64, typeof(getChannelVariableAsUInt64Del));

                getChannelVariableAsString = (getChannelVariableAsStringDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getChannelVariableAsString, typeof(getChannelVariableAsStringDel));

                getChannelIDFromChannelNames = (getChannelIDFromChannelNamesDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getChannelIDFromChannelNames, typeof(getChannelIDFromChannelNamesDel));

                setChannelVariableAsInt = (setChannelVariableAsIntDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.setChannelVariableAsInt, typeof(setChannelVariableAsIntDel));

                setChannelVariableAsUInt64 = (setChannelVariableAsUInt64Del)Marshal.GetDelegateForFunctionPointer(
                ptrs.setChannelVariableAsUInt64, typeof(setChannelVariableAsUInt64Del));

                setChannelVariableAsString = (setChannelVariableAsStringDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.setChannelVariableAsString, typeof(setChannelVariableAsStringDel));

                flushChannelUpdates = (flushChannelUpdatesDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.flushChannelUpdates, typeof(flushChannelUpdatesDel));

                flushChannelCreation = (flushChannelCreationDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.flushChannelCreation, typeof(flushChannelCreationDel));

                getChannelList = (getChannelListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getChannelList, typeof(getChannelListDel));

                getChannelClientList = (getChannelClientListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getChannelClientList, typeof(getChannelClientListDel));

                getParentChannelOfChannel = (getParentChannelOfChannelDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getParentChannelOfChannel, typeof(getParentChannelOfChannelDel));

                getServerConnectionHandlerList = (getServerConnectionHandlerListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getServerConnectionHandlerList, typeof(getServerConnectionHandlerListDel));

                getServerVariableAsInt = (getServerVariableAsIntDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getServerVariableAsInt, typeof(getServerVariableAsIntDel));

                getServerVariableAsUInt64 = (getServerVariableAsUInt64Del)Marshal.GetDelegateForFunctionPointer(
                ptrs.getServerVariableAsUInt64, typeof(getServerVariableAsUInt64Del));

                getServerVariableAsString = (getServerVariableAsStringDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getServerVariableAsString, typeof(getServerVariableAsStringDel));

                requestServerVariables = (requestServerVariablesDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerVariables, typeof(requestServerVariablesDel));

                getConnectionStatus = (getConnectionStatusDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getConnectionStatus, typeof(getConnectionStatusDel));

                getConnectionVariableAsUInt64 = (getConnectionVariableAsUInt64Del)Marshal.GetDelegateForFunctionPointer(
                ptrs.getConnectionVariableAsUInt64, typeof(getConnectionVariableAsUInt64Del));

                getConnectionVariableAsDouble = (getConnectionVariableAsDoubleDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getConnectionVariableAsDouble, typeof(getConnectionVariableAsDoubleDel));

                getConnectionVariableAsString = (getConnectionVariableAsStringDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getConnectionVariableAsString, typeof(getConnectionVariableAsStringDel));

                cleanUpConnectionInfo = (cleanUpConnectionInfoDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.cleanUpConnectionInfo, typeof(cleanUpConnectionInfoDel));

                requestClientDBIDfromUID = (requestClientDBIDfromUIDDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientDBIDfromUID, typeof(requestClientDBIDfromUIDDel));

                requestClientNamefromUID = (requestClientNamefromUIDDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientNamefromUID, typeof(requestClientNamefromUIDDel));

                requestClientNamefromDBID = (requestClientNamefromDBIDDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientNamefromDBID, typeof(requestClientNamefromDBIDDel));

                requestClientEditDescription = (requestClientEditDescriptionDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientEditDescription, typeof(requestClientEditDescriptionDel));

                requestClientSetIsTalker = (requestClientSetIsTalkerDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientSetIsTalker, typeof(requestClientSetIsTalkerDel));

                requestIsTalker = (requestIsTalkerDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestIsTalker, typeof(requestIsTalkerDel));

                requestSendClientQueryCommand = (requestSendClientQueryCommandDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestSendClientQueryCommand, typeof(requestSendClientQueryCommandDel));

                getTransferFileName = (getTransferFileNameDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getTransferFileName, typeof(getTransferFileNameDel));

                getTransferFilePath = (getTransferFilePathDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getTransferFilePath, typeof(getTransferFilePathDel));

                getTransferFileSize = (getTransferFileSizeDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getTransferFileSize, typeof(getTransferFileSizeDel));

                getTransferFileSizeDone = (getTransferFileSizeDoneDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getTransferFileSizeDone, typeof(getTransferFileSizeDoneDel));

                isTransferSender = (isTransferSenderDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.isTransferSender, typeof(isTransferSenderDel));

                getTransferStatus = (getTransferStatusDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getTransferStatus, typeof(getTransferStatusDel));

                getCurrentTransferSpeed = (getCurrentTransferSpeedDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getCurrentTransferSpeed, typeof(getCurrentTransferSpeedDel));

                getAverageTransferSpeed = (getAverageTransferSpeedDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getAverageTransferSpeed, typeof(getAverageTransferSpeedDel));

                getTransferRunTime = (getTransferRunTimeDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getTransferRunTime, typeof(getTransferRunTimeDel));

                sendFile = (sendFileDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.sendFile, typeof(sendFileDel));

                requestFile = (requestFileDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestFile, typeof(requestFileDel));

                haltTransfer = (haltTransferDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.haltTransfer, typeof(haltTransferDel));

                requestFileList = (requestFileListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestFileList, typeof(requestFileListDel));

                requestFileInfo = (requestFileInfoDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestFileInfo, typeof(requestFileInfoDel));

                requestDeleteFile = (requestDeleteFileDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestDeleteFile, typeof(requestDeleteFileDel));

                requestCreateDirectory = (requestCreateDirectoryDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestCreateDirectory, typeof(requestCreateDirectoryDel));

                requestRenameFile = (requestRenameFileDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestRenameFile, typeof(requestRenameFileDel));

                requestMessageAdd = (requestMessageAddDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestMessageAdd, typeof(requestMessageAddDel));

                requestMessageDel = (requestMessageDelDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestMessageDel, typeof(requestMessageDelDel));

                requestMessageGet = (requestMessageGetDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestMessageGet, typeof(requestMessageGetDel));

                requestMessageList = (requestMessageListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestMessageList, typeof(requestMessageListDel));

                requestMessageUpdateFlag = (requestMessageUpdateFlagDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestMessageUpdateFlag, typeof(requestMessageUpdateFlagDel));

                verifyServerPassword = (verifyServerPasswordDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.verifyServerPassword, typeof(verifyServerPasswordDel));

                verifyChannelPassword = (verifyChannelPasswordDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.verifyChannelPassword, typeof(verifyChannelPasswordDel));

                banclient = (banclientDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.banclient, typeof(banclientDel));

                banadd = (banaddDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.banadd, typeof(banaddDel));

                banclientdbid = (banclientdbidDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.banclientdbid, typeof(banclientdbidDel));

                bandel = (bandelDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.bandel, typeof(bandelDel));

                bandelall = (bandelallDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.bandelall, typeof(bandelallDel));

                requestBanList = (requestBanListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestBanList, typeof(requestBanListDel));

                requestComplainAdd = (requestComplainAddDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestComplainAdd, typeof(requestComplainAddDel));

                requestComplainDel = (requestComplainDelDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestComplainDel, typeof(requestComplainDelDel));

                requestComplainDelAll = (requestComplainDelAllDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestComplainDelAll, typeof(requestComplainDelAllDel));

                requestComplainList = (requestComplainListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestComplainList, typeof(requestComplainListDel));

                requestServerGroupList = (requestServerGroupListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerGroupList, typeof(requestServerGroupListDel));

                requestServerGroupAdd = (requestServerGroupAddDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerGroupAdd, typeof(requestServerGroupAddDel));

                requestServerGroupDel = (requestServerGroupDelDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerGroupDel, typeof(requestServerGroupDelDel));

                requestServerGroupAddClient = (requestServerGroupAddClientDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerGroupAddClient, typeof(requestServerGroupAddClientDel));

                requestServerGroupDelClient = (requestServerGroupDelClientDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerGroupDelClient, typeof(requestServerGroupDelClientDel));

                requestServerGroupsByClientID = (requestServerGroupsByClientIDDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerGroupsByClientID, typeof(requestServerGroupsByClientIDDel));

                requestServerGroupAddPerm = (requestServerGroupAddPermDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerGroupAddPerm, typeof(requestServerGroupAddPermDel));

                requestServerGroupDelPerm = (requestServerGroupDelPermDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerGroupDelPerm, typeof(requestServerGroupDelPermDel));

                requestServerGroupPermList = (requestServerGroupPermListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerGroupPermList, typeof(requestServerGroupPermListDel));

                requestServerGroupClientList = (requestServerGroupClientListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestServerGroupClientList, typeof(requestServerGroupClientListDel));

                requestChannelGroupList = (requestChannelGroupListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelGroupList, typeof(requestChannelGroupListDel));

                requestChannelGroupAdd = (requestChannelGroupAddDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelGroupAdd, typeof(requestChannelGroupAddDel));

                requestChannelGroupDel = (requestChannelGroupDelDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelGroupDel, typeof(requestChannelGroupDelDel));

                requestChannelGroupAddPerm = (requestChannelGroupAddPermDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelGroupAddPerm, typeof(requestChannelGroupAddPermDel));

                requestChannelGroupDelPerm = (requestChannelGroupDelPermDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelGroupDelPerm, typeof(requestChannelGroupDelPermDel));

                requestChannelGroupPermList = (requestChannelGroupPermListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelGroupPermList, typeof(requestChannelGroupPermListDel));

                requestSetClientChannelGroup = (requestSetClientChannelGroupDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestSetClientChannelGroup, typeof(requestSetClientChannelGroupDel));

                requestChannelAddPerm = (requestChannelAddPermDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelAddPerm, typeof(requestChannelAddPermDel));

                requestChannelDelPerm = (requestChannelDelPermDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelDelPerm, typeof(requestChannelDelPermDel));

                requestChannelPermList = (requestChannelPermListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelPermList, typeof(requestChannelPermListDel));

                requestClientAddPerm = (requestClientAddPermDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientAddPerm, typeof(requestClientAddPermDel));

                requestClientDelPerm = (requestClientDelPermDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientDelPerm, typeof(requestClientDelPermDel));

                requestClientPermList = (requestClientPermListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestClientPermList, typeof(requestClientPermListDel));

                requestChannelClientAddPerm = (requestChannelClientAddPermDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelClientAddPerm, typeof(requestChannelClientAddPermDel));

                requestChannelClientDelPerm = (requestChannelClientDelPermDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelClientDelPerm, typeof(requestChannelClientDelPermDel));

                requestChannelClientPermList = (requestChannelClientPermListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestChannelClientPermList, typeof(requestChannelClientPermListDel));

                privilegeKeyUse = (privilegeKeyUseDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.privilegeKeyUse, typeof(privilegeKeyUseDel));

                requestPermissionList = (requestPermissionListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestPermissionList, typeof(requestPermissionListDel));

                requestPermissionOverview = (requestPermissionOverviewDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestPermissionOverview, typeof(requestPermissionOverviewDel));

                clientPropertyStringToFlag = (clientPropertyStringToFlagDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.clientPropertyStringToFlag, typeof(clientPropertyStringToFlagDel));

                channelPropertyStringToFlag = (channelPropertyStringToFlagDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.channelPropertyStringToFlag, typeof(channelPropertyStringToFlagDel));

                serverPropertyStringToFlag = (serverPropertyStringToFlagDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.serverPropertyStringToFlag, typeof(serverPropertyStringToFlagDel));

                getAppPath = (getAppPathDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getAppPath, typeof(getAppPathDel));

                getResourcesPath = (getResourcesPathDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getResourcesPath, typeof(getResourcesPathDel));

                getConfigPath = (getConfigPathDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getConfigPath, typeof(getConfigPathDel));

                getPluginPath = (getPluginPathDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getPluginPath, typeof(getPluginPathDel));

                getCurrentServerConnectionHandlerID = (getCurrentServerConnectionHandlerIDDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getCurrentServerConnectionHandlerID, typeof(getCurrentServerConnectionHandlerIDDel));

                printMessage = (printMessageDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.printMessage, typeof(printMessageDel));

                printMessageToCurrentTab = (printMessageToCurrentTabDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.printMessageToCurrentTab, typeof(printMessageToCurrentTabDel));

                urlsToBB = (urlsToBBDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.urlsToBB, typeof(urlsToBBDel));

                sendPluginCommand = (sendPluginCommandDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.sendPluginCommand, typeof(sendPluginCommandDel));

                getDirectories = (getDirectoriesDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getDirectories, typeof(getDirectoriesDel));

                getServerConnectInfo = (getServerConnectInfoDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getServerConnectInfo, typeof(getServerConnectInfoDel));

                getChannelConnectInfo = (getChannelConnectInfoDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getChannelConnectInfo, typeof(getChannelConnectInfoDel));

                createReturnCode = (createReturnCodeDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.createReturnCode, typeof(createReturnCodeDel));

                requestInfoUpdate = (requestInfoUpdateDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestInfoUpdate, typeof(requestInfoUpdateDel));

                getServerVersion = (getServerVersionDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getServerVersion, typeof(getServerVersionDel));

                isWhispering = (isWhisperingDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.isWhispering, typeof(isWhisperingDel));

                isReceivingWhisper = (isReceivingWhisperDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.isReceivingWhisper, typeof(isReceivingWhisperDel));

                getAvatar = (getAvatarDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getAvatar, typeof(getAvatarDel));

                setPluginMenuEnabled = (setPluginMenuEnabledDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.setPluginMenuEnabled, typeof(setPluginMenuEnabledDel));

                showHotkeySetup = (showHotkeySetupDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.showHotkeySetup, typeof(showHotkeySetupDel));

                requestHotkeyInputDialog = (requestHotkeyInputDialogDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.requestHotkeyInputDialog, typeof(requestHotkeyInputDialogDel));

                getHotkeyFromKeyword = (getHotkeyFromKeywordDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getHotkeyFromKeyword, typeof(getHotkeyFromKeywordDel));

                getClientDisplayName = (getClientDisplayNameDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getClientDisplayName, typeof(getClientDisplayNameDel));

                getBookmarkList = (getBookmarkListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getBookmarkList, typeof(getBookmarkListDel));

                getProfileList = (getProfileListDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getProfileList, typeof(getProfileListDel));

                guiConnect = (guiConnectDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.guiConnect, typeof(guiConnectDel));

                guiConnectBookmark = (guiConnectBookmarkDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.guiConnectBookmark, typeof(guiConnectBookmarkDel));

                createBookmark = (createBookmarkDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.createBookmark, typeof(createBookmarkDel));

                getPermissionIDByName = (getPermissionIDByNameDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getPermissionIDByName, typeof(getPermissionIDByNameDel));

                getClientNeededPermission = (getClientNeededPermissionDel)Marshal.GetDelegateForFunctionPointer(
                ptrs.getClientNeededPermission, typeof(getClientNeededPermissionDel));
            }

        }

    }
}
