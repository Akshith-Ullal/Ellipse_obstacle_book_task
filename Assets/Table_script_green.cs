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
    public GameObject grabObjgreen;
    public GameObject grabObjblue;
    public GameObject endBoxblue;
    public GameObject endBoxgreen;
    public float next_angle = 180.0f;
    public float next_angle_blue = 0.0f;
    public bool green_angle_flag = false;
    System.Random rnd = new System.Random();
    public Vector3 bluebookinitrelpos; // used to store the initial relative position of the blue book to the table
    public Vector3 greenbookinitrelpos; // used to store the initial relative position of the green book to the table
    public Vector3 blueboxinitrelpos; // used to store the initial relative position of the blue box to the table
    public Vector3 greenboxinitrelpos; // used to store the initial relative position of the green box to the table

    void Start()
    {
        radius = 10.0f;
        xorigin = 0.0f;
        zorigin = 0.0f;


        //bluebookinitrelpos = grabObjblue.transform.localPosition;
        //greenbookinitrelpos = grabObjgreen.transform.localPosition;
        //blueboxinitrelpos = endBoxblue.transform.localPosition;
        //greenboxinitrelpos = endBoxgreen.transform.localPosition;





        if (this.gameObject.name == "Table_green_box")
        {
            next_angle = rnd.Next(0, 360);
            Debug.Log("Start angle green :" + next_angle);
            x = xorigin + Mathf.Sin(next_angle * Mathf.Deg2Rad) * radius;
            z = zorigin + Mathf.Cos(next_angle * Mathf.Deg2Rad) * radius;

             grabObjgreen.transform.localPosition = greenbookinitrelpos;
             endBoxgreen.transform.localPosition = greenboxinitrelpos;

            this.gameObject.transform.position = new Vector3(x, 0.0f, z);
            this.gameObject.transform.rotation = Quaternion.Euler(0, next_angle, 0);
        }
        else
        {
            next_angle_blue = next_angle_method(next_angle);
            Debug.Log("Start angle blue :" + next_angle_blue);
            x = xorigin + Mathf.Sin(next_angle_blue * Mathf.Deg2Rad) * radius;
            z = zorigin + Mathf.Cos(next_angle_blue * Mathf.Deg2Rad) * radius;

            grabObjblue.transform.localPosition = bluebookinitrelpos;
            endBoxblue.transform.localPosition = blueboxinitrelpos;

            this.gameObject.transform.position = new Vector3(x, 0.0f, z);
            this.gameObject.transform.rotation = Quaternion.Euler(0, next_angle_blue, 0);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            

            
           



            if (this.gameObject.name == "Table_green_box")
            {
                next_angle = rnd.Next(0, 360);
                Debug.Log("Next angle green :" + next_angle);

                x = xorigin + Mathf.Sin(next_angle * Mathf.Deg2Rad) * radius;
                z = zorigin + Mathf.Cos(next_angle * Mathf.Deg2Rad) * radius;

                //grabObjgreen.transform.localPosition = greenbookinitrelpos;
                //endBoxgreen.transform.localPosition = greenboxinitrelpos;


                this.gameObject.transform.position = new Vector3(x, 0.0f, z);
                this.gameObject.transform.rotation = Quaternion.Euler(0, next_angle, 0);
            }
            else
            {
                next_angle_blue = next_angle_method(next_angle);
                Debug.Log("Next angle blue :" + next_angle_blue);

                x = xorigin + Mathf.Sin(next_angle_blue * Mathf.Deg2Rad) * radius;
                z = zorigin + Mathf.Cos(next_angle_blue * Mathf.Deg2Rad) * radius;

                //grabObjblue.transform.localPosition = bluebookinitrelpos;
                //endBoxblue.transform.localPosition = blueboxinitrelpos;

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

