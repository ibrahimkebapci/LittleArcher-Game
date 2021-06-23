using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public GameObject WinImage;
    public GameObject DeathScreen;
    private float mySpeedX;
    [SerializeField] float speed;
    private Rigidbody2D myBody;
    public bool onGround;
    private bool canDoubleJump;
    private Vector3 defaultLocalScale;
    [SerializeField] float jumpPower;
    [SerializeField] GameObject arrow;
    public float arrowSpeed;
    [SerializeField] bool attacked;
    [SerializeField] float currentAttackTimer;
    [SerializeField] float defaultAttackTimer;
    private Animator myAnimator;
    [SerializeField] AudioClip dieMusic;
    [SerializeField] AudioClip winMusic;
    private GameMaster gm;

    // Start is called before the first frame update
    void Start()
    {
        //dinamik deðerler istiyorsan genellikle start fonksiyonunu kullanmak gerekiyor
        attacked = false;
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale;
        Debug.Log(defaultLocalScale);
        DeathScreen.SetActive(false);
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
    }

    // Update is called once per frame
    void Update()
    {
        mySpeedX = Input.GetAxis("Horizontal");//kullandýðýn tuþlarý tanýmlamak için
        myBody.velocity = new Vector2(mySpeedX * speed, myBody.velocity.y);//yön týþlarý ile belirlenen yönde harekt kazandýrmak için
        myAnimator.SetFloat("Speed", Mathf.Abs(mySpeedX));
        #region player donuþ hareketleri
        if (mySpeedX > 0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);//basýlan yön tuþu sað yön tuþu ise oluðu gibi devam et 
        }
        else if (mySpeedX < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);//basýlan yön tuþu sol yön tulu ise karakterin pozisyonunu 180 derece döndür
        }
        #endregion
        #region player ziplama
        if (Input.GetKeyDown(KeyCode.UpArrow))//yukarý yön tuþuna basarsam zýpla
        {
            if (onGround == true)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);//eðer oyuncu zemine temas halinde ise zýplama izini ver
                canDoubleJump = true;
                myAnimator.SetTrigger("Jump");
            }
            else
            {
                //Eðer bir kere zýplama iþlemi gerçekleþtirilmiþ ise
                //2.si için izin verilir ve 3.nün gerçekleþmesi engellenir
                if (canDoubleJump == true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                    canDoubleJump = false;
                }
            }
        }
        #endregion
        #region player ok atma

        if (Input.GetMouseButtonDown(0))
            
        {
            if (attacked == false)
            {
                attacked = true;
                myAnimator.SetTrigger("Attack");
                Invoke(nameof(Fire), 0.5f);
            }
        }

        if (attacked == true)
        {
            currentAttackTimer -= Time.deltaTime;
        }
        else
        {
            currentAttackTimer = defaultAttackTimer;
        }
        if (currentAttackTimer <= 0)
        {
            attacked = false;
        }

        #endregion

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Tuzak"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("WinTree"))
        {
            EnableWinScreen();
        }
    }
    void Die()
    {
        GameObject.Find("SoundController").GetComponent<AudioSource>().clip = null;
        GameObject.Find("SoundController").GetComponent<AudioSource>().PlayOneShot(dieMusic);
        //oyuncunun öldüðü zaman çalan müzüik ayarlamasý için olan kýsým
        myAnimator.SetTrigger("Die");
        myAnimator.SetFloat("Speed", 0);
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        EnableLoseScreen();

    }
    void Fire()
    {
        GameObject arrowObject = Instantiate(arrow, transform.position, Quaternion.identity);
        arrowObject.transform.parent = GameObject.Find("Arrows").transform;
        if (transform.localScale.x > 0)
        {
            arrowObject.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed, 0);
        }
        else
        {
            Vector3 arrowScale = arrowObject.transform.localScale;
            arrowObject.transform.localScale = new Vector3(-arrowScale.x, arrowScale.y, arrowScale.z);
            arrowObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-arrowSpeed, 0);
        }
    }
    void EnableWinScreen()
    {
        GameObject.Find("SoundController").GetComponent<AudioSource>().clip = null;
        GameObject.Find("SoundController").GetComponent<AudioSource>().PlayOneShot(winMusic);
        myAnimator.SetFloat("Speed", 0);
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        WinImage.SetActive(true);
        DeathScreen.SetActive(true);
    }
    void EnableLoseScreen()
    {
        DeathScreen.SetActive(true);
    }

}
