using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoyalFamilyTree
{
    /// <summary>
    /// Manages the Royal Family tree structure and operations
    /// </summary>
    public class RoyalFamilyTreeManager
    {
        public FamilyTreeNode Root { get; private set; }

        public RoyalFamilyTreeManager(RoyalFamilyMember monarch)
        {
            Root = new FamilyTreeNode(monarch);
        }

        /// <summary>
        /// Searches for a family member by name using BFS
        /// </summary>
        public FamilyTreeNode SearchBFS(string name)
        {
            if (Root == null) return null;

            Queue<FamilyTreeNode> queue = new Queue<FamilyTreeNode>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                FamilyTreeNode current = queue.Dequeue();

                if (current.Member.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return current;
                }

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        /// <summary>
        /// Searches for a family member by name using DFS
        /// </summary>
        public FamilyTreeNode SearchDFS(string name)
        {
            return SearchDFSRecursive(Root, name);
        }

        private FamilyTreeNode SearchDFSRecursive(FamilyTreeNode node, string name)
        {
            if (node == null) return null;

            if (node.Member.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return node;
            }

            foreach (var child in node.Children)
            {
                FamilyTreeNode result = SearchDFSRecursive(child, name);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the line of succession (living members only, in order)
        /// </summary>
        public List<FamilyTreeNode> GetLineOfSuccession()
        {
            List<FamilyTreeNode> succession = new List<FamilyTreeNode>();
            TraverseForSuccession(Root, succession);

            // Remove the monarch (root) and filter only living members
            return succession.Where(n => n != Root && n.Member.IsAlive).ToList();
        }

        private void TraverseForSuccession(FamilyTreeNode node, List<FamilyTreeNode> succession)
        {
            if (node == null) return;

            succession.Add(node);

            // Traverse children in order (primogeniture succession)
            foreach (var child in node.Children)
            {
                TraverseForSuccession(child, succession);
            }
        }

        /// <summary>
        /// Gets the position in line to the throne for a specific member
        /// </summary>
        public int GetSuccessionPosition(string name)
        {
            FamilyTreeNode member = SearchBFS(name);
            if (member == null || !member.Member.IsAlive) return -1;

            List<FamilyTreeNode> succession = GetLineOfSuccession();
            return succession.IndexOf(member) + 1; // +1 for 1-based indexing
        }

        /// <summary>
        /// Gets all nodes in the tree using BFS
        /// </summary>
        public List<FamilyTreeNode> GetAllNodesBFS()
        {
            List<FamilyTreeNode> allNodes = new List<FamilyTreeNode>();
            if (Root == null) return allNodes;

            Queue<FamilyTreeNode> queue = new Queue<FamilyTreeNode>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                FamilyTreeNode current = queue.Dequeue();
                allNodes.Add(current);

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return allNodes;
        }

        /// <summary>
        /// Gets all nodes in the tree using DFS
        /// </summary>
        public List<FamilyTreeNode> GetAllNodesDFS()
        {
            List<FamilyTreeNode> allNodes = new List<FamilyTreeNode>();
            GetAllNodesDFSRecursive(Root, allNodes);
            return allNodes;
        }

        private void GetAllNodesDFSRecursive(FamilyTreeNode node, List<FamilyTreeNode> allNodes)
        {
            if (node == null) return;

            allNodes.Add(node);

            foreach (var child in node.Children)
            {
                GetAllNodesDFSRecursive(child, allNodes);
            }
        }

        /// <summary>
        /// Gets a text representation of the tree
        /// </summary>
        public string GetTreeDisplay()
        {
            StringBuilder sb = new StringBuilder();
            BuildTreeDisplay(Root, "", true, sb);
            return sb.ToString();
        }

        private void BuildTreeDisplay(FamilyTreeNode node, string indent, bool isLast, StringBuilder sb)
        {
            if (node == null) return;

            sb.Append(indent);
            if (isLast)
            {
                sb.Append("└─ ");
                indent += "   ";
            }
            else
            {
                sb.Append("├─ ");
                indent += "│  ";
            }

            string status = node.Member.IsAlive ? "✓" : "✗";
            sb.AppendLine($"{status} {node.Member.Name} ({node.Member.DateOfBirth.Year})");

            for (int i = 0; i < node.Children.Count; i++)
            {
                BuildTreeDisplay(node.Children[i], indent, i == node.Children.Count - 1, sb);
            }
        }
    }
}