using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        RandomPosition();
    }

    // Update is called once per frame
    

    void RandomPosition()
    {
        float x = Random.Range(-8, 8);
        float y = Random.Range(-5, 5);

        transform.position = new Vector2(x, y);
    }
}
