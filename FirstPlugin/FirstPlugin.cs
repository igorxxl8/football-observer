using PluginContracts;

namespace FirstPlugin
{
	public class FirstPlugin : IPlugin
	{
        #region IPlugin Members

        public string Name => "Поиск по футболистам";

        public void Do()
		{
            PlaginWindow plugin = new PlaginWindow();
            plugin.ShowDialog();
		}

		#endregion
	}
}
