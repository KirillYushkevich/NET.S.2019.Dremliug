using System;
using System.Collections.Generic;

// Binary tree example: https://stackoverflow.com/a/13780680/11052981
// About walk orders: https://www.geeksforgeeks.org/tree-traversals-inorder-preorder-and-postorder/
// Style cop settings hate comments in nested methods, sorry.
namespace BinarySearchTreeGeneric
{
    /// <summary>
    /// Represents a generic binary search tree.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinarySearchTreeG<T>
    {
        #region Fields
        private readonly IComparer<T> _comparer;

        // Used by iterator.
        private List<Node<T>> list; 
        #endregion

        #region Constructors
        public BinarySearchTreeG(IComparer<T> comparer = null)
        {
            // If comparer is not provided and default one does not exist then refuse to create a tree.
            if (comparer is null && !typeof(IComparable).IsAssignableFrom(typeof(T)) && !typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException($"Provide a comparer for {typeof(T)}");
            }

            _comparer = comparer ?? Comparer<T>.Default;
        }

        public BinarySearchTreeG(IEnumerable<T> collection, IComparer<T> comparer = null) : this(comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException("Collection cannot be null");
            }

            foreach (var item in collection)
            {
                Insert(item);
            }
        } 
        #endregion

        #region Properties
        public Node<T> Root { get; private set; }

        private int Count { get; set; } = 0;
        #endregion

        #region Contains & Insert methods
        public void Insert(T value)
        {
            var newNode = new Node<T>(value);
            Count++;

            if (Root == null)
            {
                Root = newNode;
                return;
            }

            Insert(Root, newNode);

            void Insert(Node<T> root, Node<T> node)
            {
                if (_comparer.Compare(node.Value, root.Value) <= 0)
                {
                    if (root.Left is null)
                    {
                        root.Left = node;
                    }
                    else
                    {
                        Insert(root.Left, node);
                    }
                }
                else
                {
                    if (root.Right is null)
                    {
                        root.Right = node;
                    }
                    else
                    {
                        Insert(root.Right, node);
                    }
                }
            }
        }

        public bool Contains(T valeu)
        {
            return Contains(Root, valeu);

            bool Contains(Node<T> node, T value)
            {
                if (node is null)
                {
                    return false;
                }

                var order = _comparer.Compare(value, node.Value);

                if (order == 0)
                {
                    return true;
                }
                else if (order < 0)
                {
                    return Contains(node.Left, value);
                }
                else
                {
                    return Contains(node.Right, value);
                }
            }
        } 
        #endregion

        #region Walk Orders
        public IEnumerable<Node<T>> WalkPreorder()
        {
            return Iterate(Preorder);

            void Preorder(Node<T> node)
            {
                if (node is null)
                {
                    return;
                }

                list.Add(node);
                Preorder(node.Left);
                Preorder(node.Right);
            }
        }

        public IEnumerable<Node<T>> WalkInorder()
        {
            return Iterate(Inorder);

            void Inorder(Node<T> node)
            {
                if (node is null)
                {
                    return;
                }

                Inorder(node.Left);
                list.Add(node);
                Inorder(node.Right);
            }
        }

        public IEnumerable<Node<T>> WalkPostorder()
        {
            return Iterate(Postorder);

            void Postorder(Node<T> node)
            {
                if (node is null)
                {
                    return;
                }

                Postorder(node.Left);
                Postorder(node.Right);
                list.Add(node);
            }
        }
        #endregion

        #region Iterator
        // Iterator block. 
        private IEnumerable<Node<T>> Iterate(Action<Node<T>> walkOrder)
        {
            list = new List<Node<T>>(Count);

            // Fill the list with values in the specified order.
            walkOrder(Root);

            for (int i = 0; i < Count; i++)
            {
                yield return list[i];
            }
        } 
        #endregion
    }
}
