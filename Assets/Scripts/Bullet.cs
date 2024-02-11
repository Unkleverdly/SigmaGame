using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 4f;
    [SerializeField] private float lifeDistance = 4f;

    private float startTime;
    private Vector3 startPosition;

    private void Awake()
    {
        startTime = Time.time;
        startPosition = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, startPosition) > lifeDistance || Time.time > startTime + lifeTime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Life>(out var life))
            life.Wound();

        Destroy(gameObject);
    }
}
