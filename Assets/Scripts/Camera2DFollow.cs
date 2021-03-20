using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;

    protected void Update()
    {
        Vector3 position = _player.position + _player.forward * 10;
        Quaternion rotation = Quaternion.LookRotation(-_player.forward);
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 10);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10);
    }
}
