using System;
using System.Collections.Generic;
using System.Text;

namespace StackOfStrings
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty()
            => Count == 0;

        public Stack<string> AddRange(params string[] words)
        {
            foreach (var item in words)
                Push(item);

            return this;
        }
    }
}
