using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace GravityTutorial.InGame_Hors_jeu
{
    class web
    {
        string sURL;
        WebRequest wrGETURL;
        WebProxy myProxy = new WebProxy("myproxy",80);



        public web()
        { 
            
        }

        public void send_request(int pseudo, int score, int lvl)
        {
            sURL = "http://nicolasfley.fast-page.org/MrFreeze/jeu.php?" + "paq=ABC&paqChiffre=BCD&pseudo="+pseudo+"&score="+score+"&niveau="+lvl;
            
        }
    }
}
