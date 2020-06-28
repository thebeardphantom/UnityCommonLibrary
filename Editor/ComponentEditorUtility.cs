using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace BeardPhantom.UCL.Editor
{
    public class ComponentEditorUtility
    {
        #region Types

        private class ComponentAssemblySorter : ComponentSorter
        {
            #region Methods

            /// <inheritdoc />
            public override int Compare(Component x, Component y)
            {
                var order = SortByAssembly(x, y);

                if (order == 0)
                {
                    order = SortByName(x, y);
                }

                return order;
            }

            #endregion
        }

        private class ComponentNameSorter : ComponentSorter
        {
            #region Methods

            /// <inheritdoc />
            public override int Compare(Component x, Component y)
            {
                return SortByName(x, y);
            }

            #endregion
        }

        private abstract class ComponentSorter : IComparer<Component>
        {
            #region Methods

            /// <inheritdoc />
            public abstract int Compare(Component x, Component y);

            protected int SortByAssembly(Component x, Component y)
            {
                return string.CompareOrdinal(
                    x.GetType().Assembly.FullName,
                    y.GetType().Assembly.FullName);
            }

            protected int SortByName(Component x, Component y)
            {
                return string.CompareOrdinal(
                    x.GetType().Name,
                    y.GetType().Name);
            }

            #endregion
        }

        #endregion

        #region Methods

        [MenuItem("CONTEXT/Component/Sort By Assembly")]
        private static void SortByAssembly(MenuCommand cmd)
        {
            var component = cmd.context as Component;
            var obj = component.gameObject;
            SortComponents(obj, new ComponentAssemblySorter());
        }

        [MenuItem("CONTEXT/Component/Sort By Name")]
        private static void SortByName(MenuCommand cmd)
        {
            var component = cmd.context as Component;
            var obj = component.gameObject;
            SortComponents(obj, new ComponentNameSorter());
        }

        private static void SortComponents(
            GameObject obj,
            IComparer<Component> sorter)
        {
            var sortedComponents = obj.GetComponents<Component>().ToList();
            sortedComponents.RemoveAll(c => c is Transform);
            sortedComponents.Sort(sorter);

            for (var i = 0; i < sortedComponents.Count; i++)
            {
                var target = sortedComponents[i];
                var targetIndex = i + 1;
                var currentComponents = obj.GetComponents<Component>().ToList();
                currentComponents.RemoveAll(c => c is Transform);
                var currentIndex = currentComponents.IndexOf(target);
                var distance = currentIndex - targetIndex;

                for (var j = 0; j < Mathf.Abs(distance); j++)
                {
                    if (distance < 0)
                    {
                        ComponentUtility.MoveComponentDown(target);
                    }
                    else
                    {
                        ComponentUtility.MoveComponentUp(target);
                    }
                }
            }
        }

        [MenuItem("CONTEXT/Component/Move to Top")]
        private static void MoveToTop(MenuCommand c)
        {
            var component = c.context as Component;
            bool didMove;

            do
            {
                didMove = ComponentUtility.MoveComponentUp(component);
            }
            while (didMove);
        }

        [MenuItem("CONTEXT/Component/Move to Bottom")]
        private static void MoveToBottom(MenuCommand c)
        {
            var component = c.context as Component;
            bool didMove;

            do
            {
                didMove = ComponentUtility.MoveComponentDown(component);
            }
            while (didMove);
        }

        [MenuItem("CONTEXT/Component/Collapse All")]
        private static void CollapseAll(MenuCommand cmd)
        {
            var component = cmd.context as Component;

            if (component)
            {
                FoldAllOnGameObject(component.gameObject, false);
            }
        }

        [MenuItem("CONTEXT/Component/Expand All")]
        private static void ExpandAll(MenuCommand cmd)
        {
            var component = cmd.context as Component;

            if (component)
            {
                FoldAllOnGameObject(component.gameObject, true);
            }
        }

        private static void FoldAllOnGameObject(GameObject obj, bool enabled)
        {
            var inspectorWindow = EditorWindow.focusedWindow;

            var tracker = (ActiveEditorTracker) inspectorWindow
                .GetType()
                .GetMethod("GetTracker")
                .Invoke(inspectorWindow, null);

            for (var i = 0; i < tracker.activeEditors.Length; i++)
            {
                var cmp = tracker.activeEditors[i].target as Component;

                if (cmp && cmp.gameObject == obj)
                {
                    tracker.SetVisible(
                        i,
                        enabled
                            ? 1
                            : 0);
                    InternalEditorUtility.SetIsInspectorExpanded(cmp, enabled);
                }
            }
        }

        #endregion
    }
}