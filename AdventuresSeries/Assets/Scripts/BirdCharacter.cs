using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdCharacter : MonoBehaviour
{
    const string AnimParam_IsFlying = "IsFlying";
    const string AnimParam_IsTalking = "IsTalking";
    Animator _animator;
    AudioSource _audioSource;
    bool _isFlying = false;
    Vector3 _flyTarget;
    TargetNode _flyTargetNode;
    float _flyShakeTimer = 0;

    [SerializeField] float _speed = 1;
    [SerializeField] float _targetTolerance = .2f;
    //Hows pull to ground force affect flying character
    [SerializeField] AnimationCurve _flyShake;
    [SerializeField] AudioClip[] _wingFlapSFX;



    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFlyToLocation();
    }

    void UpdateFlyToLocation()
    {
        if (_isFlying)
        {
            var toTargetVector = _flyTarget - transform.position;
            var distanceToTarget = toTargetVector.magnitude;

            if (distanceToTarget <= _targetTolerance)
            {
                if (_flyTargetNode == null || _flyTargetNode.IsGrounded)
                {
                    SetFlying(false);
                    return;
                }
            }

            var flyDirection = toTargetVector.normalized;

            Vector3 pullDownForce = Vector3.zero;
            if (_flyShake != null)
            {
                pullDownForce.y = _flyShake.Evaluate(_flyShakeTimer);
                Vector3.ClampMagnitude(pullDownForce, 1);
            }

            transform.position = Vector3.Lerp(transform.position, transform.position + (flyDirection + pullDownForce) * _speed, Time.deltaTime);

            //Update fly shake timer
            _flyShakeTimer += Time.deltaTime;
            if (_flyShakeTimer > 1) _flyShakeTimer = 0;
        }
    }

    public void SetFlying(bool flying)
    {
        if (_animator == null)
        {
            Debug.LogError("There is no animator on " + gameObject.name);
            return;
        }

        if (!_animator.parameters.Any(p => p.name == AnimParam_IsFlying))
        {
            Debug.LogError("Parameter " + AnimParam_IsFlying + " was not found on Animator " + gameObject.name + "." + _animator.name);
            return;
        }

        _animator.SetBool(AnimParam_IsFlying, flying);
        _isFlying = flying;
        _flyShakeTimer = 0f;
    }


    public void SetTalking(bool talking)
    {
        if (_animator == null)
        {
            Debug.LogError("There is no animator on " + gameObject.name);
            return;
        }

        if (!_animator.parameters.Any(p => p.name == AnimParam_IsTalking))
        {
            Debug.LogError("Parameter " + AnimParam_IsTalking + " was not found on Animator " + gameObject.name + "." + _animator.name);
            return;
        }

        _animator.SetBool(AnimParam_IsTalking, talking);
    }

    public void FlyToObject(GameObject targetObject)
    {
        if (targetObject == null)
        {
            Debug.Log("FlyToObject argument not provided");
            return;
        }

        SetFlying(true);
        _flyTarget = targetObject.transform.position;
        _flyTargetNode = null;
    }

    public void FlyToNode(TargetNode node)
    {
        if (node == null)
        {
            Debug.Log("FlyToNode argument not provided");
            return;
        }

        SetFlying(true);
        _flyTarget = node.transform.position;
        _flyTargetNode = node;
    }

    public void OnWingFlap()
    {
        if (_wingFlapSFX.Length <= 0)
        {
            Debug.LogError("No WingFlapSFX found on " + gameObject.name);
            return;
        }

        if (_audioSource == null)
        {
            Debug.LogError("No AudioSource found on " + gameObject.name);
            return;
        }

        int randomIndex = Random.Range(0, _wingFlapSFX.Length);
        var randomClip = _wingFlapSFX[randomIndex];
        if (randomClip == null)
        {
            Debug.LogError("No wingflap SFX on index " + randomIndex + "found on " + gameObject.name);
            return;
        }
        _audioSource.PlayOneShot(randomClip);
    }
}
