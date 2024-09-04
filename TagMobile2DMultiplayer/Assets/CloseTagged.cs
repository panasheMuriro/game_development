using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTagged : MonoBehaviour
{
    public void Close()
    {
        TaggedManager.instance.HideTagged();
    }
}
