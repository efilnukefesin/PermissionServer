using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPF.Shared.AttachedProperties
{
    /// <summary>
    /// Adorner for the watermark
    /// </summary>
    internal class PlaceholderAdorner : Adorner
    {
        #region Properties

        #region contentPresenter: ContentPresenter that holds the watermark
        /// <summary>
        /// <see cref="ContentPresenter"/> that holds the watermark
        /// </summary>
        private readonly ContentControl contentControl;
        #endregion contentPresenter

        #region VisualChildrenCount: Gets the number of children for the ContainerVisual
        /// <summary>
        /// Gets the number of children for the <see cref="ContainerVisual"/>.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }
        #endregion VisualChildrenCount

        #region control: Gets the control that is being adorned
        /// <summary>
        /// Gets the control that is being adorned
        /// </summary>
        private Control control
        {
            get { return (Control)this.AdornedElement; }
        }
        #endregion control

        #endregion Properties

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="WatermarkAdorner"/> class
        /// </summary>
        /// <param name="adornedElement"><see cref="UIElement"/> to be adorned</param>
        /// <param name="watermark">The watermark</param>
        public PlaceholderAdorner(UIElement adornedElement, object watermark) : base(adornedElement)
        {
            if ((watermark is FrameworkElement) && (this.control is FrameworkElement))
            {
                //(watermark as FrameworkElement).DataContext = (this.control as FrameworkElement).DataContext;

                //((FrameworkElement)watermark).GetBindingExpression(FrameworkElement.DataContextProperty).UpdateTarget();
                Binding dataContextBinding = new Binding("DataContext");
                dataContextBinding.Source = (this.control as FrameworkElement);
                (watermark as FrameworkElement).SetBinding(FrameworkElement.DataContextProperty, dataContextBinding);
                ((FrameworkElement)watermark).GetBindingExpression(FrameworkElement.DataContextProperty).UpdateTarget();
            }

            this.IsHitTestVisible = false;

            this.contentControl = new ContentControl();
            this.contentControl.Content = watermark;
            this.contentControl.Opacity = 0.5;
            this.contentControl.Margin = new Thickness(control.Margin.Left + control.Padding.Left, control.Margin.Top + control.Padding.Top, 0, 0);

            if (this.control is ItemsControl && !(this.control is ComboBox))
            {
                this.contentControl.VerticalAlignment = VerticalAlignment.Center;
                this.contentControl.HorizontalAlignment = HorizontalAlignment.Center;
            }

            // Hide the control adorner when the adorned element is hidden
            Binding binding = new Binding("IsVisible");
            binding.Source = adornedElement;
            binding.Converter = new BooleanToVisibilityConverter();
            this.SetBinding(VisibilityProperty, binding);
        }

        #endregion Construction

        #region Methods

        #region GetVisualChild: Returns a specified child for the parent.
        /// <summary>
        /// Returns a specified child <see cref="Visual"/> for the parent <see cref="ContainerVisual"/>.
        /// </summary>
        /// <param name="index">A 32-bit signed integer that represents the index value of the child <see cref="Visual"/>. The value of index must be between 0 and <see cref="VisualChildrenCount"/> - 1.</param>
        /// <returns>The child <see cref="Visual"/>.</returns>
        protected override Visual GetVisualChild(int index)
        {
            return this.contentControl;
        }
        #endregion GetVisualChild

        #region MeasureOverride: Implements any custom measuring behavior for the adorner.
        /// <summary>
        /// Implements any custom measuring behavior for the adorner.
        /// </summary>
        /// <param name="constraint">A size to constrain the adorner to.</param>
        /// <returns>A <see cref="Size"/> object representing the amount of layout space needed by the adorner.</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            // Here's the secret to getting the adorner to cover the whole control
            this.contentControl.Measure(control.RenderSize);
            return control.RenderSize;
        }
        #endregion MeasureOverride

        #region ArrangeOverride: When overridden in a derived class, positions child elements and determines a size for a FrameworkElement derived class. 
        /// <summary>
        /// When overridden in a derived class, positions child elements and determines a size for a <see cref="FrameworkElement"/> derived class. 
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        /// <returns>The actual size used.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            this.contentControl.Arrange(new Rect(finalSize));
            return finalSize;
        }
        #endregion ArrangeOverride

        #endregion Methods
    }
}
