using System;
using System.Collections.Generic;
using System.Linq;
using static BinaryTreeInorderTraversal.BinaryTreeManager;

namespace BinaryTreeInorderTraversal
{

    //https://leetcode.com/problems/binary-tree-inorder-traversal/
    //обход бинарного дерева

    public class Program
    {
        static void Main(string[] args)
        {
            BinaryTree binaryTree = BinaryTreeManager.GenerateTree(50, 10, 100);
            binaryTree.MakeTreeDump();
            binaryTree.MakeTreeLevelsDump();

            binaryTree.GetInorderVerticalTraversal().ForEach(x => Console.Write($" {x} ")) ;
        }



        public class Solution
        {
            IList<int> rez = new List<int>();

            public IList<int> InorderTraversal(TreeNode root)
            {
                rez.Clear();
                MakeNodeOvercomeVertical(root);
                return rez;
            }

            private void MakeNodeOvercomeVertical(TreeNode startNode)
            {
                if (startNode != null)
                {
                    if (startNode.left != null) MakeNodeOvercomeVertical(startNode.left);
                    rez.Add(startNode.val);
                    if (startNode.right != null) MakeNodeOvercomeVertical(startNode.right);
                }
            }
        }
    }

    public class BinaryTree
    {
        public TreeNode Root = null;

        public TreeLevels Levels { get { return new TreeLevels(this); } }

        List<TreeNode> Nodes = new List<TreeNode>();

        public int MaxLevel
        {
            get
            {
                int[] arr = new int[Nodes.Count];

                for (int i = 0; i < Nodes.Count; i++)
                {
                    arr[i] = Nodes[i].level;
                }

                int max = arr[0];

                for (int i = 0; i < Nodes.Count; i++)
                {
                    if (arr[i] > max) max = arr[i];
                }

                return max;
            }
        }

        public void AddNode(int value)
        {
            //заполнение по слоям 
            // когда 1 слой заполнен, приниается за второй

            if (Root == null)
            {
                Root = new TreeNode(value);
                Root.level = 1;
                Root.Parent = null;
                Nodes.Add(Root);
                return;
            }
            AddNode2Node(Root, value, GetIncompleteLevel(100), 2);
        }

        private bool AddNode2Node(TreeNode node, int value, int targetLevel, int currentLevel)
        {
            if (currentLevel <= targetLevel)
            {

                if (node.Left == null)
                {
                    node.Left = new TreeNode(value);

                    node.Left.Parent = node;

                    node.Left.level = currentLevel;

                    Nodes.Add(node.Left);

                    return true;
                }

                if (node.Right == null)
                {
                    node.Right = new TreeNode(value);

                    node.Right.level = currentLevel;

                    node.Right.Parent = node;

                    Nodes.Add(node.Right);

                    return true;
                }

                bool addLeftRez = AddNode2Node(node.Left, value, targetLevel, currentLevel + 1);

                if (addLeftRez) return true;

                bool addRightRez = AddNode2Node(node.Right, value, targetLevel, currentLevel + 1);

                return addRightRez;

            }
            else
            {
                return false;
            }
        }

        private int GetIncompleteLevel(int maxlevel)
        {

            //вернуть первый незаполненный уровень

            int counter = 1;

            while (true)
            {
                if (counter > maxlevel) return -1;
                if (!TreeIsCompletedAtLevel(counter)) return counter;
                counter++;
            }
        }

        private bool TreeIsCompletedAtLevel(int targetLevel)
        {
            if (targetLevel == 1) return (Root != null);

            return NodeIsCompletedAtLevel(Root, targetLevel, 2);
        }

        private bool NodeIsCompletedAtLevel(TreeNode node, int targetLevel, int currentLevel)
        {
            if (node.IsComplete)
            {
                if (targetLevel == currentLevel) return true;

                bool leftComplete = NodeIsCompletedAtLevel(node.Left, targetLevel, currentLevel + 1);
                if (!leftComplete) return false;

                bool rightComplete = NodeIsCompletedAtLevel(node.Right, targetLevel, currentLevel + 1);
                return rightComplete;
            }
            else
            {
                //нашел
                return false;
            }
        }

        public void MakeTreeDump()
        {
            MakeTreeOvercomeHorizontal(x => Console.Write($" {x.Val}_{x.level}_{x.IsRoot} "), () => Console.WriteLine(""));
        }
        public void MakeTreeLevelsDump()
        {
            Console.WriteLine($"");
            Console.WriteLine($"*** Making tree levels dump ***");
            Levels.Levels.ForEach(level => Console.WriteLine($"{level.Id}_{level.Count} "));
        }

        public List<int> GetInorderVerticalTraversal()
        {
            List<int> rez = new List<int>();

            MakeTreeOvercomeVertical(x=>rez.Add(x.Val));

            return rez;
        }

        public delegate void TreeOvercomeDelegate(TreeNode node);

        public delegate void TreeOvercomeLevelShiftDelegate();

        public void MakeTreeOvercomeVertical(TreeOvercomeDelegate treeOvercomeDelegate)
        {
            //обход бинарного дерева слева направо по слоям
            MakeNodeOvercomeVertical(Root, treeOvercomeDelegate);
        }

        private void MakeNodeOvercomeVertical(TreeNode startNode, TreeOvercomeDelegate treeOvercomeDelegate)
        {
            if (startNode != null)
            {
                treeOvercomeDelegate(startNode);
                if (startNode.Left != null) MakeNodeOvercomeVertical(startNode.Left, treeOvercomeDelegate);
                if (startNode.Right != null) MakeNodeOvercomeVertical(startNode.Right, treeOvercomeDelegate);
            }
        }

        public void MakeTreeOvercomeHorizontal(TreeOvercomeDelegate treeNodeDelegate, TreeOvercomeLevelShiftDelegate treeLevelShiftDelegate)
        {
            //обход бинарного дерева слева направо по слоям
            MakeNodeOvercomeHorizontal(Root, treeNodeDelegate, treeLevelShiftDelegate);
        }

        private void MakeNodeOvercomeHorizontal(TreeNode startNode, TreeOvercomeDelegate treeOvercomeDelegate, TreeOvercomeLevelShiftDelegate treeLevelShiftDelegate)
        {
            if (Root == null) return;

            int max = MaxLevel;

            for (int i = 1; i <= max; i++)
            {
                treeLevelShiftDelegate();

                List<TreeNode> treeNodes = Nodes.Where(x => x.level == i).ToList();

                treeNodes.ForEach(x => treeOvercomeDelegate(x));
            }
        }

        public class TreeLevels
        {
            public List<TreeLevel> Levels = new List<TreeLevel>();
            public TreeLevels(BinaryTree tree)
            {
                if (tree.Root == null) return;

                int max = tree.MaxLevel;

                for (int i = 1; i <= max; i++)
                {
                    TreeLevel level = new TreeLevel();

                    level.Id = i;

                    List<TreeNode> treeNodes = tree.Nodes.Where(x => x.level == i).ToList();

                    treeNodes.ForEach(x => level.Nodes.Add(x));

                    Levels.Add(level);
                }
            }

            public class TreeLevel
            {
                public int Id;

                public List<BinaryTree.TreeNode> Nodes = new List<BinaryTree.TreeNode>();

                public int Count { get { return Nodes.Count; } }

            }

        }

        public class TreeNode
        {
            public int Val;

            public TreeNode Left;

            public TreeNode Right;

            public TreeNode Parent;

            public bool IsRoot { get { return Parent == null; } }

            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.Val = val;
                this.Left = left;
                this.Right = right;
            }

            public int level;

            public bool IsComplete
            {
                get { return (Left != null && Right != null); }
            }
        }
    }

    public class BinaryTreeManager
    {
        public static BinaryTree GenerateTree(int elementsCount, int minElementValue, int maxElementValue)
        {
            if (elementsCount == 0) { return null; }

            BinaryTree tree = new BinaryTree();

            Random random = new Random();

            for (int i=1; i<= elementsCount;i++)
            {
                tree.AddNode(random.Next(minElementValue, maxElementValue));
            }

            return tree;
        }



    }

    public class TreeNode
    {
          public int val;
          public TreeNode left;
          public TreeNode right;
          public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
          {
                this.val = val;
                this.left = left;
                this.right = right;
          }
    }


}
