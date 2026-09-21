using UnityEngine;

public enum SoundType
{
    Background,
    Coin,
    Fire,
    HitEnemy,
    HitCharacter,
    Jump,
    Explosion,
    WaterSplash
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource coinSFX;
    [SerializeField] private AudioSource fireSFX;
    [SerializeField] private AudioSource hitEnemySFX;
    [SerializeField] private AudioSource hitCharacterSFX;
    [SerializeField] private AudioSource jumpSFX;
    [SerializeField] private AudioSource explosionSFX;
    [SerializeField] private AudioSource waterSplashSFX;

    public static SoundManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }

    public void PlaySFX(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.Background:
                bgMusic.Play();
                break;
            case SoundType.Coin:
                coinSFX.Play();
                break;
            case SoundType.Fire:
                fireSFX.Play();
                break;
            case SoundType.HitEnemy:
                hitEnemySFX.Play();
                break;
            case SoundType.HitCharacter:
                hitCharacterSFX.Play();
                break;
            case SoundType.Jump:
                jumpSFX.Play();
                break;
            case SoundType.Explosion:
                explosionSFX.Play();
                break;
            case SoundType.WaterSplash:
                waterSplashSFX.Play();
                break;
        }
    }
}
