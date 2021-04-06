using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class home0 : MonoBehaviour {

	// Use this for initialization
    private SpriteRenderer spriteRenderer;
    public Sprite brokenSprite;    //老家被破坏的图片
    public GameObject explosion;

    public AudioClip dieAudio;

	void Start () {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Die()
    {
        spriteRenderer.sprite = brokenSprite;
        GameObject.Instantiate(explosion, transform.position, transform.rotation);
        PlayerManager0._instance.isDefeat0 = true; //告诉被打败
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}
