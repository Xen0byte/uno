#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Popups;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using System.Globalization;
using Windows.UI.ViewManagement;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using Uno;
using Uno.UI;
using Uno.UI.RuntimeTests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Private.Infrastructure;

#if !HAS_UNO
using Uno.Logging;
#endif

#if HAS_UNO_WINUI
using LaunchActivatedEventArgs = Microsoft/* UWP don't rename */.UI.Xaml.LaunchActivatedEventArgs;
#else
using LaunchActivatedEventArgs = Windows.ApplicationModel.Activation.LaunchActivatedEventArgs;
#endif

#if UNO_ISLANDS
using Microsoft.UI.Xaml.Markup;
using Uno.UI.XamlHost;
#endif

namespace SamplesApp
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	sealed public partial class App : Application
#if UNO_ISLANDS
	, IXamlMetadataProvider, IXamlMetadataContainer, IDisposable
#endif
	{
#if HAS_UNO
		private static Uno.Foundation.Logging.Logger? _log;
#else
		private static ILogger? _log;
#endif

		private static Microsoft.UI.Xaml.Window? _mainWindow;
		private bool _wasActivated;
		private bool _isSuspended;

		static App()
		{
			ConfigureLogging();
		}

		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			// Fix language for UI tests
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

#if __SKIA__
			ApplicationView.PreferredLaunchViewSize = new Windows.Foundation.Size(1024, 768);
#endif

			ConfigureFeatureFlags();
			ParseCommandLineFeatureFlags();

			AssertIssue1790ApplicationSettingsUsable();
			AssertApplicationData();

			this.InitializeComponent();

#if !WINAPPSDK
			this.Suspending += OnSuspending;
			this.Resuming += OnResuming;
#endif
		}

		internal static Microsoft.UI.Xaml.Window? MainWindow => _mainWindow;

		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used such as when the application is launched to open a specific file.
		/// </summary>
		/// <param name="e">Details about the launch request and process.</param>
		protected
#if HAS_UNO
			internal
#endif
			override void OnLaunched(LaunchActivatedEventArgs e)
		{
			EnsureMainWindow();

#if __IOS__ && !__MACCATALYST__ && !TESTFLIGHT
			// requires Xamarin Test Cloud Agent
			Xamarin.Calabash.Start();

			LaunchiOSWatchDog();
#endif
			var activationKind =
#if HAS_UNO_WINUI
				e.UWPLaunchActivatedEventArgs.Kind
#else
				e.Kind
#endif
				;

			if (activationKind == ActivationKind.Launch)
			{
				AssertIssue8356();

				AssertIssue12936();

				AssertIssue12937();
			}

			var sw = Stopwatch.StartNew();
#if DEBUG
			if (System.Diagnostics.Debugger.IsAttached)
			{
				// this.DebugSettings.EnableFrameRateCounter = true;
			}
#endif
			AssertInitialWindowSize();


			InitializeFrame(e.Arguments);

			AssertIssue8641NativeOverlayInitialized();

			ActivateMainWindow();

#if !WINAPPSDK
			ApplicationView.GetForCurrentView().Title = "Uno Samples";
#else
			MainWindow!.Title = "Uno Samples";
#endif

#if __SKIA__ && DEBUG
			AppendRepositoryPathToTitleBar();
#endif

			HandleLaunchArguments(e);

			if (SampleControl.Presentation.SampleChooserViewModel.Instance is { } vm && vm.CurrentSelectedSample is null)
			{
				vm.SetSelectedSample(CancellationToken.None, "Playground", "Playground");
			}

			Console.WriteLine("Done loading " + sw.Elapsed);
		}

#if __SKIA__ && DEBUG
		private void AppendRepositoryPathToTitleBar()
		{
			var fullPath = Package.Current.InstalledLocation.Path;
			var srcSamplesApp = $"{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}SamplesApp";
			var repositoryPath = fullPath;
			if (fullPath.IndexOf(srcSamplesApp) is int index && index > 0)
			{
				repositoryPath = fullPath.Substring(0, index);
			}

			ApplicationView.GetForCurrentView().Title += $" ({repositoryPath})";
		}
#endif

		[MemberNotNull(nameof(_mainWindow))]
		private void EnsureMainWindow()
		{
			_mainWindow ??=
#if HAS_UNO_WINUI
				new Microsoft.UI.Xaml.Window();
#else
				Microsoft.UI.Xaml.Window.Current!;
#endif
			Private.Infrastructure.TestServices.WindowHelper.CurrentTestWindow =
				_mainWindow;
		}

#if __IOS__
		/// <summary>
		/// Launches a watchdog that will terminate the app if the dispatcher does not process
		/// messages within a specific time.
		///
		/// Restarting the app is required in some cases where either the test engine, or Xamarin.UITest stall
		/// while processing the events of the app.
		///
		/// See https://github.com/unoplatform/uno/issues/3363 for details
		/// </summary>
		private void LaunchiOSWatchDog()
		{
			if (!Debugger.IsAttached)
			{
				Console.WriteLine("Starting dispatcher WatchDog...");

				var dispatcher = UnitTestDispatcherCompat.From(_mainWindow!);
				var timeout = TimeSpan.FromSeconds(240);

				Task.Run(async () =>
				{

					while (true)
					{
						var delayTask = Task.Delay(timeout);
						var messageTask = dispatcher.RunAsync(UnitTestDispatcherCompat.Priority.High, () => { }).AsTask();

						if (await Task.WhenAny(delayTask, messageTask) == delayTask)
						{
							ThreadPool.QueueUserWorkItem(
								_ =>
								{
									Console.WriteLine($"WatchDog detecting a stall in the dispatcher after {timeout}, terminating the app");
									throw new Exception($"Watchdog failed");
								});
						}

						await Task.Delay(TimeSpan.FromSeconds(5));
					}
				});
			}
		}
#endif

#if !WINAPPSDK
		protected
#if HAS_UNO
			internal
#endif
			override async void OnActivated(IActivatedEventArgs e)
		{
			base.OnActivated(e);

			EnsureMainWindow();
			InitializeFrame();
			ActivateMainWindow();

			if (e.Kind == ActivationKind.Protocol)
			{
				var protocolActivatedEventArgs = (ProtocolActivatedEventArgs)e;
				var dlg = new MessageDialog(
					$"PreviousState - {e.PreviousExecutionState}, " +
					$"Uri - {protocolActivatedEventArgs.Uri}",
					"Application activated via protocol");
				if (ApiInformation.IsMethodPresent("Windows.UI.Popups.MessageDialog", nameof(MessageDialog.ShowAsync)))
				{
					await dlg.ShowAsync();
				}
			}
		}
#endif

		private void ActivateMainWindow()
		{
#if DEBUG && (__SKIA__ || __WASM__)
			_mainWindow!.EnableHotReload();
#endif
			_mainWindow!.Activate();
			_wasActivated = true;
			_isSuspended = false;
			MainWindowActivated?.Invoke(this, EventArgs.Empty);
		}

		public event EventHandler? MainWindowActivated;

#if !HAS_UNO_WINUI
		protected override void OnWindowCreated(global::Microsoft.UI.Xaml.WindowCreatedEventArgs args)
		{
			if (Current is null)
			{
				throw new InvalidOperationException("The Window should be created later in the application lifecycle.");
			}
		}
#endif

		private void InitializeFrame(string? arguments = null)
		{
			if (_mainWindow is null)
			{
				throw new InvalidOperationException("Main window must be initialized before Frame");
			}

			var rootFrame = _mainWindow.Content as Frame;

			// Do not repeat app initialization when the Window already has content,
			// just ensure that the window is active
			if (rootFrame is null)
			{
				// Create a Frame to act as the navigation context and navigate to the first page
				rootFrame = new Frame();

				rootFrame.NavigationFailed += OnNavigationFailed;

				// Place the frame in the current Window
				_mainWindow.Content = rootFrame;
			}

			if (rootFrame.Content is null)
			{
				// When the navigation stack isn't restored navigate to the first page,
				// configuring the new page by passing required information as a navigation
				// parameter
				var startingPageType = typeof(MainPage);
				if (arguments != null)
				{
					rootFrame.Navigate(startingPageType, arguments);
				}
				else
				{
					rootFrame.Navigate(startingPageType);
				}
			}
		}

		private async void HandleLaunchArguments(LaunchActivatedEventArgs launchActivatedEventArgs)
		{
			Console.WriteLine($"HandleLaunchArguments: {launchActivatedEventArgs.Arguments}");

			if (launchActivatedEventArgs.Arguments is not { } args)
			{
				return;
			}

			if (HandleAutoScreenshots(args))
			{
				return;
			}

			if (await HandleRuntimeTests(args))
			{
				return;
			}

			if (TryNavigateToLaunchSample(args))
			{
				return;
			}

			if (!string.IsNullOrEmpty(args))
			{
				var dlg = new MessageDialog(args, "Launch arguments");
				await dlg.ShowAsync();
			}
		}

		/// <summary>
		/// Invoked when Navigation to a certain page fails
		/// </summary>
		/// <param name="sender">The Frame which failed navigation</param>
		/// <param name="e">Details about the navigation failure</param>
		void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception($"Failed to load Page {e.SourcePageType}: {e.Exception}");
		}

		/// <summary>
		/// Invoked when application execution is being suspended.  Application state is saved
		/// without knowing whether the application will be terminated or resumed with the contents
		/// of memory still intact.
		/// </summary>
		/// <param name="sender">The source of the suspend request.</param>
		/// <param name="e">Details about the suspend request.</param>
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			_isSuspended = true;

			var deferral = e.SuspendingOperation.GetDeferral();

			Console.WriteLine($"OnSuspending (Deadline:{e.SuspendingOperation.Deadline})");

			deferral.Complete();
		}

		private void OnResuming(object? sender, object e)
		{
			Console.WriteLine("OnResuming");

			AssertIssue10313ResumingAfterActivate();

			_isSuspended = false;
		}

		public static void ConfigureLogging()
		{
#if HAS_UNO
			System.Threading.Tasks.TaskScheduler.UnobservedTaskException += (s, e) => _log?.Error("UnobservedTaskException", e.Exception);
			AppDomain.CurrentDomain.UnhandledException += (s, e) => _log?.Error("UnhandledException", (e.ExceptionObject as Exception) ?? new Exception("Unknown exception " + e.ExceptionObject));
#endif
			var factory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
			{
#if __WASM__
				builder.AddProvider(new Uno.Extensions.Logging.WebAssembly.WebAssemblyConsoleLoggerProvider());
#else
				builder.AddConsole();
#endif

#if !DEBUG
				// Exclude logs below this level
				builder.SetMinimumLevel(LogLevel.Information);
#else
				// Exclude logs below this level
				builder.SetMinimumLevel(LogLevel.Debug);
#endif

				// Runtime Tests control logging
				builder.AddFilter("Uno.UI.Samples.Tests", LogLevel.Information);

				builder.AddFilter("Uno.UI.Media", LogLevel.Information);

				builder.AddFilter("Uno", LogLevel.Warning);
				builder.AddFilter("Windows", LogLevel.Warning);
				builder.AddFilter("Microsoft", LogLevel.Warning);

				// RemoteControl and HotReload related
				builder.AddFilter("Uno.UI.RemoteControl", LogLevel.Information);

				// Adjust logging when debugging the Given_HotReloadWorkspace tests
				builder.AddFilter("Uno.UI.RuntimeTests.Tests.HotReload.Given_HotReloadWorkspace", LogLevel.Debug);

				// Display Skia related information
				builder.AddFilter("Uno.UI.Runtime.Skia", LogLevel.Debug);
				builder.AddFilter("Uno.UI.Skia", LogLevel.Debug);

				// builder.AddFilter("Uno.Foundation.WebAssemblyRuntime", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.PopupPanel", LogLevel.Debug );

				// Generic Xaml events
				// builder.AddFilter("Microsoft.UI.Xaml", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.Media", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.Shapes", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.VisualStateGroup", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.StateTriggerBase", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.UIElement", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.FrameworkElement", LogLevel.Trace );
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.TextBlock", LogLevel.Debug );

				// Layouter specific messages
				// builder.AddFilter("Microsoft.UI.Xaml.Controls", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.Layouter", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.Panel", LogLevel.Debug );
				// builder.AddFilter("Windows.Storage", LogLevel.Debug );

				// Binding related messages
				// builder.AddFilter("Microsoft.UI.Xaml.Data", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.Data", LogLevel.Debug );

				// Binder memory references tracking
				// builder.AddFilter("Uno.UI.DataBinding.BinderReferenceHolder", LogLevel.Debug );

				// builder.AddFilter(ListView-related messages
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.ListViewBase", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.ListView", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.GridView", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.VirtualizingPanelLayout", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.NativeListViewBase", LogLevel.Debug );
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.ListViewBaseSource", LogLevel.Debug ); //iOS
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.ListViewBaseInternalContainer", LogLevel.Debug ); //iOS
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.NativeListViewBaseAdapter", LogLevel.Debug ); //Android
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.BufferViewCache", LogLevel.Debug ); //Android
				// builder.AddFilter("Microsoft.UI.Xaml.Controls.VirtualizingPanelGenerator", LogLevel.Debug ); //WASM
			});

			Uno.Extensions.LogExtensionPoint.AmbientLoggerFactory = factory;
#if HAS_UNO
			global::Uno.UI.Adapter.Microsoft.Extensions.Logging.LoggingAdapter.Initialize();
			_log = Uno.Foundation.Logging.LogExtensionPoint.Factory.CreateLogger(typeof(App));
#else
			_log = Uno.Extensions.LogExtensionPoint.Log(typeof(App));
#endif
		}

		static void ConfigureFeatureFlags()
		{
#if __IOS__
			Uno.UI.FeatureConfiguration.CommandBar.AllowNativePresenterContent = true;
			WinRTFeatureConfiguration.Focus.EnableExperimentalKeyboardFocus = true;
			Uno.UI.FeatureConfiguration.DatePicker.UseLegacyStyle = true;
			Uno.UI.FeatureConfiguration.TimePicker.UseLegacyStyle = true;
#endif
#if __SKIA__
			Uno.UI.FeatureConfiguration.ToolTip.UseToolTips = true;
#endif
#if HAS_UNO
			Uno.UI.FeatureConfiguration.TextBox.UseOverlayOnSkia = false;
#endif
		}

		/// <summary>
		/// a simple best-effort parsing of CLI args as feature flags
		/// </summary>
		static void ParseCommandLineFeatureFlags()
		{
#if HAS_UNO
			var commandLineArgs = Environment.GetCommandLineArgs();
			if (commandLineArgs.Length == 1)
			{
				return;
			}

			var availableFlags = new Dictionary<string, PropertyInfo>();

			foreach (var featureClass in typeof(FeatureConfiguration).GetNestedTypes(BindingFlags.Public | BindingFlags.Static))
			{
				foreach (var featureProperty in featureClass.GetProperties(BindingFlags.Public | BindingFlags.Static))
				{
					availableFlags[$"{featureClass.Name}.{featureProperty.Name}"] = featureProperty;
				}
			}

			var regex = new Regex(@"^--FeatureConfiguration\.(\w+\.\w+)=(.+)$");
			foreach (var arg in commandLineArgs.Skip(1))
			{
				var match = regex.Match(arg);
				if (match.Success)
				{
					var flag = match.Groups[1].Value;
					var value = match.Groups[2].Value;

					if (availableFlags.TryGetValue(flag, out var property))
					{
						try
						{
							property.SetValue(null, Convert.ChangeType(value, property.PropertyType));
						}
						catch (Exception)
						{
							Console.WriteLine($"Couldn't convert the value {value} of the flag {flag} to {property.PropertyType.Name}");
						}
					}
					else
					{
						Console.WriteLine($"Couldn't find the flag {flag}");
					}
				}
				else if (arg.StartsWith("--FeatureConfiguration"))
				{
					Console.WriteLine($"Failed to parse the CLI argument {arg}");
				}
				else
				{
					Console.WriteLine($"Ignored the CLI argument {arg} for the purposes of FeatureConfiguration.");
				}
			}
#endif
		}

		public static string GetDisplayScreenScaling(string displayId)
			=> (DisplayInformation.GetForCurrentView().LogicalDpi * 100f / 96f).ToString(CultureInfo.InvariantCulture);
	}
}
