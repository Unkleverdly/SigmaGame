using UnityEngine;

public class Box : MonoBehaviour, IDamageble
{
    private void Start()
    {
        Game.Instance.RegisterBox(this);
    }

    public void Die()
    {
        Game.Instance.SpeedUpBullets();
        Game.Instance.DeregisterBox(this);
        Player.Instance.SoundSource.PlayOneShot(ContentManager.Instance.PickUpSounds.GetRandom());
        Destroy(gameObject);
    }
}
