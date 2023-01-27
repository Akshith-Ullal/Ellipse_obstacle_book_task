using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Table_script_green : MonoBehaviour
{
    // Start is called before the first frame update

    public float radius;
    public float xorigin;
    public float zorigin;
    public float x;
    public float z;
    public float next_angle = 180.0f;
    public float next_angle_blue = 0.0f;
    public bool green_angle_flag = false;
    System.Random rnd = new System.Random();

    void Start()
    {
        radius = 10.0f;
        xorigin = 0.0f;
        zorigin = 0.0f;

        
        
        




        if (this.gameObject.name == "Table_green_box")
        {
            next_angle = rnd.Next(0, 360);
            Debug.Log("Start angle green :" + next_angle);
            x = xorigin + Mathf.Sin(next_angle * Mathf.Deg2Rad) * radius;
            z = zorigin + Mathf.Cos(next_angle * Mathf.Deg2Rad) * radius;

            this.gameObject.transform.position = new Vector3(x, 0.0f, z);
            this.gameObject.transform.rotation = Quaternion.Euler(0, next_angle, 0);
        }
        else
        {
            next_angle_blue = next_angle_method(next_angle);
            Debug.Log("Start angle blue :" + next_angle_blue);
            x = xorigin + Mathf.Sin(next_angle_blue * Mathf.Deg2Rad) * radius;
            z = zorigin + Mathf.Cos(next_angle_blue * Mathf.Deg2Rad) * radius;

            this.gameObject.transform.position = new Vector3(x, 0.0f, z);
            this.gameObject.transform.rotation = Quaternion.Euler(0, next_angle_blue, 0);
        }


    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        {
            
            

            
           



            if (this.gameObject.name == "Table_green_box")
            {
                next_angle = rnd.Next(0, 360);
                Debug.Log("Next angle green :" + next_angle);

                x = xorigin + Mathf.Sin(next_angle * Mathf.Deg2Rad) * radius;
                z = zorigin + Mathf.Cos(next_angle * Mathf.Deg2Rad) * radius;

                this.gameObject.transform.position = new Vector3(x, 0.0f, z);
                this.gameObject.transform.rotation = Quaternion.Euler(0, next_angle, 0);
            }
            else
            {
                next_angle_blue = next_angle_method(next_angle);
                Debug.Log("Next angle blue :" + next_angle_blue);

                x = xorigin + Mathf.Sin(next_angle_blue * Mathf.Deg2Rad) * radius;
                z = zorigin + Mathf.Cos(next_angle_blue * Mathf.Deg2Rad) * radius;

                this.gameObject.transform.position = new Vector3(x, 0.0f, z);
                this.gameObject.transform.rotation = Quaternion.Euler(0, next_angle_blue, 0);
            }
        }
    }

public float next_angle_method(float start_angle)
    {
        if (start_angle > 0 && start_angle <= 90)
        {
            next_angle_blue = rnd.Next(110, 340);
        }
        else if (start_angle > 90 && start_angle <= 180)
        {
            next_angle_blue = rnd.Next(200, 430);
        }
        else if (start_angle > 180 && start_angle <= 270)
        {
            next_angle_blue = rnd.Next(290, 520);
        }
        else
        {
            next_angle_blue = rnd.Next(20, 250);
        }

        next_angle_blue = next_angle_blue % 360.0f;

        return next_angle_blue;
    }


}

