using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphModel
{
    public class Edge
    {
        public readonly Node From;
        public readonly Node To;

        public Edge(Node from, Node to)
        {
            From = from;
            To = to;
        }

        public bool IsIncident(Node node) => From == node || To == node;

        public Node GetAnotherNode(Node node)
        {
            if (node == From)
                return To;
            else if (node == To)
                return From;
            else
                throw new ArgumentException();
        }
    }
}
