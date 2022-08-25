using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CourseWork
{
class Program
    {
        public List<Node> nodes = new List<Node>();
        public Program(string name)
        {
            string path = "";
            if(name.Contains("caveroute"))
            {
                name = name.Replace("caveroute ", "");
                path = System.IO.File.ReadAllText(System.AppContext.BaseDirectory + name + ".cav");
            }
            

            List<string> caves = path.Split(new char[] { ',' }).ToList();

            int totalNodes = int.Parse(caves[0]);

            caves.Remove(caves[0]);

            List<string> coordinates = new List<string>();
            List<string> connections = new List<string>();
            
        

            for(int i = 0; i < (totalNodes * 2); i = i + 2)
            {
                coordinates.Add(caves[i] + "," + caves[i+1]);
            }

            int count = 0;
            for(int i = (totalNodes * 2); i < caves.Count(); i++)
            {
                count++;
                if(count >= totalNodes)
                {
                    connections.Add(caves[i] + '.');
                    count = 0;
                }
                else
                {
                    connections.Add(caves[i] + ',');
                }
                
                
            }
          
            var stringSplit = String.Join("",connections.ToArray());

            Char splitter = '.';
            string[] connect = stringSplit.Split(splitter);
            
            int index = 0;
            foreach(string str in coordinates)
            {
                
                Node node = new Node(str, connect[index].ToString(), index);
                nodes.Add(node);
                index++;
            }


            matrix m = new matrix(totalNodes, name);

            count = 0;
            foreach(Node value in nodes)
            {
                
                int[] check = nodes[count].CheckConnection();
                for(int i = 0; i < totalNodes; i++)
                {
                if(check[i] == 1)
                m.insert(i, count);
                }
                count++;
            }

            m.Pathfinding(nodes);
        }

        public static string name;
        static void Main(string[] args)
        {
            name = @"\" + Console.ReadLine();
            Program p = new Program(name);
        }
    }

    class matrix
    {
        int totalNodes;
        int[,] mat;
        string name;
        public matrix(int totalNodes, string name)
        {
            this.totalNodes = totalNodes;
            mat = new int[totalNodes, totalNodes];
            this.name = name;
        }
        public void insert(int from, int to)
        {
            mat[from, to] = 1;
        }

        public void display()
        {
            Console.Write("   ");
            for (int i = 0; i < totalNodes; i++)
            {
                Console.Write((i + 1) + " ");
            }
            Console.WriteLine("\n");
            for (int i = 0; i < totalNodes; i++)
            {
                Console.Write((i + 1) + "  ");
                for (int j = 0; j < totalNodes; j++)
                {
                    Console.Write(mat[i, j] + " ");

                }
                Console.WriteLine();
            }
        }

        public void Pathfinding(List<Node> nodeList)
        {
            int goalNode = nodeList.Count() - 1;
            int startNode = 0;
            Node current = nodeList[0];
            List<int> open = new List<int>();
            List<int> closed = new List<int>();

            List<int> unvisited = new List<int>();

            for(int i = 0; i < totalNodes; i++)
            {
                unvisited.Add(i);
            }

            unvisited.Remove(unvisited[startNode]);
            open.Add(0);
            nodeList[startNode].G = 0;
            nodeList[startNode].F = nodeList[startNode].G + nodeList[startNode].getH(nodeList[goalNode]);

            while(current != nodeList[goalNode])
            {
                for(int i = 0; i < totalNodes; i++)
                {
                    if(mat[current.Index, i] > 0) //If the node shares an edge
                    {
                        if(!closed.Contains(i) && !open.Contains(i)) //If the node is not in open & closed list
                        {
                            unvisited.Remove(i);
                            open.Add(i);
                            nodeList[i].Parent = current;
                            closed.Add(i);

                            float g, h, f;
                            g = (nodeList[i].Parent.G + 1);
                            h = 0;
                            f = g + h;

                            if(nodeList[i].G == 0 || g < nodeList[i].G)
                            {
                                nodeList[i].F = f;
                                nodeList[i].G = g;
                                nodeList[i].H = h;
                                nodeList[i].Path = current.getG(nodeList[i]);
                                nodeList[i].Parent = current;
                            }
                        }
                    }
                }

                float inf = 90000;
                int newCurrent = 0;
                foreach(int i in open)//Foreach to set current to lowest f value in open
                {
                    if(nodeList[i].F <= inf)
                    {
                        inf = nodeList[i].F;
                        newCurrent = i;
                    }
                }
                open.Remove(newCurrent);
                current = nodeList[newCurrent];

                if(!open.Any())//If open is empty, return 0
                {
                    break;
                }
            }
            string str;
            if(current == nodeList[goalNode])
            {
                List<float> length = new List<float>();
                List<string> shortest = new List<string>();
                Node v;
                v = nodeList[goalNode];
            
                do
                {
                    shortest.Add((v.Index + 1).ToString());
                    length.Add(v.Path);
                    v = v.Parent;
                }while(v != null);

                shortest.Reverse();
                float total = length.Sum();
                str = string.Join(", ", shortest);
                str = str + "\n" + "Length: " + total.ToString();
                
                
            }
            else
            {
                 str = "0";
            }
            Console.WriteLine(str);
            File.WriteAllText(System.AppContext.BaseDirectory + name + ".csn", str);

        }
    }
}
