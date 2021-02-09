using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JsonController : MonoBehaviour
{
    [SerializeField] InputField m_jsonField = null;

    public void Serialize()
    {
        MyClass my = new MyClass(10, "Hero", 255);
        Debug.Log(my.ToString());
        string json = JsonUtility.ToJson(my);
        m_jsonField.text = json;
    }

    public void Deserialize()
    {
        string json = m_jsonField.text;
        var my = JsonUtility.FromJson<MyClass>(json);
        Debug.Log(my.ToString());
    }
}
