using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPath : MonoBehaviour
{
    public NavMeshAgent navMesh = null;
    EnemyManager EM;

    private int currNode = 0;
    [SerializeField] private float fov;
    [SerializeField] private LayerMask targetLayer;

    private void Awake()
    {
        EM = GetComponent<EnemyManager>();
        navMesh = GetComponent<NavMeshAgent>();
    }
    public void TraceNavSetting()
    {
        if (navMesh == null) return;
        navMesh.isStopped = false;
        navMesh.updatePosition = true;
        navMesh.updateRotation = true;
    }

    public void AttackNavSetting()
    {
        if (navMesh == null) return;
        navMesh.isStopped = true;
        navMesh.updatePosition = false;
        navMesh.updateRotation = false;
        navMesh.velocity = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        TraceNavSetting();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMesh == null) return;
        if(EM.isDead) return;

        if(EM.target != null)
        {
            Move(EM.target.transform);
        }
        else
        {
            if (navMesh.velocity == Vector3.zero)
            {
                MoveToNextNode();
            }
            else
            {
                Sight();
            }
        }
        
    }

    void MoveToNextNode()
    {
        if (EM.isDead) return;
        TraceNavSetting();
        if (EM.target != null) return;

        if (navMesh.velocity == Vector3.zero)
        {
            if (currNode == EM.enemyPath.Length) currNode = 0;
            Move(EM.enemyPath[currNode++]);
        }
    }

    void Sight()
    {
        if (EM.isDead) return;
        Collider[] cols = Physics.OverlapSphere(transform.position, fov, targetLayer);

        if (cols.Length > 0) // Follow Enmey
        {
            if (EM.target == null)
                EM.target = cols[0].gameObject; 
        } 
        else                // GoBack Node
        {
            if (EM.target != null)
                EM.target = null;
        }
    }


    void Move(Transform destination)
    {
        if (EM.isDead) return;
        if (navMesh == null) return;

        navMesh.SetDestination(destination.position);
        gameObject.transform.LookAt(destination.position);
    }
}
