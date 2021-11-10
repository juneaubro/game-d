using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AStarAlgorithm : MonoBehaviour
{
    public enum HeuristicStrategy { EuclideanDistance, ManhatanDistance };
    public class Graph4AStar {
        HeuristicStrategy heuristicStrategy = HeuristicStrategy.ManhatanDistance;
        Dictionary<char, Dictionary<char, float>> vertices = new Dictionary<char, Dictionary<char, float>>();
        Dictionary<char, Vector3> verticesPos = new Dictionary<char, Vector3>();
        public void add_vertex(char vertexName, Dictionary<char, float> adjacentEdges, Vector3 pos) {
            vertices[vertexName] = adjacentEdges;
            verticesPos[vertexName] = pos;
        }
        public string getStrategy() {
            if (heuristicStrategy.Equals(HeuristicStrategy.ManhatanDistance)) {
                return "ManhatanDistance";
            } else {
                return "EuclideanDistance";
            }
        }
        public List<char> getVertices { get { return vertices.Keys.ToList<char>(); } }
        public List<char> ShortestPathViaAStar(char start, char end) {
            //NodeMark [g,h;f,p]
            var distances = new Dictionary<char, float>();//f[=g+h]
            var parents = new Dictionary<char, char>();
            var path = new List<char>();
            var gScore = new Dictionary<char, float>();//h
            var open = new List<char>();
            var closed = new List<char>();
            //Step 0 - init
            gScore[start] = 0;
            float hScore = TargetDistanceEstimator(start,end);
            distances[start] = gScore[start] + hScore;//g+h
            parents[start] = '-';
            open.Add(start);
            //Step 1
            while (open.Count != 0) {
                open.Sort((x,y)=>distances[x].CompareTo(distances[y]));
                var n = open[0];
                open.Remove(n);
                if (n == end) {
                    while (n!='-') {
                        path.Add(n);
                        n = parents[n];
                    }
                    return path;
                }
                foreach (var nPrime in vertices[n]) {
                    var newg = gScore[n] + nPrime.Value;
                    if (open.Contains(nPrime.Key) || closed.Contains(nPrime.Key)){
                        if (gScore[nPrime.Key] <= newg)
                            continue;
                    }
                    parents[nPrime.Key] = n;
                    gScore[nPrime.Key] = newg;
                    var new_hScore = TargetDistanceEstimator(start, nPrime.Key);
                    distances[nPrime.Key] = newg + new_hScore;
                    if (closed.Contains(nPrime.Key))
                        closed.Remove(nPrime.Key);
                    if (!open.Contains(nPrime.Key))
                        open.Add(nPrime.Key);
                }
                closed.Add(n);
            }
            return path;
        }
        private float TargetDistanceEstimator(char start,char end) {
            Vector3 v1 = verticesPos[start];
            Vector3 v2 = verticesPos[end];
            float dist = 0;
            switch (heuristicStrategy) {
                case HeuristicStrategy.EuclideanDistance:
                    dist = Vector3.Distance(v1, v2);
                    break;
                case HeuristicStrategy.ManhatanDistance:
                    Vector3 dv = v1 - v2;
                    dist = Mathf.Abs(dv.x)+ Mathf.Abs(dv.y)+ Mathf.Abs(dv.z);
                    break;
            }
            return dist;
        }
    }
    void Start()
    {
        print("A Star Algorithm");
        Graph4AStar g = new Graph4AStar();
        //graph 1
        //g.add_vertex('C', new Dictionary<char, float>() { { 'B', 4 }, { 'X', 4 } }, new Vector3(8,0,4));
        //g.add_vertex('B', new Dictionary<char, float>() { { 'A', 4 }, { 'C', 4 } }, new Vector3(4,0,4));
        //g.add_vertex('A', new Dictionary<char, float>() { { 'B', 4 }, { 'Z', 20 } }, new Vector3(0,0,4));
        //g.add_vertex('X', new Dictionary<char, float>() { { 'C', 4 }, { 'Y', 4 } }, new Vector3(8,0,0));
        //g.add_vertex('Y', new Dictionary<char, float>() { { 'X', 4 }, { 'Z', 4 } }, new Vector3(4,0,0));
        //g.add_vertex('Z', new Dictionary<char, float>() { { 'Y', 4 }, { 'W', 4 }, { 'A', 20 } }, new Vector3(0,0,0));
        //g.add_vertex('W', new Dictionary<char, float>() { { 'Z', 4 } }, new Vector3(-4,0,0));
        //graph 2
        g.add_vertex('A', new Dictionary<char, float>() { { 'G', 100 }, { 'B', 10 } }, new Vector3(0, 0, 0));
        g.add_vertex('B', new Dictionary<char, float>() { { 'A', 10 }, { 'C', 10 } }, new Vector3(0, 0, 10));
        g.add_vertex('C', new Dictionary<char, float>() { { 'B', 10 }, { 'D', 10 } }, new Vector3(10, 0, 10));
        g.add_vertex('D', new Dictionary<char, float>() { { 'G', 30 }, { 'C', 10 }, { 'E', 10 } }, new Vector3(20, 0, 10));
        g.add_vertex('G', new Dictionary<char, float>() { { 'D', 30 }, { 'A', 100 }, { 'F', 10 } }, new Vector3(20, 0, 0));
        g.add_vertex('E', new Dictionary<char, float>() { { 'D', 10 }, { 'F', 10 } }, new Vector3(30, 0, 10));
        g.add_vertex('F', new Dictionary<char, float>() { { 'G', 10 }, { 'E', 10 } }, new Vector3(30, 0, 0));
        print(g.ToString());
        print(g.getVertices.Count);
        //graph 1
        //char start = 'A';
        //char end = 'W';
        //graph 2
        char start = 'A';
        char end = 'G';
        List<char> path = g.ShortestPathViaAStar(start, end);
        print("Shortest path:");
        path.Reverse();
        string sPath = "";
        path.ForEach(e => {
            sPath += e;
            print(e);
        });
        print("sPath: " + sPath + " using "+g.getStrategy());
    }
}
