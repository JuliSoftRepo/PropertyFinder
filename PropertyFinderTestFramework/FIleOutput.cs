using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyFinderTestFramework
{
    public static class FIleOutput
    {
        public static void SaveToFile(IEnumerable<object> data, string path)
        {
            if (data == null)
            {
                throw new Exception("No valid data to save - null reference");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("Save file path is incorrect");
            }

            var textBuilder = new StringBuilder();
            foreach (var obj in data)
            {
                var props = obj.GetType().GetProperties();
                foreach (var prop in props)
                {
                    textBuilder.Append(prop.Name + " : " + prop.GetValue(obj) + ", ");
                }                        
                textBuilder.Append(Environment.NewLine);
            }
            File.WriteAllText(path, textBuilder.ToString());
        }
    }
}
