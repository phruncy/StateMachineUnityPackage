
using System.Collections.Generic;

namespace StM
{
    public interface State
    {
        public void Init();
        public void Execute();
        public void Reset();
    }
}
