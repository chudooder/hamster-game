﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HamsterLocation : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    protected Hamster _hamster;
    private const float SNAP_RADIUS = 2;
    
    [SerializeField] protected Transform _hamsterSpawnLocation;

    public virtual Hamster Hamster
    {
        get
        {
            return _hamster;
        }
        set
        {
            _hamster = value;

            if (_hamster != null)
            {
                _hamster.transform.position = _hamsterSpawnLocation.position;
                _hamster.transform.rotation = _hamsterSpawnLocation.rotation;
            }
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("DRAGGING HAMMY");
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        Vector2 dropPos = Camera.main.ScreenToWorldPoint(eventData.position);
        HamsterLocation next = GetClostestHamsterLocation(dropPos);

        if (next != null)
        {
            next.Hamster = Hamster;
        }
        else
        {
            Hamster.transform.position = _hamsterSpawnLocation.position;
            Hamster.transform.rotation = _hamsterSpawnLocation.rotation;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 dragPos = Camera.main.ScreenToWorldPoint(eventData.position);
        Hamster.transform.position = dragPos;
    }

    protected HamsterLocation GetClostestHamsterLocation(Vector2 pos)
    {
        float bestDist = float.MaxValue;
        HamsterLocation bestLoc = null;
        foreach (var hamLoc in RunManager.Instance.HamsterLocations)
        {
            float dist = Vector2.Distance(pos, hamLoc.transform.position);
            if (dist > SNAP_RADIUS || hamLoc.Hamster != null) continue;

            if (dist < bestDist)
            {
                bestDist = dist;
                bestLoc = hamLoc;
            }
        }

        return bestLoc;
    }
}


public class HamsterWheel : HamsterLocation
{
    public HammyRunPhysics HammyRunPhysics;
    
    public override Hamster Hamster
    {
        get
        {
            return _hamster;
        }
        set
        {
            _hamster = value;

            if (_hamster != null)
            {
                _hamster.transform.parent = HammyRunPhysics.transform;
                _hamster.transform.position = _hamsterSpawnLocation.position;
                _hamster.transform.rotation = _hamsterSpawnLocation.rotation;
            }
        }
    }
}