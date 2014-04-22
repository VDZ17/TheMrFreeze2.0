using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GravityTutorial.LevelGeneration
{
    public class loadfile
    {
        public loadfile()
        {
            
        }

        public int[,] read(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string info = streamReader.ReadLine();
                string[] info_size = info.Split('*');
                int width = Int32.Parse(info_size[0]);
                int height = Int32.Parse(info_size[1]);
                int[,] data = new int[height,width];
                int y = 0;

                do
                {
                    string line = streamReader.ReadLine();
                    string[] numbers = line.Split(',');

                    for (int i = 0; i < numbers.Length; i++)
                    {
                        data[y,i] = Int32.Parse(numbers[i]);
                    }
                    y++;
                } while (!streamReader.EndOfStream);
                return data;
            }


        }
    }
}
