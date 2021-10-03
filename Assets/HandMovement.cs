using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    private float _armLength = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        setHandPosition();
    }

    // Update is called once per frame
    void Update()
    {
        setHandPosition();
    }

    private void setHandPosition()
    {
        float angle = PlayerAim.GetMouseAngle(player);

        float xPos = Mathf.Cos(angle);
        float yPos = Mathf.Sin(angle);

        transform.position = new Vector3(xPos * _armLength, yPos * _armLength, 0) + player.position;
        float rotateAngle = (angle * Mathf.Rad2Deg) + 270;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotateAngle));
    }


}
