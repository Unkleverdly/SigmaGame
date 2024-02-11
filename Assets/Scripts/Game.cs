using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public int EnemyCount { get => enemyCount; set => enemyCount = value; }
    public float BulletSpeed => initBulletSpeed + incrementedSpeed;
    public float BulletShootSpeed => bulletShootingSpeed;
    public bool Win { get => win; set => win = value; }

    [SerializeField] private float incrementedSpeed;
    [SerializeField] private int enemyCount;
    [SerializeField] private bool win;

    [SerializeField] private float initBulletSpeed;
    [SerializeField] private float bulletSpeedIncrement;
    [SerializeField] private float bulletShootingSpeed;

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
        incrementedSpeed += bulletSpeedIncrement;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
