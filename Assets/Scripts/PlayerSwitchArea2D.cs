using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchArea2D : MonoBehaviour
{
    [SerializeField] private Transform _planeTransform;
    [SerializeField] private bool _teleport;
    
    private void OnTriggerExit(Collider _collider)
    {
        if (_collider.TryGetComponent(out PlayerSwitcher playerSwitcher))
        {
            if (!playerSwitcher.Is3D) return;

            playerSwitcher.Deactivate();

            if (Physics.Raycast(_planeTransform.position, -_planeTransform.forward, out RaycastHit hit))
            {
                if (_teleport)
                {
                    playerSwitcher.transform.position = hit.point + (_planeTransform.forward * .035f);
                }
                else
                {
                    if (Physics.Raycast(playerSwitcher.transform.position, -_planeTransform.forward, out RaycastHit hitPlayer))
                    {
                        playerSwitcher.transform.position = hitPlayer.point + (_planeTransform.forward * .035f);
                    }        
                }

                playerSwitcher.transform.rotation = Quaternion.LookRotation(hit.normal);
            }
            else
            {
                Debug.LogError("Not Found");
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
