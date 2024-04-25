using UnityEngine;

public class GameAssetsManager : MonoBehaviour
{

    public static GameAssetsManager instance;

    public static GameAssetsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameAssetsManager>("GameAssetsManager");
            }

            return instance;
        }

        private set
        {

        }
    }


    [Header("Exposed Game Assets")]
    public Transform pfEnemy;
    public Transform pfEnemyDieParticles;
    public Transform pfArrowProjectile;
    public Transform pfBuildingConstruction;
    public Transform pfBuildingPlacedParticles;

}
