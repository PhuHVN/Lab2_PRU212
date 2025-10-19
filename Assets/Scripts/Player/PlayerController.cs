using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 10f;
    public float jumpCooldown = 0.5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;

    private Rigidbody2D rb;
    private bool canJump = true;
    private float lastJumpTime;
    private bool isGrounded;

    //cheat code
    public GameObject isCheat; 
    private string cheatInput = "";


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void FixedUpdate()
    {

        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Update()
    {
        HandleCheatInput();

        if (Input.GetButtonDown("Jump") && canJump && isGrounded)
        {
            AudioMangement.instance.PlayJump();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            lastJumpTime = Time.time;
        }

        if (!canJump && (Time.time - lastJumpTime) >= jumpCooldown)
        {
            canJump = true;
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void HandleCheatInput()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\n' || c == '\r')
            {
                CheckCheatCode();
                cheatInput = "";
            }
            else
            {
                cheatInput += char.ToLower(c);
                if (cheatInput.Length > 20)
                    cheatInput = cheatInput.Substring(cheatInput.Length - 20);
            }
        }
    }

    private void CheckCheatCode()
    {
        PlayerMonsterCollision playerMonsterCollision = GetComponent<PlayerMonsterCollision>();
        PlayerRockCollision playerRockCollision = GetComponent<PlayerRockCollision>();
        if (cheatInput.Contains("cheat"))
        {
            Debug.Log("Cheat code activated!");
            playerRockCollision.isCheatActive = !playerRockCollision.isCheatActive;
            playerMonsterCollision.isCheatActive = !playerMonsterCollision.isCheatActive;
            isCheat.SetActive(playerMonsterCollision.isCheatActive);
        }
    }
}
