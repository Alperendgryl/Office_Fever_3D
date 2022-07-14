using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> jobList = new List<GameObject>();
    [SerializeField] private GameObject jobPrefab;
    [SerializeField] private Transform collectPos;

    private float paperDiff_Y = 0.25f;

    int playerPaperLimit = 15; //player can take 15 papers at a time

    private void OnEnable()
    {
        EventManager.onCollectArea += getJob;
        EventManager.onPaperGive += giveJobToEmployee;
    }

    private void OnDisable()
    {
        EventManager.onCollectArea -= getJob;
        EventManager.onPaperGive -= giveJobToEmployee;
    }

    void getJob()
    {
        if (jobList.Count < playerPaperLimit && JobManager.canTakeJob)
        {
            GameObject temp = Instantiate(jobPrefab, new Vector3(collectPos.position.x, collectPos.position.y + paperDiff_Y, collectPos.position.z), Quaternion.identity);
            temp.transform.parent = collectPos; // add papers as child of CollectPos to move with player.
            paperDiff_Y += 0.1f; // create space between papers
            jobList.Add(temp);

            if (EventManager.jobManager != null)
            {
                EventManager.jobManager.pop();
            }
        }
    }

    public void giveJobToEmployee()
    {
        if (jobList.Count > 0 && EmployeeManager.canTakeJob)
        {
            EventManager.employeeManager.GetJob();
            pop();
        }
    }

    private void pop()
    {
        if (jobList.Count > 0)
        {
            Destroy(jobList[jobList.Count - 1]);
            jobList.RemoveAt(jobList.Count - 1);
            paperDiff_Y -= 0.1f;
        }
    }
}
