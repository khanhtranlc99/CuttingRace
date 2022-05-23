using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        this.transform.localEulerAngles += new Vector3(0, 0, 100) * Time.deltaTime;
    }
}
