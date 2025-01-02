﻿using Microsoft.CodeAnalysis.Testing;
using Uno.UI.SourceGenerators.DependencyObject;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Uno.UI.SourceGenerators.Tests.Verifiers;
using System.Collections.Immutable;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace Uno.UI.SourceGenerators.Tests.DependencyPropertyGeneratorTests;

using Verify = CSharpIncrementalSourceGeneratorVerifier<DependencyPropertyGenerator>;

[TestClass]
public class Given_DependencyPropertyGenerator
{
	[TestMethod]
	public async Task TestInStaticClass()
	{
		var test = new Verify.Test
		{
			IgnoreAccessibility = true,
			TestState =
			{
				Sources =
				{
					"""
					using Uno.UI.Xaml;
					using Microsoft.UI.Xaml;

					namespace Mynamespace
					{
						public partial class Owner { }

						public static partial class C
						{
							[GeneratedDependencyProperty(DefaultValue = 21, AttachedBackingFieldOwner = typeof(Owner), Attached = true)]
							public static DependencyProperty MyValueProperty { get; } = CreateMyValueProperty();

							public static void SetMyValue(DependencyObject instance, int value) => SetMyValueValue(instance, value);
							public static int GetMyValue(DependencyObject instance) => GetMyValueValue(instance);
						}
					}
					"""
				},
				GeneratedSources =
				{
					{ (typeof(DependencyPropertyGenerator), @"Mynamespace.C.cs", SourceText.From("""
						// <auto-generated>
						// ******************************************************************
						// This file has been generated by Uno.UI (DependencyPropertyGenerator)
						// ******************************************************************
						// </auto-generated>
						
						#pragma warning disable 1591 // Ignore missing XML comment warnings
						using System;
						using System.Linq;
						using System.Collections.Generic;
						using System.Collections;
						using System.Diagnostics.CodeAnalysis;
						using Uno.Disposables;
						using System.Runtime.CompilerServices;
						using Uno.UI;
						using Uno.UI.DataBinding;
						using Microsoft.UI.Xaml;
						using Microsoft.UI.Xaml.Controls;
						using Microsoft.UI.Xaml.Data;
						using Uno.Diagnostics.Eventing;
						namespace Mynamespace
						{
							partial class C
							{
								#region MyValue Dependency Property
								private static int GetMyValueValue(global::Microsoft.UI.Xaml.DependencyObject instance)
								{
									if(instance is global::Mynamespace.Owner backingFieldOwnerInstance)
									{
										if (!backingFieldOwnerInstance.__C_MyValuePropertyBackingFieldSet)
										{
											backingFieldOwnerInstance.__C_MyValuePropertyBackingField = (int)instance.GetValue(global::Mynamespace.C.MyValueProperty);
											backingFieldOwnerInstance.__C_MyValuePropertyBackingFieldSet = true;
										}
										return backingFieldOwnerInstance.__C_MyValuePropertyBackingField;
									}
									else
									
									{
										return (int)instance.GetValue(global::Mynamespace.C.MyValueProperty);
									}
								}
								private static void SetMyValueValue(global::Microsoft.UI.Xaml.DependencyObject instance, int value) => instance.SetValue(global::Mynamespace.C.MyValueProperty, value);
								/// <summary>
								/// Generated method used to create the <see cref="MyValueProperty" /> member value
								/// </summary>
								private static global::Microsoft.UI.Xaml.DependencyProperty CreateMyValueProperty() => 
								DependencyProperty.RegisterAttached(
									name: "MyValue",
									propertyType: typeof(int),
									ownerType: typeof(global::Mynamespace.C),
									typeMetadata: new global::Microsoft.UI.Xaml.FrameworkPropertyMetadata(
										defaultValue: (int)21 /* GetMyValueDefaultValue(), global::Mynamespace.C */
										, backingFieldUpdateCallback: (instance, newValue) => 
								{
									if(instance is global::Mynamespace.Owner backingFieldOwnerInstance)
									{
										backingFieldOwnerInstance.__C_MyValuePropertyBackingField = (int)instance.GetValue(global::Mynamespace.C.MyValueProperty);
										backingFieldOwnerInstance.__C_MyValuePropertyBackingFieldSet = true;
									}
								}
								));
								#endregion
							}
						}
						namespace Mynamespace
						{
							partial class Owner
							{
								internal bool __C_MyValuePropertyBackingFieldSet;
								internal int __C_MyValuePropertyBackingField;
							}
						}

						""", Encoding.UTF8))
					}
				}
			},
			ReferenceAssemblies = _Dotnet.Current.WithUnoPackage(),
		};

		await test.RunAsync();
	}

	[TestMethod]
	public async Task TestInDependencyObject()
	{
		var test = new Verify.Test
		{
			IgnoreAccessibility = true,
			TestState =
			{
				Sources =
				{
					"""
					using Uno.UI.Xaml;
					using Microsoft.UI.Xaml;
					using Windows.UI.Core;

					namespace Mynamespace
					{
						public partial class Owner { }

						public partial class C : DependencyObject
						{
							[GeneratedDependencyProperty(DefaultValue = 21, AttachedBackingFieldOwner = typeof(Owner), Attached = true)]
							public static DependencyProperty MyValueProperty { get; } = CreateMyValueProperty();

							public static void SetMyValue(DependencyObject instance, int value) => SetMyValueValue(instance, value);
							public static int GetMyValue(DependencyObject instance) => GetMyValueValue(instance);

							public CoreDispatcher Dispatcher { get; }
							public global::Microsoft.UI.Dispatching.DispatcherQueue DispatcherQueue { get; }
							public object GetValue(DependencyProperty dp) => null;
							public void SetValue(DependencyProperty dp, object value) { }
							public void ClearValue(DependencyProperty dp) { }
							public object ReadLocalValue(DependencyProperty dp) => null;
							public object GetAnimationBaseValue(DependencyProperty dp) => null;
							public long RegisterPropertyChangedCallback(DependencyProperty dp, DependencyPropertyChangedCallback callback) => 0;
							public void UnregisterPropertyChangedCallback(DependencyProperty dp, long token) { }
						}
					}
					"""
				},
				GeneratedSources =
				{
					{ (typeof(DependencyPropertyGenerator), @"Mynamespace.C.cs", SourceText.From("""
						// <auto-generated>
						// ******************************************************************
						// This file has been generated by Uno.UI (DependencyPropertyGenerator)
						// ******************************************************************
						// </auto-generated>
						
						#pragma warning disable 1591 // Ignore missing XML comment warnings
						using System;
						using System.Linq;
						using System.Collections.Generic;
						using System.Collections;
						using System.Diagnostics.CodeAnalysis;
						using Uno.Disposables;
						using System.Runtime.CompilerServices;
						using Uno.UI;
						using Uno.UI.DataBinding;
						using Microsoft.UI.Xaml;
						using Microsoft.UI.Xaml.Controls;
						using Microsoft.UI.Xaml.Data;
						using Uno.Diagnostics.Eventing;
						namespace Mynamespace
						{
							partial class C
							{
								#region MyValue Dependency Property
								private static int GetMyValueValue(global::Microsoft.UI.Xaml.DependencyObject instance)
								{
									if(instance is global::Mynamespace.Owner backingFieldOwnerInstance)
									{
										if (!backingFieldOwnerInstance.__C_MyValuePropertyBackingFieldSet)
										{
											backingFieldOwnerInstance.__C_MyValuePropertyBackingField = (int)instance.GetValue(global::Mynamespace.C.MyValueProperty);
											backingFieldOwnerInstance.__C_MyValuePropertyBackingFieldSet = true;
										}
										return backingFieldOwnerInstance.__C_MyValuePropertyBackingField;
									}
									else
									
									{
										return (int)instance.GetValue(global::Mynamespace.C.MyValueProperty);
									}
								}
								private static void SetMyValueValue(global::Microsoft.UI.Xaml.DependencyObject instance, int value) => instance.SetValue(global::Mynamespace.C.MyValueProperty, value);
								/// <summary>
								/// Generated method used to create the <see cref="MyValueProperty" /> member value
								/// </summary>
								private static global::Microsoft.UI.Xaml.DependencyProperty CreateMyValueProperty() => 
								DependencyProperty.RegisterAttached(
									name: "MyValue",
									propertyType: typeof(int),
									ownerType: typeof(global::Mynamespace.C),
									typeMetadata: new global::Microsoft.UI.Xaml.FrameworkPropertyMetadata(
										defaultValue: (int)21 /* GetMyValueDefaultValue(), global::Mynamespace.C */
										, backingFieldUpdateCallback: (instance, newValue) => 
								{
									if(instance is global::Mynamespace.Owner backingFieldOwnerInstance)
									{
										backingFieldOwnerInstance.__C_MyValuePropertyBackingField = (int)instance.GetValue(global::Mynamespace.C.MyValueProperty);
										backingFieldOwnerInstance.__C_MyValuePropertyBackingFieldSet = true;
									}
								}
								));
								#endregion
							}
						}
						namespace Mynamespace
						{
							partial class Owner
							{
								internal bool __C_MyValuePropertyBackingFieldSet;
								internal int __C_MyValuePropertyBackingField;
							}
						}

						""", Encoding.UTF8))
					}
				}
			},
			ReferenceAssemblies = _Dotnet.Current.WithUnoPackage(),
		};

		await test.RunAsync();
	}

	[TestMethod]
	public async Task TestGetDefaultValuePresentWithDefaultValueArgument()
	{
		var test = new Verify.Test
		{
			IgnoreAccessibility = true,
			TestState =
			{
				Sources =
				{
					"""
					using Uno.UI.Xaml;
					using Microsoft.UI.Xaml;
					using Windows.UI.Core;

					namespace Mynamespace
					{
						public partial class C : DependencyObject
						{
							[GeneratedDependencyProperty(DefaultValue = 21)]
							public static DependencyProperty MyValueProperty { get; } = CreateMyValueProperty();

							public int MyValue
							{
								get => GetMyValueValue();
								set => SetMyValueValue(value);
							}

							public static int GetMyValueDefaultValue() => 0;

							public CoreDispatcher Dispatcher { get; }
							public global::Microsoft.UI.Dispatching.DispatcherQueue DispatcherQueue { get; }
							public object GetValue(DependencyProperty dp) => null;
							public void SetValue(DependencyProperty dp, object value) { }
							public void ClearValue(DependencyProperty dp) { }
							public object ReadLocalValue(DependencyProperty dp) => null;
							public object GetAnimationBaseValue(DependencyProperty dp) => null;
							public long RegisterPropertyChangedCallback(DependencyProperty dp, DependencyPropertyChangedCallback callback) => 0;
							public void UnregisterPropertyChangedCallback(DependencyProperty dp, long token) { }
						}
					}
					"""
				},
				GeneratedSources =
				{
					{ (typeof(DependencyPropertyGenerator), @"Mynamespace.C.cs", SourceText.From("""
						// <auto-generated>
						// ******************************************************************
						// This file has been generated by Uno.UI (DependencyPropertyGenerator)
						// ******************************************************************
						// </auto-generated>

						#pragma warning disable 1591 // Ignore missing XML comment warnings
						using System;
						using System.Linq;
						using System.Collections.Generic;
						using System.Collections;
						using System.Diagnostics.CodeAnalysis;
						using Uno.Disposables;
						using System.Runtime.CompilerServices;
						using Uno.UI;
						using Uno.UI.DataBinding;
						using Microsoft.UI.Xaml;
						using Microsoft.UI.Xaml.Controls;
						using Microsoft.UI.Xaml.Data;
						using Uno.Diagnostics.Eventing;
						namespace Mynamespace
						{
							partial class C
							{
								private GeneratedDependencyPropertyFlags0 _generatedDependencyPropertyFlags0;
								private enum GeneratedDependencyPropertyFlags0 : uint
								{
									DPFlag0 = 1 << 0,
								}
								#region MyValue Dependency Property
								private int GetMyValueValue()
								{
									if (!_MyValuePropertyBackingFieldSet)
									{
										_MyValuePropertyBackingField = (int)GetValue(MyValueProperty);
										_MyValuePropertyBackingFieldSet = true;
									}
									return _MyValuePropertyBackingField;
								}
								private void SetMyValueValue(int value) => SetValue(MyValueProperty, value);
								private bool _MyValuePropertyBackingFieldSet
								{
									get => (_generatedDependencyPropertyFlags0 & GeneratedDependencyPropertyFlags0.DPFlag0) != 0;
									set { if (value) _generatedDependencyPropertyFlags0 |= GeneratedDependencyPropertyFlags0.DPFlag0; else _generatedDependencyPropertyFlags0 &= ~GeneratedDependencyPropertyFlags0.DPFlag0; }
								}
								private int _MyValuePropertyBackingField;
								/// <summary>
								/// Generated method used to create the <see cref="MyValueProperty" /> member value
								/// </summary>
								private static global::Microsoft.UI.Xaml.DependencyProperty CreateMyValueProperty() => 
								DependencyProperty.Register(
									name: "MyValue",
									propertyType: typeof(int),
									ownerType: typeof(global::Mynamespace.C),
									typeMetadata: new global::Microsoft.UI.Xaml.FrameworkPropertyMetadata(
								#error The generated property MyValue cannot contains both a DefaultValue and the GetMyValueDefaultValue() method.
										defaultValue: (int)21 /* GetMyValueDefaultValue(), global::Mynamespace.C */
										, backingFieldUpdateCallback: OnMyValueBackingFieldUpdate
								));
								private static void OnMyValueBackingFieldUpdate(object instance, object newValue)
								{
									var typedInstance = instance as global::Mynamespace.C;
									typedInstance._MyValuePropertyBackingField = (int)newValue;
									typedInstance._MyValuePropertyBackingFieldSet = true;
								}
								#endregion
							}
						}

						""", Encoding.UTF8))
					}
				}
			},
			ReferenceAssemblies = _Dotnet.Current.WithUnoPackage(),
		};

		test.ExpectedDiagnostics.Add(
			// Uno.UI.SourceGenerators.Internal\Uno.UI.SourceGenerators.DependencyObject.DependencyPropertyGenerator\Mynamespace.C.cs(56,10): error CS1029: #error: 'The generated property MyValue cannot contains both a DefaultValue and the GetMyValueDefaultValue() method.'
			DiagnosticResult.CompilerError("CS1029").WithSpan(@"Uno.UI.SourceGenerators.Internal\Uno.UI.SourceGenerators.DependencyObject.DependencyPropertyGenerator\Mynamespace.C.cs", 56, 10, 56, 117).WithArguments("The generated property MyValue cannot contains both a DefaultValue and the GetMyValueDefaultValue() method.")
		);

		await test.RunAsync();
	}
}
