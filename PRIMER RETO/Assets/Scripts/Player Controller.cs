using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriterenderer;
    public float speed = 4f;
    public float jumpForce = 2f;
    public float speedrun = 10f;
    public float inicioX = 0f;
    public float inicioY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriterenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //Mapeo del imput del player al animator
        _animator.SetFloat("MoveHorizontal", Input.GetAxis("Horizontal"));
        _animator.SetFloat("MoveVertical", Input.GetAxis("Vertical"));
        //Movimiento del jugador
        _rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, _rigidbody.velocity.y);
        if (Input.GetAxis("Horizontal") < 0)
        {
            _spriterenderer.flipX = true;
        }
        else
        {
            _spriterenderer.flipX = false;
        }
        //Salto del jugador
        _animator.SetBool("MoveJump", false);
        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetBool("MoveJump", true);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
        }
        //Corrrer
        _animator.SetBool("Run", false);
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetBool("Run", true);
            _rigidbody.AddForce(new Vector2(0, speedrun));
        }
        //Daño del jugador
        _spriterenderer.enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(inicioX, inicioY, 0);
        DamagePlayer.damageTime += Time.deltaTime;
        if (DamagePlayer.damageTime > 0.5f)
        {
            _spriterenderer.enabled = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            //appearingActive = false;
            DamagePlayer.damageSignal = false;
            DamagePlayer.Lesslife();
        }
    }
}
