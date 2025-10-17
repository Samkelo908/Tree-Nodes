using System.Collections.Generic;

namespace RoyalFamilyTree
{
    /// <summary>
    /// Represents a node in the family tree structure
    /// </summary>
    public class FamilyTreeNode
    {
        public RoyalFamilyMember Member { get; set; }
        public List<FamilyTreeNode> Children { get; set; }
        public FamilyTreeNode Parent { get; set; }

        public FamilyTreeNode(RoyalFamilyMember member)
        {
            Member = member;
            Children = new List<FamilyTreeNode>();
            Parent = null;
        }

        /// <summary>
        /// Adds a child to this node
        /// </summary>
        public void AddChild(FamilyTreeNode child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        /// <summary>
        /// Removes a child from this node
        /// </summary>
        public bool RemoveChild(FamilyTreeNode child)
        {
            if (Children.Remove(child))
            {
                child.Parent = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the depth level of this node in the tree
        /// </summary>
        public int GetDepth()
        {
            int depth = 0;
            FamilyTreeNode current = this;
            while (current.Parent != null)
            {
                depth++;
                current = current.Parent;
            }
            return depth;
        }

        public override string ToString()
        {
            return Member.ToString();
        }
    }
}