using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private const float MoveCameraSpeed = 20;

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(1 * Time.deltaTime * MoveCameraSpeed, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(0, -1 * Time.deltaTime * MoveCameraSpeed, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(-1 * Time.deltaTime * MoveCameraSpeed, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(0, 1 * Time.deltaTime * MoveCameraSpeed, 0);
        }
    }
}