using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.Networking;

static class DataBase
{
    private const string fileName = "db.bytes";
    private static string DBpath;
    private static SqliteConnection connection;
    private static SqliteCommand command;

    static DataBase()
    {
        DBpath = GetDatabasePath();
    }

    private static string GetDatabasePath()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (!File.Exists(filePath)) UnpackDatabase(filePath);
        return filePath;
    }

    private static void UnpackDatabase(string toPath)
    {
        string fromPath = Path.Combine(Application.persistentDataPath, fileName);

        using (UnityWebRequest reader = new(fromPath))
        {
            while (!reader.isDone) { }

            File.WriteAllBytes(toPath, reader.downloadHandler.data);
        }
    }

    private static void OpenConnection()
    {
        connection = new SqliteConnection("Data Source=" + DBpath);
        command = new SqliteCommand(connection);
        connection.Open();
    }
    public static void CloseConnection()
    {
        connection.Close();
        command.Dispose();
    }

    public static string ExecuteQueryWithAnswer(string query)
    {
        OpenConnection();
        command.CommandText = query;
        var answer = command.ExecuteScalar();
        
        CloseConnection();

        if (answer != null) return answer.ToString();
        else return null;
    }

    public static DataTable GetTable(string query)
    {
        OpenConnection();

        SqliteDataAdapter adapter = new(query, connection);

        DataSet DS = new();
        adapter.Fill(DS);
        adapter.Dispose();

        CloseConnection();

        return DS.Tables[0];
    }



}
