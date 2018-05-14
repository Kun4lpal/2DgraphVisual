/*
////////////////////////////////////////////////////////////////////////
// Program.cs                                                         //
// ver 3.0                                                            //
// Language:     C++ 11                                               //
// Application:  Summer Project                                       //
// Test basic functions in AdjacencyMatrix and GraphSimulation classes//
// Summer project 2017                                                //
// Author:       Kunal Paliwal, Syracuse University, Summer Project   //
//                (315) 876-8002, kupaliwa@syr.edu                    //
//////////////////////////////////////////////////////////////////////// 
 */

using System;
using System.Diagnostics;

namespace AdjacencyMatrix
{

    class Program
    {
        static void Main(string[] args)
        {
            //create a graph
            GraphSimulation gs = new GraphSimulation();
            for (int i = 0; i <= 6; i++)
                gs.AddParticle(i);

            gs.AddEdge(0, 1);
            gs.AddEdge(1, 2);
            gs.AddEdge(2, 3);
            gs.AddEdge(3, 4);
            gs.AddEdge(4, 5);
            gs.AddEdge(4, 6);
            gs.AddEdge(5, 1);
            gs.AddEdge(5, 2);

            
            //using thread to generate a realtime reflective datasheet
            const double dt = 1.0 / 60.0d;
            Stopwatch sw = Stopwatch.StartNew();
            long start = 0;
            long end = 0;
            int j = 600;
            while (j != 0)
            {
                gs.Elapse();
                Console.WriteLine(gs);
                end = sw.ElapsedMilliseconds;
                if (dt*1000 > end - start)
                {
                    System.Threading.Thread.Sleep(Convert.ToInt32(dt*1000 - (end - start)));
                    Console.Clear();
                    end = sw.ElapsedMilliseconds;
                }
                start = end;
                j--;
            }
        }
    }
}
