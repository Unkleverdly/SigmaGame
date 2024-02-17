using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public int EnemyCount { get; private set; }
    public float BulletSpeed => initBulletSpeed * incrementedSpeed;
    public float BulletShootSpeed => bulletShootingSpeed / incrementedSpeed;
    public bool Win { get; private set; }

    private float incrementedSpeed = 1;

    private List<IDamageble> damagebles;
    private List<Enemy> enemies;
    private List<Box> boxes;

    [SerializeField] private float initBulletSpeed;
    [SerializeField] private float bulletShootingSpeed;
    [Range(0, 1), SerializeField] private float bulletSpeedIncrement;

    public static Game Instance { get; private set; }

    private void Awake()
    {
        damagebles = new();
        boxes = new();
        enemies = new();

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [ContextMenu("Debug/Restart")]
    public void SpeedUpBullets()
    {
        incrementedSpeed *= 1 + bulletSpeedIncrement;
    }

    [ContextMenu("Debug/Restart")]
    public void Restart()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void RegisterEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        RegisterDamageble(enemy);
        EnemyCount++;
    }

    public void RegisterBox(Box box)
    {
        boxes.Add(box);
        RegisterDamageble(box);
    }

    public void DeregisterEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        DeregisterDamageble(enemy);
        EnemyCount--;

        if (EnemyCount == 0)
            Win = true;
    }

    public void DeregisterBox(Box box)
    {
        boxes.Remove(box);
        DeregisterDamageble(box);
    }

    [ContextMenu("Debug/Destroy/Boxes")]
    public void DestroyAllBoxes()
    {
        foreach (var box in boxes.ToArray())
            box.Die();
    }

    [ContextMenu("Debug/Destroy/Enemies")]
    public void DestroyAllEnemies()
    {
        foreach (var enemy in enemies.ToArray())
            enemy.Die();
    }

    private void RegisterDamageble(IDamageble damageble) => damagebles.Add(damageble);
    private void DeregisterDamageble(IDamageble damageble) => damagebles.Remove(damageble);
}
