using System;
using System.Collections.Generic;

public class MessageDispatcher
{
    Dictionary<uint, Action<object>> _msgCallbacks = new Dictionary<uint, Action<object>>();

    public void Add(uint msgid, Action<object> callback)
    {
        Action<object> callbacks;
        if (_msgCallbacks.TryGetValue(msgid, out callbacks))
        {
            callbacks += callback;
            _msgCallbacks[msgid] = callbacks;
        }
        else
        {
            callbacks += callback;

            _msgCallbacks.Add(msgid, callbacks);
        }
    }

    public bool Contain(uint msgid)
    {
        if (_msgCallbacks.ContainsKey(msgid))
            return true;
        return false;
    }

    public void Remove(uint msgid, Action<object> callback)
    {
        Action<object> callbacks;
        if (_msgCallbacks.TryGetValue(msgid, out callbacks))
        {
            callbacks -= callback;
            _msgCallbacks[msgid] = callbacks;
            //_msgCallbacks.Remove(msgid);
        }
    }

    public void Invoke(uint msgid, object msg)
    {
        Action<object> callbacks;
        if (!_msgCallbacks.TryGetValue(msgid, out callbacks))
        {
            return;
        }
        else
        {
            if (_msgCallbacks[msgid] == null)
                return;
        }
        callbacks.Invoke(msg);
    }

    public void ClearMsg()
    {
        _msgCallbacks.Clear();
    }
}
