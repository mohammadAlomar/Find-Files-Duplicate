using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDuplicates
{
    public interface IDuplicates
    {
        IReadOnlyCollection<string> Filepath { get; }
    }
}
