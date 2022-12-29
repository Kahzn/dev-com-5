using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerBehaviour : MonoBehaviour
{
    private Faction faction;

    public GameObject arrowPrefab;
    public GameObject arrowSpawnPoint;

    public float shootingSpeed;

    private List<Soldier> soldiersInRange;
    private bool shooting;
    private Soldier soldier;



    // Start is called before the first frame update
    void Start()
    {
        faction = GetComponent<Building>().faction;
        soldiersInRange = new List<Soldier>();
    }


    private void OnTriggerEnter(Collider other)
    {
        soldier = other.gameObject.GetComponent<Soldier>();

        if (soldier != null)
        {
            if (!soldier.faction.Equals(faction))
            {
                soldiersInRange.Add(soldier);
                shooting = true;
                StartCoroutine("ShootWithDelay");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        soldier = other.gameObject.GetComponent<Soldier>();
        if (soldier != null)
        {
            soldiersInRange.Remove(soldier);
            if(soldiersInRange.Count == 0)
            {
                shooting = false;
            }
        }   
    }

    private void Shoot()
    {
        Debug.Log("Shooting!");
        GameObject arrowInstance = Instantiate(arrowPrefab, arrowSpawnPoint.transform.position, Quaternion.identity);
        if(soldier != null)
        {
            Vector3 direction = soldier.transform.position - arrowSpawnPoint.transform.position;
            arrowInstance.transform.forward = direction;
            arrowInstance.GetComponent<Rigidbody>().AddForce(direction * shootingSpeed, ForceMode.Impulse);
        }

    }

    IEnumerator ShootWithDelay()
    {
        while (shooting)
        {
            Shoot();
            yield return new WaitForSeconds(0.5f);
        }
    }


}
