using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 50f; 

    void Start()
    {
        
    }
    void Update()
    {
        float yMove = 0;
        float xMove = 0;

        if (Input.GetKey(KeyCode.W))
        {
            yMove = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xMove = -1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yMove = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xMove = 1f;
        }

        float yMoveAmount = yMove * speed * Time.deltaTime;
        float xMoveAmount = xMove * speed * Time.deltaTime;

        transform.Translate(xMoveAmount, yMoveAmount, 0);
    }


}
