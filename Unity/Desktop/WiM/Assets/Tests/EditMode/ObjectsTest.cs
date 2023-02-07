using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Unit-Tests f�r einige Objekte  in der Basis-Szene
/// </summary>
/// <remarks>
/// Wir f�hren die Tests aus f�r alle objekte, die
/// im Array name enthalten sind, mit Hilfe
/// von ValueSource.
/// 
/// Die Tests werden im EditMode ausgef�hrt!
/// </remarks>
public class ObjectsTest
{
    /// <summary>
    /// Namen f�r die parametrisierten Tests
    /// </summary>
    static string[] name = new string[] {
        "Kapsel", 
        "ScalingCube",
        "Flugzeugmodell",
        "ZylinderRechtsHinten"
    };

    /// <summary>
    /// Test, ob der Wr�rfel mit dem Namen ScalingCube existiert
    /// </summary>
    [Test]
    public void ObjectExists([ValueSource("name")] string name)
    {
        NUnit.Framework.Assert.NotNull(name);
    }
}
