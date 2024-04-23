using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    public int _id;
    protected bool triggered = false;


    public delegate void TriggeredDelegate(CheckPointTrigger t);
    public event TriggeredDelegate onTrigger;

    public delegate void RespawnDelegate();
    public event RespawnDelegate onRespawn;


    void Start()
    {
    }


    public void SetIdentity(int id)
    {
        _id = id;
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")
            && !triggered)
        {
            triggered = true;
            CheckPointPassed(_id);
        }
    }
    void CheckPointPassed (int id)
    {
        Debug.Log("Passed checkpoint " + id);
        onTrigger.Invoke(this);
    }


    public void Respawn()
    {
        Debug.Log("Respawning checkpoint " + _id);
    }

}
