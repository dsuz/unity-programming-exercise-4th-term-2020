using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;    // ファイル入出力のための名前空間

public class FileIOController : MonoBehaviour
{
    [SerializeField] InputField m_fileName = null;
    [SerializeField] InputField m_data = null;
    [SerializeField] Transform m_ContentPanel = null;
    [SerializeField] Button m_buttonPrefab = null;

    public void CreateFile()
    {
        string filePath = GetFilePath();
        Debug.Log($"ファイル '{filePath}' を作ります");
        File.Create(filePath);
    }

    public void Save()
    {
        using (var writer = new StreamWriter(GetFilePath()))
        {
            writer.Write(m_data.text);
        }
        // 課題: 上記では上書きモードでファイルを書き込んでいる。追記モードでファイルを書き込むにはどうすればよいか。
    }

    public void Load()
    {
        using (var reader = new StreamReader(GetFilePath()))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                Button button = Instantiate<Button>(m_buttonPrefab);
                button.transform.GetComponentInChildren<Text>().text = line;
                button.transform.SetParent(m_ContentPanel);
            }
        }
    }
    
    string GetFilePath()
    {
        // Unity の場合はどこでもファイルの読み書きができるわけではないことに注意。Application.persistentDataPath を使って「読み書きできるところ」でファイル操作をすること。
        string filePath = Application.persistentDataPath + "\\" + (m_fileName.text == "" ? Application.productName : m_fileName.text) + ".txt";
        return filePath;
    }
}
