using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class PlatformerCharacter : MonoBehaviour
{
	Rigidbody2D rb;
	public float jumpheight;
	public Transform groundCheck;
	private bool isGrounded;
	public KeyCode left = KeyCode.A;
	public KeyCode right = KeyCode.D;
	public float speed = 1.5f;
	public Camera mainCamera;
	public bool facingRight = true; // на старте, персонаж смотрит вправо?
	private Vector3 theScale;
	private Rigidbody2D body;
	private Vector3 pos;
	private float h;
	private Animator anim;
	void Start()
	{
		anim = GetComponent<Animator>();
		
		rb = GetComponent<Rigidbody2D>();
	}
	void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		theScale = transform.localScale;
	}

	void FixedUpdate()
	{
		// добавляем ускорение
		body.AddForce(Vector2.right * h * speed * body.mass * 100);

		if (Mathf.Abs(body.velocity.x) > speed) // если скорость тела превышает установленную, то выравниваем ее
			body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
	}

	// разворот относительно позиции курсора
	void LookAtCursor()
	{
		if (Input.mousePosition.x < pos.x && facingRight) Flip();
		else if (Input.mousePosition.x > pos.x && !facingRight) Flip();
	}
	void CheckGround()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 1);
		isGrounded = colliders.Length > 1;
	}
	void Update()
	{
		if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
		{
			anim.SetBool("Walk", true);
		}
		else
		{
			anim.SetBool("Walk", false);
		}
		CheckGround();
		rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
			rb.AddForce(transform.up * jumpheight, ForceMode2D.Impulse);
		if (Input.GetKeyDown(KeyCode.W) && isGrounded)
			rb.AddForce(transform.up * jumpheight, ForceMode2D.Impulse);
		if (Input.GetKey(left)) h = -1;

		else if (Input.GetKey(right)) h = 1; 
		else h = 0;
		if (Input.GetMouseButton(1)) // если нажата ПКМ, персонаж будет двигаться в сторону курсора
			if (facingRight) h = 1; else h = -1;

		// переносим позицию из мировых координат в экранные
		pos = mainCamera.WorldToScreenPoint(transform.position);

		if (h == 0) LookAtCursor();

		
		else
		{
			LookAtCursor();
		}
	}

	void Flip() // отразить по горизонтали
	{
		facingRight = !facingRight;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}