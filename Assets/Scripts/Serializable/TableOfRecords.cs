using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class TableOfRecords
{
    private static TableOfRecords instance;
    private static TableOfRecords globalInstance;

    public static TableOfRecords OwnRecords()
    {
        if (instance == null)
        {
            instance = new TableOfRecords("own_records", 5);
        }
        return instance;
    }

    public static TableOfRecords GlobalRecords()
    {
        if (instance == null)
        {
            instance = new TableOfRecords("global_records", 10);
        }
        return instance;
    }

    public List<GameRecord> Records
    {
        get
        {
            return gameRecords;
        }
    }

    private List<GameRecord> gameRecords;
    private string storePath;
    private int maxElements;

    public TableOfRecords(string fileName, int maxItems = 5)
    {
        maxElements = maxItems;
        gameRecords = new List<GameRecord>(maxElements);
        storePath = Application.persistentDataPath + "/" + fileName + ".db";
        load();
    }

    public void AddRecord(GameRecord record)
    {
        gameRecords.Add(record);
        gameRecords.Sort((a, b) => b.Score - a.Score);
        if (gameRecords.Count > maxElements)
        {
            gameRecords.RemoveAt(maxElements);
        }
        persist();
    }

    public void ClearAll()
    {
        gameRecords.Clear();
        persist();
    }

    private void persist()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream;
        try
        {
            fileStream = File.Create(storePath);
            binaryFormatter.Serialize(fileStream, gameRecords);
            fileStream.Close();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Cannot store record table");
            Debug.LogError(ex.ToString());
        }
    }

    private void load()
    {
        if (File.Exists(storePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(storePath, FileMode.Open);
            gameRecords = (List<GameRecord>)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }
    }
}
