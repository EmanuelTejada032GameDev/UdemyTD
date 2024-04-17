using UnityEngine;
using UnityEngine.UI;

public class BuildingConstructionUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private BuildingConstruction _buildingConstruction;
    
    void Update()
    {
        _image.fillAmount = _buildingConstruction.GetConstructionTimerNormalized();
    }
}
