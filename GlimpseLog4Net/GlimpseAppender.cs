using Glimpse.Core.Extensibility;
using log4net.Appender;
using log4net.Core;
using System;
using System.Diagnostics;

namespace GlimpseLog4Net
{
	/// <summary>
	/// Log4Net appender whichs sends log4net messages to Glimpse.
	/// </summary>
	public class GlimpseAppender : AppenderSkeleton
	{
		private static IMessageBroker _messageBroker;
		private static Func<IExecutionTimer> _timerStrategy;

		private static Stopwatch fromLastWatch;

		/// <summary>
		/// Initializes some static properties taken from Glimpse context.
		/// </summary>
		/// <param name="messageBroker"></param>
		/// <param name="timerStrategy"></param>
		public static void Initialize(IMessageBroker messageBroker, Func<IExecutionTimer> timerStrategy)
		{
			_messageBroker = messageBroker;
			_timerStrategy = timerStrategy;
		}

		/// <summary>
		/// The one and only method which converts the <see cref="LoggingEvent" /> into a <see cref="Log4NetMessage"/>, 
		/// which will be displayed in Glimpse.
		/// </summary>
		/// <remarks>
		/// For sending the message to Glimpse, an <see cref="IMessageBroker"/> is needed.
		/// This is stored by the <see cref="Log4NetInspector"/> in log4net's global context properties.
		/// </remarks>
		/// <param name="loggingEvent"></param>
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (_timerStrategy != null && _messageBroker != null)
			{
				IExecutionTimer timer = _timerStrategy();
				if (timer != null)
				{
					_messageBroker.Publish(new Log4NetMessage
						{
							ThreadName = loggingEvent.ThreadName,
							Level = loggingEvent.Level.DisplayName,
							LoggerName = loggingEvent.LoggerName,
							Message = loggingEvent.RenderedMessage,
							FromFirst = timer.Point().Offset,
							FromLast = CalculateFromLast(timer)
						});
				}
			}
		}

		/// <summary>
		/// Calculates the elapsed time since the last event.
		/// </summary>
		/// <param name="timer"></param>
		/// <returns></returns>
		private static TimeSpan CalculateFromLast(IExecutionTimer timer)
		{
			if (fromLastWatch == null)
			{
				fromLastWatch = Stopwatch.StartNew();
				return TimeSpan.FromMilliseconds(0);
			}

			// Timer started before this request, reset it
			if (DateTime.Now - fromLastWatch.Elapsed < timer.RequestStart)
			{
				fromLastWatch = Stopwatch.StartNew();
				return TimeSpan.FromMilliseconds(0);
			}

			var result = fromLastWatch.Elapsed;
			fromLastWatch = Stopwatch.StartNew();
			return result;
		}

	}
}