using System.Globalization;
using System.IO;

namespace KerbalKustomzLtd
{
    public class Setting
    {
        public const string Id = "KKL";
        public const string Version = "v.1.1";
        
        private static readonly string Path = KSPUtil.ApplicationRootPath + "GameData/KerbalKustomzLtd/PluginData";
        private static readonly string Defaults = Path + "/settings.def";
        private static readonly string Filename = Path + "/settings.cfg";
        private ConfigNode _config, _node;
        private bool _modified;

        public Setting Load()
        {
            if (!File.Exists(Filename))
            {
                File.Copy(Defaults, Filename);
            }
            
            _config = ConfigNode.Load(Filename);
            _node = _config.GetNode(Id);
            
            return this;
        }

        public Setting Save()
        {
            if (_modified)
            {
                _config.Save(Filename);
            }
            
            return this;
        }

        public string GetValue(string name, string value = "")
        {
            return _node.HasValue(name) ? _node.GetValue(name) : value;
        } 
     
        public Setting SetValue(string name, int value)
        {
            return SetValue(name, value.ToString());
        }
        
        public Setting SetValue(string name, double value)
        {
            return SetValue(name, value.ToString(CultureInfo.CurrentCulture));
        }

        public Setting SetValue(string name, string value)
        {
            if (_node.GetValue(name) == value) return this;
            _node.SetValue(name, value);
            _modified = true;
            return this;
        }
    }
}
