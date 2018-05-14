using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface that must be implemented by the parent of a child whose child's collision must warn the father.
public interface IChildsCollisionReceiver {
    void ReceiveCollisionEnter(Collision collision);
    void ReceiveCollisionExit(Collision collision);
    void ReceiveCollisionStay(Collision collision);
}
