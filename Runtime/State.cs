
using System.Collections.Generic;

namespace UnityStateMachine
{
    /// <summary>
    /// State are expected to be used within the context of a state machine, i.e. its lifecycle-methods are not expected to be called directly.
    /// </summary>
    public interface State
    {
        /// <summary>
        /// OnInit is called immediatly after transitioning into the respective state.
        /// </summary>
        public void OnInit();

        /// <summary>
        /// Handles a single time step whenever this state is active. 
        /// </summary>
        public void OnTick();

        /// <summary>
        /// OnReset is called on a state immediately after transitioning out of it.
        /// </summary>
        public void OnReset();
    }
}
