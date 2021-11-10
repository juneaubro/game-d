using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class TestGraphAlgos : MonoBehaviour
{
    public class Graph
    {
        Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();

        Dictionary<char,bool> visited=new Dictionary<char, bool>();
        public void add_vertex(char name, Dictionary<char,int> neighborEdges)
        {
            vertices[name] = neighborEdges;
        }
        //
        // Algorithms
        //
        public bool HasEdge(char fromNode, char toNode)
        {
            return vertices[fromNode].ContainsKey(toNode);//matrix repr. return (a[i,j]==1) 
        }
        //explore starting from v
        public List<char> Dijkstra_Shortest_Path(char fromNode, char toNode)
        {
            var parents = new Dictionary<char, char>(); //if parents['A'] is 'B' the shortest path thru A comes from B
            var distances = new Dictionary<char, int>();
            var nodes_marks = new List<char>();

            List<char> path = null;
            //initialization
            foreach(var vertex in vertices) { 
                distances[vertex.Key] = (vertex.Key == fromNode ? 0: int.MaxValue);
                nodes_marks.Add(vertex.Key);
            }
            //Rest of algo
            while (nodes_marks.Count != 0)
            {
                //fake a priority queue by sorting the list
                nodes_marks.Sort((x, y) => (distances[x] - distances[y]));
                var smallest = nodes_marks[0];
                nodes_marks.Remove(smallest);
                if (smallest == toNode)
                {
                    //we reached the goal; calculate path (by backtracking) and return
                    path = new List<char>();
                    while (parents.ContainsKey(smallest)){
                        path.Add(smallest);
                        smallest = parents[smallest];
                    }
                    break;
                }
                //check for no path
                if (distances[smallest] == int.MaxValue){                    
                    break; //path is null - no path exists to the goal (toNode)
                }
                //otherwise do the relaxation 
                foreach(var np in vertices[smallest])
                {
                    var altDist = distances[smallest] + np.Value;
                    if(altDist < distances[np.Key])
                    {
                        //we found a shorter route; capture it
                        distances[np.Key] = altDist;
                        parents[np.Key] = smallest;
                    }
                }
            }
            return path;
        }
        public bool RecursiveDFS_b(char v)
        {
            //
            // Given vertex v
            // Finds all the vertices that can be visited from it
            // Those vertices will have the visited[v] true.
            // This is "The Reachability Problem"
            // Roughly stated: which vertices can be reached from the vertex v in this graph
            //
            if (visited.Count == vertices.Count)
            {
                return true;
            }
            bool retCode = false;
            if (!vertices.ContainsKey(v))
            {
                return false;
            }
            //
            // v is in vertices
            //
            if (!visited.ContainsKey(v))
            {
                visited[v] = false;
            }
            if (visited[v]==false)
            {
                visited[v] = true;
                retCode = true;
                var neighbors = vertices[v];

                foreach (var u in neighbors)
                {
                    retCode=RecursiveDFS_b(u.Key);
                }
            }



            return retCode;

        }
        //Properties
        public Dictionary<char,bool> Visited { get { return visited; } }
        public List<char> Vertices { get { return vertices.Keys.ToList<char>(); } }
        //BrentonsCleanup
        public bool RecDFS(char v, ref List<char> visited)
        {
            if (vertices.TryGetValue(v, out var edges))
            {
                visited.Add(v);

                foreach (var e in edges)
                {
                    if (!visited.Contains(e.Key) && RecDFS(e.Key, ref visited))
                    {
                        return true;
                    }

                    if (visited.Count == vertices.Count)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    Graph g;
    Graph CreateGraph1()
    {
        // A -> B-7 -> C-8 
        // B -> A-7 -> F-2
        // C -> A-8 -> F-6 -> G-4
        // D -> F-8
        // E -> H-1
        // F -> B-2 -> C-6 -> D-8 -> E-9 -> H-3
        // G -> C-4 -> F-9
        // H -> E-1 -> F-3

        Graph g = new Graph();
        g.add_vertex('A', new Dictionary<char, int>() { { 'B', 7 }, { 'C', 8 } });
        g.add_vertex('B', new Dictionary<char, int>() { { 'A', 7 }, { 'F', 12 } });
        g.add_vertex('C', new Dictionary<char, int>() { { 'A', 8 }, { 'F', 6 }, { 'G', 4 } });
        g.add_vertex('D', new Dictionary<char, int>() { { 'F', 8 } });
        g.add_vertex('E', new Dictionary<char, int>() { { 'H', 1 } });
        g.add_vertex('F', new Dictionary<char, int>() { { 'B', 2 }, { 'C', 6 }, { 'D', 8 }, { 'E', 9 }, { 'h', 3 } });
        g.add_vertex('G', new Dictionary<char, int>() { { 'C', 4 }, { 'F', 1 } });
        g.add_vertex('H', new Dictionary<char, int>() { { 'E', 1 }, { 'F', 3 } });
        g.add_vertex('I', new Dictionary<char, int>() { { 'A', 7 } });

        return g;

    }
    Graph CreateGraph2()
    {
        // 
//# http://www.graphviz.org/content/cluster

//        digraph G {
//            layout = circo;

//            1;
//            2;
//            3;
//            4;
//            5;
//            6;

//            1-> 2[label = "7"];
//            2-> 1[label = "7"];

//            1-> 3[label = "9"];
//            3-> 1[label = "9"];

//            1-> 6[label = "14"];
//            6-> 1[label = "14"];

//            2-> 3[label = "10"];
//            3-> 2[label = "10"];

//            2-> 4[label = "15"];
//            4-> 2[label = "15"];

//            3-> 4[label = "11"];
//            4-> 3[label = "11"];

//            3-> 6[label = "2"];
//            6-> 3[label = "2"];

//            4-> 5[label = "6"];
//            5-> 4[label = "6"];

//            5-> 6[label = "9"];
//            6-> 5[label = "9"];

//        }




        Graph g = new Graph();
        g.add_vertex('1', new Dictionary<char, int>() { { '2', 7 }, { '3', 9 }, { '6', 14 } });
        g.add_vertex('2', new Dictionary<char, int>() { { '1', 7 }, { '3', 10 }, { '4', 15 } });
        g.add_vertex('3', new Dictionary<char, int>() { { '1', 9 }, { '2', 10 }, { '4', 11 }, { '6', 2 } });        
        g.add_vertex('4', new Dictionary<char, int>() { { '5', 6 } });
        g.add_vertex('5', new Dictionary<char, int>() {  { '6', 9 } });
        g.add_vertex('6', new Dictionary<char, int>() {  { '5', 9 } });

        return g;

    }
    // Start is called before the first frame update
    void Start()
    {

        g = CreateGraph1();
        //g = CreateGraph2();

        //Debug.Log("(T)g.HasEdge('A', 'B')="+g.HasEdge('A', 'B')); //True
        //Debug.Log("(F)g.HasEdge('A', 'D')=" + g.HasEdge('A', 'D')); //False

        ////deg of (u->v) = g.vertices[u].Count
        ////deg of (v->u) = g.vertices[v].Count

        //bool retCode = g.RecursiveDFS_b('A');

        //Debug.Log("retCode="+retCode+" "+string.Join(" ",g.Visited.Values));
        //foreach(var e in g.Vertices)
        //{
        //    bool ve = g.Visited.ContainsKey(e) && g.Visited[e];
        //    Debug.Log(string.Format("The vertex {0} was {1}visited!", e, (!ve ? "NOT ": "")));
        //}
        ////Brenton's Cleanup Testing
        //List<char> visited = new List<char>();
        //retCode = g.RecDFS('A', ref  visited);
        
        ////Brenton's test
        //List<char> list = new List<char>();
        //var all = g.RecDFS('A', ref list);
        //print($"[{all}] -" + string.Join(", ", list));

        //test Dijkstra
        char start = 'A', to = 'F';
        //char start = '1', to = '5';
        List<char> path = g.Dijkstra_Shortest_Path(start, to);
        if (path == null)
        {
            print(string.Format("No path passes from {0} to {1}",start,to));
        }
        else
        {
            ///List<char> path_r = path.Reverse();
            string s = "";
            path.ForEach(x => { s += x; print(x); });
            print( "Reverse Path:" + s);
            path.Reverse();
            print("Path:"+start+"->" + string.Join("->", path));

        }




    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
