/* 
//////////////////////////////////////////////////////////////////////
// Vector.cs                                                        //
// ver 3.0                                                          //
// Language:     C++ 11                                             //
// Application:  Summer Project                                     //
// Provide basic vector calculus functions                          //
// Summer project 2017                                              //
// Author:       Kunal Paliwal, Syracuse University, Summer Project //
//                (315) 876-8002, kupaliwa@syr.edu                  //
//////////////////////////////////////////////////////////////////////
*/
using System;
using System.Collections.Generic;

namespace HandMade
{

    public struct Vector : IEquatable<Vector>, IEqualityComparer<Vector>
    {
        public double X { get; set; }
        public double Y { get; set; }

        public static Vector Zero
        { get { return new Vector(0, 0); } }

        public static Vector Unit
        { get { return new Vector(1, 1); } }


        public Vector(double X, double Y)
        {
            this.X = X; this.Y = Y;
        }

        public static Vector operator +(Vector one, Vector other)
        {
            return new Vector(one.X + other.X, one.Y + other.Y);
        }

        public static Vector operator -(Vector one, Vector other)
        {
            return new Vector(one.X - other.X, one.Y - other.Y);
        }

        public static Vector operator *(Vector one, double c)
        {
            return new Vector(one.X * c, one.Y * c);
        }

        public static Vector operator *(double c, Vector one)
        {
            return one * c;
        }

        public static Vector operator /(Vector one, double c)
        {
            return new Vector(one.X / c, one.Y / c);
        }

        public static Vector operator /(double c, Vector one)
        {
            return new Vector(c / one.X, c / one.Y);
        }

        public Vector Square { get { return new Vector(X * X, Y * Y); } }

        public Vector InverseSquare { get { return new Vector(1d / X / X, 1d / Y / Y); } }

        public Vector Direction
        {
            get
            {
                if (Length == 0) return Zero;
                return new Vector(X / Length, Y / Length);
            }
        }

        public double Length { get { return System.Math.Sqrt(X * X + Y * Y); } }

        public bool IsZero()
        {
            return X == 0 && Y == 0;
        }

        #region Equals

        public bool Equals(Vector other)
        {
            return X == other.X && Y == other.Y;
        }

        public bool Equals(Vector x, Vector y)
        {
            return x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType() || obj == null) return false;
            var temp = (Vector)obj;
            return Equals(temp);
        }

        public static bool operator ==(Vector one, Vector other)
        {
            return one.Equals(other);
        }

        public static bool operator !=(Vector one, Vector other)
        {
            return !one.Equals(other);
        }

        public int GetHashCode(Vector obj)
        {
            return obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        #endregion

        public override string ToString()
        {
            return "[ " + X.ToString() + ", " + Y.ToString() + " ]";
        }

    }

}
