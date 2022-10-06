using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StM
{
    public class DefaultTransition : Transition
    {
        private Condition _doesApply;
        public Condition DoesApply { get => _doesApply; set { _doesApply = value;}}
        private State _next;
        public State Next { get => _next; set { _next = value; }}

        public DefaultTransition(State next, Condition condition)
        {
            _next = next;
            DoesApply = condition;
        }
    }
}
