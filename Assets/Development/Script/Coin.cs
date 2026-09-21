using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CharacterController>())
        {
            particle.Play();
            particle.transform.parent = null;
            SoundManager.Instance.PlaySFX(SoundType.Coin);
            UIManager.Instance.CollectCoin(1);
            Destroy(gameObject);
        }
    }
}
