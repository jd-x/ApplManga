using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace jdx.ApplManga.Resources.Effects.BlurEffect {
    public class BlurEffect : ShaderEffect {
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(BlurEffect), 0);
        public static readonly DependencyProperty TopLeftCornerProperty = DependencyProperty.Register("TopLeftCorner", typeof(Point), typeof(BlurEffect), new UIPropertyMetadata(new Point(0, 0), PixelShaderConstantCallback(0)));
        public static readonly DependencyProperty BottomRightCornerProperty = DependencyProperty.Register("BottomRightCorner", typeof(Point), typeof(BlurEffect), new UIPropertyMetadata(new Point(1, 1), PixelShaderConstantCallback(1)));
        public static readonly DependencyProperty FrameworkElementProperty = DependencyProperty.Register("FrameworkElement", typeof(FrameworkElement), typeof(BlurEffect), new PropertyMetadata(null, OnFrameworkElementPropertyChanged));

        private static PropertyInfo propertyInfo;

        public BlurEffect() {
            PixelShader pixelShader = new PixelShader {
                UriSource = new Uri("/jdx.ApplManga;component/Resources/Effects/BlurEffect/Blur.ps", UriKind.Relative)
            };
            propertyInfo = typeof(BlurEffect).GetProperty("InheritanceContext", BindingFlags.Instance | BindingFlags.NonPublic);
            PixelShader = pixelShader;

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(TopLeftCornerProperty);
            UpdateShaderValue(BottomRightCornerProperty);
        }
        public Brush Input {
            get { return (Brush)(GetValue(InputProperty)); }
            set { SetValue(InputProperty, value); }
        }

        public Point TopLeftCorner {
            get { return (Point)GetValue(TopLeftCornerProperty); }
            set { SetValue(TopLeftCornerProperty, value); }
        }

        public Point BottomRightCorner {
            get { return (Point)GetValue(BottomRightCornerProperty); }
            set { SetValue(BottomRightCornerProperty, value); }
        }

        public FrameworkElement FrameworkElement {
            get { return (FrameworkElement)GetValue(FrameworkElementProperty); }
            set { SetValue(FrameworkElementProperty, value); }
        }

        private FrameworkElement GetInheritanceContext() { return propertyInfo.GetValue(this, null) as FrameworkElement; }

        private void UpdateEffect(object sender, EventArgs e) {
            Rect underRect;
            Rect overRect;
            Rect intersect;

            FrameworkElement under = GetInheritanceContext();
            FrameworkElement over = this.FrameworkElement;

            Point origin = under.PointFromScreen(new Point(0, 0));
            underRect = new Rect(origin.X, origin.Y, under.ActualWidth, under.ActualHeight);

            origin = over.PointToScreen(new Point(0, 0));
            overRect = new Rect(origin.X, origin.Y, under.ActualWidth, under.ActualHeight);

            intersect = Rect.Intersect(overRect, underRect);

            if (intersect.IsEmpty) {
                TopLeftCorner = new Point(0, 0);
                BottomRightCorner = new Point(0, 0);
            } else {
                origin = new Point(intersect.X, intersect.Y);
                origin = under.PointFromScreen(origin);

                TopLeftCorner = new Point(origin.X / under.ActualWidth, origin.Y / under.ActualHeight);
                BottomRightCorner = new Point(TopLeftCorner.X + (intersect.Width / under.ActualWidth), TopLeftCorner.Y + (intersect.Height / under.ActualHeight));
            }
        }

        private static void OnFrameworkElementPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BlurEffect blurEffect = (BlurEffect)d;

            if (e.OldValue is FrameworkElement frameworkElement) {
                frameworkElement.LayoutUpdated -= blurEffect.UpdateEffect;
            }

            frameworkElement = e.NewValue as FrameworkElement;

            if (frameworkElement != null) {
                frameworkElement.LayoutUpdated += blurEffect.UpdateEffect;
            }
        }
    }
}
