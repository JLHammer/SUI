using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class XRRemoteRotateInteractor : XRBaseControllerInteractor
{
    [SerializeField] private GameObject ProxyPrefab;
    [SerializeField] private Transform controllerTransform;
    private GameObject spawnedProxy;
    public IXRHoverInteractable currentSelection;
    public Quaternion rotationDifference;
    public GameObject rotationStorage;
    //public GameObject CurrentlySelectedObject;

    private void Start()
    {
        rotationStorage = new GameObject("Rotation Storage");
    }

    private void Update()
    {
        if (IsGripping())
        {
            if (spawnedProxy == null)
            {
                SpawnProxy();
            }
            SetCurrentRotation();
            currentSelection.transform.rotation = rotationDifference;
            //currentSelection.transform.rotation = rotationStorage.transform.rotation * controllerTransform.rotation;
        }
        if (!IsGripping())
        {
            DespawnProxy();
            rotationStorage.transform.rotation = currentSelection.transform.rotation;
        }

        //if (!IsGripping())
        //{
        //    if (currentSelection != null)
        //    {
        //        //currentSelection.transform.gameObject;
        //        DespawnProxy();
        //        currentSelection = null;
        //    }
        //    return;
        //}
        //if (FindInteractable(ref currentSelection) && currentSelection != null)
        //{
        //    //currentSelection.transform.gameObject;
        //    SetCurrentRotation();
        //    SpawnProxy();
        //}
        //spawnedProxy.transform.position = controllerTransform.position;
    }

    private void SetCurrentRotation()
    {
        rotationDifference = Quaternion.Inverse(spawnedProxy.transform.rotation) * controllerTransform.rotation * rotationStorage.transform.rotation;
        //rotationDifference = 
    }

    public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {

    }

    public override void PreprocessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {

    }
    
    private bool IsGripping()
    {
        var ctrl = xrController as ActionBasedController;
        if (ctrl != null)
        {
            return xrController.selectInteractionState.active;
        }
        return false;
    }

    private bool FindInteractable(ref IXRHoverInteractable interactable)
    {
        Ray ray = new Ray
        {
            origin = transform.position,
            direction = transform.forward
        };
        
        if (!Physics.Raycast(ray, out RaycastHit raycastHit,30, LayerMask.GetMask(new[] { "Default", "UI" }), QueryTriggerInteraction.Collide))
        {
            return false;
        }
        var correctHit = raycastHit.collider.gameObject.GetComponentInChildren<IXRHoverInteractable>();
        if (correctHit == null)
        {
            return false;
        }

        if (interactable?.transform.gameObject == correctHit.transform.gameObject)
        {
            return false;
        }
        interactable = correctHit;
        return true;
    }

    private void SpawnProxy()
    {
        spawnedProxy = Instantiate(ProxyPrefab);
        spawnedProxy.transform.position = controllerTransform.position;
        spawnedProxy.transform.rotation = controllerTransform.rotation;
    }
    
    private void DespawnProxy()
    {
        if (spawnedProxy != null)
        {
            Destroy(spawnedProxy);
            spawnedProxy = null;
        }

    }
}
