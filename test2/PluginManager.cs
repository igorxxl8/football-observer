using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;

namespace FootballManager
{
    public class PluginManager
    {
        public List<IPlugin> Plugins = new List<IPlugin>();

        public void ScanPlugins(string directory)
        {
            //перебирвем все файлы dll
            foreach (var file in Directory.EnumerateFiles(directory, "*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    //загружаем ассемблю
                    var ass = Assembly.LoadFile(file);
                    MessageBox.Show(ass.FullName);
                    //перебираем все типы из ассембли
                    foreach (var type in ass.GetTypes())
                    {
                        //проверяем наличие интерфейса IPlugin
                        var i = type.GetInterface("IPlugin");
                        if (i != null)
                        {
                            //создаем экземпляр плагина
                            Plugins.Add(ass.CreateInstance(type.FullName) as IPlugin);
                        }
                    }
                }
                catch {/*is not .NET assembly*/}
            }
        }
    }
}