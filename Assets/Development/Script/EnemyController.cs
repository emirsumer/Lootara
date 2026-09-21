using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,IDamageable
{
    [SerializeField] private List<Transform> patrolPoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private ParticleSystem particle;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private bool _isFirstTransform = true;
    private float _health = 100f;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        int index = _isFirstTransform ? 1 : 0; //true ise 1 false ise 0
        Vector3 destination = patrolPoint[index].position;
        Vector3 direction = destination - transform.position;
        direction.Normalize();

        _rigidbody2D.linearVelocity = new Vector2(direction.x * moveSpeed, _rigidbody2D.linearVelocity.y);

        if (Mathf.Abs(transform.position.x - destination.x) < 0.1f)
        {
            _isFirstTransform = !_isFirstTransform;
        }
    }
    public void OnDamage(float damageAmount)
    {
        _health -= damageAmount;
        _animator.SetTrigger("Hit");
        SoundManager.Instance.PlaySFX(SoundType.HitEnemy);
        if (_health <= 0)
        {
            particle.Play();
            particle.transform.parent = null;
            particle.transform.localScale = Vector3.one;
            SoundManager.Instance.PlaySFX(SoundType.Explosion);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable interf = collision.transform.GetComponent<IDamageable>();
        CharacterController characterController = collision.transform.GetComponent<CharacterController>();

        if (interf != null)
        {
            interf.OnDamage(25f);
        }

        if (characterController != null)
        {
            characterController.PushBack(collision.contacts[0].normal);
        }
    }
}
