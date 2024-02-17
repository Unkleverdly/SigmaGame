using UnityEngine;

public class TakeGun : MonoBehaviour
{
    [SerializeField] private float speed = 150;

    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player p))
        {
            p.GetGun();
            Destroy(gameObject);
        }
    }
}
