using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockbackable
{
    public void GetKnockedUp(Vector3 force);

    public void GetKnockedBack(Vector3 force);
}
