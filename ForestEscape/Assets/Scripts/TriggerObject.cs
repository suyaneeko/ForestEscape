using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [SerializeField] private int combatNum;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collide with Player");
            
            CombatManager.Instance.CombatStart(combatNum, other.gameObject.GetComponent<Player>().GetBatPosition(), other.transform.forward);
            //Destroy(gameObject);
        }
    }
}
