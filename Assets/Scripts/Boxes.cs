using UnityEngine;

public class Boxes : MonoBehaviour, IDamageble
{
    [SerializeField] private AudioSource BackMus;

    public void Die()
    {
        BackMus.Play();
        Game.Instance.SpeedUpBullets();
        Destroy(gameObject);
    }
}
