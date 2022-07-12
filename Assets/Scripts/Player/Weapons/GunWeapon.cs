using UnityEngine;
using System.Collections;
public class GunWeapon : Weapon
{
    [Header("Shooting")]
    public Transform firePosition;
    public ParticleSystem muzzleFlashEffect;
    public GameObject smokeEffect;
    [Header("Recoil")]
    public float recoilSmoothness = 3;
    public Vector3 recoilOffset;
    private Vector3 targetRotation;
    [Header("Feedback")]
    public float feedbackThreshold = 0.5f;
    public float feedbackSmoothness = 2;
    private Vector3 startPosition;

    //Emission Color
    private MeshRenderer meshRenderer;
    private Color targetColor;
    private Color startColor;
    public bool isChangingColor;


    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        startPosition = transform.localPosition;
        startColor = meshRenderer.material.GetColor("_EmissionColor");
        SetNewColor();
        ResetColor();
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Euler(Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * recoilSmoothness));
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * feedbackSmoothness);
        ChangeEmissionColor();
    }
    public override void Attack(float damage, float shakeFrequency, float shakeAmplitue, float shakeDuration)
    {
        muzzleFlashEffect.Play();
        PoolManager.Pull("Bullet").GetComponent<Bullet>().SetBulletInfo(damage, firePosition);
        CameraController.instance.Shake(shakeFrequency, shakeAmplitue, shakeDuration);
        targetRotation = new Vector3(Random.Range(-recoilOffset.x, 0), recoilOffset.y, recoilOffset.z);
        transform.localRotation = Quaternion.Euler(targetRotation);
        transform.localPosition = new Vector3(startPosition.x, startPosition.y + Random.Range(0, 0.03f), startPosition.z - Random.Range(0, feedbackThreshold));
        SetNewColor();
        Invoke(nameof(ResetColor), 1f);
    }

    public override void ActivateWeapon(bool state)
    {
        gameObject.SetActive(state);
    }

    private void SetNewColor()
    {
        if (isChangingColor)
            return;
        isChangingColor = true;
        targetColor = new Color(Random.Range(0, 256), Random.Range(0, 256), Random.Range(0, 256), 0);
    }

    private void ResetColor()
    {
        isChangingColor = false;
        targetColor = startColor;
    }

    private void ChangeEmissionColor()
    {
        Color currentColor = meshRenderer.material.GetColor("_EmissionColor");
        Color lerp = Color.Lerp(currentColor, targetColor, Time.deltaTime * 10);
        meshRenderer.material.SetColor("_EmissionColor", lerp);
    }
}
