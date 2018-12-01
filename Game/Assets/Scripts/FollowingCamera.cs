using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowingCamera : MonoBehaviour
{
    [SerializeField]
    private PlayerManager _playerManager;

    public GameObject Target;
    public float SmoothTime;
    public float MaxSpeed;

    private Camera _followingCamera;
    private Vector3 _startPos;

    #region References
    private Vector2 _currentVelocity;
    #endregion

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void Start()
    {
        _followingCamera = GetComponent<Camera>();

        if (_playerManager != null)
        {
            _playerManager.OnActivePlayerChanged.AddListener(SetTarget);
        }
    }

    private void FixedUpdate()
    {
        if (Target == null)
        {
            return;
        }

        var dampedPosition = Vector2.SmoothDamp(_followingCamera.transform.position, Target.transform.position,
            ref _currentVelocity, SmoothTime, MaxSpeed, Time.fixedDeltaTime);

        var newPosition = new Vector3(dampedPosition.x, dampedPosition.y, _startPos.z);
        _followingCamera.transform.position = newPosition;
    }

    private void OnDestroy()
    {
        if (_playerManager != null)
        {
            _playerManager.OnActivePlayerChanged.RemoveListener(SetTarget);
        }
    }

    public void SetTarget(Player target)
    {
        if (target == null)
        {
            return;
        }

        Target = target.gameObject;
    }
}
