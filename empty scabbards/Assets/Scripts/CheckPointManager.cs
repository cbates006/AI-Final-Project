using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public PlayerHealth health;
    public List<CheckPointTrigger> CheckPoints { get { return checkPoints; } }
    public CheckPointTrigger CurCheckPoint { get { return checkPoints.Count>0 ? checkPoints[curIndex] : null; } }
    public static CheckPointManager Instance { get { return instance; } }

    List<CheckPointTrigger> checkPoints = new List<CheckPointTrigger>();
    int curIndex = 0;
    static CheckPointManager instance = null;

    void Awake()
    {
        instance = this;

        // find all my check points children
        for (int i = 0; i < transform.childCount; ++i)
        {
            CheckPointTrigger checkPoint = transform.GetChild(i).GetComponent<CheckPointTrigger>();
            checkPoint.onTrigger += OnCheckPointTriggered;
            checkPoints.Add(checkPoint);
            checkPoint.SetIdentity(checkPoints.IndexOf(checkPoint));
        }
    }

    private void Start()
    {
        health.onDeath += OnCharacterDeath;
    }

    void OnCharacterDeath()
    {
        Debug.Log("Checkpoint Manager detecting character death");
        CurCheckPoint.Respawn();
    }


    public void OnCheckPointTriggered(CheckPointTrigger newCheckPoint)
    {
        curIndex = checkPoints.IndexOf(newCheckPoint);
    }

}