using UnityEngine;

public class Enemies : MonoBehaviour, IDamageble
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioSource source;
    // private Rigidbody rb;
    // private Vector3 moveVector;
    private bool dead;

    private void Start()
    {
        Game.Instance.EnemyCount++;
        // rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position = transform.position + 0.2f * speed * Time.deltaTime * Vector3.back;
    }

    public void Die()
    {
        if (dead)
            return;
        dead = true;
        Game.Instance.EnemyCount--;
        Destroy(gameObject);
        source.Stop();
        source.PlayOneShot(sounds.GetRandom());
    }
}
