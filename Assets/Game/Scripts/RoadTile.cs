using System.Collections;
using System.Collections.Generic;
using UnityEngine ;

public class RoadTile : MonoBehaviour {
   public MeshRenderer[] meshRenderers;
   public Vector3 position ;
    public Animator anim;
    public bool isPainted = false;
    public Transform popObj;
    public LevelManager levelManager;
    public List<RoadTile> roadTilesList = new List<RoadTile>();

    private void Awake () {
      position = transform.position ;
   }

    public void ChangeColor(Color clr)
    {
        foreach (MeshRenderer mesh in meshRenderers)
        {
            mesh.material.color = clr;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            this.transform.GetChild(2).gameObject.SetActive(false);
            //transform.GetChild(3).parent = levelManager.flowerParent.transform;
        }
    }
    public void Update()
    {
        
    }
}
