using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace jdx.ApplManga.Resources.Effects.NoiseEffect {
    public class NoiseEffect : ShaderEffect {
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(NoiseEffect), 0);
        public static readonly DependencyProperty RandomInputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("RandomInput", typeof(NoiseEffect), 1);
        public static readonly DependencyProperty RatioProperty = DependencyProperty.Register("Ratio", typeof(double), typeof(NoiseEffect), new UIPropertyMetadata(((double)(0.5D)), PixelShaderConstantCallback(0)));

        public NoiseEffect() {
            PixelShader pixelShader = new PixelShader {
                UriSource = new Uri("/jdx.ApplManga;component/Resources/Effects/NoiseEffect/Noise.ps", UriKind.Relative)
            };
            PixelShader = pixelShader;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/jdx.ApplManga;component/Resources/Images/noise.png");
            bitmap.EndInit();

            RandomInput =
                new ImageBrush(bitmap) {
                    TileMode = TileMode.Tile,
                    Viewport = new Rect(0, 0, 800, 600),
                    ViewportUnits = BrushMappingMode.Absolute
                };

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(RandomInputProperty);
            UpdateShaderValue(RatioProperty);
        }
        public Brush Input {
            get { return ((Brush)(GetValue(InputProperty))); }
            set { SetValue(InputProperty, value); }
        }

        /// <summary>The second input texture.</summary>
        public Brush RandomInput {
            get { return ((Brush)(GetValue(RandomInputProperty))); }
            set { SetValue(RandomInputProperty, value); }
        }

        public double Ratio {
            get { return ((double)(GetValue(RatioProperty))); }
            set { SetValue(RatioProperty, value); }
        }
    }
}
