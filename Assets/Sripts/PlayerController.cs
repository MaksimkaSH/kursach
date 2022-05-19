using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float YmovementSpeed;
    public float XmovementSpeed;
    public static float horizontal, vertical;
    

    public KeyCode jumpButton = KeyCode.Space;
    public float jumpForce;
    public float jumpRadius;
    public LayerMask ground;

    private Rigidbody rb;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    //Функция определения, находится ли игровой персонаж на земле
    bool isGrounded()
    {
        //В данную переменную записывается информация о пересечении луча с каким-либо объектом
        RaycastHit hit;
        //Направляем луч от центральной точки нашего персонажа вниз
        Ray checkRay = new Ray(transform.position, Vector3.down);
        //Запускаем луч и проверяем, пересёк ли он нужный нам объект, пройдя какое-то изначально заданное расстояние 
        if (Physics.Raycast(checkRay, out hit, jumpRadius, ground))
        {
            return true;
        }

        return false;
    }


    void FixedUpdate()
    {
        
            horizontal = Input.GetAxis("Horizontal") * XmovementSpeed * Time.deltaTime;
            transform.Translate(horizontal, 0, 0);
            vertical = Input.GetAxis("Vertical") * YmovementSpeed * Time.deltaTime;
            transform.Translate(0, 0, vertical);
        

        if (Input.GetKeyDown(jumpButton) && isGrounded())
        {
            rb.velocity = new Vector2(0, jumpForce);
        }
    }


    
}
