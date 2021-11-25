using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionHandler
{
    void CollisionEnter(string colliderName, GameObject other);
}
