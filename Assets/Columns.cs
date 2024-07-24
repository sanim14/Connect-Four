using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;

public class Columns : MonoBehaviour
{
    [SerializeField] Columns col;
    [SerializeField] GameObject coin;
    [SerializeField] int columnNumber;
    [SerializeField] Spawner spawner;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnMouseDown()
    {
        spawner.ColumnClicked(columnNumber);
    }
}