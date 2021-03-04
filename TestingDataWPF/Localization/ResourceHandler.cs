using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingDataWPF.Localization
{
    public static class ResourceHandler
    {
        public static string GetResource(string name)
        {
            return Properties.strings.ResourceManager.GetString(name);
        }
    }
}
