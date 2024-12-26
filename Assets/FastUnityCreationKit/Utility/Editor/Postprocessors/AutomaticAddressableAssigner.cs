using System;
using System.Collections.Generic;
using System.Reflection;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Utility.Attributes;
using FastUnityCreationKit.Utility.Editor.Extensions;
using UnityEditor;
using UnityEngine;

namespace FastUnityCreationKit.Utility.Editor.Postprocessors
{
	[InitializeOnLoad]
    public static class AutomaticAddressableAssigner 
    {
	    static AutomaticAddressableAssigner()
	    {
		    List<Type> objectsToAssign = new List<Type>();
		    
		    // Find all object types with AddressableGroupAttribute
		    Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		    for(int index = 0; index < assemblies.Length; index++)
		    {
			    Assembly assembly = assemblies[index];
			    
			    // Get all types in assembly
			    Type[] types = assembly.GetTypes();
			    for(int i = 0; i < types.Length; i++)
			    {
				    Type type = types[i];
				    
				    // Check if type has AddressableGroupAttribute
				    AddressableGroupAttribute attribute = (AddressableGroupAttribute)
					    Attribute.GetCustomAttribute(type, typeof(AddressableGroupAttribute));
				    if (attribute == null) continue;
				    
				    if (type.IsAbstract || type.IsInterface)
					    continue;
					    
				    objectsToAssign.Add(type);
			    }
		    }
		    
		    List<IAutoPopulatedContainer> containersToPopulate = new List<IAutoPopulatedContainer>();
		    
		    // Assign all objects
		    for (int i = 0; i < objectsToAssign.Count; i++)
		    {
			    Type type = objectsToAssign[i];
			    AddressableGroupAttribute attribute = (AddressableGroupAttribute)
				    Attribute.GetCustomAttribute(type, typeof(AddressableGroupAttribute));
			    
			    // Find all instances in project
			    string[] assets = AssetDatabase.FindAssets($"t:{type.Name}");
			    
			    // Assign all instances to addressable group 
			    for(int j = 0; j < assets.Length; j++)
			    {
				    string assetPath = AssetDatabase.GUIDToAssetPath(assets[j]);
				    UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(assetPath, type);
				    
				    // Assign object to addressable group using extensions
				    if (obj.SetAddressableGroup(attribute.GroupName, attribute.Labels))
				    {
					    Debug.Log($"Assigned {obj.name} to addressable group {attribute.GroupName}");

					    // Check if object is IWithDatabase and populate it
					    if (obj is IWithDatabase {RawDatabase: IAutoPopulatedContainer autoPopulatedContainer})
					    {
						    if (!containersToPopulate.Contains(autoPopulatedContainer))
							    containersToPopulate.Add(autoPopulatedContainer);
					    }
				    }
			    }
		    }
		    
		    // Populate all containers
		    for (int i = 0; i < containersToPopulate.Count; i++)
			    containersToPopulate[i].Populate();
	    }

    }
}