using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Attack(float damage, float shakeFrequency, float shakeAmplitue, float shakeDuration);

    public abstract void ActivateWeapon(bool state);

  
}
