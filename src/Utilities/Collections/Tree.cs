using System.Collections;
using System.Collections.Generic;

namespace WForest.Utilities.Collections
{
    /// <summary>
    /// Generic tree data structure that holds some data and has a parent and a list of children.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Tree<T> : IEnumerable<Tree<T>>
    {
        /// <summary>
        /// Data held by the node.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// The parent of the node. If it's null then this node is the root.
        /// </summary>
        public Tree<T>? Parent { get; set; }

        /// <summary>
        /// Children of the node, if empty it's a leaf.
        /// </summary>
        public List<Tree<T>> Children { get; set; }

        /// <summary>
        /// Utility method to check if it's root by comparing if Parent is null.
        /// </summary>
        public bool IsRoot => Parent == null;

        /// <summary>
        /// Utility method to check if it's leaf by comparing if Children.Count to 0.
        /// </summary>
        public bool IsLeaf => Children.Count == 0;

        /// <summary>
        /// Creates a Tree with the data argument as it's contained Data.
        /// </summary>
        /// <param name="data"></param>
        public Tree(T data)
        {
            this.Data = data;
            this.Children = new List<Tree<T>>();
        }

        /// <summary>
        /// Gets Data.ToString or [data null] if the tree does not contain any data.
        /// In case Data.ToString returns null, this method returns an empty string.
        /// </summary>
        /// <returns>a string to print</returns>
        public override string ToString() => (Data != null ? Data.ToString() : "[data null]") ?? string.Empty;

        #region iterating

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Enumerator to cycle through the tree and its children as a list.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Tree<T>> GetEnumerator()
        {
            yield return this;
            foreach (var directChild in Children)
            {
                foreach (var anyChild in directChild)
                    yield return anyChild;
            }
        }

        #endregion
    }
}