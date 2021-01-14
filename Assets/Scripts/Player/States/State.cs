using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class State
{
    public abstract State ExecuteState(PlayerController player);
    public abstract void ExecuteStatePhysics(PlayerController player);
    
}
