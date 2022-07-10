using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    [SerializeField] float collectSpeed = 0.15f;
    public delegate void OnCollectArea();
    public static event OnCollectArea onCollectArea;
    public static JobManager jobManager;

    public delegate void OnDeskArea();
    public static event OnDeskArea onPaperGive;
    public static EmployeeManager employeeManager;

    public delegate void OnMoneyArea();
    public static event OnMoneyArea onMoneyCollected;
    public static BuyArea areaToBuy;

    public delegate void OnBuyArea();
    public static event OnBuyArea onBuyDesk;

    bool isCollecting;
    bool isGiving;

    void Start()
    {
        StartCoroutine(Collect());
    }
    IEnumerator Collect()
    {
        while (true)
        {
            if (isCollecting)
            {
                onCollectArea();
            }
            if (isGiving)
            {
                onPaperGive();
            }
            yield return new WaitForSeconds(collectSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            onMoneyCollected();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            isCollecting = true;
            jobManager = other.gameObject.GetComponent<JobManager>();
        }
        if (other.gameObject.CompareTag("WorkArea"))
        {
            isGiving = true;
            employeeManager = other.gameObject.GetComponent<EmployeeManager>();
        }
        if (other.gameObject.CompareTag("BuyArea"))
        {
            onBuyDesk();
            areaToBuy = other.gameObject.GetComponent<BuyArea>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            isCollecting = false;
            jobManager = null;
        }
        if (other.gameObject.CompareTag("WorkArea"))
        {
            isGiving = false;
            employeeManager = null;
        }
        if (other.gameObject.CompareTag("BuyArea"))
        {
            areaToBuy = null;
        }
    }
}
