using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisRevolve : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    float speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
