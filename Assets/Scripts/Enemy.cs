using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private float speed = 7f;

    private bool dead;

    private void Start()
    {
        Game.Instance.RegisterEnemy(this);
    }

    private void Update()
    {
        transform.position += 0.2f * speed * Time.deltaTime * Vector3.back;
    }

    public void Die()
    {
        if (dead) return;

        dead = true;
        var last = Game.Instance.EnemyCount == 1;

        Game.Instance.DeregisterEnemy(this);
        if (!last)
            Player.Instance.SoundSource.PlayOneShot(ContentManager.Instance.DeathSounds.GetRandom());
        Destroy(gameObject);
    }
}
