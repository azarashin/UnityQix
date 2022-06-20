using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBoard : MonoBehaviour
{
    [SerializeField]
    Player[] _players;

    [SerializeField]
    FieldFactory _fieldFactory;

    [SerializeField]
    int _stage = 0; 

    private Field _field;

    // Start is called before the first frame update
    void Start()
    {
        _field = _fieldFactory.GenerateField(_stage);
        for(int i=0;i<_players.Length;i++)
        {
            (int px, int py) = _fieldFactory.GetInitialPosition(_stage, i); 
            _players[i].Setup(_field, px, py);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
