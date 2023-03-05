using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GraphModel
{
    public class Graph
    {
        private readonly Node[] nodes;

        public Graph(int nodesCount)
        {
            nodes = Enumerable
                .Range(0, nodesCount)
                .Select(z => new Node(z))
                .ToArray();
        }

        public static Graph MakeGraph(params (int, int)[] adjacentNodes)
        {
            var graph = new Graph(adjacentNodes
                .SelectMany(t => new int[] { t.Item1, t.Item2 })
                .Max() + 1);
            for (int i = 0; i < adjacentNodes.Length; i++)
            {
                graph.Connect(adjacentNodes[i].Item1, adjacentNodes[i].Item2);
            }
            return graph;
        }

        public Node this[int index] => nodes[index];

        public Edge this[int from, int to] => Edges
            .Where(edge => edge.From.Number == from && edge.To.Number == to)
            .FirstOrDefault();

        public IEnumerable<Node> Nodes
        {
            get
            {
                foreach (var node in nodes)
                    yield return node;
            }
        }

        public IEnumerable<Edge> Edges
        {
            get
            {
                return nodes
                    .SelectMany(n => n.IncidentEdges)
                    .Distinct();
            }
        }

        public bool Contains(Node node) => nodes.Contains(node);

        public void Connect(int v1, int v2)
        {
            if (v1 < 0 || v1 >= nodes.Length || v2 < 0 || v2 >= nodes.Length)
                throw new ArgumentOutOfRangeException();
            nodes[v1].Connect(nodes[v2]);
        }
    }
}
