using GraphModel;

namespace DijkstraAlgorithm
{
    internal class DijkstraData
    {
        public double Cost { get; set; }
        public Node Parent { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var graph = Graph.MakeGraph(
                (0, 1),
                (0, 2),
                (0, 3),
                (1, 3),
                (2, 3));

            var weights = new Dictionary<Edge, double>();
            weights[graph[0, 1]] = 1;
            weights[graph[0, 2]] = 2;
            weights[graph[0, 3]] = 6;
            weights[graph[1, 3]] = 4;
            weights[graph[2, 3]] = 2;

            foreach(var node in DijkstraAlgorithm(graph, weights, graph[0], graph[3]))
            {
                Console.WriteLine(node);
            }

            var graph1 = Graph.MakeGraph(
                (0, 1),
                (1, 0),
                (1, 2));

            var weights1 = new Dictionary<Edge, double>();
            weights1[graph1[0, 1]] = 1;
            weights1[graph1[1, 0]] = 0;
            weights1[graph1[1, 2]] = 6;

            Console.WriteLine();
            foreach (var node in DijkstraAlgorithm(graph1, weights1, graph1[0], graph1[2]))
            {
                Console.WriteLine(node);
            }
        }

        public static List<Node> DijkstraAlgorithm(Graph graph, Dictionary<Edge, double> weights, Node start, Node end)
        {
            var notVisited = graph.Nodes.ToList();
            var track = new Dictionary<Node, DijkstraData>();
            track[start] = new DijkstraData() { Cost = 0, Parent = null };

            while (true)
            {
                Node toOpen = null;
                double bestPrice = double.PositiveInfinity;
                foreach(var node in notVisited)
                {
                    if(track.ContainsKey(node) && track[node].Cost < bestPrice)
                    {
                        toOpen = node;
                        bestPrice = track[node].Cost;
                    }
                }

                if (toOpen == null) return null;
                if (toOpen == end) break;

                foreach(var edge in toOpen.IncidentEdges.Where(e => e.From == toOpen))
                {
                    var nextNode = edge.To;
                    var newCost = track[toOpen].Cost + weights[edge];
                    if(!track.ContainsKey(nextNode) || track[nextNode].Cost > newCost)
                    {
                        //track[nextNode].Cost = newCost;
                        //track[nextNode].Parent = toOpen;
                        track[nextNode] = new DijkstraData { Cost = newCost, Parent = toOpen };
                    }
                }
                notVisited.Remove(toOpen);
            }

            var path = new List<Node>();
            for(var node = end; node != null; node = track[node].Parent)
            {
                path.Add(node);
            }
            path.Reverse();
            return path;
        }
    }
}