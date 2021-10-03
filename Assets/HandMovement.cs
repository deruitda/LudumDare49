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
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -0.639f;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(player.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x);
        float xPos = Mathf.Cos(angle);
        float yPos = Mathf.Sin(angle);

        transform.position = new Vector3(xPos * _armLength, yPos * _armLength, 0) + player.position;
        float rotateAngle = (angle * Mathf.Rad2Deg) + 270;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotateAngle));
    }

    private float getMouseAngle()
    {
        return PlayerAim.GetMouseAngle(player);
    }


}
