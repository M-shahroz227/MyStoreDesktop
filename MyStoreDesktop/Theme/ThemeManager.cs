using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyStoreDesktop.Theme
{
    /// <summary>
    /// Helper class to apply professional blue theme to forms and controls
    /// </summary>
    public static class ThemeManager
    {
        /// <summary>
        /// Apply the professional blue theme to a form
        /// </summary>
        public static void ApplyTheme(Form form)
        {
            if (form == null) return;

            // Set form background
            form.BackColor = AppTheme.FormBackground;
            form.Font = AppTheme.BodyFont;

            // Apply theme to all controls recursively
            ApplyThemeToControls(form.Controls);
        }

        /// <summary>
        /// Apply theme to a collection of controls
        /// </summary>
        private static void ApplyThemeToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                ApplyThemeToControl(control);

                // Recursively apply to child controls
                if (control.HasChildren)
                {
                    ApplyThemeToControls(control.Controls);
                }
            }
        }

        /// <summary>
        /// Apply theme to a specific control based on its type
        /// </summary>
        private static void ApplyThemeToControl(Control control)
        {
            switch (control)
            {
                case Button btn:
                    ApplyButtonTheme(btn);
                    break;

                case Panel panel:
                    ApplyPanelTheme(panel);
                    break;

                case GroupBox groupBox:
                    ApplyGroupBoxTheme(groupBox);
                    break;

                case LinkLabel linkLabel:
                    ApplyLinkLabelTheme(linkLabel);
                    break;

                case Label label:
                    ApplyLabelTheme(label);
                    break;

                case TextBox textBox:
                    ApplyTextBoxTheme(textBox);
                    break;

                case ComboBox comboBox:
                    ApplyComboBoxTheme(comboBox);
                    break;

                case DataGridView dgv:
                    ApplyDataGridViewTheme(dgv);
                    break;

                case TabControl tabControl:
                    ApplyTabControlTheme(tabControl);
                    break;

                case MenuStrip menuStrip:
                    ApplyMenuStripTheme(menuStrip);
                    break;
            }
        }

        /// <summary>
        /// Apply theme to buttons with smart color detection
        /// </summary>
        private static void ApplyButtonTheme(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = AppTheme.ButtonFont;
            button.Cursor = Cursors.Hand;
            button.ForeColor = Color.White;

            // Determine button color based on name or text
            string btnText = button.Text.ToLower();
            string btnName = button.Name.ToLower();

            if (btnText.Contains("delete") || btnText.Contains("remove") || btnText.Contains("cancel") ||
                btnName.Contains("delete") || btnName.Contains("remove") || btnName.Contains("cancel"))
            {
                button.BackColor = AppTheme.AccentRed;
                button.FlatAppearance.MouseOverBackColor = AppTheme.AccentRedDark;
            }
            else if (btnText.Contains("edit") || btnText.Contains("update") || btnText.Contains("modify") ||
                     btnName.Contains("edit") || btnName.Contains("update") || btnName.Contains("modify"))
            {
                button.BackColor = AppTheme.AccentOrange;
                button.FlatAppearance.MouseOverBackColor = AppTheme.AccentOrangeDark;
            }
            else if (btnText.Contains("save") || btnText.Contains("confirm") || btnText.Contains("add") ||
                     btnText.Contains("btnConfirm") || btnText.Contains("ok") ||
                     btnName.Contains("save") || btnName.Contains("confirm") || btnName.Contains("add"))
            {
                button.BackColor = AppTheme.PrimaryBlueLight;
                button.FlatAppearance.MouseOverBackColor = AppTheme.ShadowColor;
            }
            else
            {
                // Default to primary blue
                button.BackColor = AppTheme.PrimaryBlue;
                button.FlatAppearance.MouseOverBackColor = AppTheme.PrimaryBlueDark;
            }

            // Padding for better appearance
            button.Padding = new Padding(10, 5, 10, 5);
        }

        /// <summary>
        /// Apply theme to panels
        /// </summary>
        private static void ApplyPanelTheme(Panel panel)
        {
            // Check if this is a main panel (larger panels are likely content panels)
            if (panel.Width > 300 || panel.Height > 300)
            {
                panel.BackColor = AppTheme.PanelBackground;
            }
            else
            {
                panel.BackColor = AppTheme.BackgroundWhite;
            }
        }

        /// <summary>
        /// Apply theme to group boxes
        /// </summary>
        private static void ApplyGroupBoxTheme(GroupBox groupBox)
        {
            groupBox.ForeColor = AppTheme.TextPrimary;
            groupBox.Font = AppTheme.SubHeadingFont;
        }

        /// <summary>
        /// Apply theme to labels
        /// </summary>
        private static void ApplyLabelTheme(Label label)
        {
            // Check if it's a title label (larger font)
            if (label.Font.Size >= 16)
            {
                label.ForeColor = AppTheme.PrimaryBlue;
                label.Font = AppTheme.TitleFont;
            }
            else if (label.Font.Size >= 12)
            {
                label.ForeColor = AppTheme.TextPrimary;
                label.Font = AppTheme.SubHeadingFont;
            }
            else
            {
                label.ForeColor = AppTheme.TextSecondary;
                label.Font = AppTheme.BodyFont;
            }
        }

        /// <summary>
        /// Apply theme to text boxes
        /// </summary>
        private static void ApplyTextBoxTheme(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = AppTheme.BodyFont;
            textBox.ForeColor = AppTheme.TextPrimary;
            textBox.BackColor = Color.White;
        }

        /// <summary>
        /// Apply theme to combo boxes
        /// </summary>
        private static void ApplyComboBoxTheme(ComboBox comboBox)
        {
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Font = AppTheme.BodyFont;
            comboBox.ForeColor = AppTheme.TextPrimary;
            comboBox.BackColor = Color.White;
        }

        /// <summary>
        /// Apply theme to data grid views
        /// </summary>
        private static void ApplyDataGridViewTheme(DataGridView dgv)
        {
            dgv.BackgroundColor = AppTheme.BackgroundWhite;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = AppTheme.BorderGray;
            dgv.DefaultCellStyle.SelectionBackColor = AppTheme.PrimaryBlueLight;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Font = AppTheme.BodyFont;
            dgv.DefaultCellStyle.ForeColor = AppTheme.TextPrimary;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = AppTheme.GridAlternateRow;

            // Header style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.GridHeaderBackground;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = AppTheme.GridHeaderText;
            dgv.ColumnHeadersDefaultCellStyle.Font = AppTheme.SubHeadingFont;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgv.ColumnHeadersHeight = 40;
            dgv.RowTemplate.Height = 35;
        }

        /// <summary>
        /// Apply theme to tab controls
        /// </summary>
        private static void ApplyTabControlTheme(TabControl tabControl)
        {
            tabControl.Font = AppTheme.BodyFont;
        }

        /// <summary>
        /// Apply theme to menu strips
        /// </summary>
        private static void ApplyMenuStripTheme(MenuStrip menuStrip)
        {
            menuStrip.BackColor = AppTheme.PrimaryBlue;
            menuStrip.ForeColor = Color.White;
            menuStrip.Font = AppTheme.BodyFont;

            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                item.ForeColor = Color.White;
                item.BackColor = AppTheme.PrimaryBlue;
            }
        }

        /// <summary>
        /// Apply theme to link labels
        /// </summary>
        private static void ApplyLinkLabelTheme(LinkLabel linkLabel)
        {
            linkLabel.LinkColor = AppTheme.PrimaryBlue;
            linkLabel.ActiveLinkColor = AppTheme.PrimaryBlueDark;
            linkLabel.VisitedLinkColor = AppTheme.PrimaryBlue;
            linkLabel.Font = AppTheme.BodyFont;
        }
    }
}
