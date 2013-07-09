using Glimpse.Core.Extensibility;
using Glimpse.Core.Tab.Assist;
using System.Collections.Generic;

namespace GlimpseLog4Net
{
	/// <summary>
	/// The <see cref="ISerializationConverter"/> implementation responsible converting <see cref="Log4NetMessage"/>s 
	/// representation's into a format suitable for serialization.
	/// </summary>
	public class Log4NetMessagesConverter : SerializationConverter<IEnumerable<Log4NetMessage>>
	{
		/// <summary>
		/// Converts the specified object.
		/// </summary>
		/// <param name="obj">The object to transform.</param>
		/// <returns>The new object representation.</returns>
		public override object Convert(IEnumerable<Log4NetMessage> obj)
		{
			var root = new TabSection("Level", "ThreadName", "LoggerName", "Message", "From Request Start", "From Last");
			foreach (var item in obj)
			{
				root.AddRow().
					Column(item.Level).
					Column(item.ThreadName).
					Column(item.LoggerName).
					Column(item.Message).
					Column(item.FromFirst).
					Column(item.FromLast).
					Style(GetStyle(item.Level));
			}

			return root.Build();
		}

		/// <summary>
		/// Returns the Glimpse style for the log message.
		/// </summary>
		/// <param name="levelDisplayName"></param>
		/// <returns></returns>
		private static string GetStyle(string levelDisplayName)
		{
			switch (levelDisplayName)
			{
				case "EMERGENCY":
				case "FATAL":
				case "ALERT":
					return FormattingKeywords.Fail;

				case "CRITICAL":
				case "SEVERE":
				case "ERROR":
					return FormattingKeywords.Error;

				case "WARN":
					return FormattingKeywords.Warn;

				case "NOTICE":
				case "INFO":
					return FormattingKeywords.Info;

				case "DEBUG":
				case "FINE":
				case "TRACE":
				case "FINER":
				case "VERBOSE":
				case "FINEST":
				case "ALL":
					return FormattingKeywords.Quiet;

				default:
					return FormattingKeywords.Quiet;
			}
		}

	}
}