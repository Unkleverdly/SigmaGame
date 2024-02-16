using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public int EnemyCount { get => enemyCount; set => enemyCount = value; }
    public float BulletSpeed => initBulletSpeed * incrementedSpeed;
    public float BulletShootSpeed => bulletShootingSpeed / incrementedSpeed;
    public bool Win { get => win; set => win = value; }

    private float incrementedSpeed = 1;

    [SerializeField] private int enemyCount;
    [SerializeField] private bool win;

    [SerializeField] private float initBulletSpeed;
    [SerializeField] private float bulletShootingSpeed;
    [Range(0, 1), SerializeField] private float bulletSpeedIncrement;

    public static Game Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SpeedUpBullets()
    {
        incrementedSpeed *= 1 + bulletSpeedIncrement;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
