using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DescriptorUpdater {
    void SetSelectable(Selectable selectable);
    void GetRequiredComponentsFromSelectable();
    void CheckAndSetFactionChanges(string faction);
}
