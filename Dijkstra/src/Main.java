import java.util.*;

class Edge {
    char target;
    int weight;

    Edge(char target, int weight) {
        this.target = target;
        this.weight = weight;
    }
}

class Node {
    char vertex;
    int weight;
    String path;

    Node(char vertex, int weight, String path) {
        this.vertex = vertex;
        this.weight = weight;
        this.path = path;
    }
}

class Graph {
    private final Map<Character, List<Edge>> adjList = new HashMap<>();

    void addEdge(char source, char target, int weight) {
        adjList.putIfAbsent(source, new ArrayList<>());
        adjList.putIfAbsent(target, new ArrayList<>());
        adjList.get(source).add(new Edge(target, weight));
        adjList.get(target).add(new Edge(source, weight)); // Graf nieskierowany
    }

    void dijkstra(char start, char target) {
        PriorityQueue<Node> pq = new PriorityQueue<>(Comparator.comparingInt(n -> n.weight));
        Map<Character, Integer> distances = new HashMap<>();
        Map<Character, String> paths = new HashMap<>();
        Set<Character> visited = new HashSet<>();

        for (char vertex : adjList.keySet()) {
            distances.put(vertex, Integer.MAX_VALUE);
            paths.put(vertex, "");
        }

        distances.put(start, 0);
        pq.add(new Node(start, 0, String.valueOf(start)));

        while (!pq.isEmpty()) {
            Node current = pq.poll();
            char node = current.vertex;

            if (visited.contains(node)) continue;
            visited.add(node);

            for (Edge neighbor : adjList.get(node)) {
                if (visited.contains(neighbor.target)) continue;
                int newDist = distances.get(node) + neighbor.weight;

                if (newDist < distances.get(neighbor.target)) {
                    distances.put(neighbor.target, newDist);
                    paths.put(neighbor.target, current.path + " -> " + neighbor.target);
                    pq.add(new Node(neighbor.target, newDist, paths.get(neighbor.target)));
                }
            }
        }

        System.out.println("Closest path from " + start + " to " + target + ":");
        if (distances.get(target) == Integer.MAX_VALUE) {
            System.out.println("No path found to " + target);
        } else {
            System.out.println("Path: " + paths.get(target));
            System.out.println("Cost: " + distances.get(target));
        }
    }
}

class DijkstraExample {
    public static void main(String[] args) {
        Graph graph = new Graph();
        graph.addEdge('A', 'B', 7);
        graph.addEdge('A', 'C', 9);
        graph.addEdge('A', 'F', 14);
        graph.addEdge('B', 'C', 10);
        graph.addEdge('B', 'D', 15);
        graph.addEdge('C', 'D', 11);
        graph.addEdge('C', 'F', 2);
        graph.addEdge('D', 'E', 6);
        graph.addEdge('E', 'F', 9);

        graph.dijkstra('A', 'F');
    }
}
