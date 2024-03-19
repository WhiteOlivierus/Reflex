using System.Collections;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Reflex.Extensions;
using Reflex.Injectors;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Reflex.PlayModeTests
{
    public class ExecutionOrderTests
    {
        [UnitySetUp]
        public IEnumerator Setup()
        {
            yield return SceneManager.LoadSceneAsync("ExecutionOrderTestsScene", LoadSceneMode.Single);
            yield return new WaitForEndOfFrame(); // Wait until Start is called, takes one frame
        }
        
        [Test]
        public void ExecutionOrderOf_PreInstantiated_InjectedObject_ShouldBe_AwakeInjectStart()
        {
            var injectedObject = Object.FindObjectsOfType<InjectedGameObject>().Single();
            string.Join(",", injectedObject.ExecutionOrder).Should().Be("Awake,Inject,Start");
        }
        
        [UnityTest]
        public IEnumerator ExecutionOrderOf_RuntimeInstantiated_InjectedObject_ShouldBe_AwakeInjectStart()
        {
            var prefab = new GameObject("Prefab").AddComponent<InjectedGameObject>();
            var injectedObject = Object.Instantiate(prefab);
            GameObjectInjector.InjectRecursive(injectedObject.gameObject, injectedObject.gameObject.scene.GetSceneContainer());
            yield return new WaitForEndOfFrame(); // Wait until Start is called, takes one frame
            string.Join(",", injectedObject.ExecutionOrder).Should().Be("Awake,Inject,Start");
        }
    }
}