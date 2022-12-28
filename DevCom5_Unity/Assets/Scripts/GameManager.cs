using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GameMode
{
    Normal,
    Build,
    Production,
}

public enum BuildingType
{
    TownCenter = 0,
}


public class GameManager : MonoBehaviour
{
    public Material invalidBuildingLocationMaterial = null;
    public GameMode gameMode = GameMode.Normal;
    public GameObject towncenterPrefab = null;
    private GameObject buildingPreview = null;
    private GameObject selectedBuilding = null;
    private List<GameObject> buildings = new();
    private Material oldMaterial = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (gameMode)
        {
            case GameMode.Normal:
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, 100))
                        {
                            var rigidBody = hit.collider.gameObject.GetComponentInParent<Rigidbody>();

                            if (rigidBody == null) { break; }

                            foreach (var building in buildings)
                            {
                                if (rigidBody.gameObject == building)
                                {
                                    SetProductionMode(building);
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }

            case GameMode.Build:
                { 
                    int layerMask = 1 << 10;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, layerMask))
                    {
                        if (buildingPreview == null)
                        {
                            buildingPreview = GameObject.Instantiate(towncenterPrefab);
                            buildingPreview.GetComponentInChildren<NavMeshObstacle>().enabled = false;
                            oldMaterial = buildingPreview.GetComponentInChildren<MeshRenderer>().material;
                            Debug.Assert(oldMaterial != null);
                        }
                        buildingPreview.transform.position = new Vector3(Mathf.Round(hit.point.x), hit.point.y, Mathf.Round(hit.point.z));
                        var building = buildingPreview.GetComponentInChildren<Building>();
                        if (building.IsColliding())
                        {
                            buildingPreview.GetComponentInChildren<MeshRenderer>().material = invalidBuildingLocationMaterial;
                        }
                        else
                        {
                            Debug.Assert(oldMaterial != null);
                            buildingPreview.GetComponentInChildren<MeshRenderer>().material = oldMaterial;
                        }
                        if (Input.GetMouseButtonUp(0))
                        {
                            if (!building.IsColliding())
                            {
                                buildingPreview.GetComponentInChildren<NavMeshObstacle>().enabled = true;
                                buildings.Add(buildingPreview);
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
                    break;
                }

            case GameMode.Production:
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, 100))
                        {
                            var rigidBody = hit.collider.gameObject.GetComponentInParent<Rigidbody>();

                            if (rigidBody == null) { SetNormalMode(); break; }

                            bool set = false;
                            foreach (var building in buildings)
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
                    }
                    break;
                }

            default:
                Debug.Assert(false);
                break;
        }
    }

    private void SetNormalMode()
    {
        Debug.Log("Out");
        gameMode = GameMode.Normal;
        buildingPreview = null;
        selectedBuilding = null;
    }
    public void SetBuildMode(int buildingType)
    {
        gameMode = GameMode.Build;
        buildingPreview = null;
        selectedBuilding = null;
    }
    private void SetProductionMode(GameObject selected)
    {
        Debug.Log(selected.name);
        gameMode = GameMode.Production;
        buildingPreview = null;
        selectedBuilding = selected;
    }
}
