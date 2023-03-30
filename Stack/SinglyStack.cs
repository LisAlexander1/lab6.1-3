using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6.Stack
{
    public class SinglyStack<T>:IEnumerable<T>
    {
        Node<T>? head;
        Node<T>? tail;

        public SinglyStack()
        {

        }

        public void Push(T value)
        {
            var node = new Node<T>(value);
            if (head == null)
            {
                head = node;
            } else
            {
                node.Previous = tail;
            }
            tail = node;
        }

        public T? Pop()
        {
            if (tail == null)
                return default;

            var node = tail;
            tail = tail.Previous;
            if (tail == null)
                head = null;
        
            return node.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T>? current = tail;
            while (current != null && current.Value != null)
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
