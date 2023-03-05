using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdCharacter : MonoBehaviour
{
    const string AnimParam_IsFlying = "IsFlying";
    const string AnimParam_IsTalking = "IsTalking";
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFlying(bool flying)
    {
        if (_animator)
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
    }


    public void SetTalking(bool talking)
    {
        if (_animator)
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
}
