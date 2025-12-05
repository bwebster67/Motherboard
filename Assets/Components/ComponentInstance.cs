using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ComponentInstance : MonoBehaviour
{
    public ComponentUIData UIData;
    protected int currentLevel = 1;
    public Vector2Int gridPosition; // top-left aligned
    public Vector2Int anchorPosition; // top-left 

    // Context
    protected PlayerComponentManager playerComponentManager;
    protected Transform playerTransform;

    protected virtual void Awake()
    {
        if (playerComponentManager == null) { playerComponentManager = FindAnyObjectByType<PlayerComponentManager>(); }
        if (playerTransform == null) { playerTransform = GameObject.FindGameObjectWithTag("Player").transform; }
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }


}
