using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGenLevel : MonoBehaviour
{
    [SerializeField] private Texture2D levelTexture;
    public GameObject test;
    public GameObject test2;
    private float unitPerPixel;
    private void Start()
    {
        Generate();
    }
    private void Generate()
    {

        float width = levelTexture.width;
        float height = levelTexture.height;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {    
                Color pixelColor = levelTexture.GetPixel(x, y);
                Vector3 spawnPos = new Vector3(x, y, 0) ;
                if (pixelColor == Color.black)
                    Spawn(test, spawnPos);
                else if (pixelColor == Color.white)
                    Spawn(test2, spawnPos);
            }
        }
    }
    public void Spawn(GameObject prefabTile, Vector3 position)
    {
     

        GameObject obj = Instantiate(prefabTile, position, Quaternion.identity, transform);
     
    }
}
