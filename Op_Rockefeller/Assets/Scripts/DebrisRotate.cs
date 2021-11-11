using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisRotate : MonoBehaviour
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
        transform.RotateAround(target.transform.position, new Vector3 (0,1,1), speed * Time.deltaTime);
    }
}
