using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour 
{        
    [SerializeField] GameObject player;
    [SerializeField][Range(0.5f, 2f)]
    float mouseSense = 2f; 
    [SerializeField][Range(-90, -10)]
    int lookUp = -15;
    [SerializeField][Range(15, 90)]
    int lookDown = 20;
    //булева переменная, для определения текущего состояния игрока
    public bool isSpectator;
    //скорость полета свободной камеры
    [SerializeField] float speed = 50f;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }
    void Update()
    {     
        float rotateX = Input.GetAxis("Mouse X") * mouseSense;
        float rotateY = Input.GetAxis("Mouse Y") * mouseSense;
        if (!isSpectator){
            Vector3 rotCamera = transform.rotation.eulerAngles;
            Vector3 rotPlayer = player.transform.rotation.eulerAngles;

            rotCamera.x = (rotCamera.x > 180) ? rotCamera.x - 360 : rotCamera.x;
            rotCamera.x = Mathf.Clamp(rotCamera.x, lookUp, lookDown);
            rotCamera.x -= rotateY;               
            
            rotCamera.z = 0;
            rotPlayer.y += rotateX;

            transform.rotation = Quaternion.Euler(rotCamera);
            player.transform.rotation = Quaternion.Euler(rotPlayer);   
        }
        else{
             //получаем текущий поворот камеры
        Vector3 rotCamera = transform.rotation.eulerAngles;
        //меняем поворот камеры в зависимости от значения движения мыши
        rotCamera.x -= rotateY;
        rotCamera.z = 0;
        rotCamera.y += rotateX;
        transform.rotation = Quaternion.Euler(rotCamera);
        //считываем значение клавиш WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        //Задаем вектор направления полета камеры
        Vector3 dir = transform.right * x + transform.forward * z;
        //меняем позицию камеры
        transform.position += dir * speed * Time.deltaTime;
        }
    }
}