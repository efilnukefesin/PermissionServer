using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace WPF.Shared.UserControls
{
    /// <summary>
    /// Interaktionslogik für DelayingTextInputControl.xaml
    /// </summary>
    public partial class DelayingTextInputControl : BaseUserControl
    {
        #region Properties

        #region Text Property
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(DelayingTextInputControl), new PropertyMetadata(string.Empty, Text_ValueChanged));

        static void Text_ValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            DelayingTextInputControl self = obj as DelayingTextInputControl;
            if (self.TextChanged != null) self.TextChanged(self, new EventArgs());

            self.UpdateUI();
        }

        [Description("The Text to display"), Category("Own Properties"), DisplayName("Text")]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public event EventHandler TextChanged;
        #endregion Text Property

        #region Hint Property
        public static readonly DependencyProperty HintProperty = DependencyProperty.Register("Hint", typeof(string), typeof(DelayingTextInputControl), new PropertyMetadata(string.Empty, Hint_ValueChanged));

        static void Hint_ValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            DelayingTextInputControl self = obj as DelayingTextInputControl;
            if (self.HintChanged != null) self.HintChanged(self, new EventArgs());

            self.UpdateUI();
        }

        [Description("The Hint to Show"), Category("Own Properties"), DisplayName("Hint")]
        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        public event EventHandler HintChanged;
        #endregion Hint Property

        #region Delay Property
        public static readonly DependencyProperty DelayProperty = DependencyProperty.Register("Delay", typeof(TimeSpan), typeof(DelayingTextInputControl), new PropertyMetadata(new TimeSpan(0,0,0,0,500), Delay_ValueChanged));

        static void Delay_ValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            DelayingTextInputControl self = obj as DelayingTextInputControl;
            if (self.DelayChanged != null) self.DelayChanged(self, new EventArgs());

            self.UpdateUI();
        }

        [Description("The time to wait to fire the action"), Category("Own Properties"), DisplayName("Delay")]
        public TimeSpan Delay
        {
            get { return (TimeSpan)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }

        public event EventHandler DelayChanged;
        #endregion Delay Property

        public Action ActionToExecute { get; set; }

        private Timer DelayTimer;

        #endregion Properties

        #region Construction

        public DelayingTextInputControl() : base()
        {
            InitializeComponent();

            this.DelayTimer = new Timer(this.Delay.TotalMilliseconds);
            this.DelayTimer.Elapsed += this.delayTimer_Elapsed;
        }

        #endregion Construction

        #region Methods

        #region UpdateUI
        private void UpdateUI()
        {
            // reset / start timer with all params
            this.DelayTimer?.Stop();
            this.DelayTimer.Elapsed -= this.delayTimer_Elapsed;
            this.DelayTimer = null;
            this.DelayTimer = new Timer(this.Delay.TotalMilliseconds);
            this.DelayTimer.Elapsed += this.delayTimer_Elapsed;
            this.DelayTimer?.Start();
        }
        #endregion UpdateUI

        #region delayTimer_Elapsed
        private void delayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // stop timer
            this.DelayTimer.Stop();

            //execute Action
            this.ActionToExecute?.Invoke();
        }
        #endregion delayTimer_Elapsed

        #region receiveMessage
        protected override bool receiveMessage(string Text, object Data)
        {
            bool result = false;
            if (Text.Equals(this.Name + "Action"))
            {
                this.ActionToExecute = (Action)Data;
                result = true;
            }
            return result;
        }
        #endregion receiveMessage

        #endregion Methods

        #region Events

        #endregion Events
    }
}
