using System.Collections.Generic;
using UnityEngine;
using YG;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; } // Синглтон

    [System.Serializable]
    public class Sound
    {
        public string name; // Имя звука
        public AudioClip clip; // Аудиоклип
        [Range(0f, 1f)] public float volume = 1f; // Громкость
        public bool loop; // Зациклить ли звук
        public AudioSource source; // Источник звука (создается автоматически)
    }

    [SerializeField] private List<Sound> musicTracks; // Список музыкальных треков
    [SerializeField] private List<Sound> soundEffects; // Список звуковых эффектов

    [Range(0f, 1f)] public float musicVolume = 1f; // Громкость музыки
    [Range(0f, 1f)] public float effectsVolume = 1f; // Громкость звуковых эффектов
    
    private void Awake()
    {
        // Реализация синглтона
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Инициализация источников звука
        InitializeSounds(musicTracks);
        InitializeSounds(soundEffects);
    }

    // Инициализация источников звука
    private void InitializeSounds(List<Sound> sounds)
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    // Воспроизведение музыки
    public void PlayMusic(string name)
    {
        Sound sound = musicTracks.Find(s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Музыкальный трек не найден: " + name);
            return;
        }

        sound.source.volume = musicVolume * sound.volume;
        sound.source.Play();
    }

    // Остановка музыки
    public void StopMusic(string name)
    {
        Sound sound = musicTracks.Find(s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Музыкальный трек не найден: " + name);
            return;
        }

        sound.source.Stop();
    }

    // Воспроизведение звукового эффекта
    public void PlaySoundEffect(string name)
    {
        Sound sound = soundEffects.Find(s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Звуковой эффект не найден: " + name);
            return;
        }

        sound.source.volume = effectsVolume * sound.volume;
        sound.source.Play();
    }

    // Установка громкости музыки
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        foreach (Sound sound in musicTracks)
        {
            sound.source.volume = musicVolume * sound.volume;
        }
    }

    // Установка громкости звуковых эффектов
    public void SetEffectsVolume(float volume)
    {
        effectsVolume = volume;
        foreach (Sound sound in soundEffects)
        {
            sound.source.volume = effectsVolume * sound.volume;
        }
    }

    // Отключение музыки
    public void ToggleMusic(bool isOn)
    {
        foreach (Sound sound in musicTracks)
        {
            sound.source.mute = !isOn;
        }
    }

    // Отключение звуковых эффектов
    public void ToggleEffects(bool isOn)
    {
        foreach (Sound sound in soundEffects)
        {
            sound.source.mute = !isOn;
        }
    }
}