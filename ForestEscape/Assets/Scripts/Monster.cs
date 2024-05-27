using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // Cheat key for Test
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CombatManager.Instance.CheckCombat(1);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            CombatManager.Instance.CheckCombat(1);
            Destroy(gameObject);
        }
    }
}
