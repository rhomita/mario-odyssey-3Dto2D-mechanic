using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchArea2D : MonoBehaviour
{
    [SerializeField] private Transform _planeTransform;
    
    [Header("Teleport")]
    [SerializeField] private bool _teleport;
    [SerializeField] private Vector3 _teleportPosition;
    
    private void OnTriggerExit(Collider _collider)
    {
        if (_collider.TryGetComponent(out PlayerSwitcher playerSwitcher))
        {
            if (!playerSwitcher.Is3D) return;
            playerSwitcher.transform.rotation = Quaternion.LookRotation(_planeTransform.forward);
            playerSwitcher.Deactivate();

            Vector3 raycastOrigin = _teleport ? _teleportPosition : playerSwitcher.transform.position; 
            if (Physics.Raycast(raycastOrigin, -playerSwitcher.transform.forward, 
                out RaycastHit hitPlayer))
            {
                playerSwitcher.transform.position = hitPlayer.point + playerSwitcher.transform.forward * 0.0035f;
            }
            playerSwitcher.Enable2D();
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
        Gizmos.DrawWireCube(_planeTransform.position, _planeTransform.position * .1f);
        Gizmos.DrawLine(_planeTransform.position, _planeTransform.position + _planeTransform.forward);
    }
}
