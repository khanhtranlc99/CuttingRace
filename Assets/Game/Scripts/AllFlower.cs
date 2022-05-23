using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllFlower : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isOnce;
    public LevelManager levelManager;
    void Start()
    {
        transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
        Invoke("ScaleDown", 0.3f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ScaleDown()
    {
        transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
    }
}

