using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 10;
    [SerializeField]
    private float _jumpForce = 2;
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Camera _camera;

    private float _minJumpDistance = 1.1f; // distance from ground allowed before player can jump again

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"horse position {transform.position}");
        // Horizontal movement (a and d keys)
        transform.Translate(new Vector2(Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime, 0));
        _camera.transform.position = new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z);

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            _rigidbody.velocity =new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, _jumpForce));
        }
    }

    private bool IsGrounded() => Physics.Raycast(transform.position, Vector2.down, _minJumpDistance);
}
