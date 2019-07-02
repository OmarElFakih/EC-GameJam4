using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesTesting : MonoBehaviour
{
    public new SpriteRenderer renderer;
    public float valueOffset;
    public float currentValue;
    public float intervals;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Vector2 addedValue = new Vector2(currentValue + intervals + valueOffset,2.02f);

            renderer.size = addedValue;

            currentValue += intervals;
        }
    }
}
