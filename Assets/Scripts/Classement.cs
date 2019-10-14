using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

class Classement 
{
    public string Position { get; set; }

    public string Score { get; set; }

    public string Nom { get; set; }

    public Classement(string position, string score, string nom)
    {
        this.Position = position;
        this.Score = score;
        this.Nom = nom;
    }
}
