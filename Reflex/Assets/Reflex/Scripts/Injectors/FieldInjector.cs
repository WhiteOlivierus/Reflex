using System;
using System.Collections.Generic;
using System.Reflection;
using Reflex.Scripts;
using UnityEngine;

namespace Reflex.Injectors
{
	internal static class FieldInjector
	{
		internal static void Inject(FieldInfo field, object instance, IContainer container)
		{
			try
			{
				field.SetValue(instance, container.Resolve(field.FieldType));
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
		}

		internal static void InjectMany(IEnumerable<FieldInfo> fields, object instance, IContainer container)
		{
			foreach (var field in fields)
			{
				Inject(field, instance, container);
			}
		}
	}
}