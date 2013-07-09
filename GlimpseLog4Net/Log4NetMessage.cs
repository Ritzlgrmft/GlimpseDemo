using System;
using Glimpse.Core.Message;

namespace GlimpseLog4Net
{
	/// <summary>
	/// Message containing all data needed for display in Glimpse.
	/// </summary>
	public class Log4NetMessage : IMessage
	{
		/// <summary>
		/// Id of the message.
		/// </summary>
		public Guid Id { get; private set; }

		/// <summary>
		/// Gets or sets the time from the request start.
		/// </summary>
		public TimeSpan FromFirst { get; set; }

		/// <summary>
		/// Gets or sets the time from the last trace event.
		/// </summary>
		/// <value>From last.</value>
		public TimeSpan FromLast { get; set; }

		/// <summary>
		/// Name of the thread.
		/// </summary>
		public string ThreadName { get; set; }

		/// <summary>
		/// Display name of the log level.
		/// </summary>
		public string Level { get; set; }

		/// <summary>
		/// Name of the logger.
		/// </summary>
		public string LoggerName { get; set; }

		/// <summary>
		/// The message itself.
		/// </summary>
		public string Message { get; set; }

		public Log4NetMessage()
		{
			Id = Guid.NewGuid();
		}
	}
}