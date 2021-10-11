using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using Foundation;

[TestFixture]
public sealed class TimeScaleManagerTest : ZenjectUnitTestFixture
{
    const float FloatEpsilon = 0.0001f;

    [SetUp]
    public void Install()
    {
        Container.Bind<ITimeScaleManager>().To<TimeScaleManager>().FromNewComponentOnNewGameObject().AsSingle();
    }

    [Test]
    public void TestOneHandle()
    {
        var manager = Container.Resolve<ITimeScaleManager>();
        TimeScaleHandle handle;

        Assert.AreEqual(1.0f, Time.timeScale, FloatEpsilon);

        //////////////////////////////////////////////////////////////
        // Test with 0.5

        // add 0.5
        handle = manager.BeginTimeScale(0.5f);
        Assert.AreEqual(0.5f, Time.timeScale, FloatEpsilon);

        // remove 0.5
        manager.EndTimeScale(handle);
        Assert.AreEqual(1.0f, Time.timeScale, FloatEpsilon);

        //////////////////////////////////////////////////////////////
        // Test with 0.0

        // add 0.0
        handle = manager.BeginTimeScale(0.0f);
        Assert.AreEqual(0.0f, Time.timeScale, FloatEpsilon);

        // remove 0.0
        manager.EndTimeScale(handle);
        Assert.AreEqual(1.0f, Time.timeScale, FloatEpsilon);
    }

    [Test]
    public void TestTwoHandles()
    {
        var manager = Container.Resolve<ITimeScaleManager>();
        TimeScaleHandle handle1, handle2;

        Assert.AreEqual(1.0f, Time.timeScale);

        //////////////////////////////////////////////////////////////
        // Test removal in LIFO order

        // add 0.5
        handle1 = manager.BeginTimeScale(0.5f);
        Assert.AreEqual(0.5f, Time.timeScale);

        // add 0.25
        handle2 = manager.BeginTimeScale(0.25f);
        Assert.AreEqual(0.125f, Time.timeScale);

        // remove 0.25
        manager.EndTimeScale(handle2);
        Assert.AreEqual(0.5f, Time.timeScale);

        // remove 0.5
        manager.EndTimeScale(handle1);
        Assert.AreEqual(1.0f, Time.timeScale);

        //////////////////////////////////////////////////////////////
        // Test removal in FIFO order

        // add 0.5
        handle1 = manager.BeginTimeScale(0.5f);
        Assert.AreEqual(0.5f, Time.timeScale);

        // add 0.25
        handle2 = manager.BeginTimeScale(0.25f);
        Assert.AreEqual(0.125f, Time.timeScale);

        // remove 0.5
        manager.EndTimeScale(handle1);
        Assert.AreEqual(0.25f, Time.timeScale);

        // remove 0.25
        manager.EndTimeScale(handle2);
        Assert.AreEqual(1.0f, Time.timeScale);
    }

    [Test]
    public void TestThreeHandles()
    {
        var manager = Container.Resolve<ITimeScaleManager>();
        TimeScaleHandle handle1, handle2, handle3;

        Assert.AreEqual(1.0f, Time.timeScale);

        // add 0.5
        handle1 = manager.BeginTimeScale(0.5f);
        Assert.AreEqual(0.5f, Time.timeScale);

        // add 0.0
        handle2 = manager.BeginTimeScale(0.0f);
        Assert.AreEqual(0.0f, Time.timeScale);

        // add 0.25
        handle3 = manager.BeginTimeScale(0.25f);
        Assert.AreEqual(0.0f, Time.timeScale);

        // remove 0.0
        manager.EndTimeScale(handle2);
        Assert.AreEqual(0.125f, Time.timeScale);

        // remove 0.25
        manager.EndTimeScale(handle3);
        Assert.AreEqual(0.5f, Time.timeScale);

        // remove 0.5
        manager.EndTimeScale(handle1);
        Assert.AreEqual(1.0f, Time.timeScale);
    }
}
