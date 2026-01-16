using System;
using UnityEngine;

public sealed class PickaxeSwingAnimator : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float minZ = 30f;
    [SerializeField] private float maxZ = 80f;
    [SerializeField] private float swingSpeed = 6f;

    private bool isSwinging;
    private float time;

    private float Center => (minZ + maxZ) * 0.5f;
    private float Amplitude => (maxZ - minZ) * 0.5f;

    private void OnEnable()
    {
        GameSignals.PickDrag += StartSwing;
        GameSignals.PickDragEnd += EndSwing;
    }

    private void OnDisable()
    {
        GameSignals.PickDrag -= StartSwing;
        GameSignals.PickDragEnd -= EndSwing;
    }

    private void StartSwing()
    {
        isSwinging = true;
        time = 0f;
    }

    private void EndSwing()
    {
        isSwinging = false;
    }

    private void Update()
    {
        if (!isSwinging) return;

        time += Time.deltaTime * swingSpeed;

        float z = Center + Mathf.Sin(time) * Amplitude;
        transform.localRotation = Quaternion.Euler(0f, 0f, z);
    }
}