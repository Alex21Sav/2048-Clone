using UnityEngine;

public class AvatarEnemyActiv : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyImage;
    private Animator _animatorEnemy;
    private int _lavelIndex;

    public void EnemyActiv(int index)
    {
        index = index - 1;
        if (index < _enemyImage.Length)
        {
             _enemyImage[index].SetActive(true);
            _animatorEnemy = _enemyImage[index].GetComponent<Animator>();
        }
        else
        {
            _enemyImage[_enemyImage.Length -1].SetActive(true);
            _animatorEnemy = _enemyImage[_enemyImage.Length - 1].GetComponent<Animator>();
        }
    }
    public void EnemyDisable()
    {
        for (int i = 0; i < _enemyImage.Length; i++)
        {
            _enemyImage[i].SetActive(false);
        }
    }
    public void TakeDamageAnimation()
    {
        _animatorEnemy.Play("TakeDamage");
    }
}
