using System.Collections.Generic;
using DG.Tweening;
using GamePlay.TileSystem;
using Services.CommandService;
using UnityEngine;

namespace GamePlay
{
    public class MoveCommand : ICommand
    {
        private readonly MoveData _moveData;
        
        private Transform _transform;
        
        private Vector3 _targetPosition;
        private Vector3 _initialPosition;

        private float _duration;
        
        private LetterTile _letterTile;

        private readonly List<LetterTile> _childrenTiles = new();

        public MoveCommand(MoveData moveData)
        {
            _moveData = moveData;
        }

        public void SetMoveData(LetterTile letterTile, Vector3 targetPosition)
        {
            _letterTile = letterTile;
            _transform = letterTile.GameObject.transform;
            _targetPosition = targetPosition;
            _initialPosition = _transform.position;

            _duration = CalculateDuration();
        }
        
        public void Execute()
        {
            _letterTile.RemoveChildren(out var childrenRemoved);
            _childrenTiles.AddRange(childrenRemoved);
            _transform.DOMove(_targetPosition, _duration).SetEase(_moveData.ease);
        }

        public void Undo()
        {
            for (int i = 0; i < _childrenTiles.Count; i++)
            {
                var childTile = _childrenTiles[i];
                _letterTile.AddChild(childTile);
            }
            _childrenTiles.Clear();
            _transform.DOMove(_initialPosition, _duration).SetEase(_moveData.ease);
        }

        private float CalculateDuration()
        {
            var distance = Vector3.Distance(_transform.position, _targetPosition);
            var moveSpeed = _moveData.speed;
            var duration = distance / moveSpeed;
            return duration;
        }
    }
}
