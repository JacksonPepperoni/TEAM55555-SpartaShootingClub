using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

// [박상원] 임시 플레이어 오디오 매니저
[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip walkingClip;
    [SerializeField] private AudioClip runningClip;

    private AudioMixerGroup[] audioMixGroup;

    public AudioSource Source { get; private set; }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        audioMixer = ResourceManager.Instance.GetCache<AudioMixer>("AudioMixer");
        walkingClip = ResourceManager.Instance.GetCache<AudioClip>("S_CH_Loop_Walking");
        runningClip = ResourceManager.Instance.GetCache<AudioClip>("S_CH_Loop_Running");
        audioMixGroup = audioMixer.FindMatchingGroups("Master");

        this.Source = GetComponent<AudioSource>();
        this.Source.outputAudioMixerGroup = audioMixGroup[0];
        this.Source.clip = walkingClip;
        this.Source.loop = true;

        return true;
    }

    public void MovementSound()
    {
        this.Source.clip = InputManager.Instance.FastRunPress ? runningClip : walkingClip;

        if (!this.Source.isPlaying)
        {
            this.Source.Play();
        }
    }

    public void PlayOneShot(AudioClip clip, float SoundModifier = 1)
    {
        if (clip == null) return;

        AudioSource newAudioSource = ResourceManager.Instance.InstantiatePrefab("AudioSource").GetComponent<AudioSource>();
        newAudioSource.volume = Source.volume * SoundModifier;
        newAudioSource.clip = clip;
        newAudioSource.Play();

        StartCoroutine(nameof(DestroySourceWhenFinished), newAudioSource);
    }

    private IEnumerator DestroySourceWhenFinished(AudioSource source)
    {
        yield return new WaitWhile(() => source.isPlaying);

        ResourceManager.Instance.Destroy(source.gameObject);
    }
}
