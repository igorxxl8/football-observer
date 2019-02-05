using PluginContracts;

namespace ThirdPlugin
{
    public class ThirdPlugin : IPlugin
    {
        #region IPlugin Members

        public string Name => "Поиск по лигам";
        public void Do()
        {
            PluginWindow plugin = new PluginWindow();
            plugin.ShowDialog();
        }
            
        #endregion 
    }
}
