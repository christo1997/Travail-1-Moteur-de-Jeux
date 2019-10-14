using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class ClassementManager : MonoBehaviour
{
    private string connectionString;

    private List<Classement> classement = new List<Classement>();

    public GameObject scorePrefab;

    public Transform scoreParent;

    void Start()
    {
        connectionString = "URI=file:" + Application.dataPath + "/StreamingAssets/Classement.sql";
        //connectionString = "URI=file: D:/Christopher/Téléchargement/Moteur de jeux/New Unity Project/Assets/StreamingAssets/StreamingAssets/Classement.sql";

        InsertScores("5", "125", "Joueur 5");
        //DeleteScore("Joueur 2");
        ShowScores();

    }

    private void GetScores()
    {
        classement.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {  
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM Classement";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        classement.Add(new Classement(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    private void InsertScores(string position, string score, string nom)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO Classement(Position, Score, Nom) VALUES (\"{0}\", \"{1}\", \"{2}\")", position, score, nom);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private void DeleteScore(string nom)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("SELECT FROM Classement WHERE Position = \"{0}\"", nom);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private void ShowScores()
    {
        GetScores();
        for (int i=0; i < classement.Count; i++)
        {
            GameObject tmpObjec = Instantiate(scorePrefab);

            Classement tmpScore = classement[i];

            tmpObjec.GetComponent<ClassementScript>().SetScore(tmpScore.Position, tmpScore.Score, tmpScore.Nom);

            tmpObjec.transform.SetParent(scoreParent);

            tmpObjec.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
}