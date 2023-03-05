using UnityEngine;

public class StoryPlayer : MonoBehaviour
{
    int _currentIndex = -1;
    AudioSource _audioSource;
    [SerializeField] AudioClip[] _storyClips;

    private void Awake()
    {
        _audioSource= GetComponent<AudioSource>();
    }

    public void PlayNext()
    {
        if (_audioSource == null)
        {
            Debug.LogError("No AudioSource found on " +  gameObject.name);
            return;
        }

        if (_storyClips.Length <= 0)
        {
            Debug.LogError("No StoryClips found on " + gameObject.name);
            return;
        }

        if (_currentIndex + 1 >= _storyClips.Length) 
        {
            //Story is finished
            return;
        }
        ++_currentIndex;
        _audioSource.PlayOneShot(_storyClips[_currentIndex]);
    }
}
