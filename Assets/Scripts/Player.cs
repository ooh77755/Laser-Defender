using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 10f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float padding = 0.5f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        StartCoroutine(CoroutineTest());
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private IEnumerator CoroutineTest()
    {
        Debug.Log("1st msg");
        yield return new WaitForSeconds(3);
        Debug.Log("2nd msg");
    }

    void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var deltaY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        }
    }
}
