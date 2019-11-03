using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SfxName : int
{
    Shot = 0,
    Hit,
    Spring,
    Holein,
    Button,
}

public class SfxManager : MonoBehaviour
{

    [SerializeField]
    List<AudioClip> m_ShotClipList;
    [SerializeField]
    List<AudioClip> m_HitClipList;
    [SerializeField]
    List<AudioClip> m_SpringClipList;
    [SerializeField]
    List<AudioClip> m_HoleinClipList;
    [SerializeField]
    List<AudioClip> m_ButtonClipList;

    static SfxManager m_Instance;
    List<List<AudioClip>> m_ClipsList;
    AudioSource[] m_SourceArray;
    int m_CurrentSourceIndex = 0;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        m_Instance = this;
        m_ClipsList = new List<List<AudioClip>>();
        int sfxCount = System.Enum.GetNames(typeof(SfxName)).Length;
        for (int i = 0; i < sfxCount; i++) { m_ClipsList.Add(null); }
        m_ClipsList[(int)SfxName.Shot] = m_ShotClipList;
        m_ClipsList[(int)SfxName.Hit] = m_HitClipList;
        m_ClipsList[(int)SfxName.Spring] = m_SpringClipList;
        m_ClipsList[(int)SfxName.Holein] = m_HoleinClipList;
        m_ClipsList[(int)SfxName.Button] = m_ButtonClipList;
        
        m_SourceArray = GetComponentsInChildren<AudioSource>();
    }

    public static void Play(SfxName sfxName, int clipIndex)
    {
        if (m_Instance == null) { return; }
        if (clipIndex >= m_Instance.m_ClipsList[(int)sfxName].Count) { return; }
        AudioClip clip = m_Instance.m_ClipsList[(int)sfxName][clipIndex];
        Play(clip);
    }

    public static void Play(SfxName sfxName)
    {
        int clipCount = m_Instance.m_ClipsList[(int)sfxName].Count;
        int clipIndex = 0;
        if (clipCount > 1)
        {
            clipIndex = UnityEngine.Random.RandomRange(0, clipCount);
        }
        Play(sfxName, clipIndex);
    }

    public static void Play(AudioClip clip)
    {
        m_Instance.m_CurrentSourceIndex += 1;
        if (m_Instance.m_CurrentSourceIndex >= m_Instance.m_SourceArray.Length)
        {
            m_Instance.m_CurrentSourceIndex %= m_Instance.m_SourceArray.Length;
        }

        AudioSource source = m_Instance.m_SourceArray[m_Instance.m_CurrentSourceIndex];
        if (source.isPlaying)
        {
            source.Stop();
        }
        source.PlayOneShot(clip);
    }

}
