using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public List<GunWeapon> gunWeapons = new List<GunWeapon>();

    public float attackRate;
    private float nextAttackTime;
    public float meleeDamage;
    public float bulletDamage;
    public float shakeFrequency;
    public float shakeAmplitue;
    public float shakeDuration = 0.25f;
    private int gunIndex;
    private PlayerController playerController;
    
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.F))
            if (playerController.IsReady())
                Fire();
    }

    public void Fire()
    {
        if (nextAttackTime <= Time.time)
        {
            gunIndex = gunIndex >= gunWeapons.Count? 0 : 1;
            nextAttackTime = Time.time + attackRate;
            gunWeapons[gunIndex].Attack(bulletDamage, shakeFrequency, shakeAmplitue, shakeDuration);
            playerController.characterController.Move(-transform.forward * Time.deltaTime * 0.5f);
            gunIndex++;
        }
       
    }
}
