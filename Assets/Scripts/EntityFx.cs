using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    private SpriteRenderer sr;
    
    [Header("Flash FX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMaterial;
    private Material originalMaterial;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMaterial;
        
        yield return new WaitForSeconds(flashDuration);
        
        sr.material = originalMaterial;
    }

    private void RedColorBlink()
    {
        sr.color = sr.color != Color.white ? Color.white : Color.red;
    }

    private void CancelRedColorBlink()
    {
        CancelInvoke();

        sr.color = Color.white;
    }
}
