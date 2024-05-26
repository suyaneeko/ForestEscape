using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CombatInfo
{
    public uint combatID;
    public uint monsterNum;
    public MONSTER_ID monsterID;
}

public class TriggerObject : MonoBehaviour
{
    [SerializeField] CombatInfo combatInfo;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (MONSTER_ID.COMBAT_END == combatInfo.monsterID)
                GameManager.Instance.GameClear();
            else
            {
                CombatManager.Instance.CombatStart(combatInfo, other.gameObject.GetComponent<Player>().GetBatPosition(), other.transform.forward);
                Destroy(gameObject);
            }
        }
    }
}
