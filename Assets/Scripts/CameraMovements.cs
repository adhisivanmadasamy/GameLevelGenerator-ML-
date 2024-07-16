using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveForward()
    {
        this.gameObject.transform.position = new Vector3(Mathf.Clamp((this.gameObject.transform.position.x - 10f), 10f, 125f), this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }

    public void MoveBackward()
    {
        this.gameObject.transform.position = new Vector3(Mathf.Clamp((this.gameObject.transform.position.x + 10f), 10f, 125f), this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }

    public void MoveLeft()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, Mathf.Clamp((this.gameObject.transform.position.z - 10f), -50f, 150f));
    }

    public void MoveRight()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, Mathf.Clamp((this.gameObject.transform.position.z + 10f), -50f, 150f));
    }

    public void ZoomIn()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, Mathf.Clamp((this.gameObject.transform.position.y - 10f), 20f, 120f), this.gameObject.transform.position.z);

    }

    public void ZoomOut()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, Mathf.Clamp((this.gameObject.transform.position.y + 10f), 20f, 120f), this.gameObject.transform.position.z);
    }

    public void Resetpos()
    {
        this.gameObject.transform.position = new Vector3(125f, 120f, 60f);
    }

}
