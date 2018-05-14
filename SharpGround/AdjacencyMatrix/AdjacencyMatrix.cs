/*
//////////////////////////////////////////////////////////////////////
// AdjacencyMatrix.cs                                               //
// ver 3.0                                                          //
// Language:     C++ 11                                             //
// Application:  Summer Project                                     //
// A base class depicting graphes and a derived class               //
// for gravity particle simulation                                  //
// Summer project 2017                                              //
// Author:       Kunal Paliwal, Syracuse University, Summer Project //
//                (315) 876-8002, kupaliwa@syr.edu                  //
//////////////////////////////////////////////////////////////////////
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdjacencyMatrix
{
    using Vertex = Int32;
    using Vertices = List<Int32>;
    using VerticesSet = List<List<Int32>>;
    using Edge = Tuple<Int32, Int32>;
    using Edges = List<Tuple<Int32, Int32>>;
    // contain a vector class for simulation
    using HandMade;

    public class AdjacencyMatrix
    {

        public Vertices V { get; private set; } = new Vertices();
        public Edges E { get; private set; } = new Edges();

        public int Count { get { return V.Count; } }

        public AdjacencyMatrix() { }

        public AdjacencyMatrix(int number)
        {
            for (var i = number; i >= 0; i++)
                AddVertex(i - 1);
        }

        public bool HasVertex(Vertex v)
        {
            foreach (var each in V)
                if (each == v) return true;
            return false;
        }

        public bool HasEdge(Vertex from, Vertex to)
        {
            foreach (var each in E)
                if (each.Item1 == from && each.Item2 == to) return true;
            return false;

        }


        public void AddVertex(Vertex v) { if (!HasVertex(v)) V.Add(v); }

        public void AddEdge(Vertex from, Vertex to) { AddEdge(new Edge(from, to)); }

        private void AddEdge(Edge e)
        {
            if (!HasEdge(e.Item1, e.Item2)) E.Add(e);
            AddVertex(e.Item1); AddVertex(e.Item2);
        }

        public bool RemoveVertex(Vertex v)
        {
            if (!HasVertex(v)) return false;
            V.Remove(v);
            E.RemoveAll((Edge e) => { return e.Item1 == v || e.Item2 == v; });
            return true;
        }

        public bool RemoveEdge(Vertex from, Vertex to) { return RemoveEdge(new Edge(from, to)); }

        private bool RemoveEdge(Edge e) { return E.Remove(e); }

        public List<Vertex> TopologicalSort()
        {
            // Empty list that will contain the sorted elements
            var L = new List<Vertex>();

            // Set of all nodes with no incoming edges
            var S = new HashSet<Vertex>(V.Where(n => E.All(e => e.Item2.Equals(n) == false)));

            // while S is non-empty do
            while (S.Any())
            {

                //  remove a node n from S
                var n = S.First();
                S.Remove(n);

                // add n to tail of L
                L.Add(n);

                // for each node m with an edge e from n to m do
                foreach (var e in E.Where(e => e.Item1.Equals(n)).ToList())
                {
                    var m = e.Item2;

                    // remove edge e from the graph
                    E.Remove(e);

                    // if m has no other incoming edges then
                    if (E.All(me => me.Item2.Equals(m) == false))
                    {
                        // insert m into S
                        S.Add(m);
                    }
                }
            }

            // if graph has edges then
            if (E.Any())
            {
                // return error (graph has at least one cycle)
                return null;
            }
            else
            {
                // return L (a topologically sorted order)
                return L;
            }
        }
        // return a list of vertices that has an edge from a certain vertex
        public Vertices From(Vertex v)
        {
            return
                (
                from item in E
                where item.Item1 == v
                select item.Item2
                ).ToList();
        }

        // return a list of vertices that has an edge towards a certain vertex
        public Vertices To(Vertex v)
        {
            return
                (
                from item in E
                where item.Item2 == v
                select item.Item2
                ).ToList();
        }

        // return a list of vertices that has an edge reference with a certain vertex
        public Vertices With(Vertex v)
        {
            Vertices temp = new Vertices();
            foreach (var e in E)
            {
                if (e.Item1 == v) temp.Add(e.Item2);
                if (e.Item2 == v) temp.Add(e.Item1);
            }
            return temp;
        }
        
        // for maintance purposes
        public override String ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Vertices: ");
            foreach (var item in V)
                str.Append(item.ToString() + "| ");
            str.AppendLine();
            str.Append("Edges: ");
            foreach (var item in E)
                str.Append(item.Item1.ToString() + "->" + item.Item2.ToString() + "| ");
            str.AppendLine();
            return str.ToString();
        }

    }

    public class GraphSimulation : AdjacencyMatrix
    {
        public Dictionary<Vertex, MassParticle> Particles = new Dictionary<Vertex, MassParticle>();
        private Random RNG = new Random();

        //spring constant
        public const double K = 500d;
        //resting average particle distance 
        public const double L = 30d;
        //particle mass
        public const double m = 1d;
        //friction constant
        public double Mu = 0.1 * L * System.Math.Sqrt(3d * K * m);
        //gravity constant derived from equilibrium
        public const double G = K * L * L * L;
        //time differential
        //probably nor a good idea going below 1/24
        public const double dt = 1d / 120d;

        public MassParticle this[int i]
        {
            get { return Particles[i]; }
        }

        //add particle and assign a random position if not exist
        public void AddParticle(Vertex v)
        {
            AddVertex(v);
            if (HasParticle(v)) return;
            var newp = new MassParticle();
            newp.Mass = m;
            newp.Velocity = Vector.Zero;
            newp.Position.X = (RNG.NextDouble() * 2 - 1) * L * 10;
            newp.Position.Y = (RNG.NextDouble() * 2 - 1) * L * 10;
            Particles[v] = newp;
        }

        private bool HasParticle(Vertex v)
        {
            return Particles.ContainsKey(v);
        }

        //elapse each particle with time
        public void Elapse()
        {
            Vector[] forces = new Vector[V.Count];
            foreach (var i in V)
                forces[i] = TotalForce(i);
            foreach (var i in V)
                Particles[i] = Particles[i].Elapse(forces[i], dt);
        }

        //calculate the total force for a vertex
        private Vector TotalForce(Vertex v)
        {
            var tf = Vector.Zero;
            foreach (var k in V)
            {
                if (k == v) continue;
                tf += InverseSquareForce(k, v);
            }
            foreach (var e in With(v))
                tf += LinearForce(e, v);
            tf += Friction(v);
            return tf;
        }

        //calculate gravity
        //F=G*M*m/r^2
        private Vector InverseSquareForce(Vertex v1, Vertex v2)
        {
            var p1 = Particles[v1];
            var p2 = Particles[v2];
            var r = p2.Position - p1.Position;
            return G * r * Particles[v1].Mass * Particles[v2].Mass / r.Length / r.Length / r.Length;
        }

        //calculate spring force
        //F=k*r*M*m
        //added mass for easier energy estimation
        private Vector LinearForce(Vertex v1, Vertex v2)
        {
            var p1 = Particles[v1];
            var p2 = Particles[v2];
            var r = p2.Position - p1.Position;
            return K * r * Particles[v1].Mass * Particles[v2].Mass * -1d;
        }

        //calculate friction
        //f = mu*v*m^2
        private Vector Friction(Vertex v)
        {
            return Particles[v].Velocity * Mu * Particles[v].Mass * Particles[v].Mass * -1d;
        }


        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (var k in V)
            {
                str.AppendLine(k.ToString() + ": ");
                str.Append(Particles[k]);
            }
            return str.ToString();
        }
    }

    //mass particle struct
    public struct MassParticle
    {
        public Vector Velocity;
        public Vector Position;
        public double Mass;

        public static MassParticle UnitMass
        {
            get
            {
                var u = new MassParticle();
                u.Velocity = Vector.Zero;
                u.Position = Vector.Zero;
                u.Mass = 1d;
                return u;
            }
        }

        //for a small dt, the newtonian method is stable
        //providing that forces and acceleration are both insignificant
        public MassParticle Elapse(Vector force, double dt)
        {
            var acc = force / Mass;
            var dp = Velocity * dt + acc * dt * dt * 0.5;
            var dv = acc * dt;
            var newm = new MassParticle();
            newm.Velocity = Velocity + dv;
            newm.Position = Position + dp;
            newm.Mass = Mass;
            return newm;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("Mass: " + Mass.ToString());
            str.AppendLine("Velocity: " + Velocity.ToString());
            str.AppendLine("Position: " + Position.ToString());
            return str.ToString();
        }
    }

}
