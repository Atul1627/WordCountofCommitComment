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