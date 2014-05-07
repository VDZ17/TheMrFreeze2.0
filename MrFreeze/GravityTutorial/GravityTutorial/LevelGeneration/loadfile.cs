using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GravityTutorial
{
    public static class loadfile
    {

        public static int[,] read(string path)
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

        public static void Save(double score, string pseudo)
        {
            System.Collections.Generic.Dictionary<string, double> dicotop5 = new Dictionary<string, double> { };
            string ligne;
            dicotop5.Add(pseudo, score);
            StreamReader fluxInfos2;
            using (fluxInfos2 = new StreamReader(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName) + "\\GravityTutorialContent\\hightscore\\hightscore" + Game1.Level.lvl + ".txt"))
            {
                ligne = fluxInfos2.ReadLine();
                while (ligne != null)
                {
                    string[] done = ligne.Split('/');
                    double data = double.Parse(done[1]);
                    try
                    {
                        dicotop5.Add(done[0], data);
                    }
                    catch (Exception)
                    {
                        if (done[0] == pseudo)
                        {
                            if (data > score)
                            {
                                dicotop5[pseudo] = data;
                            }
                        }
                    }
                    ligne = fluxInfos2.ReadLine();
                }
            }

            var DicoTrie = (from entry in dicotop5 orderby entry.Value descending select entry);
            StreamWriter fluxInfos3;
            using (fluxInfos3 = new StreamWriter(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName) + "\\GravityTutorialContent\\hightscore\\hightscore" + Game1.Level.lvl + ".txt"))
            {

            }
            int i = 0;
            StreamWriter fluxInfos4;
            using (fluxInfos4 = new StreamWriter(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName) + "\\GravityTutorialContent\\hightscore\\hightscore" + Game1.Level.lvl + ".txt"))
            {

                foreach (KeyValuePair<string, double> pair in DicoTrie)
                {

                    if (i < 5)
                    {
                        i++;
                        {
                            fluxInfos4.WriteLine(pair.Key + '/' + pair.Value);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            /*
            System.Collections.Generic.List<Tuple<string, double>> dicotop5 = new List<Tuple<string, double>> { };
            dicotop5.Add(new Tuple<string, double>(pseudo, score));
            StreamReader fluxInfos2;
            string ligne;
            using (fluxInfos2 = new StreamReader(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName) + "\\GravityTutorialContent\\hightscore\\hightscore" + Game1.Level.lvl + ".txt"))
            {
                ligne = fluxInfos2.ReadLine();
                while (ligne != null)
                {
                    string[] done = ligne.Split('/');
                    double point = double.Parse(done[1]);
                    dicotop5.Add(new Tuple<string, double>(done[0], point));
                    ligne = fluxInfos2.ReadLine();
                }
            }
            var DicoTrie = (from entry in dicotop5 orderby entry.Item2 descending select entry);
            StreamWriter fluxInfos3;
            using (fluxInfos3 = new StreamWriter(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName) + "\\GravityTutorialContent\\hightscore\\hightscore" + Game1.Level.lvl + ".txt"))
            {

            }

            int i = 0;
            foreach (System.Tuple<string, double> pair in DicoTrie)
            {

                if (i < 5)
                {
                    i++;
                    StreamWriter fluxInfos;
                    using (fluxInfos = new StreamWriter(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName) + "\\GravityTutorialContent\\hightscore\\hightscore" + Game1.Level.lvl + ".txt"))
                    {
                        fluxInfos.WriteLine(pair.Item1 + '/' + pair.Item2);
                    }
                }
                else
                {
                    break;
                }
            }*/
        }
        public static List<string> read_score(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                List<string> data = new List<string>();
                int y = 0;

                do
                {
                    string line = streamReader.ReadLine();
                    data.Add(line);
                    y++;
                } while (!streamReader.EndOfStream);

                return data;
            }
        }
    }
}
