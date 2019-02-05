using PluginContracts;

namespace SecondPlugin
{
	public class SecondPlugin : IPlugin
	{
        #region IPlugin Members

        public string Name => "Поиск по клубам";

        public void Do()
		{
            PluginWindow plugin = new PluginWindow();
            plugin.ShowDialog();
		}

		#endregion
	}
}
