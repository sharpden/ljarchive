using System;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// The response from calling <see cref="ILJServer.SessionGenerate"/>.
	/// </summary>
	public struct SessionGenerateResponse
	{
		public SessionGenerateResponse(string ljsession)
		{
			this.ljsession = ljsession;
		}
		public string ljsession;
	}
}
