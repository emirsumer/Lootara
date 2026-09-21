using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour,IDamageable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject bulletPrefab;

    private int _maxJumpCount = 1;
    private int _jumpCount;
    private bool _isKitted;
    private float _health = 100f;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        UIManager.Instance.InitHealth(_health);
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleFire();
    }

    private void HandleMovement()
    {
        float horizontalRaw = Input.GetAxisRaw("Horizontal"); 
        if (!_isKitted)
        {
            _rigidbody2D.linearVelocity = new Vector2(horizontalRaw * moveSpeed, _rigidbody2D.linearVelocity.y);
        }

        _animator.SetFloat("MoveSpeed", Mathf.Abs(horizontalRaw));
        if (horizontalRaw != 0)
        {
            transform.localScale = new Vector2(horizontalRaw, 1);
        }
    }

    private void HandleJump() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_jumpCount < _maxJumpCount)
            {
                _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, 0); //önceki dikey hýzý sýfýrlar daha iyi zýplama ölçüsü için
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                SoundManager.Instance.PlaySFX(SoundType.Jump);
                _animator.SetTrigger("Jump");
                _jumpCount++;
            }
        }
    }

    private void HandleFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletObject = Instantiate(bulletPrefab, spawnTransform.position, Quaternion.identity);
            bulletObject.GetComponent<Bullet>().FireBullet(transform.localScale.x);
            SoundManager.Instance.PlaySFX(SoundType.Fire);
        }
    }

    public void PushBack(Vector2 forceDirection)
    {
        _rigidbody2D.AddForce(forceDirection * -10f, ForceMode2D.Impulse);
        if (!_isKitted)
        {
            StartCoroutine(WaitForPushBack());
        }
    }


    private IEnumerator WaitForPushBack()
    {
        _isKitted = true;
        yield return new WaitForSeconds(0.75f);
        _isKitted = false;
        _rigidbody2D.linearVelocity = new Vector2(0, _rigidbody2D.linearVelocity.y);

    }

    public void OnDamage(float damageAmount)
    {
        _health -= damageAmount;
        SoundManager.Instance.PlaySFX(SoundType.HitCharacter);
        UIManager.Instance.RefreshHealth(_health);
        if (_health <= 0)
        {
            UIManager.Instance.EndGame();
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tiles"))
        {
            _jumpCount = 0;
        }
    }
}
