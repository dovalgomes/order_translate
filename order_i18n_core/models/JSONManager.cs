using System;
using System.Text;
using System.IO;

using order_i18n_core.interfaces;
using System.Collections.Generic;

namespace order_i18n_core.models
{
    public class JSONManager : IJSONManager
    {
        private string Source { get; set; }
        private StringBuilder FileContent { get; set; }

        public JSONManager(string source)
        {
            if (File.Exists(source))
            {
                this.Source = source;
            }
            else
            {
                throw new Exception("Select a file to process!");
            }
        }


        public string BuildOrder()
        {
            var result = File.ReadAllText(this.Source);
            var auxResult = result.Split(',');

            result = "{";
            List<string> listString = new List<string>();
            foreach (var line in auxResult)
            {
                var auxLine = line.Replace("}", "").Replace("{", "");
                listString.Add(auxLine);
            }

            listString.Sort();

            for (int i = 0; i < listString.Count; i++)
            {
                result = i != listString.Count - 1 ?
                    result += listString[i] + "," :
                    result += listString[i] + "\n }";
            }

            return result;
        }

        public void ClearObject()
        {
            this.Source = null;
            this.FileContent = null;
        }

        public void GenerateFile(string destination)
        {
            File.WriteAllText(destination, this.BuildOrder());
        }
    }
}