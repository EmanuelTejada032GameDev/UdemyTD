using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private BuildingTypeSO _buildingType;
    [SerializeField] private Transform _buildingDemolishBtn;
    [SerializeField] private Transform _buildingRepairBtn;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
    }

    public void Start()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().BuildingTypeSO;
        _healthSystem.OnDamaged += OnBuildingDamaged;
        _healthSystem.OnHealed += OnBuildingHealed;
        _healthSystem.OnDied += OnDied;

        _healthSystem.SetMaxHealthAmount(_buildingType.maxHealthAmount, true);
        HideDemolishBtn();
        HideRepairBtn();
    }

    private void OnBuildingDamaged(object sender, EventArgs e)
    {
        ShowRepairBtn();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
        CameraShakeComponent.Instance.ShakeCamera(4f, .1f);
        ChromaticAberrationEffect.Instance.SetChromaticEffectWeigth(.3f);

    }

    private void OnBuildingHealed(object sender, EventArgs e)
    {
        if (_healthSystem.IsFullHealth()) HideRepairBtn();
    }

    private void Update()
    {
    }

    private void OnDied(object sender, EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        Instantiate(GameAssetsManager.Instance.pfEnemyDieParticles, transform.position, Quaternion.identity);
        CameraShakeComponent.Instance.ShakeCamera(10f, .15f);
        ChromaticAberrationEffect.Instance.SetChromaticEffectWeigth(1f);
        Destroy(gameObject);
    }

    public HealthSystem HealthSystem { get { return _healthSystem; } }

    private void OnMouseEnter()
    {
        ShowDemolishBtn();
    }

    private void OnMouseExit()
    {
        HideDemolishBtn();
    }

    private void ShowDemolishBtn()
    {
        if (_buildingDemolishBtn != null)
        {
            _buildingDemolishBtn.gameObject.SetActive(true);
        }
    }

    private void HideDemolishBtn()
    {
        if (_buildingDemolishBtn != null)
        {
            _buildingDemolishBtn.gameObject.SetActive(false);
        }
    }

    private void ShowRepairBtn()
    {
        if (_buildingRepairBtn != null)
        {
            _buildingRepairBtn.gameObject.SetActive(true);
        }
    }

    private void HideRepairBtn()
    {
        if (_buildingRepairBtn != null)
        {
            _buildingRepairBtn.gameObject.SetActive(false);
        }
    }
}


