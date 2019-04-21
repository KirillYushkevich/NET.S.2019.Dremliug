using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTreeGeneric
{
    /// <summary>
    /// Represents Binary Search Tree node.
    /// </summary>
    public class Node<T>
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; private set; }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }
    }
}
