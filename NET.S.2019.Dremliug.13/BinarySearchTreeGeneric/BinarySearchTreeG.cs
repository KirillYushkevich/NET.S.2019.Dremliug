using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private readonly IComparer<T> _comparer;

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
        #endregion

        #region Contains & Insert methods
        public void Insert(T value)
        {
            var newNode = new Node<T>(value);

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
            return Preorder(Root);

            IEnumerable<Node<T>> Preorder(Node<T> node)
            {
                if (node is null)
                {
                    yield break;
                }

                yield return node;

                foreach (Node<T> n in Preorder(node.Left))
                {
                    yield return n;
                }

                foreach (Node<T> n in Preorder(node.Right))
                {
                    yield return n;
                }
            }
        }

        public IEnumerable<Node<T>> WalkInorder()
        {
            return Inorder(Root);

            IEnumerable<Node<T>> Inorder(Node<T> node)
            {
                if (node is null)
                {
                    yield break;
                }

                foreach (Node<T> n in Inorder(node.Left))
                {
                    yield return n;
                }

                yield return node;

                foreach (Node<T> n in Inorder(node.Right))
                {
                    yield return n;
                }
            }
        }

        public IEnumerable<Node<T>> WalkPostorder()
        {
            return Postorder(Root);

            IEnumerable<Node<T>> Postorder(Node<T> node)
            {
                if (node is null)
                {
                    yield break;
                }

                foreach (Node<T> n in Postorder(node.Left))
                {
                    yield return n;
                }

                foreach (Node<T> n in Postorder(node.Right))
                {
                    yield return n;
                }

                yield return node;
            }
        } 
        #endregion
    }
}
