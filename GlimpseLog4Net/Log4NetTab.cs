using Glimpse.Core.Extensibility;
using Glimpse.Core.Extensions;
using Glimpse.Core.Tab.Assist;
using System.Linq;

namespace GlimpseLog4Net
{
	/// <summary>
	/// Log4Net tab for Glimpse.
	/// </summary>
	public class Log4NetTab : TabBase, ITabSetup, IKey, ITabLayout
	{
		private static readonly object layout = TabLayout.Create()
				.Row(r =>
				{
					r.Cell(0).WidthInPixels(80);
					r.Cell(1).WidthInPixels(80);
					r.Cell(2);
					r.Cell(3);
					r.Cell(4).WidthInPercent(15).Suffix(" ms").AlignRight().Prefix("T+ ").Class("mono");
					r.Cell(5).WidthInPercent(15).Suffix(" ms").AlignRight().Class("mono");
				}).Build();

		#region TabBase implementation

		/// <summary>
		/// Gets the data that should be shown in the UI.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>List of <see cref="Log4NetMessage"/>s that will be shown.</returns>
		public override object GetData(ITabContext context)
		{
			return context.GetMessages<Log4NetMessage>().ToList();
		}

		/// <summary>
		/// Gets the name that will show in the tab.
		/// </summary>
		/// <value>The name.</value>
		public override string Name
		{
			get { return "Log4Net"; }
		}

		#endregion

		#region ITabSetup implementation

		/// <summary>
		/// Setups the targeted tab using the specified context.
		/// </summary>
		/// <param name="context">The context which should be used.</param>
		public void Setup(ITabSetupContext context)
		{
			context.PersistMessages<Log4NetMessage>();
		}

		#endregion

		#region IKey implementation

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key. Only valid JavaScript identifiers should be used for future compatibility.</value>
		public string Key
		{
			get { return "glimpse_log4net"; }
		}

		#endregion

		#region ITabLayout implementation

		public object GetLayout()
		{
			return layout;
		}

		#endregion
	}
}