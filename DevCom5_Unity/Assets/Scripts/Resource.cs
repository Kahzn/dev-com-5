using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum ResourceType
{
    BrightResource,
    DarkResource
}

public enum ResourceEvent
{
    ResourceCreated,
    ResourcePickedUp,
    ResourcePutDown,
    ResourceDestroyed
}

public interface IResourceObserver
{
    void onResourceChange(Resource r, ResourceEvent e);
}

public class Resource : MonoBehaviour
{
    public ResourceType type;

    public void Start()
    {
        GameManager gamemanager = GameManager.Instance;
        ResourceManager resource_manager = gamemanager.resources;
        resource_manager.addResource(this);
    }

    public void addObserver(IResourceObserver obs)
    {
        observers.Add(obs);
    }

    private List<IResourceObserver> observers = new();
}

public class ResourceManager
{
    public void addObserver(IResourceObserver obs)
    {
        observers.Add(obs);
    }

    public void addResource(Resource r)
    {
        Debug.Log("Moo");
        knownResources.Add(r);
        foreach(var o in observers)
            o.onResourceChange(r, ResourceEvent.ResourceCreated);
    }

    public void removeResource(Resource r)
    {
        knownResources.Remove(r);
    }

    public Resource findNearestResource(
        Vector3 position,
        ResourceType desiredType)
    {
        Resource best = null;
        bool have_best = false;
        float best_distance_squared = 0.0f;

        foreach(var r in knownResources)
        {
            if(r.type != desiredType)
                continue;

            float distance_squared =
                (position - r.transform.position).sqrMagnitude;

            if(!have_best || distance_squared < best_distance_squared)
            {
                have_best = true;
                best_distance_squared = distance_squared;
                best = r;
            }
        }
        return best;
    }

    private List<Resource> knownResources = new();
    private List<IResourceObserver> observers = new();
}