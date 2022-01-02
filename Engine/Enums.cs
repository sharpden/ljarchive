using System;

namespace EF.ljArchive.Engine
{
	/// <summary>
	/// Defines status codes that describe which operation the <see cref="Sync"/> class is performing.
	/// </summary>
	public enum SyncOperation
	{
		/// <summary>
		/// Initialize variables.
		/// </summary>
		Initialize,
		/// <summary>
		/// Log in to the server.
		/// </summary>
		Login,
		/// <summary>
		/// Get a sync index.
		/// </summary>
		SyncItems,
		/// <summary>
		/// Get journal events.
		/// </summary>
		GetEvents,
		/// <summary>
		/// Generate a web session.
		/// </summary>
		SessionGenerate,
		/// <summary>
		/// Download comments metadata.
		/// </summary>
		ExportCommentsMeta,
		/// <summary>
		/// Download comments.
		/// </summary>
		ExportCommentsBody,
		/// <summary>
		/// Merge results with local dataset.
		/// </summary>
		Merge,
		/// <summary>
		/// Sync success.
		/// </summary>
		Success,
		/// <summary>
		/// Only a partial sync.
		/// </summary>
		PartialSync,
		/// <summary>
		/// Sync failure.
		/// </summary>
		Failure
	}

	/// <summary>
	/// Defines standard error categories that Sync may encounter while running.
	/// </summary>
	public enum ExpectedError
	{
		/// <summary>
		/// User has entered an invalid password.
		/// </summary>
		InvalidPassword,
		/// <summary>
		/// User has repeated unsuccessful sync attempts.
		/// </summary>
		RepeatedRequests,
		/// <summary>
		/// User doesn't have access to the community being archived.
		/// </summary>
		CommunityAccessDenied,
		/// <summary>
		/// User has not set encoding settings on the server.
		/// </summary>
		NoEncodingSettings,
		/// <summary>
		/// Server does not support xmlrpc.
		/// </summary>
		XMLRPCNotSupported,
		/// <summary>
		/// Server does not support exporting comments.
		/// </summary>
		ExportCommentsNotSupported,
		/// <summary>
		/// The server is not responding.
		/// </summary>
		ServerNotResponding,
		/// <summary>
		/// User cancelled the sync.
		/// </summary>
		Cancel
	}

	/// <summary>
	/// Defines categories that <see cref="Exporter"/> uses to decide how to split its export into single or multiple
	/// files.
	/// </summary>
	public enum SplitExport
	{
		/// <summary>
		/// One file for the entire export.
		/// </summary>
		Single = 0,
		/// <summary>
		/// One file per year of the export.
		/// </summary>
		PerYear = 1,
		/// <summary>
		/// One file per month of the export.
		/// </summary>
		PerMonth = 2,
		/// <summary>
		/// One file per entry.
		/// </summary>
		PerEntry = 3
	}
}
