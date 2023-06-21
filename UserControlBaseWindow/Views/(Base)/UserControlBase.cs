using System;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Controls;

namespace UserControlBaseWindow.Views
{
    using Utils;

    /// <summary>
    /// Control that allows you to create and show <see cref="Window"/> with a <see cref="UserControl"/> base.
    /// </summary>
    public abstract class UserControlBase : UserControl
    {
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.Register(nameof(Owner), typeof(object), typeof(UserControlBase), new FrameworkPropertyMetadata(null));

        public object Owner
        {
            get => GetValue(OwnerProperty);
            set => SetValue(OwnerProperty, value);
        }

        public static readonly DependencyProperty CanCloseEscapeKeyProperty =
            DependencyProperty.Register(nameof(CanCloseEscapeKey), typeof(bool), typeof(UserControlBase), new PropertyMetadata(default));

        public bool CanCloseEscapeKey
        {
            get => (bool)GetValue(CanCloseEscapeKeyProperty);
            set => SetValue(CanCloseEscapeKeyProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(UserControlBase), new PropertyMetadata(UserControlBaseWindow.CornerRadiusProperty.DefaultMetadata.DefaultValue));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.Register(nameof(CaptionHeight), typeof(double), typeof(UserControlBase), new PropertyMetadata(UserControlBaseWindow.CaptionHeightProperty.DefaultMetadata.DefaultValue));

        public double CaptionHeight
        {
            get => (double)GetValue(CaptionHeightProperty);
            set => SetValue(CaptionHeightProperty, value);
        }

        public static readonly DependencyProperty GlassFrameThicknessProperty =
            DependencyProperty.Register(nameof(GlassFrameThickness), typeof(Thickness), typeof(UserControlBase), new PropertyMetadata(UserControlBaseWindow.GlassFrameThicknessProperty.DefaultMetadata.DefaultValue));

        public Thickness GlassFrameThickness
        {
            get => (Thickness)GetValue(GlassFrameThicknessProperty);
            set => SetValue(GlassFrameThicknessProperty, value);
        }

        public static readonly DependencyProperty ResizeBorderThicknessProperty =
            DependencyProperty.Register(nameof(ResizeBorderThickness), typeof(Thickness), typeof(UserControlBase), new PropertyMetadata(UserControlBaseWindow.ResizeBorderThicknessProperty.DefaultMetadata.DefaultValue));

        public Thickness ResizeBorderThickness
        {
            get => (Thickness)GetValue(ResizeBorderThicknessProperty);
            set => SetValue(ResizeBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty ShowInTaskbarProperty =
            DependencyProperty.Register(nameof(ShowInTaskbar), typeof(bool), typeof(UserControlBase), new FrameworkPropertyMetadata(default));

        public bool ShowInTaskbar
        {
            get => (bool)GetValue(ShowInTaskbarProperty);
            set => SetValue(ShowInTaskbarProperty, value);
        }

        public static readonly DependencyProperty ResizeModeProperty =
            DependencyProperty.Register(nameof(ResizeMode), typeof(ResizeMode), typeof(UserControlBase), new FrameworkPropertyMetadata(ResizeMode.NoResize));

        public ResizeMode ResizeMode
        {
            get => (ResizeMode)GetValue(ResizeModeProperty);
            set => SetValue(ResizeModeProperty, value);
        }

        public static readonly DependencyProperty WindowStartupLocationProperty =
            DependencyProperty.Register(nameof(WindowStartupLocation), typeof(WindowStartupLocation), typeof(UserControlBase), new PropertyMetadata(WindowStartupLocation.Manual));

        public WindowStartupLocation WindowStartupLocation
        {
            get => (WindowStartupLocation)GetValue(WindowStartupLocationProperty);
            set => SetValue(WindowStartupLocationProperty, value);
        }

        public static readonly DependencyProperty WindowStateProperty =
            DependencyProperty.Register(nameof(WindowState), typeof(WindowState), typeof(UserControlBase), new PropertyMetadata(UserControlBaseWindow.WindowStateProperty.DefaultMetadata.DefaultValue, ChangedWindowState));

        private static void ChangedWindowState(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is UserControlBase controlBase) || !(controlBase.ParentWindow is UserControlBaseWindow parentWindow))
                return;

            parentWindow.WindowState = (WindowState)e.NewValue;
        }

        public WindowState WindowState
        {
            get => (WindowState)GetValue(WindowStateProperty);
            set => SetValue(WindowStateProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(UserControlBase), new PropertyMetadata(string.Empty, ChangedTitle));

        private static void ChangedTitle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is UserControlBase controlBase) || !(controlBase.ParentWindow is UserControlBaseWindow parentWindow))
                return;

            parentWindow.Title = e.NewValue as string;
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TopmostProperty =
            DependencyProperty.Register(nameof(Topmost), typeof(bool), typeof(UserControlBase), new PropertyMetadata(UserControlBaseWindow.TopmostProperty.DefaultMetadata.DefaultValue));

        public bool Topmost
        {
            get => (bool)GetValue(TopmostProperty);
            set => SetValue(TopmostProperty, value);
        }

        public static new readonly DependencyProperty WidthProperty =
            DependencyProperty.Register(nameof(Width), typeof(double), typeof(UserControlBase), new FrameworkPropertyMetadata(double.NaN, ChangedWidth));

        private static void ChangedWidth(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // When the size is changed in the maximized state, the Restore size is changed.
            if (!(d is UserControlBase controlBase) || !(controlBase.ParentWindow is UserControlBaseWindow parentWindow) || parentWindow.WindowState == WindowState.Maximized)
                return;

            parentWindow.Width = (double)e.NewValue;
        }

        /// <summary>
        /// Get or set the width of the parent <see cref="UserControlBaseWindow"/>.
        /// </summary>
        public new double Width
        {
            get => (double)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

        public static new readonly DependencyProperty HeightProperty =
            DependencyProperty.Register(nameof(Height), typeof(double), typeof(UserControlBase), new FrameworkPropertyMetadata(double.NaN, ChangedHeight));

        private static void ChangedHeight(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is UserControlBase controlBase) || !(controlBase.ParentWindow is UserControlBaseWindow parentWindow) || parentWindow.WindowState == WindowState.Maximized)
                return;

            parentWindow.Height = (double)e.NewValue;
        }

        /// <summary>
        /// Get or set the height of the parent <see cref="UserControlBaseWindow"/>.
        /// </summary>
        public new double Height
        {
            get => (double)GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        public static new readonly DependencyProperty MinWidthProperty =
            DependencyProperty.Register(nameof(MinWidth), typeof(double), typeof(UserControlBase), new FrameworkPropertyMetadata(0.0, ChangedMinWidth));

        private static void ChangedMinWidth(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is UserControlBase controlBase) || !(controlBase.ParentWindow is UserControlBaseWindow parentWindow))
                return;

            parentWindow.MinWidth = (double)e.NewValue;
        }

        /// <summary>
        /// Get or set the minimum width of the parent <see cref="UserControlBaseWindow"/>.
        /// </summary>
        public new double MinWidth
        {
            get => (double)GetValue(MinWidthProperty);
            set => SetValue(MinWidthProperty, value);
        }

        public static new readonly DependencyProperty MinHeightProperty =
            DependencyProperty.Register(nameof(MinHeight), typeof(double), typeof(UserControlBase), new FrameworkPropertyMetadata(0.0, ChangedMinHeight));

        private static void ChangedMinHeight(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is UserControlBase controlBase) || !(controlBase.ParentWindow is UserControlBaseWindow parentWindow))
                return;

            parentWindow.MinHeight = (double)e.NewValue;
        }

        /// <summary>
        /// Get or set the minimum height of the parent <see cref="UserControlBaseWindow"/>.
        /// </summary>
        public new double MinHeight
        {
            get => (double)GetValue(MinHeightProperty);
            set => SetValue(MinHeightProperty, value);
        }

        public static new readonly DependencyProperty MaxWidthProperty =
            DependencyProperty.Register(nameof(MaxWidth), typeof(double), typeof(UserControlBase), new FrameworkPropertyMetadata(double.PositiveInfinity, ChangedMaxWidth));

        private static void ChangedMaxWidth(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is UserControlBase controlBase) || !(controlBase.ParentWindow is UserControlBaseWindow parentWindow))
                return;

            parentWindow.MaxWidth = (double)e.NewValue;
        }

        /// <summary>
        /// Get or set the maximum width of the parent <see cref="UserControlBaseWindow"/>.
        /// </summary>
        public new double MaxWidth
        {
            get => (double)GetValue(MaxWidthProperty);
            set => SetValue(MaxWidthProperty, value);
        }

        public static new readonly DependencyProperty MaxHeightProperty =
            DependencyProperty.Register(nameof(MaxHeight), typeof(double), typeof(UserControlBase), new FrameworkPropertyMetadata(double.PositiveInfinity, ChangedMaxHeight));

        private static void ChangedMaxHeight(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is UserControlBase controlBase) || !(controlBase.ParentWindow is UserControlBaseWindow parentWindow))
                return;

            parentWindow.MaxHeight = (double)e.NewValue;
        }

        /// <summary>
        /// Get or set the maximum height of the parent <see cref="UserControlBaseWindow"/>.
        /// </summary>
        public new double MaxHeight
        {
            get => (double)GetValue(MaxHeightProperty);
            set => SetValue(MaxHeightProperty, value);
        }

        /// <summary>
        /// Get the parent <see cref="UserControlBaseWindow"/>.
        /// </summary>
        internal UserControlBaseWindow ParentWindow => Utils.FindControls.FindVisualParent<UserControlBaseWindow>(this);

        #region [Method+] Activate
        /// <summary>
        /// Activate the parent window.
        /// </summary>
        public virtual void Activate()
        {
            ParentWindow?.Activate();
        }
        #endregion

        #region [Method+] Show
        /// <summary>
        /// Show modeless window.
        /// </summary>
        public virtual void Show()
        {
            if (base.IsLoaded && ParentWindow is UserControlBaseWindow parent)
            {
                if (parent.WindowState == WindowState.Minimized)
                    parent.WindowState = WindowState.Normal;

                parent.Activate();
                return;
            }
            CreateWindow()?.Show();
        }
        #endregion

        #region [Method+] ShowDialog
        /// <summary>
        /// Show modal window.
        /// </summary>
        /// <returns><see cref="Window.DialogResult"/></returns>
        public virtual bool? ShowDialog()
        {
            return CreateWindow()?.ShowDialog();
        }
        #endregion

        #region [Method+] Close
        /// <summary>
        /// Close window.
        /// </summary>
        public virtual void Close()
        {
            ParentWindow?.Close();
        }
        #endregion

        #region [Method#] OnParentWindowClosing
        /// <summary>
        /// Called when an parent <see cref="Window.Closing"/> event occurs.
        /// </summary>
        /// <param name="sender">Parent window</param>
        protected internal virtual void OnParentWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Debug.WriteLine($"{this} fire closing.");
        }
        #endregion

        #region [Method#] OnParentWindowClosed
        /// <summary>
        /// Called when an parent <see cref="Window.Closed"/> event occurs.
        /// </summary>
        /// <param name="sender">Parent window</param>
        protected internal virtual void OnParentWindowClosed(object sender, EventArgs e)
        {
            Debug.WriteLine($"{this} fire closed.");
        }
        #endregion

        #region [Method-] CreateWindow
        /// <summary>
        /// Create a <see cref="UserControlBaseWindow"/> with the <see cref="UserControlBase"/> as <see cref="ContentControl"/>.
        /// </summary>
        /// <returns>Returns the <see cref="UserControlBaseWindow"/> created.</returns>
        private UserControlBaseWindow CreateWindow()
        {
            var window = new UserControlBaseWindow()
            {
                // Fixed value =======================
                SizeToContent = SizeToContent.Manual,
                WindowStyle = WindowStyle.None,
                AllowsTransparency = true,
                // ================================

                ResizeMode = ResizeMode,
                CanCloseEscapeKey = CanCloseEscapeKey,
                CaptionHeight = CaptionHeight,
                CornerRadius = CornerRadius,
                GlassFrameThickness = GlassFrameThickness,
                ResizeBorderThickness = ResizeBorderThickness,
                ShowInTaskbar = ShowInTaskbar,
                WindowStartupLocation = WindowStartupLocation,
                WindowState = WindowState,
                Topmost = Topmost,
                Width = Width,
                Height = Height,
                MinWidth = MinWidth,
                MinHeight = MinHeight,
                MaxWidth = MaxWidth,
                MaxHeight = MaxHeight,
                Title = Title,
                Content = this,
            };

            switch (Owner)
            {
                case Window ownerWindow:
                    window.Owner = ownerWindow;
                    break;
                case UserControlBase controlBase:
                    {
                        if (!(controlBase.ParentWindow is UserControlBaseWindow parentWindow))
                            break;

                        window.Owner = parentWindow;
                    }
                    break;
                case IntPtr ownerHandle:
                    {
                        if (ownerHandle == IntPtr.Zero)
                            break;

                        new WindowInteropHelper(window) { Owner = ownerHandle };
                    }
                    break;
                default:
                    window.Owner = Application.Current.MainWindow;
                    break;
            }

            return window;
        }
        #endregion

        #region [Class~] UserControlBaseWindow
        /// <summary>
        /// <see cref="Window"/> created as the parent of <see cref="UserControlBase"/>.
        /// </summary>
        internal sealed class UserControlBaseWindow : Window
        {
            public static readonly DependencyProperty CanCloseEscapeKeyProperty =
              DependencyProperty.Register(nameof(CanCloseEscapeKey), typeof(bool), typeof(UserControlBaseWindow), new UIPropertyMetadata(default));

            public bool CanCloseEscapeKey
            {
                get => (bool)GetValue(CanCloseEscapeKeyProperty);
                set => SetValue(CanCloseEscapeKeyProperty, value);
            }

            public static readonly DependencyProperty CornerRadiusProperty =
                DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(UserControlBaseWindow), new PropertyMetadata(default));

            public CornerRadius CornerRadius
            {
                get => (CornerRadius)GetValue(CornerRadiusProperty);
                set => SetValue(CornerRadiusProperty, value);
            }

            public static readonly DependencyProperty CaptionHeightProperty =
                DependencyProperty.Register(nameof(CaptionHeight), typeof(double), typeof(UserControlBaseWindow), new PropertyMetadata(20.0));

            public double CaptionHeight
            {
                get => (double)GetValue(CaptionHeightProperty);
                set => SetValue(CaptionHeightProperty, value);
            }

            public static readonly DependencyProperty GlassFrameThicknessProperty =
                DependencyProperty.Register(nameof(GlassFrameThickness), typeof(Thickness), typeof(UserControlBaseWindow), new PropertyMetadata(default));

            public Thickness GlassFrameThickness
            {
                get => (Thickness)GetValue(GlassFrameThicknessProperty);
                set => SetValue(GlassFrameThicknessProperty, value);
            }

            public static readonly DependencyProperty ResizeBorderThicknessProperty =
                DependencyProperty.Register(nameof(ResizeBorderThickness), typeof(Thickness), typeof(UserControlBaseWindow), new PropertyMetadata(new Thickness(2.0)));

            public Thickness ResizeBorderThickness
            {
                get => (Thickness)GetValue(ResizeBorderThicknessProperty);
                set => SetValue(ResizeBorderThicknessProperty, value);
            }

            private UserControlBase ContentUserControlBase => base.Content as UserControlBase;

            internal UserControlBaseWindow()
            {
                CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, (sender, e) => SystemCommands.MinimizeWindow(this), (sender, e) => e.CanExecute = ResizeMode != ResizeMode.NoResize));
                CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, (sender, e) => SystemCommands.MaximizeWindow(this), (sender, e) => e.CanExecute = ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip));
                CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, (sender, e) => SystemCommands.RestoreWindow(this), (sender, e) => e.CanExecute = ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip));
                CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, (sender, e) => SystemCommands.CloseWindow(this)));
            }

            ~UserControlBaseWindow()
            {
                Debug.WriteLine($"Deconstructor {this}.");
            }

            protected override void OnSourceInitialized(EventArgs e)
            {
                base.OnSourceInitialized(e);

                var handle = (new WindowInteropHelper(this)).Handle;
                HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(Win32.WindowProc));
            }

            protected override void OnStateChanged(EventArgs e)
            {
                base.OnStateChanged(e);

                if (!(ContentUserControlBase is UserControlBase controlBase))
                    return;

                controlBase.WindowState = base.WindowState;
            }

            protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
            {
                base.OnRenderSizeChanged(sizeInfo);

                if (!(ContentUserControlBase is UserControlBase controlBase))
                    return;

                controlBase.Width = sizeInfo.NewSize.Width;
                controlBase.Height = sizeInfo.NewSize.Height;
            }

            protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
            {
                ContentUserControlBase?.OnParentWindowClosing(this, e);
                base.OnClosing(e);
            }

            protected override void OnClosed(EventArgs e)
            {
                ContentUserControlBase?.OnParentWindowClosed(this, e);
                base.OnClosed(e);
            }
        }
        #endregion
    }
}
