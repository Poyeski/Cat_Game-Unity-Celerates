using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackTimingBar : MonoBehaviour
{
    public Slider timingSlider;
    public float speed = 2f;
    public int maxDamage = 20;
    private bool isAttacking = false;
    private MenuNavigation menuNavigation;
    private BulletHellManager bulletHellManager;
    public TextMeshProUGUI damageText; // Reference to the UI Text element for damage

    void Start()
    {
        timingSlider.gameObject.SetActive(false); // Hide the timing bar at the start
        menuNavigation = FindObjectOfType<MenuNavigation>();
        bulletHellManager = FindObjectOfType<BulletHellManager>();
        damageText.gameObject.SetActive(false); // Hide the damage text at the start
    }

    void Update()
    {
        if (isAttacking)
        {
            timingSlider.value = Mathf.PingPong(Time.time * speed, timingSlider.maxValue);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EndAttack();
            }
        }
    }

    public void StartAttack()
    {
        isAttacking = true;
        timingSlider.gameObject.SetActive(true); // Show the timing bar
        menuNavigation.HideSubmenu(); // Hide the submenu
        menuNavigation.DisableAttackButton(); // Disable the attack button
        menuNavigation.DisableDefendButton();
        menuNavigation.DisableHelpButton();
    }

    public void EndAttack()
    {
        isAttacking = false;
        int damage = CalculateDamage(timingSlider.value);
        if (MenuNavigation.selectedEnemy != null)
        {
            MenuNavigation.selectedEnemy.TakeDamage(damage);
            Debug.Log("Damage inflicted: " + damage);
            StartCoroutine(DisplayDamageAndStartBulletHell(damage));
        }
        timingSlider.gameObject.SetActive(false); // Hide the timing bar
    }

    private int CalculateDamage(float sliderValue)
    {
        float normalizedValue = sliderValue / timingSlider.maxValue;
        return Mathf.RoundToInt(maxDamage * normalizedValue);
    }

    private IEnumerator DisplayDamageAndStartBulletHell(int damage)
    {
        damageText.text = damage.ToString();
        damageText.gameObject.SetActive(true); // Show the damage text
        yield return new WaitForSeconds(1f); // Display the damage text for 1 second
        damageText.gameObject.SetActive(false); // Hide the damage text
        bulletHellManager.StartBulletHell(false); // Start the bullet hell phase
    }
}