//using System;
//using FluentAssertions;
//using Reflex.Microsoft.Core;
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.Scripting;

//namespace Reflex.Microsoft.Tests
//{
//    internal class GarbageCollectionTests
//    {
//        private class Service
//        {
//            public event Action OnFinalized;

//            ~Service()
//            {
//                OnFinalized?.Invoke();
//            }
//        }

//        [Test]
//        public void Singleton_ShouldBeFinalized_WhenOwnerIsDisposed()
//        {
//            if (GarbageCollector.isIncremental) // Incremental GC can mess a bit with finalizer queue
//            {
//                Assert.Inconclusive();
//            }
            
//            var finalized = false;
            
//            var container = new ContainerDescriptor("")
//                .AddSingleton(typeof(Service), typeof(Service))
//                .Build();

//            Action dispose = () =>
//            {
//                container.Single<Service>().OnFinalized += () => finalized = true;
//                container.Dispose();
//            };

//            dispose.Invoke();
            
//            Resources.UnloadUnusedAssets();
//            GC.Collect();
//            GC.WaitForPendingFinalizers();
            
//            finalized.Should().BeTrue();
//        }

//        [Test]
//        public void DisposedScopedContainer_ShouldHaveNoReferencesToIt_AndShouldBeCollectedAndFinalized()
//        {
//            if (GarbageCollector.isIncremental) // Incremental GC can mess a bit with finalizer queue
//            {
//                Assert.Inconclusive();
//            }

//            var parent = new ContainerDescriptor("").Build();
//            WeakReference<Container> scopedWeakRef = null;

//            Action dispose = () =>
//            {
//                // This will go out of scope after dispose() is invoked
//                var scoped = parent.Scope("");
//                scopedWeakRef = new WeakReference<Container>(scoped);
//                scopedWeakRef.TryGetTarget(out _).Should().BeTrue();
//                scoped.Dispose();
//            };

//            dispose.Invoke();
            
//            Resources.UnloadUnusedAssets();
//            GC.Collect();
//            GC.WaitForPendingFinalizers();
            
//            scopedWeakRef.TryGetTarget(out _).Should().BeFalse();
//        }
        
//        [Test]
//        public void Construct_ContainerShouldNotControlConstructedObjectLifeCycle_ByNotKeepingReferenceToIt()
//        {
//            if (GarbageCollector.isIncremental) // Incremental GC can mess a bit with finalizer queue
//            {
//                Assert.Inconclusive();
//            }

//            var finalized = false;
//            var container = new ContainerDescriptor("").Build();

//            Action dispose = () =>
//            {
//                // This will go out of scope after dispose() is invoked
//                var service = container.Construct<Service>();
//                service.OnFinalized += () => finalized = true;
//            };

//            dispose.Invoke();
            
//            Resources.UnloadUnusedAssets();
//            GC.Collect();
//            GC.WaitForPendingFinalizers();

//            finalized.Should().BeTrue();
//        }
//    }
//}