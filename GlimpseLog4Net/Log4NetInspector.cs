using Glimpse.Core.Extensibility;

namespace GlimpseLog4Net
{
	/// <summary>
	/// The implementation of <see cref="IInspector"/> for providing the necessary info for
	/// <see cref="GlimpseAppender" />.
	/// </summary>
	public class Log4NetInspector : IInspector
	{
		/// <summary>
		/// Setups the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <remarks>
		/// Executed during the <see cref="Glimpse.Core.Framework.IGlimpseRuntime.Initialize" /> phase of
		/// system startup. Specifically, with the ASP.NET provider, this is wired to/implemented by the
		/// <c>System.Web.IHttpModule.Init</c> method.
		/// </remarks>
		public void Setup(IInspectorContext context)
		{
			GlimpseAppender.Initialize(context.MessageBroker, context.TimerStrategy);
		}
	}
}