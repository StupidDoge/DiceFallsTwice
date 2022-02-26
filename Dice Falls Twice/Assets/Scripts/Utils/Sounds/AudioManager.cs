using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    [SerializeField] private string _currentTheme;
    /*public AudioMixer mixer;*/

    public static AudioManager instance;

    public enum ManagerType
    {
        Music,
        EnemyHit,
        EnemyDeath,
        Spell
    }

    [SerializeField] private ManagerType _managerType;

    public string GetManagerType()
    {
        return _managerType.ToString();
    }

    public int GetSoundsCount()
    {
        return sounds.Length;
    }

    void Awake()
    {
        /*PlayStaticTheme();*/

        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            /*sound.audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[0];*/
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
        }
    }

    /*private void PlayStaticTheme()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "start menu" || scene.name == "level menu")
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
        else
            if (instance != null)
            {
                SceneManager.MoveGameObjectToScene(instance.gameObject, SceneManager.GetActiveScene());
                Destroy(instance.gameObject);
            }
    }*/

    private void Start()
    {
        if (_managerType == ManagerType.Music)
            Play(_currentTheme);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.Play();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.Pause();
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.UnPause();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.Stop();
    }
}
