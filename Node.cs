using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CourseWork
{
    class Node
    {
        int[] coordinates;
        int index;
        float g;
        float h;
        float f;
        string connect;
        public Node()
        {
            
        }
        public Node(String coordinates)
        {
            this.coordinates = coordinates.Split(',').Select(int.Parse).ToArray();
            
        }
        public Node(String coordinates, string connect, int index)
        {
            this.coordinates = coordinates.Split(',').Select(int.Parse).ToArray();
            this.connect = connect;
            this.index = index;
        }
        public Node(int[] coordinates, string connect, int index)
        {
            this.coordinates = coordinates;
            this.connect = connect;
            this.index = index;
        }
        public float getH(Node goal)
        {
            float dx = Math.Abs(this.Coordinates[0] - goal.coordinates[0]);
            float dy = Math.Abs(this.Coordinates[1] - goal.coordinates[1]);
            float d = 1;
            return(d * (float)Math.Sqrt(dx * dx + dy * dy));
        }
        public float getG(Node adj)
        {
            float dx = Math.Abs(this.Coordinates[0] - adj.coordinates[0]);
            float dy = Math.Abs(this.Coordinates[1] - adj.coordinates[1]);
            
            return((float)Math.Sqrt(dx * dx + dy * dy));
        }        
        public int[] CheckConnection()
        {
            int[] connected = connect.Split(',').Select(int.Parse).ToArray();
            
            return(connected);
        }
        public int Index
        {
            get{return index;} 
            set{index = value;}
        }
        public float G
        {
            get{return g;} 
            set{g = value;}
        }
        public float H
        {
            get{return h;} 
            set{h = value;}
        }
        public float F
        {
            get{return f;} 
            set{f = value;}
        }
        public float Path{get;set;}
        public int[] Coordinates
        {
            get{return coordinates;}
            set{coordinates = value;}
        }
        public Node Parent{get;set;}
    }
}
