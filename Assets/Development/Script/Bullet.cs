using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float upSpeed;
    [SerializeField] private Rigidbody2D rigidbody2D;


    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }
    public void FireBullet(float facing)
    {
        rigidbody2D.AddForce(transform.right * forwardSpeed * facing + transform.up * upSpeed, ForceMode2D.Impulse);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable interf = collision.transform.GetComponent<IDamageable>();
        
        if(interf != null)
        {
            interf.OnDamage(25f);
        }

        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
