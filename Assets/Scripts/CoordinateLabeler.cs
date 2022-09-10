using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TMP_Text label;
    Vector2Int coordinates = new Vector2Int();
    private void Awake()
    {
        label = GetComponent<TMP_Text>();
        DisplayCoordinates();
        UpdateObjectName();
    }
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
    }

    private void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / 10f);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / 10f);

        label.text = $"{coordinates.x}, {coordinates.y}";
    }
    public void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
