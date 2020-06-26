using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace EntFrm.Framework.Utility
{
    public partial class RdGroupBoxEx : GroupBox
	{
		// Constants
		private const int RADIOBUTTON_X_OFFSET = 10;
		private const int RADIOBUTTON_Y_OFFSET = -2;

		// Members
		private bool m_bDisableChildrenIfUnchecked;

		/// <summary>
		/// RadioGroupBox public constructor.
		/// </summary>
        public RdGroupBoxEx()
		{
			this.InitializeComponent();
			this.m_bDisableChildrenIfUnchecked = false;
			this.m_radioButton.Parent = this;
			this.m_radioButton.Location = new System.Drawing.Point(RADIOBUTTON_X_OFFSET, RADIOBUTTON_Y_OFFSET);
			this.Checked = false;

			// Set the color of the RadioButon's text to the color of the label in a standard groupbox control.
			VisualStyleRenderer vsr = new VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Normal);
			Color groupBoxTextColor = vsr.GetColor(ColorProperty.TextColor);
			this.m_radioButton.ForeColor = groupBoxTextColor;
		}

		#region Properties
		/// <summary>
		/// The text associated with the control.
		/// </summary>
		public override string Text
		{
			get
			{
				if(this.Site != null && this.Site.DesignMode == true)
				{
					// Design-time mode
					return this.m_radioButton.Text;
				}
				else
				{
					// Run-time
					return " "; // Set the text of the GroupBox to a space, so the gap appears before the RadioButton.
				}
			}
			set
			{
				base.Text = " "; // Set the text of the GroupBox to a space, so the gap appears before the RadioButton.
				this.m_radioButton.Text = value;
			}
		}

		/// <summary>
		/// Indicates whether the radio button is checked or not.
		/// </summary>
		[Description("Indicates whether the radio button is checked or not.")]
		[Category("Appearance")]
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return this.m_radioButton.Checked;
			}
			set
			{
				if(this.m_radioButton.Checked != value)
				{
					this.m_radioButton.Checked = value;
				}
			}
		}

		/// <summary>
		/// Determines if child controls of the GroupBox are disabled when the CheckBox is unchecked.
		/// </summary>
		[Description("Determines if child controls of the GroupBox are disabled when the RadioButton is unchecked.")]
		[Category("Appearance")]
		[DefaultValue(false)]
		public bool DisableChildrenIfUnchecked
		{
			get
			{
				return this.m_bDisableChildrenIfUnchecked;
			}
			set
			{
				if(this.m_bDisableChildrenIfUnchecked != value)
				{
					this.m_bDisableChildrenIfUnchecked = value;
				}
			}
		}
		#endregion Properties

		#region Event Handlers
		/// <summary>
		/// Occurs when the 'checked' property changes value.
		/// </summary>
		[Description("Occurs when the 'checked' property changes value.")]
		public event EventHandler CheckedChanged;

		//
		// Summary:
		//     Raises the System.Windows.Forms.RadioButton.checkBox_CheckedChanged event.
		/// <summary>
		/// Raises the System.Windows.Forms.
		/// </summary>
		/// <param name="e">An System.EventArgs that contains the event data.</param>
		protected virtual void OnCheckedChanged(EventArgs e)
		{
		}
		#endregion Event Handlers

		#region Events
		private void radioButton_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			if(radioButton == null)
				return;

            RdGroupBoxEx target = radioButton.Parent as RdGroupBoxEx;
			if(target == null)
				return;

			if(this.m_bDisableChildrenIfUnchecked == true)
			{
				bool bEnabled = this.m_radioButton.Checked;
				foreach(Control control in this.Controls)
				{
					if(control != this.m_radioButton)
					{
						control.Enabled = bEnabled;
					}
				}
			}

			if(target.Checked == false)
				return;

			Control parentControl = target.Parent;
			if(parentControl == null)
				return;

			foreach(Control childControl in parentControl.Controls)
			{
                if (childControl is RdGroupBoxEx)
				{
					if(childControl != this)
					{
                        (childControl as RdGroupBoxEx).Checked = false;
					}
				}
			}

			if(CheckedChanged != null)
			{
				CheckedChanged(sender, e);
			}
		}

		private void CheckGroupBox_ControlAdded(object sender, ControlEventArgs e)
		{
			if(this.m_bDisableChildrenIfUnchecked == true)
			{
				e.Control.Enabled = this.Checked;
			}
		}
		#endregion Events
	}
}
