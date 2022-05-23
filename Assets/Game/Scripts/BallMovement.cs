using UnityEngine ;
using GG.Infrastructure.Utils.Swipe ;
using DG.Tweening ;
using System.Collections.Generic ;
using UnityEngine.Events ;
using DG.Tweening;

public class BallMovement : MonoBehaviour {

    public static BallMovement Instance;

   [SerializeField] private SwipeListener swipeListener ;
   [SerializeField] private LevelManager levelManager ;

   private const float MAX_RAY_DISTANCE = 10f ;

   public UnityAction<List<RoadTile>, float> onMoveStart ;

   private Vector3 moveDirection ;
   public bool canMove = false ;

    public float speed = 5f;
    RaycastHit hit;
    public Transform rayPos;
    public float distance;
    public Transform currentObj;

    public ParticleSystem moveTrail;
    public ParticleSystem ExplodeParticle;

    public AudioSource audio;

    public Transform charParent;
    //public Animator anim;

    public MeshRenderer ballMesh;

    private void Awake()
    {
        if (!Instance)
            Instance = this;

    }
    private void Start () {
        // change default ball position :

        ChangeChar();
        transform.position = levelManager.defaultBallRoadTile.position ;
        moveTrail.startColor = levelManager.paintColors[levelManager.colorNO];
        ExplodeParticle.startColor = levelManager.paintColors[levelManager.colorNO];
         swipeListener.OnSwipe.AddListener (swipe => {
             switch (swipe)
             {
                 case "Right":
                     if (!canMove)
                     {
                         moveDirection = Vector3.right;
                         //transform.rotation = Quaternion.Euler(0,90,0);
                         canMove = RayMethod();
                     }


                     break;
                 case "Left":
                     if (!canMove)
                     {
                         moveDirection = Vector3.left;
                         //transform.rotation = Quaternion.Euler(0, -90, 0);
                         canMove = RayMethod();
                     }


                     break;
                 case "Up":
                     if (!canMove)
                     {
                         moveDirection = Vector3.forward;
                         //transform.rotation = Quaternion.Euler(0, 0, 0);
                         canMove = RayMethod();
                     }

                     break;
                 case "Down":
                     if (!canMove)
                     {
                         moveDirection = Vector3.back;
                         //transform.rotation = Quaternion.Euler(0, 180, 0);
                         canMove = RayMethod();
                     }

                     break;
             }
         });
    }

    public void Update()
    {
         MoveBall () ;
        Debug.DrawRay(rayPos.position, moveDirection, Color.red, .2f);
        distance = Vector3.Distance(rayPos.position, hit.point);
        if(!canMove)
        {
            if(currentObj)
            transform.position = currentObj.localPosition;
            //anim.SetBool("run", false);

        }else
        {
            //anim.SetBool("run", true);


        }
        transform.GetChild(0).transform.Rotate(0, 0, 90f ); //my code//
    }

    public void ResetBall()
    {
        canMove = false;
        transform.position = levelManager.defaultBallRoadTile.position;
        currentObj.localPosition = levelManager.defaultBallRoadTile.position;
        moveTrail.startColor = levelManager.paintColors[levelManager.colorNO];
        ExplodeParticle.startColor = levelManager.paintColors[levelManager.colorNO];
        audio.pitch = 1;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        //anim.SetBool("run", false);
        Transform flowerParent = transform.GetChild(2);
        flowerParent.gameObject.SetActive(false);

        for (int i = 0; i < flowerParent.childCount; i++)
        {
            flowerParent.GetChild(i).GetChild(0).gameObject.SetActive(false);
            flowerParent.GetChild(i).GetChild(1).gameObject.SetActive(false);
            flowerParent.GetChild(i).GetChild(2).gameObject.SetActive(false);
        }
        levelManager.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void ChangeChar()
    {
        foreach (Transform p in charParent)
        {
            p.gameObject.SetActive(false);
        }

        //anim = charParent.GetChild(PlayerPrefs.GetInt("charno", 0)).GetComponent<Animator>();
        //anim.gameObject.SetActive(true);
    }

    public bool RayMethod()
    {
     
        if (Physics.Raycast(rayPos.position, moveDirection, out hit, 0.32f))
        {
            if(hit.collider.tag=="wall")
            {
                //print(hit.transform.name);
                canMove = false;
                //moveTrail.Stop();
                ExplodeParticle.Play();
                audio.pitch = 1;
                //anim.SetBool("run", false);
                return false;
                    
            }else
                return true;
        }
        else
        {
            return true;
        }
    }

   private void MoveBall () {
      if (canMove) {
           canMove =   RayMethod();
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="ground")
        {
            currentObj = other.transform;
            RoadTile tile = other.GetComponent<RoadTile>();
            //tile.popObj.transform.DOScaleZ(-1f, .2f);
            if(!tile.isPainted)
            {
                tile.isPainted = true;
                foreach (MeshRenderer mesh in tile.meshRenderers)
                {
                    //mesh.material.DOColor(levelManager.paintColors[levelManager.colorNO], .2f);
                }
                levelManager.CheckWin(tile);
                audio.Play();
                audio.pitch += .1f;
            }
            if (canMove)
                moveTrail.Play();
        }

    }
}
