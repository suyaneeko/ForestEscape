using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveZ = 0f;
        float moveX = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveZ += 1f;
        }
     
        if (Input.GetKey(KeyCode.S))
        {
            moveZ -= 1f;
        }
     
        if (Input.GetKey(KeyCode.A))
        {
            moveX -= 1f;
        }
     
        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1f;
        }
     
        transform.Translate(new Vector3(moveX, 0f, moveZ) * 0.1f);
    }
}
