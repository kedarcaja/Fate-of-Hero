using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivable
{
    void Disable();
    void Active();
}
public interface IHiddable
{
    void Hide();
    void Show();
}
