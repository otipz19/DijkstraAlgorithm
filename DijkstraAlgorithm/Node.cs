using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphModel
{
    public class Node
    {
        private readonly List<Edge> incidentEdges = new List<Edge>();
        public readonly int Number;

        public Node(int number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return $"Node #{Number}";
        }

        public IEnumerable<Node> AdjacentNodes => incidentEdges.Select(e => e.GetAnotherNode(this));

        public IEnumerable<Edge> IncidentEdges
        {
            get
            {
                foreach (var edge in incidentEdges)
                    yield return edge;
            }
        }

        public void Connect(Node other)
        {
            //if (!graph.Contains(other) || !graph.Contains(this))
            //    throw new ArgumentException($"Graph doesn't contain {other} or {this} node");
            var edge = new Edge(this, other);
            this.incidentEdges.Add(edge);
            other.incidentEdges.Add(edge);
        }

        public void Disconnect(Edge edge)
        {
            edge.From.incidentEdges.Remove(edge);
            edge.To.incidentEdges.Remove(edge);
        }

        public bool IsAdjacent(Node other)
        {
            return incidentEdges
                .Select(e => e.GetAnotherNode(this))
                .Contains(other);
        }
    }
}
