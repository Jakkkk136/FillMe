﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowAndShrink : MonoBehaviour
{
// Grow parameters
    public float approachSpeed = 0.02f;
    public float growthBound = 2f;
    public float shrinkBound = 0.5f;
    private float currentRatio = 1;
 
    // The text object we're trying to manipulate
    private Text text;
    private float originalFontSize;
 
    // And something to do the manipulating
    private Coroutine routine;
    private bool keepGoing = true;

    // Attach the coroutine
    void Awake () 
    {
        // Find the text  element we want to use
        this.text = this.gameObject.GetComponent<Text>();
 
        // Then start the routine
        this.routine = StartCoroutine(this.Pulse());
    }
 
    IEnumerator Pulse()
    {
        // Run this indefinitely
        while (keepGoing)
        {
            // Get bigger for a few seconds
            while (this.currentRatio != this.growthBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards( currentRatio, growthBound, approachSpeed);
 
                // Update our text element
                this.text.transform.localScale = Vector3.one * currentRatio;

                yield return new WaitForEndOfFrame();
            }
 
            // Shrink for a few seconds
            while (this.currentRatio != this.shrinkBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards( currentRatio, shrinkBound, approachSpeed);
 
                // Update our text element
                this.text.transform.localScale = Vector3.one * currentRatio;

                yield return new WaitForEndOfFrame();
            }
        }
    }

}
