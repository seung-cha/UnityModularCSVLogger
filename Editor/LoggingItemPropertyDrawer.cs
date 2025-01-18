using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Unity.VisualScripting;
using CSVLogger.Fields;
using CSVLogger.Fields.Transforms;
using CSVLogger.Fields.RigidBodies;
using CSVLogger.Fields.Primitives;


namespace CSVLogger.Editor
{
    [CustomPropertyDrawer(typeof(LoggingItem))]
    public class LoggingItemPropertyDrawer : PropertyDrawer
    {
        static readonly float COLLAPSED_HEIGHT = EditorGUIUtility.singleLineHeight;
        static readonly float EXPANDED_HEIGHT = EditorGUIUtility.singleLineHeight * 5f;

        Dictionary<string, Tuple<ReorderableList, List<float>>> hashedList = new();


        private void BuildAddMenu(UnityEngine.Object targetObject, GenericMenu menu, ReorderableList list)
        {
            /// Add more buttons here.

            {
                // Primitives
                AddItem<IntField>("Primitive/Int", menu, list);
                AddItem<FloatField>("Primitive/Float", menu, list);
                AddItem<DoubleField>("Primitive/Double", menu, list);
                AddItem<BoolField>("Primitive/Bool", menu, list);
                AddItem<CharField>("Primitive/Char", menu, list);
                AddItem<StringField>("Primitive/String", menu, list);
                AddItem<Vec2Field>("Primitive/Vec2", menu, list);
                AddItem<Vec3Field>("Primitive/Vec3", menu, list);
            }

            if (targetObject != null)
            {

                if (targetObject.GetComponent<Transform>())
                {
                    AddItem<TransformPositionField>("Transform/Position", menu, list);
                    AddItem<TransformRotationField>("Transform/Rotation", menu, list);
                    AddItem<TransformLocalPositionField>("Transform/LocalPosition", menu, list);
                    AddItem<TransformLocalRotationField>("Transform/LocalRotation", menu, list);
                    AddItem<TransformAxisField>("Transform/Axis", menu, list);
                }
                
                if(targetObject.GetComponent<Rigidbody>())
                {
                    AddItem<RigidBodyVelocityField>("Rigidbody/Velocity", menu, list);
                    AddItem<RigidbodyAngularVelocityField>("Rigidbody/AngularVelocity", menu, list);
                }
            }
        }

        private void ClickHandler(object tuple)
        {
            var tup = (Tuple<ItemField, ReorderableList>)tuple;
            var item = tup.Item1;
            var list = tup.Item2;

            // Find the index to the selected Item
            var s = list.serializedProperty.propertyPath.Split('[');
            int index = s[1][0] - '0';

            Logger logger = (Logger)list.serializedProperty.serializedObject.targetObject;
            logger.Items[index].Fields.Add(item);

            list.serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUILayout.PropertyField(property.FindPropertyRelative("prefix"),
                new GUIContent("Prefix", 
                "All nested fields will contain this prefix. (e.g prefix_velocity, prefix_position) " +
                "Leave it blank if you do not wish to use a prefix."));

            EditorGUILayout.PropertyField(property.FindPropertyRelative("reference"),
                new GUIContent("Reference",
                "Reference to a game object to capture data from. "
                ));

            EditorGUILayout.Space();

            ReorderableList list;

            if (hashedList.ContainsKey(property.propertyPath))
            {
                list = hashedList[property.propertyPath].Item1;
            }
            else
            {
                list = InitialiseReorderableList(property);
            }

            list.DoLayoutList();
            EditorGUI.EndProperty();
        }

        private int GetPropertyIndex(SerializedProperty property)
        {
            var s = property.propertyPath.Split('[');
            return s[1][0] - '0';
        }

        private void AddItem<T>(string path, GenericMenu menu, ReorderableList list) where T : ItemField, new()
        {
            menu.AddItem(new GUIContent(path), false, ClickHandler,
                        new Tuple<ItemField, ReorderableList>(new T(), list));
        }

        private ReorderableList InitialiseReorderableList(SerializedProperty property)
        {
            hashedList.Add(property.propertyPath, new(new ReorderableList(
                        property.serializedObject,
                        property.FindPropertyRelative("fields"),
                        true, true, true, true), new()));

            ReorderableList list = hashedList[property.propertyPath].Item1;

            list.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Fields");
            };


            Logger logger = (Logger)list.serializedProperty.serializedObject.targetObject;

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty elem = list.serializedProperty.GetArrayElementAtIndex(index);
                string componentType = logger.Items[GetPropertyIndex(list.serializedProperty)].Fields[index].GetType().Name;
                

                EditorGUI.LabelField(new Rect(rect.x + 200.0f, rect.y, rect.width,
                    EditorGUIUtility.singleLineHeight), 
                    new GUIContent(componentType));

                EditorGUI.PropertyField(rect, elem, true);
                List<float> heights = hashedList[property.propertyPath].Item2;

                if (elem.isExpanded)
                {
                    if (index < heights.Count)
                        heights[index] = EXPANDED_HEIGHT;
                    else
                        heights.Add(EXPANDED_HEIGHT);
                }
                else
                {
                    if (index < heights.Count)
                        heights[index] = COLLAPSED_HEIGHT;
                    else
                        heights.Add(COLLAPSED_HEIGHT);
                }
            };


            list.elementHeightCallback = (int index) =>
            {
                List<float> heights = hashedList[property.propertyPath].Item2;
                float height;

                try
                {
                    height = heights[index];
                }
                catch
                {
                    height = COLLAPSED_HEIGHT;
                }
                return height;
            };

            list.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) =>
            {
                GenericMenu menu = new GenericMenu();
                SerializedProperty reference = property.FindPropertyRelative("reference");
                UnityEngine.Object targetObject = reference.objectReferenceValue;

                BuildAddMenu(targetObject, menu, list);
               

                menu.ShowAsContext();
            };


            return list;
        }
    }
}