using UnityEngine;
using TMPro;

public class Life : MonoBehaviour
{
    [SerializeField] private float life = 3;
    [SerializeField] private TextMeshPro textMeshPro;

    private void UpdateText()
    {
        textMeshPro.text = life.ToString();
    }

    private void Awake()
    {
        UpdateText();
    }

    private void OnValidate()
    {
        UpdateText();
    }

    public void Wound()
    {
        life -= 1;
        if (life <= 0)
        {
            if (TryGetComponent(out IDamageble damageble))
                damageble.Die();

            Destroy(gameObject);
        }
        UpdateText();
    }
}

public interface IDamageble
{
    public void Die();
}
