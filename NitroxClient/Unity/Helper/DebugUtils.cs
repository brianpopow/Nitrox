﻿using NitroxClient.GameLogic.Helper;
using NitroxModel.Logger;
using UnityEngine;
using System;
using NitroxClient.MonoBehaviours;

namespace NitroxClient.Unity.Helper
{
    public static class DebugUtils
    {
        public static void PrintHierarchy(GameObject gameObject, bool startAtRoot = false, int parentsUpwards = 1, bool listComponents = false, bool travelDown = true)
        {
            GameObject startHierarchy = gameObject;
            if (startAtRoot)
            {
                GameObject rootObject = gameObject.transform.root.gameObject;
                if (rootObject != null)
                {
                    startHierarchy = rootObject;
                }
            }
            else
            {
                GameObject parentObject = gameObject;
                int i = 0;
                while (i < parentsUpwards)
                {
                    i++;
                    if (parentObject.transform.parent != null)
                    {
                        parentObject = parentObject.transform.parent.gameObject;
                    }
                    else
                    {
                        i = parentsUpwards;
                    }
                }
            }

            TravelDown(startHierarchy, listComponents, "", travelDown);
        }

        private static void TravelDown(GameObject gameObject, bool listComponents = false, string linePrefix = "", bool travelDown = true)
        {
            Log.Instance.LogMessage(LogCategory.Debug, "{linePrefix}+GameObject GUID={NitroxEntity.GetId(gameObject)} NAME={gameObject.name} POSITION={ gameObject.transform.position}");
            if (listComponents)
            {
                ListComponents(gameObject, linePrefix);
            }

            if (!travelDown)
            {
                return;
            }
            foreach (Transform child in gameObject.transform)
            {
                TravelDown(child.gameObject, listComponents, linePrefix + "|  ");
            }
        }

        private static void ListComponents(GameObject gameObject, string linePrefix = "")
        {
            Component[] allComponents = gameObject.GetComponents<Component>();
            foreach (Component c in allComponents)
            {
                Log.Instance.LogMessage(LogCategory.Debug, "{linePrefix}=Component NAME={c.GetType().Name}");
            }
        }
    }
}
