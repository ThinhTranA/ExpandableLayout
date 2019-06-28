using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ExpandableLayout
{
    public partial class ExpandableLabel : ContentView
    {
        private bool _IsExpanded;
        private bool _IsExpanding;

        public ExpandableLabel()
        {
            InitializeComponent();
            expandIcon.Source = ImageSource.FromResource("ExpandableLayout.Icons.expand_icon.png");

            ExpandableLayout.HeightRequest = 0;
            ExpandableText.Text = Text;
            TitleText.Text = Title;
        }

        private async void Title_Clicked(object sender, EventArgs e)
        {
            if (!_IsExpanding)
            {
                _IsExpanding = true;
                var height = ExpandableContent.Height;
                if (_IsExpanded)
                {
                    //expandIcon.RotateTo(0);
                    expandIcon.RotateXTo(0);
                    var animation = new Animation(v => ExpandableLayout.HeightRequest = v, height, 0);
                    await ExpandableLayout.FadeTo(0, 250);
                    animation.Commit(this, "ExpandSize", 16, 250);

                }
                else
                {
                    //expandIcon.RotateTo(180);
                    expandIcon.RotateXTo(180);
                    var animation = new Animation(v => ExpandableLayout.HeightRequest = v, 0, height);
                    animation.Commit(this, "ExpandSize", 16, 250);
                    await ExpandableLayout.FadeTo(1, 250);
                }
                _IsExpanded = !_IsExpanded;
                _IsExpanding = false;
            }
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                                    propertyName: "Title",
                                    returnType: typeof(string),
                                    declaringType: typeof(ExpandableLabel),
                                    defaultValue: default(string));

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
                                    propertyName: "Text",
                                    returnType: typeof(string),
                                    declaringType: typeof(ExpandableLabel),
                                    defaultValue: default(string));
        public static readonly BindableProperty TitleTextColorProperty = BindableProperty.Create(
                                    propertyName: "TitleTextColor",
                                    returnType: typeof(Color),
                                    declaringType: typeof(ExpandableLabel),
                                    defaultValue: default(Color)
                                    );
        public static readonly BindableProperty TitleBackgroundColorProperty = BindableProperty.Create(
                                    propertyName: "TitleBackgroundColor",
                                    returnType: typeof(Color),
                                    declaringType: typeof(ExpandableLabel),
                                    defaultValue: Color.Blue
                                    );
        
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public Color TitleTextColor
        {
            get => (Color)GetValue(TitleTextColorProperty);
            set => SetValue(TitleTextColorProperty, value);
        }
        public Color TitleBackgroundColor
        {
            get => (Color)GetValue(TitleBackgroundColorProperty);
            set => SetValue(TitleBackgroundColorProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TitleProperty.PropertyName)
                TitleText.Text = Title;
            else if (propertyName == TextProperty.PropertyName)
                ExpandableText.Text = Text;
            else if (propertyName == TitleTextColorProperty.PropertyName)
                TitleText.TextColor = TitleTextColor;
            else if (propertyName == TitleBackgroundColorProperty.PropertyName)
                Header.BackgroundColor = TitleBackgroundColor;
        }

    }
}
