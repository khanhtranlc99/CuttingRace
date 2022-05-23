using UnityEngine ;
using DG.Tweening ;
using System.Collections.Generic ;

public class BallRoadPainter : MonoBehaviour {
   [SerializeField] private LevelManager levelManager ;
   [SerializeField] private BallMovement ballMovement ;
   [SerializeField] private MeshRenderer ballMeshRenderer ;

   public int paintedRoadTiles = 0 ;

   private void Start () {
      //paint ball:
      ballMeshRenderer.material.color = levelManager.paintColors[0];

      //paint default ball tile:
      Paint (levelManager.defaultBallRoadTile, .5f, 0f) ;

      //paint ball road :
      ballMovement.onMoveStart += OnBallMoveStartHandler ;
   }

   private void OnBallMoveStartHandler (List<RoadTile> roadTiles, float totalDuration) {
   
   }

   private void Paint (RoadTile roadTile, float duration, float delay) {
   
   }
}
