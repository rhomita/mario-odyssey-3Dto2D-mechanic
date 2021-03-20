using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchArea3D : MonoBehaviour
{
    [SerializeField] private Transform _teleportTo;
    private void OnTriggerExit(Collider _collider)
    {
        // TODO: Check that the enter side is different than the exit.
        if (_collider.TryGetComponent(out PlayerSwitcher playerSwitcher))
        {
            if (playerSwitcher.Is3D) return;
            if (_teleportTo != null)
            {
                playerSwitcher.Deactivate();
                playerSwitcher.transform.position = _teleportTo.position;
            }
            playerSwitcher.Enable3D();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
