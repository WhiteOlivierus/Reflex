﻿//using System.Collections;
//using System.Collections.Generic;
//using FluentAssertions;
//using Reflex.Microsoft.Attributes;
//using Reflex.Microsoft.Core;
//using Reflex.Microsoft.Extensions;
//using UnityEngine;
//using UnityEngine.TestTools;

//namespace Reflex.Microsoft.Tests
//{
//    internal class MonoRuntimeInstantiationTests
//    {
//        [ExecuteInEditMode]
//        private class MonoEventHook : MonoBehaviour
//        {
//            public readonly List<string> ExecutionOrder = new List<string>();

//            [Inject]
//            private void Inject()
//            {
//                ExecutionOrder.Add(nameof(Inject));
//            }

//            private void Awake()
//            {
//                ExecutionOrder.Add(nameof(Awake));
//            }

//            private void Start()
//            {
//                ExecutionOrder.Add(nameof(Start));
//            }
//        }

//        [UnityTest]
//        public IEnumerator ExecutionOrderOfRuntimeInstantiatedMonoBehaviours_ShouldBe_AwakeInjectStart()
//        {
//            using (var container = new ContainerDescriptor("").Build())
//            {
//                var prefab = new GameObject("Prefab").AddComponent<MonoEventHook>();
//                var instance = container.Instantiate(prefab);
//                yield return null;
//                string.Join(",", instance.ExecutionOrder).Should().Be("Awake,Inject,Start");
//            }
//        }
//    }
//}