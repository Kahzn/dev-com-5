using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    public static uiManager Instance { get; private set; }

    public GameMode gameMode = GameMode.Normal;
    public Material invalidBuildingLocationMaterial = null;
    public GameObject towncenterPrefab = null;
    public Button[] buttons = null;
    public TextMeshProUGUI errorText = null;
    public Button acceptErrorBtn = null;


    private GameObject buildingPreview = null;
    private GameObject selectedBuilding = null;
    private List<Material> oldMaterials = null;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateProductionButtos();
        errorText.text = "";
        acceptErrorBtn.gameObject.SetActive(false);
    }

    void UpdateNormalMode()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                var rigidBody = hit.collider.gameObject.GetComponentInParent<Rigidbody>();

                if (rigidBody == null) { return; }

                foreach (var building in GameManager.Instance.buildings)
                {
                    if (rigidBody.gameObject == building)
                    {
                        SetProductionMode(building);
                        break;
                    }
                }
            }
        }
    }

    private void UpdateBuildMode()
    {
        int layerMask = 1 << 10;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            if (buildingPreview == null)
            {
                var gamemanager = GameManager.Instance;
                var humanFaction = gamemanager.factions[0];
                var prefabCollection = gamemanager.prefabCollection;
                var buildingPrefab = prefabCollection.GetBuildingPrefab(humanFaction, BuildingType.TownCenter);
                buildingPreview = GameObject.Instantiate(buildingPrefab);
                buildingPreview.GetComponentInChildren<NavMeshObstacle>().enabled = false;

                oldMaterials = new();
                foreach (var renderer in buildingPreview.GetComponentsInChildren<MeshRenderer>())
                {
                    foreach (var material in renderer.materials)
                    {
                        oldMaterials.Add(material);
                    }
                }
                Debug.Log($"saved {oldMaterials.Count} materials");
            }
            buildingPreview.transform.position = new Vector3(Mathf.Round(hit.point.x), hit.point.y, Mathf.Round(hit.point.z));
            var building = buildingPreview.GetComponentInChildren<Building>();
            if (building.IsColliding())
            {
                Debug.Log("Setting materials to 'invalid'");
                foreach (var renderer in buildingPreview.GetComponentsInChildren<MeshRenderer>())
                {
                    var materials = new Material[renderer.materials.Length];
                    for (int i = 0; i < materials.Length; i++)
                    {
                        materials[i] = invalidBuildingLocationMaterial;
                    }
                    renderer.materials = materials;
                }
            }
            else
            {
                Debug.Log($"restoring {oldMaterials.Count} materials");
                int index = 0;
                foreach (var renderer in buildingPreview.GetComponentsInChildren<MeshRenderer>())
                {
                    var materials = new Material[renderer.materials.Length];
                    for (int i = 0; i < materials.Length; ++i)
                    {
                        materials[i] = oldMaterials[index];
                    }
                    renderer.materials = materials;
                    index += materials.Length;
                }
                Debug.Assert(index == oldMaterials.Count);
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (!building.IsColliding())
                {
                    buildingPreview.GetComponentInChildren<NavMeshObstacle>().enabled = true;
                    GameManager.Instance.commandInput.BuildBuilding(BuildingType.TownCenter, Faction.Bright, buildingPreview.transform.position);
                    GameObject.Destroy(buildingPreview.gameObject);
                    buildingPreview = null;
                    gameMode = GameMode.Normal;
                }
                else
                {
                    Debug.Log("no");
                }
            }
        }
        else
        {
            GameObject.Destroy(buildingPreview);
            buildingPreview = null;
        }
    }

    private void UpdateProductionMode()
    {
        if (Input.GetMouseButtonUp(0))
        {
            float HeightRef = Input.mousePosition.y / Screen.height;
            if (HeightRef < 0.1f) { return; }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                var rigidBody = hit.collider.gameObject.GetComponentInParent<Rigidbody>();

                if (rigidBody == null) { SetNormalMode(); return; }

                bool set = false;
                foreach (var building in GameManager.Instance.buildings)
                {
                    if (rigidBody.gameObject == building)
                    {
                        SetProductionMode(building);
                        set = true;
                        break;
                    }
                }
                if (!set) { SetNormalMode(); }
            }
            else {  SetNormalMode(); }
        }
    }

    void Update()
    {
        switch (gameMode)
        {
            case GameMode.Normal:
                UpdateNormalMode();
                break;
            case GameMode.Build:
                UpdateBuildMode();
                break;
            case GameMode.Production:
                UpdateProductionMode();
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }

    private void SetNormalMode()
    {
        gameMode = GameMode.Normal;
        buildingPreview = null;
        selectedBuilding = null;
        UpdateProductionButtos();
    }
    public void SetBuildMode(int buildingType)
    {
        gameMode = GameMode.Build;
        buildingPreview = null;
        selectedBuilding = null;
        UpdateProductionButtos();
    }
    private void SetProductionMode(GameObject selected)
    {
        gameMode = GameMode.Production;
        buildingPreview = null;
        selectedBuilding = selected;

        UpdateProductionButtos();
    }

    public void AcceptErrorBtnClicked()
    {
        errorText.text = "";
        acceptErrorBtn.gameObject.SetActive(false);
    }

    private void UpdateProductionButtos()
    {
        int unitCount = 0;
        Building building = null;
        if (selectedBuilding)
        {
            building = selectedBuilding.GetComponentInChildren<Building>();
            unitCount = building.GetUnitCount();
        }

        for (int i = 0; i < buttons.Length; ++i)
        {
            if (i < unitCount)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = GetStingFromUnitType(building.GetUnitTypeByIndex(i));

                continue;
            }

            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Invalid";
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void BuildingButtonPress(int index)
    {
        if (!selectedBuilding) { Debug.LogError("No selected building"); return; }

        var building = selectedBuilding.GetComponentInChildren<Building>();

        if (!building.HasUnitTypes()) { Debug.LogError("No Units to Build. Class: " + building.name); return; }

        var unitType = building.GetUnitTypeByIndex(index);
        if (unitType == UnitType.NONE) { Debug.LogError("Invalid Unity index for Building. Class: " + building.name + " index: " + index); return; }

        var newBuilding = GameManager.Instance.commandInput.BuildUnit(building, unitType);
        if (newBuilding == null)
        {
            errorText.text = "Not enough Materials";
            acceptErrorBtn.gameObject.SetActive(true);
            SetNormalMode();
        }
    }

    public string GetStingFromUnitType(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.Worker:
                return "Worker";
            case UnitType.NONE:
                return "Invalid";
            default:
                return "Not Defined";
        }
    }

    public string GetStringFromBuildingType(BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingType.TownCenter:
                return "Town Center";
            default:
                return "No Name";
        }
    }
}
