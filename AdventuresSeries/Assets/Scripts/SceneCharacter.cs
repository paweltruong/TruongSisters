
using Unity.Collections;
using UnityEngine;

public class SceneCharacter : MonoBehaviour
{

    [SerializeField, ReadOnly] bool _isMoving = false;
    Vector3 _target;
    TargetNode _targetNode;
    [SerializeField] float _speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        UpdateFlyToLocation();
    }

    void UpdateFlyToLocation()
    {
        if (_isMoving)
        {
            var toTargetVector = _target - transform.position;
            var distanceToTarget = toTargetVector.magnitude;

            var moveDirection = toTargetVector.normalized;

            transform.position = Vector3.Lerp(transform.position, transform.position + (moveDirection) * _speed, Time.deltaTime);
        }
    }


    public void GoToNode(TargetNode node)
    {
        if (node == null)
        {
            Debug.Log("FlyToNode argument not provided");
            return;
        }

        _isMoving = true;
        _target = node.transform.position;
        _targetNode = node;
}
}
