using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace GravityTutorial
{
    public class web
    {
        string sURL;
        WebRequest wrGETURL;
        WebProxy myProxy = new WebProxy("myproxy",80);
        Stream objStream;

        public web()
        {
            
        }

        public void send_request(string pseudo, int score, int lvl)
        {
            sURL = "http://nicolasfley.fast-page.org/MrFreeze/jeu.php?" + "paq=ABC&paqChiffre=BCD&pseudo="+pseudo+"&score="+score+"&niveau="+lvl;
            wrGETURL = WebRequest.Create(sURL);
            myProxy.BypassProxyOnLocal = true;
            wrGETURL.Proxy = WebProxy.GetDefaultProxy();
            objStream = wrGETURL.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            //while (sLine != null)
            //{
            //    i++;
            //    sLine = objReader.ReadLine();
            //    if (sLine != null)
            //        Console.WriteLine("{0}:{1}", i, sLine);
            ////}
            //Console.ReadLine();
        }
    }
}
