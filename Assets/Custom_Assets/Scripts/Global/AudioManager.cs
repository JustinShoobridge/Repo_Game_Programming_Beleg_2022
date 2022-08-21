using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    public AudioClip _MainLoop;
    public AudioClip _EnemyHit;
    public AudioClip _EnemyDead;
    public AudioClip _GunShot;

    private GameObject _BackgroundAudio;
    private AudioSource _AudioSource;

    private Input_Manager _InputManager;
    [SerializeField] private GameObject _Manager;

    public void OnEnable()
    {
        _AudioSource = this.GetComponent<AudioSource>();
        PlayBackgroundMusic();

        _InputManager = _Manager.GetComponent<Input_Manager>();
        _InputManager._OnMouseDown += PlayGunShotSound;
    }

    public void PlayBackgroundMusic()
    {
        _AudioSource.PlayOneShot(_MainLoop);
    }

    public void PlaySoundEffect(SoundEffectTypes type, Vector3 position)
    {
        switch (type)
        {
            case SoundEffectTypes.ENEMYHIT:
                AudioSource.PlayClipAtPoint(_EnemyHit,position);
                break;

            case SoundEffectTypes.ENEMYDEATH:
                AudioSource.PlayClipAtPoint(_EnemyDead, position);
                break;

            case SoundEffectTypes.GUNSHOT:
                _AudioSource.PlayOneShot(_GunShot);
                break;
        }
    }

    private void PlayGunShotSound() => PlaySoundEffect(SoundEffectTypes.GUNSHOT, this.transform.position);
}
