using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Int_of_bullet : MonoBehaviour
{
	public float speed = 10f; // скорость пули
	public Rigidbody2D bullet; // префаб нашей пули
	public Transform gunPoint; // точка рождения
	public float fireRate = 1; // скорострельность

	private float curTimeout;
	
	void Start()
	{
	}
/*
	void SetRotation()
	{
		Vector3 mousePosMain = Input.mousePosition;
		mousePosMain.z = Camera.main.transform.position.z; 
		Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePosMain);
		lookPos = lookPos - transform.position;
		float angle  = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		angle = Mathf.Clamp(angle, minAngle, maxAngle);
		zRotate.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}*/
	
	void Update()
	{
		if(Input.GetKey(KeyCode.UpArrow))
			Fire(1);
        else if(Input.GetKey(KeyCode.RightArrow))
			Fire(2);
        else if(Input.GetKey(KeyCode.DownArrow))
			Fire(3);
        else if(Input.GetKey(KeyCode.LeftArrow))
			Fire(4);
        
		else
		{
			curTimeout = 100;
		}

		//if(zRotate) SetRotation();
	}

	void Fire(int where)
	{
		curTimeout += Time.deltaTime;
		if(curTimeout > fireRate)
		{
			curTimeout = 0;
			Rigidbody2D clone = Instantiate(bullet, gunPoint.position, Quaternion.identity) as Rigidbody2D;
            if (where == 1){
                clone.velocity = transform.TransformDirection(gunPoint.up * speed);
                clone.transform.up = gunPoint.up;
            }
            else if (where == 2){
                clone.velocity = transform.TransformDirection(gunPoint.right * speed);
                clone.transform.right = gunPoint.right;
            }
            else if (where == 3){
                //clone.transform.Rotate(0, 180, 0);
                clone.velocity = transform.TransformDirection(-gunPoint.up * speed);
                clone.transform.up = gunPoint.up;
            }
            else if (where == 4){
                clone.velocity = transform.TransformDirection(-gunPoint.right * speed);
                clone.transform.right = gunPoint.right;
            }

		}
	}

    
}
