using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnArea;
    public int bulletCount = 20;
    public float spawnRate = 0.5f;
    public bool isDefensePhase = false;
    private bool isBulletHellActive = false;
    private bool bulletHellCompleted = false;
    private MenuNavigation menuNavigation;
    public GameObject playableCharacter; 
    public Animator animator;
    public Player player;

    void Start()
    {
        menuNavigation = FindObjectOfType<MenuNavigation>();
        playableCharacter.SetActive(false);
    }

    public void StartBulletHell(bool defensePhase)
    {
        isBulletHellActive = true;
        isDefensePhase = defensePhase;
        bulletHellCompleted = false;
        menuNavigation.HideSubmenu();
        menuNavigation.HideMessage(); 
        playableCharacter.SetActive(true);

       
        if (animator != null)
        {
            animator.SetBool("IsBulletHellActive", true);
        }

        StartCoroutine(StartBulletHellWithDelay());
    }

    private IEnumerator StartBulletHellWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SpawnBullets());
    }

    public void EndBulletHell()
    {
        isBulletHellActive = false;
        bulletHellCompleted = true;
        playableCharacter.SetActive(false);
        menuNavigation.EnableAttackButton();
        menuNavigation.EnableDefendButton();
        menuNavigation.EnableHelpButton();
        if (animator != null)
        {
            animator.SetBool("IsBulletHellActive", false);
        }
        if (isDefensePhase)
        {
            player.Heal(30);
        }
        StartCoroutine(ShowMessageWithDelay());
    }

    private IEnumerator ShowMessageWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        menuNavigation.ShowMessage();
        menuNavigation.HideSubmenu();
    }

    private IEnumerator SpawnBullets()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            if (!isBulletHellActive) yield break;

            Instantiate(bulletPrefab, GetRandomPositionWithinArea(), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }

        yield return new WaitForSeconds(2f);
        EndBulletHell();
    }

    private Vector3 GetRandomPositionWithinArea()
    {
        var bounds = spawnArea.GetComponent<Renderer>().bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.max.y;

        return new Vector3(x, y, 0);
    }

    public bool IsBulletHellCompleted()
    {
        return bulletHellCompleted;
    }
}