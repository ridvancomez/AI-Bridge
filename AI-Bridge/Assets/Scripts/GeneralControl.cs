using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public class GeneralControl : MonoBehaviour
{
    #region Durumlar için gerekenler

    [SerializeField] private StateType currentState;

    //Durumlar StateControl class ýnda o class ile bu class arasýndaki iletiþimi saðlayan üç prop
    public static bool goOnPatrol { get; private set; }
    public static bool goOnChase { get; private set; }
    public static bool goOnAttack { get; private set; }


    #endregion

    #region Chase durumu için gerekenler
    private Transform PlayerLocation; //player ýn konumu
    private bool notice; //player generalin scop una girdi mi
    private float remainingTime; // attack durumuna geçmesi için kalan süre
    [SerializeField] TextMeshProUGUI ekranaYazilanSure;
    #endregion

    #region Patrol için gerekenler
    [SerializeField] GameObject[] wayPoint;
    private int wayPointIndex; //way point in indisini tutuyor
    #endregion

    #region gameObject için gerekenler
    private NavMeshAgent agent;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentState = StateControl.stateType;
        goOnPatrol = true; //önce devriye baþlasýn
        goOnChase = false;
        goOnAttack = false;

        PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        notice = false; //general player i fark etmemsi durumu için bayrak yapýsý
        remainingTime = 2;
        ekranaYazilanSure.text = ""; //ekrana bir þey çýkmasýn

        agent = GetComponent<NavMeshAgent>();
        wayPointIndex = 0;       
    }   

    // Update is called once per frame
    void Update()
    {
        if (goOnPatrol)
            Patrol();
        else if (goOnChase)
            Chase();
        else if (goOnAttack)
            Attack();
    }

    private void Patrol()
    {
        if (agent.remainingDistance <= 2)
        {

            wayPointIndex++;
            wayPointIndex %= wayPoint.Length;

            agent.SetDestination(wayPoint[wayPointIndex].transform.position);

        }
        if (notice)
        {
            agent.ResetPath(); //gitmesi gereken yeri unutur
            goOnPatrol = false;
            goOnChase = true;
            goOnAttack = false;
        }
    }

    private void Chase()
    {

        ekranaYazilanSure.text = "Köprülerin açýlmasý için kalan süre: " + remainingTime.ToString("0.00");


        agent.SetDestination(PlayerLocation.position);

        if (agent.remainingDistance <= 1)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                ekranaYazilanSure.text = "Köprüler açýldý";

                goOnPatrol = false;
                goOnChase = false;
                goOnAttack = true;
            }
        }
        else
        {
            remainingTime = 2;
        }
    }

    private void Attack()
    {
        agent.SetDestination(PlayerLocation.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            notice = true;
        }
    }
}
