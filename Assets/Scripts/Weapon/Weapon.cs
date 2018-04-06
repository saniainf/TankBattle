using UnityEngine;
using System.Collections;

public abstract class Weapon
{
    public virtual void Setup(HandlerWeapon handlerWeapon) { }

    public virtual void Update() { }

    public virtual void OnButtonDown() { }

    public virtual void OnButtonPress() { }

    public virtual void OnButtonUp() { }
}
