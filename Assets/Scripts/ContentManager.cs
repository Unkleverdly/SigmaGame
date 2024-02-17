using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    public static ContentManager Instance { get; private set; }

    [SerializeField] private List<Sound> pickUpSounds;
    [SerializeField] private List<Sound> deathSounds;
    [SerializeField] private List<Sound> winSounds;

    [Space]

    [SerializeField] private List<Sound> bgMusic;
    [SerializeField] private List<Sound> winMusic;

    public List<Sound> PickUpSounds => pickUpSounds;
    public List<Sound> DeathSounds => deathSounds;
    public List<Sound> WinSounds => winSounds;
    public List<Sound> BackgroundMusic => bgMusic;
    public List<Sound> WinMusic => winMusic;

    private void Awake()
    {
        Instance = this;
    }
}

public class Content<T>
{
    [SerializeField] private string displayName;
    [SerializeField] protected T content;
    public string DisplayName => displayName;

    public static implicit operator T(Content<T> sound) => sound.content;
}

[Serializable] public class Sound : Content<AudioClip> { }

