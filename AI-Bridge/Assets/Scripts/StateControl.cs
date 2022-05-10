using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType { Patrol, Chase, Attack }

public class StateControl : MonoBehaviour
{
    public static StateType stateType { get; private set; } // hangi state de olduðumu baþka class lar bilmesi için

    private void Start()
    {
        stateType = StateType.Patrol;

    }

    private void Update()
    {
        if (GeneralControl.goOnPatrol)
            stateType = StateType.Patrol;
        else if (GeneralControl.goOnChase)
            stateType = StateType.Chase;
        else if (GeneralControl.goOnAttack)
            stateType = StateType.Attack;
    }
}
