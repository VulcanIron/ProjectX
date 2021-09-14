using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health = 100;
	public GameObject deathEffect;
	private Animator anim;
	void Start()
    {
		anim = GetComponent<Animator>();
    }
	void Update()
    {
			
	}
	public void TakeDamage (int damage)
	{
		anim.SetTrigger("damage_anim");
		health -= damage;
		if (health <= 0)
		{
			Die();
		}
	}

	void Die ()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

}
