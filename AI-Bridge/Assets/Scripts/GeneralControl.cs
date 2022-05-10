using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public class GeneralControl : MonoBehaviour
{
    #region Durumlar i�in gerekenler

    [SerializeField] private StateType currentState;

    //Durumlar StateControl class �nda o class ile bu class aras�ndaki ileti�imi sa�layan �� prop
    public static bool goOnPatrol { get; private set; }
    public static bool goOnChase { get; private set; }
    public static bool goOnAttack { get; private set; }


    #endregion

    #region Chase durumu i�in gerekenler
    private Transform PlayerLocation; //player �n konumu
    private bool notice; //player generalin scop una girdi mi
    private float remainingTime; // attack durumuna ge�mesi i�in kalan s�re
    [SerializeField] TextMeshProUGUI ekranaYazilanSure;
    #endregion

    #region Patrol i�in gerekenler
    [SerializeField] GameObject[] wayPoint;
    private int wayPointIndex; //way point in indisini tutuyor
    #endregion

    #region gameObject i�in gerekenler
    private NavMeshAgent agent;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentState = StateControl.stateType;
        goOnPatrol = true; //�nce devriye ba�las�n
        goOnChase = false;
        goOnAttack = false;

        PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        notice = false; //general player i fark etmemsi durumu i�in bayrak yap�s�
        remainingTime = 2;
        ekranaYazilanSure.text = ""; //ekrana bir �ey ��kmas�n

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

        ekranaYazilanSure.text = "K�pr�lerin a��lmas� i�in kalan s�re: " + remainingTime.ToString("0.00");


        agent.SetDestination(PlayerLocation.position);

        if (agent.remainingDistance <= 1)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                ekranaYazilanSure.text = "K�pr�ler a��ld�";

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
