using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    enum COMBAT_ID
    {
        COMBAT_BEE,
        COMBAT_SLIME
    }

    [SerializeField] private Monster[] monsters;

    void Update()
    {
        
    }

    void StartCombat(COMBAT_ID eID)
    {
        switch(eID)
        {
            case COMBAT_ID.COMBAT_BEE:
                break;
            case COMBAT_ID.COMBAT_SLIME:
                break;
        }
    }
}
