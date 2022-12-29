using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceDepotUi : MonoBehaviour
{
    public TextMeshProUGUI resourceText = null;
    public TextMeshProUGUI populationCapacityText = null;


    private void Update()
    {
        var resourceDepot = GameManager.Instance.resourceDepots[0];
        var humanFaction = GameManager.Instance.factions[0];
        var resourceName = humanFaction switch
        {
            Faction.Bright => "light",
            Faction.Dark => "darkness",
            _ => "wtf",
        };
        resourceText.text = $"{resourceDepot.Amount} {resourceName}";

        populationCapacityText.text = $"{resourceDepot.Population}/{resourceDepot.PopulationCap} pop";
    }
}
