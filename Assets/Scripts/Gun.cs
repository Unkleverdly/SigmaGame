using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;

    private float timeLeft;

    private void Start()
    {
        timeLeft = Game.Instance.BulletShootSpeed;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * Game.Instance.BulletSpeed;
            timeLeft = Game.Instance.BulletShootSpeed;
        }
    }
}
