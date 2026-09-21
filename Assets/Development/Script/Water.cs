using UnityEngine;
public class Water : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterController character))
        {
            SoundManager.Instance.PlaySFX(SoundType.WaterSplash);
            character.OnDamage(100f);
        }
    }
}
