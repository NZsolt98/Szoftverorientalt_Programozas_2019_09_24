using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Labda
{
    class Program
    {
        //ezt a programot átalakítani, úgy hogy csak akkor álljon meg a program ha 10* vagy annál többször ütköztek összesen a labdák
        //amíg nem érik el a 10 ütközést addig pattanjanak vissza egymásról
        static void Main(string[] args)
        {
            labda l1 = new labda(10, 5, +1, +1);
            labda l2 = new labda(30, 2, -1, +1);
            labda l3 = new labda(70, 10, -1, -1);

            Thread t1 = new Thread(l1.mozog);
            Thread t2 = new Thread(l2.mozog);
            Thread t3 = new Thread(l3.mozog);

            t1.Start();
            t2.Start();
            t3.Start();

            bool stop = false;
            while (!stop)
            {
                if (l1.currposx == l2.currposx && l1.currposy == l2.currposy ||
                    l1.currposx == l3.currposx && l1.currposy == l3.currposy ||
                    l3.currposx == l2.currposx && l3.currposy == l2.currposy)
                {
                    t1.Abort();
                    t2.Abort();
                    t3.Abort();
                }
            }


        }
    }

    class labda
    {
        public int currposx, currposy, intx, inty;

        public labda(int currposx, int currposy, int intx, int inty)
        {
            this.currposx = currposx;
            this.currposy = currposy;
            this.intx = intx;
            this.inty = inty;
        }

        public void mozog()
        {
            while (true)
            {
                lock (typeof(Program))
                {
                    Console.SetCursorPosition(currposx, currposy);
                    Console.Write(" ");
                }

                if (currposx < 80 && currposx > 0 && currposy < 25 && currposy > 0)
                {
                    currposx += intx;
                    currposy += inty;
                }
                else
                {
                    if (currposx == 0 || currposx == 80)
                    {
                        intx *= -1;
                        currposx += intx;
                    }

                    if (currposy == 0 || currposy == 25)
                    {
                        inty *= -1;
                        currposy += inty;
                    }
                }

                lock (typeof(Program))
                {
                    Console.SetCursorPosition(currposx, currposy);
                    Console.Write("O");
                }
                Thread.Sleep(100);
            }
        }

    }
}