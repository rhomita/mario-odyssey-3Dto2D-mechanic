using UnityEngine;

public class PlayerSwitchArea3D : MonoBehaviour
{
    [Header("Teleport")]
    [SerializeField] private bool _teleport;
    [SerializeField] private Vector3 _teleportPosition;

    private void OnTriggerExit(Collider _collider)
    {
        // TODO: Check that the enter side is different than the exit.
        if (_collider.TryGetComponent(out PlayerSwitcher playerSwitcher))
        {
            if (playerSwitcher.Is3D) return;
            if (_teleport)
            {
                playerSwitcher.Deactivate();
                playerSwitcher.transform.position = _teleportPosition;
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
