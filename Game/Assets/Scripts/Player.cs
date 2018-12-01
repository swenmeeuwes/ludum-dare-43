using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _groundCheck;
    [Range(0, .3f)] [SerializeField]
    private float _movementSmoothing = .05f;

    public float MoveSpeed;
    public float JumpForce;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private bool _isGoingToJump;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        _isGrounded = Physics2D.Linecast(transform.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButton("Jump") && _isGrounded)
        {
            _isGoingToJump = true;
        }
    }

    private void FixedUpdate()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move(input);
    }

    public void Kill()
    {
        enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Ground");
    }

    private void Move(Vector2 input)
    {
        _rigidbody.transform.Translate(input.x * MoveSpeed * Time.deltaTime, 0, 0);

        if (_isGoingToJump)
        {
            _rigidbody.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            _isGoingToJump = false;
        }
    }
}
