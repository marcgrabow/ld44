using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBehaviour : MonoBehaviour
{
    public int Health = 100;

    public Color AttackColor = Color.red;
    private int _startHealth;

    // public DoorKey Key;

    // EnemySight _sight;
    // EnemyAttack _attack;
    // EnemyWander _wander;

    public SpriteRenderer Renderer;
    Color _originalColor;
    Color _currentColor;

    public GameObject DeadEnemyPrefab;

    public ProgressBarBehaviour HealthBar;

    void Awake()
    {


        _startHealth = Health;

        _originalColor = Renderer.color;
        _currentColor = _originalColor;

    }

    void Start()
    {
        // 50% of not existing at all
        if(UnityEngine.Random.value < 0.5f){
            Destroy(gameObject);
            return;
        }

    }



    void Hit(int damage)
    {
        if (Health <= 0)
            return;

        Health -= damage;

        var healthPercentage = ((float)Health / (float)_startHealth);
        HealthBar.SetProgress(healthPercentage);

        StartCoroutine(HitAnim());

        if (Health <= 0)
        {
            StartCoroutine(Die());
        }

        GetComponent<AudioSource>().Play();
        
    }

    IEnumerator HitAnim()
    {
        Colorize(AttackColor);

        yield return new WaitForSeconds(.05f);

        Colorize(_currentColor);
    }

    void OnPlayerInSight(Transform obj)
    {
        // _wander.StopWandering();
        // _attack.Attack(obj);

        // ProCamera2D.Instance.AddCameraTarget(this.transform);

        _currentColor = AttackColor;
        Colorize(_currentColor);
    }

    void OnPlayerOutOfSight()
    {
        // _wander.StartWandering();
        // _attack.StopAttack();

        // ProCamera2D.Instance.RemoveCameraTarget(this.transform, 2);

        _currentColor = _originalColor;
        Colorize(_currentColor);
    }

    void Colorize(Color color)
    {
        Renderer.material.color = color;
    }

    // void DropLoot()
    // {
    //     if (Key != null)
    //     {
    //         Key.gameObject.SetActive(true);
    //         Key.transform.position = transform.position + new Vector3(0, 3, 0);
    //     }
    // }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        // ProCamera2DShake.Instance.Shake("SmallExplosion");

        OnPlayerOutOfSight();

        //            DropLoot();
        Instantiate(DeadEnemyPrefab, transform.position, DeadEnemyPrefab.transform.rotation);

        Destroy(gameObject);
    }

}
