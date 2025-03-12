class Node {
    int value, height;
    Node left, right;

    public Node(int value) {
        this.value = value;
        this.height = 1;
    }
}

class AVLTree {
    private Node root;

    private int height(Node node) {
        return node == null ? 0 : node.height;
    }

    private int getBalance(Node node) {
        return node == null ? 0 : height(node.left) - height(node.right);
    }

    private Node rotateRight(Node y) {
        Node x = y.left;
        Node T2 = x.right;

        x.right = y;
        y.left = T2;

        y.height = Math.max(height(y.left), height(y.right)) + 1;
        x.height = Math.max(height(x.left), height(x.right)) + 1;

        return x;
    }

    private Node rotateLeft(Node x) {
        Node y = x.right;
        Node T2 = y.left;

        y.left = x;
        x.right = T2;

        x.height = Math.max(height(x.left), height(x.right)) + 1;
        y.height = Math.max(height(y.left), height(y.right)) + 1;

        return y;
    }

    public void insert(int value) {
        root = insertRec(root, value);
    }

    private Node insertRec(Node node, int value) {
        if (node == null) return new Node(value);

        if (value < node.value) {
            node.left = insertRec(node.left, value);
        } else if (value > node.value) {
            node.right = insertRec(node.right, value);
        } else {
            return node;
        }

        node.height = Math.max(height(node.left), height(node.right)) + 1;

        int balance = getBalance(node);

        if (balance > 1 && value < node.left.value) return rotateRight(node);
        if (balance < -1 && value > node.right.value) return rotateLeft(node);
        if (balance > 1 && value > node.left.value) {
            node.left = rotateLeft(node.left);
            return rotateRight(node);
        }
        if (balance < -1 && value < node.right.value) {
            node.right = rotateRight(node.right);
            return rotateLeft(node);
        }

        return node;
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
            if (node.left == null || node.right == null) {
                Node temp = (node.left != null) ? node.left : node.right;
                if (temp == null) {
                    node = null;
                } else {
                    node = temp;
                }
            } else {
                Node temp = findMin(node.right);
                node.value = temp.value;
                node.right = deleteRec(node.right, temp.value);
            }
        }

        if (node == null) return node;

        node.height = Math.max(height(node.left), height(node.right)) + 1;

        int balance = getBalance(node);

        if (balance > 1 && getBalance(node.left) >= 0) return rotateRight(node);
        if (balance > 1 && getBalance(node.left) < 0) {
            node.left = rotateLeft(node.left);
            return rotateRight(node);
        }
        if (balance < -1 && getBalance(node.right) <= 0) return rotateLeft(node);
        if (balance < -1 && getBalance(node.right) > 0) {
            node.right = rotateRight(node.right);
            return rotateLeft(node);
        }

        return node;
    }

    private Node findMin(Node node) {
        while (node.left != null) node = node.left;
        return node;
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

        printTreeRec(node.left, level + 1, "LEFT--- ");
        printTreeRec(node.right, level + 1, "RIGHT--- ");
    }

    public static void main(String[] args) {
        AVLTree tree = new AVLTree();

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

        tree.delete(30);
        System.out.println("\nAfter deleting 30:");
        tree.printTree();
    }
}
