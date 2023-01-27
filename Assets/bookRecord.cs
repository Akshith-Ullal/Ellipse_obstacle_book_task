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
    public GameObject grabObj;
    public GameObject grabObjgreen;
    public GameObject grabObjblue;
    public GameObject endBox;
    public GameObject endBoxblue;
    public GameObject endBoxgreen;
    public bool isRecordinggreen = false;
    public bool isRecordingblue = false;
    private double startTime;
    private Vector3 startPos;
    private Quaternion startRot;
    private List<PositionEntryinfo> entries = new List<PositionEntryinfo>();
    public float startDisgreen;
    public float endDisgreen;
    public float startDisblue;
    public float endDisblue;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        startDisgreen = Vector3.Distance(grabObjgreen.transform.position, endBoxgreen.transform.position);
        endDisgreen = Vector3.Distance(grabObjgreen.transform.position, endBoxblue.transform.position);
        startDisblue = Vector3.Distance(grabObjblue.transform.position, endBoxblue.transform.position);
        endDisblue = Vector3.Distance(grabObjblue.transform.position, endBoxgreen.transform.position);

        if(startDisgreen >= 0.2)
        {
            isRecordinggreen = true;
            StartRecording();
        }

        if (startDisgreen >= 0.2)
        {
            isRecordinggreen = true;
            StartRecording();
        }

        if(endDisgreen <= 0.2)
        {
            StopAndSaveRecording();
            isRecordinggreen = false;
        }
        if (endDisblue <= 0.2)
        {
            StopAndSaveRecording();
            isRecordingblue = false;
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

    }

    public void StartRecording()
    {

        if (isRecordinggreen)
        {
            Debug.Log("Start Recording green book");
        }
        if (isRecordingblue)
        {
            Debug.Log("Start Recording blue book");
        }
        

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
        }
        if (isRecordingblue)
        {
             filename = "PositionRecording_blue_book" + this.name + "_";
        }
        //string path = Application.persistentDataPath + "/pathOfDevice";
        string path = @"C:\Users\liuj58\MRTK\Assets\savedData";

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
