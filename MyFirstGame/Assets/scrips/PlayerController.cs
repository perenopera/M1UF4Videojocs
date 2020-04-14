using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float velocidad;
    private int count;
    public Text countText;
    public Text winText;
    public Text winText2;
    public Text muertes;
    public int numMuertes;
    public float thrust = 10;
    bool m_isGrounded;
    float movimientoHorizontal;
    float movimientoVertical;
    Vector3 originalPos;
    Vector3 posicionSegundoMapa;
    Vector3 posicionTercerMapa;
    bool next;
    int nextMap;
    Rigidbody m_Rigidbody;
    bool muerte;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 10;
        velocidad = 400;
        next = false;
        SetMuerteText();
        SetCountText();
        winText.text = "";
        winText2.text = "";
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        posicionSegundoMapa = new Vector3(100f, 0f, 0f);
        posicionTercerMapa = new Vector3(-100f, 0f, 0f);
        muerte = false;
        nextMap = 0;

    }



    private void Update()
    {
        if (m_isGrounded == true)
        {
        movimientoHorizontal = Input.GetAxis("Horizontal");
        movimientoVertical = Input.GetAxis("Vertical");
        }
       
        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded == true)
        {
            rb.AddForce(0, thrust, 0, ForceMode.Impulse);
            m_isGrounded = false;
        }
        if (Input.GetButton("Jump") && m_isGrounded == true)
        {
            rb.AddForce(0, thrust, 0, ForceMode.Impulse);
            m_isGrounded = false;
        }
        Vector3 movimiento = new Vector3(movimientoHorizontal * Time.deltaTime, 0.0f, movimientoVertical * Time.deltaTime);
        rb.AddForce(movimiento * velocidad);
        if (next == true)
        {
            next = false;
            StartCoroutine(esperar());
            IEnumerator esperar()
            {
                // Do something
                yield return new WaitForSeconds(5f);  // Wait three seconds
                count = 10;
                numMuertes = 0;
                winText.text = "";
                winText2.text = "";
                SetCountText();
                SetMuerteText();
                if (nextMap == 1)
                {
                gameObject.transform.position = posicionSegundoMapa;
                }
                if (nextMap == 2)
                {
                    gameObject.transform.position = posicionTercerMapa;
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            count = count -1;
            SetCountText();
        }
    }
    void OnTriggerEnter(Collider other)
    {
       if ( other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count - 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Muerte"))
        {
            if (nextMap == 0)
            {
                gameObject.transform.position = originalPos;
            }
            if (nextMap == 1)
            {
                gameObject.transform.position = posicionSegundoMapa;
            }
            if (nextMap == 2)
            {
                gameObject.transform.position = posicionTercerMapa;
            }
            numMuertes = numMuertes + 1;
            SetMuerteText();
            muerte = true;

        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("suelo"))
        {
            m_isGrounded = true;
        }
      

    }
 
    void SetMuerteText()
    {
        muertes.text = "Intentos: " + numMuertes.ToString();
    }
    void SetCountText()
    {

        countText.text = "Pickups restantes: " + count.ToString();
        if (count == 0)
        {
            winText.text = "GANASTE";
            winText2.text = "Total Muertes: " + numMuertes.ToString(); ;
            next = true;
            nextMap = nextMap + 1;

        }
        
    }


}