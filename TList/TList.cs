using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6.TList
{
    public class TList<T>:IEnumerable<T>
    {
        PNode<T>? First { get; set; }
        PNode<T>? Last { get; set; }
        PNode<T>? Current { get; set; }

        public void InsertBefore(T value)
        {
            var node = new PNode<T>(value);
            if (Current == null)
            {
                First = node;
                Last = node;
            } else if (Current.Previous == null) 
            {
                node.Next = First;
                First.Previous = node;
                First = node;
            } else if (Current.Next == null) 
            { 
                node.Previous = Last;
                Last.Next = node;
                Last = node;
            } else
            {
                node.Previous = Current.Previous;
                node.Next = Current;
                Current.Previous.Next = node;
                Current.Previous = node;
            }

            Current = node;
        }

        public bool Remove(T data)
        {
            var current = First;
            while (current != null && current.Value != null)
            {
                if (current.Value.Equals(data))
                {
                    break;
                }
                current = current.Next;
            }

            if (current != null)
            {
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    Last = current.Previous;
                }

                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    First = current.Next;
                }

                if (current.Equals(Current))
                    Current = Last;
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            PNode<T>? current = First;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        public IEnumerator<T> GetBackEnumerator()
        {
            PNode<T>? current = Last;
            while (current != null)
            {
                yield return current.Value;
                current = current.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }
}
