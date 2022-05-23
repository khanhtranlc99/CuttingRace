using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject grass_Particle;
    public GameObject spark_Particle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            grassParticle();
            Invoke("sparkParticle", 0.05f);
        }
    }
    public void grassParticle()
    {
        GameObject particleClone = Instantiate(grass_Particle);
        particleClone.transform.position = this.transform.position + new Vector3(0f, 0f, 0f);
        particleClone.GetComponent<ParticleSystem>().Play();
        Destroy(particleClone.gameObject, 0.8f);
    }
    public void sparkParticle()
    {
        GameObject particleClone = Instantiate(spark_Particle);
        particleClone.transform.position = this.transform.position + new Vector3(0f, 0.2f, 0f);
        particleClone.GetComponent<ParticleSystem>().Play();
        Destroy(particleClone.gameObject, 0.7f);
    }
}
