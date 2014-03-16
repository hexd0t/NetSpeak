#pragma once
#ifdef _DEBUG
#using "NetSpeakManaged.dll"
#else
#using "NetSpeakManaged.netmodule"
#endif

ref class MP
{
public:
	static NetSpeakManaged::Plugin^ Instance;
	static NetSpeakManaged::TS3::TS3Events^ Events;
};