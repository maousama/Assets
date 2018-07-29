using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XmlManager : Singleton<XmlManager> {


    public int id;
    public List<int> Allkeys = new List<int>();
    public Dictionary<int, UsernameInfo> Allinfos = new Dictionary<int, UsernameInfo>();
    private void Awake()
    {
        GetInfo();
    }
    public void GetInfo()
    {
        string path = Application.dataPath + "/Resources/Xmls/UserCharacterInfo/S.xml";
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode usernames = doc.SelectSingleNode("usernames");
        XmlNodeList usernameList = usernames.ChildNodes;

        for (int i = 0; i < usernameList.Count; i++)
        {
            XmlNode username = usernameList[i];
            for (int j = 0; j < username.ChildNodes.Count; j++)
            {
                XmlNode player = username.ChildNodes[j];
                for (int k = 0; k< player.ChildNodes.Count; k++)
                {
                    XmlElement xmlElement = player.ChildNodes[k] as XmlElement;
                    Debug.Log(xmlElement.FirstChild.Value);
                }
               
            }
        }
        //foreach (XmlNode player in username)
        //{
        //    XmlNodeList iteminfo = player.ChildNodes;
        //    int ID = int.Parse(iteminfo[0].InnerText);
        //    string Name = iteminfo[1].InnerText;
        //    UsernameInfo b = new UsernameInfo(ID, Name);
        //    Debug.Log(ID);
        //    Debug.Log(Name);
        //    Allkeys.Add(ID);
        //    Allinfos.Add(ID, b);
        //}

    }
    public UsernameInfo GetInfoByID(int ID)
    {

        this.id = ID;
        UsernameInfo infos;
        Allinfos.TryGetValue(this.id, out infos);
        return infos;

    }
}
