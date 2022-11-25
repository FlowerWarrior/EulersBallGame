using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform target2;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 lookPoint = (target.position + target2.position) / 2 + new Vector3(0, 2, 0);
        offset = lookPoint - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPoint = (target.position + target2.position) / 2 + new Vector3(0, 2, 0);
        //Vector3 coinLine = new Vector3(0, 8, 0);
        //lookPoint = (lookPoint + coinLine) / 2;
        transform.LookAt(lookPoint + offset);
    }
}
