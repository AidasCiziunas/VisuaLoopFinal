using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SyedAli.Main
{
    public static class Helper
    {
        public static void AddEventTriggerListener(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityAction<BaseEventData> unityAction)
        {
            EventTrigger.Entry entry = eventTrigger.triggers.Find(x => x.eventID == eventTriggerType);
            if (entry != null)
            {
                entry.callback.AddListener(unityAction);
            }
            else
            {
                EventTrigger.Entry newEntry = new EventTrigger.Entry();
                newEntry.eventID = eventTriggerType;
                newEntry.callback.AddListener(unityAction);
                eventTrigger.triggers.Add(newEntry);
            }
        }

        public static void RemoveEventTriggerListener(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityAction<BaseEventData> unityAction)
        {
            EventTrigger.Entry entry = eventTrigger.triggers.Find(x => x.eventID == eventTriggerType);
            entry.callback.RemoveListener(unityAction);
        }
    }

    public static class ListHelper<T>
    {
        public static bool CheckListEqual(List<T> ls1, List<T> ls2)
        {
            if (ls1.Count == 0 && ls2.Count == 0)
            {
                return true;
            }
            else if (ls1.Count != ls2.Count)
            {
                return false;
            }
            else
            {
                foreach (var entry in ls1)
                {
                    bool found = false;
                    foreach (var entry2 in ls2)
                    {
                        if (entry.Equals(entry2))
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
