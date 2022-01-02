using System;
using CookComputing.XmlRpc;
using EF.ljArchive.Engine.XMLStructs;

namespace EF.ljArchive.Engine
{
	/// <summary>
	/// LJServer proxy interface used by xml-rpc.
	/// </summary>
	[XmlRpcUrl("http://www.livejournal.com/interface/xmlrpc")]
	public interface ILJServer
	{
		[XmlRpcMethod("LJ.XMLRPC.login")]
		LoginResponse Login(LoginParams loginParams);

		[XmlRpcMethod("LJ.XMLRPC.syncitems")]
		SyncItemsResponse SyncItems(SyncItemsParams syncItemsParams);

		[XmlRpcMethod("LJ.XMLRPC.getevents")]
		GetEventsResponse GetEvents(GetEventsParams getEventsParams);

		[XmlRpcMethod("LJ.XMLRPC.sessiongenerate")]
		SessionGenerateResponse SessionGenerate(SessionGenerateParams sessionGenerateParams);

		[XmlRpcMethod("LJ.XMLRPC.getchallenge")]
		GetChallengeResponse GetChallenge();
	}



}
