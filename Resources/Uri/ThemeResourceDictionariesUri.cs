namespace Black_Hole.Resources.Uri
{
    internal struct ThemeResourceDictionariesUri
    {
        private static readonly System.Uri BaseUri = new("pack://application:,,,/Black Hole;component/Resources/Themes/");

        public static readonly System.Uri BlackHoleThemeUri = new(BaseUri + "BlackHole.xaml");

        public static readonly System.Uri WhiteHoleThemeUri = new(BaseUri + "WhiteHole.xaml");
    }
}
