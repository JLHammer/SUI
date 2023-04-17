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
    private GameObject spawnedProxy;
    private IXRHoverInteractable currentSelection;
    [SerializeField] private Transform controllerTransform;
    
    private void DespawnProxy()
    {
        if (spawnedProxy != null)
        {
            Destroy(spawnedProxy);
            spawnedProxy = null;
        }

    }

    private void Update()
    {
        if (!IsGripping())
        {
            if (currentSelection != null)
            {
                //currentSelection.transform.gameObject;
                DespawnProxy();
                currentSelection = null;
            }
            return;
        }
        if (FindInteractable(ref currentSelection) && currentSelection != null)
        {
            //currentSelection.transform.gameObject;
            SetCurrentRotation();
            SpawnProxy();
        }
    }

    private void SetCurrentRotation()
    {
        //var difference = Quaternion.Inverse(spawnedProxy.transform.rotation) * controllerTransform.rotation;
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
    }
}
