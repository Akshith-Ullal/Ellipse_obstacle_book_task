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
    //public GameObject grabObj;
    public GameObject grabObjgreen;
    public GameObject grabObjblue;
   // public GameObject endBox;
    public GameObject endBoxblue;
    public GameObject endBoxgreen;
    public bool startRecordinggreen = false;
    public bool startRecordingblue = false;
    public bool isRecordinggreen = false;
    public bool isRecordingblue = false;
    public bool endRecordinggreen = false;
    public bool endRecordingblue = false;
    private double startTime;
    private Vector3 startPos;
    private Quaternion startRot;
    private List<PositionEntryinfo> entries = new List<PositionEntryinfo>();
    public float startDisgreen;
    public float endDisgreen;
    public float startDisblue;
    public float endDisblue;
    public bool trialdone;
    public bool bluetrialdone;
    public bool greentrialdone;
    public float distancelimit;
  


    // Start is called before the first frame update
    void Start()
    {
        trialdone = false;
        bluetrialdone = false;
        greentrialdone = false;
        distancelimit = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {


        startDisgreen = Vector3.Distance(grabObjgreen.transform.position, endBoxblue.transform.position);
        endDisgreen = Vector3.Distance(grabObjgreen.transform.position, endBoxgreen.transform.position);
        startDisblue = Vector3.Distance(grabObjblue.transform.position, endBoxgreen.transform.position);
        endDisblue = Vector3.Distance(grabObjblue.transform.position, endBoxblue.transform.position);

        if(startDisgreen >= distancelimit && !isRecordinggreen)
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

        if (startDisblue >= distancelimit && !isRecordingblue)
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

        if (endDisgreen <= distancelimit && !endRecordinggreen)
        {
            StopAndSaveRecording();
            endRecordinggreen = true;
            isRecordinggreen = false;
            greentrialdone = true;
        }
        if (endDisblue <= distancelimit && !endRecordingblue)
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
            entry.position = this.gameObject.transform.position - startPos;

            //quaternion diff such that startRot * diff = gameObject.transform.rotation
            entry.rotation = QuatDiff(startRot, this.gameObject.transform.rotation);

            //offset from start time
            entry.timeStamp = Time.time - startTime;

            entries.Add(entry);

        }

        if(greentrialdone && bluetrialdone)
        {
            Debug.Log("Both trials done");
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
             filename = "PositionRecording_green_book" + this.name + "_";
            Debug.Log("Green book position recorded");
        }
        if (isRecordingblue)
        {
             filename = "PositionRecording_blue_book" + this.name + "_";
            Debug.Log("Blue book position recorded");
        }
        //string path = Application.persistentDataPath + "/pathOfDevice";
        string path = @"C:\Users\ullala\Documents\GitHub\Ellipse_obstacle_book_task\Assets\savedData";

        Directory.CreateDirectory(path);

        int inc = 1;

        while (File.Exists(path + "/" + filename + inc.ToString() + ".json"))
        {
            inc++;
            Debug.Log("exists");
        }

        string filepath = path + "/" + filename + inc.ToString() + ".json";

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
}
