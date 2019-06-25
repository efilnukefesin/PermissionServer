using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace WPF.Shared.AttachedProperties
{
    /// <summary>
    /// Class that provides the Watermark attached property
    /// </summary>
    public static class PlaceholderService
    {
        #region Properties

        #region PlaceholderProperty
        /// <summary>
        /// Watermark Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.RegisterAttached(
           "Placeholder",
           typeof(object),
           typeof(PlaceholderService),
           new FrameworkPropertyMetadata((object)null, new PropertyChangedCallback(onPlaceholderChanged)));
        #endregion PlaceholderProperty

        #region itemsControls
        /// <summary>
        /// Dictionary of ItemsControls
        /// </summary>
        private static readonly Dictionary<object, ItemsControl> itemsControls = new Dictionary<object, ItemsControl>();
        #endregion itemsControls

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region GetPlaceholder: Gets the Watermark property.  This dependency property indicates the watermark for the control.
        /// <summary>
        /// Gets the Watermark property.  This dependency property indicates the watermark for the control.
        /// </summary>
        /// <param name="d"><see cref="DependencyObject"/> to get the property from</param>
        /// <returns>The value of the Watermark property</returns>
        public static object GetPlaceholder(DependencyObject d)
        {
            return (object)d.GetValue(PlaceholderProperty);
        }
        #endregion GetPlaceholder

        #region SetPlaceholder: Sets the Watermark property.  This dependency property indicates the watermark for the control.
        /// <summary>
        /// Sets the Watermark property.  This dependency property indicates the watermark for the control.
        /// </summary>
        /// <param name="d"><see cref="DependencyObject"/> to set the property on</param>
        /// <param name="value">value of the property</param>
        public static void SetPlaceholder(DependencyObject d, object value)
        {
            d.SetValue(PlaceholderProperty, value);
        }
        #endregion SetPlaceholder

        #region onPlaceholderChanged: Handles changes to the Watermark property.
        /// <summary>
        /// Handles changes to the Watermark property.
        /// </summary>
        /// <param name="d"><see cref="DependencyObject"/> that fired the event</param>
        /// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
        private static void onPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Control control = (Control)d;
            control.Loaded += control_Loaded;

            if (d is ComboBox)
            {
                control.GotKeyboardFocus += control_GotKeyboardFocus;
                control.LostKeyboardFocus += control_Loaded;
            }
            else if (d is TextBox)
            {
                control.GotKeyboardFocus += control_GotKeyboardFocus;
                control.LostKeyboardFocus += control_Loaded;
                ((TextBox)control).TextChanged += control_GotKeyboardFocus;
            }
            else if (d is PasswordBox)
            {
                control.GotKeyboardFocus += control_GotKeyboardFocus;
                control.LostKeyboardFocus += control_Loaded;
                ((PasswordBox)control).PasswordChanged += control_GotKeyboardFocus;
            }

            if (d is ItemsControl && !(d is ComboBox))
            {
                ItemsControl i = (ItemsControl)d;

                // for Items property  
                i.ItemContainerGenerator.ItemsChanged += itemsChanged;
                itemsControls.Add(i.ItemContainerGenerator, i);

                // for ItemsSource property  
                DependencyPropertyDescriptor prop = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, i.GetType());
                prop.AddValueChanged(i, itemsSourceChanged);
            }
        }
        #endregion onPlaceholderChanged

        #region control_GotKeyboardFocus: Handle the GotFocus event on the control
        /// <summary>
        /// Handle the GotFocus event on the control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
        private static void control_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            Control c = (Control)sender;
            if (PlaceholderService.shouldShowWatermark(c))
            {
                PlaceholderService.showWatermark(c);
            }
            else
            {
                PlaceholderService.removeWatermark(c);
            }
        }
        #endregion control_GotKeyboardFocus

        #region control_Loaded: Handle the Loaded and LostFocus event on the control
        /// <summary>
        /// Handle the Loaded and LostFocus event on the control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
        private static void control_Loaded(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            if (PlaceholderService.shouldShowWatermark(control))
            {
                PlaceholderService.showWatermark(control);
            }
        }
        #endregion control_Loaded

        #region itemsSourceChanged: Event handler for the items source changed event
        /// <summary>
        /// Event handler for the items source changed event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private static void itemsSourceChanged(object sender, EventArgs e)
        {
            ItemsControl c = (ItemsControl)sender;
            if (c.ItemsSource != null)
            {
                if (PlaceholderService.shouldShowWatermark(c))
                {
                    PlaceholderService.showWatermark(c);
                }
                else
                {
                    PlaceholderService.removeWatermark(c);
                }
            }
            else
            {
                PlaceholderService.showWatermark(c);
            }
        }
        #endregion itemsSourceChanged

        #region itemsChanged: Event handler for the items changed event
        /// <summary>
        /// Event handler for the items changed event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="ItemsChangedEventArgs"/> that contains the event data.</param>
        private static void itemsChanged(object sender, ItemsChangedEventArgs e)
        {
            ItemsControl control;
            if (itemsControls.TryGetValue(sender, out control))
            {
                if (PlaceholderService.shouldShowWatermark(control))
                {
                    PlaceholderService.showWatermark(control);
                }
                else
                {
                    PlaceholderService.removeWatermark(control);
                }
            }
        }
        #endregion itemsChanged

        #region removeWatermark: Remove the watermark from the specified element
        /// <summary>
        /// Remove the watermark from the specified element
        /// </summary>
        /// <param name="control">Element to remove the watermark from</param>
        private static void removeWatermark(UIElement control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);

            // layer could be null if control is no longer in the visual tree
            if (layer != null && control != null)
            {
                Adorner[] adorners = layer.GetAdorners(control);
                if (adorners == null)
                {
                    return;
                }

                foreach (Adorner adorner in adorners)
                {
                    if (adorner is PlaceholderAdorner)
                    {
                        adorner.Visibility = Visibility.Hidden;
                        layer.Remove(adorner);
                    }
                }
            }  
        }
        #endregion removeWatermark

        #region showWatermark: Show the watermark on the specified control
        /// <summary>
        /// Show the watermark on the specified control
        /// </summary>
        /// <param name="control">Control to show the watermark on</param>
        private static void showWatermark(Control control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);

            // layer could be null if control is no longer in the visual tree
            if (layer != null && control != null)
            {
                layer.Add(new PlaceholderAdorner(control, PlaceholderService.GetPlaceholder(control)));
            }  
        }
        #endregion showWatermark

        #region shouldShowWatermark: Indicates whether or not the watermark should be shown on the specified control
        /// <summary>
        /// Indicates whether or not the watermark should be shown on the specified control
        /// </summary>
        /// <param name="c"><see cref="Control"/> to test</param>
        /// <returns>true if the watermark should be shown; false otherwise</returns>
        private static bool shouldShowWatermark(Control c)
        {
            bool result = false;
            if (c != null)
            {
                if (c is ComboBox)
                {
                    result = (c as ComboBox).Text == string.Empty;
                }
                else if (c is TextBoxBase)
                {
                    result = (c as TextBox).Text == string.Empty;
                }
                else if (c is PasswordBox)
                {
                    result = (c as PasswordBox).Password == string.Empty;
                }
                else if (c is ItemsControl)
                {
                    result = (c as ItemsControl).Items.Count == 0;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
        #endregion shouldShowWatermark

        #endregion Methods
    }
}
