using UnityEngine;

public class Hostage : MonoBehaviour
{
    [SerializeField] private float speed = 150;

    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
