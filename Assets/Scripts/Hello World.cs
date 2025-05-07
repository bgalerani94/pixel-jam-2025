using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the object
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello, World!");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1.5f, -2, 0) * Time.deltaTime * speed;
    }
}
