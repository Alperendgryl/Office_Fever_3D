using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeManager : MonoBehaviour
{
    [Header("Paper")]
    public List<GameObject> jobList = new List<GameObject>();
    [SerializeField] private Transform deskPos;
    [SerializeField] private GameObject jobPrefab;
    private float paperDiff_Y = 0.25f;

    [Header("Money")]
    [SerializeField] private List<GameObject> moneyList = new List<GameObject>();
    [SerializeField] private Transform moneyPos;
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private float moneyDiff_Y;
    [SerializeField] private float timeBetweenMoney;
    [SerializeField] private float stackCount;
    [SerializeField] private int moneyCounter = 1;
    [SerializeField] private float moneyDiff_X = 0f;

    [SerializeField] private int employeeJobLimit;

    public static bool canTakeJob = true;

    private void Update()
    {
        if (jobList.Count < employeeJobLimit) canTakeJob = true;
        else canTakeJob = false;
    }
    private void Start()
    {
        StartCoroutine(Money());
    }

    IEnumerator Money()
    {
        while (true)
        {
            if (jobList.Count > 0)
            {
                GameObject temp = Instantiate(moneyPrefab, new Vector3(moneyPos.position.x + moneyDiff_X, moneyPos.position.y + moneyDiff_Y, moneyPos.position.z), Quaternion.identity);
                moneyDiff_Y += 0.1f;
                pop();
                if (moneyCounter % stackCount == 0)
                {
                    moneyDiff_X += 1f; // move to the right
                    moneyDiff_Y = 0f; //reset Y position.
                    moneyCounter = 0;
                }
                moneyList.Add(temp);
                moneyCounter++;
            }
            for (int i = 0; i < moneyList.Count; i++)
            {
                if (moneyList[i] == null) moneyList.RemoveAt(i);
            }
            yield return new WaitForSeconds(timeBetweenMoney);
        }
    }
    public void GetJob()
    {
        if (canTakeJob && jobList.Count <= employeeJobLimit)
        {
            GameObject temp = Instantiate(jobPrefab, new Vector3(deskPos.position.x, deskPos.position.y + paperDiff_Y, deskPos.position.z), Quaternion.identity);
            paperDiff_Y += 0.1f; // create space between papers
            jobList.Add(temp);
        }
    }
    private void pop()
    {
        if (jobList.Count > 0)
        {
            Destroy(jobList[jobList.Count - 1]);
            jobList.RemoveAt(jobList.Count - 1);
            if (paperDiff_Y > 0) paperDiff_Y -= 0.1f;
            if (moneyDiff_X > 4) moneyDiff_X = 0;
        }
    }
}
