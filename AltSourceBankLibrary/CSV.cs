using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace AltSourceBankLibrary
{
    public class CSV
    {
        public static List<List<string>> GetCSVData(string fileName)
        {
            List<List<string>> fileData = new List<List<string>>();
            using (TextFieldParser parser = new TextFieldParser(fileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    List<string> line = new List<string>();
                    line = parser.ReadFields().ToList();
                    fileData.Add(line);
                }
            }
            return fileData;
        }
    }
}
