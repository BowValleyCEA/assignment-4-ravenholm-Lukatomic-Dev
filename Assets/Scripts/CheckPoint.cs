using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject FPSPlayer;

    [SerializeField] List<GameObject> checkPoints;

    [SerializeField] Vector3 vectorPoint;

    [SerializeField] float dead;

    // Update is called once per frame
    void Update()
    {
        if (FPSPlayer.transform.position.y < -dead)
        {
            FPSPlayer.transform.position = vectorPoint;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            vectorPoint = FPSPlayer.transform.position;
            Destroy(other.gameObject);
        }

    }
}