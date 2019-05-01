using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfPort {drain, charge }
public class ChargingPort : MonoBehaviour {
    
    public Transform LockPosition;

    public typeOfPort port;

    CommsRelay comms;
}
