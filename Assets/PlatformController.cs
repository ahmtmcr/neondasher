using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    
    [SerializeField] Transform startPoint;
    [SerializeField] Transform TargetPoint;
    [SerializeField] Transform Platform;
    
    public bool _effector = false;
    public float _platformSpeed = 0.5f;
    public float _platformChangePositionTimer = 6f;
    public float _platformChangePositionTimer2 = 12f;
    
    private PlatformStartEffector platformStartEffector;
    private float  platformChangePositionTimerCount = 0f;



    
    
    
    void Start()
    {
        if(_effector == true)
        {
            platformStartEffector = GetComponentInChildren<PlatformStartEffector>();
        }
        Platform.position = startPoint.position;       
    }
    void FixedUpdate()
    {
        
        
        if(_effector == false)
        {
           LoopBetweenPlatformStartAndTarget();
        }
        else
        {
            if(platformStartEffector.isEffected)
            {
                StartToTarget();
            }
            else
            {
                TargetToStart();
            }
        }
      
    }
    
    void StartToTarget()
    {
        Platform.position = Vector3.Lerp(Platform.position, TargetPoint.position, _platformSpeed * Time.deltaTime);
    }
    void TargetToStart()
    {
        Platform.position = Vector3.Lerp(Platform.position, startPoint.position, _platformSpeed * Time.deltaTime);
    }
    void LoopBetweenPlatformStartAndTarget()
    {
        if(platformChangePositionTimerCount < _platformChangePositionTimer)
        {
            StartToTarget();
        }
        else if(platformChangePositionTimerCount < _platformChangePositionTimer2)
        {
            TargetToStart();;
        }
        if(platformChangePositionTimerCount > _platformChangePositionTimer2)
        {
            platformChangePositionTimerCount = 0f;
        }
        
        platformChangePositionTimerCount += Time.deltaTime;
    }
}
