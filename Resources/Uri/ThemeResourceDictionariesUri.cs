namespace Black_Hole.Resources.Uri
{
    internal struct ThemeResourceDictionariesUri
    {
        private static readonly System.Uri BaseUri = new("pack://application:,,,/Black Hole;component/Resources/Themes/");

        public static readonly System.Uri DefaultThemeUri = new(BaseUri + "DefaultTheme.xaml");

        public static readonly System.Uri DarkThemeUri = new(BaseUri + "DarkTheme.xaml");
    }
}
