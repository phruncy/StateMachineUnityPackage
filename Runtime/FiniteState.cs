using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StM
{
    public interface FiniteState : State
    {
        public bool IsDone();
    }
}
