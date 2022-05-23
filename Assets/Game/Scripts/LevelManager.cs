using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    [Header("Level texture")]
    [SerializeField] private Texture2D levelTexture;

    [Header("Tiles Prefabs")]
    [SerializeField] private GameObject prefabWallTile;
    [SerializeField] private GameObject prefabRoadTile;

    [Header("Ball and Road paint color")]
    public Color[] startColors;
    public Color[] paintColors;
    public int colorNO = 0;

    public List<RoadTile> roadTilesList = new List<RoadTile>();
    //public List<GameObject> flowerList = new List<GameObject>();
    [HideInInspector] public RoadTile defaultBallRoadTile;


    private Color colorWall = Color.white;
    private Color colorRoad = Color.black;

    private float unitPerPixel;

    private int winCount = 0;

    public GameObject popEffect;

    [Header("Levels")]
    public Texture2D[] levels;
    int lvlNo;
    public Transform TileParent;
    public Transform flowerParent;

    public BallMovement ball;

    public bool isFlower;

    private void Awake()
    {
        lvlNo = PlayerPrefs.GetInt("levelno", 0);
        levelTexture = levels[lvlNo];
        Generate();
        //assign first road tile as default poition for the ball:
        defaultBallRoadTile = roadTilesList[0];
    }



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Generate();
        }

    }

    public void NextLevel()
    {

        PlayerPrefs.SetInt("coin", canvasManager.Instance.coins + 50);   //// change rewared(50) to what ever you want!
        foreach (Transform g in TileParent)
        {
            Destroy(g.gameObject);
        }
        popEffect.SetActive(false);

        if (lvlNo < levels.Length - 1)
            lvlNo++;
        else
            lvlNo = 3;

        PlayerPrefs.SetInt("levelshow", PlayerPrefs.GetInt("levelshow", 1) + 1);
        canvasManager.Instance.NextLevel();

        if (colorNO < paintColors.Length - 1)
        {
            colorNO++;
        }
        else
        {
            colorNO = 0;
        }
        PlayerPrefs.SetInt("levelno", lvlNo);
        levelTexture = levels[lvlNo];
        roadTilesList.Clear();
        Generate();
        winCount = 0;
        defaultBallRoadTile = roadTilesList[0];
        ball.ResetBall();
        if (UnityAdsManager.instance)
            UnityAdsManager.instance.ShowInterstitialAd();

        Transform flowerParent = transform.GetChild(2);
        flowerParent.gameObject.SetActive(false);

        for (int i = 0; i < flowerParent.childCount; i++)
        {
            flowerParent.GetChild(i).GetChild(0).gameObject.SetActive(false);
            flowerParent.GetChild(i).GetChild(1).gameObject.SetActive(false);
            flowerParent.GetChild(i).GetChild(2).gameObject.SetActive(false);

        }
        ball.transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
    }

    private void Generate()
    {
        unitPerPixel = prefabWallTile.transform.lossyScale.x;
        float halfUnitPerPixel = unitPerPixel / 2f;

        float width = levelTexture.width;
        float height = levelTexture.height;

        Vector3 offset = (new Vector3(width / 2f, 0f, height / 2f) * unitPerPixel)
                         - new Vector3(halfUnitPerPixel, 0f, halfUnitPerPixel);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //Get pixel color :
                Color pixelColor = levelTexture.GetPixel(x, y);

                Vector3 spawnPos = ((new Vector3(x, 0f, y) * unitPerPixel) - offset);

                if (pixelColor == colorWall)
                    Spawn(prefabWallTile, spawnPos);
                else if (pixelColor == colorRoad)
                    Spawn(prefabRoadTile, spawnPos);
            }
        }
        ball.ballMesh.material.color = paintColors[colorNO];
    }

    public void Spawn(GameObject prefabTile, Vector3 position)
    {
        //fix Y position:
        position.y = prefabTile.transform.position.y;

        GameObject obj = Instantiate(prefabTile, position, Quaternion.identity, transform);
        obj.transform.parent = TileParent;
        
        if (prefabTile == prefabRoadTile)
        {
            RoadTile road = obj.GetComponent<RoadTile>();
            obj.transform.GetChild(3).parent = flowerParent;
            roadTilesList.Add(road);
            road.ChangeColor(startColors[colorNO]);
        }
    }

    public void CheckWin(RoadTile tile)
    {
        winCount++;
        if (roadTilesList.Count <= winCount)
        {
            foreach (RoadTile t in roadTilesList)
            {
                t.GetComponent<BoxCollider>().enabled = false;
                t.anim.enabled = true;
            }
            popEffect.SetActive(true);
            //print("Level Completed!");
            ball.transform.GetChild(0).gameObject.SetActive(false);
            Invoke("FlowerVisible", 0.5f);
        }

    }
    public void FlowerVisible()
    {
        StartCoroutine("Pop");
        
    }
    IEnumerator Pop()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        Transform flowerParent = transform.GetChild(2);
        flowerParent.gameObject.SetActive(true);

        for (int i = 0; i < flowerParent.childCount; i++)
        {
            yield return new WaitForSeconds(0.02f);
            flowerParent.GetChild(i).GetChild(Random.Range(0, 3)).gameObject.SetActive(true);
            //flowerList.Add(flowerParent.GetChild(i).GetChild(Random.Range(0, 3)).gameObject);
        }
        Invoke("finalParticle", 0.2f);
        canvasManager.Instance.WinMethod(1.5f);
        
    }
    public void finalParticle()
    {
        GameObject particleClone = Instantiate(popEffect);
        particleClone.GetComponent<ParticleSystem>().Play();
    }

}
    


