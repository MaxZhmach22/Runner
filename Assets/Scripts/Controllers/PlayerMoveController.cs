using System;
using UnityEngine;
using Zenject;


namespace Runner
{
    internal sealed class PlayerMoveController : BaseController, IFixedTickable
    {

        private readonly Player _player;
        private float _bonusFactor = 1f;
        

        public PlayerMoveController(Player player)
        {
            _player = player;
        }
        public override void Start()
        {
        }

        public override void Dispose()
        {
            ResetValues();
        }

        private void ResetValues()
        {
            _player.transform.position = Vector3.zero;
        }

        public void FixedTick()
        {
            _player.Rigidbody.MovePosition(Vector3.forward * _player.Speed * Time.deltaTime);
        }

    }
}
