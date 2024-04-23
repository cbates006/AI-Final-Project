using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class should be attached to every object that needs to be reset
/// when the game is resumed after player death, etc.
/// CheckPointTrigger must be set manually for each entity.
/// </summary>
public class RespawnController : MonoBehaviour
{
    public CheckPointTrigger respawningCheckPoint = null;


    Vector3 initialPosition;

    /// <summary>
    /// Set initial position and subscribe to respawn event.
    /// Components and children will need to be notified and reset.
    /// One way is to add a respawn event here and inovke it in OnRespawn.
    /// </summary>
    void Awake()
    {
        initialPosition = transform.position;
        if (respawningCheckPoint == null)
        {
            Debug.LogWarning("You forgot to assign a checkpoint to enemy " + gameObject.ToString());
        }
        respawningCheckPoint.onRespawn += OnRespawn;
    }
    public void OnRespawn()
    {
        transform.position = initialPosition;
    }
}
