using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDuplicates
{
    public class Duplicates : IDuplicates
    {
        public IReadOnlyCollection<string> Filepath { get; }

        public Duplicates(IReadOnlyCollection<string> filepath)
        {
            Filepath = filepath;
        }
    }
}
