class Node {
    int value;
    Node left, right, parent;

    public Node(int value) {
        this.value = value;
        this.left = this.right = this.parent = null;
    }
}

public class BinaryTree {
    private Node root;

    public void insert(int value) {
        root = insertRec(root, null, value);
    }

    private Node insertRec(Node node, Node parent, int value) {
        if (node == null) {
            Node newNode = new Node(value);
            newNode.parent = parent;
            return newNode;
        }
        if (value < node.value) {
            node.left = insertRec(node.left, node, value);
        } else if (value > node.value) {
            node.right = insertRec(node.right, node, value);
        }
        return node;
    }

    public Node search(int value) {
        return searchRec(root, value);
    }

    private Node searchRec(Node node, int value) {
        if (node == null || node.value == value) {
            return node;
        }
        if (value < node.value) {
            return searchRec(node.left, value);
        } else {
            return searchRec(node.right, value);
        }
    }

    public void delete(int value) {
        root = deleteRec(root, value);
    }

    private Node deleteRec(Node node, int value) {
        if (node == null) return node;

        if (value < node.value) {
            node.left = deleteRec(node.left, value);
        } else if (value > node.value) {
            node.right = deleteRec(node.right, value);
        } else {
            if (node.left == null) return node.right;
            if (node.right == null) return node.left;

            Node minNode = findMin(node.right);
            node.value = minNode.value;
            node.right = deleteRec(node.right, minNode.value);
        }
        return node;
    }

    private Node findMin(Node node) {
        if (node == null) return null;
        while (node.left != null) {
            node = node.left;
        }
        return node;
    }

    private Node findMax(Node node) {
        if (node == null) return null;
        while (node.right != null) {
            node = node.right;
        }
        return node;
    }

    private Node findMinOfRoot(Node node) {
        if (node == null) return null;
        while (node.left != null) {
            node = node.left;
        }
        System.out.println("Lowest number: "+ node.value);
        return node;
    }

    private Node findMaxOfRoot(Node node) {
        if (node == null) return null;
        while (node.right != null) {
            node = node.right;
        }
        System.out.println("Highest number: " + node.value);

        return node;
    }

    public Node successor(int value) {
        Node current = search(value);
        if (current == null) return null;

        if (current.right != null) {
            return findMin(current.right);
        }

        Node parent = current.parent;
        while (parent != null && current == parent.right) {
            current = parent;
            parent = parent.parent;
        }
        return parent;
    }

    public Node predecessor(int value) {
        Node current = search(value);
        if (current == null) return null;

        if (current.left != null) {
            return findMax(current.left);
        }

        Node parent = current.parent;
        while (parent != null && current == parent.left) {
            current = parent;
            parent = parent.parent;
        }
        return parent;
    }

    public void printTree() {
        if (root == null) {
            System.out.println("Tree is empty.");
            return;
        }
        printTreeRec(root, 0, "Root: ");
    }

    private void printTreeRec(Node node, int level, String prefix) {
        if (node == null) return;

        String spaces = " ".repeat(level * 4);
        System.out.println(spaces + prefix + node.value);

        if (node.left != null) {
            printTreeRec(node.left, level + 1, "LEFT--- ");
        }
        if (node.right != null) {
            printTreeRec(node.right, level + 1, "RIGHT--- ");
        }
    }

    public static void main(String[] args) {
        BinaryTree tree = new BinaryTree();

        tree.insert(50);
        tree.insert(30);
        tree.insert(70);
        tree.insert(20);
        tree.insert(40);
        tree.insert(60);
        tree.insert(80);
        tree.insert(90);
        tree.insert(85);

        System.out.println("Tree structure:");
        tree.printTree();

        System.out.println("\nFind 40: " + (tree.search(40) != null ? "Found" : "Not found"));

        tree.delete(30);
        System.out.println("\nAfter deleting 30:");
        tree.printTree();

        Node successor = tree.successor(50);
        System.out.println("\nSuccessor 50: " + (successor != null ? successor.value : "None"));

        Node predecessor = tree.predecessor(50);
        System.out.println("Predecessor 50: " + (predecessor != null ? predecessor.value : "None") + "\n");

        tree.findMinOfRoot(tree.root);
        tree.findMaxOfRoot(tree.root);
    }
}
