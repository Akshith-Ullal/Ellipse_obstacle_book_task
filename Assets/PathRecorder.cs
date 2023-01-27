using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using Newtonsoft.Json;

public struct PositionEntry
{
    public double timeStamp;
    public Vector3 position;
    public Quaternion rotation;
}

public class PathRecorder : MonoBehaviour
{
    public GameObject worldAnchor;
    private bool isRecording = false;
    private double startTime;
    private Vector3 startPos;
    private Vector3 startPos2D;
    private Quaternion startRot;
    private List<PositionEntry> entries = new List<PositionEntry>();
    private List<PositionEntry> entries2D = new List<PositionEntry>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isRecording)
        {

            PositionEntry entry;
            PositionEntry entry2D;
            //offset from start position
            entry.position = this.gameObject.transform.position - startPos;
            entry2D.position = new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z) - startPos2D;

            //quaternion diff such that startRot * diff = gameObject.transform.rotation
            entry.rotation = QuatDiff(startRot, this.gameObject.transform.rotation);
            entry2D.rotation = QuatDiff(startRot, this.gameObject.transform.rotation);

            //offset from start time
            entry.timeStamp = Time.time - startTime;
            entry2D.timeStamp = Time.time - startTime;

            entries.Add(entry);
            entries2D.Add(entry2D);

        }
    }

    public void StartRecording()
    {

        if (isRecording)
        {
            return;
        }
        else
        {
            isRecording = true;

            startTime = Time.time;
            startPos = worldAnchor.transform.position;
            // recording 2D position co-ordinates
            startPos2D = new Vector3(worldAnchor.transform.position.x, 0, worldAnchor.transform.position.z);
            startRot = worldAnchor.transform.rotation;
        }
    }

    public void StopAndSaveRecording()
    {
        if (isRecording)
        {
            // stop recording
            isRecording = false;

            //save to file
            string filename = "PositionRecording_" + this.name + "_";
            //string path = Application.persistentDataPath + "/pathOfDevice";
            //string path = @"C:\Users\liuj58\MRTK\Assets\savedData";
            string path = @"C:\Users\ullala\Desktop\ellipse_book_task_paths";

            Directory.CreateDirectory(path);

            int inc = 1;

            while (File.Exists(path + "/" + filename + inc.ToString() + ".json"))
            {
                inc++;
                Debug.Log("exists");
            }

            string filepath = path + "/" + filename + inc.ToString() + ".json";
            string filepath_2D = path + "/" + filename + inc.ToString() + "_2D.json";

            File.WriteAllText(filepath, JsonConvert.SerializeObject(entries, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));

            File.WriteAllText(filepath_2D, JsonConvert.SerializeObject(entries2D, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));

            Debug.Log(filepath);
            Debug.Log(filepath_2D);

            //clear entries for next recording
            entries.Clear();
            entries2D.Clear();
        }
    }



    // provides a quaterion, diff, such that a * diff = b
    private Quaternion QuatDiff(Quaternion a, Quaternion b)
    {
        return Quaternion.Inverse(a) * b;
    }
}

