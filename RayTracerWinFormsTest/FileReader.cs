using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerWinFormsTest
{
    class FileReader
    {
        

        public FileReader(string filesource)
        {
           string[] text = System.IO.File.ReadAllLines(filesource);
           foreach (string line in text)
           {
                int i = 0;
                string func = "";
                if (line[i].Equals("#"))
                {
                    continue;
                }
                while(!line[i].Equals(" "))
                {
                    func += line[i];
                    i++;
                }
                switch (func)
                {
                    case "size":

                        break;
                }
           }


        }


    }
}
