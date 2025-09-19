using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpAnimDuration = 0.35f;

    private Rigidbody2D rb;
    private SortingGroup sortingGroup;
    private Animator anim;
    private Vector3 baseScale;

    private bool isJumping = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sortingGroup = GetComponent<SortingGroup>();
        anim = GetComponent<Animator>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        baseScale = transform.localScale;

        anim.SetBool("isRunning", false);
        anim.SetBool("isJumping", false);
    }

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
            Invoke(nameof(EndJump), jumpAnimDuration);
        }
    }

    void MovePlayer()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        rb.linearVelocity = input.normalized * moveSpeed;
        if (input.x < 0f)
            transform.localScale = new Vector3(-Mathf.Abs(baseScale.x), baseScale.y, baseScale.z);
        else if (input.x > 0f)
            transform.localScale = new Vector3(+Mathf.Abs(baseScale.x), baseScale.y, baseScale.z);
        anim.SetBool("isRunning", rb.linearVelocity.sqrMagnitude > 0.01f);
    }
    void EndJump()
    {
        anim.SetBool("isJumping", false);
        isJumping = false;
    }
}
