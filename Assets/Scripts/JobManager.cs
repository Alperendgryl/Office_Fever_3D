using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> paperList = new List<GameObject>();
    [SerializeField] private GameObject paperPrefab;
    [SerializeField] private Transform exitPoint;

    [SerializeField] private float TimeBetweenPapers;
    [SerializeField] private float stackCount;
    [SerializeField] private float TotalPaper;

    [SerializeField] private float paperDiff_Y = 0.25f;
    [SerializeField] private float paperDiff_X = 0f;
    [SerializeField] private int jobCounter = 1;

    private bool isWorking;
    public static bool canTakeJob;

    private void Update()
    {
        if (paperList.Count == 0) canTakeJob = false;
        else canTakeJob = true;
    }
    private void Start()
    {
        StartCoroutine(createJob());
    }

    IEnumerator createJob()
    {
        while (true)
        {
            if (paperList.Count < TotalPaper)
            {
                if (isWorking)
                {
                    GameObject tempJob = Instantiate(paperPrefab, new Vector3(exitPoint.position.x + paperDiff_X, exitPoint.position.y + paperDiff_Y, exitPoint.position.z), Quaternion.identity);
                    paperDiff_Y += 0.1f; // create space between papers

                    if (jobCounter % stackCount == 0)
                    {
                        paperDiff_X += 0.5f; // move to the right
                        paperDiff_Y = 0.25f; //reset Y position. != 0 because it instantiates the papers under ground.
                        jobCounter = 0;
                    }
                    if (paperList.Count >= TotalPaper)
                    {
                        isWorking = false;
                    }

                    if (paperList.Count == 0) canTakeJob = false;

                    paperList.Add(tempJob);
                    jobCounter++;
                }
                else if (paperList.Count < TotalPaper)
                {
                    isWorking = true;
                }
            }
            yield return new WaitForSeconds(TimeBetweenPapers);
        }
    }
    public void pop()
    {
        if (paperList.Count > 0)
        {
            Destroy(paperList[paperList.Count - 1]);
            paperList.RemoveAt(paperList.Count - 1);
            if (paperDiff_Y > 0) paperDiff_Y -= 0.1f;
            if (paperDiff_X > 3) paperDiff_X = 0;
        }
    }
}
