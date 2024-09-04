using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FishNet.Transporting.Bayou;
using FishNet.Transporting.Multipass;
using FishNet.Transporting.Tugboat;

public class Startup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Multipass mp = GetComponent<Multipass>();

#if UNITY_WEBGL && !UNITY_EDITOR
        mp.SetClientTransport<Bayou>();

#else
        mp.SetClientTransport<Tugboat>();
#endif

    }

    // Update is called once per frame
    void Update()
    {

    }
}
