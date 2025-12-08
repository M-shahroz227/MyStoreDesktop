using System.Drawing;

namespace MyStoreDesktop.Theme
{
    /// <summary>
    /// Centralized professional blue theme for the entire application
    /// </summary>
    public static class AppTheme
    {
        // Primary Royal Blue Colors
        public static readonly Color PrimaryBlue = Color.FromArgb(65, 105, 225);          // Royal Blue - Main brand color
        public static readonly Color PrimaryBlueDark = Color.FromArgb(50, 80, 180);       // Darker royal blue for hover
        public static readonly Color PrimaryBlueLight = Color.FromArgb(100, 140, 235);    // Light royal blue for highlights
        public static readonly Color PrimaryBlueVeryLight = Color.FromArgb(225, 235, 255); // Very light royal blue for backgrounds

        // Accent Colors
        public static readonly Color AccentGreen = Color.FromArgb(76, 175, 80);           // Success/Confirm buttons
        public static readonly Color AccentGreenDark = Color.FromArgb(56, 142, 60);       // Darker green for hover
        public static readonly Color AccentRed = Color.FromArgb(244, 67, 54);             // Delete/Cancel buttons
        public static readonly Color AccentRedDark = Color.FromArgb(198, 40, 40);         // Darker red for hover
        public static readonly Color AccentOrange = Color.FromArgb(255, 152, 0);          // Warning/Edit buttons
        public static readonly Color AccentOrangeDark = Color.FromArgb(230, 120, 0);      // Darker orange for hover

        // Neutral Colors
        public static readonly Color BackgroundLight = Color.FromArgb(248, 249, 250);     // Light gray background
        public static readonly Color BackgroundWhite = Color.White;                        // White panels
        public static readonly Color BorderGray = Color.FromArgb(206, 212, 218);          // Border color
        public static readonly Color TextPrimary = Color.FromArgb(33, 37, 41);            // Primary text color
        public static readonly Color TextSecondary = Color.FromArgb(108, 117, 125);       // Secondary text color
        public static readonly Color TextLight = Color.FromArgb(173, 181, 189);           // Light text color

        // Component Specific Colors
        public static readonly Color PanelBackground = Color.White;
        public static readonly Color FormBackground = Color.FromArgb(240, 244, 250);      // Light blue-gray
        public static readonly Color SidebarBackground = Color.FromArgb(33, 37, 41);      // Dark sidebar
        public static readonly Color SidebarText = Color.White;
        public static readonly Color GridHeaderBackground = Color.FromArgb(65, 105, 225); // Royal Blue header
        public static readonly Color GridHeaderText = Color.White;
        public static readonly Color GridAlternateRow = Color.FromArgb(242, 246, 253);    // Light royal blue tint

        // Fonts
        public static readonly Font TitleFont = new Font("Segoe UI", 20F, FontStyle.Bold);
        public static readonly Font HeadingFont = new Font("Segoe UI", 14F, FontStyle.Bold);
        public static readonly Font SubHeadingFont = new Font("Segoe UI", 12F, FontStyle.Bold);
        public static readonly Font BodyFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font SmallFont = new Font("Segoe UI", 9F, FontStyle.Regular);
        public static readonly Font ButtonFont = new Font("Segoe UI", 11F, FontStyle.Bold);

        // Shadows & Effects
        public static readonly Color ShadowColor = Color.FromArgb(50, 0, 0, 0);           // Semi-transparent black
    }
}
