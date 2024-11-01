public class PlayerController : MonoBehaviour {
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP = 2;

    private bool gravedadEstaActivada = true;

    private GameObject gameManager;
    private GameObject playerMessage;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    public float velocityX = 10f;
    public float velocityY = 10f;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        playerMessage = GameObject.Find("PlayerMessage");
    }

    // Update is called once per frame
    void Update() {
        if (gravedadEstaActivada) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(0, 0);
        }
        
        animator.SetInteger("Estado", ANIMATION_IDLE);

        if (Input.GetKey(KeyCode.RightArrow)) {
            WalkRight();
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            WalkLeft();
        }

        if (Input.GetKey(KeyCode.UpArrow) && !gravedadEstaActivada) {
            Jump();
        }

        if (Input.GetKey(KeyCode.DownArrow) && !gravedadEstaActivada) {
            rb.velocity = new Vector2(rb.velocity.x, -velocityY);
        }

        if (rb.velocity.x != 0) {
            animator.SetInteger("Estado", ANIMATION_RUN);
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            rb.velocity = new Vector2(rb.velocity.x, velocityY);
        }
    }

    public void WalkRight() {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
        sr.flipX = false;
    }

    public void WalkLeft() {
        rb.velocity = new Vector2(-velocityX, rb.velocity.y);
        sr.flipX = true;
    }

    public void Jump() {
        if (!gravedadEstaActivada) {
            rb.velocity = new Vector2(rb.velocity.x, velocityY);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Debug.Log("Colision con Enemigo");
            gameManager.GetComponent<GameManagerController>().RemoveLife();
            playerMessage.GetComponent<TextMeshProUGUI>().text = "Ouch!";
            playerMessage.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Invoke("HideMessage", 1);
        }

        if (collision.gameObject.name == "Coin") {
            Debug.Log("Colision con Coin");
        }

        if (collision.gameObject.name == "Finish") {
            Debug.Log("Colision con Finish");
        }

        if (collision.gameObject.tag == "Recollectable") {
            var gameManagerC = gameManager.GetComponent<GameManagerController>();
            gameManagerC.AddKunai(3);
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.name == "Pared") {
            rb.gravityScale = 0;
            gravedadEstaActivada = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.name == "Pared") {
            rb.gravityScale = 1;
            gravedadEstaActivada = true;
        }
    }

    private void HideMessage() {
        playerMessage.GetComponent<TextMeshProUGUI>().text = "";
    }
}