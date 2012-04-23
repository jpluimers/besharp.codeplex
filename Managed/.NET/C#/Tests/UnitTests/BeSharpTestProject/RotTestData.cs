using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeSharpTestProject
{
    public class RotTestData: List<KeyValuePair<string, string>>
    {
        protected void add(string plain, string rot)
        {
            this.Add(new KeyValuePair<string, string>(plain, rot));
        }
    }
}
