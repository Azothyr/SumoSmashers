using UnityEngine;
using UnityEngine.Events;
using ZPTools.Utility;

public class TMPFormatBehavior : TMPBehaviorBase
{
    [SerializeField] private bool _updateOnStart;
    [SerializeField] private StringFactory _text;
    public UnityEvent startEvent;

    private new void Awake()
    {
        base.Awake();
        _text.DebugContext = this;
    }

    private void Start()
    {
        startEvent.Invoke();
        if (_updateOnStart) UpdateLabel();
    }

    public void UpdateLabel() => HandleUpdateLabel(_text.formattedString);
}