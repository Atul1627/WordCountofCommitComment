using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordCount.Models.CommentWordCount
{
    public class AVLTree<T> : IEnumerable<T> where T : IComparable
    {
        public AVLTreeNode<T> Head { get; internal set; }

        #region Add items

        public void Add(T input)
        {
            AddToNode(input, Head);
        }

        private void AddToNode(T input, AVLTreeNode<T> current)
        {
            if (Head == null)
            {
                Head = new AVLTreeNode<T>(input, null, this);
                return;
            }
            if (input.CompareTo(current.Value) < 0)
            {
                if (current.LeftNode == null)
                {
                    current.LeftNode = new AVLTreeNode<T>(input, current, this);
                }
                else
                {
                    AddToNode(input, current.LeftNode);
                }
            }
            else
            {
                if (current.RightNode == null)
                {
                    current.RightNode = new AVLTreeNode<T>(input, current, this);
                }
                else
                {
                    AddToNode(input, current.RightNode);
                }
            }

            var parent = current;
            while (parent != null)
            {
                if (parent.State != TreeState.Balanced)
                {
                    parent.Balance();
                }

                parent = parent.Parent; //keep going up
            }

        }
        #endregion
        #region Remove
        //public bool Remove(T input)
        //{
        //    AVLTreeNode<T> current, parent;

        //    current = FindWithParent(input, out parent);

        //    bool removeSucessfull = RemoveNode(current);

        //    if (removeSucessfull)
        //    {
        //        while (parent != null)
        //        {
        //            if (parent.State != TreeState.Balanced)
        //            {
        //                parent.Balance();
        //            }

        //            parent = parent.Parent; //keep going up
        //        }
        //    }

        //    return true;
        //}

        //private bool RemoveNode(AVLTreeNode<T> current)
        //{
        //    if (current == null || current._tree != this)
        //    {
        //        return false;
        //    }

        //    var parent = current.Parent;
        //    //Case: no right child

        //    if (current == Head)
        //    {
        //        Head = null;
        //    }
        //    if (current.RightNode == null)
        //    {
        //        if (parent == null)
        //        {
        //            Head = current.LeftNode;
        //        }
        //        else
        //        {
        //            int compare = parent.Value.CompareTo(current.Value);
        //            if (compare > 0)
        //            {
        //                parent.LeftNode = current.LeftNode;
        //            }
        //            else
        //            {
        //                parent.LeftNode = current.LeftNode;
        //            }
        //        }
        //    }
        //    //Case: the right child don't have left child
        //    else if (current.RightNode.LeftNode == null)
        //    {
        //        current.RightNode.LeftNode = current.LeftNode;

        //        if (parent == null)
        //        {
        //            Head = current.RightNode;
        //        }
        //        else
        //        {
        //            int compare = parent.Value.CompareTo(current.Value);
        //            if (compare > 0)
        //            {
        //                parent.LeftNode = current.RightNode;
        //            }
        //            else
        //            {
        //                parent.RightNode = current.RightNode;
        //            }
        //        }
        //    }
        //    //Case: the right child has a left child
        //    else
        //    {
        //        AVLTreeNode<T> leftMost = current.RightNode.LeftNode;
        //        AVLTreeNode<T> leftMostParent = current.RightNode;

        //        while (leftMost.LeftNode != null)
        //        {
        //            leftMostParent = leftMost;
        //            leftMost = leftMostParent.LeftNode;
        //        }
        //        leftMostParent.LeftNode = leftMost.RightNode;
        //        leftMost.LeftNode = current.LeftNode;
        //        leftMost.LeftNode = current.RightNode;

        //        if (parent == null)
        //        {
        //            Head = leftMost;
        //        }
        //        else
        //        {
        //            int compare = parent.Value.CompareTo(current.Value);
        //            if (compare > 0)
        //            {
        //                parent.LeftNode = leftMost;
        //            }
        //            else
        //            {
        //                parent.RightNode = leftMost;
        //            }
        //        }
        //    }
        //    return true;
        //}
        #endregion
        #region Search
        public bool Search(T input)
        {
            return SearchNode(input, Head);
        }
        private bool SearchNode(T input, AVLTreeNode<T> current)
        {

            if (current == null)
            {
                return false;
            }

            if (input.CompareTo(current.Value) == 0)
            {
                return true;
            }

            return SearchNode(input, input.CompareTo(current.Value) < 0 ? current.LeftNode : current.RightNode);
        }
        //private AVLTreeNode<T> FindWithParent(T input, out AVLTreeNode<T> parent)
        //{
        //    AVLTreeNode<T> current = Head;
        //    parent = null;

        //    while (current != null)
        //    {
        //        int compare = current.Value.CompareTo(input);

        //        if (compare > 0)
        //        {
        //            parent = current;
        //            current = current.LeftNode;
        //        }
        //        else if (compare < 0)
        //        {
        //            parent = current;
        //            current = current.RightNode;
        //        }
        //        else
        //        {
        //            return current;
        //        }
        //    }

        //    return null;
        //}
        #endregion
        #region Traversals
        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversal(action, Head);
        }

        private void PreOrderTraversal(Action<T> action, AVLTreeNode<T> node)
        {
            if (node == null) return;

            action(node.Value);
            PreOrderTraversal(action, node.LeftNode);
            PreOrderTraversal(action, node.RightNode);
        }



        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversal(action, Head);
        }

        private void PostOrderTraversal(Action<T> action, AVLTreeNode<T> node)
        {
            if (node == null) return;

            PostOrderTraversal(action, node.LeftNode);
            PostOrderTraversal(action, node.RightNode);
            action(node.Value);

        }

        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(action, Head);
        }

        private void InOrderTraversal(Action<T> action, AVLTreeNode<T> node)
        {
            if (node == null) return;

            InOrderTraversal(action, node.LeftNode);
            string leftn = node.LeftNode != null ? node.LeftNode.Value.ToString() : "No Node";
            string rightn = node.RightNode != null ? node.RightNode.Value.ToString() : "No Node";
            HttpContext.Current.Response.Write(node.Value + "  - (Left Node - " + leftn + "  ,Right Node - " + rightn + ")");
            action(node.Value);
            InOrderTraversal(action, node.RightNode);
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            if (Head != null)
            {
                Stack<AVLTreeNode<T>> stack = new Stack<AVLTreeNode<T>>();

                AVLTreeNode<T> current = Head;

                bool goLeft = true;

                stack.Push(current);

                while (stack.Count > 0)
                {
                    if (goLeft)
                    {
                        while (current.LeftNode != null)
                        {
                            stack.Push(current);
                            current = current.LeftNode;
                        }
                    }
                    yield return current.Value;

                    if (current.RightNode != null)
                    {
                        current = current.RightNode;
                        goLeft = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeft = false;
                    }
                }
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}