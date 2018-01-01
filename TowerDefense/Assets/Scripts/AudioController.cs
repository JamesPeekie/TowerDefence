using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.55f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;
    private AudioSource source;

    [Range(0f, 0.5f)]
    public float volumeRand = 0.1f;
    [Range(0f, 0.5f)]
    public float pitchRand = 0.1f;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-volumeRand / 2f, volumeRand / 2f));
        source.pitch = pitch * (1 + Random.Range(-pitchRand / 2f, pitchRand / 2f));
        source.Play();
    }
}

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [SerializeField] private Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one audio control in this scene");
            return;
        }

        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                sounds[i].Play();
                return;
            }
        }

        Debug.LogWarning("sound not found yo: " + name);
    }
}