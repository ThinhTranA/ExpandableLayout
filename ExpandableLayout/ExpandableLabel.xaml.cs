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

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TitleProperty.PropertyName)
            {
                TitleText.Text = Title;
            }
            else if (propertyName == TextProperty.PropertyName)
            {
                ExpandableText.Text = Text;
            }
        }

    }
}
