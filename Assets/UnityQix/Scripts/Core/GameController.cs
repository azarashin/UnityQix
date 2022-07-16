using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private AreaCalculator _calc;
    private Field _field;
    private Player[] _players;
    private IEnemy[] _enemies;
    private float _quota; 

    public void Setup(Field field, (int x, int y)[] initialPositions, float quota, int lives, int units, Player[] players, IEnemy[] enemies)
    {
        _calc = new AreaCalculator();

        _field = field; 
        _players = players; 
        _enemies = enemies;
        _quota = quota; 

        for (int i=0;i<players.Length;i++)
        {
            Player player = players[i];
            player.Setup(field, initialPositions[i].x, initialPositions[i].y, lives, units);
        }
    }

    /// <summary>
    /// プレイヤーと敵の動きが確定した後でゲームの状態を更新する
    /// </summary>
    void LateUpdate()
    {
        _calc.UpdateField(_field, _enemies.Select(s => s.LogicalPosition()).ToArray());
        foreach(Player player in _players)
        {
            IEnemy[] nears = _enemies
                .Where(s => Near(player, s))
                .ToArray();
        }
    }

    private bool Near(Player player, IEnemy enemy)
    {
        (int px, int py) = player.Position();
        int pr = player.Radius();

        (int ex, int ey) = enemy.LogicalPosition();
        int er = enemy.Radius();

        return (px - ex) * (px - ex) + (py - ey) * (py - ey) <= (pr + er) * (pr + er); 
    }

    public bool IsClearQuota()
    {
        return (float)_calc.NumberOfOccupiedPoints(_field) / (float)_calc.NumberOfAllPoints(_field) >= _quota; 
    }
}
