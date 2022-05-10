using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankControl : MonoBehaviour
{
    #region Patrol için gerekenler
    [SerializeField]
    GameObject[] wayPoint;
    private int wayPointIndex;
    #endregion

    #region Chase için gerekenler
    [SerializeField]
    private GameObject tower;
    #endregion

    #region GameObject için gerekenler
    private NavMeshAgent agent;
    private StateType currentState;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wayPointIndex = 0;
        StartCoroutine("WhicState"); // update de performans düþer
        
    }
    private IEnumerator WhicState()
    {
        while (true)
        {
            currentState = StateControl.stateType;

            switch (currentState)
            {
                case StateType.Patrol:
                    Patrol();
                    break;
                case StateType.Chase:
                    Chase();
                    break;
                case StateType.Attack:
                    Attack();
                    break;
            }
            yield return new WaitForSeconds(1);

        }
    }




    

    void Patrol()
    {
        if (agent.remainingDistance <= 2)
        {

            wayPointIndex++;
            wayPointIndex %= wayPoint.Length;

            agent.SetDestination(wayPoint[wayPointIndex].transform.position);

        }
    }

    void Chase()
    {
        agent.SetDestination(tower.transform.position);
    }

    private void Attack()
    {
        agent.SetDestination(tower.transform.position);

        if(Vector3.Distance(tower.transform.position, transform.position) < 2)
        {
            tower.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
