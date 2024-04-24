using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{

    private float _constructionTimer;
    private float _constructionMaxTime;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private BuildingTypeSO _buildingType;
    private BuildingTypeHolder _buildingTypeHolder;

    private Material _buildingConstructionSpriteMaterial;
    
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
        Transform instantiatedBuildingConstruction = Instantiate(pfBuildingConstruction, position, Quaternion.identity);
        BuildingConstruction buildingConstruction = instantiatedBuildingConstruction.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);
        return instantiatedBuildingConstruction.GetComponent<BuildingConstruction>();
    }


    private void Awake()
    {
        _spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        _buildingConstructionSpriteMaterial = _spriteRenderer.material;
        Instantiate(Resources.Load<Transform>("pfBuildingPlacedParticles"), transform.position, Quaternion.identity);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        _constructionTimer -= Time.deltaTime;
        _buildingConstructionSpriteMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());
        if (_constructionTimer <= 0)
        {
            Instantiate(_buildingType.prefab, transform.position, Quaternion.identity);
            Instantiate(Resources.Load<Transform>("pfBuildingPlacedParticles"), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void SetBuildingType(BuildingTypeSO buildingType)
    {
        _buildingType = buildingType;
        _buildingTypeHolder.BuildingTypeSO = buildingType.prefab.GetComponent<BuildingTypeHolder>().BuildingTypeSO;
        _spriteRenderer.sprite = buildingType.sprite;
        _constructionMaxTime = buildingType.constructionTime;
        _constructionTimer = _constructionMaxTime;

        BoxCollider2D buildingBoxCollider = buildingType.prefab.GetComponent<BoxCollider2D>();
        _boxCollider2D.size = buildingBoxCollider.size;
        _boxCollider2D.offset = buildingBoxCollider.offset;
    }

    public float GetConstructionTimerNormalized() => 1 - _constructionTimer / _constructionMaxTime;
}
