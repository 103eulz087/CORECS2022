using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    class FileObject
    {
        Dictionary<string, string> items = new Dictionary<string, string>();
        string destinationFile = null;
        bool isExist = false;
        public FileObject(String filepath)
        {
            this.destinationFile = filepath;
            isExist = File.Exists(this.destinationFile);
            if (!isExist) File.CreateText(this.destinationFile);
            else
            {
                string[] strlines = System.IO.File.ReadAllLines(this.destinationFile);
                if (strlines.Length != 0)
                {
                    Regex reg_ex = new Regex("^(.*)=");
                    foreach (String str in strlines)
                    {
                        String tmpstr = str, match;
                        if (!String.IsNullOrEmpty(tmpstr))
                        {
                            match = reg_ex.Match(tmpstr).ToString();
                            if (!String.IsNullOrEmpty(match))
                                this.items[match.Replace("=", "")] = tmpstr.Replace(match, "");
                        }
                    }
                }
            }
        }


        public string this[string key]
        {
            get { return items.ContainsKey(key) ? items[key] : null; }
            set { items[key] = value; }
        }


        public void Update()
        {
            using (StreamWriter writer = new StreamWriter(this.destinationFile))
            {

                foreach (String key in items.Keys)
                {
                    string value = items.ContainsKey(key) ? items[key] : "";
                    writer.WriteLine($"{ key }={ value }");
                }
            }
        }
    }
}
