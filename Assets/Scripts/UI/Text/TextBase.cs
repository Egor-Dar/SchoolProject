using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public abstract class TextBase : MonoBehaviour
{
 public Text textField;

 public GameObject Panel;


 protected abstract void WriteText([CanBeNull] object sender , EventArgs e);
}
