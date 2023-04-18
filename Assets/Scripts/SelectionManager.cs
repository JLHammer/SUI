using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectionManager : MonoBehaviour
{
    public IXRInteractable Interactable { get; set; }
    [SerializeField] private GameObject RotatePrefab;
    [SerializeField] private GameObject TranslatePrefab;
    private GameObject spawnedRotate, spawnedTranslate;
    
    public void SetInteractable(SelectEnterEventArgs args)
    {
        Interactable = args.interactableObject;
    }
    
}
