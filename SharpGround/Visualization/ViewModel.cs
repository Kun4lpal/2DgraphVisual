/*
//////////////////////////////////////////////////////////////////////////////
// ViewModel.cs                                                             //
// ver 3.0                                                                  //
// Language:     C++ 11                                                     //
// Application:  Summer Project                                             //
// Provide view model for view to bind with                                 //
// Author:       Kunal Paliwal, Syracuse University, Summer Project         //
//                (315) 876-8002, kupaliwa@syr.edu                          //
//////////////////////////////////////////////////////////////////////////////
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Threading;
using System.Xml;
using MVVM;

namespace Visualization
{
    using AdjacencyMatrix;

    //view model for the colored elipses in the simulation
    public class MassParticleViewModel : ObservableObject
    {

        public MassParticleViewModel(int index, Color color, String tooltip, double Height, double Width)
        {
            Index = index;
            Diameter = 10;
            SolidColorBrush scb = new SolidColorBrush();
            scb.Color = color;
            Color = scb;
            ToolTip = tooltip;
            dY = Height / 2;
            dX = Width / 2;
            X = dX;
            Y = dY;
        }

        public MassParticle MassParticle { get; private set; }

        private double dY;
        private double dX;

        public void Refresh(MassParticle mp)
        {
            X = mp.Position.X + dX - Radius;
            Y = mp.Position.Y + dY - Radius;
        }

        private double d;
        public double Diameter
        {
            get { return d; }
            set
            {
                if (d == value) return;
                d = value;
                RaisePropertyChanged("Diameter");
            }
        }

        public double Radius
        { get { return Diameter / 2; } }

        private double x;
        public double X
        {
            get { return x; }
            set
            {
                if (x == value) return;
                x = value;
                RaisePropertyChanged("X");
            }
        }

        private String tooltip;
        public String ToolTip
        {
            get { return tooltip; }
            set
            {
                if (tooltip == value) return;
                tooltip = value;
                RaisePropertyChanged("ToolTip");
            }
        }

        private double y;
        public double Y
        {
            get { return y; }
            set
            {
                if (y == value) return;
                y = value;
                RaisePropertyChanged("Y");
            }
        }

        private SolidColorBrush scb;
        public SolidColorBrush Color
        {
            get { return scb; }
            set
            {
                if (scb == value) return;
                scb = value;
                RaisePropertyChanged("Color");
            }
        }

        public int Index
        { get; private set; }

    }

    //view model for line segment in the simulaion
    public class EdgeViewModel : ObservableObject
    {
        public int From { get; private set; }
        public int To { get; private set; }

        private double dY;
        private double dX;

        public EdgeViewModel(int from, int to, Color color, double Height, double Width)
        {
            SolidColorBrush scb = new SolidColorBrush();
            scb.Color = color;
            Color = scb;
            From = from;
            To = to;
            dX = Width / 2;
            dY = Height / 2;
            X1 = X2 = dX;
            Y1 = Y2 = dY;
        }

        public void Refresh(MassParticle from, MassParticle to)
        {
            X1 = from.Position.X + dX;
            Y1 = from.Position.Y + dY;
            X2 = to.Position.X + dX;
            Y2 = to.Position.Y + dY;
        }

        private double x1;
        public double X1
        {
            get { return x1; }
            set
            {
                if (value == x1) return;
                x1 = value;
                RaisePropertyChanged("X1");
            }
        }

        private double x2;
        public double X2
        {
            get { return x2; }
            set
            {
                if (value == x2) return;
                x2 = value;
                RaisePropertyChanged("X2");
            }
        }

        private double y1;
        public double Y1
        {
            get { return y1; }
            set
            {
                if (value == y1) return;
                y1 = value;
                RaisePropertyChanged("Y1");
            }
        }

        private double y2;
        public double Y2
        {
            get { return y2; }
            set
            {
                if (value == y2) return;
                y2 = value;
                RaisePropertyChanged("Y2");
            }
        }

        private SolidColorBrush scb;
        public SolidColorBrush Color
        {
            get { return scb; }
            set
            {
                if (scb == value) return;
                scb = value;
                RaisePropertyChanged("Color");
            }
        }

    }

    //view model for data context
    public class ViewModel : ObservableObject
    {
        public ObservableCollection<MassParticleViewModel> Vertices { get; private set; } = new ObservableCollection<MassParticleViewModel>();

        public ObservableCollection<EdgeViewModel> Edges { get; private set; } = new ObservableCollection<EdgeViewModel>();

        public GraphSimulation GS { get; set; }

        private Thread t;
        private ManualResetEvent mrse = new ManualResetEvent(false);

        public ViewModel()
        {
            GS = new GraphSimulation();
        }

        public void Refresh()
        {
            //for (int i = 0; i < Vertices.Count; i++)
            //    Vertices[i].Refresh(GS[i]);
            //for (int j = 0; j < Edges.Count; j++)
            //    Edges[j].Refresh(GS[GS.E[j].Item1], GS[GS.E[j].Item2]);
            foreach (var v in Vertices)
                v.Refresh(GS[v.Index]);
            foreach (var e in Edges)
                e.Refresh(GS[e.From], GS[e.To]);
        }

        //test simulation with first a very simple model
        //and then a more complex model
        private void TestSetup()
        {
            //GS.AddParticle(0);
            //GS.AddParticle(1);
            //GS.AddEdge(0, 1);

            for (int i = 0; i <= 6; i++)
                GS.AddParticle(i);

            GS.AddEdge(0, 1);
            GS.AddEdge(1, 2);
            GS.AddEdge(1, 5);
            GS.AddEdge(2, 3);
            GS.AddEdge(2, 5);
            GS.AddEdge(3, 4);
            GS.AddEdge(4, 5);
            GS.AddEdge(4, 6);

            foreach (var v in GS.V)
                Vertices.Add(new MassParticleViewModel(v, Color.FromRgb(125, 125, 125), "", 450, 800));
            foreach (var e in GS.E)
                Edges.Add(new EdgeViewModel(e.Item1, e.Item2, Color.FromRgb(125, 125, 125), 450, 800));
        }

#region XMLParsing

        //retrive a list of int from a string like this:
        //1|2|3|4|5|
        private List<int> Retrive(String str)
        {
            var results = new List<int>();
            foreach (var seg in str.Split('|'))
            {
                int i;
                if (int.TryParse(seg, out i))
                    results.Add(i);
            }

            return results;
        }

        //return if two file are of same package
        //they have same file name but different extension
        private bool FromSamePackage(String name1, String name2)
        {
            var one = name1.Split('.')[0];
            var two = name2.Split('.')[0];
            return one == two;
        }

        //parse from a xml file generated by a different project
        private void FromXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("database.xml");

            XmlNode root = doc.DocumentElement;

            XmlNode treverse = root.FirstChild;
            Dictionary<int, String> NameD = new Dictionary<int, String>();
            Dictionary<int, String> PathD = new Dictionary<int, String>();
            while (treverse != null)
            {
                int key = -1;
                String v = "";
                String d = "";
                foreach (XmlNode node in treverse.ChildNodes)
                    switch (node.Name)
                    {
                        case "Key":
                            key = int.Parse(node.InnerText);
                            Console.WriteLine("Detect vertex: " + key);
                            break;
                        case "Value":
                            v = node.InnerText;
                            Console.WriteLine("Detect value: " + v);
                            break;
                        case "Description":
                            d = node.InnerText;
                            Console.WriteLine("Detect description: " + d);
                            break;
                    }
                GS.AddParticle(key);
                NameD.Add(key, v.Substring(1));
                PathD.Add(key, d.Substring(1));
                foreach (XmlNode collection in treverse.ChildNodes)
                    switch (collection.Name)
                    {
                        case "ChildCollection":
                            foreach (int n in Retrive(collection.InnerText))
                            {
                                GS.AddEdge(key, n);
                                Console.WriteLine("Detect edge: " + key + "->" + n);
                            }
                            break;
                        case "ParentCollection":
                            foreach (int n in Retrive(collection.InnerText))
                            {
                                GS.AddEdge(n, key);
                                Console.WriteLine("Detect edge: " + n + "->" + key);
                            }
                            break;
                    }
                treverse = treverse.NextSibling;
            }
            var od = new Dictionary<int, int>();
            for (int i = 0; i < GS.V.Count; i++)
                od.Add(GS.V[i], i);

            foreach (var ve in GS.V)
            {
                Color c = SimpleColor(od[ve]).Color;
                foreach (var item in od)
                    if (FromSamePackage(NameD[ve], NameD[item.Key])) c = SimpleColor(od[item.Value]).Color;
                Vertices.Add(new MassParticleViewModel(ve, c, NameD[ve] + "Path: " + PathD[ve], 450, 800));
            }
            foreach (var ed in GS.E)
                Edges.Add(new EdgeViewModel(ed.Item1, ed.Item2, Color.FromRgb(125, 125, 125), 450, 800));
        }

#endregion

        //give a random color
        private SolidColorBrush SimpleColor(int i)
        {
            Random rng = new Random(i);
            SolidColorBrush scb = new SolidColorBrush();
            scb.Color = Color.FromRgb((byte)rng.Next(255), (byte)rng.Next(255), (byte)rng.Next(255));
            return scb;
        }

        //run the model with a thread
        public void Run()
        {
            //TestSetup();

            FromXML();


            t = new Thread(() =>
            {
                while (true)
                {
                    mrse.WaitOne();
                    GS.Elapse();
                    //Console.WriteLine(GS);
                    Refresh();
                }
            });
            t.Start();
        }

        //pause thread
        public void Pause()
        {
            mrse.Reset();
        }

        //resume thread
        public void Resume()
        {
            mrse.Set();
        }
    }
}
