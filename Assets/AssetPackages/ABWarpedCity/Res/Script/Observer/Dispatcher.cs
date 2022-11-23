using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace WarpedCityPackage{
    public static class Dispatcher
    {
        static Dictionary<EventID, Action<object>> _listener = new Dictionary<EventID, Action<object>>();

        public static void RegisterListener(EventID id, Action<object> callback){
            if (_listener.ContainsKey(id)){
                _listener[id] += callback;
            }
            else {
                _listener.Add(id, null);
                _listener[id] += callback;
            }
        }

        public static void PostEvent(EventID id, object param = null){
            if (!_listener.ContainsKey(id)){
                Debug.Log("Don't have any listener for this event - ID: " + id);
                return;
            }
            var callBack = _listener[id];
            if (callBack == null){
                Debug.Log("Don't have any listener for this event - ID: " + id);
                RemoveEvent(id);
                return;
            }
            callBack(param);
        }

        public static void RemoveEvent(EventID id){
            _listener.Remove(id);
        }

        public static void RemoveListener(EventID id, Action<object> callBack){
            if (!_listener.ContainsKey(id)){
                Debug.LogError("Not found key: " + id);
                return;
            }
            _listener[id] -= callBack;
            if (_listener[id] == null){
                RemoveEvent(id);
            }
        }
    }

    public static class DispatcherExtension{
        public static void RegisterListener(this MonoBehaviour listener, EventID id, Action<object> callBack ){
            Dispatcher.RegisterListener(id, callBack);
        }

        public static void PostEvent(this MonoBehaviour sender, EventID id, object param = null){
            Dispatcher.PostEvent(id, param);
        }

        public static void RemoveListener(this MonoBehaviour listener, EventID id, Action<object> callback){
            Dispatcher.RemoveListener(id, callback);
        }
    }
}
