using System;
using System.Collections.Generic;

namespace SyedAli.Main
{
    public static class UIModuleSignals
    {
        private static readonly UIModuleSignalHub hub = new UIModuleSignalHub();

        public static SType Get<SType>() where SType : ISignal, new()
        {
            return hub.Get<SType>();
        }

        public static void GenerateDebugForEmptySignal(string hash)
        {
            hub.GenerateCallStackForEmptySignal(hash);
        }
    }
    public abstract class UIModuleABaseSignal : ISignal
    {
        protected string _hash;

        public string Hash
        {
            get
            {
                if (string.IsNullOrEmpty(_hash))
                {
                    _hash = this.GetType().ToString();
                }

                return _hash;
            }
        }
    }
    public class UIModuleSignalHub
    {
        private Dictionary<Type, ISignal> signals = new Dictionary<Type, ISignal>();

        public SType Get<SType>() where SType : ISignal, new()
        {
            Type signalType = typeof(SType);
            ISignal signal;

            if (signals.TryGetValue(signalType, out signal))
            {
                return (SType)signal;
            }

            return (SType)Bind(signalType);
        }

        public void GenerateCallStackForEmptySignal(string hash)
        {
            ISignal signal = GetSignalByHash(hash);
            Type type = signal.GetType();

            Type prevDeclaringType = type;
            while (true)
            {
                if (prevDeclaringType.DeclaringType != null)
                {
                    prevDeclaringType = type.DeclaringType;
                    continue;
                }
                else
                {
                    SmallModuleSignals.Get<DebuggingSignal.LogStack>().Dispatch("Missed Signal : " + type.Name + "->" + prevDeclaringType.Name + "->" + "UIModule", this);
                    break;
                }
            }
        }

        private ISignal Bind(Type signalType)
        {
            ISignal signal;
            if (signals.TryGetValue(signalType, out signal))
            {
                UnityEngine.Debug.LogError(string.Format("Signal already registered for type {0}",
                    signalType.ToString()));
                return signal;
            }

            signal = (ISignal)Activator.CreateInstance(signalType);
            signals.Add(signalType, signal);
            return signal;
        }

        private ISignal Bind<T>() where T : ISignal, new()
        {
            return Bind(typeof(T));
        }

        private ISignal GetSignalByHash(string signalHash)
        {
            foreach (ISignal signal in signals.Values)
            {
                if (signal.Hash == signalHash)
                {
                    return signal;
                }
            }

            return null;
        }
    }

    public abstract class UIModuleASignal : UIModuleABaseSignal
    {
        private Action callback;

        public void AddListener(Action handler)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Assert(
                handler.Method.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute),
                    inherit: false).Length == 0,
                "Adding anonymous delegates as Signal callbacks is not supported (you wouldn't be able to unregister them later).");
#endif
            callback += handler;
        }

        public void RemoveListener(Action handler)
        {
            callback -= handler;
        }

        public void Dispatch()
        {
            if (callback != null)
            {
                callback();
            }
            else
            {
                UIModuleSignals.GenerateDebugForEmptySignal(Hash);
            }
        }
    }

    public abstract class UIModuleASignal<R> : UIModuleABaseSignal
    {
        private Func<R> callback;

        public void AddListener(Func<R> handler)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Assert(
                handler.Method.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute),
                    inherit: false).Length == 0,
                "Adding anonymous delegates as Signal callbacks is not supported (you wouldn't be able to unregister them later).");
#endif
            callback += handler;
        }

        public void RemoveListener(Func<R> handler)
        {
            callback -= handler;
        }

        public R Dispatch()
        {
            if (callback != null)
            {
                return callback();
            }
            else
            {
                UIModuleSignals.GenerateDebugForEmptySignal(Hash);
                return default(R);
            }
        }
    }

    public abstract class UIModuleASignal<T, R> : UIModuleABaseSignal
    {
        private Func<T, R> callback;

        public void AddListener(Func<T, R> handler)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Assert(
                handler.Method.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute),
                    inherit: false).Length == 0,
                "Adding anonymous delegates as Signal callbacks is not supported (you wouldn't be able to unregister them later).");
#endif
            callback += handler;
        }

        public void RemoveListener(Func<T, R> handler)
        {
            callback -= handler;
        }

        public R Dispatch(T arg1)
        {
            if (callback != null)
            {
                return callback(arg1);
            }
            else
            {
                UIModuleSignals.GenerateDebugForEmptySignal(Hash);
                return default(R);
            }
        }
    }

    public abstract class UIModuleASignal<T, Q, R> : UIModuleABaseSignal
    {
        private Func<T, Q, R> callback;

        public void AddListener(Func<T, Q, R> handler)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Assert(
                handler.Method.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute),
                    inherit: false).Length == 0,
                "Adding anonymous delegates as Signal callbacks is not supported (you wouldn't be able to unregister them later).");
#endif
            callback += handler;
        }

        public void RemoveListener(Func<T, Q, R> handler)
        {
            callback -= handler;
        }

        public R Dispatch(T arg1, Q arg2)
        {
            if (callback != null)
            {
                return callback(arg1, arg2);
            }
            else
            {
                UIModuleSignals.GenerateDebugForEmptySignal(Hash);
                return default(R);
            }
        }
    }

    public abstract class UIModuleASignal<T, Q, W, R> : UIModuleABaseSignal
    {
        private Func<T, Q, W, R> callback;

        public void AddListener(Func<T, Q, W, R> handler)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Assert(
                handler.Method.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute),
                    inherit: false).Length == 0,
                "Adding anonymous delegates as Signal callbacks is not supported (you wouldn't be able to unregister them later).");
#endif
            callback += handler;
        }

        public void RemoveListener(Func<T, Q, W, R> handler)
        {
            callback -= handler;
        }

        public R Dispatch(T arg1, Q arg2, W arg3)
        {
            if (callback != null)
            {
                return callback(arg1, arg2, arg3);
            }
            else
            {
                UIModuleSignals.GenerateDebugForEmptySignal(Hash);
                return default(R);
            }
        }
    }

    public abstract class UIModuleASignal<T, Q, W, S, Y, R> : UIModuleABaseSignal
    {
        private Func<T, Q, W, S, Y, R> callback;

        public void AddListener(Func<T, Q, W, S, Y, R> handler)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Assert(
                handler.Method.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute),
                    inherit: false).Length == 0,
                "Adding anonymous delegates as Signal callbacks is not supported (you wouldn't be able to unregister them later).");
#endif
            callback += handler;
        }

        public void RemoveListener(Func<T, Q, W, S, Y, R> handler)
        {
            callback -= handler;
        }

        public R Dispatch(T arg1, Q arg2, W arg3, S arg4, Y arg5)
        {
            if (callback != null)
            {
                return callback(arg1, arg2, arg3, arg4, arg5);
            }
            else
            {
                UIModuleSignals.GenerateDebugForEmptySignal(Hash);
                return default(R);
            }
        }
    }
}
