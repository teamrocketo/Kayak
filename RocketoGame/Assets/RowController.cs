using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool right = true;

    public void OnRow(bool right)
    {
        if(right != this.right)
        {
            this.right = right;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
        }

        animator.SetBool("row", true);
    }

    private void Update()
    {
        animator.SetBool("row", false);
    }
}
