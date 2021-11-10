using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AStarAlgorithm : MonoBehaviour
{
    public enum HeuristicStrategy { EuclideanDistance, ManhatanDistance };
    HeuristicStrategy heuristicStrategy = HeuristicStrategy.EuclideanDistance;
    public class Graph4AStar {
        HeuristicStrategy heuristicStrategy = HeuristicStrategy.EuclideanDistance;
        Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();
        Dictionary<char, Vector3> verticesPos = new Dictionary<char, Vector3>();
        public void add_vertex(char vertexName, Dictionary<char, int> adjacentEdges, Vector3 pos) { 
            
        }
        public List<char> ShortestPathViaAStar(char start, char end) {
            //NodeMark [g,h;f,p]
            var distances = new Dictionary<char, int>();//f[=g+h]
            var parents = new Dictionary<char, int>();
            var path = new List<char>();
            var gScore = new Dictionary<char, int>();//h
            var open = new List<char>();
            var closed = new List<char>();
            //Step 0
            gScore[start] = 0;
            int hScore = TargetDistanceEstimator(start,end);
            distances[start] = gScore[start] + hScore;//g+h
            parents[start] = '-';
            //Step 1
            return path;
        }
        private int TargetDistanceEstimator(char start,char end) {
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
            return (int)dist;
        }
    }
    void Start()
    {
        Graph4AStar g = new Graph4AStar();
        g.add_vertex('C', new Dictionary<char, int>() { { 'B', 4 }, { 'Z', 6 } }, new Vector3(8,0,4));
    }
}
