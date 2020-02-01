﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RLTKTutorial.Part1_2
{
    [DisableAutoCreation]
    public class PlayerInputSystem : JobComponentSystem
    {
        TutorialControls _controls;
        InputAction _moveAction;

        Queue<Vector2> _inputQueue = new Queue<Vector2>();

        protected override void OnCreate()
        {
            _controls = new TutorialControls();
            _controls.Enable();
            _moveAction = _controls.DefaultMapping.Move;
        }


        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            float2 move = _moveAction.triggered ? (float2)_moveAction.ReadValue<Vector2>() : float2.zero;
            
            inputDeps = Entities.ForEach((ref PlayerInput input) =>
            {
                input.movement = move;
            }).Schedule(inputDeps);

            return inputDeps;
        }
    }
}