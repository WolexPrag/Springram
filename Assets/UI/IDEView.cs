using NUnit.Framework;
using System.Windows.Input;
using UnityEngine;

public abstract class IDEView : MonoBehaviour
{
    public abstract Command[] GetInput();
}