using UnityEngine;
using UnityEngine.AI;

public class Worker : Unit, IResourceObserver
{
    void Start()
    {
        GameManager game = GameManager.Instance;
        this.resources = game.resources;
        switch(faction)
        {
            case Faction.Bright:    this.desiredType = ResourceType.BrightResource; break;
            case Faction.Dark:      this.desiredType = ResourceType.DarkResource;   break;
            case Faction.Gaia:      Debug.Assert(false);                            break;
        }
        this.agent = GetComponent<NavMeshAgent>();
        resources.addObserver(this);
        goToNearestResource();
    }

    public void goToNearestResource()
    {
        target = resources.findNearestResource(agent.transform.position, desiredType);

        if(target == null)
            // go idle
            return;

        target.addObserver(this);
        agent.SetDestination(target.transform.position);
    }

    public void onResourceChange(Resource r, ResourceEvent e)
    {
        switch(e)
        {
        case ResourceEvent.ResourcePutDown:
            if(r == target)
                break;
            goto case ResourceEvent.ResourceCreated;
        case ResourceEvent.ResourceCreated:
            {
                Vector3 current_position = agent.transform.position;
                float current_distance_squared =
                    (current_position - target.transform.position).sqrMagnitude;
                float candidate_distance_squared =
                    (current_position - r.transform.position).sqrMagnitude;
                if(candidate_distance_squared < current_distance_squared)
                {
                    r.addObserver(this);
                    agent.SetDestination(target.transform.position);
                }
            }
            break;
        case ResourceEvent.ResourcePickedUp:
        case ResourceEvent.ResourceDestroyed:
            if(r == target)
                goToNearestResource();
            break;
        }
    }

    private ResourceManager resources;
    private ResourceType desiredType;
    private NavMeshAgent agent;
    private Resource target;
}