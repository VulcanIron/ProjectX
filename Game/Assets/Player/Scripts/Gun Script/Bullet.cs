using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 20f;
	public int damage = 40;
	public Rigidbody2D rb;
	public GameObject impactEffect;
	public float lifeTime;
	[SerializeField] bool enemyBullet;
	void Start () {
		rb.velocity = transform.right * speed;

	}
	private void Update()
    {
		Invoke("DestroyBullet", lifeTime);
	}
	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		Enemy enemy = hitInfo.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
			DestroyBullet();

		}
	}
	void DestroyBullet()
	{
		Destroy(gameObject);
	}

}
