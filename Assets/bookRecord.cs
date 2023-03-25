using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using Newtonsoft.Json;

public struct PositionEntryinfo
{
    public double timeStamp;
    public Vector3 position;
    public Quaternion rotation;
}

public class bookRecord : MonoBehaviour
{
    public GameObject worldAnchor;
    public GameObject greentable;
    public GameObject bluetable;
    //public GameObject grabObj;
    public GameObject grabObjgreen;
    public GameObject grabObjblue;
   // public GameObject endBox;
    public GameObject endBoxblue;
    public GameObject endBoxgreen;
    public GameObject startgreen;
    public GameObject endgreen;
    public GameObject startblue;
    public GameObject endblue;
    public bool startRecordinggreen = false;
    public bool startRecordingblue = false;
    public bool isRecordinggreen = false;
    public bool isRecordingblue = false;
    public bool endRecordinggreen = false;
    public bool endRecordingblue = false;
    public bool blueboxinout;
    public bool greeninout;
    private double startTime;
    private Vector3 startPos;
    private Quaternion startRot;
    private List<PositionEntryinfo> entries = new List<PositionEntryinfo>();
    public float startDisgreen;
    public float endDisgreen;
    public float startDisblue;
    public float endDisblue;
    public float startDisgreen_hmd;
    public float endDisgreen_hmd;
    public float startDisblue_hmd;
    public float endDisblue_hmd;
    public bool trialdone = false;
    public int trialnum;
    public int triallimit;
    public bool bluetrialdone;
    public bool greentrialdone;
    public float distancelimit;



    // These are variables for shifting the positions of the table randomly after each trial  
    public float radius;
    public float xorigin;
    public float zorigin;
    public float xgreen;
    public float zgreen;
    public float xblue;
    public float zblue;
    public float next_angle = 135.0f;
    public float next_angle_blue = 0.0f;
    public bool green_angle_flag = false;
    System.Random rnd = new System.Random();
    private Vector3 bluebookinitrelpos; // used to store the initial relative position of the blue book to the table
    private Vector3 greenbookinitrelpos; // used to store the initial relative position of the green book to the table
    private Vector3 blueboxinitrelpos; // used to store the initial relative position of the blue box to the table
    private Vector3 greenboxinitrelpos; // used to store the initial relative position of the green box to the table
    private Vector3 startgreenpos;
    private Vector3 endgreenpos;
    private Vector3 startbluepos;
    private Vector3 endbluepos;
    private Quaternion bluebookinitrelrot; // used to store the initial relative rotation of the blue book to the table
    private Quaternion greenbookinitrelrot; // used to store the initial relative rotation of the green book to the table
    private Quaternion blueboxinitrelrot; // used to store the initial relative rotation of the blue box to the table
    private Quaternion greenboxinitrelrot; // used to store the initial relative rotation of the green box to the table

    public int id;



    // Start is called before the first frame update
    void Start()
    {
        trialdone = false;
        bluetrialdone = false;
        greentrialdone = false;
        distancelimit = 2.0f;
        trialnum = 0;
        triallimit = 10;
        next_angle = 135.0f;
        next_angle_blue = 0.0f;

        radius = 5.0f;
        xorigin = 0.0f;
        zorigin = 0.0f;


        bluebookinitrelpos = grabObjblue.transform.localPosition;
        greenbookinitrelpos = grabObjgreen.transform.localPosition;
        blueboxinitrelpos = endBoxblue.transform.localPosition;
        greenboxinitrelpos = endBoxgreen.transform.localPosition;
        bluebookinitrelrot = grabObjblue.transform.localRotation;
        greenbookinitrelrot = grabObjgreen.transform.localRotation;
        blueboxinitrelrot = endBoxblue.transform.localRotation;
        greenboxinitrelrot = endBoxgreen.transform.localRotation;

        startgreenpos = startgreen.transform.localPosition;
        endgreenpos = endgreen.transform.localPosition;
        startbluepos = startblue.transform.localPosition;
        endbluepos = endblue.transform.localPosition;


        xgreen = xorigin + Mathf.Sin(next_angle * Mathf.Deg2Rad) * radius;
        zgreen = zorigin + Mathf.Cos(next_angle * Mathf.Deg2Rad) * radius;
        greentable.transform.position = new Vector3(xgreen, -2.0f, zgreen);
        Vector3 pos = new Vector3(worldAnchor.transform.position.x, -2.0f, worldAnchor.transform.position.z);
        greentable.transform.LookAt(pos);


        xblue = xorigin + Mathf.Sin(next_angle_blue * Mathf.Deg2Rad) * radius;
        zblue = zorigin + Mathf.Cos(next_angle_blue * Mathf.Deg2Rad) * radius;
        bluetable.transform.position = new Vector3(xblue, -2.0f, zblue);
        bluetable.transform.LookAt(pos);



        startblue.SetActive(false);
        endblue.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {


        startDisgreen = Vector3.Distance(grabObjgreen.transform.position, endBoxblue.transform.position);
        endDisgreen = Vector3.Distance(grabObjgreen.transform.position, endBoxgreen.transform.position);
        startDisblue = Vector3.Distance(grabObjblue.transform.position, endBoxgreen.transform.position);
        endDisblue = Vector3.Distance(grabObjblue.transform.position, endBoxblue.transform.position);

        startDisgreen_hmd = Vector3.Distance(transform.position, endBoxblue.transform.position);
        endDisgreen_hmd = Vector3.Distance(transform.position, endBoxgreen.transform.position);
        startDisblue_hmd = Vector3.Distance(transform.position, endBoxgreen.transform.position);
        endDisblue_hmd = Vector3.Distance(transform.position, endBoxblue.transform.position);




        if (Input.GetKeyDown(KeyCode.C))
        {
            //Debug.Log("startDisgreen : " + startDisgreen);
            //Debug.Log("endDisgreen : " + endDisgreen);
            //Debug.Log("startDisblue : " + startDisblue);
            //Debug.Log("endDisblue : " + endDisblue);

            // These lines are to manually return the box and the books to their original relative positions
            grabObjgreen.transform.localPosition = greenbookinitrelpos;
            grabObjgreen.transform.localRotation = greenbookinitrelrot;
            endBoxgreen.transform.localPosition = greenboxinitrelpos;
            endBoxgreen.transform.localRotation = greenboxinitrelrot;

            grabObjblue.transform.localPosition = bluebookinitrelpos; 
            grabObjblue.transform.localRotation = bluebookinitrelrot;
            endBoxblue.transform.localPosition = blueboxinitrelpos;
            endBoxblue.transform.localRotation = blueboxinitrelrot;

            startblue.transform.localPosition = startbluepos;
            endblue.transform.localPosition = endbluepos;

            startgreen.transform.localPosition = startgreenpos;
            endgreen.transform.localPosition = endgreenpos;
        }

        //if (startDisgreen >= distancelimit && !isRecordinggreen)
        if (startgreen.GetComponent<Collider>().bounds.Contains(transform.position) && !isRecordinggreen)
        {
            startRecordinggreen = true;
            
        }
        if (startRecordinggreen)
        {
            StartRecordinggreen();
            Debug.Log("Start Recording green book");
            startRecordinggreen = false;
            isRecordinggreen = true;
        }

        //if (startDisblue >= distancelimit && !isRecordingblue)
        if (startblue.GetComponent<Collider>().bounds.Contains(transform.position) && !isRecordingblue)
        {
            startRecordingblue = true;
            
        }
        if (startRecordingblue)
        {
            StartRecordingblue();
            Debug.Log("Start Recording blue book");
            startRecordingblue = false;
            isRecordingblue = true;
        }
        //if (endDisgreen <= distancelimit && !isRecordinggreen)
        if (endgreen.GetComponent<Collider>().bounds.Contains(transform.position) && !endRecordinggreen)
        {
            StopAndSaveRecording();
            endRecordinggreen = true;
            isRecordinggreen = false;
            greentrialdone = true;

            startblue.SetActive(true);
            endblue.SetActive(true);

            startgreen.SetActive(false);
            endgreen.SetActive(false);

        }
        //if (endDisblue <= distancelimit && !endRecordingblue)
            if (endblue.GetComponent<Collider>().bounds.Contains(transform.position) && !endRecordingblue)
            {
            StopAndSaveRecording();
            endRecordingblue = true;
            isRecordingblue = false;
            bluetrialdone = true;
        }

        if (isRecordinggreen || isRecordingblue)
        {

            PositionEntryinfo entry;
            //offset from start position
            // when using the hololens
            entry.position = this.gameObject.transform.position - startPos;

            

            //quaternion diff such that startRot * diff = gameObject.transform.rotation
            entry.rotation = QuatDiff(startRot, this.gameObject.transform.rotation);

            //offset from start time
            entry.timeStamp = Time.time - startTime;

            entries.Add(entry);

        }

        if(greentrialdone && bluetrialdone)
        {
            trialdone = true;
            greentrialdone = false;
            bluetrialdone = false;

           

            
        }

        if (trialdone && trialnum < triallimit)
        {
            Debug.Log("Both trials done");
            trialnum++;




            startblue.SetActive(true);
            endblue.SetActive(true);
            startgreen.SetActive(true);
            endgreen.SetActive(true);






            // shifting the tables to random angles after each trial is done
            next_angle = rnd.Next(0, 360);
                Debug.Log("Next angle green :" + next_angle);


           
            xgreen = xorigin + Mathf.Sin(next_angle * Mathf.Deg2Rad) * radius;
            zgreen = zorigin + Mathf.Cos(next_angle * Mathf.Deg2Rad) * radius;




            

            greentable.transform.position = new Vector3(xgreen, -2.0f, zgreen);
            Vector3 pos = new Vector3(worldAnchor.transform.position.x, -2.0f, worldAnchor.transform.position.z);
            greentable.transform.LookAt(pos);

           

            grabObjgreen.transform.localPosition = greenbookinitrelpos;
            grabObjgreen.transform.localRotation = greenbookinitrelrot;
            endBoxgreen.transform.localPosition = greenboxinitrelpos;
            endBoxgreen.transform.localRotation = greenboxinitrelrot;

            //startgreen.transform.localPosition = startgreenpos;
            //endgreen.transform.localPosition = endgreenpos;



            //next_angle_blue = next_angle_method(next_angle);
            next_angle_blue = next_angle+ rnd.Next(90, 270);
            next_angle_blue = next_angle_blue % 360.0f;
            Debug.Log("Next angle blue :" + next_angle_blue);

           

            xblue = xorigin + Mathf.Sin(next_angle_blue * Mathf.Deg2Rad) * radius;
            zblue = zorigin + Mathf.Cos(next_angle_blue * Mathf.Deg2Rad) * radius;




           

            bluetable.transform.position = new Vector3(xblue, -2.0f, zblue);
            
            bluetable.transform.LookAt(pos);
           



            grabObjblue.transform.localPosition = bluebookinitrelpos;
            grabObjblue.transform.localRotation = bluebookinitrelrot;
            endBoxblue.transform.localPosition = blueboxinitrelpos;
            endBoxblue.transform.localRotation = blueboxinitrelrot;


            //startblue.transform.localPosition = startbluepos;
            //endblue.transform.localPosition = endbluepos;




            startblue.SetActive(false);
            endblue.SetActive(false);

            trialdone = false;
            endRecordinggreen = false;
            endRecordingblue = false;
          
        }

        if(trialnum >= triallimit)
        {
            Debug.Log("All trials done");
            // when all trials done deactivate all the objects
            startblue.SetActive(false);
            endblue.SetActive(false);
            startgreen.SetActive(false);
            endgreen.SetActive(false);
        }

    }

    public void StartRecordinggreen()
    {

        
       
            startTime = Time.time;
            startPos = worldAnchor.transform.position;
            startRot = worldAnchor.transform.rotation;
             
        

        //The commented lines were in the original code
        //if (isRecording)
        //{
        //    return;
        //}
        //else
        //{
        //    isRecording = true;

        //    startTime = Time.time;
        //    startPos = worldAnchor.transform.position;
        //    startRot = worldAnchor.transform.rotation;
        //}

           
    }
    public void StartRecordingblue()
    {
        
            startTime = Time.time;
            startPos = worldAnchor.transform.position;
            startRot = worldAnchor.transform.rotation;
        
    }


        public void StopAndSaveRecording()
    {


        // stop recording
        //isRecording = false;
        string filename = "";
        //save to file
        if (isRecordinggreen) {
             filename = "p_" + id + "_PositionRecording_green_book_trial_num_"+(trialnum+1).ToString() +"_table_angle_"+next_angle.ToString()  + "_radius_"+radius;
            Debug.Log("Green book position recorded");
        }
        if (isRecordingblue)
        {
             filename = "p_" + id + "_PositionRecording_blue_book_trial_num_" + (trialnum + 1).ToString()+"_table_angle_" + next_angle_blue.ToString() + "_radius_" + radius;
            Debug.Log("Blue book position recorded");
        }
        //string path = Application.persistentDataPath + "/pathOfDevice";
        string path = @"C:\Users\ullala\Desktop\ellipse_test_paths\book_task_paths";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string participant_path = @"C:\Users\ullala\Desktop\ellipse_test_paths\book_task_paths\" + "p_" + id + "/" + "path";
        if (!Directory.Exists(participant_path))
        {
            Directory.CreateDirectory(participant_path);
        }

        

        int inc = 1;

        while (File.Exists(participant_path + "/" + filename + inc.ToString() + ".json"))
        {
            inc++;
            Debug.Log("exists");
        }

        string filepath = participant_path + "/" + filename + inc.ToString() + ".json";

        File.WriteAllText(filepath, JsonConvert.SerializeObject(entries, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));

        Debug.Log(filepath);

        //clear entries for next recording
        entries.Clear();
    }
        
    



    // provides a quaterion, diff, such that a * diff = b
    private Quaternion QuatDiff(Quaternion a, Quaternion b)
    {
        return Quaternion.Inverse(a) * b;
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
