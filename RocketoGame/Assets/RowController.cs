using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
    [SerializeField]
    private Animation animation;

    private bool right = true;

    public void OnRow(bool right)
    {
        if(right != this.right)
        {
            this.right = right;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
        }

        animation.Play();
    }
}
