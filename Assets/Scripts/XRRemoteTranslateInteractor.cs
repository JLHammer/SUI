using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class XRRemoteTranslateInteractor : XRBaseControllerInteractor
{
    [SerializeField] private AudioClip OnHoverEnteredClip;
    [SerializeField] private AudioClip OnHoverExitClip;

    [SerializeField] private AudioClip OnSelectEnteredClip;
    [SerializeField] private AudioClip OnSelectExitClip;
    
    [SerializeField] private AudioSource audioSource;
    
    [SerializeField] private GameObject ProxyPrefab;
    [SerializeField] private Transform controllerTransform;
    [SerializeField] private GameObject spawnedProxy;
    //[SerializeField] private Vector3 ProxyPosition;
    [SerializeField] private IXRHoverInteractable currentSelection;
    private IXRHoverInteractable currentHoverTarget;
    public float Speed = 4f;

    private void Update()
    {

        if (!IsGripping())
        {
            LookForHoverInteractable(ref currentHoverTarget);
            if (currentSelection != null)
            {
                audioSource.PlayOneShot(OnSelectExitClip);
                //currentSelection.transform.gameObject;
                DespawnProxy();
                currentSelection = null;
            }
            return;
        }
        if (FindInteractable(ref currentSelection) && currentSelection != null)
        {
            //currentSelection.transform.gameObject;
            if (spawnedProxy == null)
            {
                SetCurrentRotation();
                SpawnProxy();
            }
            //SetCurrentRotation();
            //SpawnProxy();
        }
        //spawnedProxy.transform.position = controllerTransform.position;
        if (currentSelection != null)
        {
            if (Vector3.Distance(spawnedProxy.transform.position, controllerTransform.position) > 0.1f)
            {
                currentSelection.transform.position -= (spawnedProxy.transform.position - controllerTransform.position) * (Time.deltaTime * Speed);
            }
        }
    }

    private void LookForHoverInteractable(ref IXRHoverInteractable interactable)
    {
        Ray ray = new Ray
        {
            origin = transform.position,
            direction = transform.forward
        };
        
        if (!Physics.Raycast(ray, out RaycastHit raycastHit,30, LayerMask.GetMask(new[] { "Default", "UI" }), QueryTriggerInteraction.Collide))
        {
            if (currentHoverTarget == null) return;
            audioSource.PlayOneShot(OnHoverExitClip);
            currentHoverTarget = null;
            return;
        }
        var correctHit = raycastHit.collider.gameObject.GetComponentInChildren<IXRHoverInteractable>();
        if (correctHit == null)
        {
            if (currentHoverTarget == null) return;
            audioSource.PlayOneShot(OnHoverExitClip);
            currentHoverTarget = null;
            return;
        }
        if (interactable?.transform.gameObject == correctHit.transform.gameObject)
        {
            return;
        }
        print("target begins hover");
        currentHoverTarget = correctHit;
        audioSource.PlayOneShot(OnHoverEnteredClip);
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

        if (currentSelection == null)
        {
            audioSource.PlayOneShot(OnSelectEnteredClip);
            interactable = correctHit;
        }
        //interactable = correctHit;
        return true;
    }

    private void SpawnProxy()
    {
        spawnedProxy = Instantiate(ProxyPrefab);
        spawnedProxy.transform.position = controllerTransform.position;
        
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
