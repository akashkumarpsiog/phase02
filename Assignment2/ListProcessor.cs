using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    public class ListProcessor
    {
        public void ProcessList(
            List<int> list,
            Predicate<int> filter,
            Func<int,int> transform,
            Action<string> output)
        {
            foreach (int number in list)
            {
                if (filter(number))
                {
                    int result = transform(number);
                    output(result.ToString());
                }
            }
        }
    }
}
