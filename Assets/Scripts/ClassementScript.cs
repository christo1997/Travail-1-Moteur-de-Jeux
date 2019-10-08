using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassementScript : MonoBehaviour
{
    public GameObject position;
    public GameObject score;
    public GameObject nom;

    public void SetScore(string position, string score, string nom)
    {
        this.position.GetComponent<Text>().text = position;
        this.score.GetComponent<Text>().text = score;
        this.nom.GetComponent<Text>().text = nom;
    }
} 

