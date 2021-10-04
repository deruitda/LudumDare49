using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndArea : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            _gameManager.Win();
        }
    }
}
