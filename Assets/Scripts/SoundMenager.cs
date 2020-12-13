using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundMenager : MonoBehaviour
{
    public static SoundMenager instance;
    public AudioClip[] clips;
    AudioSource source;
    void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(Vector3 pos)
    {
        transform.position = pos;
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
