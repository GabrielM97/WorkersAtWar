using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRotation : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RotateLevel();
        }
    }

    public void RotateLevel()
    {
        gameObject.transform.Rotate(transform.up, 90f);
    }
}
