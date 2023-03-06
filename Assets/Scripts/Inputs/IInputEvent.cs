using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputEvent
{
    string btnID { get; set; }
    void HitMe();
}
