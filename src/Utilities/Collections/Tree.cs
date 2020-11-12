using System.Collections;
using System.Collections.Generic;

namespace PiBa.Utilities.Collections
{
    public class Tree<T> : IEnumerable<Tree<T>>
    {
        public T Data { get; set; }
        public Tree<T> Parent { get; set; }
        public List<Tree<T>> Children { get; set; }

        public bool IsRoot => Parent == null;

        public bool IsLeaf => Children.Count == 0;

        public Tree(T data)
        {
            this.Data = data;
            this.Children = new List<Tree<T>>();
        }
        public override string ToString() => Data != null ? Data.ToString() : "[data null]";

        #region iterating
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Tree<T>> GetEnumerator()
        {
            yield return this;
            foreach (var directChild in this.Children)
            {
                foreach (var anyChild in directChild)
                    yield return anyChild;
            }
        }

        #endregion
    }
}