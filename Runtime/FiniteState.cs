using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStateMachine
{
    public interface FiniteState : State
    {
        public bool IsDone();
    }
}
