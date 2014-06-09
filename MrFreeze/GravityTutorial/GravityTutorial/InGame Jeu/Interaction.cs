using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GravityTutorial.InGame_Jeu
{
    public class Interaction
    {


        public static Character NearCharacter(Ennemy3 ennemy, List<Character> Heroes)
        {
            List<float> distance = new List<float> { };
            float max;
            int i, j;
            foreach (Character c in Heroes)
            {
                distance.Add(Vector2.Distance(ennemy.position, c.position));
            }
            max = distance[0];
            i = 0;
            j = -1;
            foreach (float f in distance)
            {
                j++;
                if (max < f)
                {
                    max = f;
                    i = j;
                }
            }
            return Heroes[i];
        }

        public static Character NearCharacter(Ennemy2 ennemy, List<Character> Heroes)
        {
            List<float> distance = new List<float> { };
            float max;
            int i, j;
            foreach (Character c in Heroes)
            {
                distance.Add(Vector2.Distance(ennemy.position, c.position));
            }
            max = distance[0];
            i = 0;
            j = -1;
            foreach (float f in distance)
            {
                j++;
                if (max < f)
                {
                    max = f;
                    i = j;
                }
            }
            return Heroes[i];
        }

        public static Character NearCharacter(Ennemy1 ennemy, List<Character> Heroes)
        {
            List<float> distance = new List<float> { };
            float max;
            int i, j;
            foreach (Character c in Heroes)
            {
                distance.Add(Vector2.Distance(ennemy.position, c.position));
            }
            max = distance[0];
            i = 0;
            j = -1;
            foreach (float f in distance)
            {
                j++;
                if (max > f)
                {
                    max = f;
                    i = j;
                }
            }
            return Heroes[i];
        }




        //public static int NearCharacter(Ennemy3 ennemy, List<Character> Heroes)
        //{
        //    List<float> distance = new List<float> { };
        //    float max;
        //    int i, j;
        //    foreach (Character c in Heroes)
        //    {
        //        distance.Add(Vector2.Distance(ennemy.position, c.position));
        //    }
        //    max = distance[0];
        //    i = 0;
        //    j = -1;
        //    foreach (float f in distance)
        //    {
        //        j++;
        //        if (max < f)
        //        {
        //            max = f;
        //            i = j;
        //        }
        //    }
        //    return i;
        //}

    }
}
