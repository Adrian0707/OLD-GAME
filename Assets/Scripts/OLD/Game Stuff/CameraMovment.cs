using System.Collections;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    [Header("Position wariables")]
    public Transform target;
    public float smoothing;
    public Vector3 maxPosition;
    public Vector3 minPosition;
    [Header("Animator")]
    public Animator anim;
    [Header("Position Reset not in use")]
    public ventorValue camMin;
    public ventorValue camMax;

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target && transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x,target.position.y,transform.position.z);

           // targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            //targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);


            transform.position = Vector3.Lerp(transform.position, targetPosition,smoothing);
        }

    }
    public void BeingKick()
    {
        anim.SetBool("kickActive", true);
        StartCoroutine(KickCo());
    }
    public IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("kickActive", false);
    }
}
