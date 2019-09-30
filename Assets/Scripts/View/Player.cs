using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float horizontal;

    private Rigidbody2D RbPlayer2d;
    private Animator animator;
    private bool ladoDireito;

    private int puloExtra;
    public int puloExtraValues = 2;
    public float jumpforce = 0;

    private bool estaNoChao;
    public Transform ChaoCheck;
    public float raio = 0.1f;
    public LayerMask plataforma;

    [SerializeField]
    private float velocidade = 0;
    private float valorExtraCorrida = 3;

    // Start is called before the first frame update
    private void Awake()
    {
        RbPlayer2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ladoDireito = transform.localScale.x > 0;
    }

    void Start()
    {
        puloExtra = puloExtraValues;
    }

    private void Update()
    {
        Pular();
        AnimationPulo();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        estaNoChao = Physics2D.OverlapCircle(ChaoCheck.position, raio, plataforma);
        horizontal = Input.GetAxisRaw("Horizontal");
        Direcao(horizontal);
        Movimentar(horizontal);
    }

    /*Velocidade do movimento do personagem*/

    private void Movimentar(float horiz)
    {
        if (horiz != 0)
        {
            if (Input.GetButton("Correr"))
            {
                RbPlayer2d.velocity = new Vector2(horiz * (velocidade + valorExtraCorrida), RbPlayer2d.velocity.y);
                animator.SetFloat("velocidade", Mathf.Abs(velocidade + valorExtraCorrida));
                animator.SetBool("btnCorrer", true);

            }
            else
            {
                RbPlayer2d.velocity = new Vector2(horiz * velocidade, RbPlayer2d.velocity.y);
                animator.SetFloat("velocidade", Mathf.Abs(velocidade));
                animator.SetBool("btnCorrer", false);
            }
        }
        else
        {
            RbPlayer2d.velocity = new Vector2(horiz * velocidade, RbPlayer2d.velocity.y);
            animator.SetFloat("velocidade", 0);
            animator.SetBool("btnCorrer", false);
        }
    }

    /*Direção que o personagem se movimenta*/

    private void Direcao(float horizontal)
    {
        if (horizontal > 0 && !ladoDireito || horizontal < 0 && ladoDireito)
        {
            ladoDireito = !ladoDireito;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }


    public void Pular()
    {
        if (estaNoChao == true)
        {
            puloExtra = puloExtraValues;
        }

        if (Input.GetButtonDown("Jump") && puloExtra > 0)
        {
            RbPlayer2d.velocity = Vector2.up * jumpforce;
            puloExtra--;
        }
        else if (Input.GetButtonDown("Jump") && puloExtra > 0 && estaNoChao == true)
        {
            RbPlayer2d.velocity = Vector2.up * jumpforce;
            puloExtra--;
        }
    }

    private void AnimationPulo()
    {
        if (Input.GetButton("Jump"))
        {
            animator.SetBool("btnPular", true);

            if (estaNoChao)
            {
                animator.SetBool("btnPular", false);
            }
        }
        else
        {
            animator.SetBool("btnPular", false);
        }

    }



    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.transform.tag == "PlataformaMovel")
        {
            transform.parent = colisao.transform;

        }

        if (colisao.transform.tag == "Inimigo" || colisao.transform.tag == "Morte")
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            estaNoChao = false;
            puloExtraValues = 0;
            animator.SetFloat("velocidade", 0);
            animator.SetBool("btnCorrer", false);
            StartCoroutine(EnableCol(2.0f));      
        }
    }

    private void OnCollisionExit2D(Collision2D colisao)
    {
        if (colisao.transform.tag == "PlataformaMovel")
        {
            transform.parent = null;
        }
    }

    IEnumerator EnableCol(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        SceneManager.LoadScene("PrincipalCena");
    }

}
