using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry
{
    public Entry() { FillID("NONE", "NONE"); }
    public void FillID(string _id, string _txt)
    {
        id = _id;
        txt = _txt;
        split = _id.Split('_');
    }
    
    private string[] split = new string[5];

    private string id;
    public string ID { get { return id; } }

    public string Scene { get { return "Scene_" + split[0]; } }
    public string Animal { get { return split[1]; } }
    public string Inflection { get { return split[2]; } }
    public string Type { get { return split[3] + "_" + split[4]; } }


    private string txt;
    public string Text { get { return txt; } }

}

public class CSVParser : MonoBehaviour
{
    public string csv_name;
    private static string[] data;
    private void Awake()
    {
        TextAsset csv = (TextAsset)Resources.Load(csv_name);
        print(csv.text);
        data = csv.text.Split('\n');
    }

    public static Entry GetKey(string code)
    {
        Entry to_ret = new Entry();
        foreach(string c in data)
        {
            //TODO FIX COMMA
            string[] split = c.Split('\t');
            if(code == split[0])
            {
                to_ret.FillID(split[0], split[1]);
                break;
            }
        }

        return to_ret;
    }


}
