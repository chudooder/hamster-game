using System.Collections;
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
                _hamster.transform.parent = _hamsterSpawnLocation;
                _hamster.transform.position = _hamsterSpawnLocation.position;
                _hamster.transform.rotation = _hamsterSpawnLocation.rotation;
            }
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (!Hamster) return;
        HamsterCard.CurrentHamster = Hamster;

        Debug.Log("DRAGGING HAMMY");
        Hamster.transform.parent = Camera.main.transform;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (!Hamster) return;

        Vector2 dropPos = Camera.main.ScreenToWorldPoint(eventData.position);
        HamsterLocation next = GetClostestHamsterLocation(dropPos);

        if (next != null)
        {
            next.Hamster = Hamster;
            Hamster = null;
        }
        else
        {
            Hamster = Hamster;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (!Hamster) return;
        Vector2 dragPos = Camera.main.ScreenToWorldPoint(eventData.position);
        Hamster.transform.position = dragPos;
    }

    protected HamsterLocation GetClostestHamsterLocation(Vector2 pos)
    {
        float bestDist = float.MaxValue;
        HamsterLocation bestLoc = null;
        foreach (var hamLoc in RunManager.Instance.PlacableHamsterLocations)
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