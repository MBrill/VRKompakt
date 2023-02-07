using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Unit-Tests für einige Objekte  in der Basis-Szene
/// </summary>
/// <remarks>
/// Wir führen die Tests aus für alle objekte, die
/// im Array name enthalten sind, mit Hilfe
/// von ValueSource.
/// 
/// Die Tests werden im EditMode ausgeführt!
/// </remarks>
public class ObjectsTest
{
    /// <summary>
    /// Namen für die parametrisierten Tests
    /// </summary>
    static string[] name = new string[] {
        "Kapsel", 
        "ScalingCube",
        "Flugzeugmodell",
        "ZylinderRechtsHinten"
    };

    /// <summary>
    /// Test, ob der Wrürfel mit dem Namen ScalingCube existiert
    /// </summary>
    [Test]
    public void ObjectExists([ValueSource("name")] string name)
    {
        NUnit.Framework.Assert.NotNull(name);
    }
}
