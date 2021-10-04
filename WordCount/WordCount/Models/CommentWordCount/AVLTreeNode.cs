using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordCount.Models.CommentWordCount
{
    public class AVLTreeNode<T> : IComparable<T> where T : IComparable
    {
        private AVLTreeNode<T> _left;
        private AVLTreeNode<T> _right;
        internal AVLTree<T> _tree;
        public T Value { get; set; }
        public AVLTreeNode<T> Parent { get; internal set; }

        public AVLTreeNode(T value, AVLTreeNode<T> parent, AVLTree<T> tree)
        {
            Value = value;
            Parent = parent;
            _tree = tree;
        }

        public AVLTreeNode<T> LeftNode
        {
            get { return _left; }
            internal set
            {
                _left = value;
                if (_left != null)
                {
                    _left.Parent = this;
                }
            }
        }
        public AVLTreeNode<T> RightNode
        {
            get { return _right; }
            internal set
            {
                _right = value;
                if (_right != null)
                {
                    _right.Parent = this;
                }
            }
        }
        private int LeftHeight { get { return MaxChildrenHeight(LeftNode); } }
        private int RightHeight { get { return MaxChildrenHeight(RightNode); } }
        public TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                {
                    return TreeState.LeftHeavy;
                }

                if (RightHeight - LeftHeight > 1)
                {
                    return TreeState.RightHeavy;
                }

                return TreeState.Balanced;
            }
        }
        private int BalanceFactor
        {
            get { return RightHeight - LeftHeight; }
        }
        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
        internal void Balance()
        {
            if (State == TreeState.RightHeavy)
            {
                if (RightNode != null && RightNode.BalanceFactor < 0)
                {
                    LeftRightRotation();
                }
                else
                {
                    LeftRotation();
                }
            }
            else if (State == TreeState.LeftHeavy)
            {
                if (LeftNode != null && LeftNode.BalanceFactor > 0)
                {
                    RightLeftRotation();
                }
                else
                {
                    RightRotation();
                }
            }
        }
        private void LeftRotation()
        {
            AVLTreeNode<T> rootParent = Parent;
            AVLTreeNode<T> root = this;
            AVLTreeNode<T> pivot = RightNode;

            bool isLeftChild = (rootParent != null) && rootParent.LeftNode == root;
            root.RightNode = pivot.LeftNode;
            pivot.LeftNode = root;

            root.Parent = pivot;
            pivot.Parent = rootParent;

            if (root.RightNode != null)
                root.RightNode.Parent = root;

            if (_tree.Head == root)
            {
                _tree.Head = pivot;
            }
            else if (isLeftChild)
            {
                rootParent.LeftNode = pivot;
            }
            else if (rootParent != null)
            {
                rootParent.RightNode = pivot;
            }
        }
        private void RightRotation()
        {
            AVLTreeNode<T> rootParent = Parent;
            AVLTreeNode<T> root = this;
            AVLTreeNode<T> pivot = LeftNode;
            bool isLeftChild = (rootParent != null) && rootParent.LeftNode == root;

            root.LeftNode = pivot.RightNode;
            pivot.RightNode = root;

            root.Parent = pivot;
            pivot.Parent = rootParent;

            if (root.LeftNode != null)
                root.LeftNode.Parent = root;

            if (_tree.Head == root)
            {
                _tree.Head = pivot;
            }
            else if (isLeftChild)
            {
                rootParent.LeftNode = pivot;
            }
            else if (rootParent != null)
            {
                rootParent.RightNode = pivot;
            }
        }
        private void LeftRightRotation()
        {
            RightNode.RightRotation();
            LeftRotation();
        }
        private void RightLeftRotation()
        {
            LeftNode.LeftRotation();
            RightRotation();
        }
        private int MaxChildrenHeight(AVLTreeNode<T> node)
        {
            int maxChildrenHeight = 0;
            if (node != null)
            {
                maxChildrenHeight = 1 + Math.Max(MaxChildrenHeight(node.LeftNode), MaxChildrenHeight(node.RightNode));
            }
            return maxChildrenHeight;
        }

    }
}